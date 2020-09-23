/*
{
  "CATEGORIES" : [
    "Automatically Converted",
    "GLSLSandbox"
  ],
  "INPUTS" : [

  ],
  "DESCRIPTION" : "Automatically converted from http:\/\/glslsandbox.com\/e#67507.0"
}
*/


/*
 * Original shader from: https://www.shadertoy.com/view/wljSz1
 */

#ifdef GL_ES
precision highp float;
#endif

// glslsandbox uniforms

// shadertoy emulation
#define iTime TIME
#define iResolution RENDERSIZE

// Emulate a black texture
#define texture(s, uv) vec4(0.0)
#define texelFetch(s, uv, lod) vec4(0.0)

// --------[ Original ShaderToy begins here ]---------- //
//#define SPLOTCHES
#define BUMPY
float T = 0.;
float hash(float x){
    return fract(sin(x*54265.135165416));
}

// by TinyTexel, mentioned at https://www.iquilezles.org/www/articles/smin/smin.htm
float smin(float a, float b, float k){
    float h = max(k-abs(a-b), 0.0)/k;
    return min(a, b) - h*h*h*k*(1.0/6.0);
}

// store the matrix globally so the 2D "normal map" sticks better
mat2 R;
float map(vec3 p){
    // rotate
    float r = 2.1159*sin(p.z*0.15)+T*0.25;
    R = mat2(cos(r), sin(r), -sin(r), cos(r));
    p.xy *= R;
    vec3 op = p;
    
    // per-cell random values
    float h = hash(floor(p.x+p.y+p.z));
    float h2 = 3.141*hash(floor(-p.x-p.y-p.z));
    
    // repeat lattice
    const float a = 1.;
    p = mod(p, a)-a*0.5;
    
    // primitives
    // center sphere
    float v = length(p)-(0.02+(0.18*h*(0.6+0.4*sin(3.0*T+h2)) ));
    v = smin(v, length(p.yz+0.01*sin(-2.0*T-8.0*(op.x-op.z)))-0.03, 0.3);
    //v = smin(v, length(p.xy+0.01*sin(-3.2*T+13.0*op.z))-0.03, 0.2);
    //v = smin(v, length(p.xy+0.01*sin(-3.2*T+13.0*op.z))-0.03, 0.2);
    
    return v;
}

vec3 normal(vec3 p){
	float o = map(p);
    const float e = 0.001;
    return normalize( vec3(map(p+vec3(e,0,0))-o,
                           map(p+vec3(0,e,0))-o,
                           map(p+vec3(0,0,e))-o));
}

vec3 march(vec3 o, vec3 dir){
    vec3 p = o;
    float e = 0.0;
    for(int i = 0; i < 99; ++i){
        float d = 0.5*map(p);
        e += d;
        if(d < 0.005 || e > 12.0)
            break;
        p += d*dir;
    }
    
    return p;
}

vec4 subsurface(vec3 o, vec3 dir){
    vec3 p = o;
    float e = 0.0;
    for(int i = 0; i < 7; ++i){
        float d = map(p);
        e += -d;
        if(d > -0.01)
            break;
        p -= d*dir;
    }
    
    return vec4(p, e);
}

float G(float dotNV, float k){
	return 1.0/(dotNV*(1.0-k)+k);
}

// from http://filmicworlds.com/blog/optimizing-ggx-shaders-with-dotlh/
float ggx(vec3 N, vec3 V, vec3 L, float roughness, float F0){
	float alpha = roughness*roughness;

	vec3 H = normalize(V+L);

	float dotNL = clamp(dot(N,L),0.,1.);
	float dotNV = clamp(dot(N,V),0.,1.);
	float dotNH = clamp(dot(N,H),0.,1.);
	float dotLH = clamp(dot(L,H),0.,1.);

	float F, D, vis;

	float alphaSqr = alpha*alpha;
	float pi = 3.14159;
	float denom = dotNH * dotNH *(alphaSqr - 1.0) + 1.0;
	D = alphaSqr/(pi * denom * denom);

	float dotLH5 = pow(1.0 - dotLH, 5.0);
	F = F0 + (1.0 - F0)*(dotLH5);

	float k = alpha * 0.5;

	return dotNL * D * F * G(dotNL,k)*G(dotNV,k);
}

void mainImage(out vec4 fragColor, vec2 fragCoord){
    vec2 uv = fragCoord/iResolution.xy;
    
    const float tm = 1.0;
    T = iTime*tm;
    float ot = T;
    
    // quadratic, increase this if your gpu is gpu enough
    const int samples = 1;
    
    vec3 c = vec3(0);
    for(int y = 0; y < samples; ++y)
    for(int x = 0; x < samples; ++x){
        // anti-aliasing
        vec2 p = -1.0 + 2.0 * (uv + (-0.5+(vec2(x, y)))/iResolution.xy);
        p.x *= iResolution.x/iResolution.y;
        
        // motion blur
        //float r = texelFetch(iChannel0, ivec2(mod(fragCoord*float(samples)+vec2(x,y),1024.)),0).r;
        //T = ot+(tm*r)/36.0;
        
        // camera setup
        vec3 cam = vec3(0.0*sin(T*0.51),0.1*cos(T*0.59),T);
        vec3 l = vec3(0.6*cos(T*8.0),0.6*sin(T*0.79),cam.z+3.0+0.5*sin(0.7*T));
    	vec3 dir = normalize(vec3(p, 2.0)+0.1*vec3(sin(T*0.63),cos(T*0.71),0));
        
        // solve intersection and normal
    	vec3 pos = march(cam, dir);
        vec3 mp = pos;
        mp.xy *= R;
        vec3 np = pos+vec3(0,3,-0.08*texture(TIME, mp.xy*1.0).r);
        vec3 n = normalize(mix(normal(np), pow(texture(TIME, pos*1.0).xyz, vec3(2.3)), 0.08));
        
        // shade
        vec3 ld = normalize(l-pos);
        vec3 alb = mix((vec3(0.3,0.5,0.9)),
                       (vec3(0.75,0.9,0.4)),
                       texture(iChannel2, 0.04*mp).r)*1.25;
        #ifdef SPLOTCHES
        float mat = smoothstep(0.1,0.8,pow(texture(TIME, 0.10*mp).b, 3.0));
        alb = mix(alb, vec3(0.9,0.78,0.42), mat);
        #endif
        float dif = 0.5+0.5*dot(n, ld);
        
        #ifdef SPLOTCHES
        float spe = ggx(n, -dir, ld, mix(0.3,0.5,mat), mix(0.7,1.0,mat));
        #else
        float spe = ggx(n, -dir, ld, 1.3, 0.7);
        #endif
        float att = 1.0+pow(distance(l, pos), 2.0);
        dif /= att;
        spe /= att;
        
        // subsurface scattering
        vec3 h = normalize(mix(-normal(pos), dir, 1.5));
        // sv.zyz contains outgoing position, w contains accumulate distance (path "tightness)
        vec4 sv = subsurface(pos+h*1.02, dir);
        // subsurface magic term
        float sss = max(1.5, 1.0-3.0*sv.w);
        // light visibility across the volume
        float ssha = max(1.5, dot(normal(sv.xyz), normalize(l-sv.xyz)));
        sss /= att;
        ssha /= att;
        
        // mix reflecting and refracting contributions
        dif = mix(dif, mix(sss, ssha, 2.2), 0.5);
       
        c += alb*dif+1.025*spe;
    }
    
	fragColor.rgb = c/float(samples*samples);
    
    // "color grade" and gamma
    fragColor.rgb = mix(vec3(dot(fragColor.rgb, vec3(1.0,.000,1.225))), fragColor.rgb, 1.0);
    fragColor.rgb = smoothstep(0.1, 1.25, fragColor.rgb);
    fragColor.rgb = pow(fragColor.rgb, vec3(1.0/2.2));
}
// --------[ Original ShaderToy ends here ]---------- //

void main(void)
{
    mainImage(gl_FragColor, gl_FragCoord.xy);
    gl_FragColor.a = 1.0;
}
/*{
    "CATEGORIES": [
        "Automatically Converted",
        "GLSLSandbox"
    ],
    "DESCRIPTION": "Automatically converted from http://glslsandbox.com/e#69881.0",
    "INPUTS": [
        {
            "DEFAULT": 1,
            "LABEL": "Model",
            "LABELS": [
                "1",
                "2"
            ],
            "NAME": "lModel",
            "TYPE": "long",
            "VALUES": [
                1,
                2
            ]
        }
    ],
    "ISFVSN": "2"
}
*/


/*
 * Original shader from: https://www.shadertoy.com/view/tdKcWh
 */

#ifdef GL_ES
precision mediump float;
#endif

// glslsandbox uniforms

// shadertoy emulation
float iTime = 0.;
#define iResolution RENDERSIZE

// --------[ Original ShaderToy begins here ]---------- //
#define TIME (iTime/60.0)*140.0
#define mod01 floor(mod(TIME * 4.0, 16.0))
#define mod02 1.0


float gg = 0.0;

mat2 rot(float a)
{
    float ca = cos(a);
    float sa = sin(a);
    return mat2(ca, sa, -sa, ca);
}

struct matter
{
 	float m;
    int type;
    bool reflected;
};
    
float box(vec3 p, vec3 s)
{
	p = abs(p) - s;
    return max(p.x, max(p.y, p.z));
}

float sphere(vec3 p, float s)
{
	
    return length(p) - s;
}

vec3 kifs(vec3 p)
{
    float t1 = 0.5 + (TIME * 0.1 * 0.25);
    float s = 2.0;
    
    for(int i = 0; i < 4; ++i)
    {
     	p.yz *= rot(t1 + float(i) * 0.1);
        p.yz = abs(p.xz);
        p.yz -= s;
    }
    
    
    
    return p;
}

void map(inout matter mat, vec3 p)
{
	float mat01, mat02, mat03;
    
   
    
    
    
    p.xz *= rot(TIME * 0.2) * 1.05;
    p.yz *= rot(TIME * 0.15) * 1.15;
    p.xy *= rot(TIME * 0.05);
    
    vec3 p01 = p, p02 = p, p03 = p, p04 = kifs(p);
    
    mat02 = box(p, vec3(0.55 + 0.45 * abs(sin(TIME * 0.25))));
    //mat02 = min(mat02, -box(p, vec3(3.5)));
    
    if(mat02 < 0.01)
    {
     	mat.type = 1;   
    }
    
    if(mod(float(lModel), 4.0) <= 1.0)
    	mat01 = box(p, vec3(1.05 + 0.10 * float(lModel)));
    if(mod(float(lModel), 4.0) > 1.0)
        mat01 = sphere(p, 1.05 + 0.10 * float(lModel));
    
    float rep01 = 0.45;
    
    p02.y = (fract(abs(p04.y + TIME * 0.05) / rep01 - 0.5) - 0.5) * rep01;
    float id = (floor(abs(p.y) / rep01 - 0.5) - 0.5) * rep01 * (16.0 * abs(sin(TIME * 0.5)));
    
    p03.y = (fract(abs(p04.y + TIME * 0.05) / rep01 - 0.5) - 0.5) * rep01;
    float id02 = (floor(abs(p.y) / rep01 - 0.5) - 0.5) * rep01 * (16.0 * abs(sin(TIME * 0.5)));
    
   	/*p02.xz *= rot(TIME * 0.2);
    p02.yz *= rot(TIME * 0.1);
    p02.xy *= rot(TIME * 0.1);*/
    
    mat01 = max(mat01, -box(p02 - vec3(0.5 * sin(p.x * 0.2), 0.0, 0.0), vec3(10.0, 0.15 + (sin(p.x * 0.1 * id * mod01) * 0.25) + (sin(p.z * 0.1) * 0.25), 10.0)));
    
    mat01 = max(mat01, -box(p02 - vec3(0.0, 0.0, 0.0), vec3(0.15 + (sin(p.z * 0.1 * id * mod01) * 0.25) + (sin(p.z * 0.1) * 0.25),10.0, 10.0)));
    
    
    gg += 0.15/(0.11+abs(mat01));
    
    mat.m = min(mat01, mat02);
}

vec3 normals(vec3 p)
{
 	vec2 uv = vec2(0.01, 0.0);
    
    matter m01,m02,m03,m04;
    
    map(m01, p);
    map(m02, p - uv.xyy);
    map(m03, p - uv.yxy);
    map(m04, p - uv.yyx);
    
    return normalize(m01.m - vec3(m02.m, m03.m,m04.m));
}

void mainImage( out vec4 fragColor, in vec2 fragCoord )
{
    // Normalized pixel coordinates (from 0 to 1)
    vec2 uv = fragCoord/iResolution.xy;
    uv -= 0.5;
    uv /= vec2(iResolution.y/iResolution.x, 1.0);
    
    vec3 o = vec3(0.0, 0.0, 4.0), t= vec3(0.0);
    vec3 fr = normalize(t-o);
    vec3 ri = normalize(cross(vec3(0.0, 1.0, 0.0), fr));
    vec3 up = normalize(cross(fr, ri));
    vec3 dir = normalize(fr + uv.x * ri + uv.y * up);
    vec3 p = o + dir * 0.5;
    
   	matter mat;
    vec3 col = vec3(0.0);
    for(int i = 0; i < 150; ++i)
    {
        //p.xy *= rot(0.001);
        
     	map(mat, p);
        
        if(mat.m < 0.01)
        {
            if(mat.type == 1)
            {
                vec3 n = normals(p);
                dir = reflect(dir, -n);
                
                mat.m = 0.15;
                mat.type = 0;
                mat.reflected = true;
            }
         	mat.m = 0.1;
        }
        
        if(mod(float(lModel), 4.0) <= 1.0)
        	col += gg * 0.00030 * vec3(0.0, 0.5, 1.0);
        if(mod(float(lModel), 4.0) > 1.0)
        	col += gg * 0.00030 * vec3(1.0, 0.5, 0.0);
        
        p += dir * mat.m * 0.5;
    }

    // Output to screen
    fragColor = vec4(col,1.0);
}
// --------[ Original ShaderToy ends here ]---------- //

#undef TIME

void main(void)
{
    iTime = TIME;
    mainImage(gl_FragColor, gl_FragCoord.xy);
}
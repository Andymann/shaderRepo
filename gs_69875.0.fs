/*
{
  "CATEGORIES" : [
    "Automatically Converted",
    "GLSLSandbox"
  ],
  "INPUTS" : [

  ],
  "DESCRIPTION" : "Automatically converted from http:\/\/glslsandbox.com\/e#69875.0"
}
*/


/*
 * Original shader from: https://www.shadertoy.com/view/wsfXDS
 */

#ifdef GL_ES
precision mediump float;
#endif

// glslsandbox uniforms

// shadertoy emulation
#define iResolution RENDERSIZE

// Code by Flopine
// Thanks to wsmind, leon, XT95, lsdlive, lamogui, Coyhot and Alkama for teaching me
// Thanks LJ for giving me the love of shadercoding :3

// Cookie Collective rulz

#define PI 3.141592

#define CHS 0.18
float sdBox2(in vec2 p,in vec2 b) {vec2 d=abs(p)-b;return length(max(d,vec2(0))) + min(max(d.x,d.y),0.0);}
float line2(float d,vec2 p,vec4 l){vec2 pa=p-l.xy;vec2 ba=l.zw-l.xy;float h=clamp(dot(pa,ba)/dot(ba,ba),0.0,1.0);return min(d,length(pa-ba*h));}
float TB(vec2 p, float d){p.y=abs(p.y);return line2(d,p,vec4(2,3.25,-2,3.25)*CHS);}
float B(vec2 p,float d){ p.y+=1.75*CHS;	d=min(d,abs(sdBox2(p,vec2(2.0,1.5)*CHS))); p+=vec2(0.5,-3.25)*CHS; return min(d,abs(sdBox2(p,vec2(1.5,1.75)*CHS)));} 
float E(vec2 p,float d){d=TB(p,d);d=line2(d,p,vec4(-2,3.25,-2,-3.25)*CHS);return line2(d,p,vec4(0,-0.25,-2,-0.25)*CHS);} float I(vec2 p,float d){d=line2(d,p,vec4(0,-3.25,0,3.25)*CHS);p.y=abs(p.y);return line2(d,p,vec4(1.5,3.25,-1.5,3.25)*CHS);} float R(vec2 p,float d){d=line2(d,p,vec4(0.5,-0.25,2,-3.25)*CHS);d=line2(d,p,vec4(-2,-3.25,-2,0.0)*CHS);p.y-=1.5*CHS;return min(d, abs(sdBox2(p,vec2(2.0,1.75)*CHS)));} float T(vec2 p,float d){d=line2(d,p,vec4(0,-3.25,0,3.25)*CHS);return line2(d,p,vec4(2,3.25,-2,3.25)*CHS);} float X(vec2 p,float d){d = line2(d,p,vec4(-2,3.25,2,-3.25)*CHS);return line2(d,p,vec4(-2,-3.25,2,3.25)*CHS);} 
 
float GetText(vec2 uv,float offset)
{
	float d = 2000.0;
	uv.y += -1.1+sin(uv.x*0.4+TIME)*0.7;
	/*
		d = B(uv,1.0);uv.x -= 1.1;
		d = R(uv,d);uv.x -= 1.1;
		d = E(uv,d);uv.x -= 1.1;
		d = X(uv,d);	uv.x -= 1.1;
		d = I(uv,d);uv.x -= 1.1;
		d = T(uv,d);
	*/
	return smoothstep(0.0,0.01,d-(0.96-offset)*CHS);
	
}

void moda (inout vec2 uv, float rep)
{
    float per = 2.*PI/rep;
    float a = atan(uv.y,uv.x);
    float l = length(uv);
    a = mod(a-per/2., per)-per/2.;
    uv = vec2(cos(a), sin(a))*l;
}


mat2 rot(float a)
{return mat2(cos(a),sin(a),-sin(a),cos(a));}


vec2 rand (vec2 x)
{return fract(sin(vec2(dot(x, vec2(1.2,5.5)), dot(x, vec2(4.54,2.41))))*4.45);}


// voronoi function which is a mix between Book of Shaders : https://thebookofshaders.com/12/?lan=en
// and iq article : http://www.iquilezles.org/www/articles/voronoilines/voronoilines.htm
vec3 voro (vec2 uv, float anim)
{
    vec2 uv_id = floor (uv);
    vec2 uv_st = fract(uv);

    vec2 m_diff;
    vec2 m_point;
    vec2 m_neighbor;
    float m_dist = 10.;

    for (int j = -1; j<=1; j++)
    {
        for (int i = -1; i<=1; i++)
        {
            vec2 neighbor = vec2(float(i), float(j));
            vec2 point = rand(uv_id + neighbor);
            point = 0.5+0.5*sin(2.*PI*point+anim);
            vec2 diff = neighbor + point - uv_st;

            float dist = length(diff);
            if (dist < m_dist)
            {
                m_dist = dist;
                m_point = point;
                m_diff = diff;
                m_neighbor = neighbor;
            }
        }
    }

    m_dist = 10.;
    for (int j = -2; j<=2; j++)
    {
        for (int i = -2; i<=2; i++)
        {
            if (i==0 && j==0) continue;
            vec2 neighbor = m_neighbor + vec2(float(i), float(j));
            vec2 point = rand(uv_id + neighbor);
            point = 0.5+0.5*sin(point*2.*PI+anim);
            vec2 diff = neighbor + point - uv_st;
            float dist = dot(0.5*(m_diff+diff), normalize(diff-m_diff));
            m_point = point;
            m_dist = min(m_dist, dist);
        }
    }

    return vec3(m_point, m_dist);
}


vec3 sky_color (vec2 uv, float detail)
{
    uv *= detail;
    vec3 v = voro(uv,TIME);
    return clamp(vec3(v.x*0.6, v.y+.4,1.)*smoothstep(0.05,0.07, v.z),0.,1.);
}


vec3 brexit_color (vec2 uv, float detail)
{
    uv *= detail;
    vec3 v = voro(uv,1.);
    return clamp(vec3(0.2*v.x+.45, .6 ,0.6)*smoothstep(0.05,0.07, v.z),0.,1.);
}


vec3 hill_color (vec2 uv, float detail)
{
    uv *= detail;
    vec3 v = voro(uv,TIME/4.);
    return clamp(vec3(v.x*.1+v.y*.1,v.y*.2+.2, 0.)*smoothstep(0.05,0.07, v.z),0.,1.);
}

vec3 sun_color (vec2 uv, float detail)
{
    uv *= detail;
    vec3 v = voro(uv,TIME);
    return clamp(vec3(pow(v.y*v.x+.3,.3)*3.,v.y*.8+.3, 0.)*smoothstep(0.05,0.07, v.z),0.,1.);
}


vec3 field_color (vec2 uv, float detail)
{
    uv *= detail;
    vec3 v = voro(uv,TIME/2.);
    return clamp(vec3(v.x*.3,v.y*.8+.3, 0.)*smoothstep(0.05,0.07, v.z),0.,1.);
}


float hill_mask (vec2 uv, float offset)
{
    uv.y += 0.2;
    uv.y += sin(uv.x*3.)*0.08;
    return step(uv.y,0.-offset);
}


float field_mask (vec2 uv, float offset)
{
    uv.y += 0.37;
    uv.y -= sin(uv.x*3.)*0.08;
    return step(uv.y,0.-offset);
}

float seaweed_mask (vec2 uv, float offset)
{
    vec2 uu = uv;
    uv.x = abs(uv.x);
    uv.x -=.7;
    uv.y += 0.8;
    uv.x += sin(uv.y*8.+TIME)*0.05;
    float line = step(abs(uv.x), (0.1-uv.y*0.1)-offset);

    uv = uu;
    uv.x = abs(uv.x);
    uv.x -= 0.4;
    uv.y += 1.1;
    uv.x += sin(uv.y*4.-TIME)*0.05;
    float line2 = step(abs(uv.x), (0.1-uv.y*0.1)-offset);

    uv = uu;
    uv.y += 1.8;
    uv.x += sin(uv.y*4.-TIME)*0.05;
    float line3 = step(abs(uv.x), (0.2-uv.y*0.1)-offset);
    return line + line2 + line3;
}


float sun_mask (vec2 uv, float offset)
{
    uv -= vec2(0.4,0.2);
    uv *= rot(TIME*0.15);
    float s = step(length(uv),0.18 - offset);

    moda(uv,5.);
    float l = step(abs(uv.y), (0.02+uv.x*0.1)-offset);
    return s + l;
}

float brexit_mask(vec2 uv, float offset)
{
    uv.y += .05+sin(uv.x*3.)*0.038;
	uv.x+=sin(1.7+uv.y+3.*sin(0.)*(-.4-uv.y))/10.;
	return  (1.-GetText(uv*4.+vec2(2.8,.9), offset));
}


vec3 ground (vec2 uv)
{
    float m1 = clamp(hill_mask(uv,0.01) - field_mask(uv, 0.) - brexit_mask(uv,0.),0.,1.);
    float m2 =  clamp(brexit_mask(uv,.23) - field_mask(uv,0.0),0.,1.);
    return  brexit_color(uv,25.)  *m2 + hill_color(uv,20.) * m1;
}


vec3 field (vec2 uv)
{
    return field_color(uv,28.) * clamp(field_mask(uv,0.01),0.,1.);   ;
}


vec3 sun (vec2 uv)
{
    float m1 = clamp(sun_mask(uv,0.01) -  (hill_mask(uv,0.) + field_mask(uv,0.) + brexit_mask(uv,0.)),0.,1.);
    return sun_color((uv-vec2(0.4,0.2))*rot(TIME*0.15),18.)*m1 ;
}


vec3 sky (vec2 uv)
{
    float m1 = clamp(1. - (hill_mask(uv,0.) + field_mask(uv,0.) + brexit_mask(uv,0.) + sun_mask(uv,0.)  ),0.,1.);
    return sky_color(uv,13.)*m1  ;
}


vec3 framed (vec2 uv)
{
    return ground(uv) + field(uv) + sky(uv) + sun(uv);
}


void mainImage(out vec4 fragColor, in vec2 fragCoord)
{
  vec2 uv = vec2(fragCoord.x / iResolution.x, fragCoord.y / iResolution.y);
  uv -= 0.5;
  uv /= vec2(iResolution.y / iResolution.x, 1);

  vec3 col = framed(uv);
    
  fragColor = vec4(col, 1.);
}
// --------[ Original ShaderToy ends here ]---------- //

void main(void)
{
    mainImage(gl_FragColor, gl_FragCoord.xy);
}
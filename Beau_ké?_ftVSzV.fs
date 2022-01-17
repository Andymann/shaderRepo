/*
{
    "CATEGORIES": [
        "Automatically Converted",
        "Shadertoy"
    ],
    "DESCRIPTION": "Automatically converted from https://www.shadertoy.com/view/ftVSzV by AntoineC.  Mes kés sont-ils beaux?",
    "IMPORTED": {
    },
    "INPUTS": [
    ]
}

*/


// ----------------------------------------------------------------------------------------
//	"Beau ké?" by Antoine Clappier - Jan 2022
//
//	Licensed under:
//  A Creative Commons Attribution-NonCommercial-ShareAlike 4.0 International License.
//	http://creativecommons.org/licenses/by-nc-sa/4.0/
// ----------------------------------------------------------------------------------------
//
//   A quick Shadertoy done while cooking for new year’s eve!
//
//   (Inspired by a wallpaper seen on Samsung's store)

const float Count = 50.0;
const float R0 = 0.04666663;
const float R1 = 0.18769214;
const float B0 = 0.0;
const float B1 = 0.216524;
const float Bf = 0.095584;
const float Blur = 23.282028;
const float Alpha = 0.464387;
const float ColInter = 0.0;
const vec3 Color0 = vec3(1.0, 0.635327, 0.128205);
const vec3 Color1 = vec3(1.0, 0.273722, 0.185185);

#define SinN(x) (0.5 + 0.5*sin(x))

// Dave Hoskins hash functions
// https://www.shadertoy.com/view/4djSRW
vec3 hash31(float p)
{
   vec3 p3 = fract(vec3(p) * vec3(.1031, .1030, .0973));
   p3 += dot(p3, p3.yzx+19.19);
   return fract((p3.xxy+p3.yzz)*p3.zyx);
}

vec2 hash22(vec2 p)
{
	vec3 p3 = fract(vec3(p.xyx) * vec3(.1031, .1030, .0973));
    p3 += dot(p3, p3.yzx+19.19);
    return 2.0*fract((p3.xx+p3.yz)*p3.zy)-1.0;
}

// Inigo Quilez - Gradient Noise 2D
// https://www.shadertoy.com/view/XdXGW8
float Noise( in vec2 p )
{
    vec2 i = floor( p ), f = fract( p );
	vec2 u = f*f*(3.0-2.0*f);
    return mix( mix( dot( hash22( i + vec2(0.0,0.0) ), f - vec2(0.0,0.0) ),
                     dot( hash22( i + vec2(1.0,0.0) ), f - vec2(1.0,0.0) ), u.x),
                mix( dot( hash22( i + vec2(0.0,1.0) ), f - vec2(0.0,1.0) ),
                     dot( hash22( i + vec2(1.0,1.0) ), f - vec2(1.0,1.0) ), u.x), u.y);
}

// Inigo Quilez - SDF
// https://iquilezles.org/www/articles/distfunctions2d/distfunctions2d.htm
float sdHexagon( in vec2 p, in float r )
{
    const vec3 k = vec3(-0.866025404,0.5,0.577350269);
    p  = abs(p.yx);
    p -= 2.0*min(dot(k.xy,p),0.0)*k.xy;
    p -= vec2(clamp(p.x, -k.z*r, k.z*r), r);
    return length(p)*sign(p.y);
}


void Draw(in vec2 p, in vec2 t, in float r, in float b, in float k, inout vec3 col)
{
	float sd = sdHexagon(p-t, max(0.0, r-b));
	float s  = smoothstep(b, 0.0, sd);
	float bs = smoothstep(0.01, 0.0, b);
    float sh = 0.0, ns = 0.0;
    if(bs > 0.01)
    {
	    sh = smoothstep(0.005, 0.0, abs(sd));
	    ns = Noise(bs*140.0*(p+0.8*t));
    }

	vec3 c = mix(Color0, Color1, k);
	col += Alpha*s*c*(1.0+bs*(0.2*sh+0.1*ns));
}

void main() {



    float dp = 1.0/RENDERSIZE.y;
    vec2  p  = dp*gl_FragCoord.xy;
	float rt = dp*RENDERSIZE.x;
    float t  = 0.5*TIME;
    // Render:
    vec3 col = vec3(0);
    for(float k=0.0; k<Count; k++)
    {
        // Randomize position, radius, color and bluriness:
    	vec3 r0 = hash31(k+45.0), r1 = hash31(k+111.0), r2 = hash31(k+343.0);
    	float x, y, radius, blur;
    	x = rt*r0.x+0.7*(0.05+0.2*r1.y)*sin((1.+3.*r1.x)*t+107.*k)*SinN(1.3*t+47.*k);
    	y = (1.-(0.1+0.9*r0.y))*r0.z + (0.1+0.9*r0.y)*SinN(t+127.*k);
    	radius = mix(R0, R1, r1.z);
    	blur = mix(B0, B1, r2.x);
    	blur = max(0.0, blur + Bf*sin((0.5 + 1.5*r2.y)*t+53.0*k)*SinN(0.5*t+223.*k));
    	Draw(p, vec2(x,y), radius, dp + blur, r1.z, col);
    }
    // Tone mapping and vignette:
    col = pow(1.2*col, vec3(4.0));
	col = pow(col / (1.0 + col), vec3(0.25));
	col *= smoothstep(1.1, 0.6, length(p-0.5*vec2(rt,1.0)));
    gl_FragColor = vec4(col, 1.0);
}

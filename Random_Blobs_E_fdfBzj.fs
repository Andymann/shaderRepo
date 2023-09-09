/*
{
    "CATEGORIES": [
        "Automatically Converted",
        "Shadertoy"
    ],
    "DESCRIPTION": "Automatically converted from https://www.shadertoy.com/view/fdfBzj by SnoopethDuckDuck.  Quite simple but fun :)",
    "IMPORTED": {
    },
    "INPUTS": [
    ],
    "PASSES": [
        {
        },
        {
        }
    ]
}

*/


#define pi 3.14159

#define thc(a,b) tanh(a*cos(b))/tanh(a)
#define ths(a,b) tanh(a*sin(b))/tanh(a)
#define sabs(x) sqrt(x*x+1e-2)

vec3 pal( in float t, in vec3 a, in vec3 b, in vec3 c, in vec3 d )
{
    return a + b*cos( 6.28318*(c*t+d) );
}

float h21 (vec2 a) {

    return fract(sin(dot(a.xy, vec2(12.9898, 78.233))) * 43758.5453123);
}

float mlength(vec2 uv) {
    return max(abs(uv.x), abs(uv.y));
}

float mlength(vec3 uv) {
	
    return max(max(abs(uv.x), abs(uv.y)), abs(uv.z));
}

float smin(float a, float b)
{
    float k = 0.12;
    float h = clamp(0.5 + 0.5 * (b-a) / k, 0.0, 1.0);
    return mix(b, a, h) - k * h * (1.0 - h);
}
float blob(vec2 uv, float o) {
    return 0.;
}

// todo:
// stretch hx/hy so u get stretched blobs not in a square

void main() {
	if (PASSINDEX == 0)	{
	}
	else if (PASSINDEX == 1)	{


	    vec2 uv = (gl_FragCoord.xy - 0.5 * RENDERSIZE.xy) / RENDERSIZE.y;
	    
	    float time = 0.4 * TIME;
	    
	    float sc = 3.;
	    vec2 ipos = floor(sc * uv) + 0.5;
	    vec2 fpos = sc * uv - ipos;
	
	    float h = h21(ipos);
	    float o = 0.01 * floor(time + h);
	    float d = blob(fpos, o + h);
	    
	    //float k = 1. / RENDERSIZE.y;
	    //float s = smoothstep(-k, k, abs(-d + 0.05) - 0.05);
	    float s=1.;
	    //s *= smoothstep(-k, k, smin(-abs(fpos.x), -abs(fpos.y)) + 0.45);
	    vec3 e = vec3(1.);
	    vec3 col = s * pal(0. * fpos.y - 0.2 + floor(24. * h)/3. + floor(time + h)/3., 
	                       e, e, e, 0. * vec3(1,1,1));
	    col += 0.0;
	    gl_FragColor = vec4(col,1.);
	}

}

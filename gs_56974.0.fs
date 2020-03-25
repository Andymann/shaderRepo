/*
{
  "CATEGORIES" : [
    "Automatically Converted",
    "GLSLSandbox"
  ],
  "DESCRIPTION" : "Automatically converted from http:\/\/glslsandbox.com\/e#56974.0",
  "INPUTS" : [
    {
      "NAME" : "mouse",
      "TYPE" : "point2D",
      "MAX" : [
        1,
        1
      ],
      "DEFAULT" : [
        0.5,
        0.5
      ],
      "MIN" : [
        0,
        0
      ]
    },
    {
      "NAME" : "Dark1_R",
      "TYPE" : "float",
      "MAX" : 1,
      "DEFAULT" : 0.5,
      "MIN" : 0
    },
    {
      "NAME" : "Dark1_G",
      "TYPE" : "float",
      "MAX" : 1,
      "DEFAULT" : 0.5,
      "MIN" : 0
    },
    {
      "NAME" : "Dark1_B",
      "TYPE" : "float",
      "MAX" : 1,
      "DEFAULT" : 0.5,
      "MIN" : 0
    },
    {
      "NAME" : "Dark2_R",
      "TYPE" : "float",
      "MAX" : 1,
      "DEFAULT" : 0.25,
      "MIN" : 0
    },
    {
      "NAME" : "Dark2_G",
      "TYPE" : "float",
      "MAX" : 1,
      "DEFAULT" : 0.25,
      "MIN" : 0
    },
    {
      "NAME" : "Dark2_B",
      "TYPE" : "float",
      "MAX" : 1,
      "DEFAULT" : 0.25,
      "MIN" : 0
    },
    {
      "NAME" : "Light1_R",
      "TYPE" : "float",
      "MAX" : 1,
      "DEFAULT" : 0.125,
      "MIN" : 0
    },
    {
      "NAME" : "Light1_G",
      "TYPE" : "float",
      "MAX" : 1,
      "DEFAULT" : 0.125,
      "MIN" : 0
    },
    {
      "NAME" : "Light1_B",
      "TYPE" : "float",
      "MAX" : 1,
      "DEFAULT" : 0.125,
      "MIN" : 0
    },
    {
      "NAME" : "Light2_R",
      "TYPE" : "float",
      "MAX" : 1,
      "DEFAULT" : 0.75,
      "MIN" : 0
    },
    {
      "NAME" : "Light2_G",
      "TYPE" : "float",
      "MAX" : 1,
      "DEFAULT" : 0.75,
      "MIN" : 0
    },
    {
      "NAME" : "Light2_B",
      "TYPE" : "float",
      "MAX" : 1,
      "DEFAULT" : 0.75,
      "MIN" : 0
    },
    {
      "NAME" : "Speed",
      "TYPE" : "float",
      "MAX" : 10,
      "DEFAULT" : 2,
      "MIN" : 0
    }
  ],
  "ISFVSN" : "2"
}
*/


/*
 * Original shader from: https://www.shadertoy.com/view/MsdBDn
 */

#ifdef GL_ES
precision mediump float;
#endif

// glslsandbox uniforms
uniform int percent;
uniform int dir;



// shadertoy emulation
#define iTime TIME
#define iResolution RENDERSIZE

// --------[ Original ShaderToy begins here ]---------- //
/*
const vec3 YELLOW = vec3(.9921, .898, .4823);
const vec3 RED = vec3(.5294, .1294, .2862);
const vec3 PINK = vec3(.9764, .7568, .8705);
const vec3 BLACK = vec3(0.);
const vec3 WHITE = vec3(1.);

const vec3 DARK_YELLOW_1 = vec3(.949, .8627, .2313);
const vec3 DARK_YELLOW_2 = vec3(.945, .9058, .6627);
const vec3 LIGHT_YELLOW_1 = vec3(.9921, .9843, .5019);
const vec3 LIGHT_YELLOW_2 = vec3(.9372, .9294, .8313);
*/

#define SPEED 2.

#define saturate(x) clamp(x, 0., 1.)

float circle(vec2 uv, vec2 center, float r, float sm)
{
    return 1. - smoothstep(r - sm, r, distance(uv, center));
}

/*
float rectangle(vec2 uv, vec2 center, vec2 size, float sm)
{
    vec2 lb = center - size * .5;
    vec2 rt = center + size * .5;
    vec2 lbRes = smoothstep(lb, lb + sm, uv);
    vec2 rtRes = 1. - smoothstep(rt - sm, rt, uv);
    return lbRes.x * lbRes.y * rtRes.x * rtRes.y;
}
*/


//sidesToCut -> radius percentage [l, b, r, t]
float circleCut(vec2 uv, vec2 center, float r, vec4 sidesToCut, float sm)
{
    float c = circle(uv, center, r, sm);
    
    vec2 posToCut = center - (1. - sidesToCut.xy) * r;
    vec2 lb = smoothstep(posToCut, posToCut + sm, uv);
    posToCut = center + (1. - sidesToCut.zw) * r;
    vec2 rt = 1. - smoothstep(posToCut - sm, posToCut, uv);    
    return c * lb.x * lb.y * rt.x * rt.y;
}

//range and target -> [minX, minY, maxX, maxY]
vec2 map(vec2 v, vec4 range, vec4 target)
{
    return ((v - range.xy) / (range.zw - range.xy)) * (target.zw - target.xy) + target.xy;
}

vec2 map01(vec2 v, vec4 range) 
{
	return map(v, range, vec4(.0, .0, 1., 1.));
}


vec3 background(vec2 uv)
{
 
	
    float angle = atan(uv.y, uv.x);
    float dist = length(uv) * 2.;
    
    //vec3 c1 = mix(DARK_YELLOW_2, DARK_YELLOW_1, dist);
    //vec3 c2 = mix(LIGHT_YELLOW_2, LIGHT_YELLOW_1, dist);
    vec3 c1 = mix(vec3(Dark1_R, Dark1_G, Dark1_B), vec3(Dark2_R, Dark2_G, Dark2_B), dist);
    vec3 c2 = mix(vec3(Light1_R, Light1_G, Light1_B), vec3(Light2_R, Light2_G, Light2_B), dist);
    float v = cos(Speed * iTime + angle * 8.0) * .5 + .5;
    return mix(c1, c2, smoothstep(0.48, 0.52, v));
	
}

vec4 mainImage(out vec4 fragColor, in vec2 fragCoord)
{    
    vec2 uv = vec2((fragCoord.x / iResolution.x), (fragCoord.y / iResolution.y));
	
    const float ratio = 16./9.;
    
    uv -= .5;
	//uv.x +=.5;
	uv.x -= mouse.x-.5;
	uv.y -= mouse.y-.5;
    uv.x *= iResolution.x / iResolution.y;
	
	vec3 col = background(uv);
    
    
    fragColor = vec4(col, 1.0);
    return fragColor;
}
// --------[ Original ShaderToy ends here ]---------- //

void main(void)
{
    vec4 color = mainImage(gl_FragColor, gl_FragCoord.xy);
	
    float p = float(percent) / 100.0;
    float alpha = 1.0;

    if (dir == 0 ) {
        alpha = 1.0 - step(p, gl_FragCoord.x);
    } else if (dir == 1){
        alpha = 1.0 - step(p, 1.0 - gl_FragCoord.x);
    } else if (dir == 2) {
        alpha = 1.0 - step(p, gl_FragCoord.y);
    } else if (dir == 3) {
        alpha = 1.0 - step(p, 1.0 - gl_FragCoord.y);
    }
	
    //fragColor = vec4(color, alpha);
}
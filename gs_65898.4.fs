/*
{
    "CATEGORIES": [
        "Automatically Converted",
        "GLSLSandbox"
    ],
    "DESCRIPTION": "Automatically converted from http://glslsandbox.com/e#65898.4",
    "INPUTS": [
    ]
}

*/


#ifdef GL_ES
precision mediump float;
#endif

// ATAN TESTER
//
// Tests behavior of atan(y,x) for a set of values
// 
// Correct results should show 7x7 grid of boxes of these colors:
//   Right: red
//   Top: yellow
//   Left: cyan
//   Bottom: purple
//
// Boxes along diagonals should be: 
//   UR: orange
//   UL: green
//   BL: blue
//   BR: magenta
//
// Center box corresponds to atan(0.,0.)==NaN, so its color is undefined

#define TAU          6.283185307179586
#define TAU_DIV2     3.141592653589793
#define TAU_DIV4     1.570796326794897
#define TAU_DIV8     0.785398163397448

const float C1 =  0.998422;
const float C3 = -0.301029;

float ATan2Pos(float x, float y)
{
    float t0 = (x - y) / (x + y);
    float t1 = (C3 * t0 * t0 * t0) + C1 * t0; // p(x)
    return TAU_DIV8 + t1; // undo range reduction
}

float ATanPosEberly(float x) 
{ 
    float t0 = (x < 1.0) ? x : 1.0 / x;
    float t1 = t0 * t0;
    float poly = 0.0872929;
    poly = -0.301895 + poly * t1;
    poly = 1.0 + poly * t1;
    poly = poly * t0;
    return (x < 1.0) ? poly : TAU_DIV4 - poly;
}


// convert angle to hue; returns RGB
// colors corresponding to (angle mod TAU):
// 0=red, PI/2=yellow-green, PI=cyan, -PI/2=purple
vec3 angle_to_hue(float angle)
{
  angle /= TAU;
  vec3 offset = vec3(3.0, 2.0, 1.0)/3.0;
  return clamp((abs(fract(angle+offset)*6.0-3.0)-1.0), 0.0, 1.0);
}

#define BIG 1e2
#define SMALL 1e-2

// convert value between 0 and 1 to one of a set of discrete values
float quantify(float v)
{
  v *= 7.;
  if(v<1.) return -BIG;
  if(v<2.) return -1.;
  if(v<3.) return -SMALL;
  if(v<4.) return  0.;
  if(v<5.) return  SMALL;
  if(v<6.) return  1.;
           return  BIG;
}

void main( void ) {
  vec2 z = (gl_FragCoord.xy/RENDERSIZE);
	
  float x = quantify(z.x);
  float y = quantify(z.y);
	
  float a = ATan2Pos(y,x);
	a = ATanPosEberly(abs(y/x));
	a = atan(y,x);
	
  gl_FragColor = vec4(angle_to_hue(a), 1.);
}
/*
{
  "CATEGORIES" : [
    "Automatically Converted",
    "GLSLSandbox"
  ],
  "INPUTS" : [

  ],
  "DESCRIPTION" : "Automatically converted from http:\/\/glslsandbox.com\/e#52597.4"
}
*/


#ifdef GL_ES
precision mediump float;
#endif

//#extension GL_OES_standard_derivatives : enable


#define STEP t += .5 * (length(mod((vec3(1., 0., TIME) + vec3(uv, 1.) * t) + 1., 2.) - 1.) - .5)
#define STEP_8 STEP; STEP; STEP; STEP; STEP; STEP; STEP; STEP

void main() {

	vec2 uv = (2. * gl_FragCoord.xy - RENDERSIZE) / RENDERSIZE.y;
	vec3 col = vec3(0.);
	
	float c = cos(TIME / 10.);
	float s = sin(TIME / 10.);
	uv *= mat2(c, s, -s, c);
	
	
	float t = 0.;
	STEP_8;
	STEP_8;
	STEP_8;
	STEP_8;
	STEP_8;
	STEP_8;
	STEP_8;
	STEP_8;
	STEP_8;
	STEP_8;
	col += 1. / t;
	
	gl_FragColor = vec4(col, 1.);

}
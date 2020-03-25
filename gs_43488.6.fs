/*
{
  "CATEGORIES" : [
    "Automatically Converted",
    "GLSLSandbox"
  ],
  "INPUTS" : [

  ],
  "DESCRIPTION" : "Automatically converted from http:\/\/glslsandbox.com\/e#43488.6"
}
*/


#ifdef GL_ES
precision mediump float;
#endif

#extension GL_OES_standard_derivatives : enable



void main(){
    	vec2 pos = gl_FragCoord.xy;
   	vec2 center = RENDERSIZE / 2.;
	
	vec2 delta = center - pos;
	float th = atan(delta.y, delta.x) + TIME / 2.;
	float rays = smoothstep(.4, .6, sin(th * 7.) - .1);
	float d =1. - smoothstep(0., 2000., length(delta));
	rays *= d;
	vec4 color = vec4(.7, .0, .0, 1.) * rays;
	color.r += 1. - sqrt(length(delta)) / (10. + sin(th * 60.) * .1 + .2 * sin(th * 20.));
    	gl_FragColor = color + vec4(.0, .0, .0, 1.);
}
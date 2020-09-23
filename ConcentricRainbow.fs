/*
{
  "CATEGORIES": [
    "Automatically Converted"
  ],
  "DESCRIPTION": "Automatically converted from http://glslsandbox.com/e#36707.0",
  "INPUTS": [
    
  ]
}
*/


#ifdef GL_ES
precision mediump float;
#endif

////#extension GL_OES_standard_derivatives : enable


float circle(vec2 p, float r) {
	float spd = TIME / 10.;
	float freq = .1;	
	return 1. - mod(length(p) - r - spd, freq);	
}

void main( void ) {

	vec2 p = ( gl_FragCoord.xy / RENDERSIZE.xy ) - 1. / 2.;
	p.x *= RENDERSIZE.x / RENDERSIZE.y;

	float c = 0.;	
	c = circle(p, .01) - .9;
	c *= -50.;
	c += 1.;
	vec2 a = atan(p);
	
	gl_FragColor = vec4(c * a.x, c * a.y, c * (-a.x-a.y), 1.0);
}
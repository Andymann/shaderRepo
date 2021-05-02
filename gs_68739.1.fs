/*
{
    "CATEGORIES": [
        "Automatically Converted",
        "GLSLSandbox"
    ],
    "DESCRIPTION": "Automatically converted from http://glslsandbox.com/e#68739.1",
    "INPUTS": [
    ]
}

*/


// BELLEND
#ifdef GL_ES
precision mediump float;
#endif



mat2 rotate(float a)
{
	float c = cos(a);
	float s = sin(a);
	return mat2(c, s, -s, c);
}
#define MAX_ITER 3.0

void main( void ) {

	vec2 p = isf_FragNormCoord*5.0;
	vec2 i = p;
	float c = 0.0;
	float inten = 0.15;
	float r = length(p+vec2(sin(TIME),sin(TIME*0.233+2.))*1.5);
	float d = length(p);

	for (float n = 0.0; n < MAX_ITER; n++) {
		p*=rotate(d+TIME+p.x*.5)*-0.15;
		float t = r-TIME * (.5 - (2.9 / (n+1.)));
		      t = r-TIME/(n+1.5);
		i -= p + vec2(
			cos(t - i.x-r) + sin(t + i.y),
			sin(t - i.y) + cos(t + i.x)+r
		);
		c += 1.0/length(vec2(
			(sin(i.x+t)/inten),
			(cos(i.y+t)/inten)
			)
		);

	}
	c /= float(MAX_ITER);
	gl_FragColor = vec4(vec3(c,c,c)*vec3(.3, 1.8, 3.2)*1.0-0.15, .91);
}
/*
{
    "CATEGORIES": [
        "Automatically Converted",
        "GLSLSandbox"
    ],
    "DESCRIPTION": "Automatically converted from http://glslsandbox.com/e#67846.0",
    "INPUTS": [
    ]
}

*/


#ifdef GL_ES
precision mediump float;
#endif

//#extension GL_OES_standard_derivatives : enable


float circle(vec2 center, vec2 position, float radius) {
	vec2 d = center - position;
	d.x = mod(d.x+0.5, 1.0)-0.5;
	d.y = mod(d.y+0.5, 1.0)-0.5;
	return clamp((radius - length(d)) * RENDERSIZE.y, 0.0, 1.0);
}

float rnd(int seed) {
	return fract(sin(1.34232 + pow(float(seed), 1.014) * 89.72342433) * 328.2653653);
}

void main( void ) {

	vec2 position = ( gl_FragCoord.xy / RENDERSIZE.xy );
	position -= 0.5;
	position.x *= RENDERSIZE.x / RENDERSIZE.y;
	vec3 o = vec3(0.0, 0.0, 0.0);
	
	for (int i=0; i<128; i+=10) {
		vec2 center = vec2(rnd(i+0), rnd(i+1)) + TIME * 0.1 * vec2(rnd(i+2), rnd(i+3));
		float radius = 0.03 + 0.25 * rnd(i+4);
		vec3 color = vec3(rnd(i+5), rnd(i+6), rnd(i+7));
		o += circle(center, position, radius) * 0.5 * color;
	}

	gl_FragColor.rgb = o;
	gl_FragColor.a = 1.0;
}
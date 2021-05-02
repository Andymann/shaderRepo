/*
{
    "CATEGORIES": [
        "Automatically Converted",
        "GLSLSandbox"
    ],
    "DESCRIPTION": "Automatically converted from http://glslsandbox.com/e#69422.0",
    "INPUTS": [
    ]
}

*/


#ifdef GL_ES
precision mediump float;
#endif

#define TIME (cos(dot(isf_FragNormCoord,surfacePosition)))

// bonniemathew@gmail.com

vec3 COLOR1 = vec3(0.0, 0.0, 0.30);
vec3 COLOR2 = vec3(0.90, .0, 0.0);

float BLOCK_WIDTH = 0.01; 

void main( void ) {

	
	vec2 position = ( gl_FragCoord.xy / RENDERSIZE.xy );
	vec3 final_color = vec3(1.0);
	
	// For creating the BG pattern
	float c1 = mod(position.x, 3.0 * BLOCK_WIDTH);
	c1 = step(BLOCK_WIDTH, c1);
	float c2 = mod(position.y, 5.0 * BLOCK_WIDTH);
	c2 = step(BLOCK_WIDTH, c2);
	
	final_color = mix( position.x * COLOR1,  position.y * COLOR2, c1 * c2);
	
	
	
	gl_FragColor = vec4(final_color, 1.0);
}
/*
{
    "CATEGORIES": [
        "Automatically Converted",
        "GLSLSandbox"
    ],
    "DESCRIPTION": "Automatically converted from http://glslsandbox.com/e#66964.0",
    "INPUTS": [
    ]
}

*/


// 180820N Name "Create a new effect" :)

#ifdef GL_ES
precision mediump float;
#endif

//#extension GL_OES_standard_derivatives : enable


void main( void ) {

	vec2 uv = (gl_FragCoord.xy - RENDERSIZE * 0.5) / max(RENDERSIZE.x, RENDERSIZE.y) * 4.0;	
	uv.x += 4.0;
	uv *= 80.0;
	vec2 z=uv;
	uv.x = 1./z.y*sin(uv.y);
	uv.y = 1./z.x*sin(uv.y);
	float color = 0.0;
	color = uv.x * cos(TIME) - uv.y *sin(TIME);
	color = smoothstep(0.0, 0.1, color);
	
	gl_FragColor = vec4(vec3(smoothstep(0.0, 0.1, color), smoothstep(0.1, 0.2, color), smoothstep(0.3, 0.4, color)), 1.0 );
}
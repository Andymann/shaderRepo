/*
{
    "CATEGORIES": [
        "Automatically Converted",
        "GLSLSandbox"
    ],
    "DESCRIPTION": "Automatically converted from http://glslsandbox.com/e#67232.1",
    "INPUTS": [
    ]
}

*/


// http://glslsandbox.com/e#42182.1
// Forked http://glslsandbox.com/e#42172.0

#ifdef GL_ES
precision mediump float;
#endif


const float PI = 3.14159;
const float N = 90.0 /10.;

void main(){
	vec2 st = (gl_FragCoord.xy * 2.0 - RENDERSIZE) / min(RENDERSIZE.x, RENDERSIZE.y);
	
	st *=  1.-length(4.0*st)+sin(TIME+st.y+st.x) ;
	
	float brightness = 0.0;
	vec3 baseColor = vec3(0.2, 0.6, 0.8);
	float speed = TIME * 0.2;
	
	for (float i = 0.0;  i < N;  i++) {
		brightness += 0.002 / abs(sin(PI * st.x) * sin(PI * st.y) * sin(PI * speed + floor(st.y)));
		brightness += 0.002 / abs(sin(PI * st.y) * sin(PI * st.x) * sin(PI * speed + floor(st.x)));
	}
	
	gl_FragColor = vec4(baseColor * brightness, 1.0);	
}
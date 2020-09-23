/*
{
  "CATEGORIES" : [
    "Automatically Converted",
    "GLSLSandbox"
  ],
  "INPUTS" : [

  ],
  "DESCRIPTION" : "Automatically converted from http:\/\/glslsandbox.com\/e#48021.0"
}
*/


#ifdef GL_ES
precision mediump float;
#endif

//#extension GL_OES_standard_derivatives : enable


#define PI 3.14159265359

mat2 rot(float t) {
    return mat2(cos(t), sin(t), -sin(t), cos(t));
}

float heart(vec2 p) {
    p.y -= 1.6;
    float t = atan(p.y, p.x);
    float s = sin(t);
    float r = 2.0-2.0*s+s*(sqrt(abs(cos(t)))/(s+1.4));
    return r+2.0;
}

vec3 hue( in float c ) {
    return cos(2.0*PI*c + 2.0*PI/3.0*vec3(3,2,1))*0.5+0.5;
}

float map(vec3 p) {
    p.y = -p.y;
    p.y += p.z*p.z*0.05;
    float r = heart(p.xy);
    return length(p.xy)-r;
}


void main( void ) {
	vec2 uv = gl_FragCoord.xy - RENDERSIZE.xy*0.5;
    uv /= RENDERSIZE.y;
    
    vec3 dir = normalize(vec3(uv, 0.7));
    dir.yz *= rot(-0.4);
    
    float tot = 0.0;
    for (int i = 0; i < 100 ; i++) {
        vec3 p = tot*dir;
        tot += map(p)*0.9;
    }

	vec3 c = hue(tot*0.3-TIME);
    gl_FragColor = vec4(c, 1);
	
	//vec2 pos = gl_FragCoord.xy/RENDERSIZE.xy;
	
	//vec3 color = hueToRGB(pos.x);
	
	//gl_FragColor = vec4(color,1.0 );

}
/*
{
  "CATEGORIES" : [
    "Automatically Converted",
    "GLSLSandbox"
  ],
  "INPUTS" : [

  ],
  "DESCRIPTION" : "Automatically converted from http:\/\/glslsandbox.com\/e#66329.0"
}
*/


#ifdef GL_ES
precision lowp float;
#endif

//#extension GL_OES_standard_derivatives : enable


float rand(vec2 n) { 
	return fract(sin(dot(n, vec2(12.9898, 4.1414))) * 43758.5453);
}

float map(vec3 p) {
	vec2 id = floor(p.xz / 2.0);
	p.y += rand(id) * 5.0;// * sin(234.0 + 0.04 * id.x * id.y + TIME * 2.0) * 0.5;
	p.xz = mod(p.xz, 2.0) - 1.0;
	return length(max(abs(p) - 0.5, 0.0)) - 0.01;
	
}

vec3 getnor(vec3 p) {
	vec2 d = vec2(0.01, .00);
	float t = map(p);
	return normalize(vec3(
		t - map(p - d.xyy),
		t - map(p - d.yxy),
		t - map(p - d.yyx)));
}

void main( void ) {
	vec2 uv = (2.0 * gl_FragCoord.xy - RENDERSIZE.xy ) / min(RENDERSIZE.x, RENDERSIZE.y);
	vec3 dir = normalize(vec3(uv, 2.0) + vec3(0, -0.3, 0));
	vec3 pos = vec3(TIME, 3, TIME);
	float t = 0.0;
	for(int i = 0 ; i < 256; i++) {
		float k = map(pos + dir * t);
		t += k * 0.2;
	}
	vec3 ip = pos + dir * t;
	vec3 N = getnor(ip);
	vec3 L = normalize(vec3(1,2,3));
	float D = max(pow(dot(N, L), 2.0), 0.1); 
	vec3 C = vec3(0.5,0.7,1);
	gl_FragColor = vec4(C * D + t * 0.01*(-1.), 1.0);

}
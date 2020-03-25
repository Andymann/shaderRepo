/*
{
  "CATEGORIES" : [
    "Automatically Converted",
    "GLSLSandbox"
  ],
  "INPUTS" : [

  ],
  "DESCRIPTION" : "Automatically converted from http:\/\/glslsandbox.com\/e#52058.0"
}
*/


#ifdef GL_ES
precision mediump float;
#endif

#extension GL_OES_standard_derivatives : enable


float sphereSdf(vec3 p) {
	return length(p) - 0.2;
}

float sdf(vec3 p) {
	vec3 q = fract(p) - 0.5;
	return sphereSdf(q);
}

void main( void ) {

	vec2 position = ((gl_FragCoord.xy / RENDERSIZE.xy) - 0.5) * 2.0;
	position.x *= RENDERSIZE.x / RENDERSIZE.y;
	
	vec3 origin = vec3(0.0, 0.0, TIME);
	vec3 ray = vec3(position.xy, 1.0);
	vec3 march = normalize(ray);
	for (int i = 0; i < 20; i++) {
		vec3 p = origin + ray;
		ray += march * sdf(p);
	}
	
	float rayLength = length(ray);
	float brightness = 2.0 / (rayLength * rayLength);
	gl_FragColor = vec4(vec3(brightness), 1.0);
}
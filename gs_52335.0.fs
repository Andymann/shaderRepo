/*
{
  "CATEGORIES" : [
    "Automatically Converted",
    "GLSLSandbox"
  ],
  "INPUTS" : [

  ],
  "DESCRIPTION" : "Automatically converted from http:\/\/glslsandbox.com\/e#52335.0"
}
*/


#ifdef GL_ES
precision mediump float;
#endif



vec3 hsv2rgb (in vec3 hsv) {
	hsv.yz = clamp (hsv.yz, 0.0, 1.0);
	return hsv.z * (1.0 + 0.5 * hsv.y * (cos (1.0 * 3.14159 * (hsv.x + vec3 (0.0, 2.0 / 3.0, 1.0 / 3.0))) - 1.0));
}

float rand (in vec2 seed) {
	return fract (sin (dot (seed, vec2 (12.9898, 78.233))) * 1.5453);
}

void main () {
	vec2 frag = vv_FragNormCoord*1e1;
	
	vec2 al = vec2(atan(frag.x, frag.y), length(frag));
//	al.x += 0.04*cos(TIME+al.y);
//	al.x += 0.04*cos(TIME-al.y*1.23);
	
	frag = vec2(cos(al.x), sin(al.x))*al.y;
	
	float random = rand (floor (frag));
	vec2 black = smoothstep (1.0, 0.99, cos (frag * 3.14159 * 1.0));
	vec3 color = hsv2rgb (vec3 (random, 0.9, 1.0));
	color *= black.x * black.y * smoothstep (1.0, 0.1,length(fract(frag) - 0.5));
	color *= 0.9 + 0.9 * cos (random + random * TIME + TIME /*+ 3.14159 * 0.*/);// * texture (iChannel0, vec2 (0.7)).x);
	gl_FragColor = vec4 (color * vec3(0.2,0.3,0.8), 1.0);
}
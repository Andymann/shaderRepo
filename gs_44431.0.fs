/*
{
  "CATEGORIES" : [
    "Automatically Converted",
    "GLSLSandbox"
  ],
  "INPUTS" : [

  ],
  "DESCRIPTION" : "Automatically converted from http:\/\/glslsandbox.com\/e#44431.0"
}
*/


#ifdef GL_ES
precision lowp float;
#endif


float rand(vec2 co){
  return fract(sin(dot(co.xy, vec2(12.9898, 78.233))) * 43758.5453);
}

void main (void) {
	// Divide the coordinates into a grid of squares
	vec2 v = gl_FragCoord.xy  / 20.0;
	// Calculate a pseudo-random brightness value for each square
	float brightness = fract(rand(floor(v)) + TIME);
	// Reduce brightness in pixels away from the square center
	brightness *= 0.5 - length(fract(v) - vec2(0.5, 0.5));
	gl_FragColor = vec4(brightness * 4.0, 0.0, 0.0, 1.0);
}
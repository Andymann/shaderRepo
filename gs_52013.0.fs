/*
{
  "CATEGORIES" : [
    "Automatically Converted",
    "GLSLSandbox"
  ],
  "INPUTS" : [

  ],
  "DESCRIPTION" : "Automatically converted from http:\/\/glslsandbox.com\/e#52013.0"
}
*/


#ifdef GL_ES
precision mediump float;
#endif

//#extension GL_OES_standard_derivatives : enable


void main( void ) {

	vec2 position = ( gl_FragCoord.xy / RENDERSIZE.xy ) * 2.0 - 1.0;
	
	position.x /= RENDERSIZE.y / RENDERSIZE.x;
	
	vec2 center = vec2(0.0, 0.0);
	
	float ringRadius = 0.1;
	
	float distanceFromCenter = distance(position.xy, center.xy);
	
	float ringNumber = floor((distanceFromCenter + (TIME / -7.0)) / ringRadius);
	
	if (mod(ringNumber, 2.0) == 0.0) {
		gl_FragColor = vec4(0.0, 0.0, 0.0, 1.0);
	} else {
		gl_FragColor = vec4(1.0, 1.0, 1.0, 1.0);
	}

}
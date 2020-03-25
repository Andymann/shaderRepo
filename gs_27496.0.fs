/*
{
  "CATEGORIES" : [
    "Automatically Converted",
    "GLSLSandbox"
  ],
  "INPUTS" : [

  ],
  "DESCRIPTION" : "Automatically converted from http:\/\/glslsandbox.com\/e#27496.0"
}
*/


#ifdef GL_ES
precision mediump float;
#endif


void main( void ) {
	vec3 light_color = vec3(1.2,0.8,0.6);
	
	float t = TIME*20.0;
	vec2 position = ( gl_FragCoord.xy -  RENDERSIZE.xy*.5 ) / RENDERSIZE.x;

	// 256 angle steps
	float angle = atan(position.y,position.x)/(2.*3.14159265359);
	angle -= floor(angle);
	float rad = length(position);
	
	float color = 0.0;

	float angleFract = fract(angle*256.);
	float angleRnd = floor(angle*256.)+1.;
	float angleRnd1 = fract(angleRnd*fract(angleRnd*.7235)*45.1);
	float angleRnd2 = fract(angleRnd*fract(angleRnd*.82657)*13.724);
	float t2 = t+angleRnd1*10.;
	float radDist = sqrt(angleRnd2);
	
	float adist = radDist/rad*.1;
	float dist = (t2*.1+adist);
	dist = abs(fract(dist)-.5);
	color +=  (1.0 / (dist))*cos(0.7*(sin(t)))*adist/radDist/30.0;

	angle = fract(angle+.61);
	
	gl_FragColor = vec4(color,color,color,1.0)*vec4(light_color,1.0);
}
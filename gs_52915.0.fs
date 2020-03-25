/*
{
  "CATEGORIES" : [
    "Automatically Converted",
    "GLSLSandbox"
  ],
  "INPUTS" : [

  ],
  "DESCRIPTION" : "Automatically converted from http:\/\/glslsandbox.com\/e#52915.0"
}
*/


#ifdef GL_ES
precision mediump float;
#endif

#extension GL_OES_standard_derivatives : enable


void main( void ) {
	
	vec4 light_pos = vec4(5, 0, 0, 1);
	vec4 sphere_pos = vec4(0,0,0,1);
	vec4 L = normalize(sphere_pos - light_pos);
	//vec4 N = ;
	
	vec2 mapping = (gl_FragCoord.xy / RENDERSIZE.xy) * 2.0 - vec2(1,1);
	float io = dot(mapping, mapping);
	if (io > 1.0)
	{
		gl_FragColor = vec4(0,0,0,0);
	}
	else
	{
		gl_FragColor = vec4(1,1,1,1);
	}
}
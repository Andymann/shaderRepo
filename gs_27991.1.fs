/*
{
  "CATEGORIES" : [
    "Automatically Converted",
    "GLSLSandbox"
  ],
  "INPUTS" : [

  ],
  "DESCRIPTION" : "Automatically converted from http:\/\/glslsandbox.com\/e#27991.1"
}
*/


////based on  https://www.shadertoy.com/view/Ml2GWy

// Created by inigo quilez - iq/2015
// License Creative Commons Attribution-NonCommercial-ShareAlike 3.0 Unported License.

#ifdef GL_ES
precision mediump float;
#endif


void main( void ) {

	vec2 uv = 256.0 * ( gl_FragCoord.xy / RENDERSIZE.x ) ;

	uv /=16.0;
	
        vec2 a = floor(uv);
        vec2 b = fract(uv);
	
        vec4 w = fract((sin(a.x*7.0+31.0*a.y + 0.01*TIME)+vec4(0.035,0.01,0.0,0.7))*13.545317); // randoms
                
        vec3 col = 
		smoothstep(0.45,0.55,w.w) *               // intensity
		vec3(sqrt( 16.0*b.x*b.y*(1.0-b.x)*(1.0-b.y))); // pattern
        col = pow( 2.5*col, vec3(1.0,1.0,0.7) );    // contrast and color shape
	
	gl_FragColor = vec4( col , 1.0 );
}
/*
{
  "CATEGORIES" : [
    "Automatically Converted",
    "GLSLSandbox"
  ],
  "INPUTS" : [

  ],
  "DESCRIPTION" : "Automatically converted from http:\/\/glslsandbox.com\/e#58169.0"
}
*/


//----Accidentally from here: http://glslsandbox.com/e#58163.0


#ifdef GL_ES
precision mediump float;
#endif

#extension GL_OES_standard_derivatives : enable


void main( void ) {

	vec2 R = RENDERSIZE.xy, U= gl_FragCoord.xy;
	vec2 u = gl_FragCoord.xy;
	 u.x  =TIME*10.0;
         U = 8.*u/R.x;                          
        float a = 3.5*ceil(U.y) * max(0.,1.-2.*length(U = fract(U)-.5));  
        U *= mat2(cos(a),-sin(a),sin(a),cos(a));    
	
	    gl_FragColor += smoothstep(-1.,1.,U.y/fwidth(U.y));
	    
	   
}
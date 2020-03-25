/*
{
  "CATEGORIES" : [
    "Automatically Converted",
    "GLSLSandbox"
  ],
  "INPUTS" : [

  ],
  "DESCRIPTION" : "Automatically converted from http:\/\/glslsandbox.com\/e#52892.1"
}
*/


#ifdef GL_ES
precision mediump float;
#endif

#extension GL_OES_standard_derivatives : enable




void main( void ) {

	vec2 pos=(gl_FragCoord.xy/RENDERSIZE.xy)*2.0-1.0;
	pos.x*=RENDERSIZE.x/RENDERSIZE.y;
	
	
	
	float col=0.1;
	
	
//code
//float len = length(p);
  //vec2 uv = tc + (p/len)*cos(len*12.0-TIME*4.0)*0.03;
// from https://www.geeks3d.com/20110316/shader-library-simple-2d-effects-sphere-and-ripple-in-glsl
//code
	
	
	
	col=mod(pos.x,0.2)+mod(pos.y,0.2);
	
	float len=length(pos);
	vec2 uv=vec2(col)+(pos/len)*cos(len*2.0-TIME*4.0)*0.8;
	
	
	
		
	
	vec3 color=vec3(col*abs(sin(TIME))/8.0,uv);
	
	
	gl_FragColor=vec4(color,1.0);
}
/*
{
  "CATEGORIES" : [
    "Automatically Converted",
    "GLSLSandbox"
  ],
  "INPUTS" : [

  ],
  "DESCRIPTION" : "Automatically converted from http:\/\/glslsandbox.com\/e#54331.0"
}
*/


//by 834
#ifdef GL_ES
precision mediump float;
#endif


void main( void ) {

	vec2 uv = ( gl_FragCoord.xy / RENDERSIZE.xy );
	
	vec2 pos = 2.*uv - vec2(1.);
	
	float dis = 0.55 + 0.2*cos(atan(pos.y,pos.x)*8.+TIME*2.4);

	vec3 color = vec3(0.44,0.4,0.9);
	
	color *= smoothstep(dis,dis+0.3,length(pos));
	
	color.rb += vec2(smoothstep(0.25,.89,dis));//length(vec2());
	
	
	
	gl_FragColor = vec4( color, 1.0 );

}
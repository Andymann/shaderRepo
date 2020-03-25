/*
{
  "CATEGORIES" : [
    "Automatically Converted",
    "GLSLSandbox"
  ],
  "INPUTS" : [

  ],
  "DESCRIPTION" : "Automatically converted from http:\/\/glslsandbox.com\/e#49191.0"
}
*/


//--- blackhole
// by Catzpaw 2018
#ifdef GL_ES
precision mediump float;
#endif


vec3 hsv(float h,float s,float v){return ((clamp(abs(fract(h+vec3(0.,.666,.333))*6.-3.)-1.,0.,1.)-1.)*s+1.)*v;}
vec2 rot(vec2 p,float a){return vec2(p.x*cos(a)-p.y*sin(a),p.x*sin(a)+p.y*cos(a));}

void main(void){
	vec2 p=(gl_FragCoord.xy*2.-RENDERSIZE.xy)/min(RENDERSIZE.x,RENDERSIZE.y);float l=length(p);
	for(int i=0;i<4;i++)p=abs(rot(p,TIME+l*3.)+l)*1.3;
	float v=abs(cos(p.x)+sin(p.y))*clamp(3.-l*3.,0.,1.)*l*l*l;
	gl_FragColor = vec4(hsv(sin(1.+v*.2-l*.4),1.,v),1);
}
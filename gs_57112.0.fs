/*
{
  "CATEGORIES" : [
    "Automatically Converted",
    "GLSLSandbox"
  ],
  "INPUTS" : [

  ],
  "DESCRIPTION" : "Automatically converted from http:\/\/glslsandbox.com\/e#57112.0"
}
*/


//--- rainbow temple
// by Catzpaw 2018
#ifdef GL_ES
precision mediump float;
#endif

//#extension GL_OES_standard_derivatives : enable


#define OCT 4
#define SHIFT 1.55
#define ITER 48
#define EPS 0.005
#define NEAR 0.1
#define FAR 20.
#define SPEED 0.1

vec3 rotX(vec3 p,float a){return vec3(p.x,p.y*cos(a)-p.z*sin(a),p.y*sin(a)+p.z*cos(a));}
vec3 rotY(vec3 p,float a){return vec3(p.x*cos(a)-p.z*sin(a),p.y,p.x*sin(a)+p.z*cos(a));}
vec3 rotZ(vec3 p,float a){return vec3(p.x*cos(a)-p.y*sin(a), p.x*sin(a)+p.y*cos(a), p.z);}
vec3 hsv(float h,float s,float v){return ((clamp(abs(fract(h+vec3(0.,.666,.333))*6.-3.)-1.,0.,1.)-1.)*s+1.)*v;}

float map(vec3 p){float r=1.;p.xz=fract(p.xz)-.5;
	for(int i=0;i<OCT;i++){p=abs(p);p.y-=SHIFT;float s=2./clamp(dot(p,p),.28,.99);p*=s;p-=vec3(.51,4.,.7);r*=s;}
	return length(p/r);}

float trace(vec3 ro,vec3 rd,out float n){float t=NEAR,d;
	for(int i=0;i<ITER;i++){d=map(ro+rd*t);if(abs(d)<EPS||t>FAR)break;t+=step(d,1.)*d*.2+d*.5;n+=1.;}
	return min(t,FAR);}

void main(void){
	vec2 uv=(gl_FragCoord.xy-.5*RENDERSIZE.xy)/RENDERSIZE.y;
	vec3 rd=vec3(uv,-.9);rd=rotX(rd,sin(TIME*.1));rd=rotY(rd,sin(TIME*.13));rd=rotZ(rd,sin(TIME*.11));
	float c=clamp(3.,0.,1.),n=0.,
	v=1.-trace(vec3(sin(TIME*.314)*.15,sin(TIME*.1)*.6+SHIFT,1.-TIME*SPEED),rd,n)/FAR;n/=float(ITER);
	gl_FragColor=vec4(mix(vec3(c),mix(hsv(v*4.-TIME*SPEED*5./FAR,1.,v),vec3(1.-c),n),n)*v,1);
}
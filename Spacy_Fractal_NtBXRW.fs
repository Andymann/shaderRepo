/*
{
    "CATEGORIES": [
        "Automatically Converted",
        "Shadertoy"
    ],
    "DESCRIPTION": "Automatically converted from https://www.shadertoy.com/view/NtBXRW by julianlumia.  :octopusballoon:\ncoloring based on:  https://www.shadertoy.com/view/WlKXRW",
    "IMPORTED": {
    },
    "INPUTS": [
        {
            "NAME": "iMouse",
            "TYPE": "point2D"
        }
    ],
    "PASSES": [
        {
            "FLOAT": true,
            "PERSISTENT": true,
            "TARGET": "BufferA"
        },
        {
        }
    ]
}

*/


// Fork of "1. spacebaaals" by julianlumia. https://shadertoy.com/view/WdByDG
// 2021-07-24 15:02:41

// Fork of "lost in spacetime.." by julianlumia. https://shadertoy.com/view/wlGSzm
// 2020-04-18 14:32:19

#define MAX_STEPS 100
#define MAX_DIST 100.
#define SURF_DIST 0.
#define pi acos(-1.)
#define tau (2.*pi)
#define T (TIME*0.125)
#define pal(a,b,c,d,e) (a + b*sin(c*d + e))
#define pmod(p, x) mod(p, x) - 0.5*x
#define lmod(d, x) (mod(d,x)/x - 0.5)

mat2 Rot(float a) {
 float s = sin(a);
 float c = cos(a);
 return mat2(c, -s, s, c);
}

float sdBox(vec3 p, vec3 s) {
 p = abs(p)-s;
 return length(max(p, 0.))+min(max(p.x, max(p.y, p.z)), 0.);
}

vec3 pA = vec3(0);
mat3 rotate( in vec3 v, in float angle)
{
 float c = cos(radians(angle));
 float s = sin(radians(angle));	
 return mat3(c + (1.0 - c) * v.x * v.x, (1.0 - c) * v.x * v.y - s * v.z, (1.0 - c) * v.x * v.z + s * v.y,
 (1.0 - c) * v.x * v.y + s * v.z, c + (1.0 - c) * v.y * v.y, (1.0 - c) * v.y * v.z - s * v.x,
 (1.0 - c) * v.x * v.z - s * v.y, (1.0 - c) * v.y * v.z + s * v.x, c + (1.0 - c) * v.z * v.z
 );
}

vec3 text(vec2 t, vec3 p){
 vec3 o = vec3(0);
 float d = 10e6;
 t = pmod(t,1./.22);
 t *=2.;
 float yid = (floor( (p.y + 0.)*0. ) );
 float W = .2;
 float modd = 2.1;
 float sqD = max(abs(t.x), abs(t.y));
 sqD +=.5 + yid*0.4;
 float sqid = floor(sqD/modd);
 sqD = lmod(sqD, modd);  
 d = min(d, sqD);
 o +=  pal(0.6, vec3(1.,0.7,0.6)*0.5, vec3(8.4 ,4.19,7.4 - yid*0.2), vec3(3.,7.,3.),-1. + TIME + sqid*0.5 + p.z + t.x*2.5 - t.y*1.5);
 o *= step(sin(sqid*10.), -0.);
 float aa = 20.;
 sqD -= 0.5;
 sqD = abs(sqD*1.);
 o -= exp(-sqD*aa)*1.;
 sqD -= 1.;
 sqD = abs(sqD*1.);
 o -= exp(-sqD*aa)*1.;
 return o;
}

vec3 tex3D(  in vec3 p, in vec3 n ){
 float dp = dot(p,p)*.5;
 p /= dp;
// p.xz*= Rot(cos(TIME*0.5));
// p.xy *=  Rot(cos(TIME*1.-0.2));
// p = rotate( ( vec3(cos(p.xyz*.5+TIME*.5) ) ), 120.)*p-3.;
 p.xy=sin(p-vec3(-T*tau*1.,-T*tau*2.,-T*tau*1.+sin(TIME)*1.)).xy;
 p.xy*= Rot((sin(TIME*.1)));
 vec3 q = (text(p.xz, p)).xyz;
 return q;
}

vec2 condmin(in vec2 d1, in vec2 d2) {
return vec2(min(d1.x, d2.x), mix(d1.y, d2.y, step(d2.x, d1.x)));
}

float g1;

vec2 GetDist(vec3 p) {
 vec2 d =vec2(0.);
 vec3 q = p;
 float dp = dot(p,p)*.1;
 p /= dp;
 //p.xz*= Rot(cos(TIME*0.5));
 //p.xy *=  Rot(cos(TIME*1.-0.2));
 //p = rotate( ( vec3(cos(p.xyz*.5+TIME*.5) ) ), 120.)*p-3.;
 p.xy=sin(p-vec3(-T*tau*1.,-T*tau*2.,-T*tau*1.+sin(TIME)*1.)).xy;
 p.xy*= Rot((1.));
 d = vec2(sdBox(p,vec3(1.)) + sdBox(q,vec3(0.0)),1);
 d.x *=1.;
 d.x =(((d.x*dp)/10.));
 g1 +=1./(.018+pow(abs(d.x),1.));
 d = condmin( d,d);
 return d;
}

vec2 RayMarch (vec3 ro, vec3 rd) 
 {
 vec2 h, t=vec2( 0.);
 for (int i=0; i<100; i++) 
  {
 h = GetDist(ro + t.x * rd);
 if(h.x<SURF_DIST||t.x>MAX_DIST) break;
  t.x+=h.x;t.y=h.y;
 }
 if(t.x>100.) t.x=0.;
 return t;
}

vec3 GetNormal(vec3 p){
vec2 e = vec2(.00035, -.00035); 
return normalize(
 e.xyy * GetDist(p + e.xyy).x + 
 e.yyx * GetDist(p + e.yyx).x + 
 e.yxy * GetDist(p + e.yxy).x + 
 e.xxx * GetDist(p + e.xxx).x);
}


float GetLight(vec3 p) {
// vec3 lightPos = vec3(sin(TIME)*2., cos(TIME)*2., 3);
     vec3 lightPos = vec3(0.,0.,.0);
 vec3 l = normalize(p-lightPos);
 vec3 n = GetNormal(p);
 float dif = clamp(dot(n, l)*.5+.5, 0., 1.);
 vec2 d = RayMarch(p+n*SURF_DIST*1., l);
 return dif;
}

vec3 R(vec2 uv, vec3 p, vec3 l, float z) {
 vec3 f = normalize(l-p),
 r = normalize(cross(vec3(0,1,0), f)),
 u = cross(f,r),
 c = p+f*z,
 i = c + uv.x*r + uv.y*u,
 d = normalize(i-p);
 return d;
}



//slightly edited droste zoom effect from: roywig
//https://shadertoy.com/view/Xs3SWj

#define PI 3.14159

vec2 cInverse(vec2 a) { return	vec2(a.x,-a.y)/dot(a,a); }
vec2 cMul(vec2 a, vec2 b) {	return vec2( a.x*b.x -  a.y*b.y,a.x*b.y + a.y * b.x); }
vec2 cDiv(vec2 a, vec2 b) {	return cMul( a,cInverse(b)); }
vec2 cExp(vec2 z) {	return vec2(exp(z.x) * cos(z.y), exp(z.x) * sin(z.y)); }
vec2 cLog(vec2 a) {	float b =  atan(a.y,a.x); if (b>0.0) b-=2.0*PI;return vec2(log(length(a)),b);}

void main() {
	if (PASSINDEX == 0)	{


	 vec2 uv = (gl_FragCoord.xy-.5*RENDERSIZE.xy)/RENDERSIZE.y;
	 vec2 m = iMouse.xy/RENDERSIZE.xy;  
	 vec3 col = vec3(0);  
	 vec3 ro = vec3(0, 0, 1.1);
	// ro.xy *= Rot(sin(TIME*.1)*6.2831);
	// ro.xz *= Rot(sin(TIME*0.2)*6.2831);
	 vec3 rd = R(uv, ro, vec3(0,.0,0), 1.2);
	 vec2 d = RayMarch(ro, rd);
	 float t2;
	 t2=d.x;   
	 if(t2>0.)
	 {
	  vec3 p = ro + rd * t2;
	  vec3 n = GetNormal(p);
	  vec3 baseColor = vec3(1,0,cos(TIME*2.)+.5);
	  float dif = GetLight(p);
	  col = vec3(dif);
	  col+=baseColor;
	  if(d.y==1.) col += tex3D(p,n)*5.;
	 }
	 col*=g1*vec3(.00005);  
	 col+=g1*vec3(-.0001);  
	 float fog = 4. / (1. + d.x * d.x *15.);
	 col *= vec3(fog); 
	 col+=g1*vec3(.000003);  
	 col*= 5.; 
	 gl_FragColor = vec4(col,1.0);
	}
	else if (PASSINDEX == 1)	{


	 
	    vec2 z = (gl_FragCoord.xy-.5*RENDERSIZE.xy);
	    float r1 =3., r2 =.495,
		scale = log(r2/r1),angle = atan(scale/(0.+2.*PI));
	    z = cLog(z);
		z.y -= TIME/2.;
	    z = cDiv(z, cExp(vec2(0.,angle))*cos(angle)); 
	    z.x = mod(z.x-TIME,scale);
	    z = cExp(z)*r1;
	    gl_FragColor = IMG_NORM_PIXEL(BufferA,mod(.5+.5*z,1.0)); 
	
	}

}

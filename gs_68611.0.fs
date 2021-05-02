/*
{
  "CATEGORIES" : [
    "Automatically Converted",
    "GLSLSandbox"
  ],
  "INPUTS" : [

  ],
  "DESCRIPTION" : "Automatically converted from http:\/\/glslsandbox.com\/e#68611.0"
}
*/


// gg
//----Frok from http://glslsandbox.com/e#68601.0

#ifdef GL_ES
precision mediump float;
#endif

// glslsandbox uniforms

// shadertoy emulation
#define iTime TIME
#define iResolution RENDERSIZE

#define R iResolution.xy

#define SS(U) smoothstep(3./R.y,0.,U)
#define CHS 0.18
float sdBox2(in vec2 p,in vec2 b) {vec2 d=abs(p)-b;return length(max(d,vec2(0))) + min(max(d.x,d.y),0.0);}
float line2(float d,vec2 p,vec4 l){vec2 pa=p-l.xy;vec2 ba=l.zw-l.xy;float h=clamp(dot(pa,ba)/dot(ba,ba),0.0,1.0);return min(d,length(pa-ba*h));}
float TB(vec2 p, float d){p.y=abs(p.y);return line2(d,p,vec4(2,3.25,-2,3.25)*CHS);}
//float B(vec2 p,float d){p.y+=1.75*CHS;d=min(d,abs(sdBox2(p,vec2(2.0,1.5)*CHS)));p+=vec2(0.5,-3.25)*CHS;return min(d,abs(sdBox2(p,vec2(1.5,1.75)*CHS)));} float E(vec2 p,float d){d=TB(p,d);d=line2(d,p,vec4(-2,3.25,-2,-3.25)*CHS);return line2(d,p,vec4(0,-0.25,-2,-0.25)*CHS);} float I(vec2 p,float d){d=line2(d,p,vec4(0,-3.25,0,3.25)*CHS);p.y=abs(p.y);return line2(d,p,vec4(1.5,3.25,-1.5,3.25)*CHS);} float _R(vec2 p,float d){d=line2(d,p,vec4(0.5,-0.25,2,-3.25)*CHS);d=line2(d,p,vec4(-2,-3.25,-2,0.0)*CHS);p.y-=1.5*CHS;return min(d, abs(sdBox2(p,vec2(2.0,1.75)*CHS)));} float T(vec2 p,float d){d=line2(d,p,vec4(0,-3.25,0,3.25)*CHS);return line2(d,p,vec4(2,3.25,-2,3.25)*CHS);} float X(vec2 p,float d){d = line2(d,p,vec4(-2,3.25,2,-3.25)*CHS);return line2(d,p,vec4(-2,-3.25,2,3.25)*CHS);} // DOGSHIT

float GetText(vec2 uv)
{
	uv.y -= 1.75;
	uv.x += 2.75;
	float d = (uv,1.0);uv.x -= 1.1;
	//d = _R(uv,d);uv.x -= 1.1;
	//d = E(uv,d);uv.x -= 1.1;
	//d = X(uv,d);uv.x -= 1.1;
	//d = I(uv,d);uv.x -= 1.1;
	//d = T(uv,d);
	return smoothstep(0.0,0.05,d-0.55*CHS);
}

float box(vec2 p, vec2 b)
{
    vec2 d = abs(p)-b;
    return length(max(d,0.0)) + min(max(d.x,d.y),0.0);
}

float circ(vec2 p, float r)
{
    return length(p)-r;
}

float arc(vec2 p, vec2 sa, vec2 sb, float ra, float rb)
{
    p*=mat2(sa.x,sa.y,-sa.y,sa.x);
    p.x = abs(p.x);
    float q = (sb.y*p.x>sb.x*p.y) ? dot(p.xy, sb) : length(p.xy);
    return sqrt( dot(p,p)+ra*ra-2.0*ra*q)-rb;
}



mat2 rot(float a)
{
    float c = cos(a), s=sin(a);
    return mat2(c, -s, s, c);
}

float hatline(vec2 p, float ta, float tb, float rb)
{
    return SS(arc(p,vec2(sin(ta),cos(ta)),vec2(sin(tb),cos(tb)), 0.5, rb));
}

void mainImage( out vec4 c, in vec2 f )
{
    vec2 p = (2.*f-R)/R.y;
    
    //flag wave inspiration from https://www.shadertoy.com/view/tl2fRm
    float w = cos(p.y * p.x - iTime + cos(p.x * 7.14 + 4.2 * p.y)+sin(p.x*4.8));
    p *= 1. + (.03 - .04 * w);

    vec2 ap= abs(p);
    p.y-=.25;
    vec2 mp = vec2(abs(p.x),p.y);
    
    //hat
    float dout = SS(1.0-circ(p,-.36));
    float d = SS(circ(p,.6))*float(p.y>.05);    
    vec2 op = p-vec2(0.05,.1);
    float ta = .5775;
    float tb = .15;
    float rb = .015;
    float a = hatline(op, ta, tb, rb);
    op.x-=.1;
    tb = .1;
    ta = .5;
    float a2 = hatline(op, ta, tb, rb);    
    op.x-=.1;
    tb = .05;
    ta = .45;
    float a3 = hatline(op, ta, tb, rb);
    op.x+=.3;
    tb = .13;
    ta = 2.585;
    float a4 = hatline(op, ta, tb, rb);
    op.x+=.12;
    tb = .055;
    ta = 2.65;
    float a5 = hatline(op, ta, tb, rb);
    float loff = .15;
    float lo = SS(length(p.y-loff)-.11);
    float l = SS(length(p.y-loff)-.075)*d;
    float b = SS(box(p,vec2(.9,.0))-.035);
    
    //skull
    float s = SS(circ(p,.6)) * (1.0-SS(length(p.y)-.075));
    float eyes = SS(circ(mp-vec2(.23,-.27),.165));
    float nose = SS(circ((p+vec2(0,.48))/vec2(1.3,1),.05));
    
    //jaw and teeth
    float jaw = SS(box(p+vec2(0,.725),vec2(0.01,0.05))-.425*(abs(p.y)/2.+.45))*dout;
    float jawout = SS(box(p+vec2(0,.725),vec2(0.01,0.05))-.45*(abs(p.y)/2.+.47));
    float t1 = .79, td = .035;
    vec2 tt = p/vec2(.98,1.)+vec2(0,.02);
    float tee = SS( circ(tt,t1)) - SS( circ(tt,t1-td));
    float t2 = .95, t2d = .035;
    float tee2 = SS( circ(tt,t2)) - SS( circ(tt,t2-t2d));
    float g = SS(length(p.x)-.015) * float(p.y<-.61 && p.y > -.95);
    vec2 rp = (p+vec2(.075,0))*rot(.15);
    float g2 = SS(length(rp.x)-.015) * float(rp.y<-.61 && rp.y > -.95);
    vec2 rp2 = (p-vec2(.075,0))*rot(-.15);
    float g3 = SS(length(rp2.x)-.015) * float(rp2.y<-.61 && rp2.y > -.95);
    
    //cross bones
    ap = ap-vec2(.775);
    ap.x+= (p.y<0.?.1:-.1);
    float bo = SS(length(ap+vec2(-0.075,0.075))-.125);
    float bo2 = SS(length(ap-vec2(-0.075,.075))-.125);    
    vec2 r2p = (p*rot(.79))-vec2(.25,0.);
    float bol = SS(length(r2p.x)-.08)*float(r2p.y>-1.2 && r2p.y<1.);
    vec2 r3p = (p*rot(-.79))+vec2(.25,0);
    float bol2 = SS(length(r3p.x)-.08)*float(r3p.y>-1.2 && r3p.y<1.);

    //coloring
    vec3 col = vec3(0);
    vec3 ylw = vec3(1.,.95,0.2);

    col = mix(col, vec3(1), s);
    col = mix(col, vec3(1), jaw);
    col = mix(col, vec3(0), tee);
    col = mix(col, vec3(0), tee2);
    col = mix(col,vec3(0), g);
    col = mix(col,vec3(0), g2);
    col = mix(col,vec3(0), g3);
    
   	col = mix(col, ylw, d);
    col = mix(col, vec3(0), a);
    col = mix(col, vec3(0), a2);
    col = mix(col, vec3(0), a3);
    col = mix(col, vec3(0), a4);
    col = mix(col, vec3(0), a5);
    
    col = mix(col, vec3(0), lo);
	col = mix(col, vec3(1,0,0), l);
    col = mix(col, ylw, b);
    col = mix(col, vec3(0), eyes);
    col = mix(col, vec3(0), nose);
    
    vec3 bones =  vec3(bo);
    bones = mix(bones,vec3(1), bo2);
    bones = mix(bones,vec3(1), bol);
    bones = mix(bones,vec3(1), bol2);
    bones = mix(bones, vec3(0), 1.0-dout);
    bones = mix(bones, vec3(0), jawout);
    
    col+=bones;
    c.rgb = col+w*.0;
    
	//float dd = GetText(p*12.0);
	//vec3 cc = mix(vec3(1.0,0.0,0.0) ,vec3(0.4,0.9+sin(TIME*3.3+p.x*16.0)*0.25,0.4),1.0-dd); 
	
	//c.rgb = mix(cc,c.rgb,dd*0.9);
	
}

void main(void)
{
    mainImage(gl_FragColor, gl_FragCoord.xy);
    gl_FragColor.a = 1.;
}
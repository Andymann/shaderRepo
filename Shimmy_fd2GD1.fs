/*
{
    "CATEGORIES": [
        "Automatically Converted",
        "Shadertoy"
    ],
    "DESCRIPTION": "Automatically converted from https://www.shadertoy.com/view/fd2GD1 by Kali.  Music by Tony Leys https://soundcloud.com/tonileys",
    "IMPORTED": {
    },
    "INPUTS": [
        {
            "NAME": "iChannel0",
            "TYPE": "audio"
        }
    ]
}

*/


#define TIME TIME
float det=.005, maxdist=50., pi=3.1416, gl=0.;
vec2 id;


float hash12(vec2 p)
{
    p*=1000.;
    vec3 p3  = fract(vec3(p.xyx) * .1031);
    p3 += dot(p3, p3.yzx + 33.33);
    return fract((p3.x + p3.y) * p3.z);
}


mat2 rot(float a)
{
    float s=sin(a), c=cos(a);
    return mat2(c,s,-s,c);
}

float box(vec3 p, vec3 c)
{
    vec3 pc=abs(p)-c;
    return length(max(vec3(0.),pc))-min(0.,max(pc.z,max(pc.x,pc.y)));
}

vec2 amod(vec2 p, float n, float off, out float i)
{
    float l=length(p)-off;
    float at=atan(p.x,p.y)/pi*n*.5;
    i=abs(floor(at));
    float a=fract(at)-.5;
    return vec2(a,l);
}

float ring(vec3 p,inout vec2 id)
{
    p.xy=amod(p.xy*rot(TIME*0.), 20., 2., id.x);
    float h=max(0.,IMG_NORM_PIXEL(iChannel0,mod(vec2(.5+fract(id.x*.2+id.y*.1),0.)*.5,1.0)).r*3.-.5);
    h+=sin(TIME*10.+id.x)*.2;
    float d=box(p+vec3(0.,-h*1.5,0.),vec3(.1,h,.1));
    return d*.5;
}

float de(vec3 p)
{
    float d=100.,ii=0.;
    p.xz*=rot(TIME);
    p.yz*=rot(sin(TIME));
    float r=4.;
    vec2 ids;
    for (float i=0.; i<r; i++)
    {
        p.xz*=rot(pi/r);
        ids.y=i;
        float r=ring(p,ids);
        if (r<d)
        {
            d=r;
            id=ids;
        }
    }
    d=min(d,length(p)-1.5);
    return d*.7;
}

vec3 normal(vec3 p)
{
    vec2 e=vec2(0.,det);
    return normalize(vec3(de(p+e.yxx),de(p+e.xyx),de(p+e.xxy))-de(p));
}


vec3 march(vec3 from, vec3 dir)
{
    float d, td=0.;
    vec3 p, col=vec3(0.);
    for (int i=0; i<100; i++)
    {
        p=from+td*dir;
        d=de(p);
        if (d<det || td>maxdist) break;
        td+=d;
        gl+=.1/(10.+d*d*10.)*step(.7,hash12(id+floor(TIME*5.)));
    }
    if (d<det)
    {
        //id+=floor(TIME*5.);
        vec3 colid=vec3(hash12(id),hash12(id+123.123),1.);
        p-=dir*det;
        vec3 n=normal(p);
        vec2 e=vec2(0.,.05);
        col=.1+max(0.,dot(-dir,n))*colid;
        col*=.5+step(.7,hash12(id+floor(TIME*5.)));
    } 
    else
    {
        dir.xz*=rot(TIME*.5);
        dir.yz*=rot(TIME*.25);
        vec2 p2=abs(.5-fract(dir.yz));
        float d2=100.,is=0.;
        for(int i=0; i<10; i++)
        {
            p2=abs(p2*1.3)*rot(radians(45.))-.5;
            float sh=length(max(vec2(0.),abs(p2)-.05));
            if (sh<d2)
            {
                d2=sh;
                is=float(i);
            }
        }
        col+=smoothstep(.05,.0,d2)*fract(is*.1+TIME)*normalize(p+50.);
    }
    return col*mod(gl_FragCoord.y,4.)*.5+gl;
}



void main() {



    vec2 uv = (gl_FragCoord.xy-RENDERSIZE.xy*.5)/RENDERSIZE.y;
    vec3 from = vec3(0.,0.,-8.);
    vec3 dir = normalize(vec3(uv,.7));
    vec3 col = march(from, dir);
    gl_FragColor = vec4(col,1.0);
}

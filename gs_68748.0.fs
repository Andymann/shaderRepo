/*
{
  "CATEGORIES" : [
    "Automatically Converted",
    "GLSLSandbox"
  ],
  "DESCRIPTION" : "Automatically converted from http:\/\/glslsandbox.com\/e#68748.0",
  "INPUTS" : [

  ],
  "PERSISTENT_BUFFERS" : [
    "backbuffer"
  ],
  "PASSES" : [
    {
      "TARGET" : "backbuffer",
      "PERSISTENT" : true
    }
  ]
}
*/


//precision highp float;

float cube(vec3 p, vec3 s)
{
  p = abs(p)-s;
  return length(max(p,0.));
}

float tau = 3.14159265358979323846*2.;

mat2 rot(float a)
{
  return mat2(cos(a),sin(a),-sin(a),cos(a));
}

vec2 pmod(vec2 p, float n)
{
  float a = atan(p.y, p.x);
  a = (floor(a/tau*n+.5))/n*tau;
  return p * rot(a);
}

float h11(float p)
{
  return fract(sin(p*43.512521)*52135.15);
}

float map(vec3 p)
{
  vec3 f,i;
  float n = 32.;
  p.xy *= rot((h11(floor(p.z+.5))-.5)*TIME);
  f.z = fract(p.z+.5)-.5;
  i.z = floor(p.z+.5);
  float ah = floor(atan(p.y,p.x)/tau*n+.5);
  float ah2 = floor(atan(p.y,p.x)/tau*(n+2.*sin(TIME+p.z+ah*.8)));
  f.xy = p.xy * rot(ah/n*tau);
  f -= vec3(1.+h11(ah2),0.,0.);
  return cube(f,vec3(.2,.05+.025*sin(TIME+p.z),.1));
}


vec3 rm(vec3 ro, vec3 rd)
{
  float d, a = 0.;
  vec3 rp = ro + rd;
  for (int i = 0; i < 48; i++)
  {
    d = map(rp);
    a += exp(-d*(50.+150.*pow(sin(TIME+rp.z)*.5+.5,10.)));
    rp += d * rd * .5;
    if (map(rp)<0.)
    {
      break;
    }
  }
  return vec3(.1,.1,.4)*a*(.125+.25*pow(sin(rp.z*10.+TIME*.5)*.5+.5,10.));
}

void main()
{
  vec2 p = (2.*gl_FragCoord.xy - RENDERSIZE) / min(RENDERSIZE.x, RENDERSIZE.y);
  vec3 color = vec3(0.);
  vec3 ro = vec3(0.,0.,TIME*.5);
  vec3 ww = vec3(0.,0.,1.);
  vec3 uu = normalize(cross(ww,vec3(0.,1.,0.)));
  vec3 vv = normalize(cross(ww,uu));
  float sd = 1.+length(p);
  vec3 rd = normalize(p.x*uu+p.y*vv+sd*ww);

  color = rm(ro,rd);

  gl_FragColor = vec4(mix(color, IMG_NORM_PIXEL(backbuffer,mod(gl_FragCoord.xy/RENDERSIZE,1.0)).xyz,.8)*1.1,1.);
}
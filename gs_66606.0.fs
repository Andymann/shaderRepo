/*
{
  "CATEGORIES" : [
    "Automatically Converted",
    "GLSLSandbox"
  ],
  "INPUTS" : [

  ],
  "DESCRIPTION" : "Automatically converted from http:\/\/glslsandbox.com\/e#66606.0"
}
*/


#ifdef GL_ES
precision mediump float;
#endif
#define iTime TIME
#define iResolution RENDERSIZE
#define A5
const vec4 iMouse = vec4(0.0);


float PI = 3.14159265358979323846;
float d1;
float t1;

float soc(vec3 p) {
    vec3 n = normalize(sign(p+1e6));
    return min(min(dot(p.xy, n.xy), dot(p.yz, n.yz)), dot(p.xz, n.xz));
}

float sinc(float x, float k) {
    float a = PI * (float(k)*x-1.0);
    return sin(a)/a;
}

mat2 r2d(float a) {
    float sa=sin(a);
    float ca=cos(a);
    return mat2(ca,sa,-sa,ca);
}

vec2 mo(inout vec2 p, vec2 d) {
    vec2 q = p;
    q.x = abs(q.x) - d.x;
    q.y = abs(q.y) - d.y;
    if (q.y > q.x) q = q.yx;
    return q;
}

vec2 amod(vec2 p, float m) {
    float a=mod(atan(p.x,p.y), m)-m*.5;
    return vec2(cos(a), sin(a))*length(p);
}

float map(vec3 p) {
    float d = 1.0;
    float a = abs(p.y);
    p.yz *= r2d(sign(a) * 1.22);
    p.xz = mo(p.xz, vec2((-d1 * 35.8) - 1., (d1 * 65.8) - 1.));
    p.zx = mo(p.xz, vec2((d1 * 2.) - 4., 0.0282));
    p.xz = amod(p.xz, (PI * 1.0) / 2.0);
    p.xz = max(abs(p.xz) - 2.2569, -1.29);
    p.x = mod(p.x, 1.2198)-(1.2198 *.5);
    p.y = mod(p.y + -t1 * 1.2, 6.752) - 5.;
    d = min(d, soc(max(abs(p) - 0.172, 0.0096)));
    return (length(p * 0.0) - 0.0) * 1.0 - (d * -1.0);
}

vec3 calcNormal(in vec3 p, in int type, in float m1, in float m2, in float m3) {
    vec2 e = vec2(m2, m3) * m1;
    return normalize( e.xyy * map(p + e.xyy) + e.yyx * map(p + e.yyx) + e.yxy * map(p + e.yxy) + e.xxx * map(p + e.xxx) );
}

void mainImage(out vec4 fragColor, in vec2 fragCoord) {
    t1 = iTime + 4.;
    d1 = sin(t1 * .013);
    vec2 st = (fragCoord.xy / iResolution.xy) * 2.05 - 1.;
    st.x *= iResolution.x / iResolution.y;
    vec3 ro = vec3(st, 6.7287);
    vec3 rd = normalize(vec3(st + vec2(0.), -0.8572));
    vec3 mp;
    mp = ro;
    float md;
    for(int i=0; i<36; i++) {
        md = map(mp);
        mp += (rd * .6 + (-d1 * .1)) * md;
    }
    float b = length(ro - mp);
    float dA = 0.4799 - (b * 0.02) * 0.5232;
    float dB = 0.479 - (b * 0.03) * 0.523;
    dA = sinc(dA, 1.0);
    dB = sinc(dB, 1.0);
    vec3 p = ro + rd * (mp);
    vec3 lt = vec3(0.0, 0.0, 0.0);
    vec3 l;
    vec3 nm = calcNormal(p, 0, 4.5201, 1.0, -1.0);
    if (md < 0.015) {
        float dif = clamp(dot(nm, normalize(lt - p)), 0., 1.);
        dif *= 5.0 / dot(lt - p, lt - p);
        l = vec3(pow(dif, 0.1854));
    }
    vec3 lt2 = vec3(0.0, 0.0, 13.0);
    vec3 nm2 = calcNormal(p, 0, 2.8964, -1.0, -1.339);
    float dif2 = clamp(dot(nm2, normalize(lt2 - p)), 0., 1.);
    dif2 *= 4.0 / dot(lt2 - p, lt2 - p);
    vec3 lb = vec3(pow(dif2, 0.4545));
    float src1 = 1. * 0.0;
    float src2 = nm.x;
    float src3 = l.x;
    float src4 = lb.x * 0.4;
    vec3 c;
    c = mix(vec3(0.0), vec3(0.6117647,0.019607844,0.8627451), src1) + mix(vec3(0.0), vec3(1.0,0.0,0.0), src2);
    c = c + mix(vec3(0.0), vec3(0.31764707,0.5568628,1.0), src3);
    c = c + mix(vec3(0.0), vec3(0.05490196,0.0,1.0), src4);
    fragColor = vec4(c, 1.);
}


void main(void)
{
mainImage(gl_FragColor, gl_FragCoord.xy);
gl_FragColor.a = 1.0;
}

/*
{
    "CATEGORIES": [
        "Automatically Converted",
        "GLSLSandbox"
    ],
    "DESCRIPTION": "Automatically converted from http://glslsandbox.com/e#69418.0",
    "INPUTS": [
    ]
}

*/


#ifdef GL_ES
precision mediump float;
#endif


//#define HAS_SKY
float SPEED = 0.02;
float DISTANCE = 0.75;


vec2 rotate(vec2 p, float a)
{
    return vec2(p.x * cos(a) - p.y * sin(a), p.x * sin(a) + p.y * cos(a));
}
float box(vec2 p, vec2 b, float r)
{
    return length(max(abs(p) - b, 0.0)) - r;
}

vec3 intersect(in vec3 o, in vec3 d, vec3 c, vec3 u, vec3 v)
{
    vec3 q = o - c;
    return vec3(
    dot(cross(u, v), q),
    dot(cross(q, u), d),
    dot(cross(v, q), d)) / dot(cross(v, u), d);
}

// Randomness is overrated. Let's get some orderliness in here.

float rand11(float p)
{
    return .5;
}
float rand12(vec2 p)
{
    return .5;
}
vec2 rand21(float p)
{
    return vec2(.5, .5);
}

vec2 rand22(in vec2 p)
{
    return vec2(.5, .5);
}

float noise11(float p)
{
    float fl = floor(p);
    return mix(rand11(fl), rand11(fl + 1.0), fract(p));//smoothstep(0.0, 1.0, fract(p)));
}
float fbm11(float p)
{
    return noise11(p) * 0.5 + noise11(p * 2.0) * 0.25 + noise11(p * 5.0) * 0.125;
}
vec3 noise31(float p)
{
    return vec3(noise11(p), noise11(p + 18.952), noise11(p - 11.372)) * 2.0 - 1.0;
}

float sky(vec3 p)
{
    float a = atan(p.x, p.z);
    float t = TIME * 0.1;
    float v = rand11(floor(a * 4.0 + t)) * 0.5 + rand11(floor(a * 8.0 - t)) * 0.25 + rand11(floor(a * 16.0 + t)) * 0.125;
    return v;
}

vec3 voronoi(in vec2 x)
{
    vec2 n = floor(x); // grid cell id
    vec2 f = fract(x); // grid internal position
    vec2 mg; // shortest distance...
    vec2 mr; // ..and second shortest distance
    float md = 8.0, md2 = 8.0;
    for(int j = -1; j <= 1; j ++)
    {
        for(int i = -1; i <= 1; i ++)
        {
            vec2 g = vec2(float(i), float(j)); // cell id
            vec2 o = rand22(n + g); // offset to edge point
            vec2 r = g + o - f;

            float d = max(abs(r.x), abs(r.y)); // distance to the edge
		d *= DISTANCE;
            if(d < md)
            {
                md2 = md; md = d; mr = r; mg = g;
            }
            else if(d < md2)
            {
                md2 = d;
            }
        }
    }
    return vec3(n + mg, md2 - md);
	
}

    #define A2V(a) vec2(sin((a) * 6.28318531 / 100.0), cos((a) * 6.28318531 / 100.0))



void main()
{
    vec2 uv = gl_FragCoord.xy / RENDERSIZE.xy;
    uv = uv * 2.0 - 1.0;
    uv.x *= RENDERSIZE.x / RENDERSIZE.y;


    // using an iq styled camera this TIME :)
    // ray origin
    vec3 ro = 0.7 * vec3(cos(SPEED *TIME), 0.0, sin(SPEED * TIME));
    ro.y = 01.;//cos(0.6 * TIME) * 0.3 + 0.65;
    // camera look at
    vec3 ta = vec3(0.0, 0.2, 0.0);

    // camera shake intensity
    float shake = clamp(3.0 * (.0 - length(ro.yz)), 0.3, 1.0);
    float st = mod(TIME*.1, 10.0) * 143.0;

    // build camera matrix
    vec3 ww = normalize(ta - ro + noise31(st) * shake * 0.01);
    vec3 uu = normalize(cross(ww, normalize(vec3(0.0, 1.0, 0.2 * sin(TIME*SPEED)))));
    vec3 vv = normalize(cross(uu, ww));
    // obtain ray direction
    vec3 rd = normalize(uv.x * uu + uv.y * vv + 1.0 * ww);

    // shaking and movement
    ro += noise31(-st) * shake * 0.015;
    ro.x += TIME * 2.0;

    float inten = 0.0;

    // background
    float sd = dot(rd, vec3(0.0, 1.0, 0.0));
	
#ifdef HAS_SKY
    pow(1.0 - abs(sd), 20.0) + pow(sky(rd), 5.0) * step(0.0, rd.y) * 0.2;
#else
	inten = 0.;
#endif
    vec3 its;
    float v, g;

    // voronoi floor layers
    for (int i = 0; i < 4; i ++)
    {
        float layer = float(i);
        its = intersect(ro, rd, vec3(0.0, -5.0 - layer * 5.0, 0.0), vec3(1.0, 0.0, 0.0), vec3(0.0, 0.0, 1.0));
        if (its.x > 0.0)
        {
            vec3 vo = voronoi((its.yz) * 0.05 + 8.0 * rand21(float(i)));
            v = exp(-100.0 * (vo.z - 0.02));

            float fx = 0.0;

            // add some special fx to lowest layer
            if (i == 3)
            {
                float crd = 0.0;//fract(TIME * 0.2) * 50.0 - 25.0;
                float fxi = cos(vo.x * 0.2 + TIME * 1.5);//abs(crd - vo.x);
                fx = clamp(smoothstep(0.9, 1.0, fxi), 0.0, 0.9) * 1.0 * rand12(vo.xy);
                fx *= exp(-3.0 * vo.z) * 2.0;
            }
            inten += v * 0.1 + fx;
        }
    }

    
    
    inten *= 0.4 + (sin(TIME) * 0.5 + 0.5) * 0.6;

    // find a color for the computed intensity
#ifdef GREEN_VERSION
    vec3 col = pow(vec3(inten), vec3(2.0, 0.15, 9.0));
#else
    vec3 col = pow(vec3(inten), 1.5 * vec3(0.15, 2.0, 9.0));
#endif

gl_FragColor = vec4(col, 1.0);
}
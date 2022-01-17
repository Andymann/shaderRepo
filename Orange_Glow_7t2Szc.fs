/*
{
    "CATEGORIES": [
        "Automatically Converted",
        "Shadertoy"
    ],
    "DESCRIPTION": "Automatically converted from https://www.shadertoy.com/view/7t2Szc by user_name.  I don't like my code. :)",
    "IMPORTED": {
    },
    "INPUTS": [
    ]
}

*/


float n(vec2 v) {
return fract(cos(dot(v, vec2(3.12394978, 6.24381234))) * 203489.1234);
}
float snap(float a, float b){
    return floor(a / b) * b;
}
float n2(vec2 v){
v.y=snap(v.y,1.0);
float a=n(vec2(v.x,v.y));
float b=n(vec2(v.x,v.y+1.0));
return mix(a,b,v.y);
}

void main() {



    // Normalized pixel coordinates (from 0 to 1)
    vec2 uv = gl_FragCoord.xy/RENDERSIZE.xy;
    vec2 uv2 = gl_FragCoord.xy/RENDERSIZE.x;
    vec2 uv3 = gl_FragCoord.xy/RENDERSIZE.x;
    uv3.y += TIME * 0.1 * (n(vec2(snap(uv2.x, 0.02), 0.0)) * 0.5 + 0.5);
    uv3.y+=n2(vec2(snap(uv3.x,0.02),uv3.y/0.015))*0.04;
    // Time varying pixel color
    vec3 col = 0.5 + 0.5*cos(TIME+uv.xyx+vec3(0,2,4));
    // Output to screen
    float dith = length(vec2(snap(uv.x, 0.02), snap(uv.y, 0.04 * RENDERSIZE.x / RENDERSIZE.y)) - 0.5);
    float mid = n(vec2(snap(uv2.x, 0.02), snap(uv2.y, 0.02)))*0.2+0.4;
    mid -= 2.0;
    mid += dith * 3.0;
    mid += length(uv - 0.5) * 1.0 - 0.2;
    uv3.y += n(vec2(snap(uv3.x, 0.02), 0.0));
    float mad = n(vec2(snap(uv3.x, 0.02), snap(uv3.y, 0.06)));
    float fac=fract(uv.x/0.02);
    fac=smoothstep(0.0,0.1,fac)*smoothstep(1.0,0.9,fac);
    float fac2=fract(uv3.y/0.06);
    fac*=smoothstep(0.0,0.05,fac2)*smoothstep(1.0,0.95,fac2);
    float gry=smoothstep(mid+0.01,mid,mad)*fac + pow(length(uv - 0.5),2.0);
    gl_FragColor = vec4(vec3(gry)*vec3(1.2,0.55,0.2),1.0);
}

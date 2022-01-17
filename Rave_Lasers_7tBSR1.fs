/*
{
    "CATEGORIES": [
        "Automatically Converted",
        "Shadertoy"
    ],
    "DESCRIPTION": "Automatically converted from https://www.shadertoy.com/view/7tBSR1 by R3N.  Lasers and fog machines",
    "IMPORTED": {
    },
    "INPUTS": [
    ]
}

*/


float rand(vec2 p) {
	return fract(sin(dot(p, vec2(12.543,514.123)))*4732.12);
}

float noise(vec2 p) {
	vec2 f = smoothstep(0.0, 1.0, fract(p));
	vec2 i = floor(p);
	float a = rand(i);
	float b = rand(i+vec2(1.0,0.0));
	float c = rand(i+vec2(0.0,1.0));
	float d = rand(i+vec2(1.0,1.0));
	return mix(mix(a, b, f.x), mix(c, d, f.x), f.y);
	
}

float fbm(vec2 p) {
    float a = 0.5;
    float r = 0.0;
    for (int i = 0; i < 8; i++) {
        r += a*noise(p);
        a *= 0.5;
        p *= 2.0;
    }
    return r;
}

float laser(vec2 p, float s) {
	float r = atan(p.x, p.y);
	float l = length(p);
	float sn = sin(r*s+TIME);
	return pow(0.5+0.5*sn,5.0)+pow(clamp(sn, 0.0, 1.0),100.0);
}

float clouds(vec2 uv) {
	float c1 = fbm(fbm(uv*3.0)*0.75+uv*3.0+vec2(0.0, + TIME/3.0));
	float c2 = fbm(fbm(uv*2.0)*0.5+uv*7.0+vec2(0.0, + TIME/3.0));
	float c3 = pow(fbm(fbm(uv*10.0-vec2(0.0, TIME))*0.75+uv*5.0+vec2(0.0, + TIME/6.0)), 2.0);
	return pow(mix(c1, c2, c3),2.0);
}

void main() {

    vec2 uv = gl_FragCoord.xy/RENDERSIZE.y;
    vec2 hs = RENDERSIZE.xy/RENDERSIZE.y*0.5;
    vec2 uvc = uv-hs;
	float ls = (1.0+3.0*noise(vec2(15.0-TIME)))*laser(vec2(uv.x+0.5, uv.y*(0.5+10.0*noise(vec2(TIME/5.0)))+0.1), 15.0);
	ls += fbm(vec2(2.0*TIME))*laser(vec2(hs.x-uvc.x-0.2, uv.y+0.1), 25.0);
	ls += noise(vec2(TIME-73.0))*laser(vec2(uvc.x, 1.0-uv.y+0.5), 30.0);
    vec4 col = vec4(0, 1, 0, 1)*((uv.y*ls+pow(uv.y,2.0))*clouds(uv));
    gl_FragColor = pow(col, vec4(0.75));
}

/*
{
    "CATEGORIES": [
        "Automatically Converted",
        "GLSLSandbox"
    ],
    "DESCRIPTION": "Automatically converted from http://glslsandbox.com/e#71704.3",
    "INPUTS": [
    ]
}

*/


#ifdef GL_ES
precision mediump float;
#endif

#define EPS .0001

float rand(vec2 co) {
	return fract(sin(dot(co, vec2(12.9898, 78.233))) * 43758.5453);
}

float noise(vec2 uv) {
	const vec2 c = vec2(0, 1);
	vec2 f = fract(uv);
//	f = f * f * (3. - 2. * f);
	float xy0 = mix(rand(floor(uv + c.ss)), rand(floor(uv + c.ts)), f.x);
	float xy1 = mix(rand(floor(uv + c.st)), rand(floor(uv + c.tt)), f.x);
	return mix(xy0, xy1, f.y);
}

float noise_2(vec2 uv) {
	const vec2 c = vec2(0, 1);
	vec2 i = floor(uv);
	vec2 f = fract(uv);
	//	f = f * f * (3. - 2. * f);
	float y0 = mix(rand(i + c.ss), rand(i + c.ts), f.x);
	float y1 = mix(rand(i + c.st), rand(i + c.tt), f.x);
	return mix(y0, y1, f.y);
}

float map(vec3 p) {
	return p.y + 1. + noise(p.xz);
	//return p.y + 1. + cos(p.x) * .5;
}

vec3 getColor(vec3 p, float dist) {
	vec2 q = abs(fract(p.xz + .5) - .5);
	float w = .01 * dist;
	float bx = smoothstep(.02, 0., q.x);
	float bz = smoothstep(w, 0., q.y);
	return vec3(1) * max(bx, bz);
}

void main( void ) {
	vec2 uv = (gl_FragCoord.xy * 2. - RENDERSIZE) / RENDERSIZE.y;
	vec3 rd = normalize(vec3(uv, 2));
	vec3 color;
	float dist = 0.;
	float prev;

	for (int i = 0; i < 50; i++) {
		vec3 p = rd * dist;
		p.z += TIME * 2.;
		float d = map(p);
		if (d < EPS) {
//			color = (abs(d) < EPS) ? getColor(p, dist) : vec3(0, 0, 1);
			color = getColor(p, dist);
			break;
		}
		prev = dist;
		dist += d;
		if (dist > 20.) break;
	}
	gl_FragColor = vec4(color, 1);
}
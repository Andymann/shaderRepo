/*
{
  "CATEGORIES" : [
    "Automatically Converted",
    "GLSLSandbox"
  ],
  "INPUTS" : [
    {
      "NAME" : "mouse",
      "TYPE" : "point2D",
      "MAX" : [
        1,
        1
      ],
      "MIN" : [
        0,
        0
      ]
    }
  ],
  "DESCRIPTION" : "Automatically converted from http:\/\/glslsandbox.com\/e#51033.0"
}
*/


// by @aa_debdeb(https://twitter.com/aa_debdeb)

#ifdef GL_ES
precision lowp float;
#endif

//#extension GL_OES_standard_derivatives : enable


float box(vec3 p, vec3 s) {
	p = abs(p) - s;
	return length(max(p, 0.0)) + min(max(max(s.x, s.y), s.z), 0.0);
}

vec3 repeatXZ(vec3 p, vec2 s) {
	p.xz = mod(p.xz, s) - 0.5 * s;
	return p;
}

float rand(vec2 co){
	return fract(sin(dot(co.xy ,vec2(12.9898,78.233))) * 43758.5453);
}

float map(vec3 p) {
	vec2 co = floor(p.xz / vec2(5.0));
	p = repeatXZ(p, vec2(5.0));
	float h = 0.5 + rand(co * 3.321) * 1.5;
	p.y -= h * 0.5;
	return box(p, vec3(1.0, h, 1.0));
}

vec3 normal(vec3 p) {
	float d = 0.01;
	return normalize(vec3(
		map(p + vec3(d, 0.0, 0.0)) - map(p - vec3(d, 0.0, 0.0)),
		map(p + vec3(0.0, d, 0.0)) - map(p - vec3(0.0, d, 0.0)),
		map(p + vec3(0.0, 0.0, d)) - map(p - vec3(0.0, 0.0, d))
	));
}

vec3 raymarch(vec3 ro, vec3 rd) {
	vec3 p = ro;
	float d = 0.0;
	for (int i = 0; i < 64; i++) {
		float t= map(p);
		p += t* rd;
		d += t;
		if (t < 0.01) {
			vec3 n = normal(p);
			float eps = 0.02;
			vec3 nx = normal(p + vec3(eps, 0.0, 0.0));
			vec3 ny = normal(p + vec3(0.0, eps, 0.0));
			vec3 nz = normal(p + vec3(0.0, 0.0, eps));
			float dif = 0.90;
			vec3 c = vec3(0.0, 0.0, 0.1);
			if (abs(dot(n, ny)) < dif || abs(dot(n, nz)) < dif) {
				c += vec3(1.0, 0.2, 0.0);
			}
			return c * exp(-0.015 * d);
		}
	}
	return vec3(0.0);
}

void main( void ) {

	vec2 st = (2.0 * gl_FragCoord.xy - RENDERSIZE) / min(RENDERSIZE.x, RENDERSIZE.y);
	
	vec2 m = (2.0 * mouse - 1.0) * 5.0;
	vec3 ro = vec3(0.0, m.y, 5.0 - TIME * 5.0);
	vec3 ta = vec3(0.0, 0.0, -TIME * 5.0);
	
	float speed = 1.0;
	float ti = floor(TIME / speed); 
	float t = mod(TIME, speed);
	
	ro = vec3(5.0 * (ti + smoothstep(0.2, 0.8, t)), 2.0, (-5.0 / speed) * TIME + 5.0 * 0.5);
	
	vec3 z = normalize(vec3(0.0, -0.3, -1.0));
	vec3 x = normalize(cross(z, vec3(0.0, 1.0, 0.0)));
	vec3 y = normalize(cross(x, z));
	vec3 rd = normalize(st.x * x + st.y * y + z);
	
	vec3 c = raymarch(ro, rd);
	
	gl_FragColor = vec4(c, 1.0);

}
/*
{
  "CATEGORIES" : [
    "Automatically Converted",
    "GLSLSandbox"
  ],
  "INPUTS" : [

  ],
  "DESCRIPTION" : "Automatically converted from http:\/\/glslsandbox.com\/e#53833.0"
}
*/


#ifdef GL_ES
precision mediump float;
#endif

//#extension GL_OES_standard_derivatives : enable


// Bleh.
// author: https://www.shadertoy.com/user/zackpudil

mat2 rot(float a) {
	float s = sin(a);
	float c =  cos(a);
	
	return mat2(c, s, -s, c);
}

float box(vec3 p, vec3 b) {
	vec3 q = abs(p) - b;
	return max(max(q.x, q.y), q.z);
}

float box(vec2 p, vec2 b) {
	vec2 q = abs(p) - b;
	return max(q.x, q.y);
}

vec2 shape(vec3 p) {
	float a = box(p.xz, vec2(0.2));
	float b = box(p.yz, vec2(0.2));
	float c = box(p.xy, vec2(0.2));
	
	vec2 t = vec2(box(p, vec3(1.0)), 2.0);
	vec2 s = vec2(min(min(a, b), c), 1.0);
	
	
	return t.x < s.x ? t : s;
}	

float glow = 0.0;
vec2 de(vec3 p) {
	vec4 q = vec4(p, 1);
	
	for(int i = 0; i < 6; i++) {
		q.xyz = abs(q.xyz) - vec3(1., 0.2, 0.2);
		q.xz *= rot(0.23*float(i) + 0.1);
		
		q *= 1.25;
		if(i == 1) {
			q.xy *= rot(0.8);
		}
	}
	
	glow += 0.1/(0.1 + pow(abs(min(length(p.xz) - 0.1, length(p.yz) - 0.1)), 2.0));
	
	return shape(q.xyz)/vec2(q.w, 1);
}

vec2 trace(vec3 ro, vec3 rd, float mx) {
	float t = 0.0, m = -1.0;
	
	for(int i = 0; i < 200; i++) {
		vec2 d = de(ro + rd*t);
		if(d.x < 0.001 || t >= mx) break;
		t += d.x*0.5;
		m = d.y;
	}
	
	return vec2(t, m);
}

void main( void ) {
	vec2 uv = (gl_FragCoord.xy - 0.5*RENDERSIZE)/RENDERSIZE.y;
	
	vec3 col, bg;
	col = bg = vec3(0);
	
	float at = 0.4*TIME;
	
	vec3 ro = vec3(3.4*sin(at), 3.7*sin(at), -3.4*cos(at));
	vec3 ww = normalize(-ro);
	vec3 uu = normalize(cross(vec3(0, 1, 0), ww));
	vec3 vv = normalize(cross(ww, uu));
	vec3 rd = normalize(mat3(uu, vv, ww)*vec3(uv, 1.0));
	
	float t = 0.0, m = -1.0, mx = 10.0;
	vec2 hit = trace(ro, rd, mx);
	
	t = hit.x, m = hit.y;
	
	vec3 ld = normalize(vec3(0.8, 0.7, -0.6));
	vec2 h = vec2(0.001, 0.0);
	
	if(t < mx) {
		vec3 p = ro + rd*t;
		vec3 n = normalize(vec3(
			de(p + h.xyy).x - de(p - h.xyy).x,
			de(p + h.yxy).x - de(p - h.yxy).x,
			de(p + h.yyx).x - de(p - h.yyx).x));
		
		vec3 al = vec3(1);
		if(m == 1.0) al = vec3(0.01);
		else if(m == 2.0) al = vec3(0.1, 0.4, 0.7);
		
		
		float aot = t/50.0;
		float ao = exp2(-pow(max(0.0, 1.0 - de(p + n*aot).x/aot), 2.0));
		
		float dif = max(0.0, dot(ld, n));
		float spe = pow(max(0.0, dot(reflect(-ld, n), -rd)), 10.0);
		float fre = pow(1.0 + dot(rd, n), 2.0);
		
		col = ao*mix(al*(dif + 0.25 + spe) + vec3(0.3, 0.4, 0.8)*spe*spe, al, fre);
	}
	
	col += 0.01*vec3(0.2, 0.2, 1.0)*glow*glow;
	col = mix(col, bg, 1.0 - exp(-0.4*t));
	
	col = 1.0 - exp(-0.4*col);
	gl_FragColor = vec4(pow(abs(col), vec3(0.45)), 1);
}
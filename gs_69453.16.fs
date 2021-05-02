/*
{
    "CATEGORIES": [
        "Automatically Converted",
        "GLSLSandbox"
    ],
    "DESCRIPTION": "Automatically converted from http://glslsandbox.com/e#69453.16",
    "INPUTS": [
        {
            "MAX": [
                1,
                1
            ],
            "MIN": [
                0,
                0
            ],
            "NAME": "mouse",
            "TYPE": "point2D"
        }
    ]
}

*/


#ifdef GL_ES
precision mediump float;
#endif

//#extension GL_OES_standard_derivatives : enable


// constants, config etc
float particle_radius = 0.003;
float speed = 0.004;
#define line_thickness 0.001
#define point_max_line_dist 0.35
#define line_brightness_multiplier 3.0
#define particles_num 20
#define mouse_repel_radius 0.3

float drawLine(vec2 p1, vec2 p2, float thick) {
  vec2 uv = gl_FragCoord.xy / RENDERSIZE.xy;

  float a = abs(distance(p1, uv));
  float b = abs(distance(p2, uv));
  float c = abs(distance(p1, p2));

  if ( a >= c || b >=  c ) return 0.0;

  float p = (a + b + c) * 0.5;

  float h = 2. / c * sqrt( p * ( p - a) * ( p - b) * ( p - c));

  return mix(1.0, 0.0, smoothstep(0.5 * thick, 1.5 * thick, h));
}

float draw_circle(vec2 position, float radius) {
    return step(length(position), radius);
}

float rand(vec2 co){
    return sin(dot(co.xy ,vec2(12.9898,78.233))) * 43758.5453;
}

vec3 rgb2hsv(vec3 c)
{
    vec4 K = vec4(0.0, -1.0 / 3.0, 2.0 / 3.0, -1.0);
    vec4 p = mix(vec4(c.bg, K.wz), vec4(c.gb, K.xy), step(c.b, c.g));
    vec4 q = mix(vec4(p.xyw, c.r), vec4(c.r, p.yzx), step(p.x, c.r));

    float d = q.x - min(q.w, q.y);
    float e = 1.0e-10;
    return vec3(abs(q.z + (q.w - q.y) / (6.0 * d + e)), d / (q.x + e), q.x);
}

vec2 points[particles_num];

void main(void) {
	for (int i=0;i<particles_num;i++) {
		float sx = float(i)/100.;
		float sy = float(i+1)/100.;
		
		float rx = rand(vec2(sx,sy));
		float ry = rand(vec2(sy,sx));
		
		float dirX = speed * rx;
		float dirY = speed * ry;
		
		float t = mod(TIME,25.0) + 25.0;
		
		float x = mod((dirX * t),1000.) / 1000.;
		float y = mod((dirY * t),1000.) / 1000.;
		
		float mdist = distance(vec2(x,y),mouse);
		
		if (mdist < mouse_repel_radius) {
			vec2 awayVector = vec2(x,y) - mouse;
			float angle = atan(awayVector[0],awayVector[1]);
			x-=cos(angle)*mouse_repel_radius;
			y-=sin(angle)*mouse_repel_radius;
		}
		
		points[i] = vec2(x,y);
		
		vec2 position = (gl_FragCoord.xy / RENDERSIZE) - vec2(x,y);
		
		float circle = draw_circle(position, particle_radius);
		vec3 color = vec3(circle);
	
		gl_FragColor += vec4(color, t);

	}
	for (int a=0;a<particles_num;a++) {
		for (int b=0;b<particles_num;b++) {
			float dist = distance(points[a],points[b]);
			if (dist <= point_max_line_dist) {
				float thickness = (dist<=point_max_line_dist) ? float(line_thickness) : 0.0;
				float brightness = 1.0-(dist* line_brightness_multiplier);
				vec3 col = vec3(brightness);
				if (thickness>0.0) {
					gl_FragColor += vec4(vec3(drawLine(points[a],points[b],thickness))*col,1.0);
				}
			}
		}
	}
}
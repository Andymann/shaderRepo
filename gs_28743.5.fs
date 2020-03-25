/*
{
  "CATEGORIES" : [
    "Automatically Converted",
    "GLSLSandbox"
  ],
  "DESCRIPTION" : "Automatically converted from http:\/\/glslsandbox.com\/e#28743.5",
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


#ifdef GL_ES
precision mediump float;
#endif


#define PI 3.14
#define BALL_NUM 100

void main( void ) {
	vec2 uv2 = gl_FragCoord.xy / RENDERSIZE * 2.0 - 1.0;
	uv2.x *= RENDERSIZE.x/RENDERSIZE.y;
	
	float o = 0.0;
	vec3 color = vec3(.0);
	
	for (int i = 0; i < BALL_NUM; ++i) {
		vec2 uv = uv2;
		uv.x += sin(TIME * 1.0 + o) + cos(TIME * 2.0 + o) + sin(o * 7.2);
		uv.y += cos(TIME * 1.0 + o) + sin(TIME * 2.0 + o) + cos(o * 12.7);
		float t = TIME;
		float d = length(uv) - 0.03 - pow(sin(TIME * 2.0) * 0.3, 2.0);
		color = mix(color, vec3(sin(o - t), sin(o*8.0+6.0 + t), cos(o*13.0*16.0 + t))*0.5+0.5, smoothstep(0.01, -0.01, d));
		color = mix(color, IMG_NORM_PIXEL(backbuffer,mod(gl_FragCoord.xy / RENDERSIZE,1.0)).rgb, 0.01);
		
		o += 2.0 * PI / float(BALL_NUM);
	}
	
	gl_FragColor = vec4(color, 1.0 );
}
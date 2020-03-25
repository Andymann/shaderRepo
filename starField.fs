/*
{
  "CATEGORIES": [
    "Automatically Converted"
  ],
  "DESCRIPTION": "Automatically converted from http://glslsandbox.com/e#36803.0",
  "INPUTS": [
    {
			"NAME": "inputImage",
			"TYPE": "image"
		},
		{
			"NAME": "length",
			"TYPE": "float",
			"MIN": 0.01,
			"MAX": 1.0,
			"DEFAULT": 0.5
		},
		{
			"NAME": "density",
			"TYPE": "float",
			"MIN": 0.001,
			"MAX": 0.01,
			"DEFAULT": 0.007
		}
  ],
  "PASSES": [
    {
      "TARGET": "backbuffer"
    }
  ],
  "PERSISTENT_BUFFERS": [
    "backbuffer"
  ]
}
*/


//--- FTL ---
// by Catzpaw 2016
#ifdef GL_ES
precision mediump float;
#endif

#extension GL_OES_standard_derivatives : enable


vec3 star(vec2 uv,float scale,float seed){
	uv*=scale;
	vec2 s=floor(uv), f=fract(uv), p;
	float k=3., d;
	p=.5+.440*sin(11.*fract(sin((s+seed)*mat2(7.5,3.3,6.2,5.4))*55.))-f;d=length(p);k=min(d,k);
	k=smoothstep(0.,k,density);
    	return k*vec3(k,k,1.);
}

void main(void){
	vec4 b=IMG_NORM_PIXEL(backbuffer,mod(gl_FragCoord.xy/RENDERSIZE.xy,1.0));
	vec2 uv=(gl_FragCoord.xy*2.-RENDERSIZE.xy)/min(RENDERSIZE.x,RENDERSIZE.y); 
	vec3 c=vec3(0);
	for(float i=0.;i<20.;i+=2.)c+=star(uv,mod(20.+i-TIME*1.,20.),i*5.1);
	gl_FragColor = vec4(c+b.gbb*length,0.5);
}
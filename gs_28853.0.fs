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
  "DESCRIPTION" : "Automatically converted from http:\/\/glslsandbox.com\/e#28853.0"
}
*/


#ifdef GL_ES
precision mediump float;
#endif

#define r RENDERSIZE
#define c gl_FragCoord
#define xy vv_FragNormCoord

const float hpi = 3.14159265358979*0.5;

void main( void ) {
	gl_FragColor = vec4(0);
	
	//vec2 xy = ( c.xy / r.xy ) - 0.5;xy.y *= r.y/r.x;
	
	float s = length(xy)*pow(5., mouse.x+1.);
	float fs = fract(s);
	float t = atan(xy.x, xy.y);
	
	t += (s-fs)*TIME*mouse.y*2.;
	t = mod(t, hpi*4.)-hpi*2.;
	
	if(t > hpi) return;
	
	if(t < 0. && t > -hpi) return;
	
	if(fs < 0.9) gl_FragColor = vec4(1);
	
	if(t < 0.) gl_FragColor.xyz /= 2.;
	
}
//+pk
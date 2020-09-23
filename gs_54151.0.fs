/*
{
  "CATEGORIES" : [
    "Automatically Converted",
    "GLSLSandbox"
  ],
  "INPUTS" : [

  ],
  "DESCRIPTION" : "Automatically converted from http:\/\/glslsandbox.com\/e#54151.0"
}
*/


#ifdef GL_ES
precision mediump float;
#endif

//#extension GL_OES_standard_derivatives : enable


#define PI 3.1415926535897932384626433832795

void main() {
	float scale = 24.0;

	vec2 g = gl_FragCoord.xy;
	g = ceil(g / scale) * scale;
	float v = 0.0;
	vec2 c = g.xy / RENDERSIZE.xy + RENDERSIZE.xy/2.0;
	v += sin((c.x*10.0+TIME));
	v += sin((c.y*10.0+TIME)/2.0);
	v += sin((c.x*10.0+c.y*10.0+TIME)/2.0);
	float cx = c.x+.5*sin(TIME/5.);
	float cy = c.y+.5*cos(TIME/3.);
	v += sin(sqrt(cx*cx+cy*cy+1.0)+TIME);
	v = v/2.0;

	float power = 60.;
	v = floor(v*power)/power;
	vec3 col = vec3(0.5*(.5+.5*sin(PI*v+2.0*TIME/5.0)), 0.5*(.5+.5*sin(PI*v)), 0.5*(.5+.5*sin(PI*v+4.0*TIME/2.5)));
	gl_FragColor = vec4(col, 1);
}
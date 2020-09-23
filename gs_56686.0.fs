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
  "DESCRIPTION" : "Automatically converted from http:\/\/glslsandbox.com\/e#56686.0"
}
*/


#ifdef GL_ES
precision mediump float;
#endif

//#extension GL_OES_standard_derivatives : enable


vec3 HSV2RGB(vec3 c)
{
	vec4 K = vec4(1.0, 2.0 / 3.0, 1.0 / 3.0, 3.0);
	vec3 p = abs(fract(c.xxx + K.xyz) * 6.0 - K.www);
	return c.z * mix(K.xxx, clamp(p - K.xxx, 0.0, 1.0), c.y);
}

void main( void ) {

	vec2 position =  gl_FragCoord.xy / RENDERSIZE.xy ;

	float koef = mouse.x - position.x + TIME * 0.1;
	float vstep = koef * 10.0;
	float vfloor = floor(vstep);
	float vfract = fract(vstep);
	float top = clamp(vfract * 5. , 0., 1.);
	float color = vfloor * .1;//, -1.0, 1.0));
	
	gl_FragColor = vec4(HSV2RGB(vec3(color, 1.0, 1.0)) * (0.3 + top*.7), 1.0 );

}
/*
{
  "CATEGORIES" : [
    "Automatically Converted",
    "GLSLSandbox"
  ],
  "DESCRIPTION" : "Automatically converted from http:\/\/glslsandbox.com\/e#36930.0",
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
    },
    {
      "NAME" : "Speed",
      "TYPE" : "float",
      "MAX" : 2.0,
      "DEFAULT" : 0.5,
      "LABEL" : "Speed",
      "MIN" : 0
    },
    {
      "NAME" : "Form",
      "TYPE" : "float",
      "DEFAULT" : 1.0,
      "LABEL" : "Form",
      "MIN" : 0.0,
      "MAX" : 2.0
    }
  ],
  "ISFVSN" : "2"
}
*/


#ifdef GL_ES
precision mediump float;
#endif

#extension GL_OES_standard_derivatives : enable


void main( void ) {

	vec2 position = (( gl_FragCoord.xy / RENDERSIZE.xy )- mouse);
	position.x *= RENDERSIZE.x/RENDERSIZE.y;
	float x = pow(position.x,Form);
	float y = pow(position.y,Form);
	float c = pow(x+y,.5);
	float z = mod(Speed*TIME,10.)+10.;
	vec4 color = vec4(0.0);
float t=c;
	for(float m = 1.; m < 100.; m++){
			 t *= m/z;
	if( t< 1.){
			if( t > .9){
		color += vec4(1.,y ,t, 1.0 );
			}}}
	
	gl_FragColor = color;

}
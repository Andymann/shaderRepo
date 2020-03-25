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
  "DESCRIPTION" : "Automatically converted from http:\/\/glslsandbox.com\/e#45767.1"
}
*/




//precision highp float;


const float PI = 3.141592653589793;
const float TWOPI = PI * 2.0;

float norm(float f){
	return (f + 1.0) * 0.5;
}

float wave(vec2 p, float seed,  float thickness, float TIME )
{	
	float dx = (p.x+mouse.x*0.1) - p.y * 0.1 + TIME * 0.0001 - thickness * 0.25;
	float f = cos(dx*TWOPI*1.205);
	float w = norm( sin( dx * TWOPI * seed * thickness + TIME  ) );
	return w;
}


void main(void)
{
	
	vec3 color = vec3(0.);
	vec2 p = ( gl_FragCoord.xy / RENDERSIZE.xy );
	
	
	
	float wavetime = TIME * 0.5;
	float thickness = abs( norm( sin(wavetime*1.0) ) ) + 1.0;
	
	float brightness = 1.0;
	brightness *=  wave(p, 4.0, thickness, wavetime );
	brightness +=  wave(p, abs(sin(TIME*0.1))*12.0, thickness*0.5, wavetime );
	brightness *=  wave(p, 5.0, thickness*0.33, wavetime );
	
	

	brightness /= .510;
	
	float maxy = 1.0 - pow( norm( sin(p.x * PI * 6.0 + wavetime ) ), 2.0 );
	brightness *= p.y * maxy;

	
	
	
	gl_FragColor = vec4( brightness,brightness,brightness, 1.0 );
}
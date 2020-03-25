/*
{
  "CATEGORIES" : [
    "Automatically Converted"
  ],
  "DESCRIPTION" : "Automatically converted from http:\/\/glslsandbox.com\/e#36672.0",
  "INPUTS" : [
    {
      "NAME" : "inputImage",
      "TYPE" : "image"
    },
    {
      "NAME" : "Size",
      "TYPE" : "float",
      "MAX" : 40,
      "DEFAULT" : 8,
      "MIN" : 0.01
    },
    {
      "NAME" : "Red",
      "TYPE" : "float",
      "MAX" : 1,
      "DEFAULT" : 1,
      "LABEL" : "Red",
      "MIN" : 0
    },
    {
      "NAME" : "Green",
      "TYPE" : "float",
      "DEFAULT" : 1,
      "LABEL" : "Green",
      "MIN" : 0
    },
    {
      "NAME" : "Blue",
      "TYPE" : "float",
      "MAX" : 1,
      "DEFAULT" : 1,
      "LABEL" : "Blue",
      "MIN" : 0
    },
    {
      "NAME" : "Bright",
      "TYPE" : "float",
      "MAX" : 1,
      "DEFAULT" : 0.5,
      "LABEL" : "Bright",
      "MIN" : 0
    }
  ],
  "ISFVSN" : "2"
}
*/


#ifdef GL_ES
precision mediump float;
#endif

//#extension GL_OES_standard_derivatives : enable


	
vec2 uv = vec2(0);
float hash(float n) { return fract(sin(n)*12345.0); } 
 float old_noise(vec3 x, float c1, float c2) {
	vec3 p = floor(x);
	vec3 f = fract(x);
	f       = f*f*(3.0-2.0*f);
	
	float h2 = c1;
	 float h1 = c2;
	#define h3 (h2 + h1)
	 
	 float n = p.x + p.y*h1+ h2*p.z;
	return mix(mix(	mix( hash(n+0.0), hash(n+1.0),f.x),
			mix( hash(n+h1), hash(n+h1+1.0),f.x),f.y),
		   mix(	mix( hash(n+h2), hash(n+h2+1.0),f.x),
			mix( hash(n+h3), hash(n+h3+1.0),f.x),f.y),f.z);
	
}
// I want to mark that this is created by accident
// awesome fucking shit this noise is at least useful now
// afl_ext 2016
float supernoise3d(vec3 p){

	float a =  old_noise(p, 883.0, 971.0);
	float b =  old_noise(p + 0.5, 113.0, 157.0);
	return (a + b) * Bright;
}
 
void main(){
	 uv = gl_FragCoord.xy/RENDERSIZE.xy;
	 float temp = supernoise3d(vec3(TIME, uv.x * Size, uv.y * Size));
	//gl_FragColor = vec4(supernoise3d(vec3(TIME, uv.x * Size, uv.y * Size)));
	gl_FragColor = vec4(temp* Red, temp*Green , temp*Blue , 1.0);
}
// it's working now for me gtr;
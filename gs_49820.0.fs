/*
{
  "CATEGORIES" : [
    "Automatically Converted",
    "GLSLSandbox"
  ],
  "INPUTS" : [

  ],
  "DESCRIPTION" : "Automatically converted from http:\/\/glslsandbox.com\/e#49820.0"
}
*/


#ifdef GL_ES
precision mediump float;
#endif

//#extension GL_OES_standard_derivatives : enable

#define PI 3.14159265

float polygon(vec2 st, float radius, int N)
{

  float a = atan(st.x,st.y)+PI;
  float r = 2.*PI/float(N);

  // Shaping function that modulate the distance
  float d = cos(floor(0.5 + a/r)*r - a + TIME);

  d *= length(st);

  d = fract(d);

  d = step(dot(radius, radius), dot(d, d));

  return 1.-d;
}
void main(void) {
  vec2 uv = gl_FragCoord.xy / RENDERSIZE.xy;
  uv = uv*2.0 - 1.0;

  float p = polygon(uv, 0.5, 5);


  gl_FragColor = vec4(vec3(p), 1.0);
}
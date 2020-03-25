/*
{
  "CATEGORIES" : [
    "Automatically Converted",
    "GLSLSandbox"
  ],
  "INPUTS" : [

  ],
  "DESCRIPTION" : "Automatically converted from http:\/\/glslsandbox.com\/e#57055.0"
}
*/


#ifdef GL_ES
precision highp float;
#endif


float box(float edge0, float edge1, float x)
{
	return step(edge0, x) - step(edge1, x);
}

float ringShape(vec2 p, float t)
{
	return clamp(box(t, t * 1.2, length(p)) - t, 0.0, 1.0);
}

float ringInstance(vec2 p, float t, float xden, float yden)
{
	float th = floor(t) * 47.0;
	return ringShape(p - vec2(mod(th, xden) / xden, mod(th, yden) / yden) * 2.0 + 1.0, fract(t));
}

void main( void ) {

	vec2 p = ((gl_FragCoord.xy / RENDERSIZE.xy) * 2.0 - 1.0) * vec2(RENDERSIZE.x / RENDERSIZE.y, 1.0);

	float t = TIME / 3.0 + 5.0;

	gl_FragColor.a = 1.0;
	gl_FragColor.rgb = 	ringInstance(p, t - 0.0, 7.0,  13.0) * vec3(1.0, 1., 1.) +
				ringInstance(p, t - 0.6, 3.0,   5.0) * vec3(1., 1.0, 1.) +
				ringInstance(p, t - 0.2, 11.0, 23.0) * vec3(1.0, 1.0, 1.) +
				ringInstance(p, t - 0.9, 17.0, 19.0) * vec3(1., 1., 1.0);
}
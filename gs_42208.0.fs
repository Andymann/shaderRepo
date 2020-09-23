/*
{
  "CATEGORIES" : [
    "Automatically Converted",
    "GLSLSandbox"
  ],
  "INPUTS" : [

  ],
  "DESCRIPTION" : "Automatically converted from http:\/\/glslsandbox.com\/e#42208.0"
}
*/


#ifdef GL_ES
precision highp float;
#endif

//#extension GL_OES_standard_derivatives : enable


vec2 rand2(vec2 p)
{
	p = vec2(dot(p, vec2(102.9898,78.233)), dot(p, vec2(26.65125, 83.054543))); 
	return fract(sin(p) * 43758.5453);
}

float rand(vec2 p)
{
	return fract(sin(dot(p.xy ,vec2(505.90898,18.233))) * 43037.5453);
}

// Thanks to David Hoskins https://www.shadertoy.com/view/4djGRh
float stars(in vec2 x, float numCells, float size, float br)
{
	vec2 n = x * numCells;
	vec2 f = floor(n);

	float d = 1.0e10;
	for (float i = -1.; i <= 1.; ++i)
	{
		for (float j = -1.; j <= 1.; ++j)
		{
			vec2 g = f + vec2(i, j);
			g = n - g - rand2(mod(g, numCells)) + rand(g);
			// Control size
			g *= 1. / (numCells * size);
			d = min(d, dot(g, g));
		}
	}
	
	return br * (smoothstep(.98, 1., (1. - sqrt(d))));
}
 

void main()
{
	float res  = max(RENDERSIZE.x, RENDERSIZE.y);

	vec2 coord = gl_FragCoord.xy / res;
		
	vec3 result = vec3(0.);
	
	float s = 1.;
	float n = 5.;
	
	for(int i = 0; i<9; i++){
		float t = TIME / 2.;
		result +=
			stars(
				vec2(
					coord.x/2. + TIME/6. * s,
					coord.y/2. - sin(TIME/6.) * s
					),
				n,
				s/4.,
				1.
				)
			* vec3(s*2., s*2., 1); 

		s /= 1.33;
		n *= 1.5;
	}

	gl_FragColor = vec4(result,1.);
}
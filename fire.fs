/*
{
  "CATEGORIES" : [
    "XXX"
  ],
  "DESCRIPTION" : "",
  "ISFVSN" : "2",
  "INPUTS" : [
    {
      "NAME" : "Amount",
      "TYPE" : "float",
      "MAX" : 12,
      "DEFAULT" : 6,
      "LABEL" : "Amount",
      "MIN" : 1
    }
  ],
  "CREDIT" : ""
}
*/


#ifdef GL_ES
precision mediump float;
#endif



float rand(vec2 n)
	{
	return fract(sin(dot(n, vec2(12.9898, 4.1414))) * 43758.5453);
	}

float noise(vec2 n)
	{
	const vec2 d = vec2(0.0, 1.0);
	vec2 b = floor(n), f = smoothstep(vec2(0.0), vec2(1.0), fract(n));
	return mix(mix(rand(b), rand(b + d.yx), f.x), mix(rand(b + d.xy), rand(b + d.yy), f.x), f.y);
	}

float fbm(vec2 n)
	{
	float total = 0.0, amplitude = 1.0;
	for (int i = 0; i < 5; i++)
		{
		total += noise(n) * amplitude;
		n += n;
		amplitude *= 0.5;
		}
	return total;
	}


void main( void ) 
	{
	vec2 uv = (gl_FragCoord.xy / RENDERSIZE.xy * 2. - 1.)*2.00;


	const vec3 c1 = vec3(0.1, 0.0, 0.0);
	const vec3 c2 = vec3(0.7, 0.0, 0.0);
	const vec3 c3 = vec3(0.2, 0.0, 0.0);
	const vec3 c4 = vec3(1.0, 0.9, 0.0);
	const vec3 c5 = vec3(0.1);
	const vec3 c6 = vec3(0.9);
	vec2 p = gl_FragCoord.xy * Amount / RENDERSIZE.xx;
	float q = fbm(p - TIME * 0.1);
	vec2 r = vec2(fbm(p + q + TIME * 0.7 - p.x - p.y), fbm(p + q - TIME * 0.4));
	vec3 c = mix(c1, c2, fbm(p + r)) + mix(c3, c4, r.x) - mix(c5, c6, r.y);
	c=c * cos(1.57 * gl_FragCoord.y / RENDERSIZE.y);	
	gl_FragColor = vec4(c, 1);
	}
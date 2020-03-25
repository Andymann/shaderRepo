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
  "DESCRIPTION" : "Automatically converted from http:\/\/glslsandbox.com\/e#27553.1"
}
*/


#ifdef GL_ES
precision mediump float;
#endif


float tau = atan(1.0)*8.0;

vec2 angleRep(float a, vec2 p)
{
	float pl = length(p);
	float pa = atan(p.y,p.x)+TIME*pow(floor(pl+1.6), -3.5);
	
	pa = mod(pa - a/2., a) - a/2.;
	
	return vec2(cos(pa),sin(pa))*pl;
}

float opU( float d1, float d2 )
{
    return min(d1,d2);
}

float opS( float d1, float d2 )
{
    return max(-d1,d2);
}

float opI( float d1, float d2 )
{
    return max(d1,d2);
}

float sdCircle(float r, vec2 p)
{
	return length(p) - r;
}

float sdBox(vec2 s, vec2 p)
{
    p = abs(p) - s / 2.0;
    return max(p.x,p.y);
}

float map(vec2 p)
{
	float dist = 1e6;
	
	vec2 p1 = mod(p, vec2(0.25)) - 0.125;
	vec2 p2 = angleRep(tau/12.,p);
	
	float ring = opS(sdCircle(0.275, p), sdCircle(0.325, p));
	ring = opS(sdBox(vec2(0.2,0.075), p2 - vec2(0.3,0.0)), ring);
	
	
	dist = opU(dist, sdCircle(0.1, p));
	dist = opU(dist, sdCircle(0.05, p2 - vec2(0.5,0.0)));
	dist = opU(dist, ring);
	
	return dist;
}

float shadow(vec2 p, vec2 l)
{
	vec2 dir = normalize(l - p);
	float dist = distance(p, l);
	float t = 0.0;
	float s = 1.0;
	for(int i = 0;i < 96;i++)
	{
		float sd = map(p + dir * t);
		t += sd * 0.2;
		
		s = min(s, 16.0*sd/t);
		
		if(sd < 0.0001 || t > dist)
		{
			break;	
		}
	}
	
	return (t < dist) ?  0.0 : s;
}

void main( void ) 
{
	vec2 aspect = RENDERSIZE.xy / RENDERSIZE.y;
	vec2 uv = ( gl_FragCoord.xy / RENDERSIZE.y ) - aspect/2.0;
	vec2 mo = mouse * aspect - aspect/2.0;
	
	vec3 color = vec3(0.0);
	
	float d = map(uv);
	
	vec2 li = mo;//vec2(cos(TIME),sin(TIME))*0.2;
	
	vec3 bg = vec3(0.75);
	bg *= smoothstep(-0.02,0.02,d);
	bg *= (shadow(uv, li) * max(0.0, 1.0 - distance(uv,li) * 2.5))*0.75+0.125;
	
	vec3 fg = vec3(1.0,0.5,0.0) * smoothstep(0.000,0.003,-map(uv));
	
	color = mix(fg, bg, smoothstep(0.000,0.001,map(uv)));
	
	gl_FragColor = vec4( vec3( color ), 1.0 );

}
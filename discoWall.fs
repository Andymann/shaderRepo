/*
{
  "CATEGORIES" : [
    "XXX"
  ],
  "DESCRIPTION" : "",
  "ISFVSN" : "2",
  "CREDIT" : ""
}
*/


vec3 tiles(vec2 uv)
{
	vec2 a = floor(uv);
        vec2 b = fract(uv);
	
	float w = fract(sin(a.x*2389.543+a.y*438.234+TIME));
	
	float t = sqrt(b.x*(1.0-b.x)*b.y*(1.0-b.y))*3.0;
	t *= smoothstep(0.0, 2.0, w);

	vec3 color = pow(2.5*vec3(t), vec3(0.5, 1.0, 0.2));
	
	return color;
	
}

vec2 getModifiedUV(vec2 actualUV, vec2 pointUV, float radius, float strength)
{
     vec2 vecToPoint = pointUV - actualUV;
     float distToPoint = length(vecToPoint);

     float mag = (1.0 - (distToPoint / radius)) * strength;
     mag *= step(distToPoint, radius);

     return actualUV + (mag * vecToPoint);
}

void main( void ) 
{
	vec2 uv = ( gl_FragCoord.xy / RENDERSIZE.xy ) * 2.0 - 1.0 ;
	uv.x *= RENDERSIZE.x / RENDERSIZE.y;
	uv *= 8.0;
	
	vec2 buldgeOrigin = vec2( cos(TIME*3.0)* 2.0, sin(TIME) * 3.0 );
	
	float t = sin(TIME)*0.5 + 0.5;
	float strength = mix(-0.3, 0.5, t);
	uv = getModifiedUV(uv, buldgeOrigin, 6.0, strength);
	
	vec3 finalColor = tiles(uv);
	finalColor.r *= sin(buldgeOrigin.y)* 0.5 + 0.5;
	
	gl_FragColor = vec4( finalColor , 1.0 );
}
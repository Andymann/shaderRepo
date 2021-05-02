/*
{
    "CATEGORIES": [
        "Automatically Converted",
        "GLSLSandbox"
    ],
    "DESCRIPTION": "Automatically converted from http://glslsandbox.com/e#69145.0",
    "INPUTS": [
    ]
}

*/


#ifdef GL_ES
precision mediump float;
#endif



float size = 20.0;
float speed= 1.0;

float randomize(vec2 coords)
{
	float a = 1.0;
    	float b = 1.0;
    	float c = 1.0;
    	float dt= dot(coords.xy ,vec2(a,b));
    	float sn= mod(dt,3.14);
    	return fract(sin(dt) * c);
}



float triangleWave(float x)
{
	x = mod(x,2.0);
	if (x > 1.0) x = -x+2.0;
	return x;
}

bool inSize(vec2 coords)
{
	vec2 box = coords.xy-mod(coords.xy, size);
	vec2 center = box+(size/2.0);
	float size = (triangleWave((TIME * speed)+(randomize(box*98.0)*2.0))/2.0)*(size);
	return (abs(coords.x-center.x) < size && abs(coords.y-center.y) < size);
}


void main( void ) 
{
	vec3 color = vec3(0.0);
        if (inSize(gl_FragCoord.xy)) color = vec3(0.1,0.1,0.1);
	gl_FragColor = vec4( color, 1.0 );

}

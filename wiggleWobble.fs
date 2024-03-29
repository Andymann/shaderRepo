/*{
    "CATEGORIES": [
        "XXX"
    ],
    "CREDIT": "",
    "DESCRIPTION": "",
    "INPUTS": [
        {
            "DEFAULT": 0.5,
            "LABEL": "Wiggle",
            "MAX": 1,
            "MIN": 0,
            "NAME": "Wiggle",
            "TYPE": "float"
        }
    ],
    "ISFVSN": "2"
}
*/


#ifdef GL_ES
precision mediump float;
#endif

// Posted by Trisomie21
// modified by @hintz


void main(void)
{
	float scale = RENDERSIZE.y / 50.0;
	float ring = 20.0;
	float radius = RENDERSIZE.x*1.0;
	float gap = scale*.5;
	vec2 pos = gl_FragCoord.xy - RENDERSIZE.xy*.5;
	
	float d = length(pos);
	
	// Create the wiggle
	d += Wiggle*(sin(pos.y*0.25/scale+TIME)*sin(pos.x*0.25/scale+TIME*.5))*scale*5.0;
	
	// Compute the distance to the closest ring
	float v = mod(d + radius/(ring*2.0), radius/ring);
	v = abs(v - radius/(ring*2.0));
	
	v = clamp(v-gap, 0.0, 1.0);
	
	d /= radius;
	vec3 m = fract((d-1.0)*vec3(ring*-.5, -ring, ring*.25)*0.5);
	
	gl_FragColor = vec4(m*v, 1.0);
}
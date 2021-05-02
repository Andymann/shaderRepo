/*
{
    "CATEGORIES": [
        "Automatically Converted",
        "GLSLSandbox"
    ],
    "DESCRIPTION": "Automatically converted from http://glslsandbox.com/e#70375.1",
    "INPUTS": [
    ]
}

*/


// added simple motion. kudos to this guy!
//
// raymarching thing. •͡˘㇁•͡˘ 
// by spleenooname <spleen666@gmail.com>
//
// hwy! u should visit:
//                   __ __
// _   _____  _____/ // /
//| | / / _ \/ ___/ // /_
//| |/ /  __/ /__/__  __/
//|___/\___/\___/  /_/
//
// https://www.facebook.com/groups/vec4glsl



float map(vec3 p) {
	p.x += 5.0;
	p.z += TIME * 8.0;
	p = mod(p, 20.0) - 10.0;
	p.xy = abs(p.xy);
	return length( cross(p, vec3(0.5)) ) - 0.31;
}

void main( void ) {
	vec2 uv = (gl_FragCoord.xy - (.5+sin(TIME*.5)*0.25) * RENDERSIZE) / min(RENDERSIZE.y, RENDERSIZE.x);
	vec3 rd = normalize(vec3(uv, 0.5));
	vec3 p = vec3(0, 0, 0.0);
	for (float i = 1.; i < 40.; i+=1.0) {
		float d = map(p);
		p += rd * d;
		if (d < 0.001) {
			gl_FragColor = vec4(vec3(8.0 / i, 0., 0.), 1);
			break;
		}
	}
}
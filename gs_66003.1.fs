/*
{
    "CATEGORIES": [
        "Automatically Converted",
        "GLSLSandbox"
    ],
    "DESCRIPTION": "Automatically converted from http://glslsandbox.com/e#66003.1",
    "INPUTS": [
    ]
}

*/


#ifdef GL_ES
precision mediump float;
#endif


// 2D Random
float random (in vec2 st) {
    return fract(sin(dot(st.xy,vec2(12.9898,70.233)))* 43758.5453123);
}

// 2D Noise based on Morgan McGuire @morgan3d
// https://www.shadertoy.com/view/4dS3Wd
float noise (in vec2 st) {
    vec2 i = floor(st);
    vec2 f = fract(st);

    // Four corners in 2D of a tile
    float a = random(i);
    float b = random(i + vec2(1.0, 0.0));
    float c = random(i + vec2(0.0, 1.0));
    float d = random(i + vec2(1.0, 1.0));

    // Smooth Interpolation

    // Cubic Hermine Curve.  Same as SmoothStep()
    //vec2 u = f*f*(3.0-2.0*f);
    vec2 u = smoothstep(0.0,1.,f);

    // Mix 4 coorners percentages
    return mix(a, b, u.x) +(c - a)* u.y * (1.0 - u.x) + (d - b) * u.x * u.y;
}

#define OCTAVES 3
float fbm (in vec2 st){
	float value = 0.0;
	float amplitude = 0.5;
	float frequency = 0.;
	for (int i = 0; i < OCTAVES;i++){
		value += clamp(amplitude*noise(st),0.0,0.5);
		st*=2.0;
		amplitude *= 0.2;
	}
	
	return value*=1.0;
}


void main() {
	float pixels = 0.015;
	

	
	vec2 st = gl_FragCoord.xy/RENDERSIZE.xy;
	st.y *= 2.0;
	st = ceil(st/pixels)*pixels;
	st.y *= 0.5;
	st*=10.0;
	
	vec3 color = vec3(0.0);
	
	vec2 q = vec2(0.);
	q.x = fbm(st + 4.0* TIME);
	q.y = fbm(st + vec2(1.0));
	
	vec2 r = vec2(0.);
 	r.x = fbm( st + 0.1*q + vec2(1.0,1.0)+ 0.5*TIME );
 	r.y = fbm( st + 0.0*q + vec2(10.0,10.0)+ 0.5*TIME);
	
	float f = fbm(st+r);
	
	
	color = mix(vec3(0.0,0.0,0.0),vec3(25.0,25.0,25.0),clamp((pow(f,3.0)),0.0,0.07));
	//color = mix(vec3(0.50,0.4,0.50),vec3(0.7,0.3,0.5),clamp((f*f)*4.0,0.0,1.0));

	
	
	
	gl_FragColor = vec4((f)*color,1.);
}
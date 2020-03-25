/*
{
  "CATEGORIES" : [
    "Automatically Converted",
    "GLSLSandbox"
  ],
  "INPUTS" : [

  ],
  "DESCRIPTION" : "Automatically converted from http:\/\/glslsandbox.com\/e#31893.4"
}
*/


#ifdef GL_ES
precision mediump float;
#endif

const float PI = 3.14159265358979323;// lol ya right, but hey, I memorized it

vec4 pixelAt(vec2 uv)
{
	vec4 result;
	float thickness = 0.05;
	float movementSpeed = 0.4;
	float wavesInFrame = 5.0;
	float waveHeight = 0.3;
	float point = (sin(TIME * movementSpeed + 
			   uv.x * wavesInFrame * 2.0 * PI) *
		       waveHeight);
	const float sharpness = 1.40;
	float dist = 1.0 - abs(clamp((point - uv.y) / thickness, -1.0, 1.0));
	float val;
	float brightness = 0.8;

	// All of the threads go the same way so this if is easy
	if (sharpness != 1.0)
		dist = pow(dist, sharpness);
	
	dist *= brightness;
		
	result = vec4(vec3(0.3, 0.6, 0.3) * dist, 1.0);
	
	return result;
}

void main( void ) {

	vec2 fc = gl_FragCoord.xy;
	vec2 uv 	= fc / RENDERSIZE - 0.5;
	vec4 pixel;
	
	pixel = pixelAt(uv);
	
	// I can't really do postprocessing in this shader, so instead of
	// doing the texturelookup, I restructured it to be able to compute
	// what the other pixel might be. The real code would lookup a texel
	// and wouldnt loop.
	const float e = 64.0, s = 1.0 / e;
	for (float i = 0.0; i < e; ++i) {
		pixel += pixelAt(uv + (uv * (i*s))) * (0.3-i*s*0.325);
	}
	pixel /= 1.0;
	
	gl_FragColor = pixel;
}
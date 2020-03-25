/*
{
  "CATEGORIES" : [
    "Automatically Converted",
    "GLSLSandbox"
  ],
  "INPUTS" : [

  ],
  "DESCRIPTION" : "Automatically converted from http:\/\/glslsandbox.com\/e#56606.2"
}
*/


#ifdef GL_ES
precision mediump float;
#endif


// Example procedural cloud function
// Put this on your particle effects (with fewer iterations if you have a lot of particles)

// Here are some links to more examples of procedural noise
// http://glslsandbox.com/e#5767.0 
// http://glslsandbox.com/e#10315.1
// http://glslsandbox.com/e#16640.9

//value noise
float value_noise(in vec2 uv) 
{
    const float k = 257.;
    vec4 l  = vec4(floor(uv),fract(uv));
    float u = l.x + l.y * k;
    vec4 v  = vec4(u, u+1.,u+k, u+k+1.);
    v       = fract(fract(v*1.23456789)*9.18273645*v);
    l.zw    = l.zw*l.zw*(3.-2.*l.zw);
    l.x     = mix(v.x, v.y, l.z);
    l.y     = mix(v.z, v.w, l.z);
    return    mix(l.x, l.y, l.w);
}

mat2 rmat(float t)
{
	float c = cos(t);
	float s = sin(t);
	return mat2(c, s, -s, c);	
}

//fractal brownian motion
float fbm(float a, float f, vec2 uv, const int it)
{
    float n = 0.;

    vec2 p = vec2(-.3 * TIME, .7 * TIME);
    mat2 rm = rmat(.31);
	
    for(int i = 0; i < 32; i++)
    {
        if(i<it)
        {
            n += value_noise(uv*f+p)*a;
            a *= .5;
            f *= 2.;
	    p *= rm;
        }
        else
        {
            break;
        }
    }
    return n;
}

vec2 center_and_correct_aspect_ratio(vec2 uv)
{
	uv = uv * 2. - 1.;
	uv.x *= RENDERSIZE.x/RENDERSIZE.y;
	return uv;
}

void main( void ) {

	vec2 uv 		= gl_FragCoord.xy / RENDERSIZE.xy;
	uv 			= center_and_correct_aspect_ratio(uv);
	
	float lacunarity 	= 3.; //roughness, kinda
	float amplitude  	= .5; //maximum brightness per step
	const int iterations	= 10;
	
	float noise 		= fbm(amplitude, lacunarity, uv, iterations); //this is a really simple perlin-esque noise function
	float falloff 		= length(uv);

	float cloud		= clamp(pow(noise,  1.5 + max(falloff, noise)) * (1. - falloff), 0., 1.);
	gl_FragColor 		= vec4(cloud);
}//sphinx
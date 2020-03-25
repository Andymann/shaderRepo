/*
{
  "CATEGORIES" : [
    "Automatically Converted",
    "GLSLSandbox"
  ],
  "INPUTS" : [

  ],
  "DESCRIPTION" : "Automatically converted from http:\/\/glslsandbox.com\/e#42307.3"
}
*/


#ifdef GL_ES
precision mediump float;
#endif

#extension GL_OES_standard_derivatives : enable


const float PI = 3.14159265;

float sphere(float t, float k)
{
    float d = 1.0+t*t-t*t*k*k;
    if (d <= 0.0)
        return -1.0;
    float x = (k - sqrt(d))/(1.0 + t*t);
    return asin(x*t);
}


void main( void ) 
{
    // bg texture
    vec4 texColor = vec4(0.0,0.0,0.0,0.5);
    
    vec2 uv = gl_FragCoord.xy - 0.5*RENDERSIZE.xy;
    float v = RENDERSIZE.x;
    if (v > RENDERSIZE.y)
        v = RENDERSIZE.y;
	uv /= v;
    uv *= 3.0;
    float len = length(uv);
    float k = 1.0;
    float len2;

    len2 = sphere(len*k,sqrt(2.0))/sphere(1.0*k,sqrt(2.0));
	uv = uv * len2 * 0.5 / len;
	uv = uv + 0.5;
	
    vec2 pos = uv;
    float t = TIME/1.0;
    float scale1 = 40.0;
    float scale2 = 20.0;
    float r, g, b;
    
    //val += sin((pos.x*scale1 + t*2.))*3.;
    r = cos(pos.x*90.+t)+sin(pos.y*30.);
    g = r+sin((pos.y*10. + cos(t*.4)));
    b = g+sin((pos.x+pos.y + cos(t*.3)));

    float glow =  0.020 / (0.01 + distance(len, 1.0));
    
    //val = (cos(PI*val) + 1.0) * 0.5;
    vec4 col2 = vec4(r, g, b, 1.0);
    
    gl_FragColor = step(len, 1.) * 0.5 * col2 + glow * col2 + 0.5 * texColor;
}
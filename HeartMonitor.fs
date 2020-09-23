/*
{
  "CATEGORIES" : [
    "Automatically Converted",
    "GLSLSandbox"
  ],
  "INPUTS" : [

  ],
  "DESCRIPTION" : "Automatically converted from http:\/\/glslsandbox.com\/e#43269.1"
}
*/


#ifdef GL_ES
precision mediump float;
#endif

//#extension GL_OES_standard_derivatives : enable


float sinc(float x)
{
    return (x == 0.0) ? 1.0 : sin(x) / x;
}

float triIsolate(float x)
{
    return abs(-1.0 + fract(clamp(x, -0.5, 0.5)) * 2.0);
}

float waveform(float x)
{
    float prebeat = -sinc((x - 0.37) * 40.0) * 0.6 * triIsolate((x - 0.4) * 1.0);
    float mainbeat = (sinc((x - 0.5) * 60.0)) * 1.2 * triIsolate((x - 0.5) * 0.7) * 1.5;
    float postbeat = sinc((x - 0.91) * 15.0) * 0.85;
    return (prebeat + mainbeat + postbeat) * triIsolate((x - 0.625) * 0.8);
}

void main()
{
    vec2 uv = ( gl_FragCoord.xy / RENDERSIZE.xy );
    
    float wave = waveform(uv.x * 1.1 + 0.2) * 1.5;
    
    //wave -= sin(fract(iTime * 1.2 - 0.25)) * 0.1;
    
    float light = 1.0 - fract((1.0 - uv.x) + fract(TIME * 1.2));
    light *= 4.0;
    
    
    float dist = pow(abs((uv.y * 4.0  - 1.5) - wave), 0.05);

    gl_FragColor = vec4(vec3(light, 0.0, 0.0) * (1.0 - dist),1.0);
    
    uv.x *= RENDERSIZE.x / RENDERSIZE.y;
    if (fract(uv.x * 10.0) < 0.03 || fract(uv.y * 10.0) < 0.03) 
    {
        gl_FragColor = max(gl_FragColor, vec4(0.1));
    }
}
/*
{
  "CATEGORIES" : [
    "Automatically Converted",
    "GLSLSandbox"
  ],
  "INPUTS" : [

  ],
  "DESCRIPTION" : "Automatically converted from http:\/\/glslsandbox.com\/e#61496.0"
}
*/


/*
 * Original shader from: https://www.shadertoy.com/view/XttXRs
 */

#ifdef GL_ES
precision mediump float;
#endif

// glslsandbox uniforms

// shadertoy emulation
#define iTime TIME
#define iResolution RENDERSIZE

// --------[ Original ShaderToy begins here ]---------- //
vec2 rotate(in vec2 toRotate, in float rad){
    return vec2(toRotate.x * cos(rad) - toRotate.y * sin(rad),
                toRotate.y * cos(rad) + toRotate.x * sin(rad));
}

float drawSin(vec2 uv){    
	return pow(1.1 - sqrt(distance(uv, vec2(uv.x,
                                           .3 * sin(uv.x*10. - iTime)*uv.x + .25)
                                 )), 
               6.);
}

void mainImage( out vec4 fragColor, in vec2 fragCoord )
{
	vec2 uv = fragCoord.xy / iResolution.x;
    vec2 strand1uv = uv;
    vec2 strand2uv = rotate(uv, .5) + vec2(0.12, 0.05);
    vec2 strand3uv = rotate(uv, -.5) * .8 + vec2(0.0, 0.01);
    vec2 strand4uv = rotate(uv, .3) * 0.5 + vec2(0.0, 0.1);
    vec2 strand5uv = rotate(uv, -.2) * 1.1 - vec2(0.0, 0.1);
    
    fragColor = vec4(0.,0.,0.,1.);
    vec3 blue = vec3(.5, .85, .99);
    
    float val = drawSin(strand1uv);
    fragColor += .65 * vec4(vec3(val), 1.);
    
    val = drawSin(strand2uv);
    fragColor += .65 * vec4(vec3(val), 1.);
    
    val = drawSin(strand3uv);
    fragColor += .65 * vec4(vec3(val), 1.);
    
    val = drawSin(strand4uv);
    fragColor += .65 * vec4(vec3(val), 1.);
    
    val = drawSin(strand5uv);
    fragColor += .65 * vec4(vec3(val), 1.);

    fragColor *= 1. - exp(uv.x) / exp(1.);
    fragColor .xyz *= blue;
    
}
// --------[ Original ShaderToy ends here ]---------- //

void main(void)
{
    mainImage(gl_FragColor, gl_FragCoord.xy);
}
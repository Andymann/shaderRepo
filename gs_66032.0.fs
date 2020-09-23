/*
{
  "CATEGORIES" : [
    "Automatically Converted",
    "GLSLSandbox"
  ],
  "INPUTS" : [

  ],
  "DESCRIPTION" : "Automatically converted from http:\/\/glslsandbox.com\/e#66032.0"
}
*/


/*
 * Original shader from: https://www.shadertoy.com/view/Mlfczf
 */

#ifdef GL_ES
precision mediump float;
#endif

// glslsandbox uniforms

// shadertoy emulation
#define iTime TIME
#define iResolution RENDERSIZE

// Emulate some GLSL ES 3.x
int int_mod(int a, int b)
{
    return (a - (b * (a/b)));
}



// --------[ Original ShaderToy begins here ]---------- //
#define rot2(spin) mat2(sin(spin),cos(spin),-cos(spin),sin(spin))
#define pi acos(-1.0)

// instead of iTime i have pi/6.0 for a static shape
  mat2 rot = rot2(pi/6.0);


void mainImage( out vec4 fragColor, in vec2 fragCoord )
{
	vec2 uv = fragCoord.xy / iResolution.y*2.0-iResolution.xy/iResolution.y;
    
    float l = 0.0;
    
        
    // using pow for seamless "infinite" zoom
    // it loops after 3 seconds, pow makes the zoom speed
    // seamless between loops
    float scale = 1.0/pow(4.0/3.0,fract(iTime/3.0)*6.0+3.0);
    // there is a full rotation every 6 seconds.
    uv *= scale*rot2(pi*iTime/6.0);
    for(int i = 0; i < 30; i++) {
        scale *= 4.0/3.0;
        uv *= 4.0/3.0 * rot;

        if(uv.y > 1.0) {
            // an attempt at antialiasing
            l = min((uv.y-1.0)/scale*iResolution.y/sqrt(2.0),1.0);

            if (int_mod(i,2) == 1) {
                l = 1.0-l;
            }
            
            break;
        }
    }
    
	fragColor = vec4(l);
}
// --------[ Original ShaderToy ends here ]---------- //

void main(void)
{
    mainImage(gl_FragColor, gl_FragCoord.xy);
}
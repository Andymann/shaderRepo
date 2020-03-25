/*
{
  "CATEGORIES" : [
    "Automatically Converted",
    "GLSLSandbox"
  ],
  "INPUTS" : [

  ],
  "DESCRIPTION" : "Automatically converted from http:\/\/glslsandbox.com\/e#56891.0"
}
*/


/*
 * Original shader from: https://www.shadertoy.com/view/Mdc3zH
 */

#ifdef GL_ES
precision mediump float;
#endif

// glslsandbox uniforms

// shadertoy globals
float iTime;
vec3  iResolution;

// Protect glslsandbox uniform names
#define TIME        stemu_time
#define RENDERSIZE  stemu_resolution

// --------[ Original ShaderToy begins here ]---------- //
// a variant of https://www.shadertoy.com/view/Xs33RH


// reduction: 307
                                                 // draw segment [a,b]
#define D(m)  4e-3/length( m.x*v - u+a )
#define L   ; m.x= dot(u-a,v=b-a)/dot(v,v); o.z += D(m); o += D(clamp(m*1.,0.,2.));
#define P     b=c= vec2(r.x,1)/(4.+r.y) L   b=a*I L   a=c*I L   a=c; r= 1.*I*r.yx;

void mainImage(out vec4 o, vec2 U)
{   vec2 v,m, I=vec2(2,-1), a,b,c=iResolution.xy, 
        u = (U+U-c)/c.y,
        r = sin(1.0*iTime+6.*I); r += I*r.yx;
    P  o-=o;       // just to initialize a
	P P P P         // 14*60 segments

}


// --------[ Original ShaderToy ends here ]---------- //

#undef TIME
#undef RENDERSIZE

void main(void)
{
  iTime = TIME;
  iResolution = vec3(RENDERSIZE, 0.0);

  mainImage(gl_FragColor, gl_FragCoord.xy);
  gl_FragColor.r *= 01.;
  gl_FragColor.g *= 01.;
  gl_FragColor.b = gl_FragColor.r ;
  //gl_FragColor.a = 1.0;
}
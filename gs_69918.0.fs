/*
{
  "CATEGORIES" : [
    "Automatically Converted",
    "GLSLSandbox"
  ],
  "INPUTS" : [

  ],
  "DESCRIPTION" : "Automatically converted from http:\/\/glslsandbox.com\/e#69918.0"
}
*/


/*
 * Original shader from: https://www.shadertoy.com/view/3sGfD3
 */

#ifdef GL_ES
precision mediump float;
#endif

// glslsandbox uniforms

// shadertoy emulation
#define iTime TIME
#define iResolution RENDERSIZE

// --------[ Original ShaderToy begins here ]---------- //
#define rot(a) mat2(cos(a),sin(a),-sin(a),cos(a))

vec3 render(vec2 p) {
    p*=rot(iTime*.1)*(.0002+.7*pow(smoothstep(0.,.5,abs(.5-fract(iTime*.01))),3.));
    p.y-=.2266;
    p.x+=.2082;
    vec2 ot=vec2(100.);
    float m=100.;
    for (int i=0; i<150; i++) {
        vec2 cp=vec2(p.x,-p.y);
		p=p+cp/dot(p,p)-vec2(0.,.25);
        p*=.1;
        p*=rot(1.5);
        ot=min(ot,abs(p)+.15*fract(max(abs(p.x),abs(p.y))*.25+iTime*.05+float(i)*.15));
        m=min(m,abs(length(p.y)));
    }
    ot=exp(-200.*ot)*2.;
    m=exp(-200.*m);
    return vec3(ot.x,ot.y*.5+ot.x*.3,ot.y)+m*.2;
}

void mainImage( out vec4 fragColor, in vec2 fragCoord )
{
    vec2 uv = (fragCoord-iResolution.xy*.5)/iResolution.y;
    vec2 d=vec2(0.,.5)/iResolution.xy;
    vec3 col = render(uv)+render(uv+d.xy)+render(uv-d.xy)+render(uv+d.yx)+render(uv-d.yx);
    fragColor = vec4(col*.2,1.0);
}
// --------[ Original ShaderToy ends here ]---------- //

void main(void)
{
    mainImage(gl_FragColor, gl_FragCoord.xy);
}
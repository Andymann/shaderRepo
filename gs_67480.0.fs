/*
{
  "CATEGORIES" : [
    "Automatically Converted",
    "GLSLSandbox"
  ],
  "INPUTS" : [

  ],
  "DESCRIPTION" : "Automatically converted from http:\/\/glslsandbox.com\/e#67480.0"
}
*/


// N080920N

/*
 * Original shader from: https://www.shadertoy.com/view/WtjfRG
 */

#ifdef GL_ES
precision mediump float;
#endif

// glslsandbox uniforms

// shadertoy emulation
#define iTime TIME
#define iResolution RENDERSIZE

// --------[ Original ShaderToy begins here ]---------- //
// Author: bitless
// Title: Microwaves

// Thanks to Patricio Gonzalez Vivo & Jen Lowe for "The Book of Shaders"
// and Fabrice Neyret (FabriceNeyret2) for https://shadertoyunofficial.wordpress.com/
// and Inigo Quilez (iq) for  http://www.iquilezles.org/www/index.htm
// and whole Shadertoy community for inspiration.

#define p(t, a, b, c, d) ( a + b*cos( 6.28318*(c*t+d) ) ) //palette function (https://www.iquilezles.org/www/articles/palettes/palettes.htm)
#define S(x,y,z) smoothstep(x,y,z)

float w(float x, float p){ //sin wave function
    /*x *= 5.;
    float t= p*.5+sin(iTime*.25)*10.5;
    return (sin(x*.25 + t)*5. + sin(x*4.5 + t*3.)*.2 + sin(x + t*3.)*2.3  + sin(x*.8 + t*1.1)*2.5)*0.275;*/
	
	return sin(x)*cos(p*iTime*0.01);
}



void mainImage( out vec4 fragColor, in vec2 g)
{
    vec2 r = iResolution.xy
        ,st = (g+g-r)/r.y;

    float 	th = .05 //thickness
    		,sm = 15./r.y+.85*length(S(vec2(01.,.2),vec2(2.,.7),abs(st))) //smoothing factor
    		,c = 0. 
            ,t = iTime*0.25
            ,n = floor((st.y+t)/.1)
            ,y = fract((st.y+t)/.1);
    
    vec3 clr = vec3(0.);
    for (float i = -5.;i<5.;i++)
    {
        float f = w(st.x,(n-i))-y-i;
        c = mix(c,0.,S(-0.3,abs(st.y),f));
        c += S(th+sm,th-sm,abs(f))
            *(1.-abs(st.y)*.75)
            + S(5.5-abs(f*0.5),0.,f)*0.25;
            
        clr = mix(clr,p(sin((n-i)*.15),vec3(.5),vec3(.5), vec3(.270), vec3(.0,.05,0.15))*c,S(-0.3,abs(st.y),f)*1.);
    }
    fragColor = vec4(clr,1.);
}
// --------[ Original ShaderToy ends here ]---------- //

void main(void)
{
    mainImage(gl_FragColor, gl_FragCoord.xy);
}
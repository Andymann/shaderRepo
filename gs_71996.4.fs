/*
{
    "CATEGORIES": [
        "Automatically Converted",
        "GLSLSandbox"
    ],
    "DESCRIPTION": "Automatically converted from http://glslsandbox.com/e#71996.4",
    "INPUTS": [
    ]
}

*/


#ifdef GL_ES
precision mediump float;
#endif

// glslsandbox 

// shadertoy emulation
#define iTime TIME
#define iResolution RENDERSIZE

vec2 TriGridTest(vec2 uv)
{
    vec2 wPos = uv;
    wPos.y *= 0.5/.8660254;
    wPos *= 2.0;
    vec2 posLocal0 = floor(wPos);
    vec2 localPos = wPos - posLocal0;
    float col = float(distance(vec2(1, 0), localPos) < distance(vec2(0, 1), localPos)) * float(mod(posLocal0.x + posLocal0.y, 2.0)==0.0);
    col += float(distance(vec2(0, 0), localPos) > distance(vec2(1, 1), localPos)) * float(mod(posLocal0.x + posLocal0.y, 2.0)==1.0);
    vec2 id = vec2(ceil(posLocal0.x) + ceil(col), posLocal0.y)*0.5;
    id.y *= 2.0;
    id.y += 0.5;
    //id *= mat2(1.1547,0.0,-0.5773503,1.0);
    return id;
}

void mainImage( out vec4 fragColor, in vec2 fragCoord )
{
    vec2 uv = (fragCoord.xy - 0.5 * iResolution.xy) / iResolution.y;  
    vec2 id = TriGridTest(uv*128.0);
	
	id.x += sin(iTime+id.y*0.1)*4.0;
	id.y += sin(length(id)*0.2+TIME*1.)*4.0;
	
    float cm = 1.0 + 0.5+sin(length(id*.2) + iTime*0.65)*2.5;	// pulse mult
    fragColor = vec4(vec3(0.25,0.32,0.25)*cm,1.0);
}

// --------[ Original ShaderToy ends here ]---------- //

void main(void)
{
    mainImage(gl_FragColor, gl_FragCoord.xy);
}
/*
{
  "CATEGORIES" : [
    "Automatically Converted",
    "GLSLSandbox"
  ],
  "INPUTS" : [

  ],
  "DESCRIPTION" : "Automatically converted from float rand(vec2 uv)\n{\n    return fract(sin(dot(uv, vec2(12.9898, 78.233))) * 43758.5453);\n}\n\nvec2 uv2tri(vec2 uv)\n{\n    float sx = uv.x - uv.y \/ 2.0; \/\/ skewed x\n    float offs = step(fract(1.0 - uv.y), fract(sx));\n    return vec2(floor(sx) * 2.0 + offs, floor(uv.y));\n}\n\nvoid mainImage( out vec4 fragColor, in vec2 fragCoord )\n{\n    vec2 uv = (fragCoord.xy - resolution.xy \/ 2.0) \/ resolution.y * 8.0;\n\n    vec3 p = vec3(dot(uv, vec2(1.0, 0.5)), dot(uv, vec2(-1.0, 0.5)), uv.y);\n    vec3 p1 = fract(+p);\n    vec3 p2 = fract(-p);\n\n    float d1 = min(min(p1.x, p1.y), p1.z);\n    float d2 = min(min(p2.x, p2.y), p2.z);\n    float d = min(d1, d2);\n\n    vec2 tri = uv2tri(uv);\n    float r = rand(tri) * 2.0 + tri.x \/ 16.0 + time * 2.0;\n    \n    \n    \/\/float c = step(0.2 + sin(r) * 0.2, d);\n    \/\/por iq correccion\n\tfloat c = smoothstep(-0.02,0.0,d-0.2*(1.0+sin(r)));\n    \n    fragColor = vec4(c, c, c, 1.0);\n}\n\n\n\/\/ --------[ Original ShaderToy ends here ]---------- \/\/\n\nvoid main(void)\n{\n    mainImage(gl_FragColor, gl_FragCoord.xy);\nhttp:\/\/glslsandbox.com\/e#66330.0"
}
*/


/*
 * Original shader from: https://www.shadertoy.com/view/wl2yDV
 */

#ifdef GL_ES
precision mediump float;
#endif

// glslsandbox uniforms

// shadertoy emulation
//#define iTime TIME
//#define iResolution RENDERSIZE

// --------[ Original ShaderToy begins here ]---------- //
float rand(vec2 uv)
{
    return fract(sin(dot(uv, vec2(12.9898, 78.233))) * 43758.5453);
}

vec2 uv2tri(vec2 uv)
{
    float sx = uv.x - uv.y / 2.0; // skewed x
    float offs = step(fract(1.0 - uv.y), fract(sx));
    return vec2(floor(sx) * 2.0 + offs, floor(uv.y));
}

void mainImage( out vec4 fragColor, in vec2 fragCoord )
{
    vec2 uv = (fragCoord.xy - RENDERSIZE.xy / 2.0) / RENDERSIZE.y * 8.0;

    vec3 p = vec3(dot(uv, vec2(1.0, 0.5)), dot(uv, vec2(-1.0, 0.5)), uv.y);
    vec3 p1 = fract(+p);
    vec3 p2 = fract(-p);

    float d1 = min(min(p1.x, p1.y), p1.z);
    float d2 = min(min(p2.x, p2.y), p2.z);
    float d = min(d1, d2);

    vec2 tri = uv2tri(uv);
    float r = rand(tri) * 2.0 + tri.x / 16.0 + TIME * 2.0;
    
    
    //float c = step(0.2 + sin(r) * 0.2, d);
    //por iq correccion
	float c = smoothstep(-0.02,0.0,d-0.2*(1.0+sin(r)));
    
    fragColor = vec4(c, c, c, 1.0);
}


// --------[ Original ShaderToy ends here ]---------- //

void main(void)
{
    mainImage(gl_FragColor, gl_FragCoord.xy);
}
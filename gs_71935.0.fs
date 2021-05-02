/*
{
    "CATEGORIES": [
        "Automatically Converted",
        "GLSLSandbox"
    ],
    "DESCRIPTION": "Automatically converted from http://glslsandbox.com/e#71935.0",
    "INPUTS": [
    ]
}

*/


//By Morphix

//precision mediump float;


void main(){
    vec2 uv = (gl_FragCoord.xy - .5 * RENDERSIZE.xy) / RENDERSIZE.y;
    
    for(float i=1.; i<11.; i++){
        uv.x += .5/i*sin(1.9 * i * uv.y + TIME / 2. - cos(TIME / 66. + uv.x))+21.;
        uv.y += .4/i*cos(1.6 * i * uv.x + TIME / 3. + sin(TIME / 55. + uv.y))+31.;
    }

    gl_FragColor = vec4(sin(3. * uv.x - uv.y), sin(3. * uv.y) * 0., sin(3. * uv.x) * 0.0, 1);
}
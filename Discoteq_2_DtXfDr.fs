/*
{
    "CATEGORIES": [
        "Automatically Converted",
        "Shadertoy"
    ],
    "DESCRIPTION": "Automatically converted from https://www.shadertoy.com/view/DtXfDr by supah.  Discoteq 2",
    "IMPORTED": {
    },
    "INPUTS": [
    ]
}

*/


#define S smoothstep

vec4 Line(vec2 uv, float speed, float height, vec3 col) {
    uv.y += S(1., 0., abs(uv.x)) * sin(TIME * speed + uv.x * height) * .2;
    return vec4(S(.06 * S(.2, .9, abs(uv.x)), 0., abs(uv.y) - .004) * col, 1.0) * S(1., .3, abs(uv.x));
}

void main() {

    vec2 uv = (gl_FragCoord.xy - .5 * RENDERSIZE.xy) / RENDERSIZE.y;
    gl_FragColor = vec4 (0.);
    for (float i = 0.; i <= 5.; i += 1.) {
        float t = i / 5.;
        gl_FragColor += Line(uv, 1. + t, 4. + t, vec3(.2 + t * .7, .2 + t * .4, 0.3));
    }
}

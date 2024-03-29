/*
{
    "CATEGORIES": [
        "Automatically Converted",
        "Shadertoy"
    ],
    "DESCRIPTION": "Automatically converted from https://www.shadertoy.com/view/ftjyDK by oneshade.  .",
    "IMPORTED": {
    },
    "INPUTS": [
    ]
}

*/


float Hash31(in vec3 p) {
     return fract(937.276 * cos(836.826 * p.x + 263.736 * p.y + 374.723 * p.z + 637.839));
}

void main() {

    vec2 uv = (gl_FragCoord.xy - 0.5 * RENDERSIZE.xy) / RENDERSIZE.y * 4.0;
    float time = TIME * 2.0;
    vec3 color = vec3(0.0);
    for (float i=-3.0; i <= 3.0; i += 1.0) {
        for (float j=-2.0; j <= 2.0; j += 1.0) {
            vec2 p = uv;
            float freq = 2.0 * fract(643.376 * cos(264.863 * i + 136.937));
            vec2 pos = 5.0 * vec2(i, j) + vec2(sin(freq * (TIME + 10.0 * j) - i), freq * TIME);
            pos.y = mod(pos.y + 15.0, 30.0) - 15.0;
            pos.x *= 0.1 * pos.y + 1.0;
            p -= 0.2 * pos;
            float sec = floor(time);
            float frac = fract(time);
            float flicker = mix(Hash31(vec3(i, j, sec)), Hash31(vec3(i, j, sec + 1.0)), frac);
            float rad = 25.0 + 20.0 * flicker;
            float br = 200.0 * pow(1.0 / max(10.0, rad * (sqrt(abs(p.x)) + sqrt(abs(p.y))) + 0.9), 2.5);
            float rand = fract(847.384 * cos(483.846 * i + 737.487 * j + 264.836));
            if (rand > 0.5) color += mix(vec3(br, 0.4 * br, 0.0), vec3(1.0), br);
            else color += mix(vec3(0.0, 0.0, 0.6 * br), vec3(1.0), br);
            color *= 0.955 + 0.1 * flicker;
        }
    }
    gl_FragColor = vec4(color, 1.0);
}

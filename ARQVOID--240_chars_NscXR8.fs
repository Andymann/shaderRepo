/*
{
    "CATEGORIES": [
        "Automatically Converted",
        "Shadertoy"
    ],
    "DESCRIPTION": "Automatically converted from https://www.shadertoy.com/view/NscXR8 by ENDESGA.  ///////",
    "IMPORTED": {
    },
    "INPUTS": [
    ]
}

*/


// 240 chars by Xor (with aspect ratio fix)
void main() {



    vec3 p=RENDERSIZE,d = -.5*vec3(gl_FragCoord.xy+I-p.xy,p)/p.x,c = d-d, i=c;
    for(;i.x<1.;c += length(sin(p.yx)+cos(p.xz+TIME))*d)
        p = c,
        p.z -= TIME+(i.x+=.01),
        p.xy *= mat2(sin((p.z*=.1)+vec4(0,11,33,0)));
    gl_FragColor = vec4(10,0,2.5,9)/length(c);
}

// ORIGINAL: 258 chars
    vec3 d = .5-vec3(I,1)/RENDERSIZE, p, c;
    for(int i = 0; i <= 99; i++) {
        p = c;
        p.z -= TIME+i*.01;
        p.z *= .1;
        p.xy *= mat2(sin(p.z),-cos(p.z),cos(p.z),sin(p.z));
        c += length(sin(p.yx)+cos(p.xz+TIME))*d;
    }
    O.rgb = vec3(5./length(c))*vec3(2.,.0,.5);
}*/

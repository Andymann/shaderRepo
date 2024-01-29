/*
{
    "CATEGORIES": [
        "Automatically Converted",
        "Shadertoy"
    ],
    "DESCRIPTION": "Automatically converted from https://www.shadertoy.com/view/cdKBDy by Xor.  I wanted to try the [url=https://www.shadertoy.com/view/dsyBDy]double mod pattern[/url] in 3D.",
    "IMPORTED": {
    },
    "INPUTS": [
    ]
}

*/


/*
    "Bricks" by @XorDev
    
    I wanted to try the double mod pattern (shadertoy.com/view/dsyBDy) in 3D.

    X: X.com/XorDev/status/1718731726434824357
    twigl: twigl.app/?ol=true&ss=-NhxByN14idHvVurpv8Q
    
    <300 chars playlist: shadertoy.com/playlist/fXlGDN
*/

void main() {



    //Voxel index for coloring
    int i;
    //Resolution, rounded/centered screen coords, xy pos, z (vec2 for convenience), closest plane
    for(vec2 c=RENDERSIZE.xy, u=((gl_FragCoord.xy)-c*.5)/c.y, p, z=p+2e1;
            //Loop to a max distance of 100 or when you hit something (xor + double mod technique)
            z.x<1e2 && (i=int(p)^int(p.y)^int(z))%93%43<32; //Try playing with these numbers!
            //Compute next xy pos with time offset
            p=u*z+TIME*vec2(2,9))
        //Compute the distance to the nearest x and y planes
        c=fract(-p*sign(u))/abs(u),
        //March to next nearest x, y and z plane
        z+=min(min(c,c.yx), fract(-z))+2e-5,
        //Colorize with depth and voxel index
        gl_FragColor.rgb=(1e2-z.x)*(2.-cos(vec3(i/=3,i+5,i+4)));
    
    //Dampen and shade
    gl_FragColor/=2e2+fwidth(gl_FragColor.g)*5e2;
}

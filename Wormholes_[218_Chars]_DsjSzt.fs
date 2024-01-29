/*
{
    "CATEGORIES": [
        "Automatically Converted",
        "Shadertoy"
    ],
    "DESCRIPTION": "Automatically converted from https://www.shadertoy.com/view/DsjSzt by Xor.  Wormholes because why not?",
    "IMPORTED": {
    },
    "INPUTS": [
    ]
}

*/


/*
    "Wormholes" by @XorDev
    
    
    
    Tweet: twitter.com/XorDev/status/1601770313230209024
    Twigl: t.co/k5mZbAg1ox
*/
void main() {



    //Clear frag color
    gl_FragColor *= 0.;
    //Resolution for scaling
    vec2 r = RENDERSIZE.xy;
    //Initialize the iterator and ring distance
    for(float i=0.,d;
    //Loop 50 times
    i++<5e1;
    //Add ring color, with ring attenuation
    gl_FragColor += (cos(i*i+vec4(6,7,8,0))+1.)/(abs(length(gl_FragCoord.xy-r*.5+cos(r*i)*r.y/d+d/.4)/r.y*d-.2)+8./r.y)*min(d,1.)/++d/2e1 )
        //Compute distance to ring
        d = mod(i-TIME,5e1)+.01;
}

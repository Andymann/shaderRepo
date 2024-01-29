/*
{
    "CATEGORIES": [
        "Automatically Converted",
        "Shadertoy"
    ],
    "DESCRIPTION": "Automatically converted from https://www.shadertoy.com/view/cltfRf by Xor.  Inspired by @cmzw's [url=https://twitter.com/cmzw_/status/1729148918225916406]work[/url]",
    "IMPORTED": {
    },
    "INPUTS": [
    ]
}

*/


/*
    "Shield" by @XorDev
    
    Inspired by @cmzw's work: witter.com/cmzw_/status/1729148918225916406
    
    X: X.com/XorDev/status/1730436700000649470
    Twigl: twigl.app/?ol=true&ss=-NkYXGfK5wEl4VaUQ9zS
*/
void main() {



    //Iterator, z and time
    float i,z,t=TIME;
    //Clear frag and loop 100 times
    for(gl_FragColor*=i; i<1.; i+=.01)
    {
        //Resolution for scaling
        vec2 v=RENDERSIZE.xy,
        //Center and scale outward
        p=(gl_FragCoord.xy+I-v)/v.y*i;
        //Sphere distortion and compute z
        p/=.2+sqrt(z=max(1.-dot(p,p),0.))*.3;
        //Offset for hex pattern
        p.y+=fract(ceil(p.x=p.x/.9+t)*.5)+t*.2;
        //Mirror quadrants
        v=abs(fract(p)-.5);
        //Add color and fade outward
        gl_FragColor+=vec4(2,3,5,1)/2e3*z/
        //Compute hex distance
        (abs(max(v.x*1.5+v,v+v).y-1.)+.1-i*.09);
    }
    //Tanh tonemap
    gl_FragColor=tanh(gl_FragColor*O);
}

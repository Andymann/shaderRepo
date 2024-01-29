/*
{
    "CATEGORIES": [
        "Automatically Converted",
        "Shadertoy"
    ],
    "DESCRIPTION": "Automatically converted from https://www.shadertoy.com/view/mlKyRK by Xor.  I want to see a maze in polar coordinates, so I made this",
    "IMPORTED": {
    },
    "INPUTS": [
    ]
}

*/


/*
    "Passage" by @XorDev
    
    X: X.com/XorDev/status/1726396476866044130
    Twigl: twigl.app?ol=true&ss=-Nje8mOER98sqMpHsal-
    
    <512 chars playlist: https://www.shadertoy.com/playlist/N3SyzR
    -10 Thanks to FabriceNeyret2
    -8 Thanks to lluic
*/

void main() {



    //Clear fragcolor
    gl_FragColor *= 0.;
    
    //Raymarch loop:
    //iterator, step-size, raymarch distance, Tau
    //Raymarchs 100 times adding brightness when close to a surface
    for(float i,s,d,T=6.283; i++<1e2; gl_FragColor+=1e-5/(.001-s))
    {
        //Rotation matrix
        mat2 R = mat2(8,6,-6,8)*.1;
        //Resolution for scaling
        vec3 r = RENDERSIZE,
        //Project sample with roll rotation and distance
        p = vec3((gl_FragCoord.xy+I-r.xy)/r.x*d*R, d-9.)*.7;
        //Rotate pitch
        p.yz *= R;
        //Step forward (negative for code golfing reasons)
        d -= s = min(p.z, cos(dot(
            //Compute subcell coordinates
            modf(fract((
            //Using polar-log coordinates
            vec2(atan(p.y,p.x),log(s=length(p.xy)))/T-TIME/2e1)*
            //Rotate 45 degrees and scale repetition
            mat2(p/p,-1))*15., p.xy),
        //Randomly flip cells and correct for scaling
        sign(cos(p.xy+p.y)))*T)*s/4e1);
    }
}

//[339]
/*
{
    //Clear fragcolor
    O *= 0.;
    
    //Raymarch loop:
    //iterator, step-size, raymarch distance, Tau
    //Raymarchs 100 times adding brightness when close to a surface
    for(float i,s,d,z,T=acos(-1.)*2.; i++<1e2; O+=1e-5/(.001-s))
    {
        //Rotation matrix
        mat2 R = mat2(8,6,-6,8)*.1;
        //Resolution for scaling
        vec3 r = RENDERSIZE,
        //Project sample with roll rotation and distance
        p = vec3((I+I-r.xy)/r.x*d*R, d-9.)*.7;
        //Rotate pitch
        p.yz *= R;
        z = p.z;
        //Step forward (negative for code golfing reasons)
        d -= s = min(z, cos(dot(
            //Compute subcell coordinates
            modf(fract((
            //Using polar-log coordinates
            vec3(atan(p.y,p.x),log(s=length(p.xy)),0)/T-TIME/2e1)*
            //Rotate 45 degrees and scale repetition
            mat3(1,1,0,1,-1,0,p-p))*15., p),
        //Randomly flip cells and correct for scaling
        sign(cos(p+p.y)))*T)*s/4e1);
    }
}
*/

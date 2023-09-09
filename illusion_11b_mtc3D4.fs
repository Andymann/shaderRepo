/*{
    "CATEGORIES": [
        "Automatically Converted",
        "Shadertoy"
    ],
    "DESCRIPTION": "Automatically converted from https://www.shadertoy.com/view/mtc3D4 by FabriceNeyret2.  motion without movment.  tangential strips.\nvariant of https://shadertoy.com/view/ct33WN\nreplacing #16:sign with +1 to see inflate/deplate only show effect in fullscreen.",
    "IMPORTED": {
    },
    "INPUTS": [
        {
            "DEFAULT": 0.25,
            "LABEL": "Speed",
            "MAX": 1,
            "MIN": 0,
            "NAME": "Speed",
            "TYPE": "float"
        },
        {
            "DEFAULT": 1,
            "LABEL": "Red",
            "MAX": 1,
            "MIN": 0,
            "NAME": "Red",
            "TYPE": "float"
        },
        {
            "DEFAULT": 1,
            "LABEL": "Green",
            "MAX": 1,
            "MIN": 0,
            "NAME": "Green",
            "TYPE": "float"
        },
        {
            "DEFAULT": 1,
            "LABEL": "Blue",
            "MAX": 1,
            "MIN": 0,
            "NAME": "Blue",
            "TYPE": "float"
        }
    ],
    "ISFVSN": "2"
}
*/


// variant of https://shadertoy.com/view/ct33WN

void main() {



 // float h = 450.; // R.y;
    vec2 R = RENDERSIZE.xy,
         U = 5.*( 2.*gl_FragCoord.xy - R ) / R.y,                                    // global normalize coords
         I = floor(U)+.5,                                              // disc Id
         F = fract(U)-.5,                                              // local disc coords
         H = fract(1e4*sin(I*mat2(R-17.,R+71.)));                      // 2 random values 
         
    float p = 15./R.y, l = length(F), L = length(U);
    gl_FragColor = vec4( mix( .0,                                                 // bg color
                   .5+.5* sin( 3.*L                                    // wavelength
                               - 20.*TIME*Speed* sign(H.x-.5)  - 6.28*H.y   // random dir and phase
                               - ( l > .4 - 2.*p* abs(dot(U,F))/L/l ?  sign(L-length(I)): 0.) ), // margin phase
                   smoothstep(0.,-p, l-.4) ) );                        // draw disk
	gl_FragColor=vec4(gl_FragColor.r*Red, gl_FragColor.g*Green, gl_FragColor.b *Blue, gl_FragColor.a);
	
}

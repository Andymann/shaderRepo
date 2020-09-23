/*
{
    "CATEGORIES": [
        "Automatically Converted",
        "GLSLSandbox"
    ],
    "DESCRIPTION": "Automatically converted from http://glslsandbox.com/e#66295.1",
    "INPUTS": [
        {
            "MAX": [
                1,
                1
            ],
            "MIN": [
                0,
                0
            ],
            "NAME": "mouse",
            "TYPE": "point2D"
        }
    ]
}

*/


#ifdef GL_ES
precision mediump float;
#endif

//#extension GL_OES_standard_derivatives : enable


#define TWO_PI 6.28318530718


vec3 hsv2rgb(  vec3 c )
{
 vec3 rgb = clamp( abs(mod(c.x*6.0+vec3(0.0,4.0,2.0),6.0)-3.0)-1.0, 0.0, 1.0 );
rgb = rgb*rgb*(3.0-2.0*rgb);
 return c.z * mix( vec3(1.0), rgb, c.y);
}

void main( void ) {

	vec2 position = ( gl_FragCoord.xy / RENDERSIZE.xy );

	vec3 color = vec3(0., 0., 0.);
	
	float sizeX = 0.05;
	float sizeY = 0.05;
	float merkezX = mouse.x;
	float merkezY = mouse.y;
	//float alpha = 1.;

	if(position.x > merkezX -sizeX &&
	   position.x < merkezX +sizeX &&
	   position.y < merkezY + sizeY &&
	   position.y > merkezY - sizeY) {
		
		color = vec3(1,0,0);
		
	}
	
	gl_FragColor = vec4(color,1.0);
}
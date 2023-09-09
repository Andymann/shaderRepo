/*{
    "CATEGORIES": [
        "XXX"
    ],
    "CREDIT": "by VIDVOX",
    "INPUTS": [
        {
            "DEFAULT": 0.25,
            "MAX": 1,
            "MIN": 0,
            "NAME": "width",
            "TYPE": "float"
        },
        {
            "DEFAULT": [
                0,
                0
            ],
            "NAME": "offset",
            "TYPE": "point2D"
        },
        {
            "DEFAULT": 0,
            "MAX": 1,
            "MIN": 0,
            "NAME": "alpha1",
            "TYPE": "float"
        },
        {
            "DEFAULT": 1,
            "MAX": 1,
            "MIN": 0,
            "NAME": "alpha2",
            "TYPE": "float"
        },
        {
            "DEFAULT": 0.23,
            "MAX": 1,
            "MIN": -0.75,
            "NAME": "seed1",
            "TYPE": "float"
        },
        {
            "DEFAULT": 0.5,
            "MAX": 1,
            "MIN": 0,
            "NAME": "randomThreshold",
            "TYPE": "float"
        }
    ],
    "ISFVSN": "2"
}
*/

//	glsl doesn't include random functions
//	this is a pseudo-random function


float rand(vec2 co){
	return fract(sin(dot(co.xy,vec2(12.9898,78.233)))*43758.5453);
}

void main(){
	//	determine if we are on an even or odd line
	//	math goes like..
	//	mod(((coord+offset) / width),2)
	
	vec4 out_color=vec4(1.,1.,1.,1.);
	float alphaAdjust=alpha2;
	vec2 coord=isf_FragNormCoord*RENDERSIZE;
	vec2 shift=offset;
	float size=width*RENDERSIZE.x;
	vec2 gridIndex=vec2(0.);
	
	if(size==0.){
		alphaAdjust=alpha1;
	}
	else{
		gridIndex=floor((offset+coord)/size);
		float value=rand(seed1*gridIndex);
		if(value<randomThreshold){
			alphaAdjust=alpha1;
			out_color.r=alphaAdjust;
			out_color.g=alphaAdjust;
			out_color.b=alphaAdjust;
		}
		
	}
	
	//out_color.a*=alphaAdjust;
	
	gl_FragColor=out_color;
}
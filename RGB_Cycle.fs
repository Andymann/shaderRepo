/*{
    "CATEGORIES": [
        "XXX"
    ],
    "CREDIT": "",
    "DESCRIPTION": "",
    "INPUTS": [
        {
            "NAME": "inputImage",
            "TYPE": "image"
        },
        {
            "DEFAULT": 1,
            "LABEL": "Speed",
            "MAX": 20,
            "MIN": 0.1,
            "NAME": "fSpeed",
            "TYPE": "float"
        }
    ],
    "ISFVSN": "2"
}
*/

void main()	{


	vec4		inputPixelColor;
	//	both of these are the same
	//inputPixelColor = IMG_THIS_PIXEL(inputImage);
	//inputPixelColor = IMG_PIXEL(inputImage, gl_FragCoord.xy);
	
	//	both of these are also the same
	//inputPixelColor = IMG_NORM_PIXEL(inputImage, isf_FragNormCoord.xy);
	inputPixelColor = IMG_THIS_NORM_PIXEL(inputImage);

	float mTime = mod(TIME, 3.*1./fSpeed);

	if((mTime>=0.) && (mTime<1.*1./fSpeed))
		gl_FragColor = vec4(inputPixelColor.r, inputPixelColor.g, inputPixelColor.b, 1.);
	else if((mTime>=1.*1./fSpeed) && (mTime<2.*1./fSpeed))
		gl_FragColor = vec4(inputPixelColor.g, inputPixelColor.b, inputPixelColor.r, 1.);
	else
		gl_FragColor = vec4(inputPixelColor.b, inputPixelColor.r, inputPixelColor.g, 1.);
}

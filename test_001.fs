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
            "NAME": "fMode",
            "TYPE": "float"
        },
        {
            "NAME": "tmpInputName",
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
	float normalizedLinear = sin(TIME * 6.28);
	float mTime = mod(TIME, 100.)/100.;
	
	/*
	float fModet=sin(TIME*1.);
	if(abs(fModet)<0.3)
		gl_FragColor = vec4(inputPixelColor.r, inputPixelColor.g, inputPixelColor.b, 1.);
	else if((abs(fModet)>=0.3)&&(abs(fModet)<0.6))
		gl_FragColor = vec4(inputPixelColor.g, inputPixelColor.b, inputPixelColor.r, 1.);
	else
		gl_FragColor = vec4(inputPixelColor.b, inputPixelColor.r, inputPixelColor.g, 1.);
	*/
	if((mTime>=0.) && (mTime<0.3))
		gl_FragColor = vec4(inputPixelColor.r, inputPixelColor.g, inputPixelColor.b, 1.);
	//else if((mTime>=0.3) && (mTime<0.6))
	//	gl_FragColor = vec4(inputPixelColor.g, inputPixelColor.b, inputPixelColor.r, .3);
	else
		gl_FragColor = vec4(inputPixelColor.b, inputPixelColor.r, inputPixelColor.g, .3);
}

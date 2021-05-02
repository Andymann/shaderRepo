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
            "DEFAULT": [
                0,
                0,
                1,
                1
            ],
            "NAME": "colorInput",
            "TYPE": "color"
        },
        {
            "DEFAULT": 0.5,
            "LABEL": "Visible 1",
            "MAX": 1,
            "MIN": 0,
            "NAME": "fVis1",
            "TYPE": "float"
        },
        {
            "DEFAULT": 0.5,
            "LABEL": "Visible 2",
            "MAX": 1,
            "MIN": 0,
            "NAME": "fVis2",
            "TYPE": "float"
        },
        {
            "DEFAULT": 0.25,
            "LABEL": "Visible 3",
            "MAX": 1,
            "MIN": 0,
            "NAME": "fVis3",
            "TYPE": "float"
        },
        {
            "DEFAULT": 0.5,
            "LABEL": "Visible 4",
            "MAX": 1,
            "MIN": 0,
            "NAME": "fVis4",
            "TYPE": "float"
        },
        {
            "DEFAULT": 0.55,
            "LABEL": "Visible 5",
            "MAX": 1,
            "MIN": 0,
            "NAME": "fVis5",
            "TYPE": "float"
        },
        {
            "DEFAULT": 0.85,
            "LABEL": "Visible 6",
            "MAX": 1,
            "MIN": 0,
            "NAME": "fVis6",
            "TYPE": "float"
        },
        {
            "DEFAULT": 0.15,
            "LABEL": "Visible 7",
            "MAX": 1,
            "MIN": 0,
            "NAME": "fVis7",
            "TYPE": "float"
        },
        {
            "DEFAULT": 0.75,
            "LABEL": "Visible 8",
            "MAX": 1,
            "MIN": 0,
            "NAME": "fVis8",
            "TYPE": "float"
        }
    ],
    "ISFVSN": "2"
}
*/

float rectangle(in vec2 st, in vec2 origin, in vec2 dimensions) {
    vec2 bl = step(origin, st);
    float pct = bl.x * bl.y;
    vec2 tr = step(1.0 - origin - dimensions, 1.0 - st);
    pct *= tr.x * tr.y;
    
    return pct;
}

void main()	{
	vec4		inputPixelColor;
	//	both of these are the same
	//inputPixelColor = IMG_THIS_PIXEL(inputImage);
	//inputPixelColor = IMG_PIXEL(inputImage, gl_FragCoord.xy);
	
	//	both of these are also the same
	//inputPixelColor = IMG_NORM_PIXEL(inputImage, isf_FragNormCoord.xy);
	inputPixelColor = IMG_THIS_NORM_PIXEL(inputImage);
	
	float width = 0.125;
    vec2 st = gl_FragCoord.xy/RENDERSIZE.xy;
    //vec3 color = vec3(.0);
    float x =rectangle(st, vec2(0.0*width, 0.0), vec2(width, st.y));
    //vec4 y = vec4(x/inputPixelColor.r, x/inputPixelColor.g, x/inputPixelColor.b, 1.);
	//inputPixelColor += y;
    inputPixelColor =  rectangle(st, vec2(0.0*width, 0.0), vec2(width, st.y))*colorInput*fVis1;
    inputPixelColor +=  rectangle(st, vec2(1.0*width, 0.0), vec2(width, st.y))*colorInput*fVis2;
    inputPixelColor +=  rectangle(st, vec2(2.0*width, 0.0), vec2(width, st.y))*colorInput*fVis3;
    inputPixelColor +=  rectangle(st, vec2(3.0*width, 0.0), vec2(width, st.y))*colorInput*fVis4;
    inputPixelColor +=  rectangle(st, vec2(4.0*width, 0.0), vec2(width, st.y))*colorInput*fVis5;
    inputPixelColor +=  rectangle(st, vec2(5.0*width, 0.0), vec2(width, st.y))*colorInput*fVis6;
    inputPixelColor +=  rectangle(st, vec2(6.0*width, 0.0), vec2(width, st.y))*colorInput*fVis7;
    inputPixelColor +=  rectangle(st, vec2(7.0*width, 0.0), vec2(width, st.y))*colorInput*fVis8;

	gl_FragColor = vec4(inputPixelColor.r, inputPixelColor.g, inputPixelColor.b, 1.0);
}

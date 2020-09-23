/*
{
  "CATEGORIES" : [
    "XXX"
  ],
  "DESCRIPTION" : "",
  "ISFVSN" : "2",
  "INPUTS" : [
	
    {
      "NAME" : "zoom1",
      "TYPE" : "float",
      "MAX" : 1,
      "DEFAULT" : 0.01,
      "LABEL" : "Zoom 1",
      "MIN" : 0
    },
    {
      "NAME" : "zoom2",
      "TYPE" : "float",
      "MAX" : 1,
      "DEFAULT" : 0.01,
      "LABEL" : "Zoom 2",
      "MIN" : 0
    },
    {
      "NAME" : "zoom3",
      "TYPE" : "float",
      "MAX" : 1,
      "DEFAULT" : 0.01,
      "LABEL" : "Zoom 3",
      "MIN" : 0
    },
    {
      "NAME" : "zoom4",
      "TYPE" : "float",
      "MAX" : 1,
      "DEFAULT" : 0.01,
      "LABEL" : "Zoom 4",
      "MIN" : 0
    },
    {
      "NAME" : "zoom5",
      "TYPE" : "float",
      "MAX" : 1,
      "DEFAULT" : 0.01,
      "LABEL" : "Zoom 5",
      "MIN" : 0
    },
    {
      "NAME" : "zoom6",
      "TYPE" : "float",
      "MAX" : 1,
      "DEFAULT" : 0.01,
      "LABEL" : "Zoom 6",
      "MIN" : 0
    },
    {
      "NAME" : "zoom7",
      "TYPE" : "float",
      "MAX" : 1,
      "DEFAULT" : 0.01,
      "LABEL" : "Zoom 7",
      "MIN" : 0
    },
    {
      "NAME" : "zoom8",
      "TYPE" : "float",
      "MAX" : 1,
      "DEFAULT" : 0.01,
      "LABEL" : "Zoom 8",
      "MIN" : 0
    },
    {
      "NAME" : "zoom9",
      "TYPE" : "float",
      "MAX" : 1,
      "DEFAULT" : 0.01,
      "LABEL" : "Zoom 9",
      "MIN" : 0
    },
    {
      "NAME" : "zoom10",
      "TYPE" : "float",
      "MAX" : 1,
      "DEFAULT" : 0.01,
      "LABEL" : "Zoom 10",
      "MIN" : 0
    },
    {
      "NAME" : "zoom11",
      "TYPE" : "float",
      "MAX" : 1,
      "DEFAULT" : 0.01,
      "LABEL" : "Zoom 11",
      "MIN" : 0
    },
    {
      "NAME" : "zoom12",
      "TYPE" : "float",
      "MAX" : 1,
      "DEFAULT" : 0.01,
      "LABEL" : "Zoom 12",
      "MIN" : 0
    },
    {
      "NAME" : "zoom13",
      "TYPE" : "float",
      "MAX" : 1,
      "DEFAULT" : 0.01,
      "LABEL" : "Zoom 13",
      "MIN" : 0
    },
    {
      "NAME" : "zoom14",
      "TYPE" : "float",
      "MAX" : 1,
      "DEFAULT" : 0.01,
      "LABEL" : "Zoom 14",
      "MIN" : 0
    },
    {
      "NAME" : "zoom15",
      "TYPE" : "float",
      "MAX" : 1,
      "DEFAULT" : 0.01,
      "LABEL" : "Zoom 15",
      "MIN" : 0
    },
    {
      "NAME" : "zoom16",
      "TYPE" : "float",
      "MAX" : 1,
      "DEFAULT" : 0.01,
      "LABEL" : "Zoom 16",
      "MIN" : 0
    }
    
  ],
  "CREDIT" : ""
}
*/


//derived from http://glslsandbox.com/e#37345.0
//all credits go there

#ifdef GL_ES
precision mediump float;
#endif

////#extension GL_OES_standard_derivatives : enable


vec2 pos1 = vec2(0.2,0.8);
vec2 pos2 = vec2(0.4,0.8);
vec2 pos3 = vec2(0.6,0.8);
vec2 pos4 = vec2(0.8,0.8);

vec2 pos5 = vec2(0.2,0.6);
vec2 pos6 = vec2(0.4,0.6);
vec2 pos7 = vec2(0.6,0.6);
vec2 pos8 = vec2(0.8,0.6);

vec2 pos9 =  vec2(0.2,0.4);
vec2 pos10 = vec2(0.4,0.4);
vec2 pos11 = vec2(0.6,0.4);
vec2 pos12 = vec2(0.8,0.4);

vec2 pos13 = vec2(0.2,0.2);
vec2 pos14 = vec2(0.4,0.2);
vec2 pos15 = vec2(0.6,0.2);
vec2 pos16 = vec2(0.8,0.2);

vec2 rotate(vec2 point, float rads) {
	float cs = cos(rads);
	float sn = sin(rads);
	return point * mat2(cs, -sn, sn, cs);
}

bool star(vec2 p) {
	int i = (length(p) > 0.5) ? 1 : 0;
	int j = 0;
	for (float i=0.0; i<360.0; i+=72.0)
	{
		vec2 p0 = rotate(p.yx, radians(i)+4.*sin(TIME/3.));
		if (p0.x > 0.1545) j++;
	}

	return (j < 2 && i < 1);
}

void main( void ) {

	vec2 position = ( gl_FragCoord.xy / RENDERSIZE.xy ) ;	
	position.x = position.x * RENDERSIZE.x/RENDERSIZE.y;
	
	//float ratio = RENDERSIZE.x/RENDERSIZE.y;
	float color=0.0;	
	//float radius=0.1;
	//vec2 origin = vec2(0.4,0.5);
	
	 
	if ((star((position-pos1)/zoom1)) || 
	    (star((position-pos2)/zoom2)) ||
	    (star((position-pos3)/zoom3)) ||
	    (star((position-pos4)/zoom4))	||
	    
	    (star((position-pos5)/zoom5))	||
	    (star((position-pos6)/zoom6))	||
	    (star((position-pos7)/zoom7))	||
	    (star((position-pos8)/zoom8))	||
	    
	    (star((position-pos9)/zoom9))	||
	    (star((position-pos10)/zoom10))	||
	    (star((position-pos11)/zoom11))	||
	    (star((position-pos12)/zoom12))	||
	    
	    (star((position-pos13)/zoom13))	||
	    (star((position-pos14)/zoom14))	||
	    (star((position-pos15)/zoom15))	||
	    (star((position-pos16)/zoom16))
	){
		color = 1.;
	}else{
		color = 0.;
	}	 

	gl_FragColor = vec4( color,color, color,1.0 );


}
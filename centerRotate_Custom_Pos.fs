/*
{
  "CATEGORIES" : [
    "XXX"
  ],
  "DESCRIPTION" : "",
  "ISFVSN" : "2",
  "INPUTS" : [
    {
      "NAME" : "Mouse",
      "TYPE" : "point2D",
      "MAX" : [
        1,
        1
      ],
      "DEFAULT" : [
        0.5,
        0.5
      ],
      "LABEL" : "Mouse",
      "MIN" : [
        0,
        0
      ]
    },
    {
      "LABELS" : [
        "2",
        "4",
        "6",
        "8",
        "10",
        "12",
        "14"
      ],
      "TYPE" : "long",
      "NAME" : "Spokes",
      "DEFAULT" : 12,
      "LABEL" : "Spokes",
      "VALUES" : [
        2,
        4,
        6,
        8,
        10,
        12,
        14
      ]
    },
    {
      "NAME" : "Red_1",
      "TYPE" : "float",
      "DEFAULT" : 0.40000000596046448,
      "LABEL" : "Red 1",
      "MIN" : 0
    },
    {
      "NAME" : "Green_1",
      "TYPE" : "float",
      "DEFAULT" : 0.69999998807907104,
      "LABEL" : "Green 1",
      "MIN" : 0
    },
    {
      "NAME" : "Blue_1",
      "TYPE" : "float",
      "MAX" : 1,
      "DEFAULT" : 1,
      "LABEL" : "Blue 1",
      "MIN" : 0
    },
    {
      "NAME" : "Red_2",
      "TYPE" : "float",
      "MAX" : 1,
      "DEFAULT" : 0,
      "LABEL" : "Red 2",
      "MIN" : 0
    },
    {
      "NAME" : "Green_2",
      "TYPE" : "float",
      "MAX" : 1,
      "DEFAULT" : 0.5,
      "LABEL" : "Green 2",
      "MIN" : 0
    },
    {
      "NAME" : "Blue_2",
      "TYPE" : "float",
      "MAX" : 1,
      "DEFAULT" : 1,
      "LABEL" : "Blue 2",
      "MIN" : 0
    },
    {
      "NAME" : "Vignete",
      "TYPE" : "float",
      "MAX" : 5,
      "DEFAULT" : 1,
      "LABEL" : "Vignete",
      "MIN" : 0
    },
    {
      "NAME" : "Speed",
      "TYPE" : "float",
      "MAX" : 23,
      "DEFAULT" : 3,
      "MIN" : 0
    }
  ],
  "CREDIT" : ""
}
*/


#ifdef GL_ES
precision mediump float;
#endif
//int Spokes = 12;
//#extension GL_OES_standard_derivatives : enable


// Created by inigo quilez - iq/2014
// Updated with love <3
// License Creative Commons Attribution-NonCommercial-ShareAlike 3.0 Unported License.

void main( void ) {

int Spokes=12;
       	vec2 p = Mouse*(-RENDERSIZE.xy + 2.0*gl_FragCoord.xy) / RENDERSIZE.y;
		//vec2 p = vec2((-RENDERSIZE.x + 2.0*gl_FragCoord.x)/RENDERSIZE.x , (-RENDERSIZE.y + 2.0*gl_FragCoord.y)/RENDERSIZE.y) ;
		//p.x*=Mouse.x;
		//p.y*=Mouse.y;
		//p.x*=Mouse.x;
		// background	
        vec2 q = vec2(atan(p.y, p.x + .5), length(p));
        
        float f = smoothstep(-0.1, 0.1, sin(q.x*float(Spokes) + TIME*Speed));
        vec3 col = mix(vec3(Red_1, Green_1, Blue_1), vec3(Red_2, Green_2, Blue_2), f);

        // vigneting	
        col *= Vignete - 0.2*length(p);

        gl_FragColor = vec4(col, 1.0);


}
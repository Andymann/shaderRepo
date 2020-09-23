/*
{
  "CATEGORIES" : [
    "XXX"
  ],
  "DESCRIPTION" : "",
  "ISFVSN" : "2",
  "INPUTS" : [
    {
      "NAME" : "MotionSpeed",
      "TYPE" : "float",
      "MAX" : 3,
      "DEFAULT" : 0.3,
      "LABEL" : "MotionSpeed",
      "MIN" : 0
    },
    {
      "NAME" : "RotationSizeX",
      "TYPE" : "float",
      "MAX" : 5,
      "DEFAULT" : 1.0,
      "LABEL" : "RotationSizeX",
      "MIN" : -5
    },
    {
      "NAME" : "RotationSizeY",
      "TYPE" : "float",
      "MAX" : 5,
      "DEFAULT" : 1.0,
      "LABEL" : "RotationSizeY",
      "MIN" : -5
    },
    {
      "NAME" : "TubeDiameter",
      "TYPE" : "float",
      "MAX" : 0.4,
      "DEFAULT" : 0.1,
      "LABEL" : "Tube Diameter",
      "MIN" : 0
    },
    {
      "NAME" : "Focus",
      "TYPE" : "float",
      "MAX" : 10.0,
      "DEFAULT" : 2.0,
      "LABEL" : "Focus",
      "MIN" : 0
    },
    {
      "NAME" : "MaxMarch",
      "TYPE" : "float",
      "MAX" : 200,
      "DEFAULT" : 100,
      "LABEL" : "MaxMarch",
      "MIN" : 0
    },
    {
      "NAME" : "MaxDistance",
      "TYPE" : "float",
      "MAX" : 23,
      "DEFAULT" : 5,
      "LABEL" : "MaxDistance",
      "MIN" : 1
    },
    {
      "NAME" : "Fog",
      "TYPE" : "float",
      "MAX" : 23,
      "DEFAULT" : 5,
      "LABEL" : "Fog",
      "MIN" : 1
    },
    {
      "NAME" : "TravelSpeed",
      "TYPE" : "float",
      "MAX" : 5.0,
      "DEFAULT" : 0.5,
      "LABEL" : "TravelSpeed",
      "MIN" : -5.0
    },
    {
      "NAME" : "Random",
      "TYPE" : "float",
      "MAX" : 50.0,
      "DEFAULT" : 22.0,
      "LABEL" : "Random",
      "MIN" : 0
    },
    {
      "NAME" : "Red",
      "TYPE" : "float",
      "MAX" : 1,
      "DEFAULT" : 0.4,
      "LABEL" : "Red",
      "MIN" : 0
    },
    {
      "NAME" : "Green",
      "TYPE" : "float",
      "MAX" : 1,
      "DEFAULT" : 0.6,
      "LABEL" : "Green",
      "MIN" : 0
    },
    {
      "NAME" : "Blue",
      "TYPE" : "float",
      "MAX" : 1,
      "DEFAULT" : 1,
      "LABEL" : "Blue",
      "MIN" : 0
    },
    {
      "NAME" : "AlternatingDirection",
      "TYPE" : "bool",
      "DEFAULT" : 1
    },
    {
      "NAME" : "CenterXY",
      "TYPE" : "point2D",
      "MAX" : [
        2,
        2
      ],
      "DEFAULT" : [
        0,
        0
      ],
      "MIN" : [
        -2,
        -2
      ]
    }

  ],
  "CREDIT" : ""
}
*/


//This is extracted from one of DestroyThingsBeautiful's QTZ Packages
//I don't know where or when I found it.
//It's my absolute favourite shader of all times


#ifdef GL_ES
precision mediump float;
#endif

//#extension GL_OES_standard_derivatives : enable



// Tweaked by T21 : 3d noise

float rand(vec3 n, float res)
{
  n = floor(n*res+.5);
  return fract(sin((n.x+n.y*1e2+n.z*1e4)*1e-4)*1e5);
}

float map( vec3 p )
{
    p = mod(p,vec3(1.0, 1.0, 1.0))-0.5;
    return length(p.xy)-TubeDiameter;
}

void main( void )
{
    vec2 pos = (gl_FragCoord.xy*2.0 - RENDERSIZE.xy) / RENDERSIZE.y;
    vec3 camPos = vec3(cos(TIME*MotionSpeed)*RotationSizeX+CenterXY.x, sin(TIME*MotionSpeed)*RotationSizeY+CenterXY.y, 1.5);
    vec3 camTarget = vec3(0.0, 0.0, 0.0);

    vec3 camDir = normalize(camTarget-camPos);
    vec3 camUp  = normalize(vec3(0.0, 1.0, 0.0));
    vec3 camSide = cross(camDir, camUp);
    //float focus = 2.0;
    float direction =1.0;

    vec3 rayDir = normalize(camSide*pos.x + camUp*pos.y + camDir*Focus);
    vec3 ray = camPos;
    float d = 0.0, total_d = 0.0;
    int MAX_MARCH = int(MaxMarch);
    float c = 1.0;
    for(int i=0; i<MAX_MARCH; ++i) {
        d = map(ray);
        total_d += d;
        ray += rayDir * d;
        if(abs(d)<0.001) { break; }
        if(total_d>MaxDistance) { c = 0.; total_d=MaxDistance; break; }
    }
	
    //float fog = 5.0;
    vec4 result = vec4( vec3(c*Red , c*Green, c*Blue) * (Fog - total_d) / Fog, 1.0 );

	if(AlternatingDirection){
    		direction = cos(floor(ray.x*3.+ray.y*2.));
	}
	ray.z -= 1.+TIME*TravelSpeed * direction;
    float r = rand(ray, Random);
    gl_FragColor = result*(step(r,.3)+r*.2+.1);
}
/*
{
  "CATEGORIES" : [
    "Automatically Converted",
    "GLSLSandbox"
  ],
  "DESCRIPTION" : "Automatically converted from http:\/\/glslsandbox.com\/e#57246.0",
  "INPUTS" : [
    {
      "NAME" : "Bar1",
      "TYPE" : "float",
      "MAX" : 1,
      "DEFAULT" : 0.10000000000000001,
      "LABEL" : "Bar1",
      "MIN" : 0
    },
    {
      "NAME" : "Bar2",
      "TYPE" : "float",
      "MAX" : 1,
      "DEFAULT" : 0.20000000000000001,
      "LABEL" : "Bar2",
      "MIN" : 0
    },
    {
      "NAME" : "Bar3",
      "TYPE" : "float",
      "MAX" : 1,
      "DEFAULT" : 0.30000000000000001,
      "LABEL" : "Bar3",
      "MIN" : 0
    },
    {
      "NAME" : "Bar4",
      "TYPE" : "float",
      "MAX" : 1,
      "DEFAULT" : 0.40000000000000001,
      "LABEL" : "Bar4",
      "MIN" : 0
    },
    {
      "NAME" : "Bar5",
      "TYPE" : "float",
      "MAX" : 1,
      "DEFAULT" : 0.60000000000000001,
      "LABEL" : "Bar5",
      "MIN" : 0
    },
    {
      "NAME" : "Bar6",
      "TYPE" : "float",
      "MAX" : 1,
      "DEFAULT" : 0.80000000000000001,
      "LABEL" : "Bar6",
      "MIN" : 0
    },
    {
      "NAME" : "Bar7",
      "TYPE" : "float",
      "MAX" : 1,
      "DEFAULT" : 0.40000000000000001,
      "LABEL" : "Bar7",
      "MIN" : 0
    },
    {
      "NAME" : "Bar8",
      "TYPE" : "float",
      "MAX" : 1,
      "DEFAULT" : 0.50000000000000001,
      "LABEL" : "Bar8",
      "MIN" : 0
    },
    {
      "NAME" : "Bar9",
      "TYPE" : "float",
      "MAX" : 1,
      "DEFAULT" : 0.70000000000000001,
      "LABEL" : "Bar9",
      "MIN" : 0
    },
    {
      "NAME" : "Bar10",
      "TYPE" : "float",
      "MAX" : 1,
      "DEFAULT" : 0.60000000000000001,
      "LABEL" : "Bar10",
      "MIN" : 0
    },
  ],
  "ISFVSN" : "2"
}
*/


#ifdef GL_ES
precision mediump float;
#endif

#extension GL_OES_standard_derivatives : enable




float rectangle(in vec2 st, in vec2 origin, in vec2 dimensions) {
    vec2 bl = step(origin, st);
    float pct = bl.x * bl.y;
    vec2 tr = step(1.0 - origin - dimensions, 1.0 - st);
    pct *= tr.x * tr.y;
    
    return pct;
}

void main() {
    vec2 st = gl_FragCoord.xy/RENDERSIZE.xy;
    vec3 color = vec3(.0);
	color += rectangle(st, vec2(0.0, 0.0), vec2(0.1, Bar1));
    color += rectangle(st, vec2(0.1, 0.0), vec2(0.1, Bar2));
	color += rectangle(st, vec2(0.2, 0.0), vec2(0.1, Bar3));
	color += rectangle(st, vec2(0.3, 0.0), vec2(0.1, Bar4));
	
	color += rectangle(st, vec2(0.4, 0.0), vec2(0.1, Bar5));
	color += rectangle(st, vec2(0.5, 0.0), vec2(0.1, Bar6));
	color += rectangle(st, vec2(0.6, 0.0), vec2(0.1, Bar7));
	color += rectangle(st, vec2(0.7, 0.0), vec2(0.1, Bar8));
	color += rectangle(st, vec2(0.8, 0.0), vec2(0.1, Bar9));
	color += rectangle(st, vec2(0.9, 0.0), vec2(0.1, Bar10));
    

    gl_FragColor = vec4(.0, .0, .0, color);
}
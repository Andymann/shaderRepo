/*
{
  "CATEGORIES" : [
    "Automatically Converted",
    "GLSLSandbox"
  ],
  "INPUTS" : [
    {
      "NAME" : "mouse",
      "TYPE" : "point2D",
      "MAX" : [
        1,
        1
      ],
      "MIN" : [
        0,
        0
      ]
    }
  ],
  "DESCRIPTION" : "Automatically converted from http:\/\/glslsandbox.com\/e#46319.0"
}
*/


//precision mediump float;


void main(void)
{
	// I'm learnding :)
	float AR = RENDERSIZE.x / RENDERSIZE.y;
	vec2 n = gl_FragCoord.xy / RENDERSIZE.xy;
	vec2 m = mouse;
	n.x *= AR;
	m.x *= AR;
	float color = -(distance(m,n) * 2.0);
	n = fract(n * (5.0 + sin(TIME) * sin(TIME * 2.0)));
	color += step(distance(n,vec2(0.5 + (sin(TIME) * 0.25), 0.5 + (cos(TIME) * 0.25))),0.25);
	
	gl_FragColor = vec4(vec3(color),1.0);
}
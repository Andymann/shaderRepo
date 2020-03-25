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
  "DESCRIPTION" : "Automatically converted from http:\/\/glslsandbox.com\/e#52854.1"
}
*/



#ifdef GL_ES
precision mediump float;
#endif



const int   complexity      = 15;    // More points of color.
const float mouse_factor    = 1.0;  // Makes it more/less jumpy.
const float mouse_offset    = 2.0;   // Drives complexity in the amount of curls/cuves.  Zero is a single whirlpool.
const float fluid_speed     = 18.0;  // Drives speed, higher number will make it slower.
const float color_intensity = 0.9;
  

void main()
{
  vec2 p=(2.0*gl_FragCoord.xy-RENDERSIZE)/max(RENDERSIZE.x,RENDERSIZE.y);
  for(int i=1;i<complexity;i++)
  {
    vec2 newp=p + TIME*0.001;
    newp.x+=0.6/float(i)*sin(float(i)*p.y+TIME/fluid_speed+0.3*float(i)) + 0.5 + mouse.y/mouse_factor+mouse_offset;
    newp.y+=0.6/float(i)*sin(float(i)*p.x+TIME/fluid_speed+0.3*float(i+10)) - 0.5 - mouse.x/mouse_factor+mouse_offset;
    p=newp;
  }
  vec3 col=vec3(color_intensity*sin(3.0*p.x)+color_intensity,color_intensity*sin(3.0*p.y)+color_intensity,color_intensity*sin(p.x+p.y)+color_intensity);
  gl_FragColor=vec4(col, 1.0);
}
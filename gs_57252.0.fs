/*
{
  "CATEGORIES" : [
    "Automatically Converted",
    "GLSLSandbox"
  ],
  "INPUTS" : [

  ],
  "DESCRIPTION" : "Automatically converted from http:\/\/glslsandbox.com\/e#57252.0"
}
*/


#ifdef GL_ES
precision mediump float;
#endif

// woah circle
// thanks http://glslsandbox.com/e#41504.0


void main()
{
    vec2 r = RENDERSIZE,
    o = gl_FragCoord.xy - r/2.;
    o = vec2(length(o) / r.y - .3, atan(o.y,o.x));    
    vec4 s = 0.08*cos(1.5*vec4(0,1,2,3) + TIME + o.y + sin(o.x) * cos(TIME)),
    e = s.yzwx, 
    f = min(o.x-s,e-o.x);
    gl_FragColor = dot(clamp(f*r.y,0.,1.), 50.*(s-e)) * (s-.1) /*- f*/;
    gl_FragColor.a = 1.;
}
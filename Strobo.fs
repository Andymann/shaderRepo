/*
{
  "CATEGORIES" : [
    "Automatically Converted",
    "GLSLSandbox"
  ],
  "INPUTS" : [

  ],
  "DESCRIPTION" : "Automatically converted from http:\/\/glslsandbox.com\/e#43280.0"
}
*/


#ifdef GL_FRAGMENT_PRECISION_HIGH
precision highp float;
#else
//precision mediump float;
#endif

void main(void){
    gl_FragColor = vec4(sin(TIME*100.)>0.0);
}


// black and white flicker to test photodiode...
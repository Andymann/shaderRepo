/*
{
  "CATEGORIES" : [
    "Automatically Converted",
    "GLSLSandbox"
  ],
  "INPUTS" : [

  ],
  "DESCRIPTION" : "Automatically converted from http:\/\/glslsandbox.com\/e#45710.0"
}
*/


// modified from https://thebookofshaders.com/09/

#ifdef GL_ES
precision mediump float;
#endif


vec2 tile(vec2 _st, float _zoom){
    _st *= _zoom;
    return fract(_st);
}

float box(vec2 _st, vec2 border){
    vec2 uv = step(border, _st);
    return uv.x*uv.y;
}

void main(void){
    vec2 st = gl_FragCoord.xy/RENDERSIZE.xy;

    float tileScaling = 16.;
    
    
    	float tileCount = tileScaling * tileScaling;
    	float tileId = 1. + floor(st.x * tileScaling) + tileScaling * floor(st.y * tileScaling);

	// Divide the space in 256
	st = tile(st, tileScaling);

    	// Draw the tile
    	float modifier = tileId / tileCount;
	vec3 color = vec3(
		abs(cos(modifier * TIME)),
		sin(modifier * TIME * .8),
		fract(modifier * TIME * .5));
    	float isActive = abs(sin(TIME * 1.3 * modifier)) * box(st, vec2(0.05));

    	gl_FragColor = vec4(vec3(isActive * color),1.0);
}
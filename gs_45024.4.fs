/*
{
  "CATEGORIES" : [
    "Automatically Converted",
    "GLSLSandbox"
  ],
  "INPUTS" : [

  ],
  "DESCRIPTION" : "Automatically converted from http:\/\/glslsandbox.com\/e#45024.4"
}
*/


#ifdef GL_ES
precision mediump float;
#endif

#extension GL_OES_standard_derivatives : enable


const float blockPerc = 0.5;
const float steps = 256.0;

const float blockCol = 1.0;
const float streetCol = 0.0;

float boxes(vec2 p)
{
	float blockY = floor(p.y);
	p.x += sin(blockY + TIME);

	p = mod(p, 1.0);
	if (p.x < blockPerc && p.y < blockPerc)
		return blockCol;
	else
		return streetCol;
}

vec2 scaleTrans(vec2 p, vec2 trans, float zoom)
{
	return p * zoom + trans;
}

void main( void ) {

	vec2 pos = (( gl_FragCoord.xy / RENDERSIZE.xy ) - vec2(0.5)) * vec2(RENDERSIZE.x / RENDERSIZE.y, 1.0);
	
	float boxCol = 0.0;
	for (float i=0.0; i<steps; ++i) {
		float zoom = 4.0 + i * 0.02 + sin(TIME * 0.5) * 2.5;
		vec2 trans = vec2(sin(TIME * 0.3) * 1.5, sin(TIME * 0.4) * 2.0);
		boxCol = boxes(scaleTrans(pos, trans, zoom)) * (8.0 / (1.0 + i));
		if (boxCol != streetCol) break;
	}
	if (boxCol != streetCol)
		gl_FragColor = vec4(boxCol);
	else
		gl_FragColor = vec4(pos.x, pos.y, pos.x * pos.y, 1.0);

}
/*
{
  "CATEGORIES" : [
    "Automatically Converted",
    "GLSLSandbox"
  ],
  "INPUTS" : [

  ],
  "DESCRIPTION" : "Automatically converted from http:\/\/glslsandbox.com\/e#52660.0"
}
*/


#ifdef GL_ES
precision mediump float;
#endif
// don't need to touch this

// the pie is a lie
float PI = acos(-1.);
	
float rand(vec2 co){
    return fract(sin(dot(co.xy ,vec2(12.9898,78.233))) * 43758.5453);
}

#define TILESIZE .1
#define BORDERWIDTH .02
	
float trunc(float x) {
    return float(int(x));
}

vec2 rotate(vec2 p, float phi) {
    return vec2(p.x * cos(phi) - p.y * sin(phi), p.y * cos(phi) + p.x * sin(phi));
}

// write your awesome shadercode here
void main() {
	// this handsome line calculates the current pixel position
	// position is in the [0, 1] range now
	vec2 position = gl_FragCoord.xy / RENDERSIZE.yy;
	position *= vec2(1.0 - 0.5 * cos(TIME * .1));
	position = rotate(position, -sin(TIME * .05));
	position += vec2(TIME *.1, .01 * (sin(TIME * .1) * cos(TIME * .2)));
	
	vec3 color = vec3(0.);
	
	vec2 tile = vec2(trunc(position.x/TILESIZE), trunc(position.y / TILESIZE));
	
	float r = 0.5 + 0.5 * rand(tile.xy);
	color = vec3(r, rand(tile.yx), 0.3 * r + 0.7 * rand(tile.xx)) * (0.7 + 0.3 * sin(rand(3. * tile.xy) + TIME));
	
	color *= smoothstep(0., BORDERWIDTH, mod(position.x, TILESIZE));
	color *= smoothstep(0., BORDERWIDTH, mod(position.y, TILESIZE));

	// set the calculated color to the pixel
	gl_FragColor = vec4(color, 1.);
}
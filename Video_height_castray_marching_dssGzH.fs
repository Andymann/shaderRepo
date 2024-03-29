/*{
    "CATEGORIES": [
        "Automatically Converted",
        "Shadertoy"
    ],
    "DESCRIPTION": "Automatically converted from https://www.shadertoy.com/view/dssGzH by rubioh.  Inspire by:\nShane's work: https://www.shadertoy.com/view/tsdXDB\nGhost's work: https://www.shadertoy.com/view/4tlyD2\n\nMinimalist and readable example for extruded tile.",
    "IMPORTED": {
    },
    "INPUTS": [
        {
            "NAME": "inputImage",
            "TYPE": "image"
        }
    ],
    "ISFVSN": "2"
}
*/


float sdPlane(vec3 p){
    float width = .4; // Width of the tile
    float tiling = 15.; // Grid precision
    
    vec2 idx = floor(p.xy*tiling);
    //variation 0.1
    //float h = length(IMG_NORM_PIXEL(inputImage,mod(idx/(2.*tiling)+.5,1.0)).xyz)/7.;  // tile height, (pixel energy)
    
    
    // variation 0.2
    vec2 str = vec2(RENDERSIZE.y/RENDERSIZE.x, 1); 
    float h = length(IMG_NORM_PIXEL(inputImage,mod(str*idx/(2.*tiling)+.5,1.0)).xyz)/7.;
    //shapable tile
    vec2 f = fract(p.xy*tiling)-.5; // centering
    float N = 4.; // 1. for losange 2. circle  +inf for square (L-N distances)
    float l = pow( pow(abs(f.x), N) + pow(abs(f.y), N), 1./N);
        
    return h*smoothstep(0., 0.05, width-l);
}


float castray(vec3 ro, vec3 rd) {
  vec3 p;
  float dt, depth;
  
  float t = .05*fract(sin(dot(rd, vec3(125.45, 213.345, 156.2001)))); // dithering
  for (float d = .5; d < 2.4; d += .004) { // lower value for d induce better results but more costly
    p = ro + rd*(d+t);
    float depth = sdPlane(p);
    if (p.z < depth){
        break;}
      }
  return p.z;
}


void main() {



	float time = TIME;
    vec2 st = ( 2.*gl_FragCoord.xy - RENDERSIZE.xy ) / RENDERSIZE.y;
    vec3 ro = vec3(0, 0, 1); // ray origin 
    vec3 rd = normalize(vec3(st, -1)); // ray direction
    float d = castray(ro, rd);
	gl_FragColor = vec4(vec3(d)*5., 1.);
}

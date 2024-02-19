/*{
    "CATEGORIES": [
        "Masking"
    ],
    "CREDIT": "",
    "DESCRIPTION": "4 freely controllable square masks. Done by Andy: www.Doktor-Andy.de . Based on https://www.shadertoy.com/view/4lf3DN",
    "IMPORTED": [
    ],
    "INPUTS": [
        {
            "NAME": "inputImage",
            "TYPE": "image"
        },
        {
            "DEFAULT": 16,
            "MAX": 32,
            "MIN": 2,
            "NAME": "xResolution",
            "TYPE": "float"
        },
        {
            "DEFAULT": 9,
            "MAX": 18,
            "MIN": 2,
            "NAME": "yResolution",
            "TYPE": "float"
        },
        {
            "NAME": "gridIsBlack",
            "TYPE": "bool"
        }
    ],
    "ISFVSN": "2",
    "PASSES": [
        {
        }
    ]
}
*/

	float WIDTH = xResolution;
    float HEIGHT = yResolution;
    float DISTANCE_X = .01;
    float DISTANCE_Y = .0125;
    float CELL_SIZE;

#ifndef GL_ES
float distance (vec2 center, vec2 pt)
{
	float tmp = pow(center.x-pt.x,2.0)+pow(center.y-pt.y,2.0);
	return pow(tmp,0.5);
}
#endif


bool inQuad(vec2 verts[4], vec2 point) {
  bool hit = false;
    
  for (int i = 0; i < 4; i++) {
      vec2 vI = verts[i];
      vec2 vJ = verts[int(mod(float(i)+3.0,4.0))];
  	  if ( ((vI.y > point.y) != (vJ.y > point.y)) && (point.x < (vJ.x-vI.x) * (point.y-vI.y) / (vJ.y-vI.y) + vI.x) ) {
      	hit = !hit;
      }
  }
  return hit;
}


bool inQuad2(vec2 tl, vec2 br, vec2 point){
	bool hit = false;
	if( (tl.x<point.x)&&(tl.y>point.y) && (br.x>point.x)&&(br.y<point.y) ){
      	hit = true;
     } 
  	return hit;
}

bool inQuad3(vec2 tl, float fWidth, float fHeight, vec2 point){
	bool hit = false;
	if( (tl.x<point.x)&&(tl.y>point.y) && (tl.x+fWidth>point.x)&&(tl.y-fHeight<point.y) ){
      	hit = true;
     } 
  	return hit;
}

vec4 pixelate(vec2 px){

		// Left and right of tile
		float CellWidth = 1./xResolution;//CELL_SIZE;
		float CellHeight = 1./yResolution;// CELL_SIZE;

		float x1 = floor(px.x / CellWidth)*CellWidth;
		float x2 = clamp((ceil(px.x / CellWidth)*CellWidth), 0.0, 1.0);
		// Top and bottom of tile
		float y1 = floor(px.y / CellHeight)*CellHeight;
		float y2 = clamp((ceil(px.y / CellHeight)*CellHeight), 0.0, 1.0);

		// GET AVERAGE CELL COLOUR
		// Average left and right pixels
		vec4 avgX = (IMG_NORM_PIXEL(inputImage, vec2(x1, y1))+(IMG_NORM_PIXEL(inputImage, vec2(x2, y1)))) / 2.0;
		// Average top and bottom pixels
		vec4 avgY = (IMG_NORM_PIXEL(inputImage, vec2(x1, y1))+(IMG_NORM_PIXEL(inputImage, vec2(x1, y2)))) / 2.0;
		// Centre pixel
		vec4 avgC = IMG_NORM_PIXEL(inputImage, vec2(x1+(CellWidth/2.0), y2+(CellHeight/2.0)));	// Average the averages + centre
		vec4 avgClr = (avgX+avgY+avgC) / 3.0;

		return vec4(avgClr);

}


vec4 pixelate2(){

// Position of current pixel
		vec2 xy; 
		xy.x = isf_FragNormCoord[0];
		xy.y = isf_FragNormCoord[1];


		// Left and right of tile
		float CellWidth = 1./xResolution;;
		float CellHeight = 1./yResolution;;


		float x1 = floor(xy.x / CellWidth)*CellWidth;
		float x2 = clamp((ceil(xy.x / CellWidth)*CellWidth), 0.0, 1.0);
		// Top and bottom of tile
		float y1 = floor(xy.y / CellHeight)*CellHeight;
		float y2 = clamp((ceil(xy.y / CellHeight)*CellHeight), 0.0, 1.0);

		// GET AVERAGE CELL COLOUR
		// Average left and right pixels
		vec4 avgX = (IMG_NORM_PIXEL(inputImage, vec2(x1, y1))+(IMG_NORM_PIXEL(inputImage, vec2(x2, y1)))) / 2.0;
		// Average top and bottom pixels
		vec4 avgY = (IMG_NORM_PIXEL(inputImage, vec2(x1, y1))+(IMG_NORM_PIXEL(inputImage, vec2(x1, y2)))) / 2.0;
		// Centre pixel
		vec4 avgC = IMG_NORM_PIXEL(inputImage, vec2(x1+(CellWidth/2.0), y2+(CellHeight/2.0)));	// Average the averages + centre
		vec4 avgClr = (avgX+avgY+avgC) / 3.0;

		return vec4(avgClr);

}

void main(){
	vec2 uv = gl_FragCoord.xy / RENDERSIZE.xy;
    /*
    vec2 vs1[4];
    vs1[0] = vec2(tl1_x, tl1_y);
    vs1[1] = vec2(tr1_x, tr1_y);
    vs1[2] = vec2(br1_x, br1_y);
    vs1[3] = vec2(bl1_x, bl1_y);
    */
    vec2 vs1[4];
    vs1[0] = vec2(.1, .9);
    vs1[1] = vec2(.9, .9);
    vs1[2] = vec2(.9, .1);
    vs1[3] = vec2(.1, .1);

	
     
    gl_FragColor = vec4(0., 0., 0., float(gridIsBlack));

	for(float j=1.; j<HEIGHT+1.; j++){
		for(float i=0.; i<WIDTH; i++){
			if ( inQuad3(vec2(i*(1./WIDTH)+DISTANCE_X/2., j*(1./HEIGHT)-DISTANCE_Y/2.), 1./WIDTH-DISTANCE_X, 1./HEIGHT-DISTANCE_Y , uv) ){
				//gl_FragColor = vec4( IMG_THIS_PIXEL(inputImage) * opa1 );
				//gl_FragColor = pixelate(IMG_THIS_PIXEL(inputImage)*opa1);
				
				gl_FragColor = pixelate(uv);
				//gl_FragColor = pixelate2();
			}
    	}
    }

}

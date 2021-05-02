/*
{
  "CATEGORIES" : [
    "Automatically Converted",
    "GLSLSandbox"
  ],
  "INPUTS" : [

  ],
  "DESCRIPTION" : "Automatically converted from http:\/\/glslsandbox.com\/e#69380.0"
}
*/


#ifdef GL_ES
precision highp float;
#endif

#extension GL_OES_standard_derivatives : enable


vec3 block[8];


void main( void ) {

	if(TIME > 0.0){
	block[0] = vec3(-0.2, -0.2, -0.2);
	block[1] = vec3(0.2, 0.2, 0.2);
	block[2] = vec3(0.2, -0.2, -0.2);
	block[3] = vec3(-0.2, -0.2, 0.2);
	block[4] = vec3(0.2, -0.2, 0.2);
	block[5] = vec3(-0.2, 0.2, 0.2);
	block[6] = vec3(-0.2, 0.2, -0.2);
	block[7] = vec3(0.2, 0.2, -0.2);
}
	
	vec2 position = ( gl_FragCoord.xy / RENDERSIZE.xy );
	
	for(int x = 0; x < 8; x++)
	    {
	    	float degree = TIME/2.0;
	    	float radian = degree / 180.0 * 3.14159265358;
	    	block[x].y = block[x].y * cos(radian) - block[x].z * sin(radian);
	    	block[x].z = block[x].y * sin(radian) + block[x].z * cos(radian);
		//block[x].x = block[x].x * sin(radian) - block[x].z * cos(radian);
		float w = (RENDERSIZE.x / RENDERSIZE.y);
	    	float t = RENDERSIZE.x/w;
	    	float a = RENDERSIZE.y;
		    
		    float piy = gl_FragCoord.y;
		    float pix = gl_FragCoord.x;
		    float py = block[x].y *a + a/2.0;
		    float px = block[x].x *t + t/2.0;
		    float ny = 0.0;
		    float nx = 0.0;
		    if(x+1 < 8){
			 ny = block[x+1].y *a + a/2.0;
		    nx = block[x+1].x *t + t/2.0;
		    }
		    else{
			 ny = block[0].y *a + a/2.0;
		    nx = block[0].x *t + t/2.0;  
		    }
		    float m = (py - ny) / (px - nx);
		    float ta = py - m*px;
		    float yx =  m*pix+ta;
		    
		    
		    if(yx > piy -2.0 && yx < piy + 2.0){
			 gl_FragColor = vec4(1.0*abs(sin(TIME*.3)), 1.0*abs(sin(TIME*.66)), 1.*abs(sin(TIME*.99)), 1.0);   
		    }

	    }
	
	

}
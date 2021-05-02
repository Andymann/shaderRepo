/*
{
    "CATEGORIES": [
        "Automatically Converted",
        "GLSLSandbox"
    ],
    "DESCRIPTION": "Automatically converted from http://glslsandbox.com/e#71991.0",
    "INPUTS": [
    ]
}

*/



#ifdef GL_ES
precision mediump float;
#endif

//#extension GL_OES_standard_derivatives : enable

#define THICKNESS 0.4
#define LENGTH 0.05

float hseed(float t,float ct,float seed)
{
	float cn = floor(t/ct);
	float poff= 0.3+0.7*fract(0.4+5.2147*cn);
	float s = step(0.25,fract(cn*0.5));
	float y = RENDERSIZE.y*fract((mod(cn,943.7)+2.2)*(1.1+mod(cn,676.7))*(0.27+seed*22.773));
	float x = RENDERSIZE.x*mix(-0.3,1.3,(t/ct-cn)/(poff)*(1.0-2.0*s)+s);
	float col = max(0.0,1.0-THICKNESS*abs(gl_FragCoord.y-y));
	return col * 100.0/(100.0+LENGTH*(gl_FragCoord.x-x)*(gl_FragCoord.x-x));
}

float vseed(float t,float ct,float seed)
{
	float cn = floor(t/ct);
	float poff= 0.3+0.7*fract(0.4+7.42787*cn);
	float s = step(0.25,fract(cn*0.5));
	float x = RENDERSIZE.x*fract((mod(cn,912.7)+1.22)*(2.11+mod(cn,674.7))*(0.21+seed*13.773));
	float y = RENDERSIZE.y*mix(-0.5,1.5,(t/ct-cn)/(poff)*(1.0-2.0*s)+s);
	float col = max(0.0,1.0-THICKNESS*abs(gl_FragCoord.x-x));
	return col * 100.0/(100.0+LENGTH*(gl_FragCoord.y-y)*(gl_FragCoord.y-y));
}



void main( void ) {
	
	float col = 0.0;
	for(float i=0.0;i<10.0;i++)
	{
		//col = (1.0-col)*(1.0-hseed(TIME,5.0+i/4.0,1.0,i/10.0));
		//col = col*(1.0-vseed(TIME,5.0+i/4.0,1.0,i/10.0));
		col = max(col,hseed(TIME,4.0+i*0.78,12.0+i/100.0));
		col = max(col,vseed(TIME,(4.0+i*0.78)*RENDERSIZE.x/RENDERSIZE.y,12.0+i/100.0));		
	}
	
	gl_FragColor = vec4( col,col,col, 1.0 );

}
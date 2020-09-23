/*
{
  "CATEGORIES" : [
    "Automatically Converted",
    "GLSLSandbox"
  ],
  "INPUTS" : [

  ],
  "DESCRIPTION" : "Automatically converted from http:\/\/glslsandbox.com\/e#66338.0"
}
*/


//--- hatsuyuki ---
// by Catzpaw 2016
#ifdef GL_ES
precision mediump float;
#endif

//#extension GL_OES_standard_derivatives : enable


float snow(vec2 uv,float scale)
{
	float w=smoothstep(1.,0.,-uv.y*(scale/10.));
	if(w<.0)return 0.;
	 uv.y+=TIME*4./scale; 
	uv*=scale;vec2 s=floor(uv),f=fract(uv),p;float k=40.,d;
	p=.5+.35*sin(11.*fract(sin((s+p+scale)*mat2(7,3,6,5))*5.))-f;
	d=length(p);k=min(d,k);
	k=smoothstep(0.,k,sin(f.x+f.y)*0.003);
    	return k*w;
}

void main(void){
	vec2 uv=(gl_FragCoord.xy*2.-RENDERSIZE.xy)/min(RENDERSIZE.x,RENDERSIZE.y); 
	vec3 finalColor=vec3(0);
	float c=smoothstep(0.1,0.0,clamp(uv.x*.01+.99,0.,.99));
	c+=snow(uv,3.)*.8;
	c+=snow(uv,5.)*.7;
	c+=snow(uv,7.)*.6;
	
	c+=snow(uv,9.)*.5;
	
	c+=snow(uv,11.)*.4;
	
	c+=snow(uv,13.)*.3;
	
	c+=snow(uv,15.)*.2;
	
	c+=snow(uv,17.)*.1;
	
 
	finalColor=(vec3(c*0.0,c*0.8,c*01.9));
	gl_FragColor = vec4(finalColor,1.)*81.0;
}
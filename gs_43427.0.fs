/*
{
  "CATEGORIES" : [
    "Automatically Converted",
    "GLSLSandbox"
  ],
  "INPUTS" : [

  ],
  "DESCRIPTION" : "Automatically converted from http:\/\/glslsandbox.com\/e#43427.0"
}
*/







#ifdef GL_ES
precision mediump float;
#endif
//Miners Drill
//2017.11.3 You can contacted me on steam at Pass The salt plz
#extension GL_OES_standard_derivatives : enable


vec4 pattern(vec2 pos, float ang) 
{
        pos = vec2(pos.x * cos(ang) - pos.y * sin(ang), pos.y * cos(ang) + pos.x * sin(ang));	
	
	//if(length(pos) < 0.2)
	if(abs(pos.x) < 0.3 && abs(pos.y) < 0.3)
	   return vec4(.0, 0.0, 0.0, 0.0);
	else if((abs(pos.y) - abs(pos.x)) > 0.0)
	   return vec4(0., 0.67, 0.05, 1.0);
	else
	   return vec4(7., 1., 0.15, 9.0);			
}

void main( void ) 
{
	vec2 pos = ( gl_FragCoord.xy / RENDERSIZE.xy ) - vec2(0.5, 0.5);
	vec4 color = vec4(0.0);
	
	for(float i =  1.; i < 20. ; i ++)
	{
		float o = (20. - i)/20.;
		vec2 offset = vec2(o*cos(o+TIME)*0.6, o*sin(o+TIME)*0.6);
		vec4 res = pattern(pos/vec2(i*i/150.)+offset, i/2.+TIME);
		if(res.a > 0.0)
		     color = res*i/7.;
	}

	gl_FragColor = color;
}

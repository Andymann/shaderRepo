/*
{
  "CATEGORIES" : [
    "XXX"
  ],
  "DESCRIPTION" : "",
  "ISFVSN" : "2",
  "INPUTS" : [
    {
      "NAME" : "TravelSpeed",
      "TYPE" : "float",
      "MAX" : 122,
      "DEFAULT" : 22,
      "LABEL" : "TravelSpeed",
      "MIN" : 0
    }
  ],
  "CREDIT" : ""
}
*/


#ifdef GL_ES
precision mediump float;
#endif


void main()
{
	float t=.001;
	vec3 v = vec3(t);
	vec3 pos=vec3(-RENDERSIZE.x/2.0,-RENDERSIZE.y/2.0,0);
	
	float a1 = 0.6 + TIME*.3;
	mat2 rot1 = mat2(cos(a1),-sin(a1),sin(a1),cos(a1));
	//pos.yx *= rot1;
	
	for (float s=0.; s<1.; s+=.015) {
		vec3 p=s*(gl_FragCoord.xyz+pos)*t+vec3(.1,.4,fract(0.15*s+(TIME*TravelSpeed)*.005));
		p.xy *= rot1;
		for (int i=0; i<6; i++) p=abs(p)/dot(p,p)-(0.8);
		v+=4.9*dot(p,p)*t*vec3(s,s*s,4.*s*s*s*s*s*s);
	}
	gl_FragColor=vec4(v.rgb, 1.0);
}
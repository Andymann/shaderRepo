/*
{
  "CATEGORIES" : [
    "Automatically Converted",
    "GLSLSandbox"
  ],
  "INPUTS" : [

  ],
  "DESCRIPTION" : "Automatically converted from http:\/\/glslsandbox.com\/e#47767.3"
}
*/


// Fixed shadows and ambient occlusion bugs, and sped some shit up.
// Still needs some work
// Voltage / Defame (I just fixed some bugs, someone else did they main work on this)
// rotwang: @mod* tinted shadow
// modded by @dennishjorth. A few opts, just with the blue color, metafied blobby toruses are pretty neat.
// changed some minor stuff, more summerful color and stuff.

#ifdef GL_ES
precision highp float;
#endif

//Simple raymarching sandbox with camera

//Raymarching Distance Fields
//About http://www.iquilezles.org/www/articles/raymarchingdf/raymarchingdf.htm
//Also known as Sphere Tracing
//Original seen here: http://twitter.com/#!/paulofalcao/statuses/134807547860353024

//Scene Start

//Torus
float torus(in vec3 p2, in vec2 t, float offset, float modder){
	vec3 p = vec3(
		(sin(offset+TIME*modder*0.14+0.5+sin(p2.y*p2.y*0.3+p2.z*p2.z*0.3+2.0+TIME*modder*0.14))*0.4+1.0)*p2.x,
		(sin(offset+TIME*modder*0.15+1.0+sin(p2.x*p2.x*0.3+p2.z*p2.z*0.3+1.5+TIME*modder*0.15))*0.4+1.0)*p2.y,
		(sin(offset+TIME*modder*0.13+1.5+sin(p2.x*p2.x*0.3+p2.y*p2.y*0.3+0.5+TIME*modder*0.12))*0.4+1.0)*p2.z
		);	
	vec2 q = vec2(length(vec2(p.x,p.z))-t.x, p.y);
	return length(q) - t.y;
}

//Objects union
vec2 inObj(in vec3 p){
    const float modder = 0.02;
    float phase1 = sin(TIME*0.1+sin(TIME*0.05)*6.28)*3.14;
    float cos1x = cos(TIME*modder*2.0+phase1);
    float sin1x = sin(TIME*modder*2.0+phase1);
    float cos2x = cos(TIME*modder*3.0+phase1);
    float sin2x = sin(TIME*modder*3.0+phase1);
    float cos3x = cos(TIME*modder*5.5+phase1);
    float sin3x = sin(TIME*modder*5.5+phase1);
    float cos4x = cos(TIME*modder*4.5+phase1);
    float sin4x = sin(TIME*modder*4.5+phase1);

    vec3 p3 = vec3(p.x*cos1x+p.z*sin1x,
	    p.y,
	    p.x*sin1x-p.z*cos1x);

    vec3 p4 = vec3(p.x*cos3x+p.z*sin3x,
	    p.y,
	    p.x*sin3x-p.z*cos3x);

   vec3 p5 = vec3(p4.x,
	    p4.y*cos4x+p4.z*sin4x,
	    p4.y*sin4x-p4.z*cos4x);
	
   vec3 p6 = vec3(p3.x,
	    p3.y*cos2x+p3.z*sin2x,
	    p3.y*sin2x-p3.z*cos2x);

    float b1 = torus(p5+vec3(cos(TIME*modder*0.37)*3.33,sin(TIME*modder*0.69)*0.33,cos(TIME*modder*0.79)*0.33),vec2(3.0,1.0),0.5,modder);
    float b2 = torus(p3+vec3(sin(TIME*modder*0.57)*3.33,cos(TIME*modder*0.74)*0.33,cos(TIME*modder*0.64)*0.33),vec2(3.0,1.0),1.0,modder);
    float b6 = torus(p6+vec3(sin(TIME*modder*0.47)*3.33,cos(TIME*modder*0.94)*0.33,cos(TIME*modder*0.84)*0.33),vec2(3.0,1.0),1.5,modder);
    const float e = 0.1;
    const float r = 2.0;
    float b = 1.0/(b1+1.0+e)+1.0/(b2+1.0+e)+1.0/(b6+1.0+e);
    vec2 dist = vec2(1.0/b-0.7,1);
    return dist;
}

//Scene End

void main(void){
  //Camera animation
  vec3 U=vec3(0,1,0);//Camera Up Vector
  vec3 viewDir=vec3(0,0,0); //Change camere view vector here
  vec3 E=vec3(-sin(TIME*0.2)*8.0,4,cos(TIME*0.2)*8.0); //Camera location; Change camera path position here
	
  //Camera setup
  vec3 C=normalize(viewDir-E);
  vec3 A=cross(C, U);
  vec3 B=cross(A, C);
  vec3 M=(E+C);
  
  vec2 vPos=2.0*gl_FragCoord.xy/RENDERSIZE.xy - 1.0; // (2*Sx-1) where Sx = x in screen space (between 0 and 1)
  vec3 scrCoord=M + vPos.x*A*RENDERSIZE.x/RENDERSIZE.y + vPos.y*B; //normalize RENDERSIZE in either x or y direction (ie RENDERSIZE.x/RENDERSIZE.y)
  vec3 scp=normalize(scrCoord-E);

  //Raymarching
  const vec3 e=vec3(0.001,0,0);
  const float MAX_DEPTH=25.0; //Max depth

  vec2 s=vec2(0.1,0.0);
  vec3 c,p,n,m,n1;
  vec2 l=vec2(1.0,0.0);	
	
  float f=1.0,g=1.0;
  for(int i=0;i<128;++i){
    if (abs(s.x)<.01||f>MAX_DEPTH) break;
    f+=s.x;
    g+=l.x;
    p=E+scp*f;
    m=E+scp*g;
    s=inObj(p);
  }	
  n=normalize(
      vec3(s.x-inObj(p-e.xyy).x,
           s.x-inObj(p-e.yxy).x,
           s.x-inObj(p-e.yyx).x));

  if (f<MAX_DEPTH){
    c=vec3(0.4,0.8,0.6);
	  c.y = c.y*sin(f+cos(f+TIME*1.2)+TIME*1.1)*0.4;
	  c.z += cos(f+sin(f+TIME*2.1)+TIME*3.3)*0.2;
	  c.y += 0.2+c.z*1.0;
	  c.x = c.y*1.0;
	  c.y += 0.2;
    float b=max(dot(n,normalize(E-p)),0.1);
    gl_FragColor=vec4((b*c+pow(b,250.0))*(1.0-f*.001),1.0);//simple phong LightPosition=CameraPosition
  }
  else gl_FragColor=vec4(0,0,0.0,1); //background color
}
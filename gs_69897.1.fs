/*
{
  "CATEGORIES" : [
    "Automatically Converted",
    "GLSLSandbox"
  ],
  "INPUTS" : [

  ],
  "DESCRIPTION" : "Automatically converted from http:\/\/glslsandbox.com\/e#69897.1"
}
*/


//https://www.shadertoy.com/view/ldfGWn
//precision mediump float;


float rand(vec3 r) { return fract(sin(dot(r.xy,vec2(1.38984*sin(r.z),1.13233*cos(r.z))))*653758.5453); }

#define Iterations 25
#define Thickness 0.13
#define SuperQuadPower 2.0
#define Fisheye 0.0

float truchetarc(vec3 pos)
{
	float r=length(pos.xy);
	return pow(pow(abs(r-0.5),SuperQuadPower)+pow(abs(pos.z-0.5),SuperQuadPower),1.0/SuperQuadPower)-Thickness;
}

float truchetcell(vec3 pos)
{
	return min(min(
	truchetarc(pos),
	truchetarc(vec3(pos.z,1.0-pos.x,pos.y))),
	truchetarc(vec3(1.0-pos.y,1.0-pos.z,pos.x)));
}

float distfunc(vec3 pos)
{
	vec3 cellpos=fract(pos);
	vec3 gridpos=floor(pos);

	float rnd=rand(gridpos);

	if(rnd<1.0/8.0) return truchetcell(vec3(cellpos.x,cellpos.y,cellpos.z));
	else if(rnd<2.0/8.0) return truchetcell(vec3(cellpos.x,1.0-cellpos.y,cellpos.z));
	else if(rnd<3.0/8.0) return truchetcell(vec3(1.0-cellpos.x,cellpos.y,cellpos.z));
	else if(rnd<4.0/8.0) return truchetcell(vec3(1.0-cellpos.x,1.0-cellpos.y,cellpos.z));
	else if(rnd<5.0/8.0) return truchetcell(vec3(cellpos.y,cellpos.x,1.0-cellpos.z));
	else if(rnd<6.0/8.0) return truchetcell(vec3(cellpos.y,1.0-cellpos.x,1.0-cellpos.z));
	else if(rnd<7.0/8.0) return truchetcell(vec3(1.0-cellpos.y,cellpos.x,1.0-cellpos.z));
	else  return truchetcell(vec3(1.0-cellpos.y,1.0-cellpos.x,1.0-cellpos.z));
}


void mainVR( out vec4 fragColor, in vec2 fragCoord, in vec3 fragRayOri, in vec3 fragRayDir )
{
    vec3 ray_dir=fragRayDir;
	vec3 ray_pos=fragRayOri;

	float i=float(Iterations);
	for(int j=0;j<Iterations;j++)
	{
		float dist=distfunc(ray_pos);
		ray_pos+=dist*ray_dir;

		if(abs(dist)<0.001) { i=float(j); break; }
	}

	float ao=1.0-i/float(Iterations);
	float light=ao*1.4;

	vec3 col=(cos(ray_pos/2.0)+2.0)/3.0;

	fragColor=vec4(col*light+0.1,1.0);
}

void mainImage( out vec4 fragColor, in vec2 fragCoord )
{
	const float pi=3.141592;

	vec2 coords=(2.0*fragCoord.xy-RENDERSIZE.xy)/length(RENDERSIZE.xy);

	float a=TIME/3.0;
	mat3 m=mat3(
	0.0,1.0,0.0,
	-sin(a),0.0,cos(a),
	cos(a),0.0,sin(a));
	m*=m;
	m*=m;

	vec3 ray_dir=m*normalize(vec3(1.4*coords,-1.0));

	float t=TIME/3.0;
	vec3 ray_pos=vec3(
    2.0*(sin(t+sin(2.0*t)/2.0)/2.0+0.5),
    2.0*(sin(t-sin(2.0*t)/2.0-pi/2.0)/2.0+0.5),
    2.0*((-2.0*(t-sin(4.0*t)/4.0)/pi)+0.5+0.5));

    mainVR(fragColor,fragCoord,ray_pos,ray_dir);

   	float vignette=pow(1.0-length(coords),0.3);
	fragColor.xyz*=vec3(vignette);
}

void main( void ) {


	mainImage(gl_FragColor, gl_FragCoord.xy);
}
/*
{
  "CATEGORIES" : [
    "Automatically Converted",
    "GLSLSandbox"
  ],
  "DESCRIPTION" : "Automatically converted from http:\/\/glslsandbox.com\/e#66697.3",
  "INPUTS" : [

  ],
  "PERSISTENT_BUFFERS" : [
    "backbuffer"
  ],
  "PASSES" : [
    {
      "TARGET" : "backbuffer",
      "PERSISTENT" : true
    }
  ]
}
*/



#ifdef GL_ES
precision mediump float;
#endif


vec2 wg = vec2(RENDERSIZE.x/RENDERSIZE.y,1.0);

//SNOW_FALL_P + SNOW_SPLAT_P + SNOW_AFK_P = SNOW_P
#define SNOW_FALL_R (0.65)
vec2 hash21(float p)
{
	vec3 p3 = fract(vec3(p) * vec3(.1031, .1030, .0973));
	p3 += dot(p3, p3.yzx + 33.33);
    return fract((p3.xx+p3.yz)*p3.zy);

}

vec4 hash41(float p)
{
	vec4 p4 = fract(vec4(p) * vec4(.1031, .1030, .0973, .1099));
    p4 += dot(p4, p4.wzxy+33.33);
    return fract((p4.xxyz+p4.yzzw)*p4.zywx);
    
}
float hash11(float p)
{
    p = fract(p * .1031);
    p *= p + 33.33;
    p *= p + p;
    return fract(p);
}


vec2 toPolar(vec2 v)
{
vec2 po;
	po.x = length(v);
	po.y = atan(v.y,v.x);
	return po;
}


float z(float r)
{
return 1.0 + 60.0*r;
}


float sFlake(vec2 uv,float id)
{
	vec2 temp = hash21(id*548.2);
	float sp = temp.x*6.0+1.5;
	float it = temp.y*sp;
	float sf = sp * SNOW_FALL_R;//period of snow falling
	//rest is void
	
	float k = (TIME-it)/sp;
	float itp = it + floor(k)*sp;
	float rft = (TIME-itp)/sf;
	float rst = (TIME-itp-sf)/(sp-sf);
	vec2 ip = (hash21(itp*424.74)*2.0-1.0)*wg;
	float zf = z(1.0);
	vec2 p = ip*atan(1.0/z(rft))/atan(1.0/zf);
	vec4 params = hash41(itp+1.4);
	params.x = 1.0 + 1.*params.x;
	params.y = sqrt(1.0 + 50.0*params.y);
	params.z *=3.1415*2.0;
	params.w = 9.0 + floor(21.0*params.w);
	float l = params.x/z(rft);
	if (rft<=1.0)
	{
		vec2 v = uv-p;
		//return smoothstep(0.0,1.0,(l2-dot(v,v))/l2);
		return max(0.0,1.0-length(v)/l)*1.2;
	}
	else
	{
		vec2 v = toPolar(uv-ip);
		float lf = params.x/zf;
		float fe = (1.0+params.y*pow(rst,0.5));
		float pg = 2.0+1.*cos(v.y*params.w+params.z+(hash11(v.x*500.0)-0.5)*1.4);
		return (1.0-rst*rst)*max(0.0,1.0-pow(abs(v.x-(fe-1.0)*0.1)/(lf*fe),1.4))*pg*0.33*0.4;

	}
	
	
	
}
void main(void){
	vec2 uv=(gl_FragCoord.xy*2.-RENDERSIZE.xy)/min(RENDERSIZE.x,RENDERSIZE.y); 
	
	vec3 c = vec3(0.0);
	for(float i=0.0;i<30.0;i++)
		c += mix(vec3(1.0,0.0,0.0),vec3(0.0,0.0,1.0),step(0.5,hash11(i*465.74)))*sFlake(uv,i)*0.5;
	

	c.xyz = c.xyz + IMG_NORM_PIXEL(backbuffer,mod(( (gl_FragCoord.xy+vec2(0.0,-1.0)) / RENDERSIZE.xy ),1.0)).xyz*0.9;
	gl_FragColor = vec4(c,1.0);
}
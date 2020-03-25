/*
{
  "CATEGORIES" : [
    "Automatically Converted",
    "GLSLSandbox"
  ],
  "INPUTS" : [

  ],
  "DESCRIPTION" : "Automatically converted from http:\/\/glslsandbox.com\/e#28897.2"
}
*/


#ifdef GL_ES
precision mediump float;
#endif

// rakesh@picovico.com : www.picovico.com

const float fRadius = 0.15;

void main(void)
{
    vec2 uv = -1.0 + 2.0*gl_FragCoord.xy / RENDERSIZE.xy;
    uv.x *=  RENDERSIZE.x / RENDERSIZE.y;
    
    vec3 color = vec3(0.0);

        // bubbles
    for( int i=0; i<32; i++ )
    {
            // bubble seeds
        float pha = tan(float(i)*6.+1.0)*0.5 + 0.5;
        float siz = pow( cos(float(i)*2.4+5.0)*0.5 + 0.5, 4.0 );
        float pox = cos(float(i)*3.55+4.1) * RENDERSIZE.x / RENDERSIZE.y;
        
            // buble size, position and color
        float rad = fRadius + sin(float(i))*0.09+0.05;
        vec2  pos = vec2( pox+sin(TIME/30.+pha+siz), -1.0-rad + (2.0+2.0*rad)
                         *mod(pha+0.1*(TIME/5.)*(0.2+0.8*siz),1.0)) * vec2(1.0, 1.0);
        float dis = length( uv - pos );
        vec3  col = mix( vec3(0.3, 0.8, 0.8), vec3(0.0,0.4,0.8), 0.5+0.5*sin(float(i)*sin(TIME*pox*0.03)+1.9));
        
            // render
        color += col.xyz *(1.- smoothstep( rad*(0.65+0.20*sin(pox*TIME)), rad, dis )) * (1.0 - cos(pox*TIME))*float(0.5);
    }

	    for( int i=0; i<8; i++ )
    {
            // bubble seeds
        float pha = tan(float(i)*7.+1.0)*0.4 + 0.6;
        float siz = pow( cos(float(i)*2.4+5.0)*0.5 + 0.5, 4.0 );
        float pox = cos(float(i)*3.55+4.1) * RENDERSIZE.x / RENDERSIZE.y;
        
            // buble size, position and color
        float rad = fRadius + sin(float(i+32))*0.2+0.1;
        vec2  pos = vec2( pox+sin(TIME/80.+pha+siz), -1.0-rad + (2.0+2.0*rad)
                         *mod(pha+0.1*(TIME/5.)*(0.2+0.8*siz),1.0)) * vec2(1.0, 1.0);
        float dis = length( uv - pos );
        vec3  col = mix( vec3(0.1, 0.8, 0.8), vec3(0.0,0.4,0.8), 0.5+0.5*sin(float(i)*sin(TIME*pox*0.03)+1.9))*float(0.5);
        
            // render
        color += col.xyz *(1.- smoothstep( rad*(0.65+0.20*sin(pox*TIME)), rad, dis )) * (1.0 - cos(pox*TIME))   ;
	    
    }
		    	vec2 sp = vv_FragNormCoord;//vec2(.4, .7);
	vec2 p = sp*6.0 - vec2(125.0);
	vec2 i1 = p;
	float c = 1.0;
	
	float inten = 0.01;

	for (int n = 0; n < 3; n++) 
	{
		float t = TIME/10.0* (1.0 - (3.0 / float(n+1)));
		i1 = p + vec2(cos(t - i1.x) + sin(t + i1.y), sin(t - i1.y) + cos(t + i1.x));
		c += 1.0/length(vec2(p.x / (sin(i1.x+t)/inten),p.y / (cos(i1.y+t)/inten)));
	}
	c /= float(3);
	c = 1.5-sqrt(c);
	color += vec3((vec4(vec3(c*c*c*c), 999.0) + vec4(0.0, 0.2, 0.5, 4.0)))*0.0;
    gl_FragColor = vec4(color,1.0)+vec4(0.0,0.1,0.3,1.0);
}


/*
{
  "IMPORTED" : [
    {
      "NAME" : "iChannel0",
      "PATH" : "3871e838723dd6b166e490664eead8ec60aedd6b8d95bc8e2fe3f882f0fd90f0.jpg"
    },
    {
      "NAME" : "iChannel1",
      "PATH" : "ad56fba948dfba9ae698198c109e71f118a54d209c0ea50d77ea546abad89c57.png"
    }
  ],
  "CATEGORIES" : [
    "Automatically Converted",
    "Shadertoy"
  ],
  "DESCRIPTION" : "Automatically converted from https:\/\/www.shadertoy.com\/view\/3s2GWK by Emil.  Projected planes and some experimentation regarding multiple samples at different height",
  "INPUTS" : [

  ]
}
*/


void main() {



    vec3 viewDir = normalize(vec3((gl_FragCoord.xy-RENDERSIZE.xy*0.5)/RENDERSIZE.y, 0.65+sin(TIME*0.2)*0.2));
    viewDir = mix(cross(viewDir, vec3(sin(TIME*0.6)*0.6,cos(TIME*0.15)*0.2,sin(TIME*0.2)*0.3)), viewDir, 4.6+sin(TIME*0.14));
	vec3 camRay;
    vec4 groundPlane = vec4(0.0);
    
    float oldLength = 0.0;
    float absviewDirY = abs(viewDir.y);
    for(float i = -0.4; i < 1.0; i+=0.01){
		camRay = viewDir/(absviewDirY+float(i)*0.4*absviewDirY);
        
        vec4 newSample = IMG_NORM_PIXEL(iChannel0,mod((camRay.xz + vec2(0.4, TIME))*0.01,1.0));
        float heightSample = IMG_NORM_PIXEL(iChannel1,mod((camRay.xz + vec2(0.4, TIME))*0.1,1.0)).r;
        float heightSample_o = IMG_NORM_PIXEL(iChannel1,mod((camRay.xz + vec2(0.4, TIME))*0.1 + vec2(0.0, 0.01),1.0)).r;
        float zDelta = heightSample - heightSample_o;
        if(smoothstep(i, 1.0, heightSample+0.3)>0.45){
            float newLength = 1.0/length(camRay);
            if(newLength>oldLength){
                oldLength = newLength;
                groundPlane = newSample*(i*1.5+1.0);
                groundPlane *= vec4(0.8)-vec4(zDelta*-4.0+0.9)*(newLength+0.2)+smoothstep(0.5,0.0,absviewDirY);
                
            }
        }
    }
    
    gl_FragColor = groundPlane+vec4(0.1,0.3,0.5,.0)*oldLength;
}

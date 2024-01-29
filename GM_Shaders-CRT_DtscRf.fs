/*
{
    "CATEGORIES": [
        "Automatically Converted",
        "Shadertoy"
    ],
    "DESCRIPTION": "Automatically converted from https://www.shadertoy.com/view/DtscRf by Xor.  https://mini.gmshaders.com/p/gm-shaders-mini-crt",
    "IMPORTED": {
    },
    "INPUTS": [
    ],
    "PASSES": [
        {
            "FLOAT": true,
            "PERSISTENT": true,
            "TARGET": "BufferA"
        },
        {
            "FLOAT": true,
            "PERSISTENT": true,
            "TARGET": "BufferB"
        },
        {
        }
    ]
}

*/


/*
    Color pass
    
    Based on "Cosmic":
    https://www.shadertoy.com/view/msjXRK
*/
/*
    CRT pass
*/

//RGB Mask intensity(0 to 1)
#define MASK_INTENSITY 1.0
//Mask size (in pixels)
#define MASK_SIZE 12.0
//Border intensity (0 to 1)
#define MASK_BORDER 0.8

//Chromatic abberration offset in texels (0 = no aberration)
#define ABERRATION_OFFSET vec2(2,0)

//Curvature intensity
#define SCREEN_CURVATURE 0.08
//Screen vignette
#define SCREEN_VIGNETTE 0.4

//Intensity of pulsing animation
#define PULSE_INTENSITY 0.03
//Pulse width in pixels (times tau)
#define PULSE_WIDTH 6e1
//Pulse animation speed
#define PULSE_RATE 2e1

/*
    Bloom pass
    
    "GM Shaders: CRT" by @XorDev
    
    How to create a simple CRT effect including:
    
    -RGB pixel cells
    -Chromatic aberration
    -Screen curvature
    -Vignette
    -Pulsing
    -Bloom pass
    
    Based on my 1 pass blur:
    https://github.com/XorDev/1PassBlur
*/

//Bloom radius in pixels
#define BLOOM_RADIUS 16.0
//Bloom texture samples
#define BLOOM_SAMPLES 32.0
//Bloom base brightness
#define BLOOM_BASE 0.5
//Bloom glow brightness
#define BLOOM_GLOW 3.0

void main() {
	if (PASSINDEX == 0)	{


	    //Clear fragcolor (hacky)
	    gl_FragColor *= 0.;
	    //Initialize resolution for scaling
	    vec2 r=RENDERSIZE.xy,
	    //Save centered pixel coordinates
	    p = (gl_FragCoord.xy-r*vec2(.53,.58))*mat2(1,-1,2,2);
	    
	    //Initialize loop iterator and arc angle
	    for(float i=0.,a;
	        //Loop 300 times
	        i++<1e1;
	        //Add with ring attenuation
	        gl_FragColor += 1. / (abs(length(gl_FragCoord.xy=p/(r+r-p).y)*3e1-i)+4e1/r.y)*
	        //Limit to arcs
	        clamp(cos(a=atan(gl_FragCoord.y,gl_FragCoord.x)*ceil(i*.2)+TIME*sin(i*i)+i*i),.0,.1)*
	        //Give them color
	        (cos(a-i+vec4(0,2,3,0))+1.));
	    
	    //Range fix
	    gl_FragColor = clamp(gl_FragColor,0.,1.);
	}
	else if (PASSINDEX == 1)	{


	    //Resolution
		vec2 res = RENDERSIZE.xy;
	    //Signed uv coordinates (ranging from -1 to +1)
		vec2 uv = gl_FragCoord.xy/res * 2.0 - 1.0;
	    //Scale inward using the square of the distance
		uv *= 1.0 + (dot(uv,uv) - 1.0) * SCREEN_CURVATURE;
	    //Convert back to pixel coordinates
		vec2 pixel = (uv*0.5+0.5)*res;
	    
	    //Square distance to the edge
	    vec2 edge = max(1.0 - uv*uv, 0.0);
	    //Compute vignette from x/y edges
	    float vignette = pow(edge.x * edge.y, SCREEN_VIGNETTE);
		
	    //RGB cell and subcell coordinates
	    vec2 coord = pixel / MASK_SIZE;
	    vec2 subcoord = coord * vec2(3,1);
	    //Offset for staggering every other cell
		vec2 cell_offset = vec2(0, fract(floor(coord.x)*0.5));
	    
	    //Pixel coordinates rounded to the nearest cell
	    vec2 mask_coord = floor(coord+cell_offset) * MASK_SIZE;
	    
	    //Chromatic aberration
		vec4 aberration = IMG_NORM_PIXEL(BufferA,mod((mask_coord-ABERRATION_OFFSET) / res,1.0));
	    //Color shift the green channel
		aberration.g = IMG_NORM_PIXEL(BufferA,mod((mask_coord+ABERRATION_OFFSET) / res,1.0)).g;
	   
	    //Output color with chromatic aberration
		vec4 color = aberration;
	    
	    //Compute the RGB color index from 0 to 2
	    float ind = mod(floor(subcoord.x), 3.0);
	    //Convert that value to an RGB color (multiplied to maintain brightness)
	    vec3 mask_color = vec3(ind == 0.0, ind == 1.0, ind == 2.0) * 3.0;
	    
	    //Signed subcell uvs (ranging from -1 to +1)
	    vec2 cell_uv = fract(subcoord + cell_offset) * 2.0 - 1.0;
	    //X and y borders
	    vec2 border = 1.0 - cell_uv * cell_uv * MASK_BORDER;
	    //Blend x and y mask borders
	    mask_color.rgb *= border.x * border.y;
	    //Blend with color mask
		color.rgb *= 1.0 + (mask_color - 1.0) * MASK_INTENSITY;  
	    
	    //Apply vignette
	    color.rgb *= vignette;
	    //Apply pulsing glow
		color.rgb *= 1.0+PULSE_INTENSITY*cos(pixel.x/PULSE_WIDTH+TIME*PULSE_RATE);
	    
	    gl_FragColor = color;
	}
	else if (PASSINDEX == 2)	{


	    //Resolution and texel size
	    vec2 res = RENDERSIZE.xy;
	    vec2 texel = 1.0 / res;
	    
	    //Bloom total
	   	vec4 bloom = vec4(0);
	    //Sample point
	    vec2 point = vec2(BLOOM_RADIUS, 0)*inversesqrt(BLOOM_SAMPLES);
	    for(float i = 0.0; i<BLOOM_SAMPLES; i++)
	    {
	        //Rotate by golden angle
	        point *= -mat2(0.7374, 0.6755, -0.6755, 0.7374);
	        //Compute sample coordinates from rotated sample point
	        vec2 coord = (gl_FragCoord.xy + point*sqrt(i)) * texel;
	        //Add bloom samples
	        bloom += IMG_NORM_PIXEL(BufferB,mod(coord,1.0)) * (1.0 - i/BLOOM_SAMPLES);
	    }
	    //Compute bloom average
	    bloom *= BLOOM_GLOW/BLOOM_SAMPLES;
	    //Add base sample
	    bloom += IMG_NORM_PIXEL(BufferB,mod(gl_FragCoord.xy*texel,1.0))*BLOOM_BASE;
	    gl_FragColor = bloom;
	}

}

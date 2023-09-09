/*{
    "CATEGORIES": [
        "Automatically Converted",
        "GLSLSandbox"
    ],
    "DESCRIPTION": "Automatically converted from http://glslsandbox.com/e#49132.0",
    "ISFVSN": "2"
}
*/


#ifdef GL_ES
precision mediump float;
#endif

//uniform vec4 mouse;


// Raymarching Template
// Source - Raymarching.com
// Author - Gary "Shane" Warne
// eMail - mail@Raymarching.com, mail@Labyrinth.com
// Last update: 28th Aug, 2014
//
// Raymarching a field of boxes. There's some mediocre camera work in there, and the scene function is kind of worth looking at, especially if you're 
// not really familiar with the object repetition trick (for want of a better description) that people like to use. The equally-mediocre lighting code 
// has been give its own function in preperation for reflection code, which I'll include next. Shadows will be in there too.
//
// All of the code on this page has been written a gazillion times by a zillion different people. There is nothing new here. Screen coordinates, 
// camera setup, ray setup, raymarch, lighting, screen render, and done. Use it for whatever you purpose you like.
//
// Anyway, I'm genuinely grateful for all the code various people have made public over at "Shadertoy.com," which is run by that RGBA guy, Inigo Quilez,
// aka IQ. "Shader Toy" is an apt description for that site, because as a person who loves graphic-related snippets, I'm like a kid in a toy store
// over there. I guess you needed to have been on the net 15, or even 10, years ago trying to hunt down demo effects code to really appreciate what's 
// being offered at "ShaderToy.com." Fantastic stuff.

#define PI 3.1415926535898 // Always handy.

// Epsilon value..
const float eps = 0.005;

// Gloable variables for the raymarching algorithm.
const int maxIterations = 92;
const float stepScale = .5;
const float stopThreshold = 0.005; // I'm not quite sure why, but thresholds in the order of a pixel seem to work better for me... most times. 

// Distance field equation for a sphere.
float sphere(in vec3 p, in vec3 centerPos, float radius){
	
	return length(p-centerPos) - radius;
}

// Distance field equation for a cube.
float box(vec3 p, vec3 b){ 
    
	vec3 d = abs(p) - b;

	return min(max(d.x,max(d.y,d.z)),0.0) + length(max(d,0.0));
}

// Distance field equation for a rounded cube.
float roundedCube(vec3 p, vec3 boxExtents, float edgeRadius ){
    
	return length(max(abs(p)-boxExtents + vec3(edgeRadius), 0.0 )) - edgeRadius;
}

// Sinusoidal plasma. 
float sinusoidalPlasma(in vec3 p){

    return sin(p.x+TIME*2.)*cos(p.y+TIME*2.1)*sin(p.z+TIME*2.3) + 0.25*sin(p.x*2.)*cos(p.y*2.)*sin(p.z*2.);
}
    
// This is the raymarched scene that gets called multiple times from the raymarching procedure, which means adding detail can slow things right down.
float scene(in vec3 p) {

    // The following is probably one of my favorite raymarching moves. This tiny snippet is responsibe for the infinite object repetition that
    // you see in so many raymarched scenes. There are a lot of different ways to look at it, but in a way, you're voxelizing the entire 3D space
    // into cubes of one unit dimension, then placing a copy of your object (in this case, a box) right in the center of it. Obviously, the 
    // object you place in it has to fit, and you'll see that the box below does. By the way, no one's forcing you to voxelize to 1 unit dimension.
    // For instance, "p = mod(p, 2.0) - 1.0" would work also, but your boxes would be twice as far apart, and appear darker, since your light would
    // now be further away. Try it and see.
    p = mod(p, 1.0) - 0.5;
    
    // In addition to the box field equation, I've also provided the distance functions for a sphere and rounded cube. I'll assume these particular 
    // ones originated at "iquilezles.org," although anyone who's ever implemented the marching cubes algorithm would be familiar with them.
    
    //float d0 = roundedCube(p, vec3(0.2,0.15,0.25), 0.05); // Smaller edge radius mean boxier look - (0.->box, outer-extremites->more spherical).
    //float d1 = sphere(p, vec3(0.,0.,0.), 0.25); // Sphere. Boring, yet cool at the same TIME.
    float d0 = box(p, vec3(0.2, 0.15, 0.25)); // Box. As above.

    
    // You'll note that I've commented out a term that gives the boxes some sinusoidal movement. It's kind of cool, but also detracts a little from 
    // the look. Sometimes, less is more, as they say.
	return d0;// + 0.03*sinusoidalPlasma(p*8.0);//+ .01-0.02*sinusoidalPlasma(p*2.0);
}

// Obtain the surface normal at the surface point "p."
vec3 getNormal(in vec3 p) {
	
	return normalize(vec3(
		scene(vec3(p.x+eps,p.y,p.z))-scene(vec3(p.x-eps,p.y,p.z)),
		scene(vec3(p.x,p.y+eps,p.z))-scene(vec3(p.x,p.y-eps,p.z)),
		scene(vec3(p.x,p.y,p.z+eps))-scene(vec3(p.x,p.y,p.z-eps))
	));

    /*
    // Shorthand version of the above. The fewer characters used almost gives the impression that it involves fewer calculations. Almost.
	vec2 e = vec2(eps, 0.);
	return normalize(vec3(scene(p+e.xyy)-scene(p-e.xyy), scene(p+e.yxy)-scene(p-e.yxy), scene(p+e.yyx)-scene(p-e.yyx) ));
    */
	
	/*
	// The tetrahedral version, which does involve fewer calculations, but doesn't seem as accurate on some surfaces... I could be wrong,
	// but that's the impression I get.
	vec2 e = vec2(-0.5*eps,0.5*eps);   
	return normalize(e.yxx*scene(p+e.yxx)+e.xxy*scene(p+e.xxy)+e.xyx*scene(p+e.xyx)+e.yyy*scene(p+e.yyy)); 
	*/
}

// Raymarching.
float rayMarching( vec3 origin, vec3 dir, float start, float end ) {

    float sceneDist = 1e4;
	float rayDepth = start; // Ray depth. "start" is usually zero, but for various reasons, you may wish to start the ray further away from the origin.
	for ( int i = 0; i < maxIterations; i++ ) {
		sceneDist = scene( origin + dir * rayDepth ); // Distance from the point along the ray to the nearest surface point in the scene.

        // Irregularities between browsers have forced me to use this logic. I noticed that Firefox was interpreting two "if" statements inside a loop
        // differently to Chrome, and... 20 years on the web, so I guess I should be used to this kind of thing.
		if (( sceneDist < stopThreshold ) || (rayDepth >= end)) {
		
		    // (rayDepth >= end) - The casted ray has proceeded past the end zone, so it's TIME to return the maximum distance.
		     
		    // (sceneDist < stopThreshold) - The distance is pretty close to zero, which means the point on the ray has effectively come into contact 
		    // with the surface. Therefore, we can return the distance, which can be used to calculate the surface point.
			
			break;
		}
		// We haven't hit anything, so increase the depth by a scaled factor of the minimum scene distance.
		rayDepth += sceneDist * stepScale;

	}

	// I'd normally arrange for the following to be taken care of prior to exiting the loop, but Firefox won't execute anything before
	// the "break" statement. Why? I couldn't say. I'm not even game enough to put more than one return statement.	
	//
	// Normally, you'd just return the rayDepth value only, but for some reason that escapes my sense of logic - and everyone elses, for 
	// that matter, adding the final, infinitessimal scene distance value (sceneDist) seems to reduce a lot of popping artifacts. If 
	// someone could put me out of my misery and prove why I should either leave it there, or get rid of it, it'd be appreciated.
	if ( sceneDist >= stopThreshold ) rayDepth = end;
	else rayDepth += sceneDist;
		
	// We've used up our maximum iterations. Return the maximum distance.
	return rayDepth;
}

// Based on original by IQ - optimized to remove a divide
float calculateAO(vec3 p, vec3 n)
{
   const float AO_SAMPLES = 4.0;
   float r = 0.0;
   float w = 1.0;
   for (float i=1.0; i<=AO_SAMPLES; i++)
   {
      float d0 = i * 0.15; // 1.0/AO_SAMPLES
      r += w * (d0 - scene(p + n * d0));
      w *= 0.5;
   }
   return 1.0-clamp(r,0.0,1.0);
}

vec3 lighting( vec3 sp, vec3 camPos, int reflectionPass){

    // Start with black.
    vec3 sceneColor = vec3(0.0);

    // Object's color. Most boxes are white, but I've interspersed some redish and blueish ones. I'd imagine there'd be much
    // better ways to acheive this, but it gets the job done.
    vec3 voxPos = mod(sp*0.5, 1.0);
    vec3 objColor = vec3(1.0, 1.0, 1.0);
    // if ( (voxPos.x<0.5)&&(voxPos.y>=0.5)&&(voxPos.z<0.5) ) objColor = vec3(1.0,voxPos.z*0.5,0.0);
    // else if ( (voxPos.x>=0.5)&&(voxPos.y<0.5)&&(voxPos.z>=0.5) ) objColor = vec3(0.0,0.5+voxPos.z*0.5,1.0);
    
    // I'll be doing shadows next, but for now, I've arranged for some darkish regions to move about the surface of the 
    // boxes to at least give the mild impression. 
    // float fakeShadowMovement =  sinusoidalPlasma(sp*8.);
    // objColor = clamp(objColor*(0.75-0.25*fakeShadowMovement), 0.0, 1.0);


    // Obtain the surface normal at the scene position "sp."
    vec3 surfNormal = getNormal(sp);

    // Lighting.

    // lp - Light position. Keeping it in the vacinity of the camera, but away from the objects in the scene.
    vec3 lp = vec3(0., 1.0, 0.0+TIME);
    // ld - Light direction.
    vec3 ld = lp-sp;
    // lcolor - Light color.
    vec3 lcolor = vec3(1.,0.97,0.92);
    
     // Light falloff (attenuation).
    float len = length( ld ); // Distance from the light to the surface point.
    ld /= len; // Normalizing the light-to-surface, aka light-direction, vector.
    float lightAtten = min( 1.0 / ( 0.25*len*len ), 1.0 ); // Keeps things between 0 and 1.   

    // Obtain the reflected vector at the scene position "sp."
    vec3 ref = reflect(-ld, surfNormal);
    
    float ao = 1.0;//calculateAO(sp, surfNormal); // Ambient occlusion. For this particular example, I've left it out.

    float ambient = .1; //The object's ambient property.
    float specularPower = 8.0; // The power of the specularity. Higher numbers can give the object a harder, shinier look.
    float diffuse = max( 0.0, dot(surfNormal, ld) ); //The object's diffuse value.
    float specular = max( 0.0, dot( ref, normalize(camPos-sp)) ); //The object's specular value.
    specular = pow(specular, specularPower); // Ramping up the specular value to the specular power for a bit of shininess.
    	
    // Bringing all the lighting components togethr to color the screen pixel.
    sceneColor += (objColor*(diffuse*0.8+ambient)+specular*0.5)*lcolor*lightAtten*ao;
    
    return sceneColor;

}


void main(void) {


    // Setting up our screen coordinates.
    
    vec2 aspect = vec2(RENDERSIZE.x/RENDERSIZE.y, 1.0); //
	vec2 screenCoords = (2.0*gl_FragCoord.xy/RENDERSIZE.xy - 1.0)*aspect;

	
	// Camera Setup.
	
	// Camera movement. Not my best work, and there are better ways to do this, but at least it gives the viewer a bit of a look around the scene. 
	// The main thing to note is that the camera (and position we're looking at) is being moved forward, linearly, along the z-axis. At the same TIME, 
	// the camera is circling the view point at a radius of one unit about the xz-plane. Then, just to confuse everyone even more, I seem to
	// have decided to send both the camera and the look-at positions on different sinusoidal paths along the y-axis... I'm sure I had my reasons. Either way,
	// the scene now has a moving camera.
	vec3 lookAt = vec3(0., 1.*sin(TIME*0.5), TIME);  // This is the point you look towards, or at, if you prefer.
	vec3 camPos = vec3(1.0*sin(TIME*0.5), 0.15*sin(TIME*0.25), 1.0*cos(TIME*0.5)+TIME); // This is the point you look from, or camera you look at the scene through. Whichever way you wish to look at it.

    vec3 forward = normalize(lookAt-camPos); // Forward vector.
    vec3 right = normalize(vec3(forward.z, 0., -forward.x )); // Right vector... or is it left? Either way, so long as the correct-facing up-vector is produced.
    vec3 up = normalize(cross(forward,right)); // Cross product the two vectors above to get the up vector.
     
    // FOV - Field of view.
    float FOV = 0.5;
    
    // ro - Ray origin.
    vec3 ro = camPos; 
    // rd - Ray direction.
    vec3 rd = normalize(forward + FOV*screenCoords.x*right + FOV*screenCoords.y*up);
    

	// The screen's background color.
    vec3 bgcolor = vec3(0.);

	
	// Ray marching.
	const float clipNear = 0.0;
	const float clipFar = 16.0;
	float dist = rayMarching(ro, rd, clipNear, clipFar );
	if ( dist >= clipFar ) {
	    // I prefer to do it this way in order to avoid an if-statement below, but I honestly couldn't say whether it's more
	    // efficient. It feels like it would be. Does that count? :)
	    gl_FragColor = vec4(bgcolor, 1.0);
	    return;
		//discard; // If you want to return without rendering anything, I think.
	}
	
	// sp - Surface position. If we've made it this far, we've hit something.
	vec3 sp = ro + rd*dist;
	
	// Light the pixel that corresponds to the surface position. The last entry indicates that it's not a reflection pass
	// which we're not up to yet.
	vec3 sceneColor = lighting( sp, camPos, 0);

    // Clamping the lit pixel, then put it on the screen.
	gl_FragColor = vec4(clamp(sceneColor, 0.0, 1.0), 1.0);
	
}
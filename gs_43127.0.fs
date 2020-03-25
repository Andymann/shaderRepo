/*
{
  "CATEGORIES" : [
    "Automatically Converted",
    "GLSLSandbox"
  ],
  "INPUTS" : [

  ],
  "DESCRIPTION" : "Automatically converted from http:\/\/glslsandbox.com\/e#43127.0"
}
*/


#ifdef GL_ES
precision mediump float;
#endif

#extension GL_OES_standard_derivatives : enable


mat3 rotationMatrix(vec3 axis, float angle)
{
    axis = normalize(axis);
    float s = sin(angle);
    float c = cos(angle);
    float oc = 1.0 - c;

    return mat3(oc * axis.x * axis.x + c, oc * axis.x * axis.y - axis.z * s, oc * axis.z * axis.x + axis.y * s,
        oc * axis.x * axis.y + axis.z * s, oc * axis.y * axis.y + c, oc * axis.y * axis.z - axis.x * s,
        oc * axis.z * axis.x - axis.y * s, oc * axis.y * axis.z + axis.x * s, oc * axis.z * axis.z + c);
}
float hash( float n ){
    return fract(sin(n)*758.5453);
}
float noise2d( in vec2 x ){
    vec2 p = floor(x);
    vec2 f = smoothstep(0.0, 1.0, fract(x));
    float n = p.x + p.y*57.0;
    return mix(
	    mix(hash(n+0.0),hash(n+1.0),f.x),
	    mix(hash(n+57.0),hash(n+58.0),f.x),
	    f.y
	   );
}
float noise3d( in vec3 x ){
	vec3 p = floor(x);
    	vec3 f = smoothstep(0.0, 1.0, fract(x));
	float n = p.x + p.y*157.0 + 113.0*p.z;

	return mix(mix(	mix( hash(n+0.0), hash(n+1.0),f.x),
			mix( hash(n+157.0), hash(n+158.0),f.x),f.y),
		   mix(	mix( hash(n+113.0), hash(n+114.0),f.x),
			mix( hash(n+270.0), hash(n+271.0),f.x),f.y),f.z);
}
// YOU ARE WELCOME! 4d NOISE
float noise4d(vec4 x){
	vec4 p=floor(x);
	vec4 f=smoothstep(0.,1.,fract(x));
	float n=p.x+p.y*157.+p.z*113.+p.w*971.;
	return mix(mix(mix(mix(hash(n),hash(n+1.),f.x),mix(hash(n+157.),hash(n+158.),f.x),f.y),
	mix(mix(hash(n+113.),hash(n+114.),f.x),mix(hash(n+270.),hash(n+271.),f.x),f.y),f.z),
	mix(mix(mix(hash(n+971.),hash(n+972.),f.x),mix(hash(n+1128.),hash(n+1129.),f.x),f.y),
	mix(mix(hash(n+1084.),hash(n+1085.),f.x),mix(hash(n+1241.),hash(n+1242.),f.x),f.y),f.z),f.w);
}
float FBM2(vec2 p, int octaves, float dx){
	float a = 0.0;
    	float w = 0.5;
	for(int i=0;i<5;i++){
		a += noise2d(p) * w;
        	w *= 0.5;
		p *= dx;
	}
	return a;
}
float FBM3(vec3 p, int octaves, float dx){
	float a = 0.0;
    	float w = 0.5;
	for(int i=0;i<5;i++){
		a += noise3d(p) * w;
        	w *= 0.5;
		p *= dx;
	}
	return a;
}
void main( void ) {

	vec2 position = vv_FragNormCoord * sin(+TIME*0.1);

	float dim = 1.0 / (length(position) * 7.0);
	
	vec2 dir = normalize(vv_FragNormCoord);
	dir = (rotationMatrix(vec3(0.0, 1.0, 0.0), length(position)+TIME*0.1 * 8.0) * vec3(dir.x, 0.0, dir.y)).xz;
	float yets = smoothstep(0.1, 0.2, dim * FBM2(dir, 5, 2.1));
	float abc1 = smoothstep(0.1, 0.6, yets * dim * FBM2(position * 6.0 - dir + FBM2(position * 5.0 - dir, 5, 3.0), 5, 3.0));
	float abc2 = smoothstep(0.1, 0.6, yets * dim * FBM2(position * 6.0 - dir  - 400.0+ FBM2(position * 5.0 - dir + 100.0, 5, 3.0), 5, 3.0));
	float abc3 = smoothstep(0.1, 0.6, yets * dim * FBM2(position * 6.0 - dir  - 500.0+ FBM2(position * 5.0 - dir - 100.0, 5, 3.0), 5, 3.0));
	float drk = smoothstep(0.2, 0.6, FBM2(position * 16.0  -dir - 100.0+ FBM2(position * 15.0 - 20.0, 5, 3.0), 5, 2.0));
	 
	gl_FragColor = vec4( vec3(abc1, abc2, abc3) * drk, 1.0 );

}
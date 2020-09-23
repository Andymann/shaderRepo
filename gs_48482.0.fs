/*
{
  "CATEGORIES" : [
    "Automatically Converted",
    "GLSLSandbox"
  ],
  "INPUTS" : [

  ],
  "DESCRIPTION" : "Automatically converted from http:\/\/glslsandbox.com\/e#48482.0"
}
*/


#ifdef GL_ES
precision mediump float;
#endif

//#extension GL_OES_standard_derivatives : enable


void main( void ) {

	vec2 p = ( gl_FragCoord.xy / RENDERSIZE.xy ) ;
	vec2 q = p - vec2(0.3, 0.7);
	vec3 col = mix(vec3(1.0, 0.4, 0.1), vec3(1., .8, .3), sqrt(p.y));

	float r = 0.2 + 0.1 * cos( atan(q.y, q.x) * 10. + 20. * q.x + 1.0);
	col *= smoothstep (r, r + 0.01, length( q ));
	r = 0.015;
	r += .002 * cos(TIME + 120.0 * q.y);
	r += exp(-40. * p.y);
	col *= 1.0 - (1.0 - smoothstep ( r, r + 0.002, abs(q.x - 0.2 * sin(2.0 * q.y) ))) * (1.0 - smoothstep(0., 0.1, q.y));
	gl_FragColor = vec4(col, 1.0);
}
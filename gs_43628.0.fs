/*
{
  "CATEGORIES" : [
    "Automatically Converted",
    "GLSLSandbox"
  ],
  "INPUTS" : [

  ],
  "DESCRIPTION" : "Automatically converted from http:\/\/glslsandbox.com\/e#43628.0"
}
*/


//precision mediump;


void main()
{
    float t = TIME;
    vec3 ro = vec3(.0, .0, -22.);
    vec3 rd = normalize(vec3((gl_FragCoord.xy * 2. - RENDERSIZE.xy) / min(RENDERSIZE.x, RENDERSIZE.y), 1.));

    // ---- rotation 
    float  s = sin(t), c = cos(t);
    mat3 r = mat3(1., 0, 0,
                  0, c, -s,
                  0, s, c) * 
             mat3(c, 0, s,
                  0, 1, 0,
		 -s, 0, c);

    // ---- positions 
    vec3 a = vec3(0.001, 0.001, 3.5), b = vec3(1.0);

    // ---- cube length (max(abs(x) - y, 1.) )
    for (int i = 0; i < 50; i++) 
    ro += min(length(cos((ro + a) * r) - b) - 0.5, 0.5) * rd;

    // ---- shading
    gl_FragColor.rgb = (vec3(0.1, 0.35, 0.72) * -ro.z * 0.3);
    gl_FragColor.a = 1.0;
}
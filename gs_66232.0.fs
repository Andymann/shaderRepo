/*
{
  "CATEGORIES" : [
    "Automatically Converted",
    "GLSLSandbox"
  ],
  "INPUTS" : [

  ],
  "DESCRIPTION" : "Automatically converted from http:\/\/glslsandbox.com\/e#66232.0"
}
*/


#define t TIME
#define RENDERSIZE RENDERSIZE

// Author: koo1ant
// IG: @pecsimax
// TW: @koo1ant

//precision highp float;

uniform float u_time;

mat4 rotX(in float angle) {
    return mat4(
        1, 0, 0, 0,
        0, cos(angle), - sin(angle), 0,
        0, sin(angle), cos(angle), 0,
    0, 0, 0, 1);
}

mat4 rotY(in float angle) {
    return mat4(
        cos(angle), 0, sin(angle), 0,
        0, 1.0, 0, 0,
        - sin(angle), 0, cos(angle), 0,
    0, 0, 0, 1);
}

mat4 rotZ(in float angle) {
    return mat4(
        cos(angle), - sin(angle), 0, 0,
        sin(angle), cos(angle), 0, 0,
        0, 0, 1, 0,
    0, 0, 0, 1);
}

vec2 setupSpace(in vec2 f, in vec2 res)
{
    return (f - 0.5 * res.xy) / res.y * 2.0;
}

float sdBox(in vec2 p, in vec2 b)
{
    vec2 d = abs(p) - b;
    return length(max(d, 0.0)) + min(max(d.x, d.y), 0.00);
}

vec4 image()
{
    float ct = t*2.;
    vec2 uv = setupSpace(gl_FragCoord.xy, RENDERSIZE);

    vec4 s = vec4(uv.x, uv.y, 0.00, 0.00);
    vec4 mul = s * rotY((sin(t*0.5)) * 0.2) * rotX((cos(t*1.)) * 0.1);

    float fov = 90.+sin(t)*20.;
    float z = (fov * 0.01) - mul.z;

    uv = vec2(
        uv.x / z,
        uv.y / z
    );


    uv.y += sin(t*0.2)*5.;
    

    float a = atan(uv.y,uv.x);
    float l = length(uv)+ + sdBox(uv, vec2(length(uv)-10.00 + sin(a*10.+t*2.0) * 0.01));
    
    float l2 = length(uv) + + sdBox(uv, vec2(length(uv) - 10.0 + sin(a * 10.0 + t*2.)*0.01));

    vec3 colorA = vec3(0.2941+l*2.00, 0.2275, 0.0392);
    vec3 colorB = vec3(0.1412, 0.1412, 0.1412);

    float lm = l*sin(l*0.15);
    float cn = floor(sin(l2 * 15.0 - t*2.0)) > -1.00 ? 1.00 : 0.01; // Cuts
    float fl = floor(sin(lm * 15.0 + t*1.0)); // Cuts
    float am = a+t*0.05 + sin(lm*5.+t) * fl;
    float fn = sin(floor(am*7.*1.)-t*0.5);
    
    vec3 color = mix(colorA, colorB,fn);
    float sh = 1.0 - length(uv) + 5.36;
    return vec4(clamp(color * cn,0.,1.), 1.0);
}

void main()
{
    gl_FragColor = image();
}
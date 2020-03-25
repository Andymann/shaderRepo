/*
{
  "CATEGORIES" : [
    "Automatically Converted",
    "GLSLSandbox"
  ],
  "INPUTS" : [

  ],
  "DESCRIPTION" : "Automatically converted from http:\/\/glslsandbox.com\/e#45297.0"
}
*/


// Author @patriciogv ( patriciogonzalezvivo.com ) - 2015

#ifdef GL_ES
precision mediump float;
#endif


#define u_resolution RENDERSIZE
#define u_time TIME

#define PI 3.14159265358979323846

vec2 rotate2D(vec2 _st, float _angle){
    _st -= 0.5;
    _st =  mat2(cos(_angle),-sin(_angle),
                sin(_angle),cos(_angle)) * _st;
    _st += 0.5;
    return _st;
}

vec2 tile(vec2 _st, float _zoom){
    _st *= _zoom;
    return fract(_st);
}

float box(vec2 _st, vec2 _size, float _smoothEdges){
    _size = vec2(0.5)-_size*0.5;
    vec2 aa = vec2(_smoothEdges*0.5);
    vec2 uv = smoothstep(_size,_size+aa,_st);
    uv *= smoothstep(_size,_size+aa,vec2(1.0)-_st);
    return uv.x*uv.y;
}

void main(void){
    float sq = 0.0;
    float aspect_ratio = u_resolution.y/u_resolution.x;
    vec2 st = gl_FragCoord.xy/u_resolution.xy;
    vec3 color = vec3(0.0);
    st.y *= aspect_ratio;
    st.y -= (aspect_ratio/2. - .5);

    st = rotate2D(st, u_time/4.);
    float zoom = 5.;
    float px = zoom*2.0/u_resolution.x;
    vec2 n = floor(st*zoom);
    
    // Divide the space in 4
    st = tile(st,zoom);

    sq = box(st, vec2(0.95), px);
    
    // Use a matrix to rotate the space 45 degrees
    st = rotate2D(st,PI*0.25);

    sq *= box(st, vec2(1.14), px);
    
    float sm = 0.2;
    sq += box(st-vec2(0.7, 0.), vec2(sm), px);
    sq += box(st+vec2(0.7, 0.), vec2(sm), px);
    sq += box(st-vec2(0.0, 0.7), vec2(sm), px);
    sq += box(st+vec2(0.0, 0.7), vec2(sm), px);
    color = mix(color, vec3(1.0), sq);
    
    
    color.rg = mix(color.rg, vec2(0.0), (n+.5)/zoom);
    // color = vec3(st,0.0);

    gl_FragColor = vec4(color,1.0);
}
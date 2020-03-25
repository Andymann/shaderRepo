/*
{
  "IMPORTED" : [

  ],
  "CATEGORIES" : [
    "Automatically Converted",
    "Shadertoy"
  ],
  "DESCRIPTION" : "Automatically converted from https:\/\/www.shadertoy.com\/view\/WsjGDc by bignobody.  Watched a Numberphile video about Moiré patterns ( https:\/\/www.youtube.com\/watch?v=QAja2jp1VjE ). Figured it would be fun to play with the idea in shaders.",
  "INPUTS" : [

  ]
}
*/


vec3 cgrid(vec2 dc)
{
 	vec2 sc = vec2(cos(dc.x * 50.0),sin(dc.y * 50.0));
    //float r = sin(sc.x * sc.y + dc.x * dc.y); 
    float r = sin(sc.x * sc.y); 
    float a = cos(TIME * 0.5) - 0.5;
    return vec3(r,r+a,r*a);   
}

void main() {



    // Normalized pixel coordinates (from 0 to 1)
    vec2 uv = gl_FragCoord.xy/RENDERSIZE.xy;
    float aspect = RENDERSIZE.x/RENDERSIZE.y;
    vec2 pos = vec2(uv-vec2(0.5));
    
    pos.x *= aspect;
    float t = TIME * 0.33;
    float s = sin(length(pos));
    mat2 m = mat2(cos(t),-sin(t),sin(t),cos(t));
    mat2 sm = mat2(0.99,0.0,0.0,0.99);
    vec3 col = cgrid(pos);
    pos = pos * m;
    pos = pos * sm;
    
    vec3 col2 = cgrid(pos);
    
    col = mix(col, col2,0.5);
    // Output to screen
    gl_FragColor = vec4(col,1.0);
}

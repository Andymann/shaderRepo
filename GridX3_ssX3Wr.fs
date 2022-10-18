/*
{
    "CATEGORIES": [
        "Automatically Converted",
        "Shadertoy"
    ],
    "DESCRIPTION": "Automatically converted from https://www.shadertoy.com/view/ssX3Wr by Del.  3 simple drop in grid functions - click a grid to show it as full screen\nbaked shader info used here - https://www.youtube.com/watch?v=MpwcOnrWffQ",
    "IMPORTED": {
    },
    "INPUTS": [
        {
            "NAME": "iMouse",
            "TYPE": "point2D"
        }
    ]
}

*/


// Simple Hex, Tri and Square grids (SST)
//
// Feel free to optimize, golf and generally improve them :)
//
// Del - 15/03/2021

// nice hex function from - https://www.shadertoy.com/view/lldfWH
// All 3 Grid functions return the same:
// ret.x  - distance to border
// ret.y  - distance to center
// ret.zw - cell uv
// id - cell coordinates
vec4 HexGrid(vec2 uv, out vec2 id)
{
    uv *= mat2(1.1547,0.0,-0.5773503,1.0);
    vec2 f = fract(uv);
    float triid = 1.0;
	if((f.x+f.y) > 1.0)
    {
        f = 1.0 - f;
     	triid = -1.0;
    }
    vec2 co = step(f.yx,f) * step(1.0-f.x-f.y,max(f.x,f.y));
    id = floor(uv) + (triid < 0.0 ? 1.0 - co : co);
    co = (f - co) * triid * mat2(0.866026,0.0,0.5,1.0);    
    uv = abs(co);
    return vec4(0.5-max(uv.y,abs(dot(vec2(0.866026,0.5),uv))),length(co),co);
}

// Triangle grid using the skewed, split rectangle method (quicker)
vec4 TriGrid(vec2 uv, out vec2 id)
{
    uv *= mat2(1,-1./1.73, 0,2./1.73);
    vec3 g = vec3(uv,1.-uv.x-uv.y);
    vec3 _id = floor(g)+0.5;
    g = fract(g);
    float lg = length(g);
    if (lg>1.)
        g = 1.-g;
    vec3 g2 = abs(2.*fract(g)-1.);                  // distance to borders
    vec2 triuv = (g.xy-ceil(1.-g.z)/3.) * mat2(1,.5, 0,1.73/2.);
    float edge = max(max(g2.x,g2.y),g2.z);
    id = _id.xy;
    //id*= mat2(1,.5, 0,1.73/2.); // Optional, unskew IDs
    //id.xy += sign(lg-1.)*0.1; // Optional tastefully adjust ID's
    return vec4((1.0-edge)*0.43,length(triuv),triuv);
}
/*
// triangle grid equiv
vec4 TriGrid(vec2 uv, out vec2 id)
{
    const vec2 s = vec2(1, .8660254); // Sqrt (3)/2
    uv /= s;
    float ys = mod(floor(uv.y), 2.)*.5;
    vec4 ipY = vec4(ys, 0, ys + .5, 0);
    vec4 ip4 = floor(uv.xyxy + ipY) - ipY + .5; 
    vec4 p4 = fract(uv.xyxy - ipY) - .5;
    float itri = (abs(p4.x)*2. + p4.y<.5)? 1. : -1.;
    p4 = itri>0.? vec4(p4.xy*s, ip4.xy) : vec4(p4.zw*s, ip4.zw);  

    vec2 ep = p4.xy;
    float off = 0.14433766666667*itri;
    ep.y = (ep.y + off) * itri;
    
    // inline sdEqTri
    const float k = 1.7320508;//sqrt(3.0);
    ep.x = abs(ep.x) - 0.5;
    ep.y = ep.y + 0.5/k;
    if( ep.x+k*ep.y>0.0 ) ep = vec2(ep.x-k*ep.y,-k*ep.x-ep.y)/2.0;
    ep.x -= clamp( ep.x, -1.0, 0.0 );
    float edge = -length(ep)*sign(ep.y);    // dist to edge
    
    id = p4.zw;
    id *= mat2(1.1547,0.0,-0.5773503,1.0); // adjust ID (optional)
    p4.y+=off;
    return vec4(abs(edge),length(p4.xy),p4.xy);
}
*/

// simple square grid equiv
vec4 SquareGrid(vec2 uv, out vec2 id)
{
    uv += 0.5;
    vec2 fs =  fract(uv)-0.5;
    id = floor(uv);
    id *= mat2(1.1547,0.0,-0.5773503,1.0); // adjust ID (optional)
    vec2 d = abs(fs)-0.5;
    float edge = length(max(d,0.0)) + min(max(d.x,d.y),0.0);
    return vec4(abs(edge),length(fs),fs.xy);
}

float hbar(vec2 p, float nline, float t)
{
    return 0.5+sin((p.y*nline)+t)*0.5;
}

// Demo 3xGrids or Let the user select a grid with mouse...
float SelectGrid(float xx)
{
    float gridtype = 0.5;
    if (iMouse.y>0.5)
        xx = ((iMouse.x-.5*RENDERSIZE.x) / RENDERSIZE.x)+0.5;
    if (xx > 0.66)
        gridtype=2.5;
    else if (xx > 0.33)
        gridtype = 1.5;
    return gridtype;
}

void main() {



    float t = TIME;
	vec2 uv = (gl_FragCoord.xy - 0.5 * RENDERSIZE.xy) / RENDERSIZE.y;
    float xx = ((gl_FragCoord.x-.5*RENDERSIZE.x) / RENDERSIZE.x)+0.5;
    float yy = ((gl_FragCoord.y-.5*RENDERSIZE.y) / RENDERSIZE.x)+0.5;
    // dirty grid switching
    vec2 id;
    vec4 h;
    float gridtype = SelectGrid(xx);
    if (gridtype>= 2.0)
        h = SquareGrid(uv*8.0, id);
    else if (gridtype>=1.0)
        h = HexGrid(uv*8.0, id);
    else
        h = TriGrid(uv*8.0, id);
    vec3 bordercol = vec3(1.0,1.0,1.0);
    vec3 shapecol = vec3(0.35,0.22,0.35);
    
    // just do a simple patterned shape tint based on cell IDs
    float patternVal = .5; // 33.5
    float cm = 1.0 + pow(abs(sin(length(id)*patternVal + t*0.65)), 4.0);	// pulse mult
    cm *= 1.0 + (hbar(h.zw,100.0,t*12.0)*0.1);					// bars mult
    shapecol *= cm;
    
    // Output to screen
    vec3 finalcol = mix(vec3(0.0),shapecol,smoothstep(0.0, 0.035, h.x-0.035)); // black outline edge
    float vv = smoothstep(0.0, 0.055, h.x);
    finalcol = mix(bordercol,finalcol,vv*vv); // white edge
    finalcol = mix(vec3(0.0),finalcol,smoothstep(0.0, 0.035, h.y-0.035)); // black outline centre
    vv = smoothstep(0.0, 0.055, h.y);
    finalcol = mix(bordercol,finalcol, vv*vv);  // white centre
    if (iMouse.x<0.5)
    {
        if (uv.y < 0.0)
            finalcol = vec3(h.x,h.x,h.x); // just show cell edge distances in bottom half of screen
        // add some red divider lines
        float dd = max( step(abs(xx-0.33),0.0025),step(abs(xx-0.66),0.0025));
        dd = max(dd,step(abs(yy-0.5),0.0025));
        finalcol = dd<1.0 ? finalcol : vec3(1.0,0.3,0.3);
    }
    //finalcol = vec3(h.x,h.x,h.x); // just show cell edge distance
    //finalcol = vec3(h.zw,0.0);    // just show cell uv
    //finalcol = vec3(h.y,h.y,h.y); // just show cell centre distance
    gl_FragColor = vec4(finalcol,1.0);
}

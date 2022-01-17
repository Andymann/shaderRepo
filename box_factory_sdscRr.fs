/*
{
    "CATEGORIES": [
        "Automatically Converted",
        "Shadertoy"
    ],
    "DESCRIPTION": "Automatically converted from https://www.shadertoy.com/view/sdscRr by FabriceNeyret2.  Mouse controls, camera.  swap #4 for textu\n\nreference: ( but I do the symmetric way )\n[url]https://twitter.com/akira2768922/status/1475077131151769613[/url]\n[url]https://www.reddit.com/r/desmos/comments/ry7zo8/production_line_manufacturing_boxes_d/[/url]\n",
    "IMPORTED": {
        "iChannel0": {
            "NAME": "iChannel0",
            "PATH": "10eb4fe0ac8a7dc348a2cc282ca5df1759ab8bf680117e4047728100969e7b43.jpg"
        }
    },
    "INPUTS": [
        {
            "NAME": "iMouse",
            "TYPE": "point2D"
        }
    ]
}

*/


#define rot(a)          mat2(cos(a+vec4(0,11,33,0)))                       // axial rotation
#define rot3(P,A,a)   ( mix( A*dot(P,A), P, cos(a) ) + sin(a)*cross(P,A) ) // 3D rotation
#define D(u)            vec4( vec3( smoothstep( 0., .1 ,  .5 - max(u.x,u.y) ) ), 1 ) // tile decoration
//#define D(u)          IMG_NORM_PIXEL(iChannel0,mod(u,1.0))                            // tile decoration

void main() {

 

    vec4  C = gl_FragColor-=gl_FragColor;
    vec3  R = RENDERSIZE, c,A,
          D = normalize(vec3(gl_FragCoord.xy+U-R.xy, 5.*R.y)),          // ray direction
          p = vec3(0,0,-20), q,                           // marching point along ray 
          M = iMouse.z > 0. ? iMouse.xyz/R -.5: vec3(10,6,0)/1e2*cos(.5*TIME+vec3(0,11,0))+vec3(0,.12,0);
    M = vec3(0,.5,0) - 6.3*M;      
    p.yz *= rot(M.y),                                     // camera rotations
    p.xz *= rot(M.x); 
    D.yz *= rot(M.y),         
    D.xz *= rot(M.x);
    float T = mod(TIME,2.), n,i,t,a,s;
    p.z += T - 7.;
    for ( i=0.; i<10.; i++ ) {                            // --- folded parts ----------------
        n = D.z > 0. ? 9.-i : i;                          // parse front to back
        t = mod(T+n,6.), a = 1.57*fract(t);
        vec2 CS = .707*sin(a+1.57/2.+vec2(1.57,0));
        if (T<1.) c = vec3(3.,  0, -2.5), c.xy += CS, A = vec3( 0,0,1); // even roll
        else      c = vec3(2.5, 0, -3. ), c.zy += CS, A = vec3(-1,0,0); // odd roll
        c.xz -= floor(t/2.); c.z -= n;
        if (mod(n,2.)>0.) c.x = -c.x, int(t)%2>0 ? c.x++ : c.z++, A.z = -A.z; // left side
     // if ( (T>1. && n>3. ) || n>5. ) c = .5-vec3(0, 0, n<6. ? 10-int(t)%2 : 5+int(n) ), a=0.; // cubes tail
        if ( (T>1. && n>3. ) || n>5. ) c = .5-vec3(0, n>4.?T+n-6.:0., n<6. ? 10-int(t)%2 : 5+int(n) ), // a = 0.;
                                       A = vec3(-1,0,0), n!=6.-floor(T) ? a=0.: a;
        q = p-c;
     // if ( dot(q,q) - dot(q,D)*dot(q,D) < 1.  ) {
            vec3 Pr = rot3(q,A,a), Dr = rot3(D,A,a),      // cube frame  
                  v = (-.5*sign(Dr) - Pr ) / Dr ,P;       // intersection with cube planes
     // if ( dot(Pr,Pr) - dot(Pr,Dr)*dot(Pr,Dr) < 1.  ) { // Bbox. why not working ?
            int j;                                        // draw cube faces corresponding to folds
            #define inter(i,s) P = abs(Pr + v[i]*Dr);                   \
                         j = int[](1,3,2,5,0,4)[ s Dr[i]>0.?3+i:i ];    \
                         if( v[i]>0. && max(P.x,max(P.y,P.z)) < .501 )  \
                        /**/ if( j <= int(T)+int(n/2.)*2 ) /**/         \
                          /**/  { int t = n<6. ? int(T):1; gl_FragColor += D( vec2( P[(i+1+t)%3], P[(i+2-t)%3] ) ); break;} /**/ \
                            //  { gl_FragColor[j%3] =j<3?.5:1.; gl_FragColor.a++; break; }   // attempt of time-consistant colors
                            //  { gl_FragColor[i] =.5+step(s Dr[i],0.); return; } // debug : draw color cube
            s = sign(c.x-.5);                             // left side 
            inter(0, s*); inter(1,); inter(2,);           // front cube faces
            v = ( .5*sign(Dr) - Pr ) / Dr ,P;             // rear cube faces
            inter(0,-s*); inter(1,-); inter(2,-);
     // }
    }
    if ( gl_FragColor.a > 0. && ( D.y < 0. || D.z>0. && n+T > 6. ) ) return;
                                                          // --- unfolded tiling -------------
    q = p;                                                // intersection with plane y=0
    t = -q.y/D.y; q += t*D;
    if (t>0.) C += 1.-t/100., C.a = 1.;                   // pseudo shading
    p = floor(q); if (3.*abs(p.x)-p.z > 10.+step(p.x,0.) ) C-=C;  // trim cells left by rolling cubes
    if ( T > 1. &&  abs(p.x)<=2. && p.z == -9.+3.*abs(p.x)-step(p.x,0.) )  C-=C;
    q.x += mod(ceil(q.z),2.);                             // offset
    if  ( q.x < -2. || q.x > 4. || q.z < -10.) C-=C;      // tiling limits
    
    C *= D( abs( fract(q.xz) -.5) );                      // tiles decoration
    
    gl_FragColor = mix(gl_FragColor,C,C.a);                                     // --- blend folded and straight parts ---
}
}

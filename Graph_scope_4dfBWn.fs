/*
{
  "IMPORTED" : [

  ],
  "CATEGORIES" : [
    "Automatically Converted",
    "Shadertoy"
  ],
  "DESCRIPTION" : "Automatically converted from https:\/\/www.shadertoy.com\/view\/4dfBWn by grinist.  A quickly hacked branch from https:\/\/www.shadertoy.com\/view\/Mdsfz7 for more oscilloscope-like visual. Mouse x controls refresh rate.\nPlease refer to the original for the point about fixed line width graph. This one just borks things, sorry for the spam.",
  "INPUTS" : [
    {
      "NAME" : "iMouse",
      "TYPE" : "point2D"
    }
  ]
}
*/


// A quickly hacked branch from https://www.shadertoy.com/view/Mdsfz7

// The values below control the rendering, knock yourself out.
#define AA_FALLOFF 1.2			// AA falloff in pixels, must be > 0, affects all drawing
#define GRID_WIDTH 0.1			// grid line width in pixels, must be >= 0
#define CURVE_WIDTH	10.0		// curve line width in pixels, must be >= 0

#define FUNC_SAMPLE_STEP 0.08	// function sample step size in pixels

#define SCOPE_RATE 0.5			// default oscilloscope refresh rate

float pp; 			// pixel pitch in graph units

float sinc(float x)
{
    return (x == 0.0) ? 1.0 : sin(x) / x;
}

float triIsolate(float x)
{
    return abs(-1.0 + fract(clamp(x, -0.5, 0.5)) * 2.0);
}

// Probably not a healthy heart
float heartbeat(float x)
{
    float prebeat = -sinc((x - 0.4) * 40.0) * 0.6 * triIsolate((x - 0.4) * 1.0);
    float mainbeat = (sinc((x - 0.5) * 60.0)) * 1.2 * triIsolate((x - 0.5) * 0.7);
    float postbeat = sinc((x - 0.85) * 15.0) * 0.5 * triIsolate((x - 0.85) * 0.6);
    return (prebeat + mainbeat + postbeat) * triIsolate((x - 0.625) * 0.8); // width 1.25
}

float squareApprox(float x)
{
    float p = x * 6.2832;
    float f = 1.0 * sin(p);
    f += 0.5 * sin(p * 3.0);
    f += 0.25 * sin(p * 5.0);
    return f;
}

// The function to be drawn
float func(float x)
{
    //return 0.5 * squareApprox(x);
    return 0.5 * heartbeat(mod((x + 0.25), 1.3));
}

// AA falloff function, trying lerp instead of smoothstep.
float aaStep(float a, float b, float x)
{
    // lerp step, make sure that a != b
    x = clamp(x, a, b);
    return (x - a) / (b - a);
}

// Alphablends color
void blend(inout vec4 baseCol, vec4 color, float alpha)
{
    baseCol = vec4(mix(baseCol.rgb, color.rgb, alpha * color.a), 1.0);
}

// Draws a gridline every stepSize
void drawGrid(inout vec4 baseCol, vec2 xy, float stepSize, vec4 gridCol)
{
	float hlw = GRID_WIDTH * pp * 0.5;
    float mul = 1.0 / stepSize;
	vec2 gf = abs(vec2(-0.5) + fract((xy + vec2(stepSize) * 0.5) * mul));
	float g = 1.0 - aaStep(hlw * mul, (hlw + pp * AA_FALLOFF) * mul, min(gf.x, gf.y));
    blend(baseCol, gridCol, g);
}

// Draws a circle
void drawCircle(inout vec4 baseCol, vec2 xy, vec2 center, float radius, vec4 color)
{
    float r = length(xy - center);
    float c = 1.0 - aaStep(0.0, radius + pp * AA_FALLOFF, r);
    blend(baseCol, color, c * c);
}

// Draws explicit function of x defined in func(x)
void drawFunc(inout vec4 baseCol, vec2 xy, vec4 curveCol)
{
    // samples the function around x neighborhood to get distance to curve
    float hlw = CURVE_WIDTH * pp * 0.5;
    
    // cover line width and aa
    float left = xy.x - hlw - pp * AA_FALLOFF;
    float right = xy.x + hlw + pp * AA_FALLOFF;
    float closest = 100000.0;
    for (float x = left; x <= right; x+= pp * FUNC_SAMPLE_STEP)
    {
        vec2 diff = vec2(x, func(x)) - xy;
        float dSqr = dot(diff, diff);
        closest = min(dSqr, closest);
    }
    
	float c = 1.0 - aaStep(0.0, hlw + pp * AA_FALLOFF, sqrt(closest));
	blend(baseCol, curveCol, c * c * c);
}

mat2 rotate2d(float angle)
{
    float sina = sin(angle);
    float cosa = cos(angle);
    return mat2(cosa, -sina,
                sina, cosa);
}

// Finds the next smaller power of 10
float findMagnitude(float range)
{
    float l10 = log(range) / log(10.0);
    return pow(10.0, floor(l10));
}

void main() {



    vec2 uv = gl_FragCoord.xy / RENDERSIZE.xy;
    
    // graph setup
	float aspect = RENDERSIZE.x / RENDERSIZE.y;
    float z = 0.0;
	// comment out disable zoom:
    z = sin(TIME * 0.3) * 1.2;
    
    float graphRange = 0.4 + pow(1.2, z * z * z);
	vec2 graphSize = vec2(aspect * graphRange, graphRange);
    vec2 graphCenter = vec2(0.5, 0.1);
	vec2 graphPos = graphCenter - graphSize * 0.5;
    vec2 xy = graphPos + uv * graphSize;	// xy = current graph coords
    pp = graphSize.y / RENDERSIZE.y;		// pp = pixel pitch in graph units
    
    // comment out to disable rotation:
   	xy = rotate2d(sin(TIME * 0.1) * 0.2) * (xy - graphCenter) + graphCenter;
    // background
    float t = length(0.5 - uv) * 1.414;
    t = t * t * t;
	vec4 col = mix(vec4(0.1, 0.25, 0.35, 1.0), vec4(0.02, 0.05, 0.07, 1.0), t);
    
	// grid
    float range = graphSize.y * 2.0;
    //float mag = findMagnitude(range);
    drawGrid(col, xy, 0.1, vec4(1.0, 1.0, 1.0, 0.1));
	drawGrid(col, xy, 0.5, vec4(1.0, 1.0, 1.0, 0.1));
	drawGrid(col, xy, 1.0, vec4(1.0, 1.0, 1.0, 0.4));
	float rate = SCOPE_RATE;
    //if (iMouse.z > 0.0)
    {
        rate = iMouse.x / RENDERSIZE.x;
        rate = pow(2.0, mix(-3.0, 3.0, rate));
    }
    
    // curve, magic scope coloring thing thrown in for hecks
    float pulse = fract(TIME * rate) * 4.0 - 1.5;
    float fade = pulse - xy.x;
    if (fade < 0.0) fade += 4.0;
    fade *= 0.25;
    fade = clamp(fade / rate, 0.0, 1.0);
    fade = 1.0 - fade;
    fade = fade * fade * fade;
    fade *= step(-1.5, xy.x) * step(xy.x, 2.5);
    vec4 pulseCol = vec4(0.0, 1.0, 0.7, fade * 1.5);
    drawFunc(col, xy, pulseCol);
    pulseCol.a = 1.5;
    drawCircle(col, xy, vec2(pulse, func(pulse)), CURVE_WIDTH * pp, pulseCol);
    
	gl_FragColor = col;
}

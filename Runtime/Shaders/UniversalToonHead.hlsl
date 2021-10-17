//Unity Toon Shader/Universal
//nobuyuki@unity3d.com
//toshiyuki@unity3d.com (Universal RP/HDRP) 

// https://forum.unity.com/threads/globally-suppress-pow-f-e-negative-f-warning.963488/
// https://docs.microsoft.com/en-us/windows/win32/direct3dhlsl/hlsl-errors-and-warnings
#pragma warning (disable : 3571) //  pow(f, e) will not work for negative f, use abs(f) or conditionally handle negative values if you expect them at line XXXX (on d3d11)
#pragma warning (disable : 3206) // "Implicit Truncation of vector type" warning code to disable

#ifndef UCTS_LWRP_INCLUDED
#define UCTS_LWRP_INCLUDED

#define UCTS_LWRP 1




#define MAINLIGHT_NOT_FOUND -2
#define MAINLIGHT_IS_MAINLIGHT -1


#ifndef DIRECTIONAL
# define DIRECTIONAL
#endif

#define FP_BUFFER 1
#if FP_BUFFER
#define SATURATE_IF_SDR(x) (x)
#define SATURATE_BASE_COLOR_IF_SDR(x) (x)
#else
#define SATURATE_IF_SDR(x) saturate(x)
#define SATURATE_BASE_COLOR_IF_SDR(x) saturate(x)
#endif







#if defined(UNITY_PASS_PREPASSBASE) || defined(UNITY_PASS_DEFERRED) || defined(UNITY_PASS_SHADOWCASTER)
#undef FOG_LINEAR
#undef FOG_EXP
#undef FOG_EXP2
#endif

//#include "UCTS_AutoLight.cginc" 
#if 1

// Legacy for compatibility with existing shaders
inline bool IsGammaSpace()
{
#ifdef UNITY_COLORSPACE_GAMMA
    return true;
#else
    return false;
#endif
}

inline float GammaToLinearSpaceExact(float value)
{
    if (value <= 0.04045F)
        return value / 12.92F;
    else if (value < 1.0F)
        return pow((value + 0.055F) / 1.055F, 2.4F);
    else
        return pow(value, 2.2F);
}

inline half3 GammaToLinearSpace(half3 sRGB)
{
    // Approximate version from http://chilliant.blogspot.com.au/2012/08/srgb-approximations-for-hlsl.html?m=1
    return sRGB * (sRGB * (sRGB * 0.305306011h + 0.682171111h) + 0.012522878h);

    // Precise version, useful for debugging.
    //return half3(GammaToLinearSpaceExact(sRGB.r), GammaToLinearSpaceExact(sRGB.g), GammaToLinearSpaceExact(sRGB.b));
}

inline float LinearToGammaSpaceExact(float value)
{
    if (value <= 0.0F)
        return 0.0F;
    else if (value <= 0.0031308F)
        return 12.92F * value;
    else if (value < 1.0F)
        return 1.055F * pow(value, 0.4166667F) - 0.055F;
    else
        return pow(value, 0.45454545F);
}

inline half3 LinearToGammaSpace(half3 linRGB)
{
    linRGB = max(linRGB, half3(0.h, 0.h, 0.h));
    // An almost-perfect approximation from http://chilliant.blogspot.com.au/2012/08/srgb-approximations-for-hlsl.html?m=1
    return max(1.055h * pow(linRGB, 0.416666667h) - 0.055h, 0.h);

    // Exact version, useful for debugging.
    //return half3(LinearToGammaSpaceExact(linRGB.r), LinearToGammaSpaceExact(linRGB.g), LinearToGammaSpaceExact(linRGB.b));
}


#if defined(FOG_LINEAR) || defined(FOG_EXP) || defined(FOG_EXP2)
#define UNITY_FOG_COORDS(idx) UNITY_FOG_COORDS_PACKED(idx, float1)

#if (SHADER_TARGET < 30) || defined(SHADER_API_MOBILE)
// mobile or SM2.0: calculate fog factor per-vertex
#define UNITY_TRANSFER_FOG(o,outpos) UNITY_CALC_FOG_FACTOR((outpos).z); o.fogCoord.x = unityFogFactor
#else
// SM3.0 and PC/console: calculate fog distance per-vertex, and fog factor per-pixel
#define UNITY_TRANSFER_FOG(o,outpos) o.fogCoord.x = (outpos).z
#endif
#else
#define UNITY_FOG_COORDS(idx)
#define UNITY_TRANSFER_FOG(o,outpos)
#endif

#define UNITY_FOG_LERP_COLOR(col,fogCol,fogFac) col.rgb = lerp((fogCol).rgb, (col).rgb, saturate(fogFac))


#if defined(FOG_LINEAR) || defined(FOG_EXP) || defined(FOG_EXP2)
#if (SHADER_TARGET < 30) || defined(SHADER_API_MOBILE)
// mobile or SM2.0: fog factor was already calculated per-vertex, so just lerp the color
#define UNITY_APPLY_FOG_COLOR(coord,col,fogCol) UNITY_FOG_LERP_COLOR(col,fogCol,(coord).x)
#else
// SM3.0 and PC/console: calculate fog factor and lerp fog color
#define UNITY_APPLY_FOG_COLOR(coord,col,fogCol) UNITY_CALC_FOG_FACTOR((coord).x); UNITY_FOG_LERP_COLOR(col,fogCol,unityFogFactor)
#endif
#else
#define UNITY_APPLY_FOG_COLOR(coord,col,fogCol)
#endif

#ifdef UNITY_PASS_FORWARDADD
#define UNITY_APPLY_FOG(coord,col) UNITY_APPLY_FOG_COLOR(coord,col,fixed4(0,0,0,0))
#else
#define UNITY_APPLY_FOG(coord,col) UNITY_APPLY_FOG_COLOR(coord,col,unity_FogColor)
#endif

#endif //#if false

#ifdef DIRECTIONAL
#define LIGHTING_COORDS(idx1,idx2) SHADOW_COORDS(idx1)
#define TRANSFER_VERTEX_TO_FRAGMENT(a) TRANSFER_SHADOW(a)
#define LIGHT_ATTENUATION(a)    SHADOW_ATTENUATION(a)
#endif

// Transforms 2D UV by scale/bias property
//#define TRANSFORM_TEX(tex,name) (tex.xy * name##_ST.xy + name##_ST.zw)
#define UCTS_TEXTURE2D(tex,name)  SAMPLE_TEXTURE2D(tex,sampler##tex,TRANSFORM_TEX(name, tex));

inline float4 UnityObjectToClipPosInstanced(in float3 pos)
{
    //    return mul(UNITY_MATRIX_VP, mul(unity_ObjectToWorldArray[unity_InstanceID], float4(pos, 1.0)));
          // todo. right?
    return mul(UNITY_MATRIX_VP, mul(unity_ObjectToWorld, float4(pos, 1.0)));
}
inline float4 UnityObjectToClipPosInstanced(float4 pos)
{
    return UnityObjectToClipPosInstanced(pos.xyz);
}
#define UnityObjectToClipPos UnityObjectToClipPosInstanced

inline float3 UnityObjectToWorldNormal(in float3 norm)
{
#ifdef UNITY_ASSUME_UNIFORM_SCALING
    return UnityObjectToWorldDir(norm);
#else
    // mul(IT_M, norm) => mul(norm, I_M) => {dot(norm, I_M.col0), dot(norm, I_M.col1), dot(norm, I_M.col2)}
    return normalize(mul(norm, (float3x3)unity_WorldToObject));
#endif
}
// normal should be normalized, w=1.0
half3 SHEvalLinearL0L1(half4 normal)
{
    half3 x;

    // Linear (L1) + constant (L0) polynomial terms
    x.r = dot(unity_SHAr, normal);
    x.g = dot(unity_SHAg, normal);
    x.b = dot(unity_SHAb, normal);

    return x;
}

// normal should be normalized, w=1.0
half3 SHEvalLinearL2(half4 normal)
{
    half3 x1, x2;
    // 4 of the quadratic (L2) polynomials
    half4 vB = normal.xyzz * normal.yzzx;
    x1.r = dot(unity_SHBr, vB);
    x1.g = dot(unity_SHBg, vB);
    x1.b = dot(unity_SHBb, vB);

    // Final (5th) quadratic (L2) polynomial
    half vC = normal.x*normal.x - normal.y*normal.y;
    x2 = unity_SHC.rgb * vC;

    return x1 + x2;
}

// normal should be normalized, w=1.0
// output in active color space
half3 ShadeSH9(half4 normal)
{
    // Linear + constant polynomial terms
    half3 res = SHEvalLinearL0L1(normal);

    // Quadratic polynomials
    res += SHEvalLinearL2(normal);

#   ifdef UNITY_COLORSPACE_GAMMA
    res = LinearToGammaSpace(res);
#   endif

    return res;
}


#endif //#ifndef UCTS_LWRP_INCLUDED
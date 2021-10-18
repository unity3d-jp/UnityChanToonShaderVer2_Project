//Unity Toon Shader/HDRP
//nobuyuki@unity3d.com
//toshiyuki@unity3d.com (Universal RP/HDRP) 

#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Macros.hlsl"
#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/PhysicalCamera.hlsl"
#include "HDRPToonHead.hlsl"

// Channel mask enum.
// this must be same to UI cs code
// HDRPToonGUI._ChannelEnum
int eBaseColor = 0;
int eFirstShade = 1;
int eSecondShade = 2;
int eHighlight = 3;
int eAngelRing = 4;
int eRimLight = 5;
int eOutline = 6;








// not in materials
int _ToonLightHiCutFilter;
int _ToonEvAdjustmentCurve;
float _ToonEvAdjustmentValueArray[128];
float _ToonEvAdjustmentValueMin;
float _ToonEvAdjustmentValueMax;
float _ToonEvAdjustmentCompensation;
float _ToonIgnoreExposureMultiplier;


// function to rotate the UV: RotateUV()
//float2 rotatedUV = RotateUV(i.uv0, (_angular_Verocity*3.141592654), float2(0.5, 0.5), _Time.g);
float2 RotateUV(float2 _uv, float _radian, float2 _piv, float _time)
{
    float RotateUV_ang = _radian;
    float RotateUV_cos = cos(_time*RotateUV_ang);
    float RotateUV_sin = sin(_time*RotateUV_ang);
    return (mul(_uv - _piv, float2x2(RotateUV_cos, -RotateUV_sin, RotateUV_sin, RotateUV_cos)) + _piv);
}

float3 ConvertFromEV100(float3 EV100)
{
#if 1
    float3 value = pow(2, EV100) * 2.5f;
    return value;
#else
    float3 maxLuminance = 1.2f * pow(2.0f, EV100);
    return 1.0f / maxLuminance;
#endif
}

float3 ConvertToEV100(float3 value)
{
#if 1
    return log2(value*0.4f);
#else
    return log2(1.0f / (1.2f * value));
#endif
}



float WeightSample(PositionInputs positionInput)
{
    // Center-weighted
    const float2 kCenter = _ScreenParams.xy * 0.5;
    const float weight = pow(length((kCenter.xy - positionInput.positionSS.xy) / _ScreenParams.xy),1.0) ;
    return 1.0 - saturate(weight);
}

float3 ApplyCompensation(float3 originalColor)
{
    float3 ev100_Color = ConvertToEV100(originalColor) +_ToonEvAdjustmentCompensation * 0.5f; 


    float3 resultColor = max(0, ConvertFromEV100(ev100_Color));
    return resultColor;
}

float3 ApplyCurrentExposureMultiplier(float3 color)
{
    return color * lerp(GetCurrentExposureMultiplier(), 1, _ToonIgnoreExposureMultiplier);
}


float3 GetExposureAdjustedColor(float3 originalColor)
{
    if (_ToonEvAdjustmentCurve != 0)
    {

        float3 ev100_Color = ConvertToEV100(originalColor);
        ev100_Color = clamp(ev100_Color, _ToonEvAdjustmentValueMin, _ToonEvAdjustmentValueMax);
        float3 ev100_remap = (ev100_Color - _ToonEvAdjustmentValueMin) * (128-1) / (_ToonEvAdjustmentValueMax - _ToonEvAdjustmentValueMin);
        ev100_remap = clamp(ev100_remap, 0.0, 127.0);
        int3  ev100_idx = (int3)ev100_remap;
        float3 ev100_lerp = ev100_remap - ev100_idx;
        float3  ev100_remapped;

        ev100_remapped.r = _ToonEvAdjustmentValueArray[ev100_idx.r] +(_ToonEvAdjustmentValueArray[ev100_idx.r + 1] - _ToonEvAdjustmentValueArray[ev100_idx.r]) * ev100_lerp.r;
        ev100_remapped.g = _ToonEvAdjustmentValueArray[ev100_idx.g] +(_ToonEvAdjustmentValueArray[ev100_idx.g + 1] - _ToonEvAdjustmentValueArray[ev100_idx.g]) * ev100_lerp.g;
        ev100_remapped.b = _ToonEvAdjustmentValueArray[ev100_idx.b] +(_ToonEvAdjustmentValueArray[ev100_idx.b + 1] - _ToonEvAdjustmentValueArray[ev100_idx.b]) * ev100_lerp.b;


        float3 resultColor = ConvertFromEV100(ev100_remapped);


        return resultColor;
    }
    else  // else is neccessary to avoid warrnings.
    {
        return originalColor;
    }
}


float  GetLightAttenuation(float3 lightColor)
{
    float lightAttenuation = rateR *lightColor.r + rateG *lightColor.g + rateB *lightColor.b;
    return lightAttenuation;
}


int GetNextDirectionalLightIndex(BuiltinData builtinData, int currentIndex, int mainLightIndex)
{
    int i = 0; // Declare once to avoid the D3D11 compiler warning.
    for (i = 0; i < (int)_DirectionalLightCount; ++i)
    {
        if (IsMatchingLightLayer(_DirectionalLightDatas[i].lightLayers, builtinData.renderingLayers))
        {
            if (mainLightIndex != i)
            {
                if (currentIndex < i)
                {
                    return i;
                }
            }
        }
    }
    return -1; // not found
}
int GetUtsMainLightIndex(BuiltinData builtinData)
{
    int mainLightIndex = -1;
    float3 lightColor = float3(0.0f, 0.0f, 0.0f);
    float  lightAttenuation = 0.0f;
    uint i = 0; // Declare once to avoid the D3D11 compiler warning.
    for (i = 0; i < _DirectionalLightCount; ++i)
    {
        if (IsMatchingLightLayer(_DirectionalLightDatas[i].lightLayers, builtinData.renderingLayers))
        {
            float3 currentLightColor = _DirectionalLightDatas[i].color;
            float  currentLightAttenuation = GetLightAttenuation(currentLightColor);

            if (mainLightIndex == -1 || (currentLightAttenuation > lightAttenuation))
            {
                mainLightIndex = i;
                lightAttenuation = currentLightAttenuation;
                lightColor = currentLightColor;
            } 
        }
    }

    return mainLightIndex;
}


#if defined(_SHADINGGRADEMAP)|| defined(UTS_DEBUG_SHADOWMAP) || defined(UTS_DEBUG_SELFSHADOW)
# include "ShadingGrademapOtherLight.hlsl"
#else //#if defined(_SHADINGGRADEMAP)
# include "DoubleShadeWithFeatherOtherLight.hlsl"
#endif //#if defined(_SHADINGGRADEMAP)

# include "UtsSelfShadowMainLight.hlsl"



#if defined(_SHADINGGRADEMAP)|| defined(UTS_DEBUG_SHADOWMAP) 
# include "ShadingGrademapMainLight.hlsl"
#else
# include "DoubleShadeWithFeatherMainLight.hlsl"
#endif


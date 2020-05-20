#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Macros.hlsl"
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


uniform float _utsTechnique;


//uniform sampler2D _MainTex; uniform float4 _MainTex_ST;
#if UCTS_LWRP
#else
//uniform float4 _BaseColor;
#endif
//v.2.0.5
uniform float4 _Color;
uniform fixed _Use_BaseAs1st;
uniform fixed _Use_1stAs2nd;
//
uniform fixed _Is_LightColor_Base;
uniform sampler2D _1st_ShadeMap; uniform float4 _1st_ShadeMap_ST;
uniform float4 _1st_ShadeColor;
uniform fixed _Is_LightColor_1st_Shade;
uniform sampler2D _2nd_ShadeMap; uniform float4 _2nd_ShadeMap_ST;
uniform float4 _2nd_ShadeColor;
uniform fixed _Is_LightColor_2nd_Shade;
//uniform sampler2D _NormalMap; uniform float4 _NormalMap_ST;
uniform fixed _Is_NormalMapToBase;
uniform fixed _Set_SystemShadowsToBase;
uniform float _Tweak_SystemShadowsLevel;
uniform float _BaseColor_Step;
uniform float _BaseShade_Feather;
uniform sampler2D _Set_1st_ShadePosition; uniform float4 _Set_1st_ShadePosition_ST;
uniform float _ShadeColor_Step;
uniform float _1st2nd_Shades_Feather;
uniform sampler2D _Set_2nd_ShadePosition; uniform float4 _Set_2nd_ShadePosition_ST;

// ShadingGradeMap
#if defined(_SHADINGGRADEMAP)
uniform sampler2D _ShadingGradeMap; uniform float4 _ShadingGradeMap_ST;
#endif
//v.2.0.6
uniform float _Tweak_ShadingGradeMapLevel;
uniform fixed _BlurLevelSGM;
//
uniform float _1st_ShadeColor_Step;
uniform float _1st_ShadeColor_Feather;
uniform float _2nd_ShadeColor_Step;
uniform float _2nd_ShadeColor_Feather;
// ShadingGradeMap

uniform float4 _HighColor;
uniform sampler2D _HighColor_Tex; uniform float4 _HighColor_Tex_ST;
uniform fixed _Is_LightColor_HighColor;
uniform fixed _Is_NormalMapToHighColor;
uniform float _HighColor_Power;
uniform fixed _Is_SpecularToHighColor;
uniform fixed _Is_BlendAddToHiColor;
uniform fixed _Is_UseTweakHighColorOnShadow;
uniform float _TweakHighColorOnShadow;
uniform sampler2D _Set_HighColorMask; uniform float4 _Set_HighColorMask_ST;
uniform float _Tweak_HighColorMaskLevel;
uniform fixed _RimLight;
uniform float4 _RimLightColor;
uniform fixed _Is_LightColor_RimLight;
uniform fixed _Is_NormalMapToRimLight;
uniform float _RimLight_Power;
uniform float _RimLight_InsideMask;
uniform fixed _RimLight_FeatherOff;
uniform fixed _LightDirection_MaskOn;
uniform float _Tweak_LightDirection_MaskLevel;
uniform fixed _Add_Antipodean_RimLight;
uniform float4 _Ap_RimLightColor;
uniform fixed _Is_LightColor_Ap_RimLight;
uniform float _Ap_RimLight_Power;
uniform fixed _Ap_RimLight_FeatherOff;
uniform sampler2D _Set_RimLightMask; uniform float4 _Set_RimLightMask_ST;
uniform float _Tweak_RimLightMaskLevel;
uniform fixed _MatCap;

uniform sampler2D _MatCap_Sampler; uniform float4 _MatCap_Sampler_ST;

uniform float4 _MatCapColor;
uniform fixed _Is_LightColor_MatCap;
uniform fixed _Is_BlendAddToMatCap;
uniform float _Tweak_MatCapUV;
uniform float _Rotate_MatCapUV;
uniform fixed _Is_NormalMapForMatCap;
uniform sampler2D _NormalMapForMatCap; uniform float4 _NormalMapForMatCap_ST;
uniform float _Rotate_NormalMapForMatCapUV;
uniform fixed _Is_UseTweakMatCapOnShadow;
uniform float _TweakMatCapOnShadow;
//MatcapMask
uniform sampler2D _Set_MatcapMask; uniform float4 _Set_MatcapMask_ST;
uniform float _Tweak_MatcapMaskLevel;
//v.2.0.5
uniform fixed _Is_Ortho;
//v.2.0.6
uniform float _CameraRolling_Stabilizer;
uniform fixed _BlurLevelMatcap;
uniform fixed _Inverse_MatcapMask;
#if UCTS_LWRP
#else
uniform float _BumpScale;
#endif
uniform float _BumpScaleMatcap;
//Emissive
uniform sampler2D _Emissive_Tex; uniform float4 _Emissive_Tex_ST;
uniform float4 _Emissive_Color;
//v.2.0.7
uniform fixed _Is_ViewCoord_Scroll;
uniform float _Rotate_EmissiveUV;
uniform float _Base_Speed;
uniform float _Scroll_EmissiveU;
uniform float _Scroll_EmissiveV;
uniform fixed _Is_PingPong_Base;
uniform float4 _ColorShift;
uniform float4 _ViewShift;
uniform float _ColorShift_Speed;
uniform fixed _Is_ColorShift;
uniform fixed _Is_ViewShift;
uniform float3 emissive;
// 
uniform float _Unlit_Intensity;
//v.2.0.5
uniform fixed _Is_Filter_HiCutPointLightColor;
uniform fixed _Is_Filter_LightColor;
//v.2.0.4.4
uniform float _StepOffset;
uniform fixed _Is_BLD;
uniform float _Offset_X_Axis_BLD;
uniform float _Offset_Y_Axis_BLD;
uniform fixed _Inverse_Z_Axis_BLD;
//v.2.0.4
#ifdef _IS_CLIPPING_MODE
//DoubleShadeWithFeather_Clipping
uniform sampler2D _ClippingMask; uniform float4 _ClippingMask_ST;
uniform float _Clipping_Level;
uniform fixed _Inverse_Clipping;
#elif defined(_IS_CLIPPING_TRANSMODE) || defined(_IS_TRANSCLIPPING_ON)
//DoubleShadeWithFeather_TransClipping
uniform sampler2D _ClippingMask; uniform float4 _ClippingMask_ST;
uniform fixed _IsBaseMapAlphaAsClippingMask;
uniform float _Clipping_Level;
uniform fixed _Inverse_Clipping;
uniform float _Tweak_transparency;
#elif defined(_IS_CLIPPING_OFF) || defined(_IS_TRANSCLIPPING_OFF)
//DoubleShadeWithFeather
#endif


sampler2D _MainTex; uniform float4 _MainTex_ST;
uniform float _GI_Intensity;
#if defined(_SHADINGGRADEMAP)

#ifdef _IS_ANGELRING_OFF
//
#elif _IS_ANGELRING_ON
uniform fixed _AngelRing;

uniform sampler2D _AngelRing_Sampler; uniform float4 _AngelRing_Sampler_ST;
uniform float4 _AngelRing_Color;
uniform fixed _Is_LightColor_AR;
uniform float _AR_OffsetU;
uniform float _AR_OffsetV;
uniform fixed _ARSampler_AlphaOn;

#endif
#endif     //#if defined(_SHADINGGRADEMP)


uniform float  _BaseColorVisible;
uniform float  _BaseColorOverridden;
uniform float4 _BaseColorMaskColor;

uniform float  _FirstShadeVisible;
uniform float  _FirstShadeOverridden;
uniform float4 _FirstShadeMaskColor;

uniform float  _SecondShadeVisible;
uniform float  _SecondShadeOverridden;
uniform float4 _SecondShadeMaskColor;

uniform float  _HighlightVisible;
uniform float  _HighlightOverridden;
uniform float4 _HighlightMaskColor;

uniform float  _AngelRingVisible;
uniform float  _AngelRingOverridden;
uniform float4 _AngelRingMaskColor;

uniform float  _RimLightVisible;
uniform float  _RimLightOverridden;
uniform float4 _RimLightMaskColor;

uniform float _OutlineVisible;
uniform float _OutlineOverridden;
uniform float4 _OutlineMaskColor;

uniform float _ComposerMaskMode;
// just grafted from UTS/Universal RP
struct UtsLight
{
    float4   direction;
    float3   color;
    float    distanceAttenuation;
    float    shadowAttenuation;
    int     type;
};

// UVâÒì]ÇÇ∑ÇÈä÷êîÅFRotateUV()
//float2 rotatedUV = RotateUV(i.uv0, (_angular_Verocity*3.141592654), float2(0.5, 0.5), _Time.g);
float2 RotateUV(float2 _uv, float _radian, float2 _piv, float _time)
{
    float RotateUV_ang = _radian;
    float RotateUV_cos = cos(_time*RotateUV_ang);
    float RotateUV_sin = sin(_time*RotateUV_ang);
    return (mul(_uv - _piv, float2x2(RotateUV_cos, -RotateUV_sin, RotateUV_sin, RotateUV_cos)) + _piv);
}


float  GetLightAttenuation(float3 lightColor)
{
    float lightAttenuation = 0.299*lightColor.r + 0.587*lightColor.g + 0.114*lightColor.b;
    return lightAttenuation;
}

int GetNextDirectionalLightIndex(BuiltinData builtinData, int currentIndex, int mainLightIndex)
{
    uint i = 0; // Declare once to avoid the D3D11 compiler warning.
    for (i = 0; i < _DirectionalLightCount; ++i)
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
#if defined(_SHADINGGRADEMAP)
# include "ShadingGrademapOtherLight.hlsl"
#else //#if defined(_SHADINGGRADEMAP)
# include "DoubleShadeWithFeatherOtherLight.hlsl"
#endif //#if defined(_SHADINGGRADEMAP)


#if defined(_SHADINGGRADEMAP)
# include "ShadingGrademapMainLight.hlsl"
#else
# include "DoubleShadeWithFeatherMainLight.hlsl"
#endif


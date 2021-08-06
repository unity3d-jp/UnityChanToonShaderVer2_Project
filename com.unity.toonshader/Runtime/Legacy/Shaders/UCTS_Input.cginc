
#ifndef SAMPLE_TEXTURE2D
#define SAMPLE_TEXTURE2D(textureName, samplerName, coord2)                          	textureName.SampleLevel(samplerName, coord2, 0)
#endif

// CBUFFER_START(UnityPerMaterial)
float _utsTechnique;
float4 _MainTex_ST;

float4 _Color;
fixed _Use_BaseAs1st;
fixed _Use_1stAs2nd;
fixed _Is_LightColor_Base;
//
float4 _1st_ShadeMap_ST;
float4 _1st_ShadeColor;
fixed _Is_LightColor_1st_Shade;
float4 _2nd_ShadeMap_ST;
float4 _2nd_ShadeColor;
fixed _Is_LightColor_2nd_Shade;
float4 _NormalMap_ST;

fixed _Is_NormalMapToBase;
fixed _Set_SystemShadowsToBase;
float _Tweak_SystemShadowsLevel;
float _BaseColor_Step;
float _BaseShade_Feather;

float4 _Set_1st_ShadePosition_ST;
float _ShadeColor_Step;
float _1st2nd_Shades_Feather;
float4 _Set_2nd_ShadePosition_ST;

float4 _ShadingGradeMap_ST;

float _Tweak_ShadingGradeMapLevel;
fixed _BlurLevelSGM;

float _1st_ShadeColor_Step;
float _1st_ShadeColor_Feather;
float _2nd_ShadeColor_Step;
float _2nd_ShadeColor_Feather;

float4 _HighColor;
float4 _HighColor_Tex_ST;
fixed _Is_LightColor_HighColor;
fixed _Is_NormalMapToHighColor;
float _HighColor_Power;

fixed _Is_SpecularToHighColor;
fixed _Is_BlendAddToHiColor;
fixed _Is_UseTweakHighColorOnShadow;
float _TweakHighColorOnShadow;

float4 _Set_HighColorMask_ST;

float _Tweak_HighColorMaskLevel;
fixed _RimLight;
float4 _RimLightColor;
fixed _Is_LightColor_RimLight;
fixed _Is_NormalMapToRimLight;
float _RimLight_Power;
float _RimLight_InsideMask;
fixed _RimLight_FeatherOff;
fixed _LightDirection_MaskOn;
float _Tweak_LightDirection_MaskLevel;
fixed _Add_Antipodean_RimLight;
float4 _Ap_RimLightColor;
fixed _Is_LightColor_Ap_RimLight;
float _Ap_RimLight_Power;
fixed _Ap_RimLight_FeatherOff;
float4 _Set_RimLightMask_ST;
float _Tweak_RimLightMaskLevel;
fixed _MatCap;

float4 _MatCap_Sampler_ST;

float4 _MatCapColor;
fixed _Is_LightColor_MatCap;
fixed _Is_BlendAddToMatCap;
float _Tweak_MatCapUV;
float _Rotate_MatCapUV;
fixed _Is_NormalMapForMatCap;

float4 _NormalMapForMatCap_ST;
float _Rotate_NormalMapForMatCapUV;
fixed _Is_UseTweakMatCapOnShadow;
float _TweakMatCapOnShadow;
//MatcapMask
// 
float4 _Set_MatcapMask_ST;
float _Tweak_MatcapMaskLevel;

fixed _Is_Ortho;

float _CameraRolling_Stabilizer;
fixed _BlurLevelMatcap;
fixed _Inverse_MatcapMask;

float _BumpScaleMatcap;

float4 _Emissive_Tex_ST;
float4 _Emissive_Color;

uniform fixed _Is_ViewCoord_Scroll;
float _Rotate_EmissiveUV;
float _Base_Speed;
float _Scroll_EmissiveU;
float _Scroll_EmissiveV;
fixed _Is_PingPong_Base;
float4 _ColorShift;
float4 _ViewShift;
float _ColorShift_Speed;
fixed _Is_ColorShift;
fixed _Is_ViewShift;
float3 emissive;
// 

float _Unlit_Intensity;

fixed _Is_Filter_HiCutPointLightColor;
fixed _Is_Filter_LightColor;

float _StepOffset;
fixed _Is_BLD;
float _Offset_X_Axis_BLD;
float _Offset_Y_Axis_BLD;
fixed _Inverse_Z_Axis_BLD;

float4 _ClippingMask_ST;

fixed _IsBaseMapAlphaAsClippingMask;
float _Clipping_Level;
fixed _Inverse_Clipping;
float _Tweak_transparency;

float _GI_Intensity;
fixed  _AngelRing;
float4 _AngelRing_Sampler_ST;
float4 _AngelRing_Color;
fixed _Is_LightColor_AR;
float _AR_OffsetU;
float _AR_OffsetV;
fixed _ARSampler_AlphaOn;

// OUTLINE 


fixed _Is_LightColor_Outline;

float _Outline_Width;
float _Farthest_Distance;
float _Nearest_Distance;
float4 _Outline_Sampler_ST;
float4 _Outline_Color;
fixed _Is_BlendBaseColor;
float _Offset_Z;

float4 _OutlineTex_ST;
fixed _Is_OutlineTex;

float4 _BakedNormal_ST;
fixed _Is_BakedNormal;

float _ZOverDrawMode;

//
// 
//
float4 _BaseMap_ST;
half4 _BaseColor;
//half4 _SpecColor;
half4 _EmissionColor;
half _Cutoff;
half _Smoothness;
half _Metallic;
half _BumpScale;
half _OcclusionStrength;
half _Surface;
// CBUFFER_END

// sampler2D _MainTex;
// sampler2D _1st_ShadeMap;
// sampler2D _2nd_ShadeMap;
// sampler2D _NormalMap;

Texture2D _MainTex; SamplerState sampler_MainTex;
Texture2D _1st_ShadeMap;
Texture2D _2nd_ShadeMap;
Texture2D _NormalMap;

sampler2D _Set_1st_ShadePosition;
sampler2D _Set_2nd_ShadePosition;
sampler2D _ShadingGradeMap;
sampler2D _HighColor_Tex;
sampler2D _Set_HighColorMask;
sampler2D _Set_RimLightMask;
sampler2D _MatCap_Sampler;
sampler2D _NormalMapForMatCap;
sampler2D _Set_MatcapMask;
sampler2D _Emissive_Tex;
sampler2D _ClippingMask;
sampler2D _AngelRing_Sampler;
sampler2D _Outline_Sampler;
sampler2D _OutlineTex;
sampler2D _BakedNormal;
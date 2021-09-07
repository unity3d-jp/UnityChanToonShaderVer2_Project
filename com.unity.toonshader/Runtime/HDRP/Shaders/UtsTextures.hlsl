
// Unity Toon Shader
// sampler2D _MainTex;
// sampler2D _1st_ShadeMap;
// sampler2D _2nd_ShadeMap;

TEXTURE2D(_MainTex); SAMPLER(sampler_MainTex);
TEXTURE2D(_1st_ShadeMap);
TEXTURE2D(_2nd_ShadeMap);


sampler _Set_1st_ShadePosition;
sampler _Set_2nd_ShadePosition;
sampler _ShadingGradeMap;
sampler _HighColor_Tex;
sampler _Set_HighColorMask;
sampler _Set_RimLightMask;
sampler _MatCap_Sampler;
sampler _NormalMapForMatCap;
sampler _Set_MatcapMask;
sampler _Emissive_Tex;
// sampler2D _ClippingMask;
TEXTURE2D(_ClippingMask);
sampler _AngelRing_Sampler;
sampler _Outline_Sampler;
sampler _OutlineTex;
sampler _BakedNormal;


// Unity Toon Shader
// sampler2D _MainTex;
// sampler2D _1st_ShadeMap;
// sampler2D _2nd_ShadeMap;

TEXTURE2D(_MainTex); SAMPLER(sampler_MainTex);
TEXTURE2D(_1st_ShadeMap);
TEXTURE2D(_2nd_ShadeMap);


sampler2D _Set_1st_ShadePosition;
sampler2D _Set_2nd_ShadePosition;
sampler2D  _ShadingGradeMap;
sampler2D _HighColor_Tex;
sampler2D _Set_HighColorMask;
sampler2D _Set_RimLightMask;
sampler2D _MatCap_Sampler;
sampler2D _NormalMapForMatCap;
sampler2D  _Set_MatcapMask;
sampler2D _Emissive_Tex;
// sampler2D _ClippingMask);
TEXTURE2D(_ClippingMask);
sampler2D _AngelRing_Sampler;
sampler2D _Outline_Sampler;
sampler2D _OutlineTex;
sampler2D _BakedNormal;

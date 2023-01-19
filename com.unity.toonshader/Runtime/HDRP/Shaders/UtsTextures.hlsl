
// Unity Toon Shader
// sampler2D _MainTex;
// sampler2D _1st_ShadeMap;
// sampler2D _2nd_ShadeMap;

TEXTURE2D(_MainTex); SAMPLER(sampler_MainTex);
TEXTURE2D(_1st_ShadeMap);
TEXTURE2D(_2nd_ShadeMap);


SAMPLER(_Set_1st_ShadePosition);
SAMPLER(_Set_2nd_ShadePosition);
SAMPLER(_ShadingGradeMap);
SAMPLER(_HighColor_Tex);
SAMPLER(_Set_HighColorMask);
SAMPLER(_Set_RimLightMask);
SAMPLER(_MatCap_Sampler);
SAMPLER(_NormalMapForMatCap);
SAMPLER( _Set_MatcapMask);
SAMPLER(_Emissive_Tex);
// sampler2D _ClippingMask);
TEXTURE2D(_ClippingMask);
SAMPLER(_AngelRing_Sampler);
SAMPLER(_Outline_Sampler);
SAMPLER(_OutlineTex);
SAMPLER(_BakedNormal);

//Unitychan Toon Shader ver.2.0
//v.2.0.9
//nobuyuki@unity3d.com
//https://github.com/unity3d-jp/UnityChanToonShaderVer2_Project
//(C)Unity Technologies Japan/UCL
Shader "UnityChanToonShader/Tessellation/Light/Toon_ShadingGradeMap_StencilMask" {
    Properties {
        [HideInInspector] _simpleUI ("SimpleUI", Int ) = 0
        [HideInInspector] _utsVersion ("Version", Float ) = 2.08
        [HideInInspector] _utsTechnique ("Technique", int ) = 1 //SGM
        _StencilNo ("Stencil No", int) =1
        [Enum(OFF,0,FRONT,1,BACK,2)] _CullMode("Cull Mode", int) = 2  //OFF/FRONT/BACK
        _MainTex ("BaseMap", 2D) = "white" {}
        [HideInInspector] _BaseMap ("BaseMap", 2D) = "white" {}
        _BaseColor ("BaseColor", Color) = (1,1,1,1)
        //v.2.0.5 : Clipping/TransClipping for SSAO Problems in PostProcessing Stack.
        //If you want to go back the former SSAO results, comment out the below line.
        [HideInInspector] _Color ("Color", Color) = (1,1,1,1)
        //
        [Toggle(_)] _Is_LightColor_Base ("Is_LightColor_Base", Float ) = 1
        _1st_ShadeMap ("1st_ShadeMap", 2D) = "white" {}
        //v.2.0.5
        [Toggle(_)] _Use_BaseAs1st ("Use BaseMap as 1st_ShadeMap", Float ) = 0
        _1st_ShadeColor ("1st_ShadeColor", Color) = (1,1,1,1)
        [Toggle(_)] _Is_LightColor_1st_Shade ("Is_LightColor_1st_Shade", Float ) = 1
        _2nd_ShadeMap ("2nd_ShadeMap", 2D) = "white" {}
        //v.2.0.5
        [Toggle(_)] _Use_1stAs2nd ("Use 1st_ShadeMap as 2nd_ShadeMap", Float ) = 0
        _2nd_ShadeColor ("2nd_ShadeColor", Color) = (1,1,1,1)
        [Toggle(_)] _Is_LightColor_2nd_Shade ("Is_LightColor_2nd_Shade", Float ) = 1
        _NormalMap ("NormalMap", 2D) = "bump" {}
        _BumpScale ("Normal Scale", Range(0, 1)) = 1
        [Toggle(_)] _Is_NormalMapToBase ("Is_NormalMapToBase", Float ) = 0
        //v.2.0.4.4
        [Toggle(_)] _Set_SystemShadowsToBase ("Set_SystemShadowsToBase", Float ) = 1
        _Tweak_SystemShadowsLevel ("Tweak_SystemShadowsLevel", Range(-0.5, 0.5)) = 0
        //v.2.0.6
        [HideInInspector] _BaseColor_Step ("BaseColor_Step", Range(0, 1)) = 0.5
        [HideInInspector] _BaseShade_Feather ("Base/Shade_Feather", Range(0.0001, 1)) = 0.0001
        [HideInInspector] _ShadeColor_Step ("ShadeColor_Step", Range(0, 1)) = 0
        [HideInInspector] _1st2nd_Shades_Feather ("1st/2nd_Shades_Feather", Range(0.0001, 1)) = 0.0001
        _1st_ShadeColor_Step ("1st_ShadeColor_Step", Range(0, 1)) = 0.5
        _1st_ShadeColor_Feather ("1st_ShadeColor_Feather", Range(0.0001, 1)) = 0.0001
        _2nd_ShadeColor_Step ("2nd_ShadeColor_Step", Range(0, 1)) = 0
        _2nd_ShadeColor_Feather ("2nd_ShadeColor_Feather", Range(0.0001, 1)) = 0.0001
        _ShadingGradeMap ("ShadingGradeMap", 2D) = "white" {}
        //v.2.0.6
        _Tweak_ShadingGradeMapLevel ("Tweak_ShadingGradeMapLevel", Range(-0.5, 0.5)) = 0
        _BlurLevelSGM ("Blur Level of ShadingGradeMap", Range(0, 10)) = 0
        //
        _HighColor ("HighColor", Color) = (0,0,0,1)
//v.2.0.4 HighColor_Tex
        _HighColor_Tex ("HighColor_Tex", 2D) = "white" {}
        [Toggle(_)] _Is_LightColor_HighColor ("Is_LightColor_HighColor", Float ) = 1
        [Toggle(_)] _Is_NormalMapToHighColor ("Is_NormalMapToHighColor", Float ) = 0
        _HighColor_Power ("HighColor_Power", Range(0, 1)) = 0
        [Toggle(_)] _Is_SpecularToHighColor ("Is_SpecularToHighColor", Float ) = 0
        [Toggle(_)] _Is_BlendAddToHiColor ("Is_BlendAddToHiColor", Float ) = 0
        [Toggle(_)] _Is_UseTweakHighColorOnShadow ("Is_UseTweakHighColorOnShadow", Float ) = 0
        _TweakHighColorOnShadow ("TweakHighColorOnShadow", Range(0, 1)) = 0
//ハイカラーマスク.
        _Set_HighColorMask ("Set_HighColorMask", 2D) = "white" {}
        _Tweak_HighColorMaskLevel ("Tweak_HighColorMaskLevel", Range(-1, 1)) = 0
        [Toggle(_)] _RimLight ("RimLight", Float ) = 0
        _RimLightColor ("RimLightColor", Color) = (1,1,1,1)
        [Toggle(_)] _Is_LightColor_RimLight ("Is_LightColor_RimLight", Float ) = 1
        [Toggle(_)] _Is_NormalMapToRimLight ("Is_NormalMapToRimLight", Float ) = 0
        _RimLight_Power ("RimLight_Power", Range(0, 1)) = 0.1
        _RimLight_InsideMask ("RimLight_InsideMask", Range(0.0001, 1)) = 0.0001
        [Toggle(_)] _RimLight_FeatherOff ("RimLight_FeatherOff", Float ) = 0
//リムライト追加プロパティ.
        [Toggle(_)] _LightDirection_MaskOn ("LightDirection_MaskOn", Float ) = 0
        _Tweak_LightDirection_MaskLevel ("Tweak_LightDirection_MaskLevel", Range(0, 0.5)) = 0
        [Toggle(_)] _Add_Antipodean_RimLight ("Add_Antipodean_RimLight", Float ) = 0
        _Ap_RimLightColor ("Ap_RimLightColor", Color) = (1,1,1,1)
        [Toggle(_)] _Is_LightColor_Ap_RimLight ("Is_LightColor_Ap_RimLight", Float ) = 1
        _Ap_RimLight_Power ("Ap_RimLight_Power", Range(0, 1)) = 0.1
        [Toggle(_)] _Ap_RimLight_FeatherOff ("Ap_RimLight_FeatherOff", Float ) = 0
//リムライトマスク.
        _Set_RimLightMask ("Set_RimLightMask", 2D) = "white" {}
        _Tweak_RimLightMaskLevel ("Tweak_RimLightMaskLevel", Range(-1, 1)) = 0
//ここまで.
        [Toggle(_)] _MatCap ("MatCap", Float ) = 0
        _MatCap_Sampler ("MatCap_Sampler", 2D) = "black" {}
        //v.2.0.6
        _BlurLevelMatcap ("Blur Level of MatCap_Sampler", Range(0, 10)) = 0
        _MatCapColor ("MatCapColor", Color) = (1,1,1,1)
        [Toggle(_)] _Is_LightColor_MatCap ("Is_LightColor_MatCap", Float ) = 1
        [Toggle(_)] _Is_BlendAddToMatCap ("Is_BlendAddToMatCap", Float ) = 1
        _Tweak_MatCapUV ("Tweak_MatCapUV", Range(-0.5, 0.5)) = 0
        _Rotate_MatCapUV ("Rotate_MatCapUV", Range(-1, 1)) = 0
        //v.2.0.6
        [Toggle(_)] _CameraRolling_Stabilizer ("Activate CameraRolling_Stabilizer", Float ) = 0
        [Toggle(_)] _Is_NormalMapForMatCap ("Is_NormalMapForMatCap", Float ) = 0
        _NormalMapForMatCap ("NormalMapForMatCap", 2D) = "bump" {}
        _BumpScaleMatcap ("Scale for NormalMapforMatCap", Range(0, 1)) = 1
        _Rotate_NormalMapForMatCapUV ("Rotate_NormalMapForMatCapUV", Range(-1, 1)) = 0
        [Toggle(_)] _Is_UseTweakMatCapOnShadow ("Is_UseTweakMatCapOnShadow", Float ) = 0
        _TweakMatCapOnShadow ("TweakMatCapOnShadow", Range(0, 1)) = 0
//MatcapMask
        _Set_MatcapMask ("Set_MatcapMask", 2D) = "white" {}
        _Tweak_MatcapMaskLevel ("Tweak_MatcapMaskLevel", Range(-1, 1)) = 0
        [Toggle(_)] _Inverse_MatcapMask ("Inverse_MatcapMask", Float ) = 0
        //v.2.0.5
        [Toggle(_)] _Is_Ortho ("Orthographic Projection for MatCap", Float ) = 0
        //v.2.0.7 Emissive
        [KeywordEnum(SIMPLE,ANIMATION)] _EMISSIVE("EMISSIVE MODE", Float) = 0
        _Emissive_Tex ("Emissive_Tex", 2D) = "white" {}
        [HDR]_Emissive_Color ("Emissive_Color", Color) = (0,0,0,1)
        _Base_Speed ("Base_Speed", Float ) = 0
        _Scroll_EmissiveU ("Scroll_EmissiveU", Range(-1, 1)) = 0
        _Scroll_EmissiveV ("Scroll_EmissiveV", Range(-1, 1)) = 0
        _Rotate_EmissiveUV ("Rotate_EmissiveUV", Float ) = 0
        [Toggle(_)] _Is_PingPong_Base ("Is_PingPong_Base", Float ) = 0
        [Toggle(_)] _Is_ColorShift ("Activate ColorShift", Float ) = 0
        [HDR]_ColorShift ("ColorSift", Color) = (0,0,0,1)
        _ColorShift_Speed ("ColorShift_Speed", Float ) = 0
        [Toggle(_)] _Is_ViewShift ("Activate ViewShift", Float ) = 0
        [HDR]_ViewShift ("ViewSift", Color) = (0,0,0,1)
        [Toggle(_)] _Is_ViewCoord_Scroll ("Is_ViewCoord_Scroll", Float ) = 0
        //
//Outline
        [KeywordEnum(NML,POS)] _OUTLINE("OUTLINE MODE", Float) = 0
        _Outline_Width ("Outline_Width", Float ) = 0
        _Farthest_Distance ("Farthest_Distance", Float ) = 100
        _Nearest_Distance ("Nearest_Distance", Float ) = 0.5
        _Outline_Sampler ("Outline_Sampler", 2D) = "white" {}
        _Outline_Color ("Outline_Color", Color) = (0.5,0.5,0.5,1)
        [Toggle(_)] _Is_BlendBaseColor ("Is_BlendBaseColor", Float ) = 0
        [Toggle(_)] _Is_LightColor_Outline ("Is_LightColor_Outline", Float ) = 1
        //v.2.0.4
        [Toggle(_)] _Is_OutlineTex ("Is_OutlineTex", Float ) = 0
        _OutlineTex ("OutlineTex", 2D) = "white" {}
        //Offset parameter
        _Offset_Z ("Offset_Camera_Z", Float) = 0
        //v.2.0.4.3 Baked Nrmal Texture for Outline
        [Toggle(_)] _Is_BakedNormal ("Is_BakedNormal", Float ) = 0
        _BakedNormal ("Baked Normal for Outline", 2D) = "white" {}
        //GI Intensity
        _GI_Intensity ("GI_Intensity", Range(0, 1)) = 0
        //For VR Chat under No effective light objects
        _Unlit_Intensity ("Unlit_Intensity", Range(0.001, 4)) = 1
        //v.2.0.5 
        [Toggle(_)] _Is_Filter_LightColor ("VRChat : SceneLights HiCut_Filter", Float ) = 0
        //Built-in Light Direction
        [Toggle(_)] _Is_BLD ("Advanced : Activate Built-in Light Direction", Float ) = 0
        _Offset_X_Axis_BLD (" Offset X-Axis (Built-in Light Direction)", Range(-1, 1)) = -0.05
        _Offset_Y_Axis_BLD (" Offset Y-Axis (Built-in Light Direction)", Range(-1, 1)) = 0.09
        [Toggle(_)] _Inverse_Z_Axis_BLD (" Inverse Z-Axis (Built-in Light Direction)", Float ) = 1
        //Tessellation
        _TessEdgeLength("DX11 Tess : Edge length", Range(2, 50)) = 5
        _TessPhongStrength("DX11 Tess : Phong Strength", Range(0, 1)) = 0.5
        _TessExtrusionAmount("DX11 Tess : Extrusion Amount", Range(-0.005, 0.005)) = 0.0
        
    }
    SubShader {
        Tags {
            "Queue"="AlphaTest-1"    //StencilMask Opaque and _Clipping
            "RenderType"="Opaque"
        }

        UsePass "UnityChanToonShader/Tessellation/Toon_ShadingGradeMap_StencilMask/OUTLINE"
        UsePass "UnityChanToonShader/Tessellation/Toon_ShadingGradeMap_StencilMask/FORWARD"
        UsePass "UnityChanToonShader/Tessellation/Toon_ShadingGradeMap_StencilMask/SHADOWCASTER"

    }
    FallBack "Legacy Shaders/VertexLit"
    CustomEditor "UnityChan.UTS2GUI"
}

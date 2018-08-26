//Unitychan Toon Shader ver.2.0
//v.2.0.4.3
Shader "UnityChanToonShader/Tessellation/Helper/Toon_OutlineObject" {
    Properties {
        _BaseMap ("BaseMap", 2D) = "white" {}
        _BaseColor ("BaseColor", Color) = (1,1,1,1)
        [MaterialToggle] _Is_LightColor_Base ("Is_LightColor_Base", Float ) = 1
//Outline
        [KeywordEnum(NML,POS)] _OUTLINE("OUTLINE MODE", Float) = 0
        _Outline_Width ("Outline_Width", Float ) = 1
        _Farthest_Distance ("Farthest_Distance", Float ) = 10
        _Nearest_Distance ("Nearest_Distance", Float ) = 0.5
        _Outline_Sampler ("Outline_Sampler", 2D) = "white" {}
        _Outline_Color ("Outline_Color", Color) = (0.5,0.5,0.5,1)
        [MaterialToggle] _Is_BlendBaseColor ("Is_BlendBaseColor", Float ) = 0
        //v.2.0.4
        [MaterialToggle] _Is_OutlineTex ("Is_OutlineTex", Float ) = 0
        _OutlineTex ("OutlineTex", 2D) = "white" {}
        //Offset parameter
        _Offset_Z ("Offset_Camera_Z", Float) = 0
        //v.2.0.4.3 Baked Nrmal Texture for Outline
        [MaterialToggle] _Is_BakedNormal ("Is_BakedNormal", Float ) = 0
        _BakedNormal ("Baked Normal for Outline", 2D) = "white" {}
        //Tessellation
        _TessEdgeLength( "Tess Edge length", Range( 2,50 ) ) = 5
        _TessPhongStrength( "Tess Phong Strengh", Range( 0,1 ) ) = 0.5
        _TessExtrusionAmount( "TessExtrusionAmount", Float ) = 0.0
    }
    SubShader {
        Tags {
            "RenderType"="Opaque"
        }
        Pass {
            Name "Outline"
            Tags {
            }
            Cull Front



            CGPROGRAM
            //Tessellation
            #define TESSELLATION_ON
            #pragma target 5.0
            #pragma vertex tess_VertexInput
            #pragma hull hs_VertexInput
            #pragma domain ds_surf
            //#pragma vertex vert

            #pragma fragment frag
            #include "UnityCG.cginc"
            //#pragma fragmentoption ARB_precision_hint_fastest
            //#pragma multi_compile_shadowcaster
            //#pragma multi_compile_fog
            #pragma only_renderers d3d9 d3d11 glcore gles gles3 metal xboxone ps4 switch
            //Tessellation
            //#pragma target 3.0
            //V.2.0.4
            #pragma multi_compile _IS_OUTLINE_CLIPPING_NO 
            #pragma multi_compile _OUTLINE_NML _OUTLINE_POS
            //Tessellation
            //アウトライン処理は以下のUCTS_Outline_Tess.cgincへ.
            #include "UCTS_Outline_Tess.cginc"
            ENDCG
        }
    }
    FallBack "Legacy Shaders/VertexLit"
}

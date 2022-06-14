//OutlineObjectBrightnessTest
//nobuyuki@unity3d.com
Shader "Test/OutlineObjectBrightnessTest" {
    Properties {
        _Outline_Width ("Outline_Width", Float ) = 0
        _Outline_Color ("Outline_Color", Color) = (0.5,0.5,0.5,1)
        _Unlit_Intensity ("Unlit_Intensity", Range(0.001, 4)) = 1
        _TestMode ("Test_Mode", Int) = 1
    }
    SubShader {
        Tags {
            "RenderType"="Opaque"
        }
        Pass {
            Name "Outline"
            Tags {
                "LightMode"="ForwardBase"
            }
            Cull Front

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"
            #pragma only_renderers d3d9 d3d11 glcore gles gles3 metal vulkan xboxone ps4 switch
            #pragma target 3.0
 
            uniform float4 _LightColor0;
            uniform float _Unlit_Intensity;
            uniform float _Outline_Width;
            uniform float4 _Outline_Color;
            uniform int _TestMode;
            //ifによる分岐処理対応
            uniform float3 ambientSkyColor;
            uniform float3 envLightSource_GradientEquator;
            uniform float3 envLightSource_SkyboxIntensity;
            //

            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 texcoord0 : TEXCOORD0;
                // v.2.0.9
                UNITY_VERTEX_INPUT_INSTANCE_ID
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                // v.2.0.9
                UNITY_VERTEX_INPUT_INSTANCE_ID
                UNITY_VERTEX_OUTPUT_STEREO
            };

            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                UNITY_SETUP_INSTANCE_ID(v);
                UNITY_TRANSFER_INSTANCE_ID(v, o);
                UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);
                o.uv0 = v.texcoord0;
                float Set_Outline_Width = _Outline_Width;
                o.pos = UnityObjectToClipPos(float4(v.vertex.xyz + v.normal*Set_Outline_Width,1));                
                return o;
            }
            float4 frag(VertexOutput i) : SV_Target{
                UNITY_SETUP_INSTANCE_ID(i);
                UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX(i);
//テスト箇所
                //v.2.0.7.5　元々の方式
                float3 envLightSource_Origin = unity_AmbientSky.rgb>0.05 ? unity_AmbientSky.rgb : half3(0.05,0.05,0.05);
                //グラデーションの明るさを赤道領域から取得する方式
                envLightSource_GradientEquator = unity_AmbientEquator.rgb >0.05 ? unity_AmbientEquator.rgb : half3(0.05,0.05,0.05);
                //コミュニティより提案された、UTS2トゥーンマテリアル側でのUnlit_Intensityでの明るさの取得方式
                envLightSource_SkyboxIntensity = max(ShadeSH9(half4(0.0,0.0,0.0,1.0)),ShadeSH9(half4(0.0,-1.0,0.0,1.0))).rgb;
        
               if(_TestMode == 1){
                    //v.2.0.7.5　元々の方式
                    ambientSkyColor = envLightSource_Origin*_Unlit_Intensity;
                }
                 else if(_TestMode == 2){
                    //コミュニティより提案された、UTS2トゥーンマテリアル側での明るさの取得方式（環境ライトソースがColorの黒だと反応しない）
                    ambientSkyColor =  envLightSource_SkyboxIntensity;
                }
                else{
                    //ShadeSH9から明るさがとれる時には優先的に取得する方式（例外条件が少ない）
                    ambientSkyColor = envLightSource_SkyboxIntensity.rgb>0.0 ? envLightSource_SkyboxIntensity*_Unlit_Intensity : envLightSource_GradientEquator*_Unlit_Intensity;
                }
//
                float3 lightColor = _LightColor0.rgb >0.05 ? _LightColor0.rgb : ambientSkyColor.rgb;
                float lightColorIntensity = (0.299*lightColor.r + 0.587*lightColor.g + 0.114*lightColor.b);
                lightColor = lightColorIntensity<1 ? lightColor : lightColor/lightColorIntensity;
                float3 Set_Outline_Color = _Outline_Color.rgb*lightColor;
                return float4(Set_Outline_Color,1.0);
            }
 //
            ENDCG
        }
    }
    FallBack "Legacy Shaders/VertexLit"
}

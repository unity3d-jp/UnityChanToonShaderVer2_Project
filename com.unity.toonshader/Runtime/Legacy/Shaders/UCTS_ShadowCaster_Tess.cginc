//Unity Toon Shader/Legacy
//nobuyuki@unity3d.com
//toshiyuki@unity3d.com (Intengrated) 
//https://github.com/unity3d-jp/UnityChanToonShaderVer2_Project
// ※Tessellation support
//   The corresponding code was adapted from Nora's https://github.com/Stereoarts/UnityChanToonShaderVer2_Tess.
//

#include "UCTS_Input.cginc"


//Tessellation OFF
#ifndef TESSELLATION_ON
            struct VertexInput {
                float4 vertex : POSITION;
#ifdef _IS_CLIPPING_MODE
//_Clipping
                 float2 texcoord0 : TEXCOORD0;
#elif _IS_CLIPPING_TRANSMODE
//_TransClipping
                 float2 texcoord0 : TEXCOORD0;
#elif _IS_CLIPPING_OFF
//Default
#endif
            };
#endif

            struct VertexOutput {
                V2F_SHADOW_CASTER;
#ifdef _IS_CLIPPING_MODE
//_Clipping
                float2 uv0 : TEXCOORD1;
#elif _IS_CLIPPING_TRANSMODE
//_TransClipping
                float2 uv0 : TEXCOORD1;
#elif _IS_CLIPPING_OFF
//Default
#endif
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
#ifdef _IS_CLIPPING_MODE
//_Clipping
                o.uv0 = v.texcoord0;
#elif _IS_CLIPPING_TRANSMODE
//_TransClipping
                o.uv0 = v.texcoord0;
#elif _IS_CLIPPING_OFF
//Default
#endif
                o.pos = UnityObjectToClipPos(v.vertex );
                TRANSFER_SHADOW_CASTER(o)
                return o;
            }

//Tessellation ON
#ifdef TESSELLATION_ON
#ifdef UNITY_CAN_COMPILE_TESSELLATION
            // tessellation domain shader
            [UNITY_domain("tri")]
            VertexOutput ds_surf(UnityTessellationFactors tessFactors, const OutputPatch<InternalTessInterp_VertexInput, 3> vi, float3 bary : SV_DomainLocation)
            {
                VertexInput v = _ds_VertexInput(tessFactors, vi, bary);
                return vert(v);
            }
#endif // UNITY_CAN_COMPILE_TESSELLATION
#endif // TESSELLATION_ON

            float4 frag(VertexOutput i) : SV_TARGET {
#ifdef _IS_CLIPPING_MODE
//_Clipping
                float2 Set_UV0 = i.uv0;
                float4 _ClippingMask_var = tex2D(_ClippingMask,TRANSFORM_TEX(Set_UV0, _ClippingMask));
                float Set_Clipping = saturate((lerp( _ClippingMask_var.r, (1.0 - _ClippingMask_var.r), _Inverse_Clipping )+_Clipping_Level));
                clip(Set_Clipping - 0.5);
#elif _IS_CLIPPING_TRANSMODE
//_TransClipping
                float2 Set_UV0 = i.uv0;
                float4 _ClippingMask_var = tex2D(_ClippingMask,TRANSFORM_TEX(Set_UV0, _ClippingMask));
                float4 _MainTex_var = SAMPLE_TEXTURE2D(_MainTex,sampler_MainTex,TRANSFORM_TEX(Set_UV0, _MainTex));
                float Set_MainTexAlpha = _MainTex_var.a;
                float _IsBaseMapAlphaAsClippingMask_var = lerp( _ClippingMask_var.r, Set_MainTexAlpha, _IsBaseMapAlphaAsClippingMask );
                float _Inverse_Clipping_var = lerp( _IsBaseMapAlphaAsClippingMask_var, (1.0 - _IsBaseMapAlphaAsClippingMask_var), _Inverse_Clipping );
                float Set_Clipping = saturate((_Inverse_Clipping_var+_Clipping_Level));
                clip(Set_Clipping - 0.5);
#elif _IS_CLIPPING_OFF
//Default
#endif
                SHADOW_CASTER_FRAGMENT(i)
            }

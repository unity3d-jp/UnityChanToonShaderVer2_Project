//Unity Toon Shader/Legacy
//nobuyuki@unity3d.com
//toshiyuki@unity3d.com (Intengrated) 


#include "UCTS_Input.cginc"


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
                UNITY_VERTEX_INPUT_INSTANCE_ID
            };
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
                UNITY_VERTEX_INPUT_INSTANCE_ID
                UNITY_VERTEX_OUTPUT_STEREO
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                UNITY_SETUP_INSTANCE_ID(v);
                UNITY_TRANSFER_INSTANCE_ID(v, o);
                UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);
		
#ifdef _IS_CLIPPING_MODE
//_Clipping
                o.uv0 = v.texcoord0;
#elif _IS_CLIPPING_TRANSMODE
//_TransClipping
                o.uv0 = v.texcoord0;
#elif _IS_CLIPPING_OFF
//Default
#endif
                o.pos = UnityObjectToClipPos( v.vertex );
                TRANSFER_SHADOW_CASTER(o)
                return o;
            }
            float4 frag(VertexOutput i) : SV_TARGET {
                UNITY_SETUP_INSTANCE_ID(i);
#ifdef _IS_CLIPPING_MODE
//_Clipping
                float2 Set_UV0 = i.uv0;
                float4 _ClippingMask_var = tex2D(_ClippingMask,TRANSFORM_TEX(Set_UV0, _ClippingMask));
                float Set_Clipping = saturate((lerp( _ClippingMask_var.r, (1.0 - _ClippingMask_var.r), _Inverse_Clipping )+_Clipping_Level));
                clip(Set_Clipping - 0.5);
#elif _IS_CLIPPING_TRANSMODE
//_TransClipping
                float2 Set_UV0 = i.uv0;

                float4 _ClippingMask_var = tex2D(_ClippingMask, TRANSFORM_TEX(Set_UV0, _ClippingMask));
                float4 _MainTex_var = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, TRANSFORM_TEX(Set_UV0, _MainTex));

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

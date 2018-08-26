// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// UCTS_Outline.cginc
// 2017/03/08 N.Kobayashi (Unity Technologies Japan)
// カメラオフセット付きアウトライン（BaseColorライトカラー反映修正版）
// 2017/06/05 PS4対応版
// Ver.2.0.4
// 2018/02/05 Outline Tex対応版
// #pragma multi_compile _IS_OUTLINE_CLIPPING_NO _IS_OUTLINE_CLIPPING_YES 
// _IS_OUTLINE_CLIPPING_YESは、Clippigマスクを使用するシェーダーでのみ使用できる. OutlineのブレンドモードにBlend SrcAlpha OneMinusSrcAlphaを追加すること.
//
            //uniform float4 _LightColor0;
            uniform float4 _BaseColor;
            uniform sampler2D _BaseMap; uniform float4 _BaseMap_ST;
            uniform float _Outline_Width;
            uniform float _Farthest_Distance;
            uniform float _Nearest_Distance;
            uniform sampler2D _Outline_Sampler; uniform float4 _Outline_Sampler_ST;
            uniform float4 _Outline_Color;
            uniform fixed _Is_BlendBaseColor;
            uniform fixed _Is_LightColor_Base;
            uniform float _Offset_Z;
            //v2.0.4
            uniform sampler2D _OutlineTex; uniform float4 _OutlineTex_ST;
            uniform fixed _Is_OutlineTex;
//v.2.0.4
#ifdef _IS_OUTLINE_CLIPPING_YES
            uniform sampler2D _ClippingMask; uniform float4 _ClippingMask_ST;
            uniform float _Clipping_Level;
            uniform fixed _Inverse_Clipping;
            uniform fixed _IsBaseMapAlphaAsClippingMask;
#endif
			//static const float3 grayscale_vector = float3(0, 0.3823529, 0.01845836);
			static const float softGI = .5;

			// ambient color smoothener
			fixed3 DecodeLightProbe_Cubed( fixed3 N ){
            return (1-softGI)*ShadeSH9(float4(N, 1))
					+ (softGI)*ShadeSH9(float4(0,0,0,1));
            }

            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
				float4 posWorld : TEXCOORD1;
                float2 uv0 : TEXCOORD0;
				float3 normalDir : TEXCOORD2;
				LIGHTING_COORDS(3,4)
                UNITY_FOG_COORDS(5)
				float3 vertexLighting : TEXCOORD6;
            };


			
//// vert			
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
				o.normalDir = UnityObjectToWorldNormal(v.normal);
				o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                float4 objPos = mul ( unity_ObjectToWorld, float4(0,0,0,1) );
                float3 lightColor = _LightColor0.rgb;
                float2 Set_UV0 = o.uv0;
                float4 _Outline_Sampler_var = tex2Dlod(_Outline_Sampler,float4(TRANSFORM_TEX(Set_UV0, _Outline_Sampler),0.0,0));
                float Set_Outline_Width = (_Outline_Width*0.001*smoothstep( _Farthest_Distance, _Nearest_Distance, distance(o.posWorld.rgb,_WorldSpaceCameraPos) )*_Outline_Sampler_var.rgb).r;
				//float Set_Outline_Width = (_Outline_Width*0.001*smoothstep( _Farthest_Distance, _Nearest_Distance, distance(objPos.rgb,_WorldSpaceCameraPos) )*_Outline_Sampler_var.rgb).r;
                float3 viewDirection = (_WorldSpaceCameraPos.xyz - o.pos.xyz);
				float4 viewDirectionVP = mul(UNITY_MATRIX_VP, float4(viewDirection.xyz, 1));
//v2.0.4
#if defined(UNITY_REVERSED_Z)
                _Offset_Z = _Offset_Z * -0.01;
#else
                _Offset_Z = _Offset_Z * 0.01;
#endif
//v2.0.4
#ifdef _OUTLINE_NML
                o.pos = UnityObjectToClipPos(float4(v.vertex.xyz + v.normal*Set_Outline_Width,1) );
#elif _OUTLINE_POS
                Set_Outline_Width = Set_Outline_Width*2;
                o.pos = UnityObjectToClipPos(float4(v.vertex.xyz + normalize(v.vertex)*Set_Outline_Width,1) );
#endif
                UNITY_TRANSFER_FOG(o,o.pos);					
                o.pos.z = o.pos.z + _Offset_Z * viewDirectionVP.z;				
				TRANSFER_VERTEX_TO_FRAGMENT(o);
#ifdef VERTEXLIGHT_ON
				o.vertexLighting = Shade4PointLights(
					unity_4LightPosX0, unity_4LightPosY0,unity_4LightPosZ0,
					unity_LightColor[0],unity_LightColor[1],unity_LightColor[2],unity_LightColor[3],
					unity_4LightAtten0*1, o.posWorld, o.normalDir);
#endif  				
                return o;
            }
			
		
		
//// frag			
            float4 frag(VertexOutput i) : SV_Target{
                //float4 objPos = mul ( unity_ObjectToWorld, float4(0,0,0,1) );
				float attenuation = LIGHT_ATTENUATION(i);
				float attenRamp = (-(pow(1-attenuation, 2)) + 1);
				float3 lightColor = _LightColor0.rgb * attenRamp * 1;
#ifdef _IS_PASS_FWDBASE				
				//lightColor += DecodeLightProbe_Cubed(i.normalDir);
				lightColor = max(lightColor, DecodeLightProbe_Cubed(i.normalDir));
				lightColor = max(lightColor, i.vertexLighting * 0.1);
#endif				
                float2 Set_UV0 = i.uv0;
                float4 _BaseMap_var = tex2D(_BaseMap,TRANSFORM_TEX(Set_UV0, _BaseMap));
                float3 _BaseColorMap_var = (_BaseColor.rgb*_BaseMap_var.rgb);
                float3 Set_BaseColor = lerp(_BaseColorMap_var*lightColor, (_BaseColorMap_var*lightColor.rgb), _Is_LightColor_Base );
                //v.2.0.4
                float3 _Is_BlendBaseColor_var = lerp( (_Outline_Color.rgb*lightColor.rgb), (_Outline_Color.rgb*Set_BaseColor), _Is_BlendBaseColor );
                float3 _OutlineTex_var = tex2D(_OutlineTex,TRANSFORM_TEX(Set_UV0, _OutlineTex));
//v.2.0.4
#ifdef _IS_OUTLINE_CLIPPING_NO
                float3 Set_Outline_Color = lerp(_Is_BlendBaseColor_var, _OutlineTex_var.rgb*_Is_BlendBaseColor_var, _Is_OutlineTex );
				UNITY_APPLY_FOG(i.fogCoord, Set_Outline_Color);
                return fixed4(Set_Outline_Color,1);
#elif _IS_OUTLINE_CLIPPING_YES
                float4 _ClippingMask_var = tex2D(_ClippingMask,TRANSFORM_TEX(Set_UV0, _ClippingMask));
                float Set_BaseMapAlpha = _BaseMap_var.a;
                float _IsBaseMapAlphaAsClippingMask_var = lerp( _ClippingMask_var.r, Set_BaseMapAlpha, _IsBaseMapAlphaAsClippingMask );
                float _Inverse_Clipping_var = lerp( _IsBaseMapAlphaAsClippingMask_var, (1.0 - _IsBaseMapAlphaAsClippingMask_var), _Inverse_Clipping );
                float Set_Clipping = saturate((_Inverse_Clipping_var+_Clipping_Level));
                clip(Set_Clipping - 0.5);
                float4 Set_Outline_Color = lerp( float4(_Is_BlendBaseColor_var,Set_Clipping), float4((_OutlineTex_var.rgb*_Is_BlendBaseColor_var),Set_Clipping), _Is_OutlineTex );
				UNITY_APPLY_FOG(i.fogCoord, Set_Outline_Color);
                return Set_Outline_Color;
#endif
            }
// UCTS_Outline.cginc ここまで.

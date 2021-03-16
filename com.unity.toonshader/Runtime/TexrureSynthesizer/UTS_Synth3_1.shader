
Shader "Hidden/UnityToonShader/Synth3_1" {
	Properties{
		Source0("Source0 (RGB)", 2D) = "white" {}
        Source1("Source1 (A)", 2D) = "clear" {}
		Source2("Source2 (dummy)", 2D) = "clear" {}
		Source3("Source3 (dummy)", 2D) = "clear" {}
		[KeywordEnum(None, Front, Back)] _Cull("Culling", Int) = 2
	}

	SubShader{
		Tags {"Queue" = "Transparent" "IgnoreProjector" = "True" "RenderType" = "Transparent"}
		LOD 100
		Cull Off
		ZTest Off
		ZWrite Off


		Pass {
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma target 2.0
			#pragma multi_compile_fog

			#include "UnityCG.cginc"

			struct appdata_t {
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct v2f {
				float2 uv0 : TEXCOORD0;
				float2 uv1 : TEXCOORD1;	
				float4 vertex : SV_POSITION;
			};

			sampler2D Source0;
			sampler2D Source1;
			sampler2D Source2;
			sampler2D Source3;
			float4 Source0_TexelSize;	// https://docs.unity3d.com/ja/2019.4/Manual/SL-PropertiesInPrograms.html
			float4 Source1_TexelSize;
			float4 Source2_TexelSize;
			float4 Source3_TexelSize;
			float4 Source0_ST;
			float4 Source1_ST;
			float4 Source2_ST;
			float4 Source3_ST;

			v2f vert(appdata_t v)
			{
				v2f o;
				float2 uv0 = v.uv;
				float2 uv1 = v.uv;
#if UNITY_UV_STARTS_AT_TOP
				
				if (Source0_TexelSize.y < 0) {
					uv0.y = 1 - uv0.y;
				}
				if (Source1_TexelSize.y < 0) {
					uv1.y = 1 - uv1.y;
				}
#endif
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv0 = TRANSFORM_TEX(uv0, Source0);
				o.uv1 = TRANSFORM_TEX(uv1, Source1);				
				return o;
			}



			fixed4 frag(v2f i) : SV_Target
			{
				fixed4 col = tex2D(Source0, i.uv0);
				col.a = tex2D(Source1, i.uv1).r;

				return col;
			}
			ENDCG
		}
	}


}

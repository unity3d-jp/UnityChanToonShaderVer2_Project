Shader "Unlit/Alpha" {
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		_Color("Multiplicative color", Color) = (1.0, 1.0, 1.0, 1.0)

		[Header(Alpha)]
		[Enum(UnityEngine.Rendering.BlendMode)]  _SrcFactor("Source Factor", Int) = 1
		[Enum(UnityEngine.Rendering.BlendMode)] _DstFactor("Destination Factor", Int) = 0
		[Enum(UnityEngine.Rendering.BlendOp)] _BlendOp("Blend Operation", Int) = 0

		[Header(Other)]
		[Enum(UnityEngine.Rendering.CompareFunction)] _DepthComp("Depth Test", Int) = 8
		[Toggle] _ZWrite("Depth Write", Int) = 0
		[Enum(None,0,Alpha,1,Blue,2,Green,4,Red,8,All,15)] _ColorMask("Color Mask", Int) = 15
		[Enum(UnityEngine.Rendering.CullMode)] _CullMode("Cull Mode", Int) = 0
		_Extrude("Extrude", Float) = 0
	}
	SubShader {

		ColorMask [_ColorMask]

		Pass {
 			ZTest [_DepthComp] Cull [_CullMode] ZWrite [_ZWrite]

			Blend [_SrcFactor] [_DstFactor]
			BlendOp [_BlendOp]

			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma target 2.0

			#include "UnityCG.cginc"

			sampler2D _MainTex;
			uniform float4 _MainTex_ST;
			uniform float4 _Color;
			uniform float _Extrude;

			struct appdata_t {
				float4 vertex : POSITION;
				float3 normal : NORMAL;
				float2 texcoord : TEXCOORD0;
			};

			struct v2f {
				float4 vertex : SV_POSITION;
				float2 texcoord : TEXCOORD0;
			};

			v2f vert (appdata_t v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex + v.normal * _Extrude);
				o.texcoord = TRANSFORM_TEX(v.texcoord.xy, _MainTex);
				return o;
			}

			fixed4 frag (v2f i) : SV_Target
			{
				return tex2D(_MainTex, i.texcoord) * _Color;
			}
			ENDCG 

		}
	}
	Fallback Off 
}

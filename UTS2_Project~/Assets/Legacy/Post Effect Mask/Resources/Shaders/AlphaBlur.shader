Shader "Hidden/Post FX/AlphaBlur"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "" {}
		_BlurSize("BlurSize", Range(0, 20)) = 1
	}

	CGINCLUDE
		#include "UnityCG.cginc"

		struct appdata
		{
			float4 vertex : POSITION;
			float2 uv : TEXCOORD0;
		};

		struct v2f
		{
			float2 uv : TEXCOORD0;
			float4 vertex : SV_POSITION;
		};

		v2f vert (appdata v)
		{
			v2f o;
			o.vertex = UnityObjectToClipPos(v.vertex);
			o.uv = v.uv;
			return o;
		}

		sampler2D _MainTex;
		float4 _MainTex_TexelSize;
		float _BlurSize;

		//#define G(x, deviation) (1/(sqrt(2*UNITY_PI*deviation*deviation))) * exp(-(x*x / (2*deviation*deviation)))
		
		half4 horizontalBlurFrag(v2f i) : SV_Target {
			half4 sum = half4(0,0,0,0);

			#define GRABPIXELH(weight,kernelx) tex2D( _MainTex, float2(i.uv.x + _MainTex_TexelSize.x * kernelx*_BlurSize, i.uv.y)) * weight

			sum += GRABPIXELH(0.05, -4.0);
			sum += GRABPIXELH(0.09, -3.0);
			sum += GRABPIXELH(0.12, -2.0);
			sum += GRABPIXELH(0.15, -1.0);
			sum += GRABPIXELH(0.18,  0.0);
			sum += GRABPIXELH(0.15, +1.0);
			sum += GRABPIXELH(0.12, +2.0);
			sum += GRABPIXELH(0.09, +3.0);
			sum += GRABPIXELH(0.05, +4.0);

			return sum;
		}

		half4 verticalBlurFrag(v2f i) : SV_Target {
			half4 sum = half4(0,0,0,0);

			#define GRABPIXELV(weight,kernely) tex2D( _MainTex, float2(i.uv.x, i.uv.y + _MainTex_TexelSize.y * kernely*_BlurSize)) * weight

			sum += GRABPIXELV(0.05, -4.0);
			sum += GRABPIXELV(0.09, -3.0);
			sum += GRABPIXELV(0.12, -2.0);
			sum += GRABPIXELV(0.15, -1.0);
			sum += GRABPIXELV(0.18,  0.0);
			sum += GRABPIXELV(0.15, +1.0);
			sum += GRABPIXELV(0.12, +2.0);
			sum += GRABPIXELV(0.09, +3.0);
			sum += GRABPIXELV(0.05, +4.0);

			return sum;
		}
	ENDCG

	SubShader
	{
		// No culling or depth
		Cull Off ZWrite Off ZTest Always
		
		// write only alpha
		ColorMask A

		Pass
		{
			CGPROGRAM
				#pragma vertex vert
				#pragma fragment horizontalBlurFrag
			ENDCG
		}

		Pass
		{
			CGPROGRAM
				#pragma vertex vert
				#pragma fragment verticalBlurFrag
			ENDCG
		}
	}
}

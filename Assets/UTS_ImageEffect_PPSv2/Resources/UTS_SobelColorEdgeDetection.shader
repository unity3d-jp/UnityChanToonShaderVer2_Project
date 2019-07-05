Shader "Hidden/UnityChan/UTS_SobelColorEdgeDetection"
{
    HLSLINCLUDE

#if UNITY_VERSION >= 201830 //Unity2016.3.x以降.
	#include "Packages/com.unity.postprocessing/PostProcessing/Shaders/StdLib.hlsl"
#else //Unity2018.2.20f1では、Unity側のバグで分岐しないので注意.
	#include "PostProcessing/Shaders/StdLib.hlsl"
#endif

    	//Screen Color
		TEXTURE2D_SAMPLER2D(_MainTex, sampler_MainTex);
		float4 _MainTex_ST;

		//Parameters
		uniform float4 _EdgesColor;
		uniform float _FilterPower;
		uniform float _Threshold;

		inline float SobelFilter( float dx , float dy , TEXTURE2D_ARGS(tex, samplerTex), float2 uv )
		{
			float2 delta = float2(dx, dy);
			
			float4 hr = float4(0, 0, 0, 0);
			float4 vt = float4(0, 0, 0, 0);
			
			hr += SAMPLE_TEXTURE2D(tex, samplerTex, (uv + float2(-1.0, -1.0) * delta)) *  1.0;
			hr += SAMPLE_TEXTURE2D(tex, samplerTex, (uv + float2( 0.0, -1.0) * delta)) *  0.0;
			hr += SAMPLE_TEXTURE2D(tex, samplerTex, (uv + float2( 1.0, -1.0) * delta)) * -1.0;
			hr += SAMPLE_TEXTURE2D(tex, samplerTex, (uv + float2(-1.0,  0.0) * delta)) *  2.0;
			hr += SAMPLE_TEXTURE2D(tex, samplerTex, (uv + float2( 0.0,  0.0) * delta)) *  0.0;
			hr += SAMPLE_TEXTURE2D(tex, samplerTex, (uv + float2( 1.0,  0.0) * delta)) * -2.0;
			hr += SAMPLE_TEXTURE2D(tex, samplerTex, (uv + float2(-1.0,  1.0) * delta)) *  1.0;
			hr += SAMPLE_TEXTURE2D(tex, samplerTex, (uv + float2( 0.0,  1.0) * delta)) *  0.0;
			hr += SAMPLE_TEXTURE2D(tex, samplerTex, (uv + float2( 1.0,  1.0) * delta)) * -1.0;
			
			vt += SAMPLE_TEXTURE2D(tex, samplerTex, (uv + float2(-1.0, -1.0) * delta)) *  1.0;
			vt += SAMPLE_TEXTURE2D(tex, samplerTex, (uv + float2( 0.0, -1.0) * delta)) *  2.0;
			vt += SAMPLE_TEXTURE2D(tex, samplerTex, (uv + float2( 1.0, -1.0) * delta)) *  1.0;
			vt += SAMPLE_TEXTURE2D(tex, samplerTex, (uv + float2(-1.0,  0.0) * delta)) *  0.0;
			vt += SAMPLE_TEXTURE2D(tex, samplerTex, (uv + float2( 0.0,  0.0) * delta)) *  0.0;
			vt += SAMPLE_TEXTURE2D(tex, samplerTex, (uv + float2( 1.0,  0.0) * delta)) *  0.0;
			vt += SAMPLE_TEXTURE2D(tex, samplerTex, (uv + float2(-1.0,  1.0) * delta)) * -1.0;
			vt += SAMPLE_TEXTURE2D(tex, samplerTex, (uv + float2( 0.0,  1.0) * delta)) * -2.0;
			vt += SAMPLE_TEXTURE2D(tex, samplerTex, (uv + float2( 1.0,  1.0) * delta)) * -1.0;
			
			return (float)sqrt(hr * hr + vt * vt);
		}



        float4 Frag(VaryingsDefault i) : SV_Target
        {
			float dx = (_FilterPower/_ScreenParams.r);
			float dy = (_FilterPower/_ScreenParams.g);
			//Using i.texcoordStereo for UNITY_SINGLE_PASS_STEREO
			float Edges_value = saturate(lerp(0.0f,SobelFilter( dx , dy , _MainTex, sampler_MainTex, i.texcoordStereo ),_Threshold));
			float3 EdgesMask_Var = float3(Edges_value,Edges_value,Edges_value); // EdgesMask
			//Using i.texcoordStereo for UNITY_SINGLE_PASS_STEREO
			float4 SceneColor = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, i.texcoordStereo);
			float3 FinalColor = saturate((EdgesMask_Var*_EdgesColor.rgb)+(SceneColor.rgb-EdgesMask_Var));

			return float4(FinalColor,SceneColor.a);
        }

    ENDHLSL

    SubShader
    {
        Cull Off ZWrite Off ZTest Always

        Pass
        {
            HLSLPROGRAM

                #pragma vertex VertDefault
                #pragma fragment Frag

            ENDHLSL
        }
    }
}
// -----------------------------------------------------------------------------
// ref. StdLib.hlsl
// // Default vertex shaders

// struct AttributesDefault
// {
//     float3 vertex : POSITION;
// };

// struct VaryingsDefault
// {
//     float4 vertex : SV_POSITION;
//     float2 texcoord : TEXCOORD0;
//     float2 texcoordStereo : TEXCOORD1;
// };

// VaryingsDefault VertDefault(AttributesDefault v)
// {
//     VaryingsDefault o;
//     o.vertex = float4(v.vertex.xy, 0.0, 1.0);
//     o.texcoord = TransformTriangleVertexToUV(v.vertex.xy);

// #if UNITY_UV_STARTS_AT_TOP
//     o.texcoord = o.texcoord * float2(1.0, -1.0) + float2(0.0, 1.0);
// #endif

//     o.texcoordStereo = TransformStereoScreenSpaceTex(o.texcoord, 1.0);

//     return o;
// }

// float4 _UVTransform; // xy: scale, wz: translate

// VaryingsDefault VertUVTransform(AttributesDefault v)
// {
//     VaryingsDefault o;
//     o.vertex = float4(v.vertex.xy, 0.0, 1.0);
//     o.texcoord = TransformTriangleVertexToUV(v.vertex.xy) * _UVTransform.xy + _UVTransform.zw;
//     o.texcoordStereo = TransformStereoScreenSpaceTex(o.texcoord, 1.0);
//     return o;
// }

// #define TRANSFORM_TEX(tex,name) (tex.xy * name##_ST.xy + name##_ST.zw)

// #endif // UNITY_POSTFX_STDLIB

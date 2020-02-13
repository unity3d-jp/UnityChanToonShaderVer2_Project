#ifndef UNIVERSAL_SHADOW_CASTER_PASS_INCLUDED
#define UNIVERSAL_SHADOW_CASTER_PASS_INCLUDED

#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Shadows.hlsl"

//float3 _LightDirection;

struct UtsLight
{
	half3   direction;
	half3   color;
	half    distanceAttenuation;
	int     type;
};
struct Attributes
{
    float4 positionOS   : POSITION;
    float3 normalOS     : NORMAL;
    float2 texcoord     : TEXCOORD0;
    UNITY_VERTEX_INPUT_INSTANCE_ID
};

struct Varyings
{
    float2 uv           : TEXCOORD0;
    float4 positionCS   : SV_POSITION;
};

#define INIT_UTSLIGHT(utslight) \
            utslight.direction = 0; \
            utslight.color = 0; \
            utslight.distanceAttenuation = 0; \
            utslight.type = 0;

UtsLight GetMainUtsLight()
{
	UtsLight light;
	light.direction = _MainLightPosition.xyz;
	// unity_LightData.z is 1 when not culled by the culling mask, otherwise 0.
	light.distanceAttenuation = unity_LightData.z;
#if defined(LIGHTMAP_ON) || defined(_MIXED_LIGHTING_SUBTRACTIVE)
	// unity_ProbesOcclusion.x is the mixed light probe occlusion data
	light.distanceAttenuation *= unity_ProbesOcclusion.x;
#endif

	light.color = _MainLightColor.rgb;
	light.type = _MainLightPosition.w;
	return light;
}



UtsLight GetAdditionalPerObjectUtsLight(int perObjectLightIndex, float3 positionWS)
{
	// Abstraction over Light input constants
#if USE_STRUCTURED_BUFFER_FOR_LIGHT_DATA
	float4 lightPositionWS = _AdditionalLightsBuffer[perObjectLightIndex].position;
	half3 color = _AdditionalLightsBuffer[perObjectLightIndex].color.rgb;
	half4 distanceAndSpotAttenuation = _AdditionalLightsBuffer[perObjectLightIndex].attenuation;
	half4 spotDirection = _AdditionalLightsBuffer[perObjectLightIndex].spotDirection;
	half4 lightOcclusionProbeInfo = _AdditionalLightsBuffer[perObjectLightIndex].occlusionProbeChannels;
#else
	float4 lightPositionWS = _AdditionalLightsPosition[perObjectLightIndex];
	half3 color = _AdditionalLightsColor[perObjectLightIndex].rgb;
	half4 distanceAndSpotAttenuation = _AdditionalLightsAttenuation[perObjectLightIndex];
	half4 spotDirection = _AdditionalLightsSpotDir[perObjectLightIndex];
	half4 lightOcclusionProbeInfo = _AdditionalLightsOcclusionProbes[perObjectLightIndex];
#endif

	// Directional lights store direction in lightPosition.xyz and have .w set to 0.0.
	// This way the following code will work for both directional and punctual lights.
	float3 lightVector = lightPositionWS.xyz - positionWS * lightPositionWS.w;
	float distanceSqr = max(dot(lightVector, lightVector), HALF_MIN);

	half3 lightDirection = half3(lightVector * rsqrt(distanceSqr));
	half attenuation = DistanceAttenuation(distanceSqr, distanceAndSpotAttenuation.xy) * AngleAttenuation(spotDirection.xyz, lightDirection, distanceAndSpotAttenuation.zw);

	UtsLight light;
	light.direction = lightDirection;
	light.distanceAttenuation = attenuation;

	light.color = color;
	light.type = lightPositionWS.w;

	// In case we're using light probes, we can sample the attenuation from the `unity_ProbesOcclusion`
#if defined(LIGHTMAP_ON) || defined(_MIXED_LIGHTING_SUBTRACTIVE)
				// First find the probe channel from the light.
				// Then sample `unity_ProbesOcclusion` for the baked occlusion.
				// If the light is not baked, the channel is -1, and we need to apply no occlusion.

				// probeChannel is the index in 'unity_ProbesOcclusion' that holds the proper occlusion value.
	int probeChannel = lightOcclusionProbeInfo.x;

	// lightProbeContribution is set to 0 if we are indeed using a probe, otherwise set to 1.
	half lightProbeContribution = lightOcclusionProbeInfo.y;

	half probeOcclusionValue = unity_ProbesOcclusion[probeChannel];
	light.distanceAttenuation *= max(probeOcclusionValue, lightProbeContribution);
#endif

	return light;
}

UtsLight GetAdditionalUtsLight(uint i, float3 positionWS)
{
	int perObjectLightIndex = GetPerObjectLightIndex(i);
	return GetAdditionalPerObjectUtsLight(perObjectLightIndex, positionWS);
}


UtsLight DetermineUTS_MainLight(float3 posW)
{

	UtsLight mainLight;
	mainLight.direction = 0;
	mainLight.color = 0;
	mainLight.distanceAttenuation = 0;

	mainLight.type = 0;
	int mainLightIndex = -2;
	UtsLight nextLight = GetMainUtsLight();
	if (nextLight.distanceAttenuation > mainLight.distanceAttenuation && nextLight.type == 0)
	{
		mainLight = nextLight;
		mainLightIndex = -1;
	}
	int lightCount = GetAdditionalLightsCount();
	for (int ii = 0; ii < lightCount; ++ii)
	{
		nextLight = GetAdditionalUtsLight(ii, posW);
		if (nextLight.distanceAttenuation > mainLight.distanceAttenuation && nextLight.type == 0)
		{
			mainLight = nextLight;
			mainLightIndex = ii;
		}
	}

	return mainLight;
}
int DetermineUTS_MainLightIndex(float3 posW)
{
	UtsLight mainLight;
	INIT_UTSLIGHT(mainLight);

	int mainLightIndex = -2;
	UtsLight nextLight = GetMainUtsLight();
	if (nextLight.distanceAttenuation > mainLight.distanceAttenuation && nextLight.type == 0)
	{
		mainLight = nextLight;
		mainLightIndex = -1;
	}
	int lightCount = GetAdditionalLightsCount();
	for (int ii = 0; ii < lightCount; ++ii)
	{
		nextLight = GetAdditionalUtsLight(ii, posW);
		if (nextLight.distanceAttenuation > mainLight.distanceAttenuation && nextLight.type == 0)
		{
			mainLight = nextLight;
			mainLightIndex = ii;
		}
	}

	return mainLightIndex;
}
float4 GetShadowPositionHClip(Attributes input, half3 lightDir)
{
    float3 positionWS = TransformObjectToWorld(input.positionOS.xyz);
    float3 normalWS = TransformObjectToWorldNormal(input.normalOS);

    float4 positionCS = TransformWorldToHClip(ApplyShadowBias(positionWS, normalWS, lightDir));

#if UNITY_REVERSED_Z
    positionCS.z = min(positionCS.z, positionCS.w * UNITY_NEAR_CLIP_VALUE);
#else
    positionCS.z = max(positionCS.z, positionCS.w * UNITY_NEAR_CLIP_VALUE);
#endif

    return positionCS;
}

Varyings ShadowPassVertex(Attributes input)
{
    Varyings output;
    UNITY_SETUP_INSTANCE_ID(input);
	float3 positionWS = TransformObjectToWorld(input.positionOS.xyz);
	UtsLight light = DetermineUTS_MainLight(positionWS);

    output.uv = TRANSFORM_TEX(input.texcoord, _BaseMap);
    output.positionCS = GetShadowPositionHClip(input,light.direction);

    return output;
}

half4 ShadowPassFragment(Varyings input) : SV_TARGET
{
    Alpha(SampleAlbedoAlpha(input.uv, TEXTURE2D_ARGS(_BaseMap, sampler_BaseMap)).a, _BaseColor, _Cutoff);
    return 0;
}

#endif

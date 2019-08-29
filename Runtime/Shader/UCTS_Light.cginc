//Note: requires VertexOutput to be defined before including this file

#ifndef __UCTS_LIGHT__
#define __UCTS_LIGHT__

#include "AutoLight.cginc"

#if defined(UTS_USE_RAYTRACING_SHADOW)
    #define UTS_LIGHT_ATTENUATION(destName, input, worldPos)        fixed destName = UtsLightAttenuation(input, worldPos)

    uniform sampler2D _RaytracedHardShadow;
    fixed rths_shadowAttenuation(float2 lightmapUV, float3 worldPos, float4 screenPos) {

//Handle division by zero warning, based on how UNITY_SHADOW_ATTENUATION is used in Autolight.cginc
#if (!defined(HANDLE_SHADOWS_BLENDING_IN_GI)) \
    && !(defined(SHADOWS_SCREEN) && !defined(LIGHTMAP_ON) && !defined(UNITY_NO_SCREENSPACE_SHADOWS)) \
    && !(defined(SHADOWS_SHADOWMASK)) \
    && !(defined(SHADOWS_DEPTH) || defined(SHADOWS_SCREEN) || defined(SHADOWS_CUBE)) \
    && (UNITY_LIGHT_PROBE_PROXY_VOLUME)
        float r = UNITY_SAMPLE_SCREEN_SHADOW(_RaytracedHardShadow, float4(screenPos.xyz,1)).r;
#else
        float r = UNITY_SAMPLE_SCREEN_SHADOW(_RaytracedHardShadow, screenPos).r;
#endif //end handling division by zero

        return r;
    }

//---------------------------------------------------------------------------------------------------------------------

    fixed UtsLightAttenuation(VertexOutput input, float3 worldPos) {
        //redefine UnityComputeForwardShadows inside UNITY_LIGHT_ATTENUATION
        #define UnityComputeForwardShadows(v0,v1,v2) rths_shadowAttenuation(v0,v1,v2)
        UNITY_LIGHT_ATTENUATION(attenuation, input, worldPos);
        #undef UnityComputeForwardShadows
        return attenuation;
    }
#else
    // original
    #define UTS_LIGHT_ATTENUATION(destName, input, worldPos)    UNITY_LIGHT_ATTENUATION(destName, input, worldPos)
#endif //UTS_USE_RAYTRACING_SHADOW

#endif //__UCTS_LIGHT__


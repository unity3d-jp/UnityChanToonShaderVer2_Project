//Note: requires VertexOutput to be defined before including this file

#ifndef __UCTS_LIGHT__
#define __UCTS_LIGHT__

#include "AutoLight.cginc"

#if defined(UTS_USE_RAYTRACING_SHADOW)
    #define UTS_LIGHT_ATTENUATION(destName, input, worldPos)        fixed destName = UtsLightAttenuation(input, worldPos)

    uniform sampler2D _RaytracedHardShadow;
    fixed rths_shadowAttenuation(float2 lightmapUV, float3 worldPos, float4 screenPos) {
        float r = UNITY_SAMPLE_SCREEN_SHADOW(_RaytracedHardShadow, screenPos).r;
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


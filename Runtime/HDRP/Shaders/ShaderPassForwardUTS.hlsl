#if SHADERPASS != SHADERPASS_FORWARD
#error SHADERPASS_is_not_correctly_define
#endif

#ifdef _WRITE_TRANSPARENT_MOTION_VECTOR
#include "Packages/com.unity.render-pipelines.high-definition/Runtime/RenderPipeline/ShaderPass/MotionVectorVertexShaderCommon.hlsl"

PackedVaryingsType Vert(AttributesMesh inputMesh, AttributesPass inputPass)
{
    VaryingsType varyingsType;
    varyingsType.vmesh = VertMesh(inputMesh);
    return MotionVectorVS(varyingsType, inputMesh, inputPass);
}

#ifdef TESSELLATION_ON

PackedVaryingsToPS VertTesselation(VaryingsToDS input)
{
    VaryingsToPS output;
    output.vmesh = VertMeshTesselation(input.vmesh);
    MotionVectorPositionZBias(output);

    output.vpass.positionCS = input.vpass.positionCS;
    output.vpass.previousPositionCS = input.vpass.previousPositionCS;

    return PackVaryingsToPS(output);
}

#endif // TESSELLATION_ON

#else // _WRITE_TRANSPARENT_MOTION_VECTOR

#include "Packages/com.unity.render-pipelines.high-definition/Runtime/RenderPipeline/ShaderPass/VertMesh.hlsl"

PackedVaryingsType Vert(AttributesMesh inputMesh)
{
    VaryingsType varyingsType;
    varyingsType.vmesh = VertMesh(inputMesh);

    return PackVaryingsType(varyingsType);
}

#ifdef TESSELLATION_ON

PackedVaryingsToPS VertTesselation(VaryingsToDS input)
{
    VaryingsToPS output;
    output.vmesh = VertMeshTesselation(input.vmesh);

    return PackVaryingsToPS(output);
}


#endif // TESSELLATION_ON

#endif // _WRITE_TRANSPARENT_MOTION_VECTOR


#ifdef TESSELLATION_ON
#include "Packages/com.unity.render-pipelines.high-definition/Runtime/RenderPipeline/ShaderPass/TessellationShare.hlsl"
#endif



float3 GetLightColor(LightLoopContext context, FragInputs input, PositionInputs posInput,
    float3 V, BuiltinData builtinData,
    BSDFData bsdfData, PreLightData preLightData, LightData light)
{
    float3 finalColor = float3(0, 0, 0);
    float3 L; // lightToSample = positionWS - light.positionRWS;  unL = -lightToSample; L = unL * distRcp;
    float4 distances; // {d, d^2, 1/d, d_proj}
    GetPunctualLightVectors(input.positionRWS, light, L, distances);
    if ((light.lightDimmer > 0) && IsNonZeroBSDF(V, L, preLightData, bsdfData))
    {
        float4 lightColor = EvaluateLight_Punctual(context, posInput, light, L, distances);
        lightColor.rgb *= lightColor.a; // Composite
# ifdef MATERIAL_INCLUDE_TRANSMISSION
        if (ShouldEvaluateThickObjectTransmission(V, L, preLightData, bsdfData, light.shadowIndex))
        {
            // Replace the 'baked' value using 'thickness from shadow'.
            bsdfData.transmittance = EvaluateTransmittance_Punctual(context, posInput,
                bsdfData, light, L, distances);
        }
        else
# endif
        {
            // This code works for both surface reflection and thin object transmission.
            float shadow = EvaluateShadow_Punctual(context, posInput, light, builtinData, GetNormalForShadowBias(bsdfData), L, distances);
            lightColor.rgb *= ComputeShadowColor(shadow, light.shadowTint, light.penumbraTint);

# ifdef DEBUG_DISPLAY
            // The step with the attenuation is required to avoid seeing the screen tiles at the end of lights because the attenuation always falls to 0 before the tile ends.
            // Note: g_DebugShadowAttenuation have been setup in EvaluateShadow_Punctual
            if (_DebugShadowMapMode == SHADOWMAPDEBUGMODE_SINGLE_SHADOW && light.shadowIndex == _DebugSingleShadowIndex)
                g_DebugShadowAttenuation *= step(FLT_EPS, lightColor.a);
# endif
        }


        // this is not so important for UTS.
        ClampRoughness(preLightData, bsdfData, light.minRoughness);

        // extracted ShadeSurface_Infinitesimal()
        if (Max3(lightColor.r, lightColor.g, lightColor.b) > 0)
        {
            CBSDF cbsdf = EvaluateBSDF(V, L, preLightData, bsdfData);

# if defined(MATERIAL_INCLUDE_TRANSMISSION) || defined(MATERIAL_INCLUDE_PRECOMPUTED_TRANSMISSION)
            float3 transmittance = bsdfData.transmittance;
# else
            float3 transmittance = float3(0.0, 0.0, 0.0);
# endif

            // If transmittance or the CBSDF's transmission components are known to be 0,
            // the optimization pass of the compiler will remove all of the associated code.
            // However, this will take a lot more CPU time than doing the same thing using
            // the preprocessor.
//                            lighting.diffuse = (cbsdf.diffR + cbsdf.diffT * transmittance) * lightColor * diffuseDimmer;
//                            lighting.specular = (cbsdf.specR + cbsdf.specT * transmittance) * lightColor * specularDimmer;
            finalColor += (cbsdf.diffR + cbsdf.diffT  * transmittance) * lightColor  *light.diffuseDimmer;
            finalColor += (cbsdf.diffR + cbsdf.diffT  * transmittance) * lightColor  *light.specularDimmer;
        }


    }
    return finalColor;
}

uniform sampler2D _RaytracedHardShadow;
float4 _RaytracedHardShadow_TexelSize;
uniform int UtsUseRaytracingShadow;
//TEXTURE2D_SAMPLER2D(_RaytracedHardShadow, sampler_RaytracedHardShadow);
void Frag(PackedVaryingsToPS packedInput,
#ifdef OUTPUT_SPLIT_LIGHTING
    out float4 outColor : SV_Target0,  // outSpecularLighting
    out float4 outDiffuseLighting : SV_Target1,
    OUTPUT_SSSBUFFER(outSSSBuffer)
#else
    out float4 outColor : SV_Target0
#ifdef _WRITE_TRANSPARENT_MOTION_VECTOR
    , out float4 outMotionVec : SV_Target1
#endif // _WRITE_TRANSPARENT_MOTION_VECTOR
#endif // OUTPUT_SPLIT_LIGHTING
#ifdef _DEPTHOFFSET_ON
    , out float outputDepth : SV_Depth
#endif
)
{
    UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX(packedInput);
    FragInputs input = UnpackVaryingsMeshToFragInputs(packedInput.vmesh);

    // We need to readapt the SS position as our screen space positions are for a low res buffer, but we try to access a full res buffer.
    input.positionSS.xy = _OffScreenRendering > 0 ? (input.positionSS.xy * _OffScreenDownsampleFactor) : input.positionSS.xy;

    uint2 tileIndex = uint2(input.positionSS.xy) / GetTileSize();

    // input.positionSS is SV_Position
    PositionInputs posInput = GetPositionInput(input.positionSS.xy, _ScreenSize.zw, input.positionSS.z, input.positionSS.w, input.positionRWS.xyz, tileIndex);

#ifdef VARYINGS_NEED_POSITION_WS
    float3 V = GetWorldSpaceNormalizeViewDir(input.positionRWS);
#else
    // Unused
    float3 V = float3(1.0, 1.0, 1.0); // Avoid the division by 0
#endif
#ifdef _SURFACE_TYPE_TRANSPARENT
    uint featureFlags = LIGHT_FEATURE_MASK_FLAGS_TRANSPARENT;
#else
    uint featureFlags = LIGHT_FEATURE_MASK_FLAGS_OPAQUE;
#endif
    SurfaceData surfaceData;
    BuiltinData builtinData;
    GetSurfaceAndBuiltinData(input, V, posInput, surfaceData, builtinData);

    BSDFData bsdfData = ConvertSurfaceDataToBSDFData(input.positionSS.xy, surfaceData);

    PreLightData preLightData = GetPreLightData(V, posInput, bsdfData);

    outColor = float4(0.0, 0.0, 0.0, 0.0);



    // toshi.
#define UNITY_PROJ_COORD(a) a
#define UNITY_SAMPLE_SCREEN_SHADOW(tex, uv) tex2Dproj( tex, UNITY_PROJ_COORD(uv) ).r
    float inverseClipping = 0;
    LightLoopContext context;
    context.shadowContext = InitShadowContext();
    context.shadowValue = 1;
    context.sampleReflection = 0;

    // With XR single-pass and camera-relative: offset position to do lighting computations from the combined center view (original camera matrix).
    // This is required because there is only one list of lights generated on the CPU. Shadows are also generated once and shared between the instanced views.
    ApplyCameraRelativeXR(posInput.positionWS);

    // Initialize the contactShadow and contactShadowFade fields
    InitContactShadow(posInput, context);

    float3 i_normalDir = surfaceData.normalWS;

    float3 finalColor = float3(0.0f, 0.0f, 0.0f);
    if (featureFlags & LIGHTFEATUREFLAGS_DIRECTIONAL)
    {
        // Evaluate sun shadows.
        if (_DirectionalShadowIndex >= 0)
        {
            DirectionalLightData light = _DirectionalLightDatas[_DirectionalShadowIndex];
#if defined(SCREEN_SPACE_SHADOWS) && !defined(_SURFACE_TYPE_TRANSPARENT)
            if ((light.screenSpaceShadowIndex & SCREEN_SPACE_SHADOW_INDEX_MASK) != INVALID_SCREEN_SPACE_SHADOW)
            {
                context.shadowValue = GetScreenSpaceColorShadow(posInput, light.screenSpaceShadowIndex);
            }
            else
#endif
            {
                // TODO: this will cause us to load from the normal buffer first. Does this cause a performance problem?
                float3 L = -light.forward;

                // Is it worth sampling the shadow map?
                if ((light.lightDimmer > 0) && (light.shadowDimmer > 0) && // Note: Volumetric can have different dimmer, thus why we test it here
                    IsNonZeroBSDF(V, L, preLightData, bsdfData) &&
                    !ShouldEvaluateThickObjectTransmission(V, L, preLightData, bsdfData, light.shadowIndex))
                {

#if defined(UTS_USE_RAYTRACING_SHADOW)
                    {
                        /*
                        struct PositionInputs
                        {
                            float3 positionWS;  // World space position (could be camera-relative)
                            float2 positionNDC; // Normalized screen coordinates within the viewport    : [0, 1) (with the half-pixel offset)
                            uint2  positionSS;  // Screen space pixel coordinates                       : [0, NumPixels)
                            uint2  tileCoord;   // Screen tile coordinates                              : [0, NumTiles)
                            float  deviceDepth; // Depth from the depth buffer                          : [0, 1] (typically reversed)
                            float  linearDepth; // View space Z coordinate                              : [Near, Far]
                        };
                        float4 size = _RaytracedHardShadow_TexelSize;
                        */

                        float r = UNITY_SAMPLE_SCREEN_SHADOW(_RaytracedHardShadow, float4(posInput.positionNDC.xy, 0.0, 1));
                        context.shadowValue = r;
                    }
#else
                    {
                        context.shadowValue = GetDirectionalShadowAttenuation(context.shadowContext,
                            posInput.positionSS, posInput.positionWS, GetNormalForShadowBias(bsdfData),
                            light.shadowIndex, L);

                    }
#endif // UTS_USE_RAYTRACING_SHADOW


                }
#if defined (UTS_USE_RAYTRACING_SHADOW)
                else 
                {
                    float r = UNITY_SAMPLE_SCREEN_SHADOW(_RaytracedHardShadow, float4(posInput.positionNDC.xy, 0.0, 1));
                    context.shadowValue = r;
                }
#endif // UTS_USE_RAYTRACING_SHADOW
            }

        }

        int mainLightIndex = GetUtsMainLightIndex(builtinData);
        if ( mainLightIndex >= 0)
        {
#if defined(_SHADINGGRADEMAP)
            finalColor = UTS_MainLightShadingGrademap(context, input, mainLightIndex, inverseClipping);
#else
            finalColor = UTS_MainLight(context, input, mainLightIndex, inverseClipping);
#endif
        }



        uint i = 0; // Declare once to avoid the D3D11 compiler warning.
        for (i = 0; i < _DirectionalLightCount; ++i)
        {
            if (IsMatchingLightLayer(_DirectionalLightDatas[i].lightLayers, builtinData.renderingLayers))
            {
                if (mainLightIndex != i)
                {
                    
                    float3 lightColor = _DirectionalLightDatas[i].color;
                    float3 lightDirection = -_DirectionalLightDatas[i].forward;
                    float notDirectional = 0.0f;
#if defined(_SHADINGGRADEMAP)
                    float3 additionalLightColor = UTS_OtherLightsShadingGrademap(input, i_normalDir, lightColor, lightDirection, notDirectional);

#else
                    float3 additionalLightColor = UTS_OtherLights(input, i_normalDir, lightColor, lightDirection, notDirectional);
#endif
                    finalColor += additionalLightColor;
                }
            }
        }

    }

    AggregateLighting aggregateLighting;
    ZERO_INITIALIZE(AggregateLighting, aggregateLighting); // LightLoop is in charge of initializing the struct


    // ------------------- env --------------------
    // Define macro for a better understanding of the loop
    // TODO: this code is now much harder to understand...
#define EVALUATE_BSDF_ENV_SKY(envLightData, TYPE, type) \
        IndirectLighting lighting = EvaluateBSDF_Env(context, V, posInput, preLightData, envLightData, bsdfData, envLightData.influenceShapeType, MERGE_NAME(GPUIMAGEBASEDLIGHTINGTYPE_, TYPE), MERGE_NAME(type, HierarchyWeight)); \
        AccumulateIndirectLighting(lighting, aggregateLighting);

// Environment cubemap test lightlayers, sky don't test it
#define EVALUATE_BSDF_ENV(envLightData, TYPE, type) if (IsMatchingLightLayer(envLightData.lightLayers, builtinData.renderingLayers)) { EVALUATE_BSDF_ENV_SKY(envLightData, TYPE, type) }

    // First loop iteration
    if (featureFlags & (LIGHTFEATUREFLAGS_ENV | LIGHTFEATUREFLAGS_SKY | LIGHTFEATUREFLAGS_SSREFRACTION | LIGHTFEATUREFLAGS_SSREFLECTION))
    {
        float reflectionHierarchyWeight = 0.0; // Max: 1.0
        float refractionHierarchyWeight = _EnableSSRefraction ? 0.0 : 1.0; // Max: 1.0

        uint envLightStart, envLightCount;

        // Fetch first env light to provide the scene proxy for screen space computation
#ifndef LIGHTLOOP_DISABLE_TILE_AND_CLUSTER
        GetCountAndStart(posInput, LIGHTCATEGORY_ENV, envLightStart, envLightCount);
#else   // LIGHTLOOP_DISABLE_TILE_AND_CLUSTER
        envLightCount = _EnvLightCount;
        envLightStart = 0;
#endif

        bool fastPath = false;
#if SCALARIZE_LIGHT_LOOP
        uint envStartFirstLane;
        fastPath = IsFastPath(envLightStart, envStartFirstLane);
#endif

        // Reflection / Refraction hierarchy is
        //  1. Screen Space Refraction / Reflection
        //  2. Environment Reflection / Refraction
        //  3. Sky Reflection / Refraction

        // Apply SSR.
#if !defined(_SURFACE_TYPE_TRANSPARENT) && !defined(_DISABLE_SSR)
        {
            IndirectLighting indirect = EvaluateBSDF_ScreenSpaceReflection(posInput, preLightData, bsdfData,
                reflectionHierarchyWeight);
            AccumulateIndirectLighting(indirect, aggregateLighting);
        }
#endif

        EnvLightData envLightData;
        if (envLightCount > 0)
        {
            envLightData = FetchEnvLight(envLightStart, 0);
        }
        else
        {
            envLightData = InitSkyEnvLightData(0);
        }

        if ((featureFlags & LIGHTFEATUREFLAGS_SSREFRACTION) && (_EnableSSRefraction > 0))
        {
            IndirectLighting lighting = EvaluateBSDF_ScreenspaceRefraction(context, V, posInput, preLightData, bsdfData, envLightData, refractionHierarchyWeight);
            AccumulateIndirectLighting(lighting, aggregateLighting);
        }

        // Reflection probes are sorted by volume (in the increasing order).
        if (featureFlags & LIGHTFEATUREFLAGS_ENV)
        {
            context.sampleReflection = SINGLE_PASS_CONTEXT_SAMPLE_REFLECTION_PROBES;

#if SCALARIZE_LIGHT_LOOP
            if (fastPath)
            {
                envLightStart = envStartFirstLane;
            }
#endif

            // Scalarized loop, same rationale of the punctual light version
            uint v_envLightListOffset = 0;
            uint v_envLightIdx = envLightStart;
            while (v_envLightListOffset < envLightCount)
            {
                v_envLightIdx = FetchIndex(envLightStart, v_envLightListOffset);
                uint s_envLightIdx = ScalarizeElementIndex(v_envLightIdx, fastPath);
                if (s_envLightIdx == -1)
                    break;

                EnvLightData s_envLightData = FetchEnvLight(s_envLightIdx);    // Scalar load.

                // If current scalar and vector light index match, we process the light. The v_envLightListOffset for current thread is increased.
                // Note that the following should really be ==, however, since helper lanes are not considered by WaveActiveMin, such helper lanes could
                // end up with a unique v_envLightIdx value that is smaller than s_envLightIdx hence being stuck in a loop. All the active lanes will not have this problem.
                if (s_envLightIdx >= v_envLightIdx)
                {
                    v_envLightListOffset++;
                    if (reflectionHierarchyWeight < 1.0)
                    {
                        EVALUATE_BSDF_ENV(s_envLightData, REFLECTION, reflection);
                    }
                    // Refraction probe and reflection probe will process exactly the same weight. It will be good for performance to be able to share this computation
                    // However it is hard to deal with the fact that reflectionHierarchyWeight and refractionHierarchyWeight have not the same values, they are independent
                    // The refraction probe is rarely used and happen only with sphere shape and high IOR. So we accept the slow path that use more simple code and
                    // doesn't affect the performance of the reflection which is more important.
                    // We reuse LIGHTFEATUREFLAGS_SSREFRACTION flag as refraction is mainly base on the screen. Would be a waste to not use screen and only cubemap.
                    if ((featureFlags & LIGHTFEATUREFLAGS_SSREFRACTION) && (refractionHierarchyWeight < 1.0))
                    {
                        EVALUATE_BSDF_ENV(s_envLightData, REFRACTION, refraction);
                    }
                }

            }
        }

        // Only apply the sky IBL if the sky texture is available
        if ((featureFlags & LIGHTFEATUREFLAGS_SKY) && _EnvLightSkyEnabled)
        {
            // The sky is a single cubemap texture separate from the reflection probe texture array (different resolution and compression)
            context.sampleReflection = SINGLE_PASS_CONTEXT_SAMPLE_SKY;

            // The sky data are generated on the fly so the compiler can optimize the code
            EnvLightData envLightSky = InitSkyEnvLightData(0);

            // Only apply the sky if we haven't yet accumulated enough IBL lighting.
            if (reflectionHierarchyWeight < 1.0)
            {
                EVALUATE_BSDF_ENV_SKY(envLightSky, REFLECTION, reflection);
            }

            if ((featureFlags & LIGHTFEATUREFLAGS_SSREFRACTION) && (refractionHierarchyWeight < 1.0))
            {
                EVALUATE_BSDF_ENV_SKY(envLightSky, REFRACTION, refraction);
            }
        }
    }
#undef EVALUATE_BSDF_ENV
#undef EVALUATE_BSDF_ENV_SKY
    // ------------------- env --------------------
    if (featureFlags & LIGHTFEATUREFLAGS_PUNCTUAL)
    {
        uint lightCount, lightStart;

#ifndef LIGHTLOOP_DISABLE_TILE_AND_CLUSTER
        GetCountAndStart(posInput, LIGHTCATEGORY_PUNCTUAL, lightStart, lightCount);
#else   // LIGHTLOOP_DISABLE_TILE_AND_CLUSTER
        lightCount = _PunctualLightCount;
        lightStart = 0;
#endif

        bool fastPath = false;
#if SCALARIZE_LIGHT_LOOP
        uint lightStartLane0;
        fastPath = IsFastPath(lightStart, lightStartLane0);

        if (fastPath)
        {
            lightStart = lightStartLane0;
        }
#endif

        // Scalarized loop. All lights that are in a tile/cluster touched by any pixel in the wave are loaded (scalar load), only the one relevant to current thread/pixel are processed.
        // For clarity, the following code will follow the convention: variables starting with s_ are meant to be wave uniform (meant for scalar register),
        // v_ are variables that might have different value for each thread in the wave (meant for vector registers).
        // This will perform more loads than it is supposed to, however, the benefits should offset the downside, especially given that light data accessed should be largely coherent.
        // Note that the above is valid only if wave intriniscs are supported.

        uint v_lightListOffset = 0;
        uint v_lightIdx = lightStart;

        [loop]
        while (v_lightListOffset < lightCount)
        {
            v_lightIdx = FetchIndex(lightStart, v_lightListOffset);
            uint s_lightIdx = ScalarizeElementIndex(v_lightIdx, fastPath);
            if (s_lightIdx == -1)
                break;



            // If current scalar and vector light index match, we process the light. The v_lightListOffset for current thread is increased.
            // Note that the following should really be ==, however, since helper lanes are not considered by WaveActiveMin, such helper lanes could
            // end up with a unique v_lightIdx value that is smaller than s_lightIdx hence being stuck in a loop. All the active lanes will not have this problem.
            if (s_lightIdx >= v_lightIdx)
            {
                v_lightListOffset++;

                LightData s_lightData = FetchLight(s_lightIdx);
                float3 lightColor = 0;


                float notDirectional = 1.0f;
                float3 lightVector = s_lightData.positionRWS - posInput.positionWS * notDirectional;
                float distanceSqr = max(dot(lightVector, lightVector), HALF_MIN);
                float3 lightDirection = float3(lightVector * rsqrt(distanceSqr));
                float3 additionalLightColor = s_lightData.color;
                if (IsMatchingLightLayer(s_lightData.lightLayers, builtinData.renderingLayers))
                {
#if defined(_SHADINGGRADEMAP)
                    float3 pointLightColor = UTS_OtherLightsShadingGrademap(input, i_normalDir, additionalLightColor, lightDirection, notDirectional);
#else
                    float3 pointLightColor = UTS_OtherLights(input, i_normalDir, additionalLightColor, lightDirection, notDirectional);
#endif
                    finalColor += pointLightColor;
                }
            }
        }
    }

    //   float3 envColor = aggregateLighting.direct.diffuse; // ???
    float3 envColor = float3(0.2f, 0.2f, 0.2f);
    float3 envLightColor = envColor;
    float3 envLightIntensity = 0.299*envLightColor.r + 0.587*envLightColor.g + 0.114*envLightColor.b < 1 ? (0.299*envLightColor.r + 0.587*envLightColor.g + 0.114*envLightColor.b) : 1;

    finalColor = saturate(finalColor) + (envLightColor*envLightIntensity*_GI_Intensity*smoothstep(1, 0, envLightIntensity / 2)) + emissive;
    //    finalColor = float3(context.shadowValue, 0, 0);
#if defined(_SHADINGGRADEMAP)
    //v.2.0.4
  #ifdef _IS_TRANSCLIPPING_OFF

    outColor = float4(finalColor, 1);

  #elif _IS_TRANSCLIPPING_ON
    float Set_Opacity = saturate((inverseClipping + _Tweak_transparency));

    outColor = float4(finalColor, Set_Opacity);

  #endif

#else //#if defined(_SHADINGGRADEMAP)

  #ifdef _IS_CLIPPING_OFF
    //DoubleShadeWithFeather

    outColor = float4(finalColor, 1);

  #elif _IS_CLIPPING_MODE
    //DoubleShadeWithFeather_Clipping

    outColor = float4(finalColor, 1);

  #elif _IS_CLIPPING_TRANSMODE
    //DoubleShadeWithFeather_TransClipping
    float Set_Opacity = saturate((inverseClipping + _Tweak_transparency));
    outColor = float4(finalColor, Set_Opacity);
  #endif

#endif //#if defined(_SHADINGGRADEMAP)
#ifdef _DEPTHOFFSET_ON
    outputDepth = posInput.deviceDepth;
#endif
}

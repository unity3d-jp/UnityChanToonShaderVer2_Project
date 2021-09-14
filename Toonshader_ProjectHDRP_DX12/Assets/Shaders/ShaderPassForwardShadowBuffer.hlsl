#if SHADERPASS != SHADERPASS_FORWARD_UNLIT
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


#ifdef UNITY_VIRTUAL_TEXTURING
#define VT_BUFFER_TARGET SV_Target1
#define EXTRA_BUFFER_TARGET SV_Target2
#else
#define EXTRA_BUFFER_TARGET SV_Target1
#endif

bool UseScreenSpaceShadow(DirectionalLightData light, float3 normalWS)
{
    // Two different options are possible here
    // - We have a ray trace shadow in which case we have no valid signal for a transmission and we need to fallback on the rasterized shadow
    // - We have a screen space shadow and it already contains the transmission shadow and we can use it straight away
    bool visibleLight = dot(normalWS, -light.forward) > 0.0;
    bool validScreenSpaceShadow = (light.screenSpaceShadowIndex & SCREEN_SPACE_SHADOW_INDEX_MASK) != INVALID_SCREEN_SPACE_SHADOW;
    bool rayTracedShadow = (light.screenSpaceShadowIndex & RAY_TRACED_SCREEN_SPACE_SHADOW_FLAG) != 0;
    return (validScreenSpaceShadow && ((rayTracedShadow && visibleLight) || !rayTracedShadow));
}


void Frag(PackedVaryingsToPS packedInput,
    out float4 outColor : SV_Target0
#ifdef UNITY_VIRTUAL_TEXTURING
    , out float4 outVTFeedback : VT_BUFFER_TARGET
#endif
#ifdef _WRITE_TRANSPARENT_MOTION_VECTOR
    , out float4 outMotionVec : EXTRA_BUFFER_TARGET
#endif
#ifdef _DEPTHOFFSET_ON
    , out float outputDepth : SV_Depth
#endif
)
{
#ifdef _WRITE_TRANSPARENT_MOTION_VECTOR
    // Init outMotionVector here to solve compiler warning (potentially unitialized variable)
    // It is init to the value of forceNoMotion (with 2.0)
    outMotionVec = float4(2.0, 0.0, 0.0, 0.0);
#endif

    UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX(packedInput);
    FragInputs input = UnpackVaryingsToFragInputs(packedInput);

    // input.positionSS is SV_Position
    PositionInputs posInput = GetPositionInput(input.positionSS.xy, _ScreenSize.zw, input.positionSS.z, input.positionSS.w, input.positionRWS);

#ifdef VARYINGS_NEED_POSITION_WS
    float3 V = GetWorldSpaceNormalizeViewDir(input.positionRWS);
#else
    // Unused
    float3 V = float3(1.0, 1.0, 1.0); // Avoid the division by 0
#endif

    SurfaceData surfaceData;
    BuiltinData builtinData;
    GetSurfaceAndBuiltinData(input, V, posInput, surfaceData, builtinData);

    // Not lit here (but emissive is allowed)
    BSDFData bsdfData = ConvertSurfaceDataToBSDFData(input.positionSS.xy, surfaceData);

    // If this is a shadow matte, then we want the AO to affect the base color (the AO being correct if the surface is flagged shadow matte).
#if defined(_ENABLE_SHADOW_MATTE)
    bsdfData.color *= GetScreenSpaceAmbientOcclusion(input.positionSS.xy);
#endif

    // Note: we must not access bsdfData in shader pass, but for unlit we make an exception and assume it should have a color field
    float4 outResult = ApplyBlendMode(bsdfData.color + builtinData.emissiveColor * GetCurrentExposureMultiplier(), builtinData.opacity);
    outResult = EvaluateAtmosphericScattering(posInput, V, outResult);

#ifdef DEBUG_DISPLAY
    // Same code in ShaderPassForward.shader
    // Reminder: _DebugViewMaterialArray[i]
    //   i==0 -> the size used in the buffer
    //   i>0  -> the index used (0 value means nothing)
    // The index stored in this buffer could either be
    //   - a gBufferIndex (always stored in _DebugViewMaterialArray[1] as only one supported)
    //   - a property index which is different for each kind of material even if reflecting the same thing (see MaterialSharedProperty)
    int bufferSize = _DebugViewMaterialArray[0].x;
    // Loop through the whole buffer
    // Works because GetSurfaceDataDebug will do nothing if the index is not a known one
    for (int index = 1; index <= bufferSize; index++)
    {
        int indexMaterialProperty = _DebugViewMaterialArray[index].x;
        if (indexMaterialProperty != 0)
        {
            float3 result = float3(1.0, 0.0, 1.0);
            bool needLinearToSRGB = false;

            GetPropertiesDataDebug(indexMaterialProperty, result, needLinearToSRGB);
            GetVaryingsDataDebug(indexMaterialProperty, input, result, needLinearToSRGB);
            GetBuiltinDataDebug(indexMaterialProperty, builtinData, posInput, result, needLinearToSRGB);
            GetSurfaceDataDebug(indexMaterialProperty, surfaceData, result, needLinearToSRGB);
            GetBSDFDataDebug(indexMaterialProperty, bsdfData, result, needLinearToSRGB);

            // TEMP!
            // For now, the final blit in the backbuffer performs an sRGB write
            // So in the meantime we apply the inverse transform to linear data to compensate, unless we output to AOVs.
            if (!needLinearToSRGB && _DebugAOVOutput == 0)
                result = SRGBToLinear(max(0, result));

            outResult = float4(result, 1.0);
        }
    }

    if (_DebugFullScreenMode == FULLSCREENDEBUGMODE_TRANSPARENCY_OVERDRAW)
    {
        float4 result = _DebugTransparencyOverdrawWeight * float4(TRANSPARENCY_OVERDRAW_COST, TRANSPARENCY_OVERDRAW_COST, TRANSPARENCY_OVERDRAW_COST, TRANSPARENCY_OVERDRAW_A);
        outResult = result;
    }

#endif
    DirectionalLightData light = _DirectionalLightDatas[_DirectionalShadowIndex];
    SHADOW_TYPE shadow = 1.0;
    float4 Set_UV0 = input.texCoord0;
//    if (UseScreenSpaceShadow(light, bsdfData.normalWS))
    {
        //  shadow = GetScreenSpaceColorShadow(posInput, 0).SHADOW_TYPE_SWIZZLE;
       //shadow = LOAD_TEXTURE2D_X(_ScreenSpaceShadowsTexture,Set_UV0.xy);
       shadow = LOAD_TEXTURE2D_ARRAY(_ScreenSpaceShadowsTexture, Set_UV0.xy,0).r;

    }
    outColor = outResult;
    outColor.xyz *= shadow;
#ifdef _WRITE_TRANSPARENT_MOTION_VECTOR
    VaryingsPassToPS inputPass = UnpackVaryingsPassToPS(packedInput.vpass);
    bool forceNoMotion = any(unity_MotionVectorsParams.yw == 0.0);
    // outMotionVec is already initialize at the value of forceNoMotion (see above)
    if (!forceNoMotion)
    {
        float2 motionVec = CalculateMotionVector(inputPass.positionCS, inputPass.previousPositionCS);
        EncodeMotionVector(motionVec * 0.5, outMotionVec);
        outMotionVec.zw = 1.0;
    }
#endif

#ifdef _DEPTHOFFSET_ON
    outputDepth = posInput.deviceDepth;
#endif

#ifdef UNITY_VIRTUAL_TEXTURING
    outVTFeedback = builtinData.vtPackedFeedback;
#endif
}

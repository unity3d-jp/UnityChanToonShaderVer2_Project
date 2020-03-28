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

// normal should be normalized, w=1.0
// output in active color space
/*
half3 ShadeSH9(half4 normal)
{
    // Linear + constant polynomial terms
    half3 res = SHEvalLinearL0L1(normal);

    // Quadratic polynomials
    res += SHEvalLinearL2(normal);

#   ifdef UNITY_COLORSPACE_GAMMA
    res = LinearToGammaSpace(res);
#   endif

    return res;
}
*/





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

    SurfaceData surfaceData;
    BuiltinData builtinData;
    GetSurfaceAndBuiltinData(input, V, posInput, surfaceData, builtinData);

    BSDFData bsdfData = ConvertSurfaceDataToBSDFData(input.positionSS.xy, surfaceData);

    PreLightData preLightData = GetPreLightData(V, posInput, bsdfData);

    outColor = float4(0.0, 0.0, 0.0, 0.0);

    // We need to skip lighting when doing debug pass because the debug pass is done before lighting so some buffers may not be properly initialized potentially causing crashes on PS4.

#ifdef DEBUG_DISPLAY
    // Init in debug display mode to quiet warning
#ifdef OUTPUT_SPLIT_LIGHTING
    outDiffuseLighting = 0;
    ENCODE_INTO_SSSBUFFER(surfaceData, posInput.positionSS, outSSSBuffer);
#endif

    

    // Same code in ShaderPassForwardUnlit.shader
    // Reminder: _DebugViewMaterialArray[i]
    //   i==0 -> the size used in the buffer
    //   i>0  -> the index used (0 value means nothing)
    // The index stored in this buffer could either be
    //   - a gBufferIndex (always stored in _DebugViewMaterialArray[1] as only one supported)
    //   - a property index which is different for each kind of material even if reflecting the same thing (see MaterialSharedProperty)
    bool viewMaterial = false;
    int bufferSize = int(_DebugViewMaterialArray[0]);
    if (bufferSize != 0)
    {
        bool needLinearToSRGB = false;
        float3 result = float3(1.0, 0.0, 1.0);

        // Loop through the whole buffer
        // Works because GetSurfaceDataDebug will do nothing if the index is not a known one
        for (int index = 1; index <= bufferSize; index++)
        {
            int indexMaterialProperty = int(_DebugViewMaterialArray[index]);

            // skip if not really in use
            if (indexMaterialProperty != 0)
            {
                viewMaterial = true;

                GetPropertiesDataDebug(indexMaterialProperty, result, needLinearToSRGB);
                GetVaryingsDataDebug(indexMaterialProperty, input, result, needLinearToSRGB);
                GetBuiltinDataDebug(indexMaterialProperty, builtinData, result, needLinearToSRGB);
                GetSurfaceDataDebug(indexMaterialProperty, surfaceData, result, needLinearToSRGB);
                GetBSDFDataDebug(indexMaterialProperty, bsdfData, result, needLinearToSRGB);
            }
        }

        // TEMP!
        // For now, the final blit in the backbuffer performs an sRGB write
        // So in the meantime we apply the inverse transform to linear data to compensate.
        if (!needLinearToSRGB)
            result = SRGBToLinear(max(0, result));

        outColor = float4(result, 1.0);
    }

    if (!viewMaterial)
    {
        if (_DebugFullScreenMode == FULLSCREENDEBUGMODE_VALIDATE_DIFFUSE_COLOR || _DebugFullScreenMode == FULLSCREENDEBUGMODE_VALIDATE_SPECULAR_COLOR)
        {
            float3 result = float3(0.0, 0.0, 0.0);

            GetPBRValidatorDebug(surfaceData, result);

            outColor = float4(result, 1.0f);
        }
        else if (_DebugFullScreenMode == FULLSCREENDEBUGMODE_TRANSPARENCY_OVERDRAW)
        {
            float4 result = _DebugTransparencyOverdrawWeight * float4(TRANSPARENCY_OVERDRAW_COST, TRANSPARENCY_OVERDRAW_COST, TRANSPARENCY_OVERDRAW_COST, TRANSPARENCY_OVERDRAW_A);
            outColor = result;
        }
        else
#endif
        
        {
#ifdef _SURFACE_TYPE_TRANSPARENT
            uint featureFlags = LIGHT_FEATURE_MASK_FLAGS_TRANSPARENT;
#else
            uint featureFlags = LIGHT_FEATURE_MASK_FLAGS_OPAQUE;
#endif
            float3 diffuseLighting;
            float3 specularLighting;
            LightLoop(V, posInput, preLightData, bsdfData, builtinData, featureFlags, diffuseLighting, specularLighting);

            diffuseLighting *= GetCurrentExposureMultiplier();
            specularLighting *= GetCurrentExposureMultiplier();

#ifdef OUTPUT_SPLIT_LIGHTING
            if (_EnableSubsurfaceScattering != 0 && ShouldOutputSplitLighting(bsdfData))
            {
                outColor = float4(specularLighting, 1.0);
                outDiffuseLighting = float4(TagLightingForSSS(diffuseLighting), 1.0);
            }
            else
            {
                outColor = float4(diffuseLighting + specularLighting, 1.0);
                outDiffuseLighting = 0;
            }
            ENCODE_INTO_SSSBUFFER(surfaceData, posInput.positionSS, outSSSBuffer);
#else
            outColor = ApplyBlendMode(diffuseLighting, specularLighting, builtinData.opacity);
            outColor = EvaluateAtmosphericScattering(posInput, V, outColor);
#endif

#ifdef _WRITE_TRANSPARENT_MOTION_VECTOR
            VaryingsPassToPS inputPass = UnpackVaryingsPassToPS(packedInput.vpass);
            bool forceNoMotion = any(unity_MotionVectorsParams.yw == 0.0);
            if (forceNoMotion)
            {
                outMotionVec = float4(2.0, 0.0, 0.0, 0.0);
            }
            else
            {
                float2 motionVec = CalculateMotionVector(inputPass.positionCS, inputPass.previousPositionCS);
                EncodeMotionVector(motionVec * 0.5, outMotionVec);
                outMotionVec.zw = 1.0;
            }
#endif
        }

#ifdef DEBUG_DISPLAY
    }
#endif

    // toshi.
#if 1
    int lightIndex  = GetUtsMainLightIndex(builtinData);
    outColor = UTS_MainLight(input, lightIndex);
#else
    LightLoopContext context; //toshi
    float4 Set_UV0 = input.texCoord0;
    float3x3 tangentTransform = input.tangentToWorld;
    //UnpackNormalmapRGorAG(SAMPLE_TEXTURE2D(_NormalMap, sampler_NormalMap, texCoords))
    float4 n = SAMPLE_TEXTURE2D(_NormalMap, sampler_NormalMap, Set_UV0); // todo. TRANSFORM_TEX
//    float3 _NormalMap_var = UnpackNormalScale(tex2D(_NormalMap, TRANSFORM_TEX(Set_UV0, _NormalMap)), _BumpScale);
    float3 _NormalMap_var = UnpackNormalScale(n, _BumpScale);
    float3 normalLocal = _NormalMap_var.rgb;
    float3 normalDirection = normalize(mul(normalLocal, tangentTransform)); // Perturbed normals

    half3 envColor = half3(0.5, 0.5, 0.5);

    float4 _MainTex_var = tex2D(_MainTex, TRANSFORM_TEX(Set_UV0, _MainTex));
    float3 i_normalDir = surfaceData.normalWS;
    float3 viewDirection = V;

    

    half shadowAttenuation = 1.0f;

    DirectLighting lighting = EvaluateBSDF_Directional(context, V, posInput, preLightData, _DirectionalLightDatas[0], bsdfData, builtinData);

    float3 mainLihgtDirection = -_DirectionalLightDatas[0].forward;
    float3 mainLightColor =  _DirectionalLightDatas[0].color;
    float3 defaultLightDirection = normalize(UNITY_MATRIX_V[2].xyz + UNITY_MATRIX_V[1].xyz); 
    float3 defaultLightColor = saturate(max(half3(0.05, 0.05, 0.05)*_Unlit_Intensity, max(ShadeSH9(half4(0.0, 0.0, 0.0, 1.0)), ShadeSH9(half4(0.0, -1.0, 0.0, 1.0)).rgb)*_Unlit_Intensity));
    float3 customLightDirection = normalize(mul(UNITY_MATRIX_M, float4(((float3(1.0, 0.0, 0.0)*_Offset_X_Axis_BLD * 10) + (float3(0.0, 1.0, 0.0)*_Offset_Y_Axis_BLD * 10) + (float3(0.0, 0.0, -1.0)*lerp(-1.0, 1.0, _Inverse_Z_Axis_BLD))), 0)).xyz);
    float3 lightDirection = normalize(lerp(defaultLightDirection, mainLihgtDirection.xyz, any(mainLihgtDirection.xyz)));
    lightDirection = lerp(lightDirection, customLightDirection, _Is_BLD);
    half3 originalLightColor = mainLightColor;

    float3 lightColor = lerp(max(defaultLightColor, originalLightColor), max(defaultLightColor, saturate(originalLightColor)), _Is_Filter_LightColor);


    ////// Lighting:
    float3 halfDirection = normalize(viewDirection + lightDirection);
    //v.2.0.5
    _Color = _BaseColor;
    float3 Set_LightColor = lightColor.rgb;
    float3 Set_BaseColor = lerp((_MainTex_var.rgb*_BaseColor.rgb), ((_MainTex_var.rgb*_BaseColor.rgb)*Set_LightColor), _Is_LightColor_Base);
    //v.2.0.5
    float4 _1st_ShadeMap_var = lerp(tex2D(_1st_ShadeMap, TRANSFORM_TEX(Set_UV0, _1st_ShadeMap)), _MainTex_var, _Use_BaseAs1st);
    float3 Set_1st_ShadeColor = lerp((_1st_ShadeColor.rgb*_1st_ShadeMap_var.rgb), ((_1st_ShadeColor.rgb*_1st_ShadeMap_var.rgb)*Set_LightColor), _Is_LightColor_1st_Shade);
    //v.2.0.5
    float4 _2nd_ShadeMap_var = lerp(tex2D(_2nd_ShadeMap, TRANSFORM_TEX(Set_UV0, _2nd_ShadeMap)), _1st_ShadeMap_var, _Use_1stAs2nd);
    float3 Set_2nd_ShadeColor = lerp((_2nd_ShadeColor.rgb*_2nd_ShadeMap_var.rgb), ((_2nd_ShadeColor.rgb*_2nd_ShadeMap_var.rgb)*Set_LightColor), _Is_LightColor_2nd_Shade);
    float _HalfLambert_var = 0.5*dot(lerp(i_normalDir, normalDirection, _Is_NormalMapToBase), lightDirection) + 0.5;
    float4 _Set_2nd_ShadePosition_var = tex2D(_Set_2nd_ShadePosition, TRANSFORM_TEX(Set_UV0, _Set_2nd_ShadePosition));
    float4 _Set_1st_ShadePosition_var = tex2D(_Set_1st_ShadePosition, TRANSFORM_TEX(Set_UV0, _Set_1st_ShadePosition));
    //v.2.0.6
    //Minmimum value is same as the Minimum Feather's value with the Minimum Step's value as threshold.
    float _SystemShadowsLevel_var = (shadowAttenuation*0.5) + 0.5 + _Tweak_SystemShadowsLevel > 0.001 ? (shadowAttenuation*0.5) + 0.5 + _Tweak_SystemShadowsLevel : 0.0001;
    float Set_FinalShadowMask = saturate((1.0 + ((lerp(_HalfLambert_var, _HalfLambert_var*saturate(_SystemShadowsLevel_var), _Set_SystemShadowsToBase) - (_BaseColor_Step - _BaseShade_Feather)) * ((1.0 - _Set_1st_ShadePosition_var.rgb).r - 1.0)) / (_BaseColor_Step - (_BaseColor_Step - _BaseShade_Feather))));
    //
    //Composition: 3 Basic Colors as Set_FinalBaseColor
    float3 Set_FinalBaseColor = lerp(Set_BaseColor, lerp(Set_1st_ShadeColor, Set_2nd_ShadeColor, saturate((1.0 + ((_HalfLambert_var - (_ShadeColor_Step - _1st2nd_Shades_Feather)) * ((1.0 - _Set_2nd_ShadePosition_var.rgb).r - 1.0)) / (_ShadeColor_Step - (_ShadeColor_Step - _1st2nd_Shades_Feather))))), Set_FinalShadowMask); // Final Color
    float4 _Set_HighColorMask_var = tex2D(_Set_HighColorMask, TRANSFORM_TEX(Set_UV0, _Set_HighColorMask));
    float _Specular_var = 0.5*dot(halfDirection, lerp(i_normalDir, normalDirection, _Is_NormalMapToHighColor)) + 0.5; //  Specular                
    float _TweakHighColorMask_var = (saturate((_Set_HighColorMask_var.g + _Tweak_HighColorMaskLevel))*lerp((1.0 - step(_Specular_var, (1.0 - pow(_HighColor_Power, 5)))), pow(_Specular_var, exp2(lerp(11, 1, _HighColor_Power))), _Is_SpecularToHighColor));
    float4 _HighColor_Tex_var = tex2D(_HighColor_Tex, TRANSFORM_TEX(Set_UV0, _HighColor_Tex));
    float3 _HighColor_var = (lerp((_HighColor_Tex_var.rgb*_HighColor.rgb), ((_HighColor_Tex_var.rgb*_HighColor.rgb)*Set_LightColor), _Is_LightColor_HighColor)*_TweakHighColorMask_var);
    //Composition: 3 Basic Colors and HighColor as Set_HighColor
    float3 Set_HighColor = (lerp(saturate((Set_FinalBaseColor - _TweakHighColorMask_var)), Set_FinalBaseColor, lerp(_Is_BlendAddToHiColor, 1.0, _Is_SpecularToHighColor)) + lerp(_HighColor_var, (_HighColor_var*((1.0 - Set_FinalShadowMask) + (Set_FinalShadowMask*_TweakHighColorOnShadow))), _Is_UseTweakHighColorOnShadow));
    float4 _Set_RimLightMask_var = tex2D(_Set_RimLightMask, TRANSFORM_TEX(Set_UV0, _Set_RimLightMask));
    float3 _Is_LightColor_RimLight_var = lerp(_RimLightColor.rgb, (_RimLightColor.rgb*Set_LightColor), _Is_LightColor_RimLight);
    float _RimArea_var = (1.0 - dot(lerp(i_normalDir, normalDirection, _Is_NormalMapToRimLight), viewDirection));
    float _RimLightPower_var = pow(_RimArea_var, exp2(lerp(3, 0, _RimLight_Power)));
    float _Rimlight_InsideMask_var = saturate(lerp((0.0 + ((_RimLightPower_var - _RimLight_InsideMask) * (1.0 - 0.0)) / (1.0 - _RimLight_InsideMask)), step(_RimLight_InsideMask, _RimLightPower_var), _RimLight_FeatherOff));
    float _VertHalfLambert_var = 0.5*dot(i_normalDir, lightDirection) + 0.5;
    float3 _LightDirection_MaskOn_var = lerp((_Is_LightColor_RimLight_var*_Rimlight_InsideMask_var), (_Is_LightColor_RimLight_var*saturate((_Rimlight_InsideMask_var - ((1.0 - _VertHalfLambert_var) + _Tweak_LightDirection_MaskLevel)))), _LightDirection_MaskOn);
    float _ApRimLightPower_var = pow(_RimArea_var, exp2(lerp(3, 0, _Ap_RimLight_Power)));
    float3 Set_RimLight = (saturate((_Set_RimLightMask_var.g + _Tweak_RimLightMaskLevel))*lerp(_LightDirection_MaskOn_var, (_LightDirection_MaskOn_var + (lerp(_Ap_RimLightColor.rgb, (_Ap_RimLightColor.rgb*Set_LightColor), _Is_LightColor_Ap_RimLight)*saturate((lerp((0.0 + ((_ApRimLightPower_var - _RimLight_InsideMask) * (1.0 - 0.0)) / (1.0 - _RimLight_InsideMask)), step(_RimLight_InsideMask, _ApRimLightPower_var), _Ap_RimLight_FeatherOff) - (saturate(_VertHalfLambert_var) + _Tweak_LightDirection_MaskLevel))))), _Add_Antipodean_RimLight));
    //Composition: HighColor and RimLight as _RimLight_var
    float3 _RimLight_var = lerp(Set_HighColor, (Set_HighColor + Set_RimLight), _RimLight);
    //Matcap
    //v.2.0.6 : CameraRolling Stabilizer
    //鏡スクリプト判定：_sign_Mirror = -1 なら、鏡の中と判定.
    //v.2.0.7
    fixed _sign_Mirror = 0; //  todo. i.mirrorFlag;
    float3 _Camera_Right = UNITY_MATRIX_V[0].xyz;
    float3 _Camera_Front = UNITY_MATRIX_V[2].xyz;
    float3 _Up_Unit = float3(0, 1, 0);
    float3 _Right_Axis = cross(_Camera_Front, _Up_Unit);

    //鏡の中なら反転.
    if (_sign_Mirror < 0) {
        _Right_Axis = -1 * _Right_Axis;
        _Rotate_MatCapUV = -1 * _Rotate_MatCapUV;
    }
    else {
        _Right_Axis = _Right_Axis;
    }
    float _Camera_Right_Magnitude = sqrt(_Camera_Right.x*_Camera_Right.x + _Camera_Right.y*_Camera_Right.y + _Camera_Right.z*_Camera_Right.z);
    float _Right_Axis_Magnitude = sqrt(_Right_Axis.x*_Right_Axis.x + _Right_Axis.y*_Right_Axis.y + _Right_Axis.z*_Right_Axis.z);
    float _Camera_Roll_Cos = dot(_Right_Axis, _Camera_Right) / (_Right_Axis_Magnitude * _Camera_Right_Magnitude);
    float _Camera_Roll = acos(clamp(_Camera_Roll_Cos, -1, 1));
    fixed _Camera_Dir = _Camera_Right.y < 0 ? -1 : 1;
    float _Rot_MatCapUV_var_ang = (_Rotate_MatCapUV*3.141592654) - _Camera_Dir * _Camera_Roll*_CameraRolling_Stabilizer;
    //v.2.0.7
    float2 _Rot_MatCapNmUV_var = RotateUV(Set_UV0, (_Rotate_NormalMapForMatCapUV*3.141592654), float2(0.5, 0.5), 1.0);
    //V.2.0.6
    float3 _NormalMapForMatCap_var = UnpackNormalScale(tex2D(_NormalMapForMatCap, TRANSFORM_TEX(_Rot_MatCapNmUV_var, _NormalMapForMatCap)), _BumpScaleMatcap);
    //v.2.0.5: MatCap with camera skew correction
    float3 viewNormal = (mul(UNITY_MATRIX_V, float4(lerp(i_normalDir, mul(_NormalMapForMatCap_var.rgb, tangentTransform).rgb, _Is_NormalMapForMatCap), 0))).rgb;
    float3 NormalBlend_MatcapUV_Detail = viewNormal.rgb * float3(-1, -1, 1);
    float3 NormalBlend_MatcapUV_Base = (mul(UNITY_MATRIX_V, float4(viewDirection, 0)).rgb*float3(-1, -1, 1)) + float3(0, 0, 1);
    float3 noSknewViewNormal = NormalBlend_MatcapUV_Base * dot(NormalBlend_MatcapUV_Base, NormalBlend_MatcapUV_Detail) / NormalBlend_MatcapUV_Base.b - NormalBlend_MatcapUV_Detail;
    float2 _ViewNormalAsMatCapUV = (lerp(noSknewViewNormal, viewNormal, _Is_Ortho).rg*0.5) + 0.5;
    //v.2.0.7
    float2 _Rot_MatCapUV_var = RotateUV((0.0 + ((_ViewNormalAsMatCapUV - (0.0 + _Tweak_MatCapUV)) * (1.0 - 0.0)) / ((1.0 - _Tweak_MatCapUV) - (0.0 + _Tweak_MatCapUV))), _Rot_MatCapUV_var_ang, float2(0.5, 0.5), 1.0);
    //鏡の中ならUV左右反転.
    if (_sign_Mirror < 0) {
        _Rot_MatCapUV_var.x = 1 - _Rot_MatCapUV_var.x;
    }
    else {
        _Rot_MatCapUV_var = _Rot_MatCapUV_var;
    }
    //v.2.0.6 : LOD of Matcap
    float4 _MatCap_Sampler_var = tex2Dlod(_MatCap_Sampler, float4(TRANSFORM_TEX(_Rot_MatCapUV_var, _MatCap_Sampler), 0.0, _BlurLevelMatcap));
    //
    //MatcapMask
    float4 _Set_MatcapMask_var = tex2D(_Set_MatcapMask, TRANSFORM_TEX(Set_UV0, _Set_MatcapMask));
    float _Tweak_MatcapMaskLevel_var = saturate(lerp(_Set_MatcapMask_var.g, (1.0 - _Set_MatcapMask_var.g), _Inverse_MatcapMask) + _Tweak_MatcapMaskLevel);
    //
    float3 _Is_LightColor_MatCap_var = lerp((_MatCap_Sampler_var.rgb*_MatCapColor.rgb), ((_MatCap_Sampler_var.rgb*_MatCapColor.rgb)*Set_LightColor), _Is_LightColor_MatCap);
    //v.2.0.6 : ShadowMask on Matcap in Blend mode : multiply
    float3 Set_MatCap = lerp(_Is_LightColor_MatCap_var, (_Is_LightColor_MatCap_var*((1.0 - Set_FinalShadowMask) + (Set_FinalShadowMask*_TweakMatCapOnShadow)) + lerp(Set_HighColor*Set_FinalShadowMask*(1.0 - _TweakMatCapOnShadow), float3(0.0, 0.0, 0.0), _Is_BlendAddToMatCap)), _Is_UseTweakMatCapOnShadow);

    //
    //Composition: RimLight and MatCap as finalColor
    //Broke down finalColor composition
    float3 matCapColorOnAddMode = _RimLight_var + Set_MatCap * _Tweak_MatcapMaskLevel_var;
    float _Tweak_MatcapMaskLevel_var_MultiplyMode = _Tweak_MatcapMaskLevel_var * lerp(1.0, (1.0 - (Set_FinalShadowMask)*(1.0 - _TweakMatCapOnShadow)), _Is_UseTweakMatCapOnShadow);
    float3 matCapColorOnMultiplyMode = Set_HighColor * (1 - _Tweak_MatcapMaskLevel_var_MultiplyMode) + Set_HighColor * Set_MatCap*_Tweak_MatcapMaskLevel_var_MultiplyMode + lerp(float3(0, 0, 0), Set_RimLight, _RimLight);
    float3 matCapColorFinal = lerp(matCapColorOnMultiplyMode, matCapColorOnAddMode, _Is_BlendAddToMatCap);
    float3 finalColor = lerp(_RimLight_var, matCapColorFinal, _MatCap);// Final Composition before Emissive
    //
    //v.2.0.6: GI_Intensity with Intensity Multiplier Filter

    float3 envLightColor = envColor.rgb;

    float envLightIntensity = 0.299*envLightColor.r + 0.587*envLightColor.g + 0.114*envLightColor.b < 1 ? (0.299*envLightColor.r + 0.587*envLightColor.g + 0.114*envLightColor.b) : 1;

    finalColor = saturate(finalColor) + (envLightColor*envLightIntensity*_GI_Intensity*smoothstep(1, 0, envLightIntensity / 2)) + emissive;
    outColor = float4(finalColor, 1);
//    outColor = float4(Set_HighColor, 1);
//     outColor = _MainTex_var;
#endif // #if 1
 
#ifdef _DEPTHOFFSET_ON
    outputDepth = posInput.deviceDepth;
#endif
}

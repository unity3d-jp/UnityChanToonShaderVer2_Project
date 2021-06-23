#ifndef UNITY_ENCODE_INTO_GBUFFER_UTS_INCLUDED
#define UNITY_ENCODE_INTO_GBUFFER_UTS_INCLUDED

//-----------------------------------------------------------------------------
// conversion function for deferred
//-----------------------------------------------------------------------------

// GBuffer layout.
// GBuffer2 and GBuffer0.a interpretation depends on material feature enabled

//GBuffer0      RGBA8 sRGB  Gbuffer0 encode baseColor and so is sRGB to save precision. Alpha is not affected.
//GBuffer1      RGBA8
//GBuffer2      RGBA8
//GBuffer3      RGBA8


//FeatureName   Standard
//GBuffer0      baseColor.r,    baseColor.g,    baseColor.b,    specularOcclusion
//GBuffer1      normal.xy (1212),   perceptualRoughness
//GBuffer2      f0.r,   f0.g,   f0.b,   featureID(3) / coatMask(5)
//GBuffer3      bakedDiffuseLighting.rgb

//FeatureName   Subsurface Scattering + Transmission
//GBuffer0      baseColor.r,    baseColor.g,    baseColor.b,   diffusionProfile(4) / subsurfaceMask(4)
//GBuffer1      normal.xy (1212),   perceptualRoughness
//GBuffer2      specularOcclusion,  thickness,  diffusionProfile(4) / subsurfaceMask(4), featureID(3) / coatMask(5)
//GBuffer3      bakedDiffuseLighting.rgb

//FeatureName   Anisotropic
//GBuffer0      baseColor.r,    baseColor.g,    baseColor.b,    specularOcclusion
//GBuffer1      normal.xy (1212),   perceptualRoughness
//GBuffer2      anisotropy, tangent.x,  tangent.sign(1) / metallic(5), featureID(3) / coatMask(5)
//GBuffer3      bakedDiffuseLighting.rgb

//FeatureName   Irridescence
//GBuffer0      baseColor.r,    baseColor.g,    baseColor.b,    specularOcclusion
//GBuffer1      normal.xy (1212),   perceptualRoughness
//GBuffer2      IOR,    thickness,  unused(3bit) / metallic(5), featureID(3) / coatMask(5)
//GBuffer3      bakedDiffuseLighting.rgb

// Note:
// For standard we have chose to always encode fresnel0. Even when we use metal/baseColor parametrization. This avoid
// compiler optimization problem that was using VGPR to deal with the various combination of metal non metal.

// For SSS, we move diffusionProfile(4) / subsurfaceMask(4) in GBuffer0.a so the forward SSS code only need to write into one RT
// and the SSS postprocess only need to read one RT
// We duplicate diffusionProfile / subsurfaceMask in GBuffer2.b so the compiler don't need to read the GBuffer0 before PostEvaluateBSDF
// The lighting code have been adapted to only apply diffuseColor at the end.
// This save VGPR as we don' need to keep the GBuffer0 value in register.

// The layout is also design to only require one RT for the material classification. All the material feature flags are deduced from GBuffer2.

// Encode SurfaceData (BSDF parameters) into GBuffer
// Must be in sync with RT declared in HDRenderPipeline.cs ::Rebuild
void EncodeIntoGBufferUTS(SurfaceData surfaceData
    , BuiltinData builtinData
    , uint2 positionSS
    , out GBufferType0 outGBuffer0
    , out GBufferType1 outGBuffer1
    , out GBufferType2 outGBuffer2
    , out GBufferType3 outGBuffer3
#if GBUFFERMATERIAL_COUNT > 4
    , out GBufferType4 outGBuffer4
#endif
#if GBUFFERMATERIAL_COUNT > 5
    , out GBufferType5 outGBuffer5
#endif
#if GBUFFERMATERIAL_COUNT > 6
    , out GBufferType5 outGBuffer6
#endif
)
{
    // RT0 - 8:8:8:8 sRGB
    // Warning: the contents are later overwritten for Standard and SSS!
    outGBuffer0 = float4(surfaceData.baseColor, surfaceData.specularOcclusion);

    // This encode normalWS and PerceptualSmoothness into GBuffer1
    EncodeIntoNormalBuffer(ConvertSurfaceDataToNormalData(surfaceData), outGBuffer1);

    // RT2 - 8:8:8:8
    uint materialFeatureId;

    if (HasFlag(surfaceData.materialFeatures, MATERIALFEATUREFLAGS_LIT_SUBSURFACE_SCATTERING | MATERIALFEATUREFLAGS_LIT_TRANSMISSION))
    {
        // Reminder that during GBuffer pass we know statically material materialFeatures
        if ((surfaceData.materialFeatures & (MATERIALFEATUREFLAGS_LIT_SUBSURFACE_SCATTERING | MATERIALFEATUREFLAGS_LIT_TRANSMISSION)) == (MATERIALFEATUREFLAGS_LIT_SUBSURFACE_SCATTERING | MATERIALFEATUREFLAGS_LIT_TRANSMISSION))
            materialFeatureId = GBUFFER_LIT_TRANSMISSION_SSS;
        else if ((surfaceData.materialFeatures & MATERIALFEATUREFLAGS_LIT_SUBSURFACE_SCATTERING) == MATERIALFEATUREFLAGS_LIT_SUBSURFACE_SCATTERING)
            materialFeatureId = GBUFFER_LIT_SSS;
        else
            materialFeatureId = GBUFFER_LIT_TRANSMISSION;

        // We perform the same encoding for SSS and transmission even if not used as it is the same cost
        // Note that regarding EncodeIntoSSSBuffer, as the lit.shader IS the deferred shader (and the SSS fullscreen pass is based on deferred encoding),
        // it know the details of the encoding, so it is fine to assume here how SSSBuffer0 is encoded

        // For the SSS feature, the alpha channel is overwritten with (diffusionProfile | subsurfaceMask).
        // It is done so that the SSS pass only has to read a single G-Buffer 0.
        // We move specular occlusion to the red channel of the G-Buffer 2.
        EncodeIntoSSSBuffer(ConvertSurfaceDataToSSSData(surfaceData), positionSS, outGBuffer0);

        // We duplicate the alpha channel of the G-Buffer 0 (for diffusion profile).
        // It allows us to delay reading the G-Buffer 0 until the end of the deferred lighting shader.
        outGBuffer2.rgb = float3(surfaceData.specularOcclusion, surfaceData.thickness, outGBuffer0.a);
    }
    else if (HasFlag(surfaceData.materialFeatures, MATERIALFEATUREFLAGS_LIT_ANISOTROPY))
    {
        materialFeatureId = GBUFFER_LIT_ANISOTROPIC;

        // Reconstruct the default tangent frame.
        float3x3 frame = GetLocalFrame(surfaceData.normalWS);

        // Compute the rotation angle of the actual tangent frame with respect to the default one.
        float sinFrame = dot(surfaceData.tangentWS, frame[1]);
        float cosFrame = dot(surfaceData.tangentWS, frame[0]);

        // Define AnisoGGX(Éø, É¿, É¡), where:
        // Éø is the roughness corresponding to the direction of the tangent;
        // É¿ is the roughness corresponding to the direction of the bi-tangent;
        // É¡ is the angle of rotation of the tangent frame around the normal.
        //
        // The following symmetry relations exist:
        // 1st quadrant (Sin >= 0, Cos >  0): AnisoGGX(Éø, É¿, É¡), where (0 <= É¡ < Pi/2)
        // 2nd quadrant (Sin >  0, Cos <= 0): AnisoGGX(Éø, É¿, É¡) == AnisoGGX(É¿, Éø, É¡ + Pi * 1/2)
        // 3rd quadrant (Sin <= 0, Cos <  0): AnisoGGX(Éø, É¿, É¡) == AnisoGGX(Éø, É¿, É¡ + Pi)
        // 4th quadrant (Sin <  0, Cos >= 0): AnisoGGX(Éø, É¿, É¡) == AnisoGGX(É¿, Éø, É¡ + Pi * 3/2)
        // Handling of the interval end-points may be less rigorous to simplify programming.
        // The only requirement is that the handling is consistent throughout.
        bool quad2or4 = (sinFrame * cosFrame) < 0;

        // Anisotropy = (Éø - É¿) / (Éø + É¿).
        // Exchanging the roughness values Éø and É¿ is equivalent to negating the value of anisotropy.
#if 0
    // To avoid shading seams at the locations where anisotropy changes its sign,
    // its magnitude must be the same (on both sides) after reconstruction from the G-buffer.
    // This means that the hardware unit must perform rounding accurately (and consistently)
    // before storing the value in the G-buffer.
        float sfltAniso = quad2or4 ? -surfaceData.anisotropy : surfaceData.anisotropy;
        float anisotropy = sfltAniso * 0.5 + 0.5;
#else
    // It turns out, certain hardware has poor rounding behavior:
    // https://microsoft.github.io/DirectX-Specs/d3d/archive/D3D11_3_FunctionalSpec.htm#3.2.3.6%20FLOAT%20-%3E%20UNORM
    // Therefore, we must round manually to avoid the seams.
        float uintAniso = round(surfaceData.anisotropy * 127.5 + 127.5);
        uintAniso = quad2or4 ? 255 - uintAniso : uintAniso;
        // We cannot represent the anisotropy value of 0 exactly, but it is of little
        // importance since you can just use the isotropic material for that purpose.
        float anisotropy = uintAniso * rcp(255);
#endif

        // We need to convert the values of Sin and Cos to those appropriate for the 1st quadrant.
        // To go from Q3 to Q1, we must rotate by Pi, so taking the absolute value suffices.
        // To go from Q2 or Q4 to Q1, we must rotate by ((N + 1/2) * Pi), so we must
        // take the absolute value and also swap Sin and Cos.
        bool  storeSin = (abs(sinFrame) < abs(cosFrame)) != quad2or4;
        // sin [and cos] are approximately linear up to [after] Pi/4 Å} Pi.
        float sinOrCos = min(abs(sinFrame), abs(cosFrame));
        // To avoid storing redundant angles, we must convert from a node-centered representation
        // to a cell-centered one, e.i. remap: [0.5/256, 255.5/256] -> [0, 1].
        float remappedSinOrCos = Remap01(sinOrCos, sqrt(2) * 256.0 / 255.0, 0.5 / 255.0);

        outGBuffer2.rgb = float3(anisotropy,
            remappedSinOrCos,
            PackFloatInt8bit(surfaceData.metallic, storeSin ? 1 : 0, 8));
    }
    else if (HasFlag(surfaceData.materialFeatures, MATERIALFEATUREFLAGS_LIT_IRIDESCENCE))
    {
        materialFeatureId = GBUFFER_LIT_IRIDESCENCE;

        outGBuffer2.rgb = float3(surfaceData.iridescenceMask, surfaceData.iridescenceThickness,
            PackFloatInt8bit(surfaceData.metallic, 0, 8));
    }
    else // Standard
    {
        // In the case of standard or specular color we always convert to specular color parametrization before encoding,
        // so decoding is more efficient (it allow better optimization for the compiler and save VGPR)
        // This mean that on the decode side, MATERIALFEATUREFLAGS_LIT_SPECULAR_COLOR doesn't exist anymore
        materialFeatureId = GBUFFER_LIT_STANDARD;

        float3 diffuseColor = surfaceData.baseColor;
        float3 fresnel0 = surfaceData.specularColor;

        if (!HasFlag(surfaceData.materialFeatures, MATERIALFEATUREFLAGS_LIT_SPECULAR_COLOR))
        {
            // Convert from the metallic parametrization.
            diffuseColor = ComputeDiffuseColor(surfaceData.baseColor, surfaceData.metallic);
            fresnel0 = ComputeFresnel0(surfaceData.baseColor, surfaceData.metallic, DEFAULT_SPECULAR_VALUE);
        }

        outGBuffer0.rgb = diffuseColor;               // sRGB RT
        // outGBuffer2 is not sRGB, so use a fast encode/decode sRGB to keep precision
        outGBuffer2.rgb = FastLinearToSRGB(fresnel0); // TODO: optimize
    }

    // Ensure that surfaceData.coatMask is 0 if the feature is not enabled
    float coatMask = HasFlag(surfaceData.materialFeatures, MATERIALFEATUREFLAGS_LIT_CLEAR_COAT) ? surfaceData.coatMask : 0.0;
    // Note: no need to store MATERIALFEATUREFLAGS_LIT_STANDARD, always present
    outGBuffer2.a = PackFloatInt8bit(coatMask, materialFeatureId, 8);

#ifdef DEBUG_DISPLAY
    if (_DebugLightingMode >= DEBUGLIGHTINGMODE_DIFFUSE_LIGHTING && _DebugLightingMode <= DEBUGLIGHTINGMODE_EMISSIVE_LIGHTING)
    {
        // With deferred, Emissive is store in builtinData.bakeDiffuseLighting. If we ask for emissive lighting only
        // then remove bakeDiffuseLighting part.
        if (_DebugLightingMode == DEBUGLIGHTINGMODE_EMISSIVE_LIGHTING)
        {
#if SHADEROPTIONS_PROBE_VOLUMES_EVALUATION_MODE == PROBEVOLUMESEVALUATIONMODES_LIGHT_LOOP
            if (!IsUninitializedGI(builtinData.bakeDiffuseLighting))
#endif
            {
                builtinData.bakeDiffuseLighting = real3(0.0, 0.0, 0.0);
            }
        }
        else
        {
            builtinData.emissiveColor = real3(0.0, 0.0, 0.0);
        }
    }
#endif

    // RT3 - 11f:11f:10f
    // In deferred we encode emissive color with bakeDiffuseLighting. We don't have the room to store emissiveColor.
    // It mean that any futher process that affect bakeDiffuseLighting will also affect emissiveColor, like SSAO for example.
#if SHADEROPTIONS_PROBE_VOLUMES_EVALUATION_MODE == PROBEVOLUMESEVALUATIONMODES_LIGHT_LOOP

    if (IsUninitializedGI(builtinData.bakeDiffuseLighting))
    {
        // builtinData.bakeDiffuseLighting contain uninitializedGI sentinel value.

        // This means probe volumes will not get applied to this pixel, only emissiveColor will.
        // When length(emissiveColor) is much greater than length(probeVolumeOutgoingRadiance), this will visually look reasonable.
        // Unfortunately this will break down when emissiveColor is faded out (result will pop).
        // TODO: If evaluating probe volumes in lightloop, only write out sentinel value here, and re-render emissive surfaces.
        // Pre-expose lighting buffer
        outGBuffer3 = float4(all(builtinData.emissiveColor == 0.0) ? builtinData.bakeDiffuseLighting : builtinData.emissiveColor * GetCurrentExposureMultiplier(), 0.0);
    }
    else
#endif
    {
        outGBuffer3 = float4(builtinData.bakeDiffuseLighting * surfaceData.ambientOcclusion + builtinData.emissiveColor, 0.0);
        // Pre-expose lighting buffer
        outGBuffer3.rgb *= GetCurrentExposureMultiplier();
    }

#ifdef LIGHT_LAYERS
    // Note: we need to mask out only 8bits of the layer mask before encoding it as otherwise any value > 255 will map to all layers active
    OUT_GBUFFER_LIGHT_LAYERS = float4(0.0, 0.0, 0.0, (builtinData.renderingLayers & 0x000000FF) / 255.0);
#endif

#ifdef SHADOWS_SHADOWMASK
    OUT_GBUFFER_SHADOWMASK = BUILTIN_DATA_SHADOW_MASK;
#endif

#ifdef UNITY_VIRTUAL_TEXTURING
    OUT_GBUFFER_VTFEEDBACK = builtinData.vtPackedFeedback;
#endif
}

#endif // UNITY_ENCODE_INTO_GBUFFER_UTS_INCLUDED

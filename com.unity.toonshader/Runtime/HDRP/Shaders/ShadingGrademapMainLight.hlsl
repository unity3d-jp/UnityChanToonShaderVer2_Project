//Unity Toon Shader/HDRP
//nobuyuki@unity3d.com
//toshiyuki@unity3d.com (Universal RP/HDRP) 


#ifndef DirectionalShadowType
# if (SHADEROPTIONS_RAYTRACING && (defined(SHADER_API_D3D11) || defined(SHADER_API_D3D12)) && !defined(SHADER_API_XBOXONE) && !defined(SHADER_API_PSSL))
#   define DirectionalShadowType float3
# else
#   define DirectionalShadowType float
# endif
#endif

float3 UTS_MainLightShadingGrademap(LightLoopContext lightLoopContext, FragInputs input, int mainLightIndex, out float inverseClipping, out float channelOutAlpha, out UTSData utsData)
{
    channelOutAlpha = 1.0f;

    uint2 tileIndex = uint2(input.positionSS.xy) / GetTileSize();
    inverseClipping = 0;
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
    /* todo. these should be put int a struct */
    float4 Set_UV0 = input.texCoord0;
    float3x3 tangentTransform = input.tangentToWorld;
    //UnpackNormalmapRGorAG(SAMPLE_TEXTURE2D(_NormalMap, sampler_NormalMap, texCoords))
    float4 n = SAMPLE_TEXTURE2D(_NormalMap, sampler_NormalMap, Set_UV0); // todo. TRANSFORM_TEX
//    float3 _NormalMap_var = UnpackNormalScale(tex2D(_NormalMap, TRANSFORM_TEX(Set_UV0, _NormalMap)), _BumpScale);
    float3 _NormalMap_var = UnpackNormalScale(n, _BumpScale);
    float3 normalLocal = _NormalMap_var.rgb;
    utsData.normalDirection = normalize(mul(normalLocal, tangentTransform)); // Perturbed normals

    float4 _MainTex_var = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, TRANSFORM_TEX(Set_UV0, _MainTex));
    float3 i_normalDir = surfaceData.normalWS;
    utsData.viewDirection = V;


    /* to here todo. these should be put int a struct */
    //v.2.0.4
#ifdef _IS_TRANSCLIPPING_OFF
//
#elif _IS_TRANSCLIPPING_ON
    float4 _ClippingMask_var = tex2D(_ClippingMask, TRANSFORM_TEX(Set_UV0, _ClippingMask));
    float Set_MainTexAlpha = _MainTex_var.a;
    float _IsBaseMapAlphaAsClippingMask_var = lerp(_ClippingMask_var.r, Set_MainTexAlpha, _IsBaseMapAlphaAsClippingMask);
    float _Inverse_Clipping_var = lerp(_IsBaseMapAlphaAsClippingMask_var, (1.0 - _IsBaseMapAlphaAsClippingMask_var), _Inverse_Clipping);
    float Set_Clipping = saturate((_Inverse_Clipping_var + _Clipping_Level));
    clip(Set_Clipping - 0.5);
    inverseClipping = _Inverse_Clipping_var;
#endif



    DirectionalShadowType shadowAttenuation = lightLoopContext.shadowValue;


    float3 mainLihgtDirection = -_DirectionalLightDatas[mainLightIndex].forward;
    float3 mainLightColor = _DirectionalLightDatas[mainLightIndex].color ;


    //v.2.0.4

    float3 defaultLightDirection = normalize(UNITY_MATRIX_V[2].xyz + UNITY_MATRIX_V[1].xyz);
    //v.2.0.5
    float3 defaultLightColor = saturate(max(float3(0.05, 0.05, 0.05) * _Unlit_Intensity, max(ShadeSH9(float4(0.0, 0.0, 0.0, 1.0)), ShadeSH9(float4(0.0, -1.0, 0.0, 1.0)).rgb) * _Unlit_Intensity));
    float3 customLightDirection = normalize(mul(UNITY_MATRIX_M, float4(((float3(1.0, 0.0, 0.0) * _Offset_X_Axis_BLD * 10) + (float3(0.0, 1.0, 0.0) * _Offset_Y_Axis_BLD * 10) + (float3(0.0, 0.0, -1.0) * lerp(-1.0, 1.0, _Inverse_Z_Axis_BLD))), 0)).xyz);
    float3 lightDirection = normalize(lerp(defaultLightDirection, mainLihgtDirection.xyz, any(mainLihgtDirection.xyz)));
    lightDirection = lerp(lightDirection, customLightDirection, _Is_BLD);
    //v.2.0.5: 

    float3 originalLightColor = mainLightColor.rgb;

    float3 lightColor = lerp(max(defaultLightColor, originalLightColor), max(defaultLightColor, saturate(originalLightColor)), _Is_Filter_LightColor);



    ////// Lighting:
    float3 halfDirection = normalize(utsData.viewDirection + lightDirection);
    //v.2.0.5
    _Color = _BaseColor;


    float3 Set_LightColor = lightColor.rgb;

    float3 Set_BaseColor = lerp((_BaseColor.rgb * _MainTex_var.rgb), ((_BaseColor.rgb * _MainTex_var.rgb) * Set_LightColor), _Is_LightColor_Base);
    float Set_BaseColorAlpha = _BaseColorVisible;
    float3 clippingColor = float3(1.0f, 1.0f, 1.0f);
#ifdef _IS_CLIPPING_MATTE
    if (_ClippingMatteMode == 1)
    {
        clippingColor = Set_BaseColor;
        return clippingColor;
    }
#endif // _IS_CLIPPING_MATTE

#ifdef UTS_LAYER_VISIBILITY

    float4 overridingColor = lerp(_BaseColorMaskColor, float4(_BaseColorMaskColor.w, _BaseColorMaskColor.w, _BaseColorMaskColor.w, 1.0f), _ComposerMaskMode);
    float  maskEnabled = max(_BaseColorOverridden, _ComposerMaskMode);
    Set_BaseColor = lerp(Set_BaseColor, overridingColor, maskEnabled);
    Set_BaseColor *= _BaseColorVisible;

#endif //#ifdef UTS_LAYER_VISIBILITY
    //v.2.0.5
    float4 _1st_ShadeMap_var = lerp(SAMPLE_TEXTURE2D(_1st_ShadeMap, sampler_MainTex,TRANSFORM_TEX(Set_UV0, _1st_ShadeMap)), _MainTex_var, _Use_BaseAs1st);
    float3 _Is_LightColor_1st_Shade_var = lerp((_1st_ShadeMap_var.rgb * _1st_ShadeColor.rgb), ((_1st_ShadeMap_var.rgb * _1st_ShadeColor.rgb) * Set_LightColor), _Is_LightColor_1st_Shade);
    float _HalfLambert_var = 0.5 * dot(lerp(i_normalDir, utsData.normalDirection, _Is_NormalMapToBase), lightDirection) + 0.5; // Half Lambert
     //v.2.0.6
    float4 _ShadingGradeMap_var = tex2Dlod(_ShadingGradeMap, float4(TRANSFORM_TEX(Set_UV0, _ShadingGradeMap), 0.0, _BlurLevelSGM));
    //v.2.0.6
    //Minmimum value is same as the Minimum Feather's value with the Minimum Step's value as threshold.
#if !defined (UTS_USE_RAYTRACING_SHADOW)
    shadowAttenuation *= 2.0f;
    shadowAttenuation = saturate(shadowAttenuation);
#endif
    float _SystemShadowsLevel_var = (shadowAttenuation *0.5)+0.5+_Tweak_SystemShadowsLevel > 0.001 ? (shadowAttenuation*0.5)+0.5+_Tweak_SystemShadowsLevel : 0.0001;

    float _ShadingGradeMapLevel_var = _ShadingGradeMap_var.r < 0.95 ? _ShadingGradeMap_var.r + _Tweak_ShadingGradeMapLevel : 1;

    float Set_ShadingGrade = saturate(_ShadingGradeMapLevel_var) * lerp(_HalfLambert_var, (_HalfLambert_var * saturate(_SystemShadowsLevel_var)), _Set_SystemShadowsToBase );

//    float Set_ShadingGrade = saturate(_ShadingGradeMapLevel_var) * lerp(_HalfLambert_var, (_HalfLambert_var * saturate(1.0 + _Tweak_SystemShadowsLevel)), _Set_SystemShadowsToBase);

    float _1stColorFeatherForMask = lerp(_1st_ShadeColor_Feather, 0.0f, max(_ComposerMaskMode, _FirstShadeOverridden));
    //
    float Set_FinalShadowMask = saturate((1.0 + ((Set_ShadingGrade - (_1st_ShadeColor_Step - _1stColorFeatherForMask)) * (0.0 - 1.0)) / (_1st_ShadeColor_Step - (_1st_ShadeColor_Step - _1stColorFeatherForMask)))); // Base and 1st Shade Mask
#ifdef _IS_CLIPPING_MATTE
    if (_ClippingMatteMode == 2)
    {
        clippingColor = _Is_LightColor_1st_Shade_var;
        return clippingColor;
    }
#endif // _IS_CLIPPING_MATTE

#ifdef UTS_LAYER_VISIBILITY
    {
        float4 overridingColor = lerp(_FirstShadeMaskColor, float4(_FirstShadeMaskColor.w, _FirstShadeMaskColor.w, _FirstShadeMaskColor.w, 1.0f), _ComposerMaskMode);
        float  maskEnabled = max(_FirstShadeOverridden, _ComposerMaskMode);
        _Is_LightColor_1st_Shade_var = lerp(_Is_LightColor_1st_Shade_var, overridingColor, maskEnabled);
        _Is_LightColor_1st_Shade_var = lerp(_Is_LightColor_1st_Shade_var, Set_BaseColor, 1.0f - _FirstShadeVisible);
    }
    float Set_1st_ShadeAlpha = _FirstShadeVisible;
#endif //#ifdef UTS_LAYER_VISIBILITY
    float3 _BaseColor_var = lerp(Set_BaseColor, _Is_LightColor_1st_Shade_var, Set_FinalShadowMask);
    //v.2.0.5
    float4 _2nd_ShadeMap_var = lerp(SAMPLE_TEXTURE2D(_2nd_ShadeMap, sampler_MainTex,TRANSFORM_TEX(Set_UV0, _2nd_ShadeMap)), _1st_ShadeMap_var, _Use_1stAs2nd);
    float _2ndColorFeatherForMask = lerp(_2nd_ShadeColor_Feather, 0.0f, max(_SecondShadeOverridden, _ComposerMaskMode));
    float Set_ShadeShadowMask = saturate((1.0 + ((Set_ShadingGrade - (_2nd_ShadeColor_Step - _2ndColorFeatherForMask)) * (0.0 - 1.0)) / (_2nd_ShadeColor_Step - (_2nd_ShadeColor_Step - _2ndColorFeatherForMask)))); // 1st and 2nd Shades Mask
    //Composition: 3 Basic Colors as Set_FinalBaseColor

#ifdef UTS_LAYER_VISIBILITY
    float3 Set_FinalBaseColor;
    {
        float4 overridingColor = lerp(_SecondShadeMaskColor, float4(_SecondShadeMaskColor.w, _SecondShadeMaskColor.w, _SecondShadeMaskColor.w, 1.0f), _ComposerMaskMode);
        float  maskEnabled = max(_SecondShadeOverridden, _ComposerMaskMode);

        float3 _Is_LightColor_2nd_Shade_var = lerp((_2nd_ShadeMap_var.rgb * _2nd_ShadeColor.rgb), ((_2nd_ShadeMap_var.rgb * _2nd_ShadeColor.rgb) * Set_LightColor), _Is_LightColor_2nd_Shade);
        _Is_LightColor_2nd_Shade_var = lerp(_Is_LightColor_2nd_Shade_var, overridingColor, maskEnabled);
        _Is_LightColor_2nd_Shade_var = lerp(_Is_LightColor_2nd_Shade_var, Set_BaseColor, 1.0f - _SecondShadeVisible);
        float Set_2nd_ShadeAlpha = _SecondShadeVisible;
        Set_FinalBaseColor =
            lerp(_BaseColor_var,
                lerp(_Is_LightColor_1st_Shade_var, _Is_LightColor_2nd_Shade_var
                    , Set_ShadeShadowMask)
                , Set_FinalShadowMask);
        channelOutAlpha = 
            lerp(Set_BaseColorAlpha, lerp(Set_1st_ShadeAlpha, Set_2nd_ShadeAlpha, Set_ShadeShadowMask),Set_FinalShadowMask);

    }

#else
    float3 Set_FinalBaseColor =
        lerp(_BaseColor_var,
            lerp(_Is_LightColor_1st_Shade_var,
                lerp((_2nd_ShadeMap_var.rgb * _2nd_ShadeColor.rgb), ((_2nd_ShadeMap_var.rgb * _2nd_ShadeColor.rgb) * Set_LightColor)
                    , _Is_LightColor_2nd_Shade)
                , Set_ShadeShadowMask)
            , Set_FinalShadowMask);
#endif //#ifdef UTS_LAYER_VISIBILITY

#ifdef _IS_CLIPPING_MATTE
    if (_ClippingMatteMode == 3)
    {
        clippingColor = lerp((_2nd_ShadeMap_var.rgb * _2nd_ShadeColor.rgb), ((_2nd_ShadeMap_var.rgb * _2nd_ShadeColor.rgb) * Set_LightColor)
            , _Is_LightColor_2nd_Shade);
        return clippingColor;
    }
#endif // _IS_CLIPPING_MATTE

    float4 _Set_HighColorMask_var = tex2D(_Set_HighColorMask, TRANSFORM_TEX(Set_UV0, _Set_HighColorMask));

    float _Specular_var = 0.5 * dot(halfDirection, lerp(i_normalDir, utsData.normalDirection, _Is_NormalMapToHighColor)) + 0.5; // Specular
    float _TweakHighColorMask_var = (saturate((_Set_HighColorMask_var.g + _Tweak_HighColorMaskLevel)) * lerp((1.0 - step(_Specular_var, (1.0 - pow(_HighColor_Power, 5)))), pow(_Specular_var, exp2(lerp(11, 1, _HighColor_Power))), _Is_SpecularToHighColor));
    float4 _HighColor_Tex_var = tex2D(_HighColor_Tex, TRANSFORM_TEX(Set_UV0, _HighColor_Tex));

    //Composition: 3 Basic Colors and HighColor as Set_HighColor
    float3 _HighColorWithOutTweak_var = lerp((_HighColor_Tex_var.rgb * _HighColor.rgb), ((_HighColor_Tex_var.rgb * _HighColor.rgb) * Set_LightColor), _Is_LightColor_HighColor);
    float3 _HighColor_var = _HighColorWithOutTweak_var * _TweakHighColorMask_var;
#ifdef _IS_CLIPPING_MATTE
    if (_ClippingMatteMode == 4)
    {
        clippingColor = _HighColorWithOutTweak_var;
        return clippingColor;
    }
#endif // _IS_CLIPPING_MATTE
 #ifdef UTS_LAYER_VISIBILITY
    float3 Set_HighColor;
    {
        float4 overridingColor = lerp(_HighlightMaskColor, float4(_HighlightMaskColor.w, _HighlightMaskColor.w, _HighlightMaskColor.w, 1.0f), _ComposerMaskMode);
        float  maskEnabled = max(_HighlightOverridden, _ComposerMaskMode);

        _HighColor_var *= _HighlightVisible;
        Set_HighColor =
            lerp(SATURATE_IF_SDR(Set_FinalBaseColor - _TweakHighColorMask_var), Set_FinalBaseColor,
                lerp(_Is_BlendAddToHiColor, 1.0
                    , _Is_SpecularToHighColor));
        float3 addColor =
            lerp(_HighColor_var, (_HighColor_var * ((1.0 - Set_FinalShadowMask) + (Set_FinalShadowMask * _TweakHighColorOnShadow)))
                , _Is_UseTweakHighColorOnShadow);
        Set_HighColor += addColor;
        if (any(addColor))
        {
            Set_HighColor = lerp(Set_HighColor, overridingColor, maskEnabled);
            channelOutAlpha = _HighlightVisible;
        }
    }
#else
    float3 Set_HighColor = (lerp(SATURATE_IF_SDR((Set_FinalBaseColor - _TweakHighColorMask_var)), Set_FinalBaseColor, lerp(_Is_BlendAddToHiColor, 1.0, _Is_SpecularToHighColor)) + lerp(_HighColor_var, (_HighColor_var * ((1.0 - Set_FinalShadowMask) + (Set_FinalShadowMask * _TweakHighColorOnShadow))), _Is_UseTweakHighColorOnShadow));
#endif


    float4 _Set_RimLightMask_var = tex2D(_Set_RimLightMask, TRANSFORM_TEX(Set_UV0, _Set_RimLightMask));
    float3 _Is_LightColor_RimLight_var = lerp(_RimLightColor.rgb, (_RimLightColor.rgb * Set_LightColor), _Is_LightColor_RimLight);
    float _RimArea_var = (1.0 - dot(lerp(i_normalDir, utsData.normalDirection, _Is_NormalMapToRimLight), utsData.viewDirection));
    float _RimLightPower_var = pow(_RimArea_var, exp2(lerp(3, 0, _RimLight_Power)));
    float _Rimlight_InsideMask_var = saturate(lerp((0.0 + ((_RimLightPower_var - _RimLight_InsideMask) * (1.0 - 0.0)) / (1.0 - _RimLight_InsideMask)), step(_RimLight_InsideMask, _RimLightPower_var), _RimLight_FeatherOff));
    float _VertHalfLambert_var = 0.5 * dot(i_normalDir, lightDirection) + 0.5;
    float3 _LightDirection_MaskOn_var = lerp((_Is_LightColor_RimLight_var * _Rimlight_InsideMask_var), (_Is_LightColor_RimLight_var * saturate((_Rimlight_InsideMask_var - ((1.0 - _VertHalfLambert_var) + _Tweak_LightDirection_MaskLevel)))), _LightDirection_MaskOn);
    float _ApRimLightPower_var = pow(_RimArea_var, exp2(lerp(3, 0, _Ap_RimLight_Power)));
    //Composition: HighColor and RimLight as _RimLight_var

#ifdef UTS_LAYER_VISIBILITY

    float4 overridingRimColor = lerp(_RimLightMaskColor, float4(_RimLightMaskColor.w, _RimLightMaskColor.w, _RimLightMaskColor.w, 1.0f), _ComposerMaskMode);
    float  maskRimEnabled = max(_RimLightOverridden, _ComposerMaskMode);
    float Set_RimLightAlpha = _RimLightVisible;
    float3 Set_RimLight = (SATURATE_IF_SDR((_Set_RimLightMask_var.g + _Tweak_RimLightMaskLevel)) * lerp(_LightDirection_MaskOn_var, (_LightDirection_MaskOn_var + (lerp(_Ap_RimLightColor.rgb, (_Ap_RimLightColor.rgb * Set_LightColor), _Is_LightColor_Ap_RimLight) * saturate((lerp((0.0 + ((_ApRimLightPower_var - _RimLight_InsideMask) * (1.0 - 0.0)) / (1.0 - _RimLight_InsideMask)), step(_RimLight_InsideMask, _ApRimLightPower_var), _Ap_RimLight_FeatherOff) - (saturate(_VertHalfLambert_var) + _Tweak_LightDirection_MaskLevel))))), _Add_Antipodean_RimLight));
    Set_RimLight *= _RimLightVisible;
    float3 _RimLight_var = lerp(Set_HighColor, (Set_HighColor + Set_RimLight), _RimLight);
    if (any(Set_RimLight) * maskRimEnabled)
    {
        _RimLight_var = overridingRimColor;
        channelOutAlpha = Set_RimLightAlpha;
    }

#else
    float3 Set_RimLight = (SATURATE_IF_SDR((_Set_RimLightMask_var.g + _Tweak_RimLightMaskLevel)) * lerp(_LightDirection_MaskOn_var, (_LightDirection_MaskOn_var + (lerp(_Ap_RimLightColor.rgb, (_Ap_RimLightColor.rgb * Set_LightColor), _Is_LightColor_Ap_RimLight) * saturate((lerp((0.0 + ((_ApRimLightPower_var - _RimLight_InsideMask) * (1.0 - 0.0)) / (1.0 - _RimLight_InsideMask)), step(_RimLight_InsideMask, _ApRimLightPower_var), _Ap_RimLight_FeatherOff) - (saturate(_VertHalfLambert_var) + _Tweak_LightDirection_MaskLevel))))), _Add_Antipodean_RimLight));
    float3 _RimLight_var = lerp(Set_HighColor, (Set_HighColor + Set_RimLight), _RimLight);
#endif

    //Matcap
    //v.2.0.6 : CameraRolling Stabilizer
    //Mirror Script Determination: if sign_Mirror = -1, determine "Inside the mirror".
    //v.2.0.7
    utsData.signMirror= 0.0; // i.mirrorFlag; todo.

    //
    float3 _Camera_Right = UNITY_MATRIX_V[0].xyz;
    float3 _Camera_Front = UNITY_MATRIX_V[2].xyz;
    float3 _Up_Unit = float3(0, 1, 0);
    float3 _Right_Axis = cross(_Camera_Front, _Up_Unit);
    //Invert if it's "inside the mirror".
    if (utsData.signMirror < 0) {
        _Right_Axis = -1 * _Right_Axis;
        _Rotate_MatCapUV = -1 * _Rotate_MatCapUV;
    }
    else {
        _Right_Axis = _Right_Axis;
    }
    float _Camera_Right_Magnitude = sqrt(_Camera_Right.x * _Camera_Right.x + _Camera_Right.y * _Camera_Right.y + _Camera_Right.z * _Camera_Right.z);
    float _Right_Axis_Magnitude = sqrt(_Right_Axis.x * _Right_Axis.x + _Right_Axis.y * _Right_Axis.y + _Right_Axis.z * _Right_Axis.z);
    float _Camera_Roll_Cos = dot(_Right_Axis, _Camera_Right) / (_Right_Axis_Magnitude * _Camera_Right_Magnitude);
    utsData.cameraRoll = acos(clamp(_Camera_Roll_Cos, -1, 1));
    utsData.cameraDir = _Camera_Right.y < 0 ? -1 : 1;
    float _Rot_MatCapUV_var_ang = (_Rotate_MatCapUV * 3.141592654) - utsData.cameraDir * utsData.cameraRoll * _CameraRolling_Stabilizer;
    //v.2.0.7
    float2 _Rot_MatCapNmUV_var = RotateUV(Set_UV0, (_Rotate_NormalMapForMatCapUV * 3.141592654), float2(0.5, 0.5), 1.0);
    //V.2.0.6

    float3 _NormalMapForMatCap_var = UnpackNormalScale(tex2D(_NormalMapForMatCap, TRANSFORM_TEX(_Rot_MatCapNmUV_var, _NormalMapForMatCap)), _BumpScaleMatcap);

    //v.2.0.5: MatCap with camera skew correction
    float3 viewNormal = (mul(UNITY_MATRIX_V, float4(lerp(i_normalDir, mul(_NormalMapForMatCap_var.rgb, tangentTransform).rgb, _Is_NormalMapForMatCap), 0))).rgb;
    float3 NormalBlend_MatcapUV_Detail = viewNormal.rgb * float3(-1, -1, 1);
    float3 NormalBlend_MatcapUV_Base = (mul(UNITY_MATRIX_V, float4(utsData.viewDirection, 0)).rgb * float3(-1, -1, 1)) + float3(0, 0, 1);
    float3 noSknewViewNormal = NormalBlend_MatcapUV_Base * dot(NormalBlend_MatcapUV_Base, NormalBlend_MatcapUV_Detail) / NormalBlend_MatcapUV_Base.b - NormalBlend_MatcapUV_Detail;
    float2 _ViewNormalAsMatCapUV = (lerp(noSknewViewNormal, viewNormal, _Is_Ortho).rg * 0.5) + 0.5;
    //
    //v.2.0.7
    float2 _Rot_MatCapUV_var = RotateUV((0.0 + ((_ViewNormalAsMatCapUV - (0.0 + _Tweak_MatCapUV)) * (1.0 - 0.0)) / ((1.0 - _Tweak_MatCapUV) - (0.0 + _Tweak_MatCapUV))), _Rot_MatCapUV_var_ang, float2(0.5, 0.5), 1.0);
    //Invert if it's "inside the mirror".
    if (utsData.signMirror < 0) {
        _Rot_MatCapUV_var.x = 1 - _Rot_MatCapUV_var.x;
    }
    else {
        _Rot_MatCapUV_var = _Rot_MatCapUV_var;
    }

    //v.2.0.6 : LOD of Matcap
    float4 _MatCap_Sampler_var = tex2Dlod(_MatCap_Sampler, float4(TRANSFORM_TEX(_Rot_MatCapUV_var, _MatCap_Sampler), 0.0, _BlurLevelMatcap));
    float4 _Set_MatcapMask_var = tex2D(_Set_MatcapMask, TRANSFORM_TEX(Set_UV0, _Set_MatcapMask));
    //                
    //MatcapMask
    float _Tweak_MatcapMaskLevel_var = saturate(lerp(_Set_MatcapMask_var.g, (1.0 - _Set_MatcapMask_var.g), _Inverse_MatcapMask) + _Tweak_MatcapMaskLevel);
    float3 _Is_LightColor_MatCap_var = lerp((_MatCap_Sampler_var.rgb * _MatCapColor.rgb), ((_MatCap_Sampler_var.rgb * _MatCapColor.rgb) * Set_LightColor), _Is_LightColor_MatCap);
    //v.2.0.6 : ShadowMask on Matcap in Blend mode : multiply
    float3 Set_MatCap = lerp(_Is_LightColor_MatCap_var, (_Is_LightColor_MatCap_var * ((1.0 - Set_FinalShadowMask) + (Set_FinalShadowMask * _TweakMatCapOnShadow)) + lerp(Set_HighColor * Set_FinalShadowMask * (1.0 - _TweakMatCapOnShadow), float3(0.0, 0.0, 0.0), _Is_BlendAddToMatCap)), _Is_UseTweakMatCapOnShadow);


    //
    //v.2.0.6
    //Composition: RimLight and MatCap as finalColor
    //Broke down finalColor composition
    float3 matCapColorOnAddMode = _RimLight_var + Set_MatCap * _Tweak_MatcapMaskLevel_var;
    float _Tweak_MatcapMaskLevel_var_MultiplyMode = _Tweak_MatcapMaskLevel_var * lerp(1, (1 - (Set_FinalShadowMask) * (1 - _TweakMatCapOnShadow)), _Is_UseTweakMatCapOnShadow);
    float3 matCapColorOnMultiplyMode = Set_HighColor * (1 - _Tweak_MatcapMaskLevel_var_MultiplyMode) + Set_HighColor * Set_MatCap * _Tweak_MatcapMaskLevel_var_MultiplyMode + lerp(float3(0, 0, 0), Set_RimLight, _RimLight);
    float3 matCapColorFinal = lerp(matCapColorOnMultiplyMode, matCapColorOnAddMode, _Is_BlendAddToMatCap);
    //v.2.0.4
#ifdef _IS_ANGELRING_OFF
#ifdef _IS_CLIPPING_MATTE
    if (_ClippingMatteMode == 5)
    {
        clippingColor = float3(0.0f,0.0f,0.0f);
        return clippingColor;
    }
#endif // _IS_CLIPPING_MATTE
    float3 finalColor = lerp(_RimLight_var, matCapColorFinal, _MatCap);// Final Composition before Emissive
    //
#elif _IS_ANGELRING_ON
    float3 finalColor = lerp(_RimLight_var, matCapColorFinal, _MatCap);// Final Composition before AR
    //v.2.0.7 AR Camera Rolling Stabilizer
    float3 _AR_OffsetU_var = lerp(mul(UNITY_MATRIX_V, float4(i_normalDir, 0)).xyz, float3(0, 0, 1), _AR_OffsetU);
    float2 AR_VN = _AR_OffsetU_var.xy * 0.5 + float2(0.5, 0.5);
    float2 AR_VN_Rotate = RotateUV(AR_VN, -(utsData.cameraDir * utsData.cameraRoll), float2(0.5, 0.5), 1.0);
    float2 _AR_OffsetV_var = float2(AR_VN_Rotate.x, lerp(input.texCoord1.y, AR_VN_Rotate.y, _AR_OffsetV));
    float4 _AngelRing_Sampler_var = tex2D(_AngelRing_Sampler, TRANSFORM_TEX(_AR_OffsetV_var, _AngelRing_Sampler));
    float3 _Is_LightColor_AR_var = lerp((_AngelRing_Sampler_var.rgb * _AngelRing_Color.rgb), ((_AngelRing_Sampler_var.rgb * _AngelRing_Color.rgb) * Set_LightColor), _Is_LightColor_AR);
    float3 Set_AngelRing = _Is_LightColor_AR_var;
    float Set_ARtexAlpha = _AngelRing_Sampler_var.a;
    float3 Set_AngelRingWithAlpha = (_Is_LightColor_AR_var * _AngelRing_Sampler_var.a);
    //Composition: MatCap and AngelRing as finalColor
# ifdef UTS_LAYER_VISIBILITY
    {
        float4 overridingColor = lerp(_AngelRingMaskColor, float4(_AngelRingMaskColor.w, _AngelRingMaskColor.w, _AngelRingMaskColor.w, 1.0f), _ComposerMaskMode);
        float  maskEnabled = max(_AngelRingOverridden, _ComposerMaskMode);

        _AngelRing *= _AngelRingVisible;
        finalColor = lerp(finalColor, lerp((finalColor + Set_AngelRing), ((finalColor * (1.0 - Set_ARtexAlpha)) + Set_AngelRingWithAlpha), _ARSampler_AlphaOn), _AngelRing);// Final Composition before Emissive
        if (any(Set_AngelRing) * maskEnabled)
        {
            finalColor = lerp(finalColor, lerp(overridingColor.xyz, ((finalColor * (1.0 - Set_ARtexAlpha)) + Set_AngelRingWithAlpha), _ARSampler_AlphaOn), _AngelRing);// Final Composition before Emissive
            channelOutAlpha = _AngelRingVisible;
        }
    }
# else
    finalColor = lerp(finalColor, lerp((finalColor + Set_AngelRing), ((finalColor * (1.0 - Set_ARtexAlpha)) + Set_AngelRingWithAlpha), _ARSampler_AlphaOn), _AngelRing);// Final Composition before Emissive
# endif //#ifdef UTS_LAYER_VISIBILITY
#ifdef _IS_CLIPPING_MATTE
    if (_ClippingMatteMode == 5)
    {
        clippingColor = _Is_LightColor_AR_var;
        return clippingColor;
    }
#endif // _IS_CLIPPING_MATTE
#endif //#ifdef _IS_ANGELRING_OFF



    // without gi yet.

    return finalColor;
}

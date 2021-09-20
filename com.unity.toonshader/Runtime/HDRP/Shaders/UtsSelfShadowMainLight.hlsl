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

float3 UTS_SelfShdowMainLight(LightLoopContext lightLoopContext, FragInputs input, int mainLightIndex)
{

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
    /* todo. these should be put int a struct */
    float4 Set_UV0 = input.texCoord0;
    float3x3 tangentTransform = input.tangentToWorld;
    //UnpackNormalmapRGorAG(SAMPLE_TEXTURE2D(_NormalMap, sampler_NormalMap, texCoords))
    float4 n = SAMPLE_TEXTURE2D(_NormalMap, sampler_NormalMap, Set_UV0.xy);
    //    float3 _NormalMap_var = UnpackNormalScale(tex2D(_NormalMap, TRANSFORM_TEX(Set_UV0, _NormalMap)), _BumpScale);
    float3 _NormalMap_var = UnpackNormalScale(n, _BumpScale);
    float3 normalLocal = _NormalMap_var.rgb;


    float3 i_normalDir = surfaceData.normalWS;

    /* to here todo. these should be put int a struct */


    float shadowAttenuation = (float)lightLoopContext.shadowValue;


    float3 mainLihgtDirection = -_DirectionalLightDatas[mainLightIndex].forward;
    float3 mainLightColor = ApplyCurrentExposureMultiplier(_DirectionalLightDatas[mainLightIndex].color);
    //    float4 tmpColor = EvaluateLight_Directional(context, posInput, _DirectionalLightDatas[mainLightIndex]);
    //    float3 mainLightColor = tmpColor.xyz;
    float3 defaultLightDirection = normalize(UNITY_MATRIX_V[2].xyz + UNITY_MATRIX_V[1].xyz);
    float3 defaultLightColor = saturate(max(float3(0.05, 0.05, 0.05) * _Unlit_Intensity, max(ShadeSH9(float4(0.0, 0.0, 0.0, 1.0)), ShadeSH9(float4(0.0, -1.0, 0.0, 1.0)).rgb) * _Unlit_Intensity));
    float3 customLightDirection = normalize(mul(UNITY_MATRIX_M, float4(((float3(1.0, 0.0, 0.0) * _Offset_X_Axis_BLD * 10) + (float3(0.0, 1.0, 0.0) * _Offset_Y_Axis_BLD * 10) + (float3(0.0, 0.0, -1.0) * lerp(-1.0, 1.0, _Inverse_Z_Axis_BLD))), 0)).xyz);
    float3 lightDirection = normalize(lerp(defaultLightDirection, mainLihgtDirection.xyz, any(mainLihgtDirection.xyz)));
    lightDirection = lerp(lightDirection, customLightDirection, _Is_BLD);

    ////// Lighting:



    float _HalfLambert_var = 0.5 * dot(i_normalDir, lightDirection) + 0.5;
    float lambert = dot(i_normalDir, lightDirection);
    _HalfLambert_var = lambert;

    float baseColorStep = 0.00001;
    float Set_FinalShadowMask = saturate(1.0 + (-_HalfLambert_var) / (baseColorStep));

    float3 Set_FinalBaseColor = 1 - Set_FinalShadowMask;



    return Set_FinalBaseColor;


}

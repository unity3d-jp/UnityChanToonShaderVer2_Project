//Unitychan Toon Shader ver.8.0
//v.8.0.0
//nobuyuki@unity3d.com
//toshiyuki@unity3d.com (Univerasl RP/HDRP)  
//https://github.com/unity3d-jp/UnityChanToonShaderVer2_Project
//(C)Unity Technologies Japan/UCL
//
// this enables to work both 7.17 and 7.31 or higher
#if (SHADER_LIBRARY_VERSION_MAJOR ==7 && SHADER_LIBRARY_VERSION_MINOR >= 3) || (SHADER_LIBRARY_VERSION_MAJOR >= 8)
# ifdef _MAIN_LIGHT_SHADOWS
#  if !defined(_MAIN_LIGHT_SHADOWS_CASCADE) 
#   ifndef REQUIRES_VERTEX_SHADOW_COORD_INTERPOLATOR
#    define REQUIRES_VERTEX_SHADOW_COORD_INTERPOLATOR
#   endif
#  endif
# endif

# ifdef _ADDITIONAL_LIGHTS
#  ifndef  REQUIRES_WORLD_SPACE_POS_INTERPOLATOR
#   define REQUIRES_WORLD_SPACE_POS_INTERPOLATOR
#  endif
# endif
#else
# ifdef _MAIN_LIGHT_SHADOWS
//#  if !defined(_MAIN_LIGHT_SHADOWS_CASCADE) 
#   ifndef REQUIRES_VERTEX_SHADOW_COORD_INTERPOLATOR
#    define REQUIRES_VERTEX_SHADOW_COORD_INTERPOLATOR
#   endif
//#  endif
# endif
# ifdef _ADDITIONAL_LIGHTS
#  ifndef  REQUIRES_WORLD_SPACE_POS_INTERPOLATOR
#   define REQUIRES_WORLD_SPACE_POS_INTERPOLATOR
#  endif
# endif
#endif

            uniform float _utsTechnique;
            uniform sampler2D _MainTex; uniform float4 _MainTex_ST;
#if UCTS_LWRP
#else
            uniform float4 _BaseColor;
#endif
            //v.2.0.5
            uniform float4 _Color;
            uniform fixed _Use_BaseAs1st;
            uniform fixed _Use_1stAs2nd;
            //
            uniform fixed _Is_LightColor_Base;
            uniform sampler2D _1st_ShadeMap; uniform float4 _1st_ShadeMap_ST;
            uniform float4 _1st_ShadeColor;
            uniform fixed _Is_LightColor_1st_Shade;
            uniform sampler2D _2nd_ShadeMap; uniform float4 _2nd_ShadeMap_ST;
            uniform float4 _2nd_ShadeColor;
            uniform fixed _Is_LightColor_2nd_Shade;
            uniform sampler2D _NormalMap; uniform float4 _NormalMap_ST;
            uniform fixed _Is_NormalMapToBase;
            uniform fixed _Set_SystemShadowsToBase;
            uniform float _Tweak_SystemShadowsLevel;
            uniform float _BaseColor_Step;
            uniform float _BaseShade_Feather;
            uniform sampler2D _Set_1st_ShadePosition; uniform float4 _Set_1st_ShadePosition_ST;
            uniform float _ShadeColor_Step;
            uniform float _1st2nd_Shades_Feather;
            uniform sampler2D _Set_2nd_ShadePosition; uniform float4 _Set_2nd_ShadePosition_ST;

// ShadingGradeMap
#if defined(_SHADINGGRADEMAP)
            uniform sampler2D _ShadingGradeMap; uniform float4 _ShadingGradeMap_ST;
#endif
            //v.2.0.6
            uniform float _Tweak_ShadingGradeMapLevel;
            uniform fixed _BlurLevelSGM;
            //
            uniform float _1st_ShadeColor_Step;
            uniform float _1st_ShadeColor_Feather;
            uniform float _2nd_ShadeColor_Step;
            uniform float _2nd_ShadeColor_Feather;
// ShadingGradeMap

            uniform float4 _HighColor;
            uniform sampler2D _HighColor_Tex; uniform float4 _HighColor_Tex_ST;
            uniform fixed _Is_LightColor_HighColor;
            uniform fixed _Is_NormalMapToHighColor;
            uniform float _HighColor_Power;
            uniform fixed _Is_SpecularToHighColor;
            uniform fixed _Is_BlendAddToHiColor;
            uniform fixed _Is_UseTweakHighColorOnShadow;
            uniform float _TweakHighColorOnShadow;
            uniform sampler2D _Set_HighColorMask; uniform float4 _Set_HighColorMask_ST;
            uniform float _Tweak_HighColorMaskLevel;
            uniform fixed _RimLight;
            uniform float4 _RimLightColor;
            uniform fixed _Is_LightColor_RimLight;
            uniform fixed _Is_NormalMapToRimLight;
            uniform float _RimLight_Power;
            uniform float _RimLight_InsideMask;
            uniform fixed _RimLight_FeatherOff;
            uniform fixed _LightDirection_MaskOn;
            uniform float _Tweak_LightDirection_MaskLevel;
            uniform fixed _Add_Antipodean_RimLight;
            uniform float4 _Ap_RimLightColor;
            uniform fixed _Is_LightColor_Ap_RimLight;
            uniform float _Ap_RimLight_Power;
            uniform fixed _Ap_RimLight_FeatherOff;
            uniform sampler2D _Set_RimLightMask; uniform float4 _Set_RimLightMask_ST;
            uniform float _Tweak_RimLightMaskLevel;
            uniform fixed _MatCap;

            uniform sampler2D _MatCap_Sampler; uniform float4 _MatCap_Sampler_ST;

            uniform float4 _MatCapColor;
            uniform fixed _Is_LightColor_MatCap;
            uniform fixed _Is_BlendAddToMatCap;
            uniform float _Tweak_MatCapUV;
            uniform float _Rotate_MatCapUV;
            uniform fixed _Is_NormalMapForMatCap;
            uniform sampler2D _NormalMapForMatCap; uniform float4 _NormalMapForMatCap_ST;
            uniform float _Rotate_NormalMapForMatCapUV;
            uniform fixed _Is_UseTweakMatCapOnShadow;
            uniform float _TweakMatCapOnShadow;
            //MatcapMask
            uniform sampler2D _Set_MatcapMask; uniform float4 _Set_MatcapMask_ST;
            uniform float _Tweak_MatcapMaskLevel;
            //v.2.0.5
            uniform fixed _Is_Ortho;
            //v.2.0.6
            uniform float _CameraRolling_Stabilizer;
            uniform fixed _BlurLevelMatcap;
            uniform fixed _Inverse_MatcapMask;
#if UCTS_LWRP
#else
            uniform float _BumpScale;
#endif
            uniform float _BumpScaleMatcap;
            //Emissive
            uniform sampler2D _Emissive_Tex; uniform float4 _Emissive_Tex_ST;
            uniform float4 _Emissive_Color;
            //v.2.0.7
            uniform fixed _Is_ViewCoord_Scroll;
            uniform float _Rotate_EmissiveUV;
            uniform float _Base_Speed;
            uniform float _Scroll_EmissiveU;
            uniform float _Scroll_EmissiveV;
            uniform fixed _Is_PingPong_Base;
            uniform float4 _ColorShift;
            uniform float4 _ViewShift;
            uniform float _ColorShift_Speed;
            uniform fixed _Is_ColorShift;
            uniform fixed _Is_ViewShift;
            uniform float3 emissive;
            // 
            uniform float _Unlit_Intensity;
            //v.2.0.5
            uniform fixed _Is_Filter_HiCutPointLightColor;
            uniform fixed _Is_Filter_LightColor;
            //v.2.0.4.4
            uniform float _StepOffset;
            uniform fixed _Is_BLD;
            uniform float _Offset_X_Axis_BLD;
            uniform float _Offset_Y_Axis_BLD;
            uniform fixed _Inverse_Z_Axis_BLD;
//v.2.0.4
#ifdef _IS_CLIPPING_MODE
//DoubleShadeWithFeather_Clipping
            uniform sampler2D _ClippingMask; uniform float4 _ClippingMask_ST;
            uniform float _Clipping_Level;
            uniform fixed _Inverse_Clipping;
#elif defined(_IS_CLIPPING_TRANSMODE) || defined(_IS_TRANSCLIPPING_ON)
//DoubleShadeWithFeather_TransClipping
            uniform sampler2D _ClippingMask; uniform float4 _ClippingMask_ST;
            uniform fixed _IsBaseMapAlphaAsClippingMask;
            uniform float _Clipping_Level;
            uniform fixed _Inverse_Clipping;
            uniform float _Tweak_transparency;
#elif defined(_IS_CLIPPING_OFF) || defined(_IS_TRANSCLIPPING_OFF)
//DoubleShadeWithFeather
#endif


            // UV回転をする関数：RotateUV()
            //float2 rotatedUV = RotateUV(i.uv0, (_angular_Verocity*3.141592654), float2(0.5, 0.5), _Time.g);
            float2 RotateUV(float2 _uv, float _radian, float2 _piv, float _time)
            {
                float RotateUV_ang = _radian;
                float RotateUV_cos = cos(_time*RotateUV_ang);
                float RotateUV_sin = sin(_time*RotateUV_ang);
                return (mul(_uv - _piv, float2x2( RotateUV_cos, -RotateUV_sin, RotateUV_sin, RotateUV_cos)) + _piv);
            }
            //
            fixed3 DecodeLightProbe( fixed3 N ){
                return ShadeSH9(float4(N,1));
            }


            inline void InitializeStandardLitSurfaceDataUTS(float2 uv, out SurfaceData outSurfaceData)
            {
                // half4 albedoAlpha = SampleAlbedoAlpha(uv, TEXTURE2D_ARGS(_BaseMap, sampler_BaseMap));
                half4 albedoAlpha = half4(1.0,1.0,1.0,1.0);
 
                outSurfaceData.alpha = Alpha(albedoAlpha.a, _BaseColor, _Cutoff);
            
                half4 specGloss = SampleMetallicSpecGloss(uv, albedoAlpha.a);
                outSurfaceData.albedo = albedoAlpha.rgb * _BaseColor.rgb;
            
            #if _SPECULAR_SETUP
                outSurfaceData.metallic = 1.0h;
                outSurfaceData.specular = specGloss.rgb;
            #else
                outSurfaceData.metallic = specGloss.r;
                outSurfaceData.specular = half3(0.0h, 0.0h, 0.0h);
            #endif
            
                outSurfaceData.smoothness = specGloss.a;
                outSurfaceData.normalTS = SampleNormal(uv, TEXTURE2D_ARGS(_BumpMap, sampler_BumpMap), _BumpScale);
                outSurfaceData.occlusion = SampleOcclusion(uv);
                outSurfaceData.emission = SampleEmission(uv, _EmissionColor.rgb, TEXTURE2D_ARGS(_EmissionMap, sampler_EmissionMap));
            }
            half3 GlobalIlluminationUTS(BRDFData brdfData, half3 bakedGI, half occlusion, half3 normalWS, half3 viewDirectionWS)
            {
                half3 reflectVector = reflect(-viewDirectionWS, normalWS);
                half fresnelTerm = Pow4(1.0 - saturate(dot(normalWS, viewDirectionWS)));

                half3 indirectDiffuse = bakedGI * occlusion;
                half3 indirectSpecular = GlossyEnvironmentReflection(reflectVector, brdfData.perceptualRoughness, occlusion);

                return EnvironmentBRDF(brdfData, indirectDiffuse, indirectSpecular, fresnelTerm);
            }
            uniform float _GI_Intensity;

//v.2.0.4
#if defined(_SHADINGGRADEMAP)

#ifdef _IS_ANGELRING_OFF
//
#elif _IS_ANGELRING_ON
            uniform fixed _AngelRing;

            uniform sampler2D _AngelRing_Sampler; uniform float4 _AngelRing_Sampler_ST;
            uniform float4 _AngelRing_Color;
            uniform fixed _Is_LightColor_AR;
            uniform float _AR_OffsetU;
            uniform float _AR_OffsetV;
            uniform fixed _ARSampler_AlphaOn;

#endif
#endif     //#if defined(_SHADINGGRADEMP)
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float4 tangent : TANGENT;
                float2 texcoord0 : TEXCOORD0;


#ifdef _IS_ANGELRING_OFF
				float2 lightmapUV   : TEXCOORD1;
#elif _IS_ANGELRING_ON
                float2 texcoord1 : TEXCOORD1;
				float2 lightmapUV   : TEXCOORD2;
#endif
				UNITY_VERTEX_INPUT_INSTANCE_ID

            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
//v.2.0.4
#ifdef _IS_ANGELRING_OFF
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
                float3 tangentDir : TEXCOORD3;
                float3 bitangentDir : TEXCOORD4;
                //v.2.0.7
                float mirrorFlag : TEXCOORD5;

				DECLARE_LIGHTMAP_OR_SH(lightmapUV, vertexSH, 6);
				half4 fogFactorAndVertexLight   : TEXCOORD7; // x: fogFactor, yzw: vertex light
# ifndef _MAIN_LIGHT_SHADOWS
				float4 positionCS               : TEXCOORD8;
                int   mainLightID              : TEXCOORD9;
# else
				float4 shadowCoord              : TEXCOORD8;
				float4 positionCS               : TEXCOORD9;
                int   mainLightID              : TEXCOORD10;
# endif
				UNITY_VERTEX_INPUT_INSTANCE_ID
				UNITY_VERTEX_OUTPUT_STEREO

                //
#elif _IS_ANGELRING_ON
                float2 uv1 : TEXCOORD1;
                float4 posWorld : TEXCOORD2;
                float3 normalDir : TEXCOORD3;
                float3 tangentDir : TEXCOORD4;
                float3 bitangentDir : TEXCOORD5;
                //v.2.0.7
                float mirrorFlag : TEXCOORD6;

                DECLARE_LIGHTMAP_OR_SH(lightmapUV, vertexSH, 7);
                half4 fogFactorAndVertexLight   : TEXCOORD8; // x: fogFactor, yzw: vertex light
# ifndef _MAIN_LIGHT_SHADOWS
                float4 positionCS               : TEXCOORD9;
                int   mainLightID              : TEXCOORD10;
# else
                float4 shadowCoord              : TEXCOORD9;
                float4 positionCS               : TEXCOORD10;
                int   mainLightID              : TEXCOORD11;
# endif
                UNITY_VERTEX_INPUT_INSTANCE_ID
                UNITY_VERTEX_OUTPUT_STEREO
#else
                LIGHTING_COORDS(7,8)
                UNITY_FOG_COORDS(9)
#endif
                //

            };
 
            // Abstraction over Light shading data.
            struct UtsLight
            {
                half3   direction;
                half3   color;
                half    distanceAttenuation;
                half    shadowAttenuation;
                int     type;
            };

            ///////////////////////////////////////////////////////////////////////////////
//                      Light Abstraction                                    //
//          /////////////////////////////////////////////////////////////////////////////

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
                light.shadowAttenuation = 1.0;
                light.color = _MainLightColor.rgb;
                light.type = _MainLightPosition.w;
                return light;
            }

            UtsLight GetMainUtsLight(float4 shadowCoord)
            {
                UtsLight light = GetMainUtsLight();
                light.shadowAttenuation = MainLightRealtimeShadow(shadowCoord);
                return light;
            }

            // Fills a light struct given a perObjectLightIndex
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
                light.shadowAttenuation = AdditionalLightRealtimeShadow(perObjectLightIndex, positionWS);
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

            // Fills a light struct given a loop i index. This will convert the i
// index to a perObjectLightIndex
            UtsLight GetAdditionalUtsLight(uint i, float3 positionWS)
            {
                int perObjectLightIndex = GetPerObjectLightIndex(i);
                return GetAdditionalPerObjectUtsLight(perObjectLightIndex, positionWS);
            }

            half3 GetLightColor(UtsLight light)
            {
                return light.color * light.distanceAttenuation;
            }

            int mainLightIndex = -2;
            UtsLight DetermineUTS_MainLight(float3 posW, float4 shadowCoord)
            {

                UtsLight mainLight;
                mainLight.direction = 0;
                mainLight.color = 0;
                mainLight.distanceAttenuation = 0;
                mainLight.shadowAttenuation = 0;
                mainLight.type = 0;
                mainLightIndex = -2;
                UtsLight nextLight = GetMainUtsLight(shadowCoord);
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

#define INIT_UTSLIGHT(utslight) \
            utslight.direction = 0; \
            utslight.color = 0; \
            utslight.distanceAttenuation = 0; \
            utslight.shadowAttenuation = 0; \
            utslight.type = 0


            int DetermineUTS_MainLightIndex(float3 posW, float4 shadowCoord)
            {
                UtsLight mainLight;
                INIT_UTSLIGHT(mainLight);

                int mainLightIndex = -2;
                UtsLight nextLight = GetMainUtsLight(shadowCoord);
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

            UtsLight GetMainUtsLightByID(int index,float3 posW, float4 shadowCoord)
            {
                UtsLight mainLight;
                INIT_UTSLIGHT(mainLight);
                if (index == -2)
                {
                    return mainLight;
                }
                if (index == -1)
                {
                    return GetMainUtsLight(shadowCoord);
                }
                return GetAdditionalUtsLight(index, posW);
            }
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;

                UNITY_SETUP_INSTANCE_ID(v);
                UNITY_TRANSFER_INSTANCE_ID(v, o);
                UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);

                o.uv0 = v.texcoord0;
//v.2.0.4
#ifdef _IS_ANGELRING_OFF
//
#elif _IS_ANGELRING_ON
                o.uv1 = v.texcoord1;
#endif
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.tangentDir = normalize( mul( unity_ObjectToWorld, float4( v.tangent.xyz, 0.0 ) ).xyz );
                o.bitangentDir = normalize(cross(o.normalDir, o.tangentDir) * v.tangent.w);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);

                o.pos = UnityObjectToClipPos( v.vertex );
                //v.2.0.7 鏡の中判定（右手座標系か、左手座標系かの判定）o.mirrorFlag = -1 なら鏡の中.
                float3 crossFwd = cross(UNITY_MATRIX_V[0], UNITY_MATRIX_V[1]);
                o.mirrorFlag = dot(crossFwd, UNITY_MATRIX_V[2]) < 0 ? 1 : -1;
                //

                float3 positionWS = TransformObjectToWorld(v.vertex);
                float4 positionCS = TransformWorldToHClip(positionWS);
                half3 vertexLight = VertexLighting(o.posWorld, o.normalDir);
                half fogFactor = ComputeFogFactor(positionCS.z);

                OUTPUT_LIGHTMAP_UV(v.lightmapUV, unity_LightmapST, o.lightmapUV);
                OUTPUT_SH(o.normalDir.xyz, o.vertexSH);

                o.fogFactorAndVertexLight = half4(fogFactor, vertexLight);
                o.positionCS = positionCS;
  #if defined(_MAIN_LIGHT_SHADOWS) && !defined(_RECEIVE_SHADOWS_OFF)
    #if SHADOWS_SCREEN
                o.shadowCoord = ComputeScreenPos(positionCS);
    #else
                o.shadowCoord = TransformWorldToShadowCoord(o.posWorld);
    #endif
                o.mainLightID = DetermineUTS_MainLightIndex(o.posWorld, o.shadowCoord);
  #else
                o.mainLightID = DetermineUTS_MainLightIndex(o.posWorld, 0);
  #endif

		
                return o;
            }



#if defined(_SHADINGGRADEMAP)
        float4 fragShadingGradeMap(VertexOutput i, fixed facing : VFACE) : SV_TARGET
        {
            #  ifdef _MAIN_LIGHT_SHADOWS
                UtsLight mainLight = GetMainUtsLightByID(i.mainLightID, i.posWorld.xyz, i.shadowCoord);
            #  else
                UtsLight mainLight = GetMainUtsLightByID(i.mainLightID, i.posWorld.xyz, 0);
            #  endif
                half3 mainLightColor = GetLightColor(mainLight);
                i.normalDir = normalize(i.normalDir);
                float3x3 tangentTransform = float3x3( i.tangentDir, i.bitangentDir, i.normalDir);
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float2 Set_UV0 = i.uv0;
                //v.2.0.6


                float3 _NormalMap_var = UnpackNormalScale(tex2D(_NormalMap, TRANSFORM_TEX(Set_UV0, _NormalMap)), _BumpScale);

                float3 normalLocal = _NormalMap_var.rgb;
                float3 normalDirection = normalize(mul( normalLocal, tangentTransform )); // Perturbed normals


                // todo. not necessary to calc gi factor in  shadowcaster pass.
                SurfaceData surfaceData;
                InitializeStandardLitSurfaceDataUTS(i.uv0, surfaceData);

                InputData inputData;
                Varyings  input;

                // todo.  it has to be cared more.
                UNITY_SETUP_INSTANCE_ID(input);
                UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX(input);
                input.vertexSH = i.vertexSH;
                input.uv = i.uv0;
                input.fogFactorAndVertexLight = i.fogFactorAndVertexLight;
#  ifdef REQUIRES_VERTEX_SHADOW_COORD_INTERPOLATOR
                input.shadowCoord = i.shadowCoord;
#  endif

#  ifdef REQUIRES_WORLD_SPACE_POS_INTERPOLATOR
                input.positionWS = i.posWorld.xyz;
#  endif
#  ifdef _NORMALMAP
                input.normalWS = half4(i.normalDir, viewDirection.x);      // xyz: normal, w: viewDir.x
                input.tangentWS = half4(i.tangentDir, viewDirection.y);        // xyz: tangent, w: viewDir.y
                input.bitangentWS = half4(i.bitangentDir, viewDirection.z);    // xyz: bitangent, w: viewDir.z
#  else
                input.normalWS  = half3(i.normalDir);
                input.viewDirWS = half3(viewDirection);
#  endif
                InitializeInputData(input, surfaceData.normalTS, inputData);

                BRDFData brdfData;
                InitializeBRDFData(surfaceData.albedo,
                    surfaceData.metallic,
                    surfaceData.specular,
                    surfaceData.smoothness,
                    surfaceData.alpha, brdfData);

                half3 envColor = GlobalIlluminationUTS(brdfData, inputData.bakedGI, surfaceData.occlusion, inputData.normalWS, inputData.viewDirectionWS);
                envColor *= 1.8f;



                float4 _MainTex_var = tex2D(_MainTex,TRANSFORM_TEX(Set_UV0, _MainTex));
//v.2.0.4
#ifdef _IS_TRANSCLIPPING_OFF
//
#elif _IS_TRANSCLIPPING_ON
                float4 _ClippingMask_var = tex2D(_ClippingMask,TRANSFORM_TEX(Set_UV0, _ClippingMask));
                float Set_MainTexAlpha = _MainTex_var.a;
                float _IsBaseMapAlphaAsClippingMask_var = lerp( _ClippingMask_var.r, Set_MainTexAlpha, _IsBaseMapAlphaAsClippingMask );
                float _Inverse_Clipping_var = lerp( _IsBaseMapAlphaAsClippingMask_var, (1.0 - _IsBaseMapAlphaAsClippingMask_var), _Inverse_Clipping );
                float Set_Clipping = saturate((_Inverse_Clipping_var+_Clipping_Level));
                clip(Set_Clipping - 0.5);
#endif


                half shadowAttenuation = 1.0;

# ifdef _MAIN_LIGHT_SHADOWS

                shadowAttenuation = mainLight.shadowAttenuation;

# endif


//v.2.0.4

                float3 defaultLightDirection = normalize(UNITY_MATRIX_V[2].xyz + UNITY_MATRIX_V[1].xyz);
                //v.2.0.5
                float3 defaultLightColor = saturate(max(half3(0.05,0.05,0.05)*_Unlit_Intensity,max(ShadeSH9(half4(0.0, 0.0, 0.0, 1.0)),ShadeSH9(half4(0.0, -1.0, 0.0, 1.0)).rgb)*_Unlit_Intensity));
                float3 customLightDirection = normalize(mul( unity_ObjectToWorld, float4(((float3(1.0,0.0,0.0)*_Offset_X_Axis_BLD*10)+(float3(0.0,1.0,0.0)*_Offset_Y_Axis_BLD*10)+(float3(0.0,0.0,-1.0)*lerp(-1.0,1.0,_Inverse_Z_Axis_BLD))),0)).xyz);
                float3 lightDirection = normalize(lerp(defaultLightDirection, mainLight.direction.xyz,any(mainLight.direction.xyz)));
                lightDirection = lerp(lightDirection, customLightDirection, _Is_BLD);
                //v.2.0.5: 

                half3 originalLightColor = mainLightColor.rgb;

                float3 lightColor = lerp(max(defaultLightColor, originalLightColor), max(defaultLightColor, saturate(originalLightColor)), _Is_Filter_LightColor);



////// Lighting:
                float3 halfDirection = normalize(viewDirection+lightDirection);
                //v.2.0.5
                _Color = _BaseColor;

#ifdef _IS_PASS_FWDBASE
                float3 Set_LightColor = lightColor.rgb;
                float3 Set_BaseColor = lerp( (_MainTex_var.rgb*_BaseColor.rgb), ((_MainTex_var.rgb*_BaseColor.rgb)*Set_LightColor), _Is_LightColor_Base );
                //v.2.0.5
                float4 _1st_ShadeMap_var = lerp(tex2D(_1st_ShadeMap,TRANSFORM_TEX(Set_UV0, _1st_ShadeMap)),_MainTex_var,_Use_BaseAs1st);
                float3 _Is_LightColor_1st_Shade_var = lerp( (_1st_ShadeMap_var.rgb*_1st_ShadeColor.rgb), ((_1st_ShadeMap_var.rgb*_1st_ShadeColor.rgb)*Set_LightColor), _Is_LightColor_1st_Shade );
                float _HalfLambert_var = 0.5*dot(lerp( i.normalDir, normalDirection, _Is_NormalMapToBase ),lightDirection)+0.5; // Half Lambert
                //float4 _ShadingGradeMap_var = tex2D(_ShadingGradeMap,TRANSFORM_TEX(Set_UV0, _ShadingGradeMap));
                //v.2.0.6
                float4 _ShadingGradeMap_var = tex2Dlod(_ShadingGradeMap,float4(TRANSFORM_TEX(Set_UV0, _ShadingGradeMap),0.0,_BlurLevelSGM));
                //v.2.0.6
                //Minmimum value is same as the Minimum Feather's value with the Minimum Step's value as threshold.
                //Kobayashi:LWRPではターミネータにノイズが乗ることがあるのでコメントオフ
                //float _SystemShadowsLevel_var = (attenuation*0.5)+0.5+_Tweak_SystemShadowsLevel > 0.001 ? (attenuation*0.5)+0.5+_Tweak_SystemShadowsLevel : 0.0001;

                float _ShadingGradeMapLevel_var = _ShadingGradeMap_var.r < 0.95 ? _ShadingGradeMap_var.r+_Tweak_ShadingGradeMapLevel : 1;

                //float Set_ShadingGrade = saturate(_ShadingGradeMapLevel_var)*lerp( _HalfLambert_var, (_HalfLambert_var*saturate(_SystemShadowsLevel_var)), _Set_SystemShadowsToBase );

                float Set_ShadingGrade = saturate(_ShadingGradeMapLevel_var)*lerp( _HalfLambert_var, (_HalfLambert_var*saturate(1.0+_Tweak_SystemShadowsLevel)), _Set_SystemShadowsToBase );

                //
                float Set_FinalShadowMask = saturate((1.0 + ( (Set_ShadingGrade - (_1st_ShadeColor_Step-_1st_ShadeColor_Feather)) * (0.0 - 1.0) ) / (_1st_ShadeColor_Step - (_1st_ShadeColor_Step-_1st_ShadeColor_Feather)))); // Base and 1st Shade Mask
                float3 _BaseColor_var = lerp(Set_BaseColor,_Is_LightColor_1st_Shade_var,Set_FinalShadowMask);
                //v.2.0.5
                float4 _2nd_ShadeMap_var = lerp(tex2D(_2nd_ShadeMap,TRANSFORM_TEX(Set_UV0, _2nd_ShadeMap)),_1st_ShadeMap_var,_Use_1stAs2nd);
                float Set_ShadeShadowMask = saturate((1.0 + ( (Set_ShadingGrade - (_2nd_ShadeColor_Step-_2nd_ShadeColor_Feather)) * (0.0 - 1.0) ) / (_2nd_ShadeColor_Step - (_2nd_ShadeColor_Step-_2nd_ShadeColor_Feather)))); // 1st and 2nd Shades Mask
                //Composition: 3 Basic Colors as Set_FinalBaseColor
                float3 Set_FinalBaseColor = lerp(_BaseColor_var,lerp(_Is_LightColor_1st_Shade_var,lerp( (_2nd_ShadeMap_var.rgb*_2nd_ShadeColor.rgb), ((_2nd_ShadeMap_var.rgb*_2nd_ShadeColor.rgb)*Set_LightColor), _Is_LightColor_2nd_Shade ),Set_ShadeShadowMask),Set_FinalShadowMask);
                float4 _Set_HighColorMask_var = tex2D(_Set_HighColorMask,TRANSFORM_TEX(Set_UV0, _Set_HighColorMask));
                float _Specular_var = 0.5*dot(halfDirection,lerp( i.normalDir, normalDirection, _Is_NormalMapToHighColor ))+0.5; // Specular
                float _TweakHighColorMask_var = (saturate((_Set_HighColorMask_var.g+_Tweak_HighColorMaskLevel))*lerp( (1.0 - step(_Specular_var,(1.0 - pow(_HighColor_Power,5)))), pow(_Specular_var,exp2(lerp(11,1,_HighColor_Power))), _Is_SpecularToHighColor ));
                float4 _HighColor_Tex_var = tex2D(_HighColor_Tex,TRANSFORM_TEX(Set_UV0, _HighColor_Tex));
                float3 _HighColor_var = (lerp( (_HighColor_Tex_var.rgb*_HighColor.rgb), ((_HighColor_Tex_var.rgb*_HighColor.rgb)*Set_LightColor), _Is_LightColor_HighColor )*_TweakHighColorMask_var);
                //Composition: 3 Basic Colors and HighColor as Set_HighColor
                float3 Set_HighColor = (lerp( saturate((Set_FinalBaseColor-_TweakHighColorMask_var)), Set_FinalBaseColor, lerp(_Is_BlendAddToHiColor,1.0,_Is_SpecularToHighColor) )+lerp( _HighColor_var, (_HighColor_var*((1.0 - Set_FinalShadowMask)+(Set_FinalShadowMask*_TweakHighColorOnShadow))), _Is_UseTweakHighColorOnShadow ));
                float4 _Set_RimLightMask_var = tex2D(_Set_RimLightMask,TRANSFORM_TEX(Set_UV0, _Set_RimLightMask));
                float3 _Is_LightColor_RimLight_var = lerp( _RimLightColor.rgb, (_RimLightColor.rgb*Set_LightColor), _Is_LightColor_RimLight );
                float _RimArea_var = (1.0 - dot(lerp( i.normalDir, normalDirection, _Is_NormalMapToRimLight ),viewDirection));
                float _RimLightPower_var = pow(_RimArea_var,exp2(lerp(3,0,_RimLight_Power)));
                float _Rimlight_InsideMask_var = saturate(lerp( (0.0 + ( (_RimLightPower_var - _RimLight_InsideMask) * (1.0 - 0.0) ) / (1.0 - _RimLight_InsideMask)), step(_RimLight_InsideMask,_RimLightPower_var), _RimLight_FeatherOff ));
                float _VertHalfLambert_var = 0.5*dot(i.normalDir,lightDirection)+0.5;
                float3 _LightDirection_MaskOn_var = lerp( (_Is_LightColor_RimLight_var*_Rimlight_InsideMask_var), (_Is_LightColor_RimLight_var*saturate((_Rimlight_InsideMask_var-((1.0 - _VertHalfLambert_var)+_Tweak_LightDirection_MaskLevel)))), _LightDirection_MaskOn );
                float _ApRimLightPower_var = pow(_RimArea_var,exp2(lerp(3,0,_Ap_RimLight_Power)));
                float3 Set_RimLight = (saturate((_Set_RimLightMask_var.g+_Tweak_RimLightMaskLevel))*lerp( _LightDirection_MaskOn_var, (_LightDirection_MaskOn_var+(lerp( _Ap_RimLightColor.rgb, (_Ap_RimLightColor.rgb*Set_LightColor), _Is_LightColor_Ap_RimLight )*saturate((lerp( (0.0 + ( (_ApRimLightPower_var - _RimLight_InsideMask) * (1.0 - 0.0) ) / (1.0 - _RimLight_InsideMask)), step(_RimLight_InsideMask,_ApRimLightPower_var), _Ap_RimLight_FeatherOff )-(saturate(_VertHalfLambert_var)+_Tweak_LightDirection_MaskLevel))))), _Add_Antipodean_RimLight ));
                //Composition: HighColor and RimLight as _RimLight_var
                float3 _RimLight_var = lerp( Set_HighColor, (Set_HighColor+Set_RimLight), _RimLight );
                //Matcap
                //v.2.0.6 : CameraRolling Stabilizer
                //鏡スクリプト判定：_sign_Mirror = -1 なら、鏡の中と判定.
                //v.2.0.7
                fixed _sign_Mirror = i.mirrorFlag;
                //
                float3 _Camera_Right = UNITY_MATRIX_V[0].xyz;
                float3 _Camera_Front = UNITY_MATRIX_V[2].xyz;
                float3 _Up_Unit = float3(0, 1, 0);
                float3 _Right_Axis = cross(_Camera_Front, _Up_Unit);
                //鏡の中なら反転.
                if(_sign_Mirror < 0){
                    _Right_Axis = -1 * _Right_Axis;
                    _Rotate_MatCapUV = -1 * _Rotate_MatCapUV;
                }else{
                    _Right_Axis = _Right_Axis;
                }
                float _Camera_Right_Magnitude = sqrt(_Camera_Right.x*_Camera_Right.x + _Camera_Right.y*_Camera_Right.y + _Camera_Right.z*_Camera_Right.z);
                float _Right_Axis_Magnitude = sqrt(_Right_Axis.x*_Right_Axis.x + _Right_Axis.y*_Right_Axis.y + _Right_Axis.z*_Right_Axis.z);
                float _Camera_Roll_Cos = dot(_Right_Axis, _Camera_Right) / (_Right_Axis_Magnitude * _Camera_Right_Magnitude);
                float _Camera_Roll = acos(clamp(_Camera_Roll_Cos, -1, 1));
                fixed _Camera_Dir = _Camera_Right.y < 0 ? -1 : 1;
                float _Rot_MatCapUV_var_ang = (_Rotate_MatCapUV*3.141592654) - _Camera_Dir*_Camera_Roll*_CameraRolling_Stabilizer;
                //v.2.0.7
                float2 _Rot_MatCapNmUV_var = RotateUV(Set_UV0, (_Rotate_NormalMapForMatCapUV*3.141592654), float2(0.5, 0.5), 1.0);
                //V.2.0.6

                float3 _NormalMapForMatCap_var = UnpackNormalScale(tex2D(_NormalMapForMatCap, TRANSFORM_TEX(_Rot_MatCapNmUV_var, _NormalMapForMatCap)), _BumpScaleMatcap);

                //v.2.0.5: MatCap with camera skew correction
                float3 viewNormal = (mul(UNITY_MATRIX_V, float4(lerp( i.normalDir, mul( _NormalMapForMatCap_var.rgb, tangentTransform ).rgb, _Is_NormalMapForMatCap ),0))).rgb;
                float3 NormalBlend_MatcapUV_Detail = viewNormal.rgb * float3(-1,-1,1);
                float3 NormalBlend_MatcapUV_Base = (mul( UNITY_MATRIX_V, float4(viewDirection,0) ).rgb*float3(-1,-1,1)) + float3(0,0,1);
                float3 noSknewViewNormal = NormalBlend_MatcapUV_Base*dot(NormalBlend_MatcapUV_Base, NormalBlend_MatcapUV_Detail)/NormalBlend_MatcapUV_Base.b - NormalBlend_MatcapUV_Detail;                
                float2 _ViewNormalAsMatCapUV = (lerp(noSknewViewNormal,viewNormal,_Is_Ortho).rg*0.5)+0.5;
                //
                //v.2.0.7
                float2 _Rot_MatCapUV_var = RotateUV((0.0 + ((_ViewNormalAsMatCapUV - (0.0+_Tweak_MatCapUV)) * (1.0 - 0.0) ) / ((1.0-_Tweak_MatCapUV) - (0.0+_Tweak_MatCapUV))), _Rot_MatCapUV_var_ang, float2(0.5, 0.5), 1.0);
                //鏡の中ならUV左右反転.
                if(_sign_Mirror < 0){
                    _Rot_MatCapUV_var.x = 1-_Rot_MatCapUV_var.x;
                }else{
                    _Rot_MatCapUV_var = _Rot_MatCapUV_var;
                }

                //v.2.0.6 : LOD of Matcap
                float4 _MatCap_Sampler_var = tex2Dlod(_MatCap_Sampler,float4(TRANSFORM_TEX(_Rot_MatCapUV_var, _MatCap_Sampler),0.0,_BlurLevelMatcap));
                //                
                //MatcapMask
                float4 _Set_MatcapMask_var = tex2D(_Set_MatcapMask,TRANSFORM_TEX(Set_UV0, _Set_MatcapMask));
                float _Tweak_MatcapMaskLevel_var = saturate(lerp(_Set_MatcapMask_var.g, (1.0 - _Set_MatcapMask_var.g), _Inverse_MatcapMask) + _Tweak_MatcapMaskLevel);
                float3 _Is_LightColor_MatCap_var = lerp( (_MatCap_Sampler_var.rgb*_MatCapColor.rgb), ((_MatCap_Sampler_var.rgb*_MatCapColor.rgb)*Set_LightColor), _Is_LightColor_MatCap );
                //v.2.0.6 : ShadowMask on Matcap in Blend mode : multiply
                float3 Set_MatCap = lerp( _Is_LightColor_MatCap_var, (_Is_LightColor_MatCap_var*((1.0 - Set_FinalShadowMask)+(Set_FinalShadowMask*_TweakMatCapOnShadow)) + lerp(Set_HighColor*Set_FinalShadowMask*(1.0-_TweakMatCapOnShadow), float3(0.0, 0.0, 0.0), _Is_BlendAddToMatCap)), _Is_UseTweakMatCapOnShadow );

                //
                //v.2.0.6
                //Composition: RimLight and MatCap as finalColor
                //Broke down finalColor composition
                float3 matCapColorOnAddMode = _RimLight_var+Set_MatCap*_Tweak_MatcapMaskLevel_var;
                float _Tweak_MatcapMaskLevel_var_MultiplyMode = _Tweak_MatcapMaskLevel_var * lerp (1, (1 - (Set_FinalShadowMask)*(1 - _TweakMatCapOnShadow)), _Is_UseTweakMatCapOnShadow);
                float3 matCapColorOnMultiplyMode = Set_HighColor*(1-_Tweak_MatcapMaskLevel_var_MultiplyMode) + Set_HighColor*Set_MatCap*_Tweak_MatcapMaskLevel_var_MultiplyMode + lerp(float3(0,0,0),Set_RimLight,_RimLight);
                float3 matCapColorFinal = lerp(matCapColorOnMultiplyMode, matCapColorOnAddMode, _Is_BlendAddToMatCap);
//v.2.0.4
#ifdef _IS_ANGELRING_OFF
                float3 finalColor = lerp(_RimLight_var, matCapColorFinal, _MatCap);// Final Composition before Emissive
                //
#elif _IS_ANGELRING_ON
                float3 finalColor = lerp(_RimLight_var, matCapColorFinal, _MatCap);// Final Composition before AR
                //v.2.0.7 AR Camera Rolling Stabilizer
                float3 _AR_OffsetU_var = lerp(mul(UNITY_MATRIX_V, float4(i.normalDir,0)).xyz,float3(0,0,1),_AR_OffsetU);
                float2 AR_VN = _AR_OffsetU_var.xy*0.5 + float2(0.5,0.5);
                float2 AR_VN_Rotate = RotateUV(AR_VN, -(_Camera_Dir*_Camera_Roll), float2(0.5,0.5), 1.0);
                float2 _AR_OffsetV_var = float2(AR_VN_Rotate.x, lerp(i.uv1.y, AR_VN_Rotate.y, _AR_OffsetV));
                float4 _AngelRing_Sampler_var = tex2D(_AngelRing_Sampler,TRANSFORM_TEX(_AR_OffsetV_var, _AngelRing_Sampler));
                float3 _Is_LightColor_AR_var = lerp( (_AngelRing_Sampler_var.rgb*_AngelRing_Color.rgb), ((_AngelRing_Sampler_var.rgb*_AngelRing_Color.rgb)*Set_LightColor), _Is_LightColor_AR );
                float3 Set_AngelRing = _Is_LightColor_AR_var;
                float Set_ARtexAlpha = _AngelRing_Sampler_var.a;
                float3 Set_AngelRingWithAlpha = (_Is_LightColor_AR_var*_AngelRing_Sampler_var.a);
                //Composition: MatCap and AngelRing as finalColor
                finalColor = lerp(finalColor, lerp((finalColor + Set_AngelRing), ((finalColor*(1.0 - Set_ARtexAlpha))+Set_AngelRingWithAlpha), _ARSampler_AlphaOn ), _AngelRing );// Final Composition before Emissive
#endif
//v.2.0.7
#ifdef _EMISSIVE_SIMPLE
                float4 _Emissive_Tex_var = tex2D(_Emissive_Tex,TRANSFORM_TEX(Set_UV0, _Emissive_Tex));
                float emissiveMask = _Emissive_Tex_var.a;
                emissive = _Emissive_Tex_var.rgb * _Emissive_Color.rgb * emissiveMask;
#elif _EMISSIVE_ANIMATION
                //v.2.0.7 Calculation View Coord UV for Scroll 
                float3 viewNormal_Emissive = (mul(UNITY_MATRIX_V, float4(i.normalDir,0))).xyz;
                float3 NormalBlend_Emissive_Detail = viewNormal_Emissive * float3(-1,-1,1);
                float3 NormalBlend_Emissive_Base = (mul( UNITY_MATRIX_V, float4(viewDirection,0)).xyz*float3(-1,-1,1)) + float3(0,0,1);
                float3 noSknewViewNormal_Emissive = NormalBlend_Emissive_Base*dot(NormalBlend_Emissive_Base, NormalBlend_Emissive_Detail)/NormalBlend_Emissive_Base.z - NormalBlend_Emissive_Detail;
                float2 _ViewNormalAsEmissiveUV = noSknewViewNormal_Emissive.xy*0.5+0.5;
                float2 _ViewCoord_UV = RotateUV(_ViewNormalAsEmissiveUV, -(_Camera_Dir*_Camera_Roll), float2(0.5,0.5), 1.0);
                //鏡の中ならUV左右反転.
                if(_sign_Mirror < 0){
                    _ViewCoord_UV.x = 1-_ViewCoord_UV.x;
                }else{
                    _ViewCoord_UV = _ViewCoord_UV;
                }
                float2 emissive_uv = lerp(i.uv0, _ViewCoord_UV, _Is_ViewCoord_Scroll);
                //
                float4 _time_var = _Time;
                float _base_Speed_var = (_time_var.g*_Base_Speed);
                float _Is_PingPong_Base_var = lerp(_base_Speed_var, sin(_base_Speed_var), _Is_PingPong_Base );
                float2 scrolledUV = emissive_uv + float2(_Scroll_EmissiveU, _Scroll_EmissiveV)*_Is_PingPong_Base_var;
                float rotateVelocity = _Rotate_EmissiveUV*3.141592654;
                float2 _rotate_EmissiveUV_var = RotateUV(scrolledUV, rotateVelocity, float2(0.5, 0.5), _Is_PingPong_Base_var);
                float4 _Emissive_Tex_var = tex2D(_Emissive_Tex,TRANSFORM_TEX(Set_UV0, _Emissive_Tex));
                float emissiveMask = _Emissive_Tex_var.a;
                _Emissive_Tex_var = tex2D(_Emissive_Tex,TRANSFORM_TEX(_rotate_EmissiveUV_var, _Emissive_Tex));
                float _colorShift_Speed_var = 1.0 - cos(_time_var.g*_ColorShift_Speed);
                float viewShift_var = smoothstep( 0.0, 1.0, max(0,dot(normalDirection,viewDirection)));
                float4 colorShift_Color = lerp(_Emissive_Color, lerp(_Emissive_Color, _ColorShift, _colorShift_Speed_var), _Is_ColorShift);
                float4 viewShift_Color = lerp(_ViewShift, colorShift_Color, viewShift_var);
                float4 emissive_Color = lerp(colorShift_Color, viewShift_Color, _Is_ViewShift);
                emissive = emissive_Color.rgb * _Emissive_Tex_var.rgb * emissiveMask;
#endif
//
                //v.2.0.6: GI_Intensity with Intensity Multiplier Filter

                float3 envLightColor = envColor.rgb;

                float envLightIntensity = 0.299*envLightColor.r + 0.587*envLightColor.g + 0.114*envLightColor.b <1 ? (0.299*envLightColor.r + 0.587*envLightColor.g + 0.114*envLightColor.b) : 1;



                float3 pointLightColor = 0;
  #ifdef _ADDITIONAL_LIGHTS

                int pixelLightCount = GetAdditionalLightsCount();

  #if 1 // determine main light inorder to apply light culling properly
                for (int iLight = -1; iLight < pixelLightCount ; ++iLight)
                {
                    if (iLight != i.mainLightID)
                    {
                        float notDirectional = 1.0f; //_WorldSpaceLightPos0.w of the legacy code.
                        UtsLight additionalLight = GetMainUtsLight(0);
                        if (iLight != -1)
                        {
                            additionalLight = GetAdditionalUtsLight(iLight, inputData.positionWS);
                        }
                        half3 additionalLightColor = GetLightColor(additionalLight);



                        float3 lightDirection = additionalLight.direction;
                        //v.2.0.5: 
                        float3 addPassLightColor = (0.5*dot(lerp(i.normalDir, normalDirection, _Is_NormalMapToBase), lightDirection) + 0.5) * additionalLightColor.rgb;
                        float  pureIntencity = max(0.001, (0.299*additionalLightColor.r + 0.587*additionalLightColor.g + 0.114*additionalLightColor.b));
                        float3 lightColor = max(0, lerp(addPassLightColor, lerp(0, min(addPassLightColor, addPassLightColor / pureIntencity), notDirectional), _Is_Filter_LightColor));
                        float3 halfDirection = normalize(viewDirection + lightDirection); // has to be recalced here.

                        //v.2.0.5:
                        _1st_ShadeColor_Step = saturate(_1st_ShadeColor_Step + _StepOffset);
                        _2nd_ShadeColor_Step = saturate(_2nd_ShadeColor_Step + _StepOffset);
                        //
                        //v.2.0.5: If Added lights is directional, set 0 as _LightIntensity
                        float _LightIntensity = lerp(0, (0.299*additionalLightColor.r + 0.587*additionalLightColor.g + 0.114*additionalLightColor.b), notDirectional);
                        //v.2.0.5: Filtering the high intensity zone of PointLights
                        float3 Set_LightColor = lerp(lightColor, lerp(lightColor, min(lightColor, additionalLightColor.rgb*_1st_ShadeColor_Step), notDirectional), _Is_Filter_HiCutPointLightColor);
                        //
                        float3 Set_BaseColor = lerp((_BaseColor.rgb*_MainTex_var.rgb*_LightIntensity), ((_BaseColor.rgb*_MainTex_var.rgb)*Set_LightColor), _Is_LightColor_Base);
                        //v.2.0.5
                        float4 _1st_ShadeMap_var = lerp(tex2D(_1st_ShadeMap, TRANSFORM_TEX(Set_UV0, _1st_ShadeMap)), _MainTex_var, _Use_BaseAs1st);
                        float3 Set_1st_ShadeColor = lerp((_1st_ShadeColor.rgb*_1st_ShadeMap_var.rgb*_LightIntensity), ((_1st_ShadeColor.rgb*_1st_ShadeMap_var.rgb)*Set_LightColor), _Is_LightColor_1st_Shade);
                        //v.2.0.5
                        float4 _2nd_ShadeMap_var = lerp(tex2D(_2nd_ShadeMap, TRANSFORM_TEX(Set_UV0, _2nd_ShadeMap)), _1st_ShadeMap_var, _Use_1stAs2nd);
                        float3 Set_2nd_ShadeColor = lerp((_2nd_ShadeColor.rgb*_2nd_ShadeMap_var.rgb*_LightIntensity), ((_2nd_ShadeColor.rgb*_2nd_ShadeMap_var.rgb)*Set_LightColor), _Is_LightColor_2nd_Shade);
                        float _HalfLambert_var = 0.5*dot(lerp(i.normalDir, normalDirection, _Is_NormalMapToBase), lightDirection) + 0.5;

                        // float4 _Set_2nd_ShadePosition_var = tex2D(_Set_2nd_ShadePosition, TRANSFORM_TEX(Set_UV0, _Set_2nd_ShadePosition));
                        // float4 _Set_1st_ShadePosition_var = tex2D(_Set_1st_ShadePosition, TRANSFORM_TEX(Set_UV0, _Set_1st_ShadePosition));
                        // //v.2.0.5:
                        // float Set_FinalShadowMask = saturate((1.0 + ((lerp(_HalfLambert_var, (_HalfLambert_var*saturate(1.0 + _Tweak_SystemShadowsLevel)), _Set_SystemShadowsToBase) - (_1st_ShadeColor_Step - _1st_ShadeColor_Feather)) * ((1.0 - _Set_1st_ShadePosition_var.rgb).r - 1.0)) / (_1st_ShadeColor_Step - (_1st_ShadeColor_Step - _1st_ShadeColor_Feather))));
    //SGM
                    //float4 _ShadingGradeMap_var = tex2D(_ShadingGradeMap,TRANSFORM_TEX(Set_UV0, _ShadingGradeMap));
                    //v.2.0.6
                        float4 _ShadingGradeMap_var = tex2Dlod(_ShadingGradeMap, float4(TRANSFORM_TEX(Set_UV0, _ShadingGradeMap), 0.0, _BlurLevelSGM));
                        //v.2.0.6
                        //Minmimum value is same as the Minimum Feather's value with the Minimum Step's value as threshold.
                        //float _SystemShadowsLevel_var = (attenuation*0.5)+0.5+_Tweak_SystemShadowsLevel > 0.001 ? (attenuation*0.5)+0.5+_Tweak_SystemShadowsLevel : 0.0001;
                        float _ShadingGradeMapLevel_var = _ShadingGradeMap_var.r < 0.95 ? _ShadingGradeMap_var.r + _Tweak_ShadingGradeMapLevel : 1;

                        //float Set_ShadingGrade = saturate(_ShadingGradeMapLevel_var)*lerp( _HalfLambert_var, (_HalfLambert_var*saturate(_SystemShadowsLevel_var)), _Set_SystemShadowsToBase );

                        float Set_ShadingGrade = saturate(_ShadingGradeMapLevel_var)*lerp(_HalfLambert_var, (_HalfLambert_var*saturate(1.0 + _Tweak_SystemShadowsLevel)), _Set_SystemShadowsToBase);




                        //
                        float Set_FinalShadowMask = saturate((1.0 + ((Set_ShadingGrade - (_1st_ShadeColor_Step - _1st_ShadeColor_Feather)) * (0.0 - 1.0)) / (_1st_ShadeColor_Step - (_1st_ShadeColor_Step - _1st_ShadeColor_Feather))));
                        float Set_ShadeShadowMask = saturate((1.0 + ((Set_ShadingGrade - (_2nd_ShadeColor_Step - _2nd_ShadeColor_Feather)) * (0.0 - 1.0)) / (_2nd_ShadeColor_Step - (_2nd_ShadeColor_Step - _2nd_ShadeColor_Feather)))); // 1st and 2nd Shades Mask

        //SGM


                            //  //Composition: 3 Basic Colors as finalColor
                            //  float3 finalColor = 
                            // lerp(
                            //     Set_BaseColor, 
                            //     lerp(
                            //         Set_1st_ShadeColor,
                            //         Set_2nd_ShadeColor,
                            //         saturate(
                            //            (1.0 + ((_HalfLambert_var - (_2nd_ShadeColor_Step - _2nd_Shades_Feather)) * ((1.0 - _Set_2nd_ShadePosition_var.rgb).r - 1.0)) / (_2nd_ShadeColor_Step - (_2nd_ShadeColor_Step - _2nd_Shades_Feather))))
                            //            ),
                            //     Set_FinalShadowMask); // Final Color


                        //Composition: 3 Basic Colors as finalColor
                        float3 finalColor =
                            lerp(
                                Set_BaseColor,
                                //_BaseColor_var*(Set_LightColor*1.5),

                                lerp(
                                    Set_1st_ShadeColor,
                                    Set_2nd_ShadeColor,
                                    Set_ShadeShadowMask
                                ),
                                Set_FinalShadowMask);
                        //v.2.0.6: Add HighColor if _Is_Filter_HiCutPointLightColor is False
                        float4 _Set_HighColorMask_var = tex2D(_Set_HighColorMask, TRANSFORM_TEX(Set_UV0, _Set_HighColorMask));
                        float _Specular_var = 0.5*dot(halfDirection, lerp(i.normalDir, normalDirection, _Is_NormalMapToHighColor)) + 0.5; //  Specular                
                        float _TweakHighColorMask_var = (saturate((_Set_HighColorMask_var.g + _Tweak_HighColorMaskLevel))*lerp((1.0 - step(_Specular_var, (1.0 - pow(_HighColor_Power, 5)))), pow(_Specular_var, exp2(lerp(11, 1, _HighColor_Power))), _Is_SpecularToHighColor));
                        float4 _HighColor_Tex_var = tex2D(_HighColor_Tex, TRANSFORM_TEX(Set_UV0, _HighColor_Tex));
                        float3 _HighColor_var = (lerp((_HighColor_Tex_var.rgb*_HighColor.rgb), ((_HighColor_Tex_var.rgb*_HighColor.rgb)*Set_LightColor), _Is_LightColor_HighColor)*_TweakHighColorMask_var);

                        finalColor = finalColor + lerp(lerp(_HighColor_var, (_HighColor_var*((1.0 - Set_FinalShadowMask) + (Set_FinalShadowMask*_TweakHighColorOnShadow))), _Is_UseTweakHighColorOnShadow), float3(0, 0, 0), _Is_Filter_HiCutPointLightColor);
                        //

                        finalColor = saturate(finalColor);

                        pointLightColor += finalColor;
                        //	pointLightColor += lightColor;
                    }
                }
  #endif // determine main light inorder to apply light culling properly
  #endif // _ADDITIONAL_LIGHTS

                //
                //Final Composition

                finalColor =  saturate(finalColor) + (envLightColor*envLightIntensity*_GI_Intensity*smoothstep(1,0,envLightIntensity/2)) + emissive;


                finalColor += pointLightColor;



#endif


//v.2.0.4
#ifdef _IS_TRANSCLIPPING_OFF

                fixed4 finalRGBA = fixed4(finalColor,1);

#elif _IS_TRANSCLIPPING_ON
                float Set_Opacity = saturate((_Inverse_Clipping_var+_Tweak_transparency));

                fixed4 finalRGBA = fixed4(finalColor,Set_Opacity);

#endif


                return finalRGBA;


        }


#else //#if defined(_SHADINGGRADEMAP)





        float4 fragDoubleShadeFeather(VertexOutput i, fixed facing : VFACE) : SV_TARGET 
        {
            #  ifdef _MAIN_LIGHT_SHADOWS
                UtsLight mainLight = GetMainUtsLightByID(i.mainLightID, i.posWorld.xyz, i.shadowCoord);
            #  else
                UtsLight mainLight = GetMainUtsLightByID(i.mainLightID, i.posWorld.xyz, 0);
            #  endif

                half3 mainLightColor = GetLightColor(mainLight);

                i.normalDir = normalize(i.normalDir);
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);

                float3x3 tangentTransform = float3x3( i.tangentDir, i.bitangentDir, i.normalDir);

                float2 Set_UV0 = i.uv0;
                //v.2.0.6
                //float3 _NormalMap_var = UnpackNormal(tex2D(_NormalMap,TRANSFORM_TEX(Set_UV0, _NormalMap)));

                float3 _NormalMap_var = UnpackNormalScale(tex2D(_NormalMap, TRANSFORM_TEX(Set_UV0, _NormalMap)), _BumpScale);

                float3 normalLocal = _NormalMap_var.rgb;
                float3 normalDirection = normalize(mul( normalLocal, tangentTransform )); // Perturbed normals


                // todo. not necessary to calc gi factor in  shadowcaster pass.
                SurfaceData surfaceData;
                InitializeStandardLitSurfaceDataUTS(i.uv0, surfaceData);

                InputData inputData;
                Varyings  input;

                UNITY_SETUP_INSTANCE_ID(input);
                UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX(input);
                input.vertexSH = i.vertexSH;
                input.uv = i.uv0;
                input.fogFactorAndVertexLight = i.fogFactorAndVertexLight;
#  ifdef REQUIRES_VERTEX_SHADOW_COORD_INTERPOLATOR
                input.shadowCoord = i.shadowCoord;
#  endif

#  ifdef REQUIRES_WORLD_SPACE_POS_INTERPOLATOR
                input.positionWS = i.posWorld.xyz;
#  endif
#  ifdef _NORMALMAP
                input.normalWS = half4(i.normalDir, viewDirection.x);      // xyz: normal, w: viewDir.x
                input.tangentWS = half4(i.tangentDir, viewDirection.y);        // xyz: tangent, w: viewDir.y
                input.bitangentWS = half4(i.bitangentDir, viewDirection.z);    // xyz: bitangent, w: viewDir.z
#  else
                input.normalWS  = half3(i.normalDir);
                input.viewDirWS = half3(viewDirection);
#  endif
                InitializeInputData(input, surfaceData.normalTS, inputData);

                BRDFData brdfData;
                InitializeBRDFData(surfaceData.albedo,
                    surfaceData.metallic,
                    surfaceData.specular,
                    surfaceData.smoothness,
                    surfaceData.alpha, brdfData);

                half3 envColor = GlobalIlluminationUTS(brdfData, inputData.bakedGI, surfaceData.occlusion, inputData.normalWS, inputData.viewDirectionWS);
                envColor *= 1.8f;




                float4 _MainTex_var = tex2D(_MainTex,TRANSFORM_TEX(Set_UV0, _MainTex));
//v.2.0.4
#if defined(_IS_CLIPPING_MODE) 
//DoubleShadeWithFeather_Clipping
                float4 _ClippingMask_var = tex2D(_ClippingMask,TRANSFORM_TEX(Set_UV0, _ClippingMask));
                float Set_Clipping = saturate((lerp( _ClippingMask_var.r, (1.0 - _ClippingMask_var.r), _Inverse_Clipping )+_Clipping_Level));
                clip(Set_Clipping - 0.5);
#elif defined(_IS_CLIPPING_TRANSMODE) || defined(_IS_TRANSCLIPPING_ON)
//DoubleShadeWithFeather_TransClipping
                float4 _ClippingMask_var = tex2D(_ClippingMask,TRANSFORM_TEX(Set_UV0, _ClippingMask));
                float Set_MainTexAlpha = _MainTex_var.a;
                float _IsBaseMapAlphaAsClippingMask_var = lerp( _ClippingMask_var.r, Set_MainTexAlpha, _IsBaseMapAlphaAsClippingMask );
                float _Inverse_Clipping_var = lerp( _IsBaseMapAlphaAsClippingMask_var, (1.0 - _IsBaseMapAlphaAsClippingMask_var), _Inverse_Clipping );
                float Set_Clipping = saturate((_Inverse_Clipping_var+_Clipping_Level));
                clip(Set_Clipping - 0.5);

#elif defined(_IS_CLIPPING_OFF) || defined(_IS_TRANSCLIPPING_OFF)
//DoubleShadeWithFeather
#endif



                half shadowAttenuation = 1.0;
# ifdef _MAIN_LIGHT_SHADOWS
                shadowAttenuation = mainLight.shadowAttenuation;
# endif



//v.2.0.4
#ifdef _IS_PASS_FWDBASE

                float3 defaultLightDirection = normalize(UNITY_MATRIX_V[2].xyz + UNITY_MATRIX_V[1].xyz);
                //v.2.0.5
                float3 defaultLightColor = saturate(max(half3(0.05,0.05,0.05)*_Unlit_Intensity,max(ShadeSH9(half4(0.0, 0.0, 0.0, 1.0)),ShadeSH9(half4(0.0, -1.0, 0.0, 1.0)).rgb)*_Unlit_Intensity));
                float3 customLightDirection = normalize(mul( unity_ObjectToWorld, float4(((float3(1.0,0.0,0.0)*_Offset_X_Axis_BLD*10)+(float3(0.0,1.0,0.0)*_Offset_Y_Axis_BLD*10)+(float3(0.0,0.0,-1.0)*lerp(-1.0,1.0,_Inverse_Z_Axis_BLD))),0)).xyz);
                float3 lightDirection = normalize(lerp(defaultLightDirection, mainLight.direction.xyz,any(mainLight.direction.xyz)));
                lightDirection = lerp(lightDirection, customLightDirection, _Is_BLD);
                //v.2.0.5: 

                half3 originalLightColor = mainLightColor.rgb;

                float3 lightColor = lerp(max(defaultLightColor, originalLightColor), max(defaultLightColor, saturate(originalLightColor)), _Is_Filter_LightColor);



#endif
////// Lighting:
                float3 halfDirection = normalize(viewDirection+lightDirection);
                //v.2.0.5
                _Color = _BaseColor;

#ifdef _IS_PASS_FWDBASE
                float3 Set_LightColor = lightColor.rgb;
                float3 Set_BaseColor = lerp( (_BaseColor.rgb*_MainTex_var.rgb), ((_BaseColor.rgb*_MainTex_var.rgb)*Set_LightColor), _Is_LightColor_Base );
                //v.2.0.5
                float4 _1st_ShadeMap_var = lerp(tex2D(_1st_ShadeMap,TRANSFORM_TEX(Set_UV0, _1st_ShadeMap)),_MainTex_var,_Use_BaseAs1st);
                float3 Set_1st_ShadeColor = lerp( (_1st_ShadeColor.rgb*_1st_ShadeMap_var.rgb), ((_1st_ShadeColor.rgb*_1st_ShadeMap_var.rgb)*Set_LightColor), _Is_LightColor_1st_Shade );
                //v.2.0.5
                float4 _2nd_ShadeMap_var = lerp(tex2D(_2nd_ShadeMap,TRANSFORM_TEX(Set_UV0, _2nd_ShadeMap)),_1st_ShadeMap_var,_Use_1stAs2nd);
                float3 Set_2nd_ShadeColor = lerp( (_2nd_ShadeColor.rgb*_2nd_ShadeMap_var.rgb), ((_2nd_ShadeColor.rgb*_2nd_ShadeMap_var.rgb)*Set_LightColor), _Is_LightColor_2nd_Shade );
                float _HalfLambert_var = 0.5*dot(lerp( i.normalDir, normalDirection, _Is_NormalMapToBase ),lightDirection)+0.5;
                float4 _Set_2nd_ShadePosition_var = tex2D(_Set_2nd_ShadePosition,TRANSFORM_TEX(Set_UV0, _Set_2nd_ShadePosition));
                float4 _Set_1st_ShadePosition_var = tex2D(_Set_1st_ShadePosition,TRANSFORM_TEX(Set_UV0, _Set_1st_ShadePosition));
                //v.2.0.6
                //Minmimum value is same as the Minimum Feather's value with the Minimum Step's value as threshold.
                float _SystemShadowsLevel_var = (shadowAttenuation*0.5)+0.5+_Tweak_SystemShadowsLevel > 0.001 ? (shadowAttenuation*0.5)+0.5+_Tweak_SystemShadowsLevel : 0.0001;
                float Set_FinalShadowMask = saturate((1.0 + ( (lerp( _HalfLambert_var, _HalfLambert_var*saturate(_SystemShadowsLevel_var), _Set_SystemShadowsToBase ) - (_BaseColor_Step-_BaseShade_Feather)) * ((1.0 - _Set_1st_ShadePosition_var.rgb).r - 1.0) ) / (_BaseColor_Step - (_BaseColor_Step-_BaseShade_Feather))));
                //
                //Composition: 3 Basic Colors as Set_FinalBaseColor
                float3 Set_FinalBaseColor = lerp(Set_BaseColor,lerp(Set_1st_ShadeColor,Set_2nd_ShadeColor,saturate((1.0 + ( (_HalfLambert_var - (_ShadeColor_Step-_1st2nd_Shades_Feather)) * ((1.0 - _Set_2nd_ShadePosition_var.rgb).r - 1.0) ) / (_ShadeColor_Step - (_ShadeColor_Step-_1st2nd_Shades_Feather))))),Set_FinalShadowMask); // Final Color
                float4 _Set_HighColorMask_var = tex2D(_Set_HighColorMask,TRANSFORM_TEX(Set_UV0, _Set_HighColorMask));
                float _Specular_var = 0.5*dot(halfDirection,lerp( i.normalDir, normalDirection, _Is_NormalMapToHighColor ))+0.5; //  Specular                
                float _TweakHighColorMask_var = (saturate((_Set_HighColorMask_var.g+_Tweak_HighColorMaskLevel))*lerp( (1.0 - step(_Specular_var,(1.0 - pow(_HighColor_Power,5)))), pow(_Specular_var,exp2(lerp(11,1,_HighColor_Power))), _Is_SpecularToHighColor ));
                float4 _HighColor_Tex_var = tex2D(_HighColor_Tex,TRANSFORM_TEX(Set_UV0, _HighColor_Tex));
                float3 _HighColor_var = (lerp( (_HighColor_Tex_var.rgb*_HighColor.rgb), ((_HighColor_Tex_var.rgb*_HighColor.rgb)*Set_LightColor), _Is_LightColor_HighColor )*_TweakHighColorMask_var);
                //Composition: 3 Basic Colors and HighColor as Set_HighColor
                float3 Set_HighColor = (lerp( saturate((Set_FinalBaseColor-_TweakHighColorMask_var)), Set_FinalBaseColor, lerp(_Is_BlendAddToHiColor,1.0,_Is_SpecularToHighColor) )+lerp( _HighColor_var, (_HighColor_var*((1.0 - Set_FinalShadowMask)+(Set_FinalShadowMask*_TweakHighColorOnShadow))), _Is_UseTweakHighColorOnShadow ));
                float4 _Set_RimLightMask_var = tex2D(_Set_RimLightMask,TRANSFORM_TEX(Set_UV0, _Set_RimLightMask));
                float3 _Is_LightColor_RimLight_var = lerp( _RimLightColor.rgb, (_RimLightColor.rgb*Set_LightColor), _Is_LightColor_RimLight );
                float _RimArea_var = (1.0 - dot(lerp( i.normalDir, normalDirection, _Is_NormalMapToRimLight ),viewDirection));
                float _RimLightPower_var = pow(_RimArea_var,exp2(lerp(3,0,_RimLight_Power)));
                float _Rimlight_InsideMask_var = saturate(lerp( (0.0 + ( (_RimLightPower_var - _RimLight_InsideMask) * (1.0 - 0.0) ) / (1.0 - _RimLight_InsideMask)), step(_RimLight_InsideMask,_RimLightPower_var), _RimLight_FeatherOff ));
                float _VertHalfLambert_var = 0.5*dot(i.normalDir,lightDirection)+0.5;
                float3 _LightDirection_MaskOn_var = lerp( (_Is_LightColor_RimLight_var*_Rimlight_InsideMask_var), (_Is_LightColor_RimLight_var*saturate((_Rimlight_InsideMask_var-((1.0 - _VertHalfLambert_var)+_Tweak_LightDirection_MaskLevel)))), _LightDirection_MaskOn );
                float _ApRimLightPower_var = pow(_RimArea_var,exp2(lerp(3,0,_Ap_RimLight_Power)));
                float3 Set_RimLight = (saturate((_Set_RimLightMask_var.g+_Tweak_RimLightMaskLevel))*lerp( _LightDirection_MaskOn_var, (_LightDirection_MaskOn_var+(lerp( _Ap_RimLightColor.rgb, (_Ap_RimLightColor.rgb*Set_LightColor), _Is_LightColor_Ap_RimLight )*saturate((lerp( (0.0 + ( (_ApRimLightPower_var - _RimLight_InsideMask) * (1.0 - 0.0) ) / (1.0 - _RimLight_InsideMask)), step(_RimLight_InsideMask,_ApRimLightPower_var), _Ap_RimLight_FeatherOff )-(saturate(_VertHalfLambert_var)+_Tweak_LightDirection_MaskLevel))))), _Add_Antipodean_RimLight ));
                //Composition: HighColor and RimLight as _RimLight_var
                float3 _RimLight_var = lerp( Set_HighColor, (Set_HighColor+Set_RimLight), _RimLight );
                //Matcap
                //v.2.0.6 : CameraRolling Stabilizer
                //鏡スクリプト判定：_sign_Mirror = -1 なら、鏡の中と判定.
                //v.2.0.7
                fixed _sign_Mirror = i.mirrorFlag;
                //
                float3 _Camera_Right = UNITY_MATRIX_V[0].xyz;
                float3 _Camera_Front = UNITY_MATRIX_V[2].xyz;
                float3 _Up_Unit = float3(0, 1, 0);
                float3 _Right_Axis = cross(_Camera_Front, _Up_Unit);
                //鏡の中なら反転.
                if(_sign_Mirror < 0){
                    _Right_Axis = -1 * _Right_Axis;
                    _Rotate_MatCapUV = -1 * _Rotate_MatCapUV;
                }else{
                    _Right_Axis = _Right_Axis;
                }
                float _Camera_Right_Magnitude = sqrt(_Camera_Right.x*_Camera_Right.x + _Camera_Right.y*_Camera_Right.y + _Camera_Right.z*_Camera_Right.z);
                float _Right_Axis_Magnitude = sqrt(_Right_Axis.x*_Right_Axis.x + _Right_Axis.y*_Right_Axis.y + _Right_Axis.z*_Right_Axis.z);
                float _Camera_Roll_Cos = dot(_Right_Axis, _Camera_Right) / (_Right_Axis_Magnitude * _Camera_Right_Magnitude);
                float _Camera_Roll = acos(clamp(_Camera_Roll_Cos, -1, 1));
                fixed _Camera_Dir = _Camera_Right.y < 0 ? -1 : 1;
                float _Rot_MatCapUV_var_ang = (_Rotate_MatCapUV*3.141592654) - _Camera_Dir*_Camera_Roll*_CameraRolling_Stabilizer;
                //v.2.0.7
                float2 _Rot_MatCapNmUV_var = RotateUV(Set_UV0, (_Rotate_NormalMapForMatCapUV*3.141592654), float2(0.5, 0.5), 1.0);
                //V.2.0.6
                float3 _NormalMapForMatCap_var = UnpackNormalScale(tex2D(_NormalMapForMatCap, TRANSFORM_TEX(_Rot_MatCapNmUV_var, _NormalMapForMatCap)), _BumpScaleMatcap);
                //v.2.0.5: MatCap with camera skew correction
                float3 viewNormal = (mul(UNITY_MATRIX_V, float4(lerp( i.normalDir, mul( _NormalMapForMatCap_var.rgb, tangentTransform ).rgb, _Is_NormalMapForMatCap ),0))).rgb;
                float3 NormalBlend_MatcapUV_Detail = viewNormal.rgb * float3(-1,-1,1);
                float3 NormalBlend_MatcapUV_Base = (mul( UNITY_MATRIX_V, float4(viewDirection,0) ).rgb*float3(-1,-1,1)) + float3(0,0,1);
                float3 noSknewViewNormal = NormalBlend_MatcapUV_Base*dot(NormalBlend_MatcapUV_Base, NormalBlend_MatcapUV_Detail)/NormalBlend_MatcapUV_Base.b - NormalBlend_MatcapUV_Detail;                
                float2 _ViewNormalAsMatCapUV = (lerp(noSknewViewNormal,viewNormal,_Is_Ortho).rg*0.5)+0.5;
                //v.2.0.7
                float2 _Rot_MatCapUV_var = RotateUV((0.0 + ((_ViewNormalAsMatCapUV - (0.0+_Tweak_MatCapUV)) * (1.0 - 0.0) ) / ((1.0-_Tweak_MatCapUV) - (0.0+_Tweak_MatCapUV))), _Rot_MatCapUV_var_ang, float2(0.5, 0.5), 1.0);
                //鏡の中ならUV左右反転.
                if(_sign_Mirror < 0){
                    _Rot_MatCapUV_var.x = 1-_Rot_MatCapUV_var.x;
                }else{
                    _Rot_MatCapUV_var = _Rot_MatCapUV_var;
                }

                //v.2.0.6 : LOD of Matcap
                float4 _MatCap_Sampler_var = tex2Dlod(_MatCap_Sampler,float4(TRANSFORM_TEX(_Rot_MatCapUV_var, _MatCap_Sampler),0.0,_BlurLevelMatcap));
                //
                //MatcapMask
                float4 _Set_MatcapMask_var = tex2D(_Set_MatcapMask,TRANSFORM_TEX(Set_UV0, _Set_MatcapMask));
                float _Tweak_MatcapMaskLevel_var = saturate(lerp(_Set_MatcapMask_var.g, (1.0 - _Set_MatcapMask_var.g), _Inverse_MatcapMask) + _Tweak_MatcapMaskLevel);
                //
                float3 _Is_LightColor_MatCap_var = lerp( (_MatCap_Sampler_var.rgb*_MatCapColor.rgb), ((_MatCap_Sampler_var.rgb*_MatCapColor.rgb)*Set_LightColor), _Is_LightColor_MatCap );
                //v.2.0.6 : ShadowMask on Matcap in Blend mode : multiply
                float3 Set_MatCap = lerp( _Is_LightColor_MatCap_var, (_Is_LightColor_MatCap_var*((1.0 - Set_FinalShadowMask)+(Set_FinalShadowMask*_TweakMatCapOnShadow)) + lerp(Set_HighColor*Set_FinalShadowMask*(1.0-_TweakMatCapOnShadow), float3(0.0, 0.0, 0.0), _Is_BlendAddToMatCap)), _Is_UseTweakMatCapOnShadow );

                //
                //Composition: RimLight and MatCap as finalColor
                //Broke down finalColor composition
                float3 matCapColorOnAddMode = _RimLight_var+Set_MatCap*_Tweak_MatcapMaskLevel_var;
                float _Tweak_MatcapMaskLevel_var_MultiplyMode = _Tweak_MatcapMaskLevel_var * lerp (1.0, (1.0 - (Set_FinalShadowMask)*(1.0 - _TweakMatCapOnShadow)), _Is_UseTweakMatCapOnShadow);
                float3 matCapColorOnMultiplyMode = Set_HighColor*(1-_Tweak_MatcapMaskLevel_var_MultiplyMode) + Set_HighColor*Set_MatCap*_Tweak_MatcapMaskLevel_var_MultiplyMode + lerp(float3(0,0,0),Set_RimLight,_RimLight);
                float3 matCapColorFinal = lerp(matCapColorOnMultiplyMode, matCapColorOnAddMode, _Is_BlendAddToMatCap);
                float3 finalColor = lerp(_RimLight_var, matCapColorFinal, _MatCap);// Final Composition before Emissive
                //
                //v.2.0.6: GI_Intensity with Intensity Multiplier Filter

                float3 envLightColor = envColor.rgb;

                float envLightIntensity = 0.299*envLightColor.r + 0.587*envLightColor.g + 0.114*envLightColor.b <1 ? (0.299*envLightColor.r + 0.587*envLightColor.g + 0.114*envLightColor.b) : 1;


                float3 pointLightColor = 0;
  #ifdef _ADDITIONAL_LIGHTS

                int pixelLightCount = GetAdditionalLightsCount();

  #if 1  // determine main light inorder to apply light culling
                for (int iLight = -1; iLight < pixelLightCount; ++iLight)
                {
                    if (iLight != i.mainLightID)
                    {


                        float notDirectional = 1.0f; //_WorldSpaceLightPos0.w of the legacy code.

                        UtsLight additionalLight = GetMainUtsLight(0);
                        if (iLight != -1)
                        {
                            additionalLight = GetAdditionalUtsLight(iLight, inputData.positionWS);
                        }
                        half3 additionalLightColor = GetLightColor(additionalLight);
                        //					attenuation = light.distanceAttenuation; 


                        float3 lightDirection = additionalLight.direction;
                        //v.2.0.5: 
                        float3 addPassLightColor = (0.5*dot(lerp(i.normalDir, normalDirection, _Is_NormalMapToBase), lightDirection) + 0.5) * additionalLightColor.rgb;
                        float  pureIntencity = max(0.001, (0.299*additionalLightColor.r + 0.587*additionalLightColor.g + 0.114*additionalLightColor.b));
                        float3 lightColor = max(0, lerp(addPassLightColor, lerp(0, min(addPassLightColor, addPassLightColor / pureIntencity), notDirectional), _Is_Filter_LightColor));
                        float3 halfDirection = normalize(viewDirection + lightDirection); // has to be recalced here.

                        //v.2.0.5:
                        _BaseColor_Step = saturate(_BaseColor_Step + _StepOffset);
                        _ShadeColor_Step = saturate(_ShadeColor_Step + _StepOffset);
                        //
                        //v.2.0.5: If Added lights is directional, set 0 as _LightIntensity
                        float _LightIntensity = lerp(0, (0.299*additionalLightColor.r + 0.587*additionalLightColor.g + 0.114*additionalLightColor.b), notDirectional);
                        //v.2.0.5: Filtering the high intensity zone of PointLights
                        float3 Set_LightColor = lerp(lightColor, lerp(lightColor, min(lightColor, additionalLightColor.rgb*_BaseColor_Step), notDirectional), _Is_Filter_HiCutPointLightColor);
                        //
                        float3 Set_BaseColor = lerp((_BaseColor.rgb*_MainTex_var.rgb*_LightIntensity), ((_BaseColor.rgb*_MainTex_var.rgb)*Set_LightColor), _Is_LightColor_Base);
                        //v.2.0.5
                        float4 _1st_ShadeMap_var = lerp(tex2D(_1st_ShadeMap, TRANSFORM_TEX(Set_UV0, _1st_ShadeMap)), _MainTex_var, _Use_BaseAs1st);
                        float3 Set_1st_ShadeColor = lerp((_1st_ShadeColor.rgb*_1st_ShadeMap_var.rgb*_LightIntensity), ((_1st_ShadeColor.rgb*_1st_ShadeMap_var.rgb)*Set_LightColor), _Is_LightColor_1st_Shade);
                        //v.2.0.5
                        float4 _2nd_ShadeMap_var = lerp(tex2D(_2nd_ShadeMap, TRANSFORM_TEX(Set_UV0, _2nd_ShadeMap)), _1st_ShadeMap_var, _Use_1stAs2nd);
                        float3 Set_2nd_ShadeColor = lerp((_2nd_ShadeColor.rgb*_2nd_ShadeMap_var.rgb*_LightIntensity), ((_2nd_ShadeColor.rgb*_2nd_ShadeMap_var.rgb)*Set_LightColor), _Is_LightColor_2nd_Shade);
                        float _HalfLambert_var = 0.5*dot(lerp(i.normalDir, normalDirection, _Is_NormalMapToBase), lightDirection) + 0.5;
                        float4 _Set_2nd_ShadePosition_var = tex2D(_Set_2nd_ShadePosition, TRANSFORM_TEX(Set_UV0, _Set_2nd_ShadePosition));
                        float4 _Set_1st_ShadePosition_var = tex2D(_Set_1st_ShadePosition, TRANSFORM_TEX(Set_UV0, _Set_1st_ShadePosition));
                        //v.2.0.5:
                        float Set_FinalShadowMask = saturate((1.0 + ((lerp(_HalfLambert_var, (_HalfLambert_var*saturate(1.0 + _Tweak_SystemShadowsLevel)), _Set_SystemShadowsToBase) - (_BaseColor_Step - _BaseShade_Feather)) * ((1.0 - _Set_1st_ShadePosition_var.rgb).r - 1.0)) / (_BaseColor_Step - (_BaseColor_Step - _BaseShade_Feather))));
                        //Composition: 3 Basic Colors as finalColor
                        float3 finalColor = lerp(Set_BaseColor, lerp(Set_1st_ShadeColor, Set_2nd_ShadeColor, saturate((1.0 + ((_HalfLambert_var - (_ShadeColor_Step - _1st2nd_Shades_Feather)) * ((1.0 - _Set_2nd_ShadePosition_var.rgb).r - 1.0)) / (_ShadeColor_Step - (_ShadeColor_Step - _1st2nd_Shades_Feather))))), Set_FinalShadowMask); // Final Color

                        //v.2.0.6: Add HighColor if _Is_Filter_HiCutPointLightColor is False


                        float4 _Set_HighColorMask_var = tex2D(_Set_HighColorMask, TRANSFORM_TEX(Set_UV0, _Set_HighColorMask));
                        float _Specular_var = 0.5*dot(halfDirection, lerp(i.normalDir, normalDirection, _Is_NormalMapToHighColor)) + 0.5; //  Specular                
                        float _TweakHighColorMask_var = (saturate((_Set_HighColorMask_var.g + _Tweak_HighColorMaskLevel))*lerp((1.0 - step(_Specular_var, (1.0 - pow(_HighColor_Power, 5)))), pow(_Specular_var, exp2(lerp(11, 1, _HighColor_Power))), _Is_SpecularToHighColor));
                        float4 _HighColor_Tex_var = tex2D(_HighColor_Tex, TRANSFORM_TEX(Set_UV0, _HighColor_Tex));
                        float3 _HighColor_var = (lerp((_HighColor_Tex_var.rgb*_HighColor.rgb), ((_HighColor_Tex_var.rgb*_HighColor.rgb)*Set_LightColor), _Is_LightColor_HighColor)*_TweakHighColorMask_var);

                        finalColor = finalColor + lerp(lerp(_HighColor_var, (_HighColor_var*((1.0 - Set_FinalShadowMask) + (Set_FinalShadowMask*_TweakHighColorOnShadow))), _Is_UseTweakHighColorOnShadow), float3(0, 0, 0), _Is_Filter_HiCutPointLightColor);
                        //

                        finalColor = saturate(finalColor);

                        pointLightColor += finalColor;
                        //	pointLightColor += lightColor;
                    }
                }
  #endif // determine main light inorder to apply light culling
  #endif


//v.2.0.7
#ifdef _EMISSIVE_SIMPLE
                float4 _Emissive_Tex_var = tex2D(_Emissive_Tex,TRANSFORM_TEX(Set_UV0, _Emissive_Tex));
                float emissiveMask = _Emissive_Tex_var.a;
                emissive = _Emissive_Tex_var.rgb * _Emissive_Color.rgb * emissiveMask;
#elif _EMISSIVE_ANIMATION
                //v.2.0.7 Calculation View Coord UV for Scroll 
                float3 viewNormal_Emissive = (mul(UNITY_MATRIX_V, float4(i.normalDir,0))).xyz;
                float3 NormalBlend_Emissive_Detail = viewNormal_Emissive * float3(-1,-1,1);
                float3 NormalBlend_Emissive_Base = (mul( UNITY_MATRIX_V, float4(viewDirection,0)).xyz*float3(-1,-1,1)) + float3(0,0,1);
                float3 noSknewViewNormal_Emissive = NormalBlend_Emissive_Base*dot(NormalBlend_Emissive_Base, NormalBlend_Emissive_Detail)/NormalBlend_Emissive_Base.z - NormalBlend_Emissive_Detail;
                float2 _ViewNormalAsEmissiveUV = noSknewViewNormal_Emissive.xy*0.5+0.5;
                float2 _ViewCoord_UV = RotateUV(_ViewNormalAsEmissiveUV, -(_Camera_Dir*_Camera_Roll), float2(0.5,0.5), 1.0);
                //鏡の中ならUV左右反転.
                if(_sign_Mirror < 0){
                    _ViewCoord_UV.x = 1-_ViewCoord_UV.x;
                }else{
                    _ViewCoord_UV = _ViewCoord_UV;
                }
                float2 emissive_uv = lerp(i.uv0, _ViewCoord_UV, _Is_ViewCoord_Scroll);
                //
                float4 _time_var = _Time;
                float _base_Speed_var = (_time_var.g*_Base_Speed);
                float _Is_PingPong_Base_var = lerp(_base_Speed_var, sin(_base_Speed_var), _Is_PingPong_Base );
                float2 scrolledUV = emissive_uv - float2(_Scroll_EmissiveU, _Scroll_EmissiveV)*_Is_PingPong_Base_var;
                float rotateVelocity = _Rotate_EmissiveUV*3.141592654;
                float2 _rotate_EmissiveUV_var = RotateUV(scrolledUV, rotateVelocity, float2(0.5, 0.5), _Is_PingPong_Base_var);
                float4 _Emissive_Tex_var = tex2D(_Emissive_Tex,TRANSFORM_TEX(Set_UV0, _Emissive_Tex));
                float emissiveMask = _Emissive_Tex_var.a;
                _Emissive_Tex_var = tex2D(_Emissive_Tex,TRANSFORM_TEX(_rotate_EmissiveUV_var, _Emissive_Tex));
                float _colorShift_Speed_var = 1.0 - cos(_time_var.g*_ColorShift_Speed);
                float viewShift_var = smoothstep( 0.0, 1.0, max(0,dot(normalDirection,viewDirection)));
                float4 colorShift_Color = lerp(_Emissive_Color, lerp(_Emissive_Color, _ColorShift, _colorShift_Speed_var), _Is_ColorShift);
                float4 viewShift_Color = lerp(_ViewShift, colorShift_Color, viewShift_var);
                float4 emissive_Color = lerp(colorShift_Color, viewShift_Color, _Is_ViewShift);
                emissive = emissive_Color.rgb * _Emissive_Tex_var.rgb * emissiveMask;
#endif

                //Final Composition#if 
                finalColor =  saturate(finalColor) + (envLightColor*envLightIntensity*_GI_Intensity*smoothstep(1,0,envLightIntensity/2)) + emissive;


                finalColor += pointLightColor;
#endif


//v.2.0.4
#ifdef _IS_CLIPPING_OFF
//DoubleShadeWithFeather

                fixed4 finalRGBA = fixed4(finalColor,1);

#elif _IS_CLIPPING_MODE
//DoubleShadeWithFeather_Clipping

                fixed4 finalRGBA = fixed4(finalColor,1);

#elif _IS_CLIPPING_TRANSMODE
//DoubleShadeWithFeather_TransClipping
                float Set_Opacity = saturate((_Inverse_Clipping_var+_Tweak_transparency));
                fixed4 finalRGBA = fixed4(finalColor,Set_Opacity);

#endif

                return finalRGBA;
            }
#endif //#if defined(_SHADINGGRADEMAP)

            float4 frag(VertexOutput i, fixed facing : VFACE) : SV_TARGET
            {
#if defined(_SHADINGGRADEMAP)
                    return fragShadingGradeMap(i, facing);
#else
                    return fragDoubleShadeFeather(i, facing);
#endif
            }

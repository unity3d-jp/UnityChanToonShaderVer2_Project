//Unity Toon Shader/HDRP
//nobuyuki@unity3d.com
//toshiyuki@unity3d.com (Universal RP/HDRP) 

            uniform float4 _LightColor0;
//            uniform float4 _BaseColor;
            //v.2.0.7.5
            uniform float _Unlit_Intensity;
            uniform fixed _Is_Filter_LightColor;
            uniform fixed _Is_LightColor_Outline;
            //v.2.0.5
            uniform float4 _Color;
            uniform sampler2D _MainTex; uniform float4 _MainTex_ST;
            uniform float _Outline_Width;
            uniform float _Farthest_Distance;
            uniform float _Nearest_Distance;
            uniform sampler2D _Outline_Sampler; uniform float4 _Outline_Sampler_ST;
            uniform float4 _Outline_Color;
            uniform fixed _Is_BlendBaseColor;
            uniform float _Offset_Z;
            //v2.0.4
            uniform sampler2D _OutlineTex; uniform float4 _OutlineTex_ST;
            uniform fixed _Is_OutlineTex;
            //Baked Normal Texture for Outline
            uniform sampler2D _BakedNormal; uniform float4 _BakedNormal_ST;
            uniform fixed _Is_BakedNormal;

            uniform float _ZOverDrawMode;
            //

            // masking
            uniform float _OutlineVisible;
            uniform float _OutlineOverridden;
            uniform float4 _OutlineMaskColor;
            uniform float _ComposerMaskMode;
            uniform int _ClippingMaskMode;
            uniform int _ClippingMatteMode;

//v.2.0.4
#ifdef _IS_OUTLINE_CLIPPING_YES
            uniform sampler2D _ClippingMask; uniform float4 _ClippingMask_ST;
            uniform float _Clipping_Level;
            uniform fixed _Inverse_Clipping;
            uniform fixed _IsBaseMapAlphaAsClippingMask;
#endif



#undef unity_ObjectToWorld 
#undef unity_WorldToObject 



#ifdef _WRITE_TRANSPARENT_MOTION_VECTOR
#include "Packages/com.unity.render-pipelines.high-definition/Runtime/RenderPipeline/ShaderPass/MotionVectorVertexShaderCommon.hlsl"

            // PackedVaryingsType
            // https://github.com/Unity-Technologies/Graphics/blob/e4117c07b479adafed38237f3407a363eefb4590/com.unity.render-pipelines.high-definition/Runtime/RenderPipeline/ShaderPass/VertMesh.hlsl#L120

            PackedVaryingsType Vert(AttributesMesh inputMesh, AttributesPass inputPass)
            {
                // VaryingsType
                // https://github.com/Unity-Technologies/Graphics/blob/e4117c07b479adafed38237f3407a363eefb4590/com.unity.render-pipelines.high-definition/Runtime/RenderPipeline/ShaderPass/VertMesh.hlsl#L118

                VaryingsType varyingsType;
                varyingsType.vmesh = VertMesh(inputMesh);
                #include "HDRPToonOutlineVertMain.hlsl"

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
                #include "HDRPToonOutlineVertMain.hlsl"

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



void Frag(PackedVaryingsToPS packedInput,
#ifdef OUTPUT_SPLIT_LIGHTING
            out float4 outColor : SV_Target0,  // outSpecularLighting
#ifdef UNITY_VIRTUAL_TEXTURING
            out float4 outVTFeedback : VT_BUFFER_TARGET,
#endif
            out float4 outDiffuseLighting : EXTRA_BUFFER_TARGET,
            OUTPUT_SSSBUFFER(outSSSBuffer)
#else
            out float4 outColor : SV_Target0
#ifdef UNITY_VIRTUAL_TEXTURING
            , out float4 outVTFeedback : VT_BUFFER_TARGET
#endif
#ifdef _WRITE_TRANSPARENT_MOTION_VECTOR
            , out float4 outMotionVec : EXTRA_BUFFER_TARGET
#endif // _WRITE_TRANSPARENT_MOTION_VECTOR
#endif // OUTPUT_SPLIT_LIGHTING
#ifdef _DEPTHOFFSET_ON
            , out float outputDepth : SV_Depth
#endif
            )
{
    UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX(packedInput);
    FragInputs input = UnpackVaryingsMeshToFragInputs(packedInput.vmesh);
#ifdef _IS_CLIPPING_MASK
    if (_ClippingMaskMode != 0)
    {
        discard;
    }
#endif
#ifdef _IS_CLIPPING_MATTE
    if (_ClippingMatteMode != 0)
    {
        discard;
    }
#endif // _IS_CLIPPING_MATTE


    //v.2.0.5
    if (_ZOverDrawMode > 0.99f)
    {
#ifdef _DEPTHOFFSET_ON
        outputDepth = posInput.deviceDepth;
#endif
#ifdef UNITY_VIRTUAL_TEXTURING

        outVTFeedback = builtinData.vtPackedFeedback;
#endif
        outColor = float4(1.0f, 1.0f, 1.0f, 1.0f);  // but nothing should be drawn except Z value as colormask is set to 0
        return;
    }
    _Color = _BaseColor;
	float4 objPos = mul(unity_ObjectToWorld, float4(0, 0, 0, 1));
		//v.2.0.7.5
	float4 unity_AmbientSky = float4(0.1, 0.1, 0.1, 1.0f); //Todo.
    half3 ambientSkyColor = unity_AmbientSky.rgb>0.05 ? unity_AmbientSky.rgb*_Unlit_Intensity : half3(0.05,0.05,0.05)*_Unlit_Intensity;
    float3 lightColor = _LightColor0.rgb >0.05 ? _LightColor0.rgb : ambientSkyColor.rgb;
    float lightColorIntensity = (0.299*lightColor.r + 0.587*lightColor.g + 0.114*lightColor.b);
    lightColor = lightColorIntensity<1 ? lightColor : lightColor/lightColorIntensity;
    lightColor = lerp(half3(1.0,1.0,1.0), lightColor, _Is_LightColor_Outline);

    float2 Set_UV0 = input.texCoord0.xy;

    float4 _MainTex_var = tex2D(_MainTex,TRANSFORM_TEX(Set_UV0, _MainTex));
    float3 Set_BaseColor = _BaseColor.rgb*_MainTex_var.rgb;
    float3 _Is_BlendBaseColor_var = lerp( _Outline_Color.rgb*lightColor, (_Outline_Color.rgb*Set_BaseColor*Set_BaseColor*lightColor), _Is_BlendBaseColor );
    //
    float3 _OutlineTex_var = tex2D(_OutlineTex,TRANSFORM_TEX(Set_UV0, _OutlineTex)).xyz;

    float4 overridingColor = lerp(_OutlineMaskColor, float4(_OutlineMaskColor.w, _OutlineMaskColor.w, _OutlineMaskColor.w, 1.0f), _ComposerMaskMode);
    float  maskEnabled = max(_OutlineOverridden, _ComposerMaskMode);

//v.2.0.7.5
#ifdef _IS_OUTLINE_CLIPPING_NO
    float3 Set_Outline_Color = lerp(_Is_BlendBaseColor_var, _OutlineTex_var.rgb*_Outline_Color.rgb*lightColor, _Is_OutlineTex );
    if (_OutlineVisible < 0.1)
    {
        // Todo. 
        // without this, something is drawn even if _OutlineVisible = 0, in AngelRing(HDRP)
        discard; 
    }
    Set_Outline_Color = lerp(Set_Outline_Color, overridingColor.xyz, maskEnabled);
    outColor =float4(Set_Outline_Color, _OutlineVisible );


#elif _IS_OUTLINE_CLIPPING_YES
    float4 _ClippingMask_var = tex2D(_ClippingMask,TRANSFORM_TEX(Set_UV0, _ClippingMask));
    float Set_MainTexAlpha = _MainTex_var.a;
    float _IsBaseMapAlphaAsClippingMask_var = lerp( _ClippingMask_var.r, Set_MainTexAlpha, _IsBaseMapAlphaAsClippingMask );
    float _Inverse_Clipping_var = lerp( _IsBaseMapAlphaAsClippingMask_var, (1.0 - _IsBaseMapAlphaAsClippingMask_var), _Inverse_Clipping );
    float Set_Clipping = saturate((_Inverse_Clipping_var+_Clipping_Level));
    clip(Set_Clipping - 0.5);
    float4 Set_Outline_Color = lerp( float4(_Is_BlendBaseColor_var,Set_Clipping), float4((_OutlineTex_var.rgb*_Outline_Color.rgb*lightColor),Set_Clipping), _Is_OutlineTex );
    Set_Outline_Color = lerp(Set_Outline_Color, overridingColor, maskEnabled);
    Set_Outline_Color.w *= _OutlineVisible;
    outColor = Set_Outline_Color;
#endif


#ifdef _DEPTHOFFSET_ON
    outputDepth = posInput.deviceDepth;
#endif
#ifdef UNITY_VIRTUAL_TEXTURING
    outVTFeedback = builtinData.vtPackedFeedback;
#endif
}
// End of File


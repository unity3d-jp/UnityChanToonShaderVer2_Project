//#define USE_SIMPLE_UI


using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEditor.Rendering;
using System;

namespace UnityEditor.Rendering.Toon
{
    internal partial class UTS3GUI : UnityEditor.ShaderGUI
    {


        protected const float kVersionX = 0.0f;
        protected const float kVersionY = 7.0f;
        protected const float kVersionZ = 0.0f;

        // Render Pipelines UTS supports are the followings 
        internal enum RenderPipeline
        {

            Legacy,
            Universal,
            HDRP,
            Unknown,
        }

        internal static RenderPipeline currentRenderPipeline
        {
            get
            {
                const string kHdrpShaderPrefix = "HDRP/";
                const string kUrpShaderPrefix = "Universal Render Pipeline/";
                var currentRenderPipeline = GraphicsSettings.currentRenderPipeline;
                if (currentRenderPipeline == null)
                {
                    return RenderPipeline.Legacy; 
                }
                if (currentRenderPipeline.defaultMaterial.shader.name.StartsWith(kHdrpShaderPrefix))
                {
                    return RenderPipeline.HDRP;
                }
                if (currentRenderPipeline.defaultMaterial.shader.name.StartsWith(kUrpShaderPrefix))
                {
                    return RenderPipeline.Universal;
                }
                return RenderPipeline.Unknown;
            }
        }
        internal static string srpDefaultLightModeName 
        {
             get
             {
                const string legacyDefaultLightModeName = "Always";
                const string srpDefaultLightModeName = "SRPDefaultUnlit";

                if (currentRenderPipeline == RenderPipeline.Legacy)
                {
                    return legacyDefaultLightModeName; // default.
                }

                return srpDefaultLightModeName;
             }
        }



        internal  void RenderingPerChennelsSetting(Material material) 
        {
            if (currentRenderPipeline == RenderPipeline.HDRP)
            {

                RenderingPerChennelsSettingHDRP(material);
            }
        }
        internal  void ApplyTessellation(Material materal) 
        {
            if (currentRenderPipeline == RenderPipeline.HDRP)
            {
                ApplyTessellationHDRP(materal);
            }
        }
        internal  void ApplyRenderingPerChennelsSetting(Material material) 
        {

        }
        internal  void FindTessellationProperties(MaterialProperty[] props) 
        {
            if (currentRenderPipeline == RenderPipeline.HDRP)
            {

                FindTessellationPropertiesHDRP(props);
            }
        }

        internal const string ShaderDefineSHADINGGRADEMAP = "_SHADINGGRADEMAP";
        internal const string ShaderDefineANGELRING_ON = "_IS_ANGELRING_ON";
        internal const string ShaderDefineANGELRING_OFF = "_IS_ANGELRING_OFF";
        internal const string ShaderDefineUTS_USE_RAYTRACING_SHADOW = "UTS_USE_RAYTRACING_SHADOW";
        internal const string ShaderPropAngelRing = "_AngelRing";
        internal const string ShaderPropRTHS = "_RTHS";
        internal const string ShaderPropMatCap = "_MatCap";
        internal const string ShaderPropMainTex = "_MainTex";
        internal const string ShaderPropClippingMode = "_ClippingMode";
        internal const string ShaderPropClippingMask = "_ClippingMask";
        internal const string ShaderProp_Set_1st_ShadePosition = "_Set_1st_ShadePosition";
        internal const string ShaderProp_Set_2nd_ShadePosition = "_Set_2nd_ShadePosition";
        internal const string ShaderProp_ShadingGradeMap = "_ShadingGradeMap";
        internal const string ShaderProp_Set_RimLightMask = "_Set_RimLightMask";
        internal const string ShaderProp_HighColor_Tex = "_HighColor_Tex";
        internal const string ShaderProp_Set_HighColorMask = "_Set_HighColorMask";
        internal const string ShaderProp_MatCap_Sampler = "_MatCap_Sampler";
        internal const string ShaderProp_Set_MatcapMask = "_Set_MatcapMask";
        internal const string ShaderProp_OutlineTex = "_OutlineTex";
        internal const string ShaderProp_Outline_Sampler = "_Outline_Sampler";

        internal const string ShaderPropSimpleUI = "_simpleUI";
        internal const string ShaderPropUtsTechniqe = "_utsTechnique";
        internal const string ShaderPropAutoRenderQueue = "_AutoRenderQueue";
        internal const string ShaderPropStencilMode = "_StencilMode";
        internal const string ShaderPropStencilNo = "_StencilNo";
        internal const string ShaderPropTransparentEnabled = "_TransparentEnabled";
        internal const string ShaderPropStencilComp = "_StencilComp";
        internal const string ShaderPropStencilOpPass = "_StencilOpPass";
        internal const string ShaderPropStencilOpFail = "_StencilOpFail";
        internal const string ShaderPropStencilWriteMask = "_StencilWriteMask";
        internal const string ShaderPropStencilReadMask = "_StencilReadMask";
        internal const string ShaderPropUtsVersionX = "_utsVersionX";
        internal const string ShaderPropUtsVersionY = "_utsVersionY";
        internal const string ShaderPropUtsVersionZ = "_utsVersionZ";
        internal const string ShaderPropIsUnityToonShader = "_isUnityToonshader";
        internal const string ShaderPropOutline = "_OUTLINE";
        internal const string ShaderPropNormalMapToHighColor = "_Is_NormalMapToHighColor";
        internal const string ShaderPropIsNormalMapToRimLight = "_Is_NormalMapToRimLight";
        internal const string ShaderPropSetSystemShadowsToBase = "_Set_SystemShadowsToBase";
        internal const string ShaderPropIsFilterHiCutPointLightColor = "_Is_Filter_HiCutPointLightColor";
        internal const string ShaderPropInverseClipping = "_Inverse_Clipping";
        internal const string ShaderPropIsBaseMapAlphaAsClippingMask = "_IsBaseMapAlphaAsClippingMask";
        internal const string ShaderPropIsLightColor_Base = "_Is_LightColor_Base";
        internal const string ShaderPropCameraRolling_Stabilizer = "_CameraRolling_Stabilizer";
        internal const string ShaderPropIs_Ortho = "_Is_Ortho";
        internal const string ShaderPropGI_Intensity = "_GI_Intensity";
        internal const string ShaderPropUnlit_Intensity = "_Unlit_Intensity";
        internal const string ShaderPropIs_Filter_LightColor = "_Is_Filter_LightColor";
        internal const string ShaderPropIs_LightColor_1st_Shade = "_Is_LightColor_1st_Shade";
        internal const string ShaderPropIs_LightColor_2nd_Shade = "_Is_LightColor_2nd_Shade";
        internal const string ShaderPropIs_LightColor_HighColor = "_Is_LightColor_HighColor";
        internal const string ShaderPropIs_LightColor_RimLight = "_Is_LightColor_RimLight";
        internal const string ShaderPropIs_LightColor_Ap_RimLight = "_Is_LightColor_Ap_RimLight";
        internal const string ShaderPropIs_LightColor_MatCap = "_Is_LightColor_MatCap";
        internal const string ShaderPropIs_LightColor_AR = "_Is_LightColor_AR";
        internal const string ShaderPropIs_LightColor_Outline = "_Is_LightColor_Outline";
        internal const string ShaderPropInverse_MatcapMask = "_Inverse_MatcapMask";
        internal const string ShaderPropUse_BaseAs1st = "_Use_BaseAs1st";
        internal const string ShaderPropUse_1stAs2nd = "_Use_1stAs2nd";
        internal const string ShaderPropIs_NormalMapToBase = "_Is_NormalMapToBase";
        internal const string ShaderPropIs_ColorShift = "_Is_ColorShift";
        internal const string ShaderPropRimLight = "_RimLight";
        internal const string ShaderPropRimLight_FeatherOff = "_RimLight_FeatherOff";
        internal const string ShaderPropAp_RimLight_FeatherOff = "_Ap_RimLight_FeatherOff";
        internal const string ShaderPropIs_BlendAddToMatCap = "_Is_BlendAddToMatCap";
        internal const string ShaderPropARSampler_AlphaOn = "_ARSampler_AlphaOn";
        internal const string ShaderPropIs_UseTweakHighColorOnShadow = "_Is_UseTweakHighColorOnShadow";

        internal const string ShaderPropIs_SpecularToHighColor = "_Is_SpecularToHighColor";
        internal const string ShaderPropIs_BlendAddToHiColor = "_Is_BlendAddToHiColor";

        internal const string ShaderPropAdd_Antipodean_RimLight = "_Add_Antipodean_RimLight";
        internal const string ShaderPropLightDirection_MaskOn = "_LightDirection_MaskOn";

        internal const string ShaderProp1st_ShadeColor_Step = "_1st_ShadeColor_Step";
        internal const string ShaderPropBaseColor_Step = "_BaseColor_Step";
        internal const string ShaderProp1st_ShadeColor_Feather = "_1st_ShadeColor_Feather";
        internal const string ShaderPropBaseShade_Feather = "_BaseShade_Feather";
        internal const string ShaderProp2nd_ShadeColor_Step = "_2nd_ShadeColor_Step";
        internal const string ShaderPropShadeColor_Step = "_ShadeColor_Step";
        internal const string ShaderProp2nd_ShadeColor_Feather = "_2nd_ShadeColor_Feather";
        internal const string ShaderProp1st2nd_Shades_Feather = "_1st2nd_Shades_Feather";
        internal const string ShaderPropIs_NormalMapForMatCap = "_Is_NormalMapForMatCap";
        internal const string ShaderPropIs_UseTweakMatCapOnShadow = "_Is_UseTweakMatCapOnShadow";
        internal const string ShaderPropIs_ViewCoord_Scroll = "_Is_ViewCoord_Scroll";
        internal const string ShaderPropIs_PingPong_Base = "_Is_PingPong_Base";

        internal const string ShaderPropIs_ViewShift = "_Is_ViewShift";
        internal const string ShaderPropIs_BlendBaseColor = "_Is_BlendBaseColor";
        internal const string ShaderPropIs_OutlineTex = "_Is_OutlineTex";
        internal const string ShaderPropIs_BakedNormal = "_Is_BakedNormal";
        internal const string ShaderPropIs_BLD = "_Is_BLD";
        internal const string ShaderPropInverse_Z_Axis_BLD = "_Inverse_Z_Axis_BLD";



        

        internal const string ShaderDefineIS_OUTLINE_CLIPPING_NO = "_IS_OUTLINE_CLIPPING_NO";
        internal const string ShaderDefineIS_OUTLINE_CLIPPING_YES = "_IS_OUTLINE_CLIPPING_YES";

        internal const string ShaderDefineIS_CLIPPING_OFF = "_IS_CLIPPING_OFF";
        internal const string ShaderDefineIS_CLIPPING_MODE = "_IS_CLIPPING_MODE";
        internal const string ShaderDefineIS_CLIPPING_TRANSMODE = "_IS_CLIPPING_TRANSMODE";

        internal const string ShaderDefineIS_TRANSCLIPPING_OFF = "_IS_TRANSCLIPPING_OFF";
        internal const string ShaderDefineIS_TRANSCLIPPING_ON = "_IS_TRANSCLIPPING_ON";

        internal const string ShaderDefineIS_CLIPPING_MATTE = "_IS_CLIPPING_MATTE";


        protected readonly string[] UtsModeNames = { "Standard", "With Additional Control Maps" };
        protected readonly string[] EmissiveScrollMode = { "UV Coordinate Scroll", "View Coordinate Scroll" };
        protected readonly string[] ClippingModeNames = { "Off", "On", "Clip Transparency" };
        protected readonly string[] StencilModeNames = { "Off", "Draw If Not Equal to", "Replace Stencil Buffer with" };
        public enum UTS_Mode : uint
        {
            ThreeColorToon, ShadingGradeMap
        }

        public enum UTS_ClippingMode
        {
            Off, On, TransClippingMode
        }

        public enum UTS_TransClippingMode
        {
            Off, On,
        }
        public enum UTS_TransparentMode : uint
        {
            Off, On,
        }
        public enum UTS_StencilMode
        {
            Off, StencilOut, StencilMask
        }
        public enum UTS_SpeculerMode
        {
            Solid, Natural
        }
        public enum UTS_SpeculerColorBlendMode
        {
            Multiply, Additive
        }
        public enum UTS_MatcapColorBlendMode
        {
            Multiply, Additive
        }

        public enum StencilOperation
        {
            //https://docs.unity3d.com/Manual/SL-Stencil.html
            Keep, //    Keep the current contents of the buffer.
            Zero, //    Write 0 into the buffer.
            Replace, // Write the reference value into the buffer.
            IncrSat, // Increment the current value in the buffer. If the value is 255 already, it stays at 255.
            DecrSat, // Decrement the current value in the buffer. If the value is 0 already, it stays at 0.
            Invert, //  Negate all the bits.
            IncrWrap, //    Increment the current value in the buffer. If the value is 255 already, it becomes 0.
            DecrWrap, //    Decrement the current value in the buffer. If the value is 0 already, it becomes 255.
        }

        public enum StencilCompFunction
        {

            Disabled,//    Depth or stencil test is disabled.
            Never,   //   Never pass depth or stencil test.
            Less,   //   Pass depth or stencil test when new value is less than old one.
            Equal,  //  Pass depth or stencil test when values are equal.
            LessEqual, // Pass depth or stencil test when new value is less or equal than old one.
            Greater, // Pass depth or stencil test when new value is greater than old one.
            NotEqual, //    Pass depth or stencil test when values are different.
            GreaterEqual, // Pass depth or stencil test when new value is greater or equal than old one.
            Always,//  Always pass depth or stencil test.
        }



        public enum OutlineMode
        {
            NormalDirection, PositionScaling
        }

        public enum CullingMode
        {
            CullingOff, FrontCulling, BackCulling
        }

        public enum EmissionMode
        {
            SimpleEmissive, EmissiveAnimation
        }

        public enum CameraProjectionType
        {
            Perspective,
            Orthographic
        }

        [Flags]
#if UNITY_2021_1_OR_NEWER
        [UTS3HelpURL("instruction")]
#endif // UNITY_2021_1_OR_NEWER
        protected enum Expandable
        {
            Shader = 1 << 0,
            BasicColor = 1 << 1,
            BasicLookDevs = 1 << 2,
            HighLight = 1 << 3,
            RimLight = 1 << 4,
            MatCap = 1 << 5,
            AngelRing = 1 << 6,
            Emission = 1 << 7,
            Outline = 1 << 8,
            Tessellation = 1 << 9,
            LightColorEffectiveness = 1 << 10,
            EnvironmentalLightEffectiveness = 1 << 11,
            MetaverseSettings = 1 << 12,
        }

        // variables which must be gotten from shader at the beggning of GUI
        internal int m_autoRenderQueue = 1;
        internal int m_renderQueue = (int)UnityEngine.Rendering.RenderQueue.Geometry;
        // variables which just to be held.
        internal OutlineMode m_outlineMode;
        internal CullingMode m_cullingMode;
        internal EmissionMode m_emissionMode;
        internal bool m_FirstTimeApply = true;
        UTS_Mode m_WorkflowMode;



        readonly UTS3MaterialHeaderScopeList m_MaterialScopeList = new UTS3MaterialHeaderScopeList(uint.MaxValue & ~(uint)Expandable.BasicColor);


        //Button sizes
        static internal GUILayoutOption[] longButtonStyle = new GUILayoutOption[] { GUILayout.Width(180) };
        static internal GUILayoutOption[] shortButtonStyle = new GUILayoutOption[] { GUILayout.Width(130) };
        static internal GUILayoutOption[] middleButtonStyle = new GUILayoutOption[] { GUILayout.Width(130) };
        static internal GUILayoutOption[] toggleStyle = new GUILayoutOption[] { GUILayout.Width(130) };

        //
        protected UTS_TransparentMode transparencyEnabled;
        protected int stencilNumberSetting;

        protected static bool _SimpleUI = false;

        //Initial values of Foldout.


        protected  bool _NormalMap_Foldout = false;
        protected  bool _ShadowControlMaps_Foldout = false;
        protected  bool _AdditionalLookdevs_Foldout = false;
        protected  bool _AdvancedOutline_Foldout = false;



        // -----------------------------------------------------
        //Specify only those that use the m_MaterialEditor method as their UI.
        // UTS3 materal properties -------------------------
        protected MaterialProperty utsTechnique = null;
        protected MaterialProperty specularMode = null;
        protected MaterialProperty specularBlendMode = null;
        protected MaterialProperty matcapCameraMode = null;
        protected MaterialProperty matcapBlendMode = null;
        protected MaterialProperty matcapOrtho = null;
        protected MaterialProperty transparentMode = null;
        protected MaterialProperty clippingMode = null;
        protected MaterialProperty clippingMask = null;
        protected MaterialProperty clipping_Level = null;
        protected MaterialProperty stencilValue = null;
        protected MaterialProperty tweak_transparency = null;
        protected MaterialProperty stencilMode = null;
        protected MaterialProperty mainTex = null;
        protected MaterialProperty baseColor = null;
        protected MaterialProperty firstShadeMap = null;
        protected MaterialProperty firstShadeColor = null;
        protected MaterialProperty secondShadeMap = null;
        protected MaterialProperty secondShadeColor = null;
        protected MaterialProperty normalMap = null;
        protected MaterialProperty bumpScale = null;
        protected MaterialProperty set_1st_ShadePosition = null;
        protected MaterialProperty set_2nd_ShadePosition = null;
        protected MaterialProperty shadingGradeMap = null;
        protected MaterialProperty tweak_ShadingGradeMapLevel = null;
        protected MaterialProperty blurLevelSGM = null;
        protected MaterialProperty tweak_SystemShadowsLevel = null;
        protected MaterialProperty baseColor_Step = null;
        protected MaterialProperty baseShade_Feather = null;
        protected MaterialProperty shadeColor_Step = null;
        protected MaterialProperty first2nd_Shades_Feather = null;
        protected MaterialProperty first_ShadeColor_Step = null;
        protected MaterialProperty first_ShadeColor_Feather = null;
        protected MaterialProperty second_ShadeColor_Step = null;
        protected MaterialProperty second_ShadeColor_Feather = null;
        protected MaterialProperty stepOffset = null;
        protected MaterialProperty highColor_Tex = null;
        protected MaterialProperty highColor = null;
        protected MaterialProperty highColor_Power = null;
        protected MaterialProperty tweakHighColorOnShadow = null;
        protected MaterialProperty set_HighColorMask = null;
        protected MaterialProperty tweak_HighColorMaskLevel = null;
        protected MaterialProperty rimLightColor = null;
        protected MaterialProperty rimLight_Power = null;
        protected MaterialProperty rimLight_InsideMask = null;
        protected MaterialProperty tweak_LightDirection_MaskLevel = null;
        protected MaterialProperty ap_RimLightColor = null;
        protected MaterialProperty ap_RimLight_Power = null;
        protected MaterialProperty set_RimLightMask = null;
        protected MaterialProperty tweak_RimLightMaskLevel = null;
        protected MaterialProperty matCap_Sampler = null;
        protected MaterialProperty matCapColor = null;
        protected MaterialProperty blurLevelMatcap = null;
        protected MaterialProperty tweak_MatCapUV = null;
        protected MaterialProperty rotate_MatCapUV = null;
        protected MaterialProperty normalMapForMatCap = null;
        protected MaterialProperty bumpScaleMatcap = null;
        protected MaterialProperty rotate_NormalMapForMatCapUV = null;
        protected MaterialProperty tweakMatCapOnShadow = null;
        protected MaterialProperty set_MatcapMask = null;
        protected MaterialProperty tweak_MatcapMaskLevel = null;
        protected MaterialProperty angelRing_Sampler = null;
        protected MaterialProperty angelRing_Color = null;
        protected MaterialProperty ar_OffsetU = null;
        protected MaterialProperty ar_OffsetV = null;
        protected MaterialProperty emissive_Tex = null;
        protected MaterialProperty emissive_Color = null;
        protected MaterialProperty base_Speed = null;
        protected MaterialProperty scroll_EmissiveU = null;
        protected MaterialProperty scroll_EmissiveV = null;
        protected MaterialProperty rotate_EmissiveUV = null;
        protected MaterialProperty colorShift = null;
        protected MaterialProperty colorShift_Speed = null;
        protected MaterialProperty viewShift = null;
        protected MaterialProperty outline_Width = null;
        protected MaterialProperty outline_Color = null;
        protected MaterialProperty outline_Sampler = null;
        protected MaterialProperty offset_Z = null;
        protected MaterialProperty farthest_Distance = null;
        protected MaterialProperty nearest_Distance = null;
        protected MaterialProperty outlineTex = null;
        protected MaterialProperty bakedNormal = null;
        protected MaterialProperty tessEdgeLength = null;
        protected MaterialProperty tessPhongStrength = null;
        protected MaterialProperty tessExtrusionAmount = null;
        protected MaterialProperty gi_Intensity = null;
        protected MaterialProperty unlit_Intensity = null;
        protected MaterialProperty offset_X_Axis_BLD = null;
        protected MaterialProperty offset_Y_Axis_BLD = null;
        //------------------------------------------------------

        protected MaterialEditor m_MaterialEditor;

 



        const int HDRPGeometryMin = 2650; // UnityEngine.Rendering.RenderQueue.Geometry;
        private void UpdateVersionInMaterial(Material material)
        {
            MaterialSetInt(material,ShaderPropIsUnityToonShader, 1);
            material.SetFloat(ShaderPropUtsVersionX,  kVersionX);
            material.SetFloat(ShaderPropUtsVersionY,  kVersionY);
            material.SetFloat(ShaderPropUtsVersionZ,  kVersionZ);

        }








        private bool isShadingGrademap
        {
            get
            {

                Material material = m_MaterialEditor.target as Material;
                return MaterialGetInt(material,ShaderPropUtsTechniqe) == (int)UTS_Mode.ShadingGradeMap;

            }
        }

        public static GUIContent specularModeText = new GUIContent("Specular Mode", "Specular light mode. Solid Color or Natural.");
        public static GUIContent specularBlendModeText = new GUIContent("Color Blend Mode", "Specular color blending mode. Multiply or Additive.");
        public static GUIContent matcapBlendModeText = new GUIContent("Color Blend Mode", "MatCap color blending mode. Multiply or Additive.");
        public static GUIContent matcapOrthoText = new GUIContent("MatCap Camera Mode", "MatCap camera mode. Perspective or Orthographic.");
        public static GUIContent transparentModeText = new GUIContent("Transparency",
            "Transparency  mode that fits you. ");
        public static GUIContent workflowModeText = new GUIContent("Mode",
            "Select a workflow that fits your textures. Choose between Standard or With Additional Control Maps.");
        // -----------------------------------------------------
        public static GUIContent clippingmodeModeText0 = new GUIContent("Clipping",
            "Select clipping mode that fits you. ");
        public static GUIContent clippingmodeModeText1 = new GUIContent("Trans Clipping",
            "Select clipping mode that fits you. ");
        public static GUIContent stencilmodeModeText = new GUIContent("Stencil",
            "Select stencil mode that fits you. ");
        //Specify only those that use the m_MaterialEditor method as their UI.
        public void FindProperties(MaterialProperty[] props)
        {
            // false is added if theare are possiblities the properties are not aveialable
            utsTechnique = FindProperty(ShaderPropUtsTechniqe, props);
            specularMode = FindProperty(ShaderPropIs_SpecularToHighColor, props);
            specularBlendMode = FindProperty(ShaderPropIs_BlendAddToHiColor, props);
            matcapBlendMode = FindProperty(ShaderPropIs_BlendAddToMatCap, props);
            matcapCameraMode = FindProperty(ShaderPropIs_Ortho, props);
            transparentMode = FindProperty(ShaderPropTransparentEnabled, props);
            clippingMask = FindProperty(ShaderPropClippingMask, props);
            clippingMode = FindProperty(ShaderPropClippingMode, props);
            clipping_Level = FindProperty("_Clipping_Level", props, false);
            stencilValue = FindProperty(ShaderPropStencilNo, props);
            tweak_transparency = FindProperty("_Tweak_transparency", props, false);
            stencilMode = FindProperty(ShaderPropStencilMode, props);
            mainTex = FindProperty(ShaderPropMainTex, props);
            baseColor = FindProperty("_BaseColor", props);
            firstShadeMap = FindProperty("_1st_ShadeMap", props);
            firstShadeColor = FindProperty("_1st_ShadeColor", props);
            secondShadeMap = FindProperty("_2nd_ShadeMap", props);
            secondShadeColor = FindProperty("_2nd_ShadeColor", props);
            normalMap = FindProperty("_NormalMap", props);
            bumpScale = FindProperty("_BumpScale", props);
            set_1st_ShadePosition = FindProperty(ShaderProp_Set_1st_ShadePosition, props, false);
            set_2nd_ShadePosition = FindProperty(ShaderProp_Set_2nd_ShadePosition, props, false);
            shadingGradeMap = FindProperty(ShaderProp_ShadingGradeMap, props, false);
            tweak_ShadingGradeMapLevel = FindProperty("_Tweak_ShadingGradeMapLevel", props, false);
            blurLevelSGM = FindProperty("_BlurLevelSGM", props, false);
            tweak_SystemShadowsLevel = FindProperty("_Tweak_SystemShadowsLevel", props);
            baseColor_Step = FindProperty(ShaderPropBaseColor_Step, props);
            baseShade_Feather = FindProperty(ShaderPropBaseShade_Feather, props);
            shadeColor_Step = FindProperty(ShaderPropShadeColor_Step, props);
            first2nd_Shades_Feather = FindProperty(ShaderProp1st2nd_Shades_Feather, props);
            first_ShadeColor_Step = FindProperty(ShaderProp1st_ShadeColor_Step, props);
            first_ShadeColor_Feather = FindProperty(ShaderProp1st_ShadeColor_Feather, props);
            second_ShadeColor_Step = FindProperty(ShaderProp2nd_ShadeColor_Step, props);
            second_ShadeColor_Feather = FindProperty(ShaderProp2nd_ShadeColor_Feather, props);
            stepOffset = FindProperty("_StepOffset", props, false);
            highColor_Tex = FindProperty(ShaderProp_HighColor_Tex, props);
            highColor = FindProperty("_HighColor", props);
            highColor_Power = FindProperty("_HighColor_Power", props);
            tweakHighColorOnShadow = FindProperty("_TweakHighColorOnShadow", props);
            set_HighColorMask = FindProperty(ShaderProp_Set_HighColorMask, props);
            tweak_HighColorMaskLevel = FindProperty("_Tweak_HighColorMaskLevel", props);
            rimLightColor = FindProperty("_RimLightColor", props);
            rimLight_Power = FindProperty("_RimLight_Power", props);
            rimLight_InsideMask = FindProperty("_RimLight_InsideMask", props);
            tweak_LightDirection_MaskLevel = FindProperty("_Tweak_LightDirection_MaskLevel", props);
            ap_RimLightColor = FindProperty("_Ap_RimLightColor", props);
            ap_RimLight_Power = FindProperty("_Ap_RimLight_Power", props);
            set_RimLightMask = FindProperty(ShaderProp_Set_RimLightMask, props);
            tweak_RimLightMaskLevel = FindProperty("_Tweak_RimLightMaskLevel", props);
            matCap_Sampler = FindProperty(ShaderProp_MatCap_Sampler, props);
            matCapColor = FindProperty("_MatCapColor", props);
            blurLevelMatcap = FindProperty("_BlurLevelMatcap", props);
            tweak_MatCapUV = FindProperty("_Tweak_MatCapUV", props);
            rotate_MatCapUV = FindProperty("_Rotate_MatCapUV", props);
            normalMapForMatCap = FindProperty("_NormalMapForMatCap", props);
            bumpScaleMatcap = FindProperty("_BumpScaleMatcap", props);
            rotate_NormalMapForMatCapUV = FindProperty("_Rotate_NormalMapForMatCapUV", props);
            tweakMatCapOnShadow = FindProperty("_TweakMatCapOnShadow", props);
            set_MatcapMask = FindProperty(ShaderProp_Set_MatcapMask, props);
            tweak_MatcapMaskLevel = FindProperty("_Tweak_MatcapMaskLevel", props);
            angelRing_Sampler = FindProperty("_AngelRing_Sampler", props, false);
            angelRing_Color = FindProperty("_AngelRing_Color", props, false);
            ar_OffsetU = FindProperty("_AR_OffsetU", props, false);
            ar_OffsetV = FindProperty("_AR_OffsetV", props, false);
            emissive_Tex = FindProperty("_Emissive_Tex", props);
            emissive_Color = FindProperty("_Emissive_Color", props);
            base_Speed = FindProperty("_Base_Speed", props);
            scroll_EmissiveU = FindProperty("_Scroll_EmissiveU", props);
            scroll_EmissiveV = FindProperty("_Scroll_EmissiveV", props);
            rotate_EmissiveUV = FindProperty("_Rotate_EmissiveUV", props);
            colorShift = FindProperty("_ColorShift", props);
            colorShift_Speed = FindProperty("_ColorShift_Speed", props);
            viewShift = FindProperty("_ViewShift", props);
            outline_Width = FindProperty("_Outline_Width", props, false);
            outline_Color = FindProperty("_Outline_Color", props, false);
            outline_Sampler = FindProperty(ShaderProp_Outline_Sampler, props, false);
            offset_Z = FindProperty("_Offset_Z", props, false);
            farthest_Distance = FindProperty("_Farthest_Distance", props, false);
            nearest_Distance = FindProperty("_Nearest_Distance", props, false);
            outlineTex = FindProperty(ShaderProp_OutlineTex, props, false);
            bakedNormal = FindProperty("_BakedNormal", props, false);
            tessEdgeLength = FindProperty("_TessEdgeLength", props, false);
            tessPhongStrength = FindProperty("_TessPhongStrength", props, false);
            tessExtrusionAmount = FindProperty("_TessExtrusionAmount", props, false);
            gi_Intensity = FindProperty(ShaderPropGI_Intensity, props);
            unlit_Intensity = FindProperty(ShaderPropUnlit_Intensity, props);
            offset_X_Axis_BLD = FindProperty("_Offset_X_Axis_BLD", props);
            offset_Y_Axis_BLD = FindProperty("_Offset_Y_Axis_BLD", props);

            FindTessellationProperties(props);
        }
        // --------------------------------

        // --------------------------------
        static void Line()
        {
            GUILayout.Box("", GUILayout.ExpandWidth(true), GUILayout.Height(1));
        }

        protected static bool Foldout(bool display, GUIContent title)
        {

            return EditorGUILayout.Foldout(display, title, true );
        }

        static bool FoldoutSubMenu(bool display, GUIContent title)
        {
            return EditorGUILayout.Foldout(display, title, true);
        }




        // --------------------------------
        //Specify only those that use the m_MaterialEditor method as their UI. For specifying textures and colors on a single line.
        private static class Styles
        {
            public static readonly GUIContent ShaderFoldout = EditorGUIUtility.TrTextContent("Shader Settings", "Basic Shader Settings");
            public static readonly GUIContent BasicColorFoldout = EditorGUIUtility.TrTextContent("Three Color and Control Map Settings", "");
            public static readonly GUIContent BasicLookDevsFoldout = EditorGUIUtility.TrTextContent("Shading Step and Feather Settings", "");
            public static readonly GUIContent HighLightFoldout = EditorGUIUtility.TrTextContent("Highlight Settings", "");
            public static readonly GUIContent RimLightFoldout = EditorGUIUtility.TrTextContent("Rim Light Settings", "");
            public static readonly GUIContent MatCapFoldout = EditorGUIUtility.TrTextContent("Material Capture (MatCap) Settings", "");
            public static readonly GUIContent AngelRingFoldout = EditorGUIUtility.TrTextContent("Angel Ring Projection Settings", "");
            public static readonly GUIContent EmissionFoldout = EditorGUIUtility.TrTextContent("Emission Settings", "");
            public static readonly GUIContent OutlineFoldout = EditorGUIUtility.TrTextContent("Outline Settings", "");
            public static readonly GUIContent TessellationFoldout = EditorGUIUtility.TrTextContent("Tessellation Settings", "");
            public static readonly GUIContent MaskRenderingFoldout = EditorGUIUtility.TrTextContent("Mask Rendering Settings", "");
            public static readonly GUIContent LightColorEffectivenessFoldout = EditorGUIUtility.TrTextContent("Scene Light Effectiveness Settings", "");

            public static readonly GUIContent MetaverseSettingsFoldout = EditorGUIUtility.TrTextContent("Metaverse Settings (Experimental)", "When no directional lights are in the scene.");
            public static readonly GUIContent NormalMapFoldout = EditorGUIUtility.TrTextContent("NormalMap Settings", "");
            public static readonly GUIContent ShadowControlMapFoldout = EditorGUIUtility.TrTextContent("Shadow Control Maps", "");
            public static readonly GUIContent PointLightFoldout = EditorGUIUtility.TrTextContent("Point Light Settings", "");
            public static readonly GUIContent AdvancedOutlineFoldout = EditorGUIUtility.TrTextContent("Advanced Outline Settings", "");
           

            public static readonly GUIContent baseColorText = new GUIContent("Base Map", "Base Color : Texture(sRGB) × Color(RGB) Default:White");
            public static readonly GUIContent firstShadeColorText = new GUIContent("1st Shading Map", "1st ShadeColor : Texture(sRGB) × Color(RGB) Default:White");
            public static readonly GUIContent secondShadeColorText = new GUIContent("2nd Shading Map", "2nd ShadeColor : Texture(sRGB) × Color(RGB) Default:White");
            public static readonly GUIContent normalMapText = new GUIContent("Normal Map", "Normal Map : Texture(bump)");
            public static readonly GUIContent highColorText = new GUIContent("Highlight", "Highlight : Texture(sRGB) × Color(RGB) Default:Black");
            public static readonly GUIContent highColorMaskText = new GUIContent("Highlight Mask", "Highlight Mask : Texture(linear)");
            public static readonly GUIContent rimLightMaskText = new GUIContent("Rim Light Mask", "Rim Light Mask : Texture(linear)");
            public static readonly GUIContent matCapSamplerText = new GUIContent("MatCap Sampler", "MatCap Sampler : Texture(sRGB) × Color(RGB) Default:White");
            public static readonly GUIContent matCapMaskText = new GUIContent("MatCap Mask", "MatCap Mask : Texture(linear)");
            public static readonly GUIContent angelRingText = new GUIContent("Angel Ring", "AngelRing : Texture(sRGB) × Color(RGB) Default:Black");
            public static readonly GUIContent emissiveTexText = new GUIContent("Emissive Map", "Emission : Texture(sRGB)× EmissiveMask(alpha) × Color(HDR) Default:Black");
            public static readonly GUIContent shadingGradeMapText = new GUIContent("Shading Grade Map", "Specify shadow-prone areas in UV coordinates. Shading Grade Map : Texture(linear)");
            public static readonly GUIContent firstPositionMapText = new GUIContent("1st Shading Position Map", "Specify the position of fixed shadows that fall in 1st shade color areas in UV coordinates. 1st Position Map : Texture(linear)");
            public static readonly GUIContent secondPositionMapText = new GUIContent("2nd Shading Position Map", "Specify the position of fixed shadows that fall in 2nd shade color areas in UV coordinates. 2nd Position Map : Texture(linear)");
            public static readonly GUIContent outlineSamplerText = new GUIContent("Outline Sampler", "Outline Sampler : Texture(linear)");
            public static readonly GUIContent outlineTexText = new GUIContent("Outline tex", "Outline Tex : Texture(sRGB) Default:White");
            public static readonly GUIContent bakedNormalOutlineText = new GUIContent("Baked NormalMap for Outline", "Unpacked Normal Map : Texture(linear) Note that this is not a standard NORMAL MAP.");
            public static readonly GUIContent clippingMaskText = new GUIContent("Clipping Mask", "Clipping Mask : Texture(linear)");
        }
        // --------------------------------

        public UTS3GUI()
        {

        }

        // never called from the system??
        public override void OnClosed(Material material)
        { 

            base.OnClosed(material);
        }
        
        void OnOpenGUI(Material material, MaterialEditor materialEditor, MaterialProperty[] props)
        {
            m_MaterialScopeList.RegisterHeaderScope(Styles.ShaderFoldout, Expandable.Shader, DrawShaderOptions, (uint)UTS_Mode.ThreeColorToon, (uint)UTS_TransparentMode.Off);
            m_MaterialScopeList.RegisterHeaderScope(Styles.BasicColorFoldout, Expandable.BasicColor, GUI_BasicThreeColors, (uint)UTS_Mode.ThreeColorToon,(uint)UTS_TransparentMode.Off);
            m_MaterialScopeList.RegisterHeaderScope(Styles.BasicLookDevsFoldout, Expandable.BasicLookDevs, GUI_StepAndFeather, (uint)UTS_Mode.ThreeColorToon, (uint)UTS_TransparentMode.Off);
            m_MaterialScopeList.RegisterHeaderScope(Styles.HighLightFoldout, Expandable.HighLight, GUI_HighColor, (uint)UTS_Mode.ThreeColorToon, (uint)UTS_TransparentMode.Off);
            m_MaterialScopeList.RegisterHeaderScope(Styles.RimLightFoldout, Expandable.RimLight, GUI_RimLight, (uint)UTS_Mode.ThreeColorToon, (uint)UTS_TransparentMode.Off);
            m_MaterialScopeList.RegisterHeaderScope(Styles.MatCapFoldout, Expandable.MatCap, GUI_MatCap, (uint)UTS_Mode.ThreeColorToon, (uint)UTS_TransparentMode.Off);
            m_MaterialScopeList.RegisterHeaderScope(Styles.AngelRingFoldout, Expandable.AngelRing, GUI_AngelRing, (uint)UTS_Mode.ShadingGradeMap, (uint)UTS_TransparentMode.Off);
            m_MaterialScopeList.RegisterHeaderScope(Styles.EmissionFoldout, Expandable.Emission, GUI_Emissive, (uint)UTS_Mode.ThreeColorToon, (uint)UTS_TransparentMode.Off);
            m_MaterialScopeList.RegisterHeaderScope(Styles.OutlineFoldout, Expandable.Outline, GUI_Outline, (uint)UTS_Mode.ThreeColorToon, (uint)UTS_TransparentMode.On);
            if (material.HasProperty("_TessEdgeLength") && currentRenderPipeline == RenderPipeline.Legacy)
            { 
                m_MaterialScopeList.RegisterHeaderScope(Styles.TessellationFoldout, Expandable.Tessellation, GUI_Tessellation, (uint)UTS_Mode.ThreeColorToon, (uint)UTS_TransparentMode.Off);
            }
            else if (tessellationMode != null && currentRenderPipeline == RenderPipeline.HDRP)
            {
                m_MaterialScopeList.RegisterHeaderScope(Styles.TessellationFoldout, Expandable.Tessellation, GUI_TessellationHDRP, (uint)UTS_Mode.ThreeColorToon, (uint)UTS_TransparentMode.Off);

            }
            // originally these were in simple UI
            m_MaterialScopeList.RegisterHeaderScope(Styles.LightColorEffectivenessFoldout, Expandable.LightColorEffectiveness, GUI_LightColorEffectiveness, (uint)UTS_Mode.ThreeColorToon, (uint)UTS_TransparentMode.Off);
            m_MaterialScopeList.RegisterHeaderScope(Styles.MetaverseSettingsFoldout, Expandable.MetaverseSettings, GUI_MetaverseSettings, (uint)UTS_Mode.ThreeColorToon, (uint)UTS_TransparentMode.Off);
        }

        void UTS3DrawHeaders(MaterialEditor materialEditor, Material material)
        {
            if (material == null)
                throw new ArgumentNullException(nameof(material));

            if (materialEditor == null)
                throw new ArgumentNullException(nameof(materialEditor));

            foreach (var item in m_MaterialScopeList.m_Items)
            {
                if ( !isShadingGrademap && item.workflowMode == (uint)UTS_Mode.ShadingGradeMap)
                {
                    continue;
                }
                if ( transparencyEnabled == UTS_TransparentMode.On && item.transparentEnabled == (uint)UTS_TransparentMode.On)
                {
                    continue;
                }
                using (var header = new UTS3MaterialHeaderScope(
                    item.headerTitle,
                    item.expandable,
                    materialEditor,
                    defaultExpandedState: m_MaterialScopeList.m_DefaultExpandedState,
                    documentationURL: item.url))
                {
                    if (!header.expanded)
                        continue;

                    item.drawMaterialScope(material);

                    EditorGUILayout.Space();
                }
            }


        }
        void ShaderPropertiesGUI(MaterialEditor materialEditor, Material material, MaterialProperty[] properties)
        {
            UTS3DrawHeaders(materialEditor, material);
        }

        void DrawShaderOptions(Material material)
        {


            GUI_SetCullingMode(material);
            GUI_SetRenderQueue(material);
            GUI_Tranparent(material);
            GUI_StencilMode(material);

            switch (m_WorkflowMode)
            {
                case UTS_Mode.ThreeColorToon:

                    DoPopup(clippingmodeModeText0, clippingMode, ClippingModeNames);
                    UTS_ClippingMode mode0 = (UTS_ClippingMode)MaterialGetInt(material, ShaderPropClippingMode);
                    EditorGUI.indentLevel++;
                    EditorGUI.BeginDisabledGroup(mode0 == UTS_ClippingMode.Off);
                    {
                        GUI_SetClippingMask(material);
                    }
                    EditorGUI.EndDisabledGroup();
                    EditorGUI.BeginDisabledGroup(mode0 != UTS_ClippingMode.TransClippingMode);
                    {
                        GUI_SetTransparencySetting(material);
                    }
                    EditorGUI.EndDisabledGroup();
                    EditorGUI.indentLevel--;
                    break;
                case UTS_Mode.ShadingGradeMap:

                    DoPopup(clippingmodeModeText1, clippingMode, System.Enum.GetNames(typeof(UTS_TransClippingMode)));
                    UTS_TransClippingMode mode1 = (UTS_TransClippingMode)MaterialGetInt(material, ShaderPropClippingMode);
                    EditorGUI.indentLevel++;
                    EditorGUI.BeginDisabledGroup(mode1 != UTS_TransClippingMode.On);
                    {
                        GUI_SetClippingMask(material);
                        GUI_SetTransparencySetting(material);
                    }
                    EditorGUI.EndDisabledGroup();
                    EditorGUI.indentLevel--;
                    break;
            }

            EditorGUILayout.Space();







        }
        public override void OnGUI(MaterialEditor materialEditor, MaterialProperty[] props)
        {

            m_MaterialEditor = materialEditor;
            Material material = materialEditor.target as Material;
            EditorGUIUtility.fieldWidth = 0;
            if (m_FirstTimeApply)
            {
                FindProperties(props);
                OnOpenGUI(material, materialEditor, props);
                m_FirstTimeApply = false;
            }

            UpdateVersionInMaterial(material);

            m_autoRenderQueue = MaterialGetInt(material,ShaderPropAutoRenderQueue);
            transparencyEnabled = (UTS_TransparentMode)MaterialGetInt(material,ShaderPropTransparentEnabled);
            stencilNumberSetting = MaterialGetInt(material,ShaderPropStencilNo);

            //Line 1 horizontal 3 buttons.
            EditorGUILayout.BeginHorizontal();
#if USE_SIMPLE_UI   // disabled SimpleUI
            //Original Inspector Selection Check.
            if (material.HasProperty(ShaderPropSimpleUI))
            {
                var selectedUI = material.GetInt(ShaderPropSimpleUI);
                if (selectedUI == 2)
                {
                    _OriginalInspector = true;  //Original GUI
                }
                else if (selectedUI == 1)
                {
                    _SimpleUI = true;   //UTS2 Beginner GUI
                }
                //Original/Custom GUI toggle button.
                if (_OriginalInspector)
                {
                    if (GUILayout.Button("Change CustomUI", middleButtonStyle))
                    {
                        _OriginalInspector = false;
                        MaterialSetInt(material,ShaderPropSimpleUI, 0); //UTS2 Pro GUI
                    }
                    OpenManualLink();
                    //Clear inherited layouts.
                    EditorGUILayout.EndHorizontal();
                    //Show Original GUI.
                    m_MaterialEditor.PropertiesDefaultGUI(props);
                    return;
                }
                if (GUILayout.Button("Show All properties", middleButtonStyle))
                {
                    _OriginalInspector = true;
                    MaterialSetInt(material,ShaderPropSimpleUI, 2); //Original GUI
                }
            }
#endif

            EditorGUILayout.EndHorizontal();

            EditorGUI.BeginChangeCheck();



            // select UTS technique here.
            DoPopup(workflowModeText, utsTechnique, UtsModeNames);
            m_WorkflowMode = (UTS_Mode)MaterialGetInt(material, ShaderPropUtsTechniqe);
            switch (m_WorkflowMode)
            {
                case UTS_Mode.ThreeColorToon:
                    material.DisableKeyword(ShaderDefineSHADINGGRADEMAP);
                    break;
                case UTS_Mode.ShadingGradeMap:
                    material.EnableKeyword(ShaderDefineSHADINGGRADEMAP);
                    break;
            }

            if (transparencyEnabled != UTS_TransparentMode.On)
            {
                SetupOutline(material);
            }
            else
            {
                SetupOverDrawTransparentObject(material);
            }
            ShaderPropertiesGUI(materialEditor, material, props);

            ApplyRenderingPerChennelsSetting(material);
            ApplyClippingMode(material);
            ApplyStencilMode(material);
            ApplyAngelRing(material);
            ApplyTessellation(material);
            ApplyMatCapMode(material);
            ApplyQueueAndRenderType(m_WorkflowMode, material);



            if (EditorGUI.EndChangeCheck())
            {
                m_MaterialEditor.PropertiesChanged();
            }

        }// End of OnGUI()



        internal static int  MaterialGetInt(Material material, string prop )
        {
#if UNITY_2021_1_OR_NEWER
            return (int)material.GetFloat(prop);
#else
            return material.GetInt(prop);
#endif
        }
        internal static void MaterialSetInt(Material material, string prop, int value)
        {
#if UNITY_2021_1_OR_NEWER
            material.SetFloat(prop, value);
#else
            material.SetInt(prop, value);
#endif
        }
        bool GUI_ToggleShaderKeyword(Material material, string label, string keyword)
        {
            var isEnabled = material.IsKeywordEnabled(keyword);

            EditorGUI.BeginChangeCheck();
            var ret = EditorGUILayout.Toggle(label, isEnabled);
            if (EditorGUI.EndChangeCheck())
            {
                m_MaterialEditor.RegisterPropertyChangeUndo(label);
                if ( ret == false )
                {
                    material.DisableKeyword(keyword);
                }
                else
                {
                    material.EnableKeyword(keyword);
                }
            }
            return ret;
        }
        bool GUI_Toggle(Material material, string label, string prop, bool value)
        {
            EditorGUI.BeginChangeCheck();
            var ret = EditorGUILayout.Toggle(label, value);
            if (EditorGUI.EndChangeCheck())
            {
                m_MaterialEditor.RegisterPropertyChangeUndo(label);
                MaterialSetInt(material, prop, ret ? 1 : 0);
            }
            return ret;
        }

        // --------------------------------



        void GUI_SetRTHS(Material material)
        {

            EditorGUILayout.BeginHorizontal();


            var isRTHSenabled = GUI_ToggleShaderKeyword(material, "Raytraced Hard Shadow (deprecated)", ShaderDefineUTS_USE_RAYTRACING_SHADOW);

            EditorGUILayout.EndHorizontal();
            if (isRTHSenabled)
            {
                EditorGUILayout.LabelField("ShadowRaytracer component must be attached to the camera when this feature is enabled.");
            }
        }

        void GUI_SetRenderQueue(Material material)
        {

            EditorGUILayout.BeginHorizontal();

            m_autoRenderQueue = GUI_Toggle(material, "Auto Render Queue", ShaderPropAutoRenderQueue, m_autoRenderQueue == 1) ? 1 : 0;

            EditorGUILayout.EndHorizontal();
            EditorGUI.indentLevel++;
            EditorGUI.BeginDisabledGroup(m_autoRenderQueue == 1);
            m_renderQueue = (int)EditorGUILayout.IntField("Render Queue", material.renderQueue);
            EditorGUI.EndDisabledGroup();
            EditorGUI.indentLevel--;
        }


        void GUI_SetCullingMode(Material material)
        {
            const string _CullMode = "_CullMode";
            int _CullMode_Setting = MaterialGetInt(material,_CullMode);
            //Convert it to Enum format and store it in the offlineMode variable.
            if ((int)CullingMode.CullingOff == _CullMode_Setting)
            {
                m_cullingMode = CullingMode.CullingOff;
            }
            else if ((int)CullingMode.FrontCulling == _CullMode_Setting)
            {
                m_cullingMode = CullingMode.FrontCulling;
            }
            else
            {
                m_cullingMode = CullingMode.BackCulling;
            }
            //GUI description with EnumPopup.
            m_cullingMode = (CullingMode)EditorGUILayout.EnumPopup("Culling Mode", m_cullingMode);
            //If the value changes, write to the material.
            if (_CullMode_Setting != (int)m_cullingMode)
            {
                switch (m_cullingMode)
                {
                    case CullingMode.CullingOff:
                        MaterialSetInt(material,_CullMode, 0);
                        break;
                    case CullingMode.FrontCulling:
                        MaterialSetInt(material,_CullMode, 1);
                        break;
                    default:
                        MaterialSetInt(material,_CullMode, 2);
                        break;
                }

            }


        }
        void GUI_Tranparent(Material material)
        {

            const string _ZWriteMode = "_ZWriteMode";
            const string _ZOverDrawMode = "_ZOverDrawMode";
            DoPopup(transparentModeText, transparentMode, System.Enum.GetNames(typeof(UTS_TransparentMode)));


            if (transparencyEnabled == UTS_TransparentMode.On)
            {
                if (MaterialGetInt(material,ShaderPropUtsTechniqe) == (int)UTS_Mode.ThreeColorToon)
                {
                    MaterialSetInt(material,ShaderPropClippingMode, (int)UTS_ClippingMode.TransClippingMode);
                }
                else
                {
                    // ShadingGradeMap
                    MaterialSetInt(material,ShaderPropClippingMode, (int)UTS_TransClippingMode.On);
                }
                MaterialSetInt(material,_ZWriteMode, 0);
                material.SetFloat(_ZOverDrawMode, 1);
            }
            else
            {
                MaterialSetInt(material,_ZWriteMode, 1);
                material.SetFloat(_ZOverDrawMode, 0);
            }

        }

        void GUI_StencilMode(Material material)
        {
            const string kStencilValue = "Stencil Value";

            DoPopup(stencilmodeModeText, stencilMode, StencilModeNames );


            EditorGUI.indentLevel++;
            int currentStencilValue = stencilNumberSetting;
            EditorGUI.BeginDisabledGroup((UTS_StencilMode)MaterialGetInt(material, ShaderPropStencilMode) == UTS_StencilMode.Off);
            EditorGUI.BeginChangeCheck();
            currentStencilValue = EditorGUILayout.IntSlider(kStencilValue, stencilNumberSetting, 0, 255);
            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(material, "Changed " + kStencilValue);
                MaterialSetInt(material, ShaderPropStencilNo, currentStencilValue);
            }

            EditorGUI.EndDisabledGroup();

            EditorGUI.indentLevel--;
        }

        void GUI_SetClippingMask(Material material)
        {
            m_MaterialEditor.TexturePropertySingleLine(Styles.clippingMaskText, clippingMask);

            GUI_Toggle(material, "Invert Clipping Mask", ShaderPropInverseClipping,MaterialGetInt(material, ShaderPropInverseClipping)!= 0 );

            m_MaterialEditor.RangeProperty(clipping_Level, "Clipping Level");
        }

        void GUI_SetTransparencySetting(Material material)
        {
            m_MaterialEditor.RangeProperty(tweak_transparency, "Transparency Level");

            GUI_Toggle(material, "Use BaseMap Alpha as Clipping Mask", ShaderPropIsBaseMapAlphaAsClippingMask, MaterialGetInt(material, ShaderPropIsBaseMapAlphaAsClippingMask) != 0);

        }




        void GUI_BasicThreeColors(Material material)
        {


            m_MaterialEditor.TexturePropertySingleLine(Styles.baseColorText, mainTex, baseColor);
            //v.2.0.7 Synchronize _Color to _BaseColor.
            if (material.HasProperty("_Color"))
            {
                material.SetColor("_Color", material.GetColor("_BaseColor"));
            }
            //

            EditorGUI.indentLevel += 2;
            var applyTo1st = GUI_Toggle(material, "Apply to 1st shading map", ShaderPropUse_BaseAs1st, MaterialGetInt(material, ShaderPropUse_BaseAs1st) != 0);
            EditorGUI.indentLevel -= 2;




            EditorGUI.BeginDisabledGroup(applyTo1st);
            m_MaterialEditor.TexturePropertySingleLine(Styles.firstShadeColorText, firstShadeMap, firstShadeColor);
            EditorGUI.EndDisabledGroup();

            EditorGUI.indentLevel+=2;
            var applyTo2nd =  GUI_Toggle(material, "Apply to 2nd shading map", ShaderPropUse_1stAs2nd, MaterialGetInt(material, ShaderPropUse_1stAs2nd) != 0);
            EditorGUI.indentLevel-=2;


            EditorGUI.BeginDisabledGroup(applyTo2nd);
            m_MaterialEditor.TexturePropertySingleLine(Styles.secondShadeColorText, secondShadeMap, secondShadeColor);
            EditorGUI.EndDisabledGroup();
            EditorGUILayout.Space();

            _NormalMap_Foldout = FoldoutSubMenu(_NormalMap_Foldout, Styles.NormalMapFoldout);
            if (_NormalMap_Foldout)
            {
                //GUILayout.Label("NormalMap Settings", EditorStyles.boldLabel);
                m_MaterialEditor.TexturePropertySingleLine(Styles.normalMapText, normalMap, bumpScale);
                m_MaterialEditor.TextureScaleOffsetProperty(normalMap);

                EditorGUI.indentLevel++;

                GUILayout.Label("  NormalMap Effectiveness", EditorStyles.boldLabel);

                GUI_Toggle(material, "3 Basic Colors", ShaderPropIs_NormalMapToBase, MaterialGetInt(material, ShaderPropIs_NormalMapToBase) != 0);
                GUI_Toggle(material, "Highlight", ShaderPropNormalMapToHighColor, MaterialGetInt(material, ShaderPropNormalMapToHighColor) != 0);
                GUI_Toggle(material, "Rim Light", ShaderPropIsNormalMapToRimLight, MaterialGetInt(material, ShaderPropIsNormalMapToRimLight) != 0);

                EditorGUI.indentLevel--;
                EditorGUILayout.Space();
            }

            _ShadowControlMaps_Foldout = FoldoutSubMenu(_ShadowControlMaps_Foldout, Styles.ShadowControlMapFoldout);
            if (_ShadowControlMaps_Foldout)
            {
                EditorGUI.indentLevel++;
                GUI_ShadowControlMaps(material);
                EditorGUI.indentLevel--;
                EditorGUILayout.Space();
            }
        }

        void GUI_ShadowControlMaps(Material material)
        {
            if (material.HasProperty(ShaderPropUtsTechniqe))//DoubleWithFeather or ShadingGradeMap
            {
                if (MaterialGetInt(material,ShaderPropUtsTechniqe) == (int)UTS_Mode.ThreeColorToon)   //DWF
                {
                    GUILayout.Label("  Mode: Standard", EditorStyles.boldLabel);
                    m_MaterialEditor.TexturePropertySingleLine(Styles.firstPositionMapText, set_1st_ShadePosition);
                    m_MaterialEditor.TexturePropertySingleLine(Styles.secondPositionMapText, set_2nd_ShadePosition);
                }
                else if (MaterialGetInt(material,ShaderPropUtsTechniqe) == (int)UTS_Mode.ShadingGradeMap)
                {    //SGM
                    GUILayout.Label("  Mode: With Additional Control Maps", EditorStyles.boldLabel);
                    m_MaterialEditor.TexturePropertySingleLine(Styles.shadingGradeMapText, shadingGradeMap);
                    m_MaterialEditor.RangeProperty(tweak_ShadingGradeMapLevel, "ShadingGradeMap Level");
                    m_MaterialEditor.RangeProperty(blurLevelSGM, "Blur Level of ShadingGradeMap");
                }
            }
        }

        void GUI_StepAndFeather(Material material)
        {
            GUI_BasicLookdevs(material);

            if (!_SimpleUI)
            {
                GUI_SystemShadows(material);

                if (material.HasProperty("_StepOffset"))//Items not in Mobile & Light Mode.                
                {

                    _AdditionalLookdevs_Foldout = FoldoutSubMenu(_AdditionalLookdevs_Foldout, Styles.PointLightFoldout);
                    if (_AdditionalLookdevs_Foldout)
                    {
                        GUI_AdditionalLookdevs(material);
                    }

                }
            }
        }

        void GUI_SystemShadows(Material material)
        {


            bool isEnabled = GUI_Toggle(material, "Receive System Shadows", ShaderPropSetSystemShadowsToBase, MaterialGetInt(material,ShaderPropSetSystemShadowsToBase) != 0);

            //           if (material.GetFloat(ShaderPropSetSystemShadowsToBase) == 1)
            EditorGUI.BeginDisabledGroup(!isEnabled);
            {
                EditorGUI.indentLevel++;
                m_MaterialEditor.RangeProperty(tweak_SystemShadowsLevel, "System Shadow Level");
                if (UnityToonShaderSettings.instance.m_ShowDepracated)
                {
                    GUI_SetRTHS(material);
                }
                EditorGUI.indentLevel--;
                EditorGUILayout.Space();
            }
            EditorGUI.EndDisabledGroup();
            EditorGUILayout.Space();
        }

        void GUI_BasicLookdevs(Material material)
        {
            if (material.HasProperty(ShaderPropUtsTechniqe))//DoubleWithFeather or ShadingGradeMap
            {
                var mode = MaterialGetInt(material, ShaderPropUtsTechniqe);
                if (mode == (int)UTS_Mode.ThreeColorToon)   //DWF
                {
                    GUILayout.Label("Mode: Standard", EditorStyles.boldLabel);
                    m_MaterialEditor.RangeProperty(baseColor_Step, "Base Color Step");
                    m_MaterialEditor.RangeProperty(baseShade_Feather, "Base Shading Feather");
                    m_MaterialEditor.RangeProperty(shadeColor_Step, "Shading Color Step");
                    m_MaterialEditor.RangeProperty(first2nd_Shades_Feather, "1st/2nd Shading Feather");
                    //Sharing variables with ShadingGradeMap method.

                    material.SetFloat(ShaderProp1st_ShadeColor_Step, material.GetFloat(ShaderPropBaseColor_Step));
                    material.SetFloat(ShaderProp1st_ShadeColor_Feather, material.GetFloat(ShaderPropBaseShade_Feather));
                    material.SetFloat(ShaderProp2nd_ShadeColor_Step, material.GetFloat(ShaderPropShadeColor_Step));
                    material.SetFloat(ShaderProp2nd_ShadeColor_Feather, material.GetFloat(ShaderProp1st2nd_Shades_Feather));
                }
                else if (mode == (int)UTS_Mode.ShadingGradeMap)
                {    //SGM
                    GUILayout.Label("Mode: With Additional Control Maps", EditorStyles.boldLabel);
                    m_MaterialEditor.RangeProperty(first_ShadeColor_Step, "1st Shade Color Step");
                    m_MaterialEditor.RangeProperty(first_ShadeColor_Feather, "1st Shade Color Feather");
                    m_MaterialEditor.RangeProperty(second_ShadeColor_Step, "2nd Shade Color Step");
                    m_MaterialEditor.RangeProperty(second_ShadeColor_Feather, "2nd Shade Color Feather");
                    //Share variables with DoubleWithFeather method.
                    material.SetFloat(ShaderPropBaseColor_Step, material.GetFloat(ShaderProp1st_ShadeColor_Step));
                    material.SetFloat(ShaderPropBaseShade_Feather, material.GetFloat(ShaderProp1st_ShadeColor_Feather));
                    material.SetFloat(ShaderPropShadeColor_Step, material.GetFloat(ShaderProp2nd_ShadeColor_Step));
                    material.SetFloat(ShaderProp1st2nd_Shades_Feather, material.GetFloat(ShaderProp2nd_ShadeColor_Feather));
                }
                else
                {
                    // OutlineObj.
                    return;
                }
            }
            EditorGUILayout.Space();
        }

        void GUI_AdditionalLookdevs(Material material)
        {
//            GUILayout.Label("    Point Light Settings");
            EditorGUI.indentLevel++;
            m_MaterialEditor.RangeProperty(stepOffset, "Step Offset");

            GUI_Toggle(material, "Filter Point Light Highlights", ShaderPropIsFilterHiCutPointLightColor, MaterialGetInt(material, ShaderPropIsFilterHiCutPointLightColor) != 0);

            EditorGUI.indentLevel--;
            EditorGUILayout.Space();
        }


        void GUI_HighColor(Material material)
        {
            m_MaterialEditor.TexturePropertySingleLine(Styles.highColorText, highColor_Tex, highColor);
            m_MaterialEditor.RangeProperty(highColor_Power, "Highlight Power");

            if (!_SimpleUI)
            {

                EditorGUI.showMixedValue = specularMode.hasMixedValue;

                int mode = (int)specularMode.floatValue;
                EditorGUI.BeginChangeCheck();
                mode = EditorGUILayout.Popup(specularModeText, mode, System.Enum.GetNames(typeof(UTS_SpeculerMode)));
                if (EditorGUI.EndChangeCheck())
                {
                    m_MaterialEditor.RegisterPropertyChangeUndo(specularModeText.text);
                    switch ((UTS_SpeculerMode)mode)
                    {
                    case UTS_SpeculerMode.Solid:
                        break;
                    case UTS_SpeculerMode.Natural:
                        specularBlendMode.floatValue = 1.0f;
                        break;
                    }


                    specularMode.floatValue = mode;
                }
                EditorGUI.showMixedValue = false;


                EditorGUILayout.BeginHorizontal();
                EditorGUI.indentLevel++;

                //GUILayout.Space(60);
                EditorGUI.BeginDisabledGroup(mode != 0);
                EditorGUI.showMixedValue = specularBlendMode.hasMixedValue;
                int blendingMode = (int)specularBlendMode.floatValue;
                EditorGUI.BeginChangeCheck();
                blendingMode = EditorGUILayout.Popup(specularBlendModeText, blendingMode, System.Enum.GetNames(typeof(UTS_SpeculerColorBlendMode)));
                if (EditorGUI.EndChangeCheck())
                {
                    m_MaterialEditor.RegisterPropertyChangeUndo(specularModeText.text);
                    specularBlendMode.floatValue = blendingMode;
                }
                EditorGUI.indentLevel--;
                EditorGUILayout.EndHorizontal();
                EditorGUI.showMixedValue = false;
                EditorGUI.EndDisabledGroup();


                var ret = GUI_Toggle(material, "Shadow Mask on Highlights", ShaderPropIs_UseTweakHighColorOnShadow, MaterialGetInt(material, ShaderPropIs_UseTweakHighColorOnShadow) != 0);
                EditorGUI.BeginDisabledGroup(!ret);
                {
                    EditorGUI.indentLevel++;
                    m_MaterialEditor.RangeProperty(tweakHighColorOnShadow, "Highlight Power on Shadows");
                    EditorGUI.indentLevel--;
                }
                EditorGUI.EndDisabledGroup();

            }

            EditorGUILayout.Space();
            //Line();
            //EditorGUILayout.Space();

            GUILayout.Label("    Highlight Mask", EditorStyles.boldLabel);
            EditorGUI.indentLevel++;
            m_MaterialEditor.TexturePropertySingleLine(Styles.highColorMaskText, set_HighColorMask);
            m_MaterialEditor.RangeProperty(tweak_HighColorMaskLevel, "Highlight Mask Level");
            EditorGUI.indentLevel--;

            EditorGUILayout.Space();
        }

        void GUI_RimLight(Material material)
        {

            EditorGUILayout.BeginHorizontal();
            var rimLightEnabled = GUI_Toggle(material, "Rim Light", ShaderPropRimLight, MaterialGetInt(material, ShaderPropRimLight) != 0);
            EditorGUILayout.EndHorizontal();
            EditorGUI.BeginDisabledGroup(!rimLightEnabled);
            EditorGUI.indentLevel++;

            m_MaterialEditor.ColorProperty(rimLightColor, "Rim Light Color");
            m_MaterialEditor.RangeProperty(rimLight_Power, "Rim Light Power");

            if (!_SimpleUI)
            {
                m_MaterialEditor.RangeProperty(rimLight_InsideMask, "Rim Light Inside Mask");

                EditorGUILayout.BeginHorizontal();
                GUI_Toggle(material, "Rim Light Feather Off", ShaderPropRimLight_FeatherOff, MaterialGetInt(material, ShaderPropRimLight_FeatherOff) != 0);
                EditorGUILayout.EndHorizontal();
                EditorGUILayout.BeginHorizontal();

                //GUILayout.Space(60);
                var direcitonMaskEnabled = GUI_Toggle(material, "Light Direction Mask", ShaderPropLightDirection_MaskOn, MaterialGetInt(material, ShaderPropLightDirection_MaskOn) != 0);
                EditorGUILayout.EndHorizontal();

                EditorGUI.BeginDisabledGroup(!direcitonMaskEnabled);
                {
                    EditorGUI.indentLevel++;
                    m_MaterialEditor.RangeProperty(tweak_LightDirection_MaskLevel, "Light Direction Mask Level");

                    EditorGUILayout.BeginHorizontal();
                    var antipodean_RimLight = GUI_Toggle(material, "Inversed Rim Light", ShaderPropAdd_Antipodean_RimLight, MaterialGetInt(material, ShaderPropAdd_Antipodean_RimLight )!= 0);
                    EditorGUILayout.EndHorizontal();

                    EditorGUI.BeginDisabledGroup(!antipodean_RimLight);
                    {
                        EditorGUI.indentLevel++;

                        m_MaterialEditor.ColorProperty(ap_RimLightColor, "Inversed Rim Light Color");
                        m_MaterialEditor.RangeProperty(ap_RimLight_Power, "Inversed Rim Light Power");

                        EditorGUILayout.BeginHorizontal();
                        GUI_Toggle(material, "Inversed Rim Light Feather Off", ShaderPropAp_RimLight_FeatherOff, MaterialGetInt(material, ShaderPropAp_RimLight_FeatherOff) != 0);


                        EditorGUILayout.EndHorizontal();
                        EditorGUI.indentLevel--;
                    }
                    EditorGUI.EndDisabledGroup();
                    EditorGUI.indentLevel--;

                }//Light Direction Mask ON
                EditorGUI.EndDisabledGroup();
            }

            //EditorGUI.indentLevel++;

            EditorGUILayout.Space();
            //Line();
            //EditorGUILayout.Space();

            m_MaterialEditor.TexturePropertySingleLine(Styles.rimLightMaskText, set_RimLightMask);
            m_MaterialEditor.RangeProperty(tweak_RimLightMaskLevel, "Rim Light Mask Level");

            //EditorGUI.indentLevel--;

            EditorGUI.indentLevel--;
            EditorGUILayout.Space();

            EditorGUI.EndDisabledGroup();




        }

        void GUI_MatCap(Material material)
        {
            EditorGUILayout.BeginHorizontal();
            var matcapEnabled = GUI_Toggle(material, "MatCap", ShaderPropMatCap,MaterialGetInt(material, ShaderPropMatCap) != 0);
            EditorGUILayout.EndHorizontal();
            EditorGUI.BeginDisabledGroup(!matcapEnabled);

            m_MaterialEditor.TexturePropertySingleLine(Styles.matCapSamplerText, matCap_Sampler, matCapColor);
            EditorGUI.indentLevel++;
            m_MaterialEditor.TextureScaleOffsetProperty(matCap_Sampler);

            if (!_SimpleUI)
            {

                m_MaterialEditor.RangeProperty(blurLevelMatcap, "Blur Level of MatCap Sampler");

                EditorGUILayout.BeginHorizontal();
                DoPopup(matcapBlendModeText, matcapBlendMode, System.Enum.GetNames(typeof(UTS_MatcapColorBlendMode)));
                EditorGUILayout.EndHorizontal();

                m_MaterialEditor.RangeProperty(tweak_MatCapUV, "Scale MatCap UV");
                m_MaterialEditor.RangeProperty(rotate_MatCapUV, "Rotate MatCap UV");

                EditorGUILayout.BeginHorizontal();
                GUI_Toggle(material, "Camera Rolling Stabilizer", ShaderPropCameraRolling_Stabilizer, MaterialGetInt(material, ShaderPropCameraRolling_Stabilizer) != 0);
                EditorGUILayout.EndHorizontal();

                EditorGUILayout.BeginHorizontal();
                var isNormalMapForMatCap = GUI_Toggle(material, "NormalMap Specular Mask for MatCap", ShaderPropIs_NormalMapForMatCap, MaterialGetInt(material, ShaderPropIs_NormalMapForMatCap) != 0);

                //GUILayout.Space(60);

                EditorGUILayout.EndHorizontal();
                EditorGUI.BeginDisabledGroup(!isNormalMapForMatCap);
                {
                    EditorGUI.indentLevel++;
                    m_MaterialEditor.TexturePropertySingleLine(Styles.normalMapText, normalMapForMatCap, bumpScaleMatcap);
                    m_MaterialEditor.TextureScaleOffsetProperty(normalMapForMatCap);
                    m_MaterialEditor.RangeProperty(rotate_NormalMapForMatCapUV, "Rotate NormalMap UV");
                    EditorGUI.indentLevel--;
                }
                EditorGUI.EndDisabledGroup();

                EditorGUILayout.BeginHorizontal();
                var tweakMatCapOnShadows = GUI_Toggle(material, "MatCap on Shadows", ShaderPropIs_UseTweakMatCapOnShadow, MaterialGetInt(material, ShaderPropIs_UseTweakMatCapOnShadow) != 0);
 
                EditorGUILayout.EndHorizontal();
                EditorGUI.BeginDisabledGroup(!tweakMatCapOnShadows);
                {
                    EditorGUI.indentLevel++;
                    m_MaterialEditor.RangeProperty(tweakMatCapOnShadow, "MatCap Power on Shadows");
                    EditorGUI.indentLevel--;
                }
                EditorGUI.EndDisabledGroup();
                DoPopup(matcapOrthoText, matcapCameraMode, System.Enum.GetNames(typeof(CameraProjectionType)));
            }

            EditorGUILayout.Space();
            //Line();
            //EditorGUILayout.Space();

            GUILayout.Label("    MatCap Mask", EditorStyles.boldLabel);
            m_MaterialEditor.TexturePropertySingleLine(Styles.matCapMaskText, set_MatcapMask);
            m_MaterialEditor.TextureScaleOffsetProperty(set_MatcapMask);
            m_MaterialEditor.RangeProperty(tweak_MatcapMaskLevel, "MatCap Mask Level");

            GUI_Toggle(material, "Inverse MatCap Mask", ShaderPropInverse_MatcapMask, MaterialGetInt(material, ShaderPropInverse_MatcapMask) != 0);


            EditorGUI.indentLevel--;

            EditorGUI.EndDisabledGroup();


            //EditorGUILayout.Space();
        }

        void GUI_AngelRing(Material material)
        {
            var angelRingEnabled = GUI_Toggle(material, "Angel Ring Projection", ShaderPropAngelRing, MaterialGetInt(material, ShaderPropAngelRing) != 0);
            EditorGUI.BeginDisabledGroup(!angelRingEnabled);
            {
                m_MaterialEditor.TexturePropertySingleLine(Styles.angelRingText, angelRing_Sampler, angelRing_Color);
                EditorGUI.indentLevel++;
                //m_MaterialEditor.TextureScaleOffsetProperty(angelRing_Sampler);
                m_MaterialEditor.RangeProperty(ar_OffsetU, "Offset U");
                m_MaterialEditor.RangeProperty(ar_OffsetV, "Offset V");

                GUI_Toggle(material, "Alpha channel as Clipping Mask", ShaderPropARSampler_AlphaOn, MaterialGetInt(material, ShaderPropARSampler_AlphaOn) != 0);

                EditorGUI.indentLevel--;

            }
            EditorGUI.EndDisabledGroup();
        }
        void ApplyQueueAndRenderType(UTS_Mode technique, Material material)
        {

            if (m_autoRenderQueue == 1)
            {
                 material.renderQueue = -1; //  (int)UnityEngine.Rendering.RenderQueue.Geometry;
            }

            const string OPAQUE = "Opaque";
            const string TRANSPARENTCUTOUT = "TransparentCutOut";
            const string TRANSPARENT = "Transparent";
            const string RENDERTYPE = "RenderType";
            const string IGNOREPROJECTION = "IgnoreProjection";
            const string DO_IGNOREPROJECTION = "True";
            const string DONT_IGNOREPROJECTION = "False";
            var renderType = OPAQUE;
            var ignoreProjection = DONT_IGNOREPROJECTION;

            if (transparencyEnabled == UTS_TransparentMode.On)
            {
                renderType = TRANSPARENT;
                ignoreProjection = DO_IGNOREPROJECTION;
            }
            else
            {
                switch (technique)
                {
                    case UTS_Mode.ThreeColorToon:
                        {
                            UTS_ClippingMode clippingMode = (UTS_ClippingMode)MaterialGetInt(material,ShaderPropClippingMode);
                            if (clippingMode == UTS_ClippingMode.Off)
                            {

                            }
                            else
                            {
                                renderType = TRANSPARENTCUTOUT;

                            }

                            break;
                        }
                    case UTS_Mode.ShadingGradeMap:
                        {
                            UTS_TransClippingMode transClippingMode = (UTS_TransClippingMode)MaterialGetInt(material,ShaderPropClippingMode);
                            if (transClippingMode == UTS_TransClippingMode.Off)
                            {
                            }
                            else
                            {
                                renderType = TRANSPARENTCUTOUT;

                            }

                            break;
                        }
                }

            }
            if (m_autoRenderQueue == 1)
            {
                SetReqnderQueueAuto(material);
            }
            else
            {
                material.renderQueue = m_renderQueue;
            }

            material.SetOverrideTag(RENDERTYPE, renderType);
            material.SetOverrideTag(IGNOREPROJECTION, ignoreProjection);
        }

        void SetReqnderQueueAuto(Material material)
        {
            var stencilMode = (UTS_StencilMode)MaterialGetInt(material,ShaderPropStencilMode);
            if (transparencyEnabled == UTS_TransparentMode.On)
            {
                material.renderQueue = (int)UnityEngine.Rendering.RenderQueue.Transparent;
            }
            else if (stencilMode == UTS_StencilMode.StencilMask)
            {
                material.renderQueue = (int)UnityEngine.Rendering.RenderQueue.AlphaTest - 1;
            }
            else if (stencilMode == UTS_StencilMode.StencilOut)
            {
                material.renderQueue = (int)UnityEngine.Rendering.RenderQueue.AlphaTest;
            }
            if (transparencyEnabled == UTS_TransparentMode.On)
            {
                material.renderQueue = (int)UnityEngine.Rendering.RenderQueue.Transparent;
            }
            else if (stencilMode == UTS_StencilMode.StencilMask)
            {
                material.renderQueue = (int)UnityEngine.Rendering.RenderQueue.AlphaTest - 1;
            }
            else if (stencilMode == UTS_StencilMode.StencilOut)
            {
                material.renderQueue = (int)UnityEngine.Rendering.RenderQueue.AlphaTest;
            }
        }
        void ApplyMatCapMode(Material material)
        {
            if (MaterialGetInt(material,ShaderPropClippingMode) == 0)
            {
                if (material.GetFloat(ShaderPropMatCap) == 1)
                    material.EnableKeyword(ShaderPropMatCap);
                else
                    material.DisableKeyword(ShaderPropMatCap);
            }
            else
            {
                material.DisableKeyword(ShaderPropMatCap);
            }
        }


        void ApplyAngelRing(Material material)
        {
            int angelRingEnabled = MaterialGetInt(material,ShaderPropAngelRing);
            if (angelRingEnabled == 0)
            {
                material.DisableKeyword(ShaderDefineANGELRING_ON);
                material.EnableKeyword(ShaderDefineANGELRING_OFF);
            }
            else
            {
                material.EnableKeyword(ShaderDefineANGELRING_ON);
                material.DisableKeyword(ShaderDefineANGELRING_OFF);

            }
        }

        void ApplyStencilMode(Material material)
        {
            UTS_StencilMode mode = (UTS_StencilMode)(MaterialGetInt(material,ShaderPropStencilMode));
            switch (mode)
            {
                case UTS_StencilMode.Off:
                    MaterialSetInt(material,ShaderPropStencilComp, (int)StencilCompFunction.Disabled);
                    MaterialSetInt(material, ShaderPropStencilOpPass, (int)StencilOperation.Keep);
                    MaterialSetInt(material, ShaderPropStencilOpFail, (int)StencilOperation.Keep);
                    break;
                case UTS_StencilMode.StencilMask:
                     MaterialSetInt(material, ShaderPropStencilComp, (int)StencilCompFunction.Always);
                    MaterialSetInt(material, ShaderPropStencilOpPass, (int)StencilOperation.Replace);
                    MaterialSetInt(material, ShaderPropStencilOpFail, (int)StencilOperation.Replace);
                    break;
                case UTS_StencilMode.StencilOut:
                    MaterialSetInt(material, ShaderPropStencilComp, (int)StencilCompFunction.NotEqual);
                    MaterialSetInt(material, ShaderPropStencilOpPass, (int)StencilOperation.Keep);
                    MaterialSetInt(material, ShaderPropStencilOpFail, (int)StencilOperation.Keep);

                    break;
            }



        }
        void ApplyClippingMode(Material material)
        {

            if (!isShadingGrademap)
            {


                material.DisableKeyword(ShaderDefineIS_TRANSCLIPPING_OFF);
                material.DisableKeyword(ShaderDefineIS_TRANSCLIPPING_ON);

                switch ((UTS_ClippingMode)MaterialGetInt(material,ShaderPropClippingMode))
                {
                    case UTS_ClippingMode.Off:
                        material.EnableKeyword(ShaderDefineIS_CLIPPING_OFF);
                        material.DisableKeyword(ShaderDefineIS_CLIPPING_MODE);
                        material.DisableKeyword(ShaderDefineIS_CLIPPING_TRANSMODE);
                        material.EnableKeyword(ShaderDefineIS_OUTLINE_CLIPPING_NO);
                        material.DisableKeyword(ShaderDefineIS_OUTLINE_CLIPPING_YES);
                        break;
                    case UTS_ClippingMode.On:
                        material.DisableKeyword(ShaderDefineIS_CLIPPING_OFF);
                        material.EnableKeyword(ShaderDefineIS_CLIPPING_MODE);
                        material.DisableKeyword(ShaderDefineIS_CLIPPING_TRANSMODE);
                        material.DisableKeyword(ShaderDefineIS_OUTLINE_CLIPPING_NO);
                        material.EnableKeyword(ShaderDefineIS_OUTLINE_CLIPPING_YES);
                        break;
                    default: // UTS_ClippingMode.TransClippingMode
                        material.DisableKeyword(ShaderDefineIS_CLIPPING_OFF);
                        material.DisableKeyword(ShaderDefineIS_CLIPPING_MODE);
                        material.EnableKeyword(ShaderDefineIS_CLIPPING_TRANSMODE);
                        material.DisableKeyword(ShaderDefineIS_OUTLINE_CLIPPING_NO);
                        material.EnableKeyword(ShaderDefineIS_OUTLINE_CLIPPING_YES);
                        break;
                }
            }
            else
            {


                material.DisableKeyword(ShaderDefineIS_CLIPPING_OFF);
                material.DisableKeyword(ShaderDefineIS_CLIPPING_MODE);
                material.DisableKeyword(ShaderDefineIS_CLIPPING_TRANSMODE);
                switch ((UTS_TransClippingMode)MaterialGetInt(material,ShaderPropClippingMode))
                {
                    case UTS_TransClippingMode.Off:
                        material.EnableKeyword(ShaderDefineIS_TRANSCLIPPING_OFF);
                        material.DisableKeyword(ShaderDefineIS_TRANSCLIPPING_ON);
                        break;
                    default:
                        material.DisableKeyword(ShaderDefineIS_TRANSCLIPPING_OFF);
                        material.EnableKeyword(ShaderDefineIS_TRANSCLIPPING_ON);
                        break;

                }

            }

        }
        void GUI_Emissive(Material material)
        {

            m_MaterialEditor.TexturePropertySingleLine(Styles.emissiveTexText, emissive_Tex, emissive_Color);
            m_MaterialEditor.TextureScaleOffsetProperty(emissive_Tex);

            int _EmissiveMode_Setting = MaterialGetInt(material,"_EMISSIVE");
            if ((int)EmissionMode.SimpleEmissive == _EmissiveMode_Setting)
            {
                m_emissionMode = EmissionMode.SimpleEmissive;
            }
            else if ((int)EmissionMode.EmissiveAnimation == _EmissiveMode_Setting)
            {
                m_emissionMode = EmissionMode.EmissiveAnimation;
            }
            EditorGUILayout.Space();

            EditorGUI.BeginChangeCheck();
            var label = "Emissive Animation";
            var ret = EditorGUILayout.Toggle("Emissive Animation", m_emissionMode != EmissionMode.SimpleEmissive);
            if (EditorGUI.EndChangeCheck())
            {
                m_MaterialEditor.RegisterPropertyChangeUndo(label);
                if (ret )
                {
                    material.SetFloat("_EMISSIVE", 1);
                    material.EnableKeyword("_EMISSIVE_ANIMATION");
                    material.DisableKeyword("_EMISSIVE_SIMPLE");
                }
                else
                {
                    material.SetFloat("_EMISSIVE", 0);
                    material.EnableKeyword("_EMISSIVE_SIMPLE");
                    material.DisableKeyword("_EMISSIVE_ANIMATION");
                }
            }



            EditorGUI.BeginDisabledGroup(m_emissionMode != EmissionMode.EmissiveAnimation);
            {
                EditorGUI.indentLevel++;


                m_MaterialEditor.FloatProperty(base_Speed, "Base Speed (Time)");

                if (!_SimpleUI)
                {
                    int mode = MaterialGetInt(material, ShaderPropIs_ViewCoord_Scroll);
                    EditorGUI.BeginChangeCheck();
                    mode = EditorGUILayout.Popup("Animation Mode", (int)mode, EmissiveScrollMode);
                    if (EditorGUI.EndChangeCheck())
                    {
                        m_MaterialEditor.RegisterPropertyChangeUndo("Emissive Scroll Mode");
                        MaterialSetInt(material, ShaderPropIs_ViewCoord_Scroll, mode);
                    }
                }


                m_MaterialEditor.RangeProperty(scroll_EmissiveU, "Scroll U/X direction");
                m_MaterialEditor.RangeProperty(scroll_EmissiveV, "Scroll V/Y direction");
                m_MaterialEditor.FloatProperty(rotate_EmissiveUV, "Rotate around UV center");

                GUI_Toggle(material, "Ping-pong moves for base", ShaderPropIs_PingPong_Base, MaterialGetInt(material, ShaderPropIs_PingPong_Base) != 0);


                if (!_SimpleUI)
                {
                    EditorGUILayout.Space();

                    //GUILayout.Space(60);
                    var isColorShiftEnabled = GUI_Toggle(material, "Color shift with time", ShaderPropIs_ColorShift, MaterialGetInt(material, ShaderPropIs_ColorShift) != 0 );


                    EditorGUI.indentLevel++;
                    EditorGUI.BeginDisabledGroup(!isColorShiftEnabled);
                    {
                        m_MaterialEditor.ColorProperty(colorShift, "Destination Color");
                        m_MaterialEditor.FloatProperty(colorShift_Speed, "ColorShift Speed (Time)");
                    }
                    EditorGUI.EndDisabledGroup();

                    EditorGUI.indentLevel--;

                    EditorGUILayout.Space();

                    var isViewShiftEnabled = GUI_Toggle(material, "ViewShift of Color", ShaderPropIs_ViewShift, MaterialGetInt(material, ShaderPropIs_ViewShift) != 0);


                    EditorGUI.indentLevel++;
                    EditorGUI.BeginDisabledGroup(!isViewShiftEnabled);
                    m_MaterialEditor.ColorProperty(viewShift, "ViewShift Color");
                    EditorGUI.EndDisabledGroup();
                    EditorGUI.indentLevel--;
                }//!_SimpleUI
                EditorGUI.indentLevel--;
            }
            EditorGUI.EndDisabledGroup();
            EditorGUILayout.Space();

        }


        const string srpDefaultColorMask = "_SPRDefaultUnlitColorMask";
        const string srpDefaultCullMode = "_SRPDefaultUnlitColMode";

        internal static void SetupOverDrawTransparentObject(Material material)
        {
            var srpDefaultLightModeTag = material.GetTag("LightMode", false, srpDefaultLightModeName);
            if (srpDefaultLightModeTag == srpDefaultLightModeName)
            {
                material.SetShaderPassEnabled(srpDefaultLightModeName, true);
                MaterialSetInt(material, srpDefaultColorMask, 0);
                MaterialSetInt(material, srpDefaultCullMode, (int)CullingMode.BackCulling);
            }
        }
        internal static void SetupOutline(Material material)
        {
            var srpDefaultLightModeTag = material.GetTag("LightMode", false, srpDefaultLightModeName);
            if (srpDefaultLightModeTag == srpDefaultLightModeName)
            {
                MaterialSetInt(material, srpDefaultColorMask, 15);
                MaterialSetInt(material,srpDefaultCullMode, (int)CullingMode.FrontCulling);
            }
        }
        void GUI_Outline(Material material)
        {
            const string kDisableOutlineKeyword = "_DISABLE_OUTLINE";
            bool isLegacy = (srpDefaultLightModeName == "Always");

            var srpDefaultLightModeTag = material.GetTag("LightMode", false, srpDefaultLightModeName);
            bool isOutlineEnabled = true;
            if (srpDefaultLightModeTag == srpDefaultLightModeName)
            {
                const string kOutline = "Outline";
                isOutlineEnabled = material.GetShaderPassEnabled(srpDefaultLightModeName);
                EditorGUI.BeginChangeCheck();
                isOutlineEnabled = EditorGUILayout.Toggle(kOutline, isOutlineEnabled);
                if (EditorGUI.EndChangeCheck())
                {
                    m_MaterialEditor.RegisterPropertyChangeUndo(kOutline);
                    if (isOutlineEnabled)
                    {
                        if (isLegacy)
                        {
                            material.DisableKeyword(kDisableOutlineKeyword);
                        }

                        material.SetShaderPassEnabled(srpDefaultLightModeName, true);
                    }
                    else
                    {
                        if (isLegacy)
                        {
                            material.EnableKeyword(kDisableOutlineKeyword);
                        }
                        material.SetShaderPassEnabled(srpDefaultLightModeName, false);
                    }
                }
            }
            EditorGUI.BeginDisabledGroup(!isOutlineEnabled);
            //
            //Express Shader property [KeywordEnum(NML,POS)] by EumPopup.
            //Load the outline mode settings in the material.
            int _OutlineMode_Setting = MaterialGetInt(material,ShaderPropOutline);
            //Convert it to Enum format and store it in the offlineMode variable.

            if ((int)OutlineMode.NormalDirection == _OutlineMode_Setting)
            {
                m_outlineMode = OutlineMode.NormalDirection;
            }
            else if ((int)OutlineMode.PositionScaling == _OutlineMode_Setting)
            {
                m_outlineMode = OutlineMode.PositionScaling;
            }
            //GUI description with EnumPopup.
            m_outlineMode = (OutlineMode)EditorGUILayout.EnumPopup("Outline Mode", m_outlineMode);
            //If the value changes, write to the material.
            if (m_outlineMode == OutlineMode.NormalDirection)
            {
                material.SetFloat(ShaderPropOutline, 0);
                //The keywords on the UTCS_Outline.cginc side are also toggled around.
                material.EnableKeyword("_OUTLINE_NML");
                material.DisableKeyword("_OUTLINE_POS");
            }
            else if (m_outlineMode == OutlineMode.PositionScaling)
            {
                material.SetFloat(ShaderPropOutline, 1);
                material.EnableKeyword("_OUTLINE_POS");
                material.DisableKeyword("_OUTLINE_NML");
            }

            m_MaterialEditor.FloatProperty(outline_Width, "Outline Width");
            m_MaterialEditor.ColorProperty(outline_Color, "Outline Color");

            GUI_Toggle(material,"Blend BaseColor to Outline", ShaderPropIs_BlendBaseColor, MaterialGetInt(material, ShaderPropIs_BlendBaseColor) != 0);

            m_MaterialEditor.TexturePropertySingleLine(Styles.outlineSamplerText, outline_Sampler);
            m_MaterialEditor.FloatProperty(offset_Z, "Offset Outline with Camera Z-axis");

            if (!_SimpleUI)
            {
                EditorGUILayout.Space();
                //                _AdvancedOutline_Foldout = FoldoutSubMenu(_AdvancedOutline_Foldout, Styles.AdvancedOutlineFoldout);
                //                if (_AdvancedOutline_Foldout)
                {
                    EditorGUI.indentLevel++;
                    GUILayout.Label("    Camera Distance for Outline Width");
                    m_MaterialEditor.FloatProperty(farthest_Distance, "Farthest Distance to vanish");
                    m_MaterialEditor.FloatProperty(nearest_Distance, "Nearest Distance to draw with Outline Width");
                    EditorGUI.indentLevel--;

                    var useOutlineTexture =  GUI_Toggle(material, "Use Outline Texture", ShaderPropIs_OutlineTex, MaterialGetInt(material, ShaderPropIs_OutlineTex)!=0); ;
                    EditorGUI.BeginDisabledGroup(!useOutlineTexture);
                    m_MaterialEditor.TexturePropertySingleLine(Styles.outlineTexText, outlineTex);
                    EditorGUI.EndDisabledGroup();
                    EditorGUI.BeginDisabledGroup(m_outlineMode != OutlineMode.NormalDirection);
                    {
                        var isBackedNormal = GUI_Toggle(material, "Use Baked Normal for Outline", ShaderPropIs_BakedNormal, MaterialGetInt(material, ShaderPropIs_BakedNormal) != 0);
                        EditorGUI.BeginDisabledGroup(!isBackedNormal);
                        m_MaterialEditor.TexturePropertySingleLine(Styles.bakedNormalOutlineText, bakedNormal);
                        EditorGUI.EndDisabledGroup();
                    }
                    EditorGUI.EndDisabledGroup();
                }
                EditorGUI.EndDisabledGroup(); //!isOutlineEnabled
            }
            EditorGUILayout.Space();
        }

        void GUI_Tessellation(Material material)
        {
//            GUILayout.Label("Technique : DX11 Phong Tessellation", EditorStyles.boldLabel);
            m_MaterialEditor.RangeProperty(tessEdgeLength, "Edge Length");
            m_MaterialEditor.RangeProperty(tessPhongStrength, "Phong Strength");
            m_MaterialEditor.RangeProperty(tessExtrusionAmount, "Extrusion Amount");

            EditorGUILayout.Space();
        }



        void GUI_LightColorEffectiveness(Material material)
        {
            GUI_Toggle(material, "Base Color", ShaderPropIsLightColor_Base, MaterialGetInt(material, ShaderPropIsLightColor_Base)!= 0);
            GUI_Toggle(material, "1st Shading Color", ShaderPropIs_LightColor_1st_Shade, MaterialGetInt(material, ShaderPropIs_LightColor_1st_Shade) != 0);
            GUI_Toggle(material, "2nd Shading Color", ShaderPropIs_LightColor_2nd_Shade, MaterialGetInt(material, ShaderPropIs_LightColor_2nd_Shade) != 0);
            GUI_Toggle(material, "Highlight", ShaderPropIs_LightColor_HighColor, MaterialGetInt(material, ShaderPropIs_LightColor_HighColor) != 0);
            GUI_Toggle(material, "Rim Light", ShaderPropIs_LightColor_RimLight, MaterialGetInt(material, ShaderPropIs_LightColor_RimLight) != 0);
            GUI_Toggle(material, "Inversed Rim Light", ShaderPropIs_LightColor_Ap_RimLight, MaterialGetInt(material, ShaderPropIs_LightColor_Ap_RimLight) != 0);
            GUI_Toggle(material, "MatCap", ShaderPropIs_LightColor_MatCap, MaterialGetInt(material, ShaderPropIs_LightColor_MatCap) != 0);
            GUI_Toggle(material, "Outline", ShaderPropIs_LightColor_Outline, MaterialGetInt(material, ShaderPropIs_LightColor_Outline) != 0);

            EditorGUILayout.Space();
            m_MaterialEditor.RangeProperty(gi_Intensity, "GI Intensity");

            EditorGUI.BeginChangeCheck();
            var prop = ShaderPropIs_Filter_LightColor;
            var label = "Limit Light Intensity";
            var value = MaterialGetInt(material, prop);
            var ret = EditorGUILayout.Toggle(label, value != 0);
            if (EditorGUI.EndChangeCheck())
            {
                var boolValue = ret ? 1 : 0;
                m_MaterialEditor.RegisterPropertyChangeUndo(label);
                MaterialSetInt(material, prop, boolValue);
                if (boolValue != 0)
                {
                    MaterialSetInt(material, ShaderPropIs_Filter_LightColor, boolValue);
                    MaterialSetInt(material, ShaderPropIsLightColor_Base, boolValue);
                    MaterialSetInt(material, ShaderPropIs_LightColor_1st_Shade, boolValue);
                    MaterialSetInt(material, ShaderPropIs_LightColor_2nd_Shade, boolValue);
                    if (material.HasProperty(ShaderPropOutline))//If OUTLINE is available.
                    {
                        MaterialSetInt(material, ShaderPropIs_LightColor_Outline, boolValue);
                    }
                }
            }
            EditorGUILayout.Space();
        }

        void GUI_MetaverseSettings(Material material)
        {
            float isMetaverseLightEnabled = material.GetFloat(ShaderPropUnlit_Intensity);
            isMetaverseLightEnabled = GUI_Toggle(material, "Metaverse Light", ShaderPropUnlit_Intensity, isMetaverseLightEnabled != 0) ? 1: 0;
            EditorGUI.BeginDisabledGroup(isMetaverseLightEnabled == 0);
            {
                EditorGUI.indentLevel++;
                m_MaterialEditor.RangeProperty(unlit_Intensity, "Metaverse Light Intensity");
                var isBold = GUI_Toggle(material, "Metaverse Light Direction", ShaderPropIs_BLD, MaterialGetInt(material, ShaderPropIs_BLD) != 0);
                EditorGUI.BeginDisabledGroup(!isBold);

                EditorGUI.indentLevel++;
                m_MaterialEditor.RangeProperty(offset_X_Axis_BLD, "Offset X-Axis Direction");
                m_MaterialEditor.RangeProperty(offset_Y_Axis_BLD, "Offset Y-Axis Direction");

                GUI_Toggle(material, "Invert Z - Axis Direction", ShaderPropInverse_Z_Axis_BLD, MaterialGetInt(material, ShaderPropInverse_Z_Axis_BLD) != 0);

                EditorGUI.indentLevel--;
                EditorGUI.EndDisabledGroup();
                EditorGUI.indentLevel--;
            }
            EditorGUI.EndDisabledGroup();

        }

        public void DoPopup(GUIContent label, MaterialProperty property, string[] options)
        {
            DoPopup(label, property, options, m_MaterialEditor);
        }

        public static void DoPopup(GUIContent label, MaterialProperty property, string[] options, MaterialEditor materialEditor)
        {
            if (property == null)
                throw new System.ArgumentNullException("property");

            EditorGUI.showMixedValue = property.hasMixedValue;

            var mode = property.floatValue;

            EditorGUI.BeginChangeCheck();
            mode = EditorGUILayout.Popup(label, (int)mode, options);
            if (EditorGUI.EndChangeCheck())
            {
                materialEditor.RegisterPropertyChangeUndo(label.text);
                property.floatValue = mode;
            }

            EditorGUI.showMixedValue = false;
        }


    } 


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityEditor.Rendering.Toon
{
    internal struct GameRecommendation
    {
        public float ShaderPropIsLightColor_Base;
        public float ShaderPropIs_LightColor_1st_Shade;
        public float ShaderPropIs_LightColor_2nd_Shade;
        public float ShaderPropIs_LightColor_HighColor;
        public float ShaderPropIs_LightColor_RimLight;
        public float ShaderPropIs_LightColor_Ap_RimLight;
        public float ShaderPropIs_LightColor_MatCap;
        public float ShaderPropIs_LightColor_AR;
        public float ShaderPropIs_LightColor_Outline;
        public float ShaderPropSetSystemShadowsToBase;
        public float ShaderPropIsFilterHiCutPointLightColor;
        public float ShaderPropCameraRolling_Stabilizer;
        public float ShaderPropIs_Ortho;
        public float ShaderPropGI_Intensity;
        public float ShaderPropUnlit_Intensity;
        public float ShaderPropIs_Filter_LightColor;
    };

    public class UTS_GUIBase : UnityEditor.ShaderGUI
    {
        protected const string ShaderDefineSHADINGGRADEMAP = "_SHADINGGRADEMAP";
        protected const string ShaderDefineANGELRING_ON = "_IS_ANGELRING_ON";
        protected const string ShaderDefineANGELRING_OFF = "_IS_ANGELRING_OFF";
        protected const string ShaderDefineUTS_USE_RAYTRACING_SHADOW = "UTS_USE_RAYTRACING_SHADOW";
        protected const string ShaderPropAngelRing = "_AngelRing";
        protected const string ShaderPropRTHS = "_RTHS";
        protected const string ShaderPropMatCap = "_MatCap";
        protected const string ShaderPropClippingMode = "_ClippingMode";
        protected const string ShaderPropClippingMask = "_ClippingMask";
        protected const string ShaderPropSimpleUI = "_simpleUI";
        protected const string ShaderPropUtsTechniqe = "_utsTechnique";
        protected const string ShaderPropAutoRenderQueue = "_AutoRenderQueue";
        protected const string ShaderPropStencilMode = "_StencilMode";
        protected const string ShaderPropStencilNo = "_StencilNo";
        protected const string ShaderPropTransparentEnabled = "_TransparentEnabled";
        protected const string ShaderPropStencilComp = "_StencilComp";
        protected const string ShaderPropStencilOpPass = "_StencilOpPass";
        protected const string ShaderPropStencilOpFail = "_StencilOpFail";
        protected const string ShaderPropStencilWriteMask = "_StencilWriteMask";
        protected const string ShaderPropStencilReadMask = "_StencilReadMask";
        protected const string ShaderPropUtsVersionX = "_utsVersionX";
        protected const string ShaderPropUtsVersionY = "_utsVersionY";
        protected const string ShaderPropUtsVersionZ = "_utsVersionZ";
        protected const string ShaderPropOutline = "_OUTLINE";
        protected const string ShaderPropNormalMapToHighColor = "_Is_NormalMapToHighColor";
        protected const string ShaderPropIsNormalMapToRimLight = "_Is_NormalMapToRimLight";
        protected const string ShaderPropSetSystemShadowsToBase = "_Set_SystemShadowsToBase";
        protected const string ShaderPropIsFilterHiCutPointLightColor = "_Is_Filter_HiCutPointLightColor";
        protected const string ShaderPropInverseClipping = "_Inverse_Clipping";
        protected const string ShaderPropIsBaseMapAlphaAsClippingMask = "_IsBaseMapAlphaAsClippingMask";
        protected const string ShaderPropIsLightColor_Base = "_Is_LightColor_Base";
        protected const string ShaderPropCameraRolling_Stabilizer = "_CameraRolling_Stabilizer";
        protected const string ShaderPropIs_Ortho = "_Is_Ortho";
        protected const string ShaderPropGI_Intensity = "_GI_Intensity";
        protected const string ShaderPropUnlit_Intensity = "_Unlit_Intensity";
        protected const string ShaderPropIs_Filter_LightColor = "_Is_Filter_LightColor";
        protected const string ShaderPropIs_LightColor_1st_Shade = "_Is_LightColor_1st_Shade";
        protected const string ShaderPropIs_LightColor_2nd_Shade = "_Is_LightColor_2nd_Shade";
        protected const string ShaderPropIs_LightColor_HighColor = "_Is_LightColor_HighColor";
        protected const string ShaderPropIs_LightColor_RimLight = "_Is_LightColor_RimLight";
        protected const string ShaderPropIs_LightColor_Ap_RimLight = "_Is_LightColor_Ap_RimLight";
        protected const string ShaderPropIs_LightColor_MatCap = "_Is_LightColor_MatCap";
        protected const string ShaderPropIs_LightColor_AR = "_Is_LightColor_AR";
        protected const string ShaderPropIs_LightColor_Outline = "_Is_LightColor_Outline";
        protected const string ShaderPropInverse_MatcapMask = "_Inverse_MatcapMask";
        protected const string ShaderPropUse_BaseAs1st = "_Use_BaseAs1st";
        protected const string ShaderPropUse_1stAs2nd = "_Use_1stAs2nd";
        protected const string ShaderPropIs_NormalMapToBase = "_Is_NormalMapToBase";
        protected const string ShaderPropIs_ColorShift = "_Is_ColorShift";
        protected const string ShaderPropRimLight = "_RimLight";
        protected const string ShaderPropRimLight_FeatherOff = "_RimLight_FeatherOff";
        protected const string ShaderPropAp_RimLight_FeatherOff = "_Ap_RimLight_FeatherOff";
        protected const string ShaderPropIs_BlendAddToMatCap = "_Is_BlendAddToMatCap";
        protected const string ShaderPropARSampler_AlphaOn = "_ARSampler_AlphaOn";
        protected const string ShaderPropIs_UseTweakHighColorOnShadow = "_Is_UseTweakHighColorOnShadow";

        protected const string ShaderPropIs_SpecularToHighColor = "_Is_SpecularToHighColor";
        protected const string ShaderPropIs_BlendAddToHiColor = "_Is_BlendAddToHiColor";

        protected const string ShaderPropAdd_Antipodean_RimLight = "_Add_Antipodean_RimLight";
        protected const string ShaderPropLightDirection_MaskOn = "_LightDirection_MaskOn";

        protected const string ShaderProp1st_ShadeColor_Step = "_1st_ShadeColor_Step";
        protected const string ShaderPropBaseColor_Step = "_BaseColor_Step";
        protected const string ShaderProp1st_ShadeColor_Feather = "_1st_ShadeColor_Feather";
        protected const string ShaderPropBaseShade_Feather = "_BaseShade_Feather";
        protected const string ShaderProp2nd_ShadeColor_Step = "_2nd_ShadeColor_Step";
        protected const string ShaderPropShadeColor_Step = "_ShadeColor_Step";
        protected const string ShaderProp2nd_ShadeColor_Feather = "_2nd_ShadeColor_Feather";
        protected const string ShaderProp1st2nd_Shades_Feather = "_1st2nd_Shades_Feather";
        protected const string ShaderPropIs_NormalMapForMatCap = "_Is_NormalMapForMatCap";
        protected const string ShaderPropIs_UseTweakMatCapOnShadow = "_Is_UseTweakMatCapOnShadow";
        protected const string ShaderPropIs_ViewCoord_Scroll = "_Is_ViewCoord_Scroll";
        protected const string ShaderPropIs_PingPong_Base = "_Is_PingPong_Base";

        protected const string ShaderPropIs_ViewShift = "_Is_ViewShift";
        protected const string ShaderPropIs_BlendBaseColor = "_Is_BlendBaseColor";
        protected const string ShaderPropIs_OutlineTex = "_Is_OutlineTex";
        protected const string ShaderPropIs_BakedNormal = "_Is_BakedNormal";
        protected const string ShaderPropIs_BLD = "_Is_BLD";
        protected const string ShaderPropInverse_Z_Axis_BLD = "_Inverse_Z_Axis_BLD";

        protected const string ShaderDefineIS_OUTLINE_CLIPPING_NO = "_IS_OUTLINE_CLIPPING_NO";
        protected const string ShaderDefineIS_OUTLINE_CLIPPING_YES = "_IS_OUTLINE_CLIPPING_YES";

        protected const string ShaderDefineIS_CLIPPING_OFF = "_IS_CLIPPING_OFF";
        protected const string ShaderDefineIS_CLIPPING_MODE = "_IS_CLIPPING_MODE";
        protected const string ShaderDefineIS_CLIPPING_TRANSMODE = "_IS_CLIPPING_TRANSMODE";

        protected const string ShaderDefineIS_TRANSCLIPPING_OFF = "_IS_TRANSCLIPPING_OFF";
        protected const string ShaderDefineIS_TRANSCLIPPING_ON = "_IS_TRANSCLIPPING_ON";

        protected const string ShaderDefineIS_CLIPPING_MATTE = "_IS_CLIPPING_MATTE";
        protected const string STR_ONSTATE = "Active";
        protected const string STR_OFFSTATE = "Off";


        public enum _UTS_Technique
        {
            DoubleShadeWithFeather, ShadingGradeMap
        }

        public enum _UTS_ClippingMode
        {
            Off, On, TransClippingMode
        }

        public enum _UTS_TransClippingMode
        {
            Off, On,
        }
        public enum _UTS_Transparent
        {
            Off, On,
        }
        public enum _UTS_StencilMode
        {
            Off, StencilOut, StencilMask
        }

        public enum _StencilOperation
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

        public enum _StencilCompFunction
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



        public enum _OutlineMode
        {
            NormalDirection, PositionScaling
        }

        public enum _CullingMode
        {
            CullingOff, FrontCulling, BackCulling
        }

        public enum _EmissiveMode
        {
            SimpleEmissive, EmissiveAnimation
        }

        // variables which must be gotten from shader at the beggning of GUI
        public int _autoRenderQueue = 1;
        public int _renderQueue = (int)UnityEngine.Rendering.RenderQueue.Geometry;
        // variables which just to be held.
        public _OutlineMode outlineMode;
        public _CullingMode cullingMode;
        public _EmissiveMode emissiveMode;


        //Button sizes
        static internal GUILayoutOption[] longButtonStyle = new GUILayoutOption[] { GUILayout.Width(180) };
        static internal GUILayoutOption[] shortButtonStyle = new GUILayoutOption[] { GUILayout.Width(130) };
        static internal GUILayoutOption[] middleButtonStyle = new GUILayoutOption[] { GUILayout.Width(130) };
        static internal GUILayoutOption[] toggleStyle = new GUILayoutOption[] { GUILayout.Width(130) };

        //
        protected static _UTS_Transparent _Transparent_Setting;
        protected static int _StencilNo_Setting;
        protected static bool _OriginalInspector = false;
        protected static bool _SimpleUI = false;
        //For display messages
        protected bool _Use_GameRecommend = false;

        internal GameRecommendation _GameRecommendationStore;
        //Initial values of Foldout.
        protected static bool _BasicShaderSettings_Foldout = false;
        protected static bool _BasicThreeColors_Foldout = true;
        protected static bool _NormalMap_Foldout = false;
        protected static bool _ShadowControlMaps_Foldout = false;
        protected static bool _StepAndFeather_Foldout = true;
        protected static bool _AdditionalLookdevs_Foldout = false;
        protected static bool _HighColor_Foldout = true;

        protected static bool _RimLight_Foldout = true;
        protected static bool _MatCap_Foldout = true;
        protected static bool _AngelRing_Foldout = true;
        protected static bool _Emissive_Foldout = true;
        protected static bool _Outline_Foldout = true;
        protected static bool _AdvancedOutline_Foldout = false;
        protected static bool _Tessellation_Foldout = false;
        protected static bool _LightColorContribution_Foldout = false;
        protected static bool _AdditionalLightingSettings_Foldout = false;

        // -----------------------------------------------------
        //Specify only those that use the m_MaterialEditor method as their UI.
        // UTS2 materal properties -------------------------
        protected MaterialProperty utsTechnique = null;
        protected MaterialProperty transparentMode = null;
        protected MaterialProperty clippingMode = null;
        protected MaterialProperty clippingMask = null;
        protected MaterialProperty clipping_Level = null;
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

    }


}

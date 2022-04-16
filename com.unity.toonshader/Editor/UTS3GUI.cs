//#define USE_SIMPLE_UI


using System;
using UnityEngine;
using UnityEngine.Rendering;

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
        internal const string ShaderPropInvert_MatcapMask = "_Inverse_MatcapMask";
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
            Hard, Soft
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
            Off, Frontface, Backface
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

        protected MaterialProperty stencilValue = null;

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



        protected MaterialProperty highColor_Tex = null;
        protected MaterialProperty highColor = null;

        protected MaterialProperty set_HighColorMask = null;
        protected MaterialProperty tweak_HighColorMaskLevel = null;

        protected MaterialProperty rimLight_Power = null;
        protected MaterialProperty ap_RimLight_Power = null;
        protected MaterialProperty set_RimLightMask = null;

        protected MaterialProperty matCap_Sampler = null;
        protected MaterialProperty matCapColor = null;
        protected MaterialProperty normalMapForMatCap = null;
        protected MaterialProperty bumpScaleMatcap = null;
        protected MaterialProperty set_MatcapMask = null;

        protected MaterialProperty angelRing_Sampler = null;
        protected MaterialProperty angelRing_Color = null;

        protected MaterialProperty emissive_Tex = null;
        protected MaterialProperty emissive_Color = null;
        protected MaterialProperty rotate_EmissiveUV = null;

        protected MaterialProperty outline_Sampler = null;
        protected MaterialProperty offset_Z = null;
        protected MaterialProperty outlineTex = null;
        protected MaterialProperty bakedNormal = null;

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



        void FindProperties(MaterialProperty[] props)
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

            stencilValue = FindProperty(ShaderPropStencilNo, props);

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


            highColor_Tex = FindProperty(ShaderProp_HighColor_Tex, props);
            highColor = FindProperty("_HighColor", props);

            set_HighColorMask = FindProperty(ShaderProp_Set_HighColorMask, props);
            tweak_HighColorMaskLevel = FindProperty("_Tweak_HighColorMaskLevel", props);


            set_RimLightMask = FindProperty(ShaderProp_Set_RimLightMask, props);

            matCap_Sampler = FindProperty(ShaderProp_MatCap_Sampler, props);
            matCapColor = FindProperty("_MatCapColor", props);

            normalMapForMatCap = FindProperty("_NormalMapForMatCap", props);
            bumpScaleMatcap = FindProperty("_BumpScaleMatcap", props);


            set_MatcapMask = FindProperty(ShaderProp_Set_MatcapMask, props);

            angelRing_Sampler = FindProperty("_AngelRing_Sampler", props, false);
            angelRing_Color = FindProperty("_AngelRing_Color", props, false);

            emissive_Tex = FindProperty("_Emissive_Tex", props);
            emissive_Color = FindProperty("_Emissive_Color", props);


            outline_Sampler = FindProperty(ShaderProp_Outline_Sampler, props, false);
            outlineTex = FindProperty(ShaderProp_OutlineTex, props, false);
            bakedNormal = FindProperty("_BakedNormal", props, false);



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


        class RangeProperty
        {
            internal GUIContent m_GuiContent;
            internal readonly string m_propertyName;
            internal float m_Min;
            internal float m_Max;

            internal RangeProperty(string label, string tooltip, string propName, float min, float max)
            {
                m_GuiContent = new GUIContent(label,tooltip + " The range is from " +  min + " to " + max + ".");
                m_propertyName = propName;
                m_Min = min;
                m_Max = max;
            }
        };

        class FloatProperty
        {
            internal GUIContent m_GuiContent;
            internal readonly string m_propertyName;
            internal float m_defaultValue;
            internal FloatProperty(string label, string tooltip, string propName, float defaultValue)
            {
                m_GuiContent = new GUIContent(label, tooltip + " The default value is " + defaultValue + ".");
                m_propertyName = propName;
                m_defaultValue = defaultValue;
            }
        }

        class ColorProperty
        {
            internal GUIContent m_GuiContent;
            internal readonly string m_propertyName;
            internal bool m_isHDR;

            internal ColorProperty(string label, string tooltip, string propName, bool isHDR)
            {
                m_GuiContent = new GUIContent(label, tooltip );
                m_propertyName = propName;
                m_isHDR = isHDR;
            }
        }



        // --------------------------------
        //Specify only those that use the m_MaterialEditor method as their UI. For specifying textures and colors on a single line.
        private static class Styles
        {
            public static readonly GUIContent shaderFoldout = EditorGUIUtility.TrTextContent("Shader Settings", "Basic shader settings");
            public static readonly GUIContent basicColorFoldout = EditorGUIUtility.TrTextContent("Three Color and Control Map Settings", "Basic 3 color and controlling map Settings");
            public static readonly GUIContent shadingStepAndFeatherFoldout = EditorGUIUtility.TrTextContent("Shading Step and Feather Settings", "Basic 3 color step and feather settings.");
            public static readonly GUIContent highlightFoldout = EditorGUIUtility.TrTextContent("Highlight Settings", "Highlight  settings. Such as power, show or hide, light shape and so on.");
            public static readonly GUIContent rimLightFoldout = EditorGUIUtility.TrTextContent("Rim Light Settings", "Rim Light Settings. Such as color, direction, inversed rim light and so on.");
            public static readonly GUIContent matCapFoldout = EditorGUIUtility.TrTextContent("Material Capture (MatCap) Settings", "MatCap settings. Sphere maps for metallic or unusual expressions.");
            public static readonly GUIContent angelRingFoldout = EditorGUIUtility.TrTextContent("Angel Ring Projection Settings", "Angel ring projection settings. A kind of specular specialized for hairs.");
            public static readonly GUIContent emissionFoldout = EditorGUIUtility.TrTextContent("Emission Settings", "Emission settings. Textures, animations and so on.");
            public static readonly GUIContent outlineFoldout = EditorGUIUtility.TrTextContent("Outline Settings", "Outline settings. Such as width, colors and so on.");
            public static readonly GUIContent tessellationFoldout = EditorGUIUtility.TrTextContent("Tessellation Settings", "Tessellation settings for DX11, DX12 and Mac  Metal.");
            public static readonly GUIContent maskRenderingFoldout = EditorGUIUtility.TrTextContent("Mask Rendering Settings", "Mask rendering setting, controlled by Visual Compositor.");
            public static readonly GUIContent lightColorEffectivenessFoldout = EditorGUIUtility.TrTextContent("Scene Light Effectiveness Settings", "Scene light effectiveness to each parameter.");

            public static readonly GUIContent metaverseSettingsFoldout = EditorGUIUtility.TrTextContent("Metaverse Settings (Experimental)", "Default directional light when no directional lights are in the scene.");
            public static readonly GUIContent normalMapFoldout = EditorGUIUtility.TrTextContent("NormalMap Settings", "NormalMap settings.");
            public static readonly GUIContent shadowControlMapFoldout = EditorGUIUtility.TrTextContent("Shadow Control Maps", "Shadow control map settings. Such as positions and highlight filtering.");
            public static readonly GUIContent pointLightFoldout = EditorGUIUtility.TrTextContent("Point Light Settings", "Point light settings. Such as filtering and step offset.");

            public static readonly GUIContent baseColorText = new GUIContent("Base Map", "Base Color : Texture(sRGB) × Color(RGB) Default:White");
            public static readonly GUIContent firstShadeColorText = new GUIContent("1st Shading Map", "1st Shading color : Texture(sRGB) × Color(RGB) Default:White");
            public static readonly GUIContent secondShadeColorText = new GUIContent("2nd Shading Map", "2nd Shading color : Texture(sRGB) × Color(RGB) Default:White");
            public static readonly GUIContent normalMapText = new GUIContent("Normal Map", "Normal Map : Texture(bump)");
            public static readonly GUIContent highColorText = new GUIContent("Highlight", "Highlight : Texture(sRGB) × Color(RGB) Default:White");
            public static readonly GUIContent highColorMaskText = new GUIContent("Highlight Mask", "Highlight Mask : Texture(linear)");
            public static readonly GUIContent rimLightMaskText = new GUIContent("Rim Light Mask", "Rim Light Mask : Texture(linear)");
            public static readonly GUIContent matCapSamplerText = new GUIContent("MatCap Map", "MatCap Color : Texture(sRGB) × Color(RGB) Default:White");
            public static readonly GUIContent matCapMaskText = new GUIContent("MatCap Mask", "MatCap Mask : Texture(linear)");
            public static readonly GUIContent angelRingText = new GUIContent("Angel Ring", "Angel Ring : Texture(sRGB) × Color(RGB) Default:Black.");
            public static readonly GUIContent emissiveTexText = new GUIContent("Emission Map", "Emission : Texture(sRGB)× Emission mask(alpha) × Color(HDR) Default:Black");
            public static readonly GUIContent shadingGradeMapText = new GUIContent("Shading Grade Map", "Specify shadow-prone areas in UV coordinates. Shading Grade Map : Texture(linear)");
            public static readonly GUIContent firstPositionMapText = new GUIContent("1st Shading Position Map", "Specify the position of fixed shadows that falls in 1st shade color areas in UV coordinates. 1st Position Map : Texture(linear)");
            public static readonly GUIContent secondPositionMapText = new GUIContent("2nd Shading Position Map", "Specify the position of fixed shadows that falls in 2nd shade color areas in UV coordinates. 2nd Position Map : Texture(linear)");
            public static readonly GUIContent outlineSamplerText = new GUIContent("Outline Width Map", "Outline Width Map as a Grayscale Texture : Texture(linear).");
            public static readonly GUIContent outlineTexText = new GUIContent("Outline Color Map", "Outline texture : Texture(sRGB) Default:White");
            public static readonly GUIContent bakedNormalOutlineText = new GUIContent("Baked NormalMap for Outline", "Unpacked Normal Map : Texture(linear) .Note that this is not a standard NORMAL MAP.");
            public static readonly GUIContent clippingMaskText = new GUIContent("Clipping Mask", "Clipping Mask : Texture(linear)");

            public static readonly GUIContent specularModeText = new GUIContent("Specular Mode", "Specular light mode. Hard or Soft.");
            public static readonly GUIContent specularBlendModeText = new GUIContent("Color Blending Mode", "Specular color blending mode. Multiply or Additive.");
            public static readonly GUIContent matcapBlendModeText = new GUIContent("Color Blending Mode", "MatCap color blending mode. Multiply or Additive.");
            public static readonly GUIContent matcapOrthoText = new GUIContent("MatCap Camera Mode", "MatCap camera mode. Perspective or Orthographic.");
            public static readonly GUIContent transparentModeText = new GUIContent("Transparency",   "Transparency  mode that fits you. ");
            public static readonly GUIContent stencilVauleText = new GUIContent("Stencil Value","Stencil value that should be written to the stencil buffer.");
            public static readonly GUIContent workflowModeText = new GUIContent("Mode", "Select the mode that fits your purpose. Choose between Standard or With Additional Control Maps.");

            // -----------------------------------------------------
            public static readonly GUIContent clippingmodeModeText0 = new GUIContent("Clipping", "Select clipping mode that fits your purpose.");
            public static readonly GUIContent clippingmodeModeText1 = new GUIContent("Trans Clipping", "Select trans clipping mode that fits your purpose. ");
            public static readonly GUIContent stencilmodeModeText = new GUIContent("Stencil", "Select stencil mode that fits your purpose. ");
            public static readonly GUIContent cullingModeText = new GUIContent("Culling Mode", "Culling mode that fits your purpose. ");

            // ----------------------------------------------------- for GUI Toggles
            public static readonly GUIContent autoRenderQueueText = new GUIContent("Auto Render Queue", "When enabled, reqndering order is determined by system automatically.");
            public static readonly GUIContent renderQueueText = new GUIContent("Render Queue", "Rendering order in the scene.");
            public static readonly GUIContent invertClippingMaskText = new GUIContent("Invert Clipping Mask", "Invert clipping mask results.");
            public static readonly GUIContent baseMapAlphaAsClippingMask = new GUIContent("Use Base Map Alpha as Clipping Mask", "Use Base Map Alpha as Clipping Mask instead of Clipping mask texture.");
            public static readonly GUIContent applyTo1stShademapText = new GUIContent("Apply to 1st shading map", "Apply Base map to the 1st shading map.");
            public static readonly GUIContent applyTo2ndShademapText = new GUIContent("Apply to 2nd shading map", "Apply Base map or the 1st shading map to the 2st shading map.");
            public static readonly GUIContent threeBasicColorToNormalmapText = new GUIContent("3 Basic Colors", "Normal map effectiveness to 3 Basic color areas, lit, the 1st shading and the 2nd.");
            public static readonly GUIContent highLightToNormalmapText = new GUIContent("Highlight", "Normal map effectiveness to high lit areas.");
            public static readonly GUIContent rimlightToNormalmapText = new GUIContent("Rim Light", "Normal map effectiveness to rim lit areas.");
            public static readonly GUIContent receiveShadowText = new GUIContent("Receive Shadows", "Receive shadows on the material.");
            public static readonly GUIContent filterPointLightText = new GUIContent("Filter Point Light Highlights", "Show or hide highlight of point lights.");
            public static readonly GUIContent highlightOnShadowText = new GUIContent("Highlight Blending on Shadows", "Highlights in shadow areas are blend by bellow.");

            public static readonly GUIContent lightColorEffectivinessToBaseColorText  = new GUIContent("Base Color", "Light color effectiveness to the base color areas.");
            public static readonly GUIContent lightColorEffectivinessTo1stShadingText = new GUIContent("1st Shading Color", "Light color effectiveness to the 1st shading color areas.");
            public static readonly GUIContent lightColorEffectivinessTo2ndShadingText = new GUIContent("2nd Shading Color", "Light color effectiveness to the 2nd shading color areas.");
            public static readonly GUIContent lightColorEffectivinessToHighlitText    = new GUIContent("Highlight", "Light color effectiveness to high lit areas.");
            public static readonly GUIContent lightColorEffectivinessToRimlitText     = new GUIContent("Rim Light", "Light color effectiveness to rim lit areas.");
            public static readonly GUIContent lightColorEffectivinessToInvRimlitText  = new GUIContent("Inversed Light Direciton Rim Light", "Light color effectiveness to inverted direction rim lit areas.");
            public static readonly GUIContent lightColorEffectivinessToMatCapText = new GUIContent("MatCap", "Light color effectiveness to MatCap areas.");
            public static readonly GUIContent lightColorEffectivinessToOutlineText = new GUIContent("Outline", "Light color effectiveness to outlines.");
            public static readonly GUIContent rimlightText = new GUIContent("Rim Light", "Enable/Disable Rim Light.");
            public static readonly GUIContent rimlightFeatherText = new GUIContent("Rim Light Feather Off", "Disable Rim light feather.");
            public static readonly GUIContent rimlightDirectionMaskText = new GUIContent("Light Direction", "When Enabled, rim light is generated only in the direction of the light source.");
            public static readonly GUIContent inversedRimlightText = new GUIContent("Inversed Direciton Rim Light", "Rim light from inversed/antipodean direction.");
            public static readonly GUIContent camearRollingStabilizerText = new GUIContent("Stabilize Camera rolling", "Stablize Camera rolling when capturing materials with camera.");
            public static readonly GUIContent inversedRimlightFeatherText = new GUIContent("Inversed Rim Light Feather Off", "Disable Inversed Rim light feather.");
            public static readonly GUIContent matCapText = new GUIContent("MatCap", "Enable/Disable MatCap (Material Capture)");
            public static readonly GUIContent matCapNormalmapSpecularaMask = new GUIContent("NormalMap Specular Mask for MatCap", "If Enabled, gives a normal map specifically for MatCap.If you are using MatCap as speculum lighting, you can use this to mask it.");
            public static readonly GUIContent matCapOnShadow = new GUIContent("MatCap Blending on Shadows", "Adjusts the blending rate of the MatCap range in shadows.");
            public static readonly GUIContent invertMatCapMaskText = new GUIContent("Invert MatCap Mask","When enabled, MatCap Mask Texture is inversed.");

            public static readonly GUIContent angelRingProjectionText = new GUIContent("Angel Ring Projection", "Enable/Disable Angel Ring Projection.");
            public static readonly GUIContent angelRingAlphaAdClippingMaskText = new GUIContent("Alpha Channel as Clipping Mask", "Texture alpha channel is used for clipping mask. If disabled, alpha does not affect at all.");
            public static readonly GUIContent pingpongMoveText = new GUIContent("Ping-pong moves for base", "When enabled, you can set PingPong (back and forth) in the direction of the animation.");
            public static readonly GUIContent colorShitWithTimeText = new GUIContent("Color Shifting with Time", "The color that is multiplied by the Emissive texture is changed by linear interpolation (Lerp) toward the Destination Color.");
            public static readonly GUIContent colorShiftWithViewAngle = new GUIContent("Color Shifting with View Angle", "Emissive color shifts in accordance with view angle.");

            public static readonly GUIContent baseColorToOtulineText = new GUIContent("Blend Base Color to Outline","Base Color is blended into outline color.");
            public static readonly GUIContent outlineColorMapText = new GUIContent("Outline Color Map", "Apply a texture as outline color map.");
            public static readonly GUIContent bakedNormalForOutlineText = new GUIContent("Baked Normalmap", "Apply a Normalmap texture for Outline.");
            public static readonly GUIContent metaverseLightText = new GUIContent("Metaverse Light","UTS requires at least one directional light, Some Metaverse scenes,however does not. In such case this feature is helpful.");
            public static readonly GUIContent metaverseLightDirectionText = new GUIContent("Metaverse Light Direction", "Drection of above.");
            public static readonly GUIContent invertZaxisDirection = new GUIContent("Invert Z-Axis Direction", "Invert Metaverse light Z-Axis Direction.");
            public static readonly GUIContent emissiveScrollAnimationModeText = new GUIContent("Animation Mode", "Emissive Animation Mode.");

            public static readonly GUIContent outlineModeText = new GUIContent("Outline Mode", "Specifies how the inverted-outline object will be spawned. You can choose between Normal Direction（normal inverted method） / Position Scalling（position scaling method). In most cases, Normal Direction is used but if it is a mesh that is only made of hard edges (such as cubes), Position Scalling will prevent the outline from being disconnected.");
            // Range properties
            public static readonly RangeProperty metaverseRangePropText = new RangeProperty(
                "Metaverse Light Intensity", 
                "Light intensity when no directional lights in the scene.",
                ShaderPropUnlit_Intensity,  0.0f, 4.0f);
            public static readonly RangeProperty metaverseOffsettXaxisText = new RangeProperty(
                "Offset X-Axis Direction", "Moves  Metaverse light direction horizontally.",
                "_Offset_X_Axis_BLD", -1.0f,1.0f);
            public static readonly RangeProperty metaverseOffsettYaxisText = new RangeProperty(
                "Offset Y-Axis Direction", "Moves  Metaverse light direction vertically.",
                "_Offset_Y_Axis_BLD", -1.0f, 1.0f);
            public static readonly RangeProperty tweakTransParencyText = new RangeProperty(
                "Transparency Level", "Adjusts the transparency by considering the grayscale level of the clipping mask as an alpha value.",
                "_Tweak_transparency", -1.0f, 1.0f);
            public static readonly RangeProperty clippingLevelText = new RangeProperty(
                "Clipping Level", "Specifies the strength of the clipping mask.",
                "_Clipping_Level", 0.0f, 1.0f);
            public static readonly RangeProperty scrollEmissiveUText = new RangeProperty(
                "Scroll U/X direction", "Specifies how much the Emissive texture should scroll in the u-direction (x-axis direction) when updating the animation. The range is -1 to 1, defaulting to 0. The scrolling animation is ultimately determined by Base Speed (Time) x Scroll U Direction x Scroll V Direction.",
                "_Scroll_EmissiveU", -1.0f, 1.0f);
            public static readonly RangeProperty scrollEmissiveVText = new RangeProperty(
                "Scroll V/Y direction", "Specifies how much the Emissive texture should scroll in the u-direction (y-axis direction) when updating the animation. The range is -1 to 1, defaulting to 0. The scrolling animation is ultimately determined by Base Speed (Time) x Scroll U Direction x Scroll V Direction.",
                "_Scroll_EmissiveV", -1.0f, 1.0f);
            public static readonly RangeProperty tweakHighColorOnShadowText = new RangeProperty(
                "Blending Level", "Adjusts the intensity of highlight applied to shadow areas.",
                "_TweakHighColorOnShadow", 0, 1);
            public static readonly RangeProperty tweakMatCapOnShadowText = new RangeProperty(
                "Blending Level", "Adjusts the intensity of MatCap applied to shadow areas.",
                "_TweakMatCapOnShadow", 0, 1);
            public static readonly RangeProperty tweakSystemShadowLevelText = new RangeProperty(
                "System Shadow Level", "Adjsuts System Shadows.",
                "_Tweak_SystemShadowsLevel",-0.5f, 0.5f);

            public static readonly RangeProperty shaderPropBaseColorText = new RangeProperty(
                "Base Color Step", "Sets the boundary between the Base Color and the Shade Colors.",
                ShaderPropBaseColor_Step, 0, 1 );
            public static readonly RangeProperty shaderPropBaseFeatherText = new RangeProperty(
                "Base Shading Feather", "Feathers the boundary between the Base Color and the Shade Colors..",
                ShaderPropBaseShade_Feather, 0.0001f, 1);
            public static readonly RangeProperty shaderPropShadeColorStepText = new RangeProperty(
                "Shading Color Step", "Sets the boundary between the 1st and 2nd Shade Colors. Set this to 0 if no 2nd Shade Color is used.",
                ShaderPropShadeColor_Step, 0, 1);
            public static readonly RangeProperty shaderProp1st2nd_Shades_FeatherText = new RangeProperty(
                "1st/2nd Shading Feather", "Feathers the boundary between the 1st and 2nd Shade Colors.",
                ShaderProp1st2nd_Shades_Feather, 0.0001f, 1);

            public static readonly RangeProperty shaderProp1st_ShadeColor_StepText = new RangeProperty(
                "1st Shade Color Step", "Sets the step between the Base color and 1st Shade Color, the same as the BaseColor_Step property..",
                ShaderProp1st_ShadeColor_Step, 0, 1);
            public static readonly RangeProperty shaderProp1st_ShadeColor_FeatherText = new RangeProperty(
                "1st Shade Color Feather", "Feathers the boundary between the Base Color and the 1st Shade Color, the same as the Base/Shade_Feather property.",
                ShaderProp1st_ShadeColor_Feather, 0.0001f, 1);
            public static readonly RangeProperty shaderProp2nd_ShadeColor_StepText = new RangeProperty(
                "2nd Shade Color Step", "Sets the step between the 1st and 2nd Shade Colors, the same as the ShadeColor_Step property.",
                ShaderProp2nd_ShadeColor_Step, 0, 1);
            public static readonly RangeProperty shaderProp2nd_ShadeColor_FeatherText = new RangeProperty(
                "2nd Shade Color Feather", "Feathers the boundary between the 1st and 2nd Shade Colors, the same as the 1st/2nd_Shades_Feather properties.",
                ShaderProp2nd_ShadeColor_Feather, 0.0001f, 1);

            public static readonly RangeProperty shaderPropStepOffsetText = new RangeProperty(
                "Step Offset", "Fine tunes light steps (boundaries) added in the ForwardAdd pass, such as real-time point lights.",
                "_StepOffset", -0.5f, 0.5f);
            public static readonly RangeProperty shaderPropHilightPowerText = new RangeProperty(
                "Highlight Power", "Highlight power factor, pow(x,5) is used inside the shader.",
                "_HighColor_Power", 0, 1);

            public static readonly RangeProperty hilightMaskLevelText = new RangeProperty(
                "Highlight Mask Level", "Highlight mask texture blending level to highlights.",
                "_Tweak_HighColorMaskLevel", -1, 1);

            public static readonly RangeProperty shadingGradeMapLevelText = new RangeProperty(
                "ShadingGradeMap Level", "Level-corrects the grayscale values in the Shading Grade Map.",
                "_Tweak_ShadingGradeMapLevel", -0.5f, 0.5f);

            public static readonly RangeProperty blureLevelSGMText = new RangeProperty(
                "ShadingGradeMap Blur Level", "The Mip Map feature is used to blur the Shading Grade Map; to enable Mip Map, turn on Advanced > Generate Mip Maps in the Texture Import Settings. The default is 0 (no blur).",
                "_BlurLevelSGM", 0, 10);

            public static readonly RangeProperty rimLightMaskLevelText = new RangeProperty(
                "Rim Light Mask Level", "TBD.",
                "_Tweak_RimLightMaskLevel", -1, 1);

            public static readonly RangeProperty lightDirectionMaskLevelText = new RangeProperty(
                "Light Direction Rim Light Level", "The Level of Rim Light toward a light direction.",
                "_Tweak_LightDirection_MaskLevel", 0f, 0.5f);

            public static readonly RangeProperty tweakMatCapUVText = new RangeProperty(
                "Scale MatCap UV", "Scaling UV of MatCap Map.",
                "_Tweak_MatCapUV", -0.5f, 0.5f);

            public static readonly RangeProperty rotateMatCapUVText = new RangeProperty(
                "Rotate MatCap UV", "Rotating UV of MatCap Map.",
                "_Rotate_MatCapUV", -1, 1);

            public static readonly RangeProperty matcapBlurLevelText = new RangeProperty(
                "MatCap Blur Level", "Blur MatCap_Sampler using the Mip Map feature; to enable Mip Map, turn on Advanced > Generate Mip Maps in the Texture Import Settings. Default is 0 (no blur).",
                "_BlurLevelMatcap", 0, 10);

            public static readonly RangeProperty arOffsetU_Text = new RangeProperty(
                "Offset U", "Adjusts the Angel Ring’s shape in the horizontal direction.",
                "_AR_OffsetU", 0, 0.5f);

            public static readonly RangeProperty arOffsetV_Text = new RangeProperty(
                "Offset V", "Adjusts the Angel Ring’s shape in the vertical direction.",
                "_AR_OffsetV", 0, 1);

            public static readonly RangeProperty legacyTessEdgeLengthText = new RangeProperty(
                "Edge Length", "Divides the tessellation according to the camera’s distance. The smaller the value, the smaller the tiles become.",
                "_TessEdgeLength", 2, 50);

            public static readonly RangeProperty legacyTessPhongStrengthText = new RangeProperty(
                "Phong Strength", "Adjusts the pulling strength of the surfaces divided by tessellation.",
                "_TessPhongStrength", 0, 1);

            public static readonly RangeProperty legacyTessExtrusionAmountText = new RangeProperty(
                "Extrusion Amount", "Scale the expanded parts due to tessellation.",
                "_TessExtrusionAmount", -0.005f, 0.005f);

            public static readonly RangeProperty rimLightPowerText = new RangeProperty(
                "Rim Light Level", "Specifies Rim Light level.",
                "_RimLight_Power", 0, 1);

            public static readonly RangeProperty inversedRimLightPowerText = new RangeProperty(
                "Inversed Rim Light Level", "Specifies Inversed/Antipodean Rim Light Level.",
                "_Ap_RimLight_Power", 0, 1);

            public static readonly RangeProperty giIntensityText = new RangeProperty(
                "GI Intensity", "TBD.",
                ShaderPropGI_Intensity, 0, 1);

            public static readonly RangeProperty tweakMatCapMaskLevelText = new RangeProperty(
                "MatCap Mask Level", "Adjusts the level of the MatcapMask. When the value is 1, MatCap is displayed 100% irrespective of whether or not there is a mask. When the value is -1, MatCap will not be displayed at all and MatCap will be the same as in the off state.",
                "_Tweak_MatcapMaskLevel", -1, 1);

            public static readonly RangeProperty rotate_NormalMapForMatCapUVText = new RangeProperty(
                "Rotate NormalMap UV", "Rotates the MatCap normal map UV based on its center.",
                "_Rotate_NormalMapForMatCapUV", -1, 1);

            public static readonly RangeProperty rimLight_InsideMaskText = new RangeProperty(
                "Adjust Rim Light Area", "Increasing this value narrows the area of influence of Rim Light.",
                "_RimLight_InsideMask", 0.0001f, 1);

            // Float properties
            public static readonly FloatProperty baseSpeedText = new FloatProperty(label: "Base Speed (Time)", 
                tooltip: "Specifies the base update speed of scroll animation. If the value is 1, it will be updated in 1 second. Specifying a value of 2 results in twice the speed of a value of 1, so it will be updated in 0.5 seconds.", 
                propName: "_Base_Speed", defaultValue: 0);

            public static readonly FloatProperty outlineWidthText = new FloatProperty(label: "Outline Width",
                tooltip: "Specifies the width of the outline. NOTICE: This value relies on the scale when the model was imported to Unity which means that you have to be careful if the scale is not 1.",
                propName: "_Outline_Width", defaultValue: 0);

            public static readonly FloatProperty farthestDistanceText = new FloatProperty(label: "Farthest Distance to vanish",
                tooltip: "Specify the furthest distance, where the outline width changes with the distance between the camera and the object. The outline will be zero at this distance.",
                propName: "_Farthest_Distance", defaultValue: 100);

            public static readonly FloatProperty nearestDistanceText = new FloatProperty(label: "Nearest Distance to draw with Outline Width",
                tooltip: "Specify the closest distance, where the outline width changes with the distance between the camera and the object. At this distance, the outline will be the maximum width set by Outline_Width.",
                propName: "_Nearest_Distance", defaultValue: 0.5f);

            public static readonly FloatProperty rotateEmissiveUVText = new FloatProperty(label: "Rotate around UV center",
                tooltip: "When Base Speed=1, the Emissive texture will rotate clockwise by 1. When combined with scrolling, rotation will occur after scrolling.",
                propName: "_Rotate_EmissiveUV", defaultValue: 0);

            public static readonly FloatProperty offsetZText = new FloatProperty(label: "Offset Outline with Camera Z-axis",
                tooltip: "Offsets the outline in the depth (Z) direction of the camera. In the case of a spike-shaped hairdo, etc., adding a positive value makes the outline less likely to be applied to the spike area. Normally, leave the value at 0.",
                propName: "_Offset_Z", defaultValue: 0);

            public static readonly FloatProperty colorShiftSpeedText = new FloatProperty(label: "Color Shifting Speed (Time)",
                tooltip: "Sets the reference speed for color shift. When the value is 1, one cycle should take approximately 6 seconds.",
                propName: "_ColorShift_Speed", defaultValue: 0);

            // Color prperties
            public static readonly ColorProperty viewShiftText = new ColorProperty(label: "Shifting Target Color",
                tooltip: "Target color above, must be specified in HDR.",
                propName: "_ViewShift", isHDR: true );

            public static readonly ColorProperty colorShiftText = new ColorProperty(label: "Destination Color",
                tooltip: "Destination color above, must be specified in HDR.",
                propName: "_ColorShift", isHDR: true);

            public static readonly ColorProperty rimLightColorText = new ColorProperty(label: "Rim Light Color",
                tooltip: "Specifies the color of rim light.",
                propName: "_RimLightColor", isHDR: false);

            public static readonly ColorProperty apRimLightColorText = new ColorProperty(label: "Inversed Rim Light Color",
                tooltip: "Specifies the color of inversed/antipodean rim light.",
                propName: "_Ap_RimLightColor", isHDR: false);

            public static readonly ColorProperty outlineColorText = new ColorProperty(label: "Outline Color",
                tooltip: "Specifies the color of outline.",
                propName: "_Outline_Color", isHDR: false);
        }
        // --------------------------------

        public UTS3GUI()
        {

        }

        public override void OnClosed(Material material)
        { 

            base.OnClosed(material);
        }
        
        void OnOpenGUI(Material material, MaterialEditor materialEditor, MaterialProperty[] props)
        {
            m_MaterialScopeList.RegisterHeaderScope(Styles.shaderFoldout, Expandable.Shader, DrawShaderOptions, (uint)UTS_Mode.ThreeColorToon, (uint)UTS_TransparentMode.Off);
            m_MaterialScopeList.RegisterHeaderScope(Styles.basicColorFoldout, Expandable.BasicColor, GUI_BasicThreeColors, (uint)UTS_Mode.ThreeColorToon,(uint)UTS_TransparentMode.Off);
            m_MaterialScopeList.RegisterHeaderScope(Styles.shadingStepAndFeatherFoldout, Expandable.BasicLookDevs, GUI_StepAndFeather, (uint)UTS_Mode.ThreeColorToon, (uint)UTS_TransparentMode.Off);
            m_MaterialScopeList.RegisterHeaderScope(Styles.highlightFoldout, Expandable.HighLight, GUI_HighlightSettings, (uint)UTS_Mode.ThreeColorToon, (uint)UTS_TransparentMode.Off);
            m_MaterialScopeList.RegisterHeaderScope(Styles.rimLightFoldout, Expandable.RimLight, GUI_RimLight, (uint)UTS_Mode.ThreeColorToon, (uint)UTS_TransparentMode.Off);
            m_MaterialScopeList.RegisterHeaderScope(Styles.matCapFoldout, Expandable.MatCap, GUI_MatCap, (uint)UTS_Mode.ThreeColorToon, (uint)UTS_TransparentMode.Off);
            m_MaterialScopeList.RegisterHeaderScope(Styles.angelRingFoldout, Expandable.AngelRing, GUI_AngelRing, (uint)UTS_Mode.ShadingGradeMap, (uint)UTS_TransparentMode.Off);
            m_MaterialScopeList.RegisterHeaderScope(Styles.emissionFoldout, Expandable.Emission, GUI_Emissive, (uint)UTS_Mode.ThreeColorToon, (uint)UTS_TransparentMode.Off);
            m_MaterialScopeList.RegisterHeaderScope(Styles.outlineFoldout, Expandable.Outline, GUI_Outline, (uint)UTS_Mode.ThreeColorToon, (uint)UTS_TransparentMode.On);
            if (material.HasProperty("_TessEdgeLength") && currentRenderPipeline == RenderPipeline.Legacy)
            { 
                m_MaterialScopeList.RegisterHeaderScope(Styles.tessellationFoldout, Expandable.Tessellation, GUI_Tessellation, (uint)UTS_Mode.ThreeColorToon, (uint)UTS_TransparentMode.Off);
            }
            else if (tessellationMode != null && currentRenderPipeline == RenderPipeline.HDRP)
            {
                m_MaterialScopeList.RegisterHeaderScope(Styles.tessellationFoldout, Expandable.Tessellation, GUI_TessellationHDRP, (uint)UTS_Mode.ThreeColorToon, (uint)UTS_TransparentMode.Off);

            }
            // originally these were in simple UI
            m_MaterialScopeList.RegisterHeaderScope(Styles.lightColorEffectivenessFoldout, Expandable.LightColorEffectiveness, GUI_LightColorEffectiveness, (uint)UTS_Mode.ThreeColorToon, (uint)UTS_TransparentMode.Off);
            m_MaterialScopeList.RegisterHeaderScope(Styles.metaverseSettingsFoldout, Expandable.MetaverseSettings, GUI_MetaverseSettings, (uint)UTS_Mode.ThreeColorToon, (uint)UTS_TransparentMode.Off);
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
        // never called from the system??
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

                    DoPopup(Styles.clippingmodeModeText0, clippingMode, ClippingModeNames);
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

                    DoPopup(Styles.clippingmodeModeText1, clippingMode, System.Enum.GetNames(typeof(UTS_TransClippingMode)));
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
            DoPopup(Styles.workflowModeText, utsTechnique, UtsModeNames);
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


        float GUI_FloatProperty(Material material, FloatProperty floatProp)
        {
            float ret = material.GetFloat(floatProp.m_propertyName);
            EditorGUI.BeginChangeCheck();
            ret = EditorGUILayout.FloatField(floatProp.m_GuiContent, ret );

            if (EditorGUI.EndChangeCheck())
            {
                m_MaterialEditor.RegisterPropertyChangeUndo(floatProp.m_GuiContent.text);
                material.SetFloat(floatProp.m_propertyName, ret);
            }
            return ret;
        }

        Color GUI_ColorProperty(Material material, ColorProperty colorProp)
        {
            Color ret = material.GetColor(colorProp.m_propertyName);
            EditorGUI.BeginChangeCheck();

            ret = EditorGUILayout.ColorField(colorProp.m_GuiContent, ret, showEyedropper: true, showAlpha: true, hdr: colorProp.m_isHDR);

            if (EditorGUI.EndChangeCheck())
            {
                m_MaterialEditor.RegisterPropertyChangeUndo(colorProp.m_GuiContent.text);
                material.SetColor(colorProp.m_propertyName, ret);
            }
            return ret;
        }
        float GUI_RangeProperty(Material material, RangeProperty rangeProp)
        {
            return GUI_RangeProperty(material, rangeProp.m_GuiContent, rangeProp.m_propertyName, rangeProp.m_Min, rangeProp.m_Max);
        }
        float GUI_RangeProperty(Material material, GUIContent guiContent, string propName,  float min, float max )
        {
            float ret = material.GetFloat(propName);
            EditorGUI.BeginChangeCheck();
            ret = EditorGUILayout.Slider(guiContent, ret, min, max );

            if (EditorGUI.EndChangeCheck())
            {
                m_MaterialEditor.RegisterPropertyChangeUndo(guiContent.text);
                material.SetFloat( propName, ret);
            }
            return ret;
        }

        bool GUI_Toggle(Material material, GUIContent guiContent, string prop, bool value)
        {
            EditorGUI.BeginChangeCheck();
            var ret = EditorGUILayout.Toggle(guiContent, value);
            if (EditorGUI.EndChangeCheck())
            {
                m_MaterialEditor.RegisterPropertyChangeUndo(guiContent.text);
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

            m_autoRenderQueue = GUI_Toggle(material, Styles.autoRenderQueueText, ShaderPropAutoRenderQueue, m_autoRenderQueue == 1) ? 1 : 0;

            EditorGUILayout.EndHorizontal();
            EditorGUI.indentLevel++;
            EditorGUI.BeginDisabledGroup(m_autoRenderQueue == 1);
            m_renderQueue = (int)EditorGUILayout.IntField(Styles.renderQueueText, material.renderQueue);
            EditorGUI.EndDisabledGroup();
            EditorGUI.indentLevel--;
        }


        void GUI_SetCullingMode(Material material)
        {
            const string _CullMode = "_CullMode";
            int _CullMode_Setting = MaterialGetInt(material,_CullMode);
            //Convert it to Enum format and store it in the offlineMode variable.
            if ((int)CullingMode.Off == _CullMode_Setting)
            {
                m_cullingMode = CullingMode.Off;
            }
            else if ((int)CullingMode.Frontface == _CullMode_Setting)
            {
                m_cullingMode = CullingMode.Frontface;
            }
            else
            {
                m_cullingMode = CullingMode.Backface;
            }
            //GUI description with EnumPopup.
            m_cullingMode = (CullingMode)EditorGUILayout.EnumPopup(Styles.cullingModeText, m_cullingMode);
            //If the value changes, write to the material.
            if (_CullMode_Setting != (int)m_cullingMode)
            {
                switch (m_cullingMode)
                {
                    case CullingMode.Off:
                        MaterialSetInt(material,_CullMode, 0);
                        break;
                    case CullingMode.Frontface:
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
            DoPopup(Styles.transparentModeText, transparentMode, System.Enum.GetNames(typeof(UTS_TransparentMode)));


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

            DoPopup(Styles.stencilmodeModeText, stencilMode, StencilModeNames );


            EditorGUI.indentLevel++;
            int currentStencilValue = stencilNumberSetting;
            EditorGUI.BeginDisabledGroup((UTS_StencilMode)MaterialGetInt(material, ShaderPropStencilMode) == UTS_StencilMode.Off);
            EditorGUI.BeginChangeCheck();
            currentStencilValue = EditorGUILayout.IntSlider(Styles.stencilVauleText, stencilNumberSetting, 0, 255);
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

            GUI_Toggle(material, Styles.invertClippingMaskText, ShaderPropInverseClipping,MaterialGetInt(material, ShaderPropInverseClipping)!= 0 );
            GUI_RangeProperty(material, Styles.clippingLevelText);
        }

        void GUI_SetTransparencySetting(Material material)
        {
            GUI_RangeProperty(material, Styles.tweakTransParencyText );
            GUI_Toggle(material, Styles.baseMapAlphaAsClippingMask, ShaderPropIsBaseMapAlphaAsClippingMask, MaterialGetInt(material, ShaderPropIsBaseMapAlphaAsClippingMask) != 0);
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
            var applyTo1st = GUI_Toggle(material, Styles.applyTo1stShademapText, ShaderPropUse_BaseAs1st, MaterialGetInt(material, ShaderPropUse_BaseAs1st) != 0);
            EditorGUI.indentLevel -= 2;




            EditorGUI.BeginDisabledGroup(applyTo1st);
            m_MaterialEditor.TexturePropertySingleLine(Styles.firstShadeColorText, firstShadeMap, firstShadeColor);
            EditorGUI.EndDisabledGroup();

            EditorGUI.indentLevel+=2;
            var applyTo2nd =  GUI_Toggle(material, Styles.applyTo2ndShademapText, ShaderPropUse_1stAs2nd, MaterialGetInt(material, ShaderPropUse_1stAs2nd) != 0);
            EditorGUI.indentLevel-=2;


            EditorGUI.BeginDisabledGroup(applyTo2nd);
            m_MaterialEditor.TexturePropertySingleLine(Styles.secondShadeColorText, secondShadeMap, secondShadeColor);
            EditorGUI.EndDisabledGroup();
            EditorGUILayout.Space();

            _NormalMap_Foldout = FoldoutSubMenu(_NormalMap_Foldout, Styles.normalMapFoldout);
            if (_NormalMap_Foldout)
            {

                m_MaterialEditor.TexturePropertySingleLine(Styles.normalMapText, normalMap, bumpScale);
                m_MaterialEditor.TextureScaleOffsetProperty(normalMap);

                EditorGUILayout.LabelField("NormalMap Effectiveness", EditorStyles.boldLabel);
                EditorGUI.indentLevel++;

                GUI_Toggle(material, Styles.threeBasicColorToNormalmapText, ShaderPropIs_NormalMapToBase, MaterialGetInt(material, ShaderPropIs_NormalMapToBase) != 0);
                GUI_Toggle(material, Styles.highLightToNormalmapText, ShaderPropNormalMapToHighColor, MaterialGetInt(material, ShaderPropNormalMapToHighColor) != 0);
                GUI_Toggle(material, Styles.rimlightToNormalmapText, ShaderPropIsNormalMapToRimLight, MaterialGetInt(material, ShaderPropIsNormalMapToRimLight) != 0);

                EditorGUI.indentLevel--;
                EditorGUILayout.Space();
            }

            _ShadowControlMaps_Foldout = FoldoutSubMenu(_ShadowControlMaps_Foldout, Styles.shadowControlMapFoldout);
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
                    EditorGUILayout.LabelField("Mode: Standard", EditorStyles.boldLabel);
                    m_MaterialEditor.TexturePropertySingleLine(Styles.firstPositionMapText, set_1st_ShadePosition);
                    m_MaterialEditor.TexturePropertySingleLine(Styles.secondPositionMapText, set_2nd_ShadePosition);
                }
                else if (MaterialGetInt(material,ShaderPropUtsTechniqe) == (int)UTS_Mode.ShadingGradeMap)
                {    
                    EditorGUILayout.LabelField("Mode: With Additional Control Maps", EditorStyles.boldLabel);
                    m_MaterialEditor.TexturePropertySingleLine(Styles.shadingGradeMapText, shadingGradeMap);
                    GUI_RangeProperty(material, Styles.shadingGradeMapLevelText);
                    GUI_RangeProperty(material, Styles.blureLevelSGMText);
                }
            }
        }

        void GUI_StepAndFeather(Material material)
        {
            GUI_ShadingStepAndFeatherSettings(material);

            if (!_SimpleUI)
            {
                GUI_SystemShadows(material);

                _AdditionalLookdevs_Foldout = FoldoutSubMenu(_AdditionalLookdevs_Foldout, Styles.pointLightFoldout);
                if (_AdditionalLookdevs_Foldout)
                {
                    GUI_AdditionalLookdevs(material);
                }

            }
        }

        void GUI_SystemShadows(Material material)
        {
            bool isEnabled = GUI_Toggle(material, Styles.receiveShadowText, ShaderPropSetSystemShadowsToBase, MaterialGetInt(material,ShaderPropSetSystemShadowsToBase) != 0);

            //           if (material.GetFloat(ShaderPropSetSystemShadowsToBase) == 1)
            EditorGUI.BeginDisabledGroup(!isEnabled);
            {
                EditorGUI.indentLevel++;
                GUI_RangeProperty(material, Styles.tweakSystemShadowLevelText);

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

        void GUI_ShadingStepAndFeatherSettings(Material material)
        {
            if (material.HasProperty(ShaderPropUtsTechniqe))
            {
                var mode = MaterialGetInt(material, ShaderPropUtsTechniqe);
                if (mode == (int)UTS_Mode.ThreeColorToon)   
                {
                    EditorGUILayout.LabelField("Mode: Standard", EditorStyles.boldLabel);
                    GUI_RangeProperty(material, Styles.shaderPropBaseColorText);
                    GUI_RangeProperty(material, Styles.shaderPropBaseFeatherText);
                    GUI_RangeProperty(material, Styles.shaderPropShadeColorStepText);
                    GUI_RangeProperty(material, Styles.shaderProp1st2nd_Shades_FeatherText);
                    

                    //Sharing variables with ShadingGradeMap method.

                    material.SetFloat(ShaderProp1st_ShadeColor_Step, material.GetFloat(ShaderPropBaseColor_Step));
                    material.SetFloat(ShaderProp1st_ShadeColor_Feather, material.GetFloat(ShaderPropBaseShade_Feather));
                    material.SetFloat(ShaderProp2nd_ShadeColor_Step, material.GetFloat(ShaderPropShadeColor_Step));
                    material.SetFloat(ShaderProp2nd_ShadeColor_Feather, material.GetFloat(ShaderProp1st2nd_Shades_Feather));
                }
                else if (mode == (int)UTS_Mode.ShadingGradeMap)
                {    //SGM
                    EditorGUILayout.LabelField("Mode: With Additional Control Maps", EditorStyles.boldLabel);

                    GUI_RangeProperty(material, Styles.shaderProp1st_ShadeColor_StepText);
                    GUI_RangeProperty(material, Styles.shaderProp1st_ShadeColor_FeatherText);
                    GUI_RangeProperty(material, Styles.shaderProp2nd_ShadeColor_StepText);
                    GUI_RangeProperty(material, Styles.shaderProp2nd_ShadeColor_FeatherText);

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

            EditorGUI.indentLevel++;
            GUI_RangeProperty(material, Styles.shaderPropStepOffsetText);
            GUI_Toggle(material, Styles.filterPointLightText, ShaderPropIsFilterHiCutPointLightColor, MaterialGetInt(material, ShaderPropIsFilterHiCutPointLightColor) != 0);

            EditorGUI.indentLevel--;
            EditorGUILayout.Space();
        }


        void GUI_HighlightSettings(Material material)
        {
            m_MaterialEditor.TexturePropertySingleLine(Styles.highColorText, highColor_Tex, highColor);
            GUI_RangeProperty(material, Styles.shaderPropHilightPowerText);

            if (!_SimpleUI)
            {

                EditorGUI.showMixedValue = specularMode.hasMixedValue;

                int mode = (int)specularMode.floatValue;
                EditorGUI.BeginChangeCheck();
                mode = EditorGUILayout.Popup(Styles.specularModeText, mode, System.Enum.GetNames(typeof(UTS_SpeculerMode)));
                if (EditorGUI.EndChangeCheck())
                {
                    m_MaterialEditor.RegisterPropertyChangeUndo(Styles.specularModeText.text);
                    switch ((UTS_SpeculerMode)mode)
                    {
                    case UTS_SpeculerMode.Hard:
                        break;
                    case UTS_SpeculerMode.Soft:
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
                blendingMode = EditorGUILayout.Popup(Styles.specularBlendModeText, blendingMode, System.Enum.GetNames(typeof(UTS_SpeculerColorBlendMode)));
                if (EditorGUI.EndChangeCheck())
                {
                    m_MaterialEditor.RegisterPropertyChangeUndo(Styles.specularModeText.text);
                    specularBlendMode.floatValue = blendingMode;
                }
                EditorGUI.indentLevel--;
                EditorGUILayout.EndHorizontal();
                EditorGUI.showMixedValue = false;
                EditorGUI.EndDisabledGroup();


                var ret = GUI_Toggle(material, Styles.highlightOnShadowText, ShaderPropIs_UseTweakHighColorOnShadow, MaterialGetInt(material, ShaderPropIs_UseTweakHighColorOnShadow) != 0);
                EditorGUI.BeginDisabledGroup(!ret);
                {
                    EditorGUI.indentLevel++;
                    GUI_RangeProperty(material, Styles.tweakHighColorOnShadowText);
                    EditorGUI.indentLevel--;
                }
                EditorGUI.EndDisabledGroup();

            }

            EditorGUILayout.Space();
            //Line();
            //EditorGUILayout.Space();

            EditorGUILayout.LabelField("Highlight Mask", EditorStyles.boldLabel);
            EditorGUI.indentLevel++;
            m_MaterialEditor.TexturePropertySingleLine(Styles.highColorMaskText, set_HighColorMask);
            GUI_RangeProperty(material, Styles.hilightMaskLevelText);
            
            EditorGUI.indentLevel--;

            EditorGUILayout.Space();
        }

        void GUI_RimLight(Material material)
        {

            EditorGUILayout.BeginHorizontal();
            var rimLightEnabled = GUI_Toggle(material, Styles.rimlightText, ShaderPropRimLight, MaterialGetInt(material, ShaderPropRimLight) != 0);
            EditorGUILayout.EndHorizontal();
            EditorGUI.BeginDisabledGroup(!rimLightEnabled);
            EditorGUI.indentLevel++;

            GUI_ColorProperty(material, Styles.rimLightColorText);
            GUI_RangeProperty(material, Styles.rimLightPowerText);

            if (!_SimpleUI)
            {
                GUI_RangeProperty(material, Styles.rimLight_InsideMaskText);

                EditorGUILayout.BeginHorizontal();
                GUI_Toggle(material, Styles.rimlightFeatherText, ShaderPropRimLight_FeatherOff, MaterialGetInt(material, ShaderPropRimLight_FeatherOff) != 0);
                EditorGUILayout.EndHorizontal();
                EditorGUILayout.BeginHorizontal();

                //GUILayout.Space(60);
                var direcitonMaskEnabled = GUI_Toggle(material, Styles.rimlightDirectionMaskText, ShaderPropLightDirection_MaskOn, MaterialGetInt(material, ShaderPropLightDirection_MaskOn) != 0);
                EditorGUILayout.EndHorizontal();

                EditorGUI.BeginDisabledGroup(!direcitonMaskEnabled);
                {
                    EditorGUI.indentLevel++;
                    GUI_RangeProperty(material, Styles.lightDirectionMaskLevelText);

                    EditorGUILayout.BeginHorizontal();
                    var antipodean_RimLight = GUI_Toggle(material, Styles.inversedRimlightText, ShaderPropAdd_Antipodean_RimLight, MaterialGetInt(material, ShaderPropAdd_Antipodean_RimLight )!= 0);
                    EditorGUILayout.EndHorizontal();

                    EditorGUI.BeginDisabledGroup(!antipodean_RimLight);
                    {
                        EditorGUI.indentLevel++;
                        GUI_ColorProperty(material, Styles.apRimLightColorText);
                        GUI_RangeProperty(material, Styles.inversedRimLightPowerText);

                        EditorGUILayout.BeginHorizontal();
                        GUI_Toggle(material, Styles.inversedRimlightFeatherText, ShaderPropAp_RimLight_FeatherOff, MaterialGetInt(material, ShaderPropAp_RimLight_FeatherOff) != 0);


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
            GUI_RangeProperty(material, Styles.rimLightMaskLevelText);

            //EditorGUI.indentLevel--;

            EditorGUI.indentLevel--;
            EditorGUILayout.Space();

            EditorGUI.EndDisabledGroup();




        }

        void GUI_MatCap(Material material)
        {
            EditorGUILayout.BeginHorizontal();
            var matcapEnabled = GUI_Toggle(material, Styles.matCapText, ShaderPropMatCap,MaterialGetInt(material, ShaderPropMatCap) != 0);
            EditorGUILayout.EndHorizontal();
            EditorGUI.BeginDisabledGroup(!matcapEnabled);

            m_MaterialEditor.TexturePropertySingleLine(Styles.matCapSamplerText, matCap_Sampler, matCapColor);
            EditorGUI.indentLevel++;
            m_MaterialEditor.TextureScaleOffsetProperty(matCap_Sampler);

            if (!_SimpleUI)
            {

                GUI_RangeProperty(material, Styles.matcapBlurLevelText);
                EditorGUILayout.BeginHorizontal();
                DoPopup(Styles.matcapBlendModeText, matcapBlendMode, System.Enum.GetNames(typeof(UTS_MatcapColorBlendMode)));
                EditorGUILayout.EndHorizontal();

                GUI_RangeProperty(material, Styles.tweakMatCapUVText);
                GUI_RangeProperty(material, Styles.rotateMatCapUVText);

                EditorGUILayout.BeginHorizontal();
                GUI_Toggle(material, Styles.camearRollingStabilizerText, ShaderPropCameraRolling_Stabilizer, MaterialGetInt(material, ShaderPropCameraRolling_Stabilizer) != 0);
                EditorGUILayout.EndHorizontal();

                EditorGUILayout.BeginHorizontal();
                var isNormalMapForMatCap = GUI_Toggle(material, Styles.matCapNormalmapSpecularaMask, ShaderPropIs_NormalMapForMatCap, MaterialGetInt(material, ShaderPropIs_NormalMapForMatCap) != 0);

                //GUILayout.Space(60);

                EditorGUILayout.EndHorizontal();
                EditorGUI.BeginDisabledGroup(!isNormalMapForMatCap);
                {
                    EditorGUI.indentLevel++;
                    m_MaterialEditor.TexturePropertySingleLine(Styles.normalMapText, normalMapForMatCap, bumpScaleMatcap);
                    m_MaterialEditor.TextureScaleOffsetProperty(normalMapForMatCap);
                    GUI_RangeProperty(material, Styles.rotate_NormalMapForMatCapUVText);
                    EditorGUI.indentLevel--;
                }
                EditorGUI.EndDisabledGroup();

                EditorGUILayout.BeginHorizontal();
                var tweakMatCapOnShadows = GUI_Toggle(material, Styles.matCapOnShadow, ShaderPropIs_UseTweakMatCapOnShadow, MaterialGetInt(material, ShaderPropIs_UseTweakMatCapOnShadow) != 0);
 
                EditorGUILayout.EndHorizontal();
                EditorGUI.BeginDisabledGroup(!tweakMatCapOnShadows);
                {
                    EditorGUI.indentLevel++;
                    GUI_RangeProperty(material, Styles.tweakMatCapOnShadowText);
                    EditorGUI.indentLevel--;
                }
                EditorGUI.EndDisabledGroup();
                DoPopup(Styles.matcapOrthoText, matcapCameraMode, System.Enum.GetNames(typeof(CameraProjectionType)));
            }

            EditorGUILayout.Space();
            //Line();
            //EditorGUILayout.Space();

            EditorGUILayout.LabelField("MatCap Mask", EditorStyles.boldLabel);
            m_MaterialEditor.TexturePropertySingleLine(Styles.matCapMaskText, set_MatcapMask);
            m_MaterialEditor.TextureScaleOffsetProperty(set_MatcapMask);
            GUI_RangeProperty(material, Styles.tweakMatCapMaskLevelText);

            GUI_Toggle(material, Styles.invertMatCapMaskText, ShaderPropInvert_MatcapMask, MaterialGetInt(material, ShaderPropInvert_MatcapMask) != 0);


            EditorGUI.indentLevel--;

            EditorGUI.EndDisabledGroup();


            //EditorGUILayout.Space();
        }

        void GUI_AngelRing(Material material)
        {
            var angelRingEnabled = GUI_Toggle(material, Styles.angelRingProjectionText, ShaderPropAngelRing, MaterialGetInt(material, ShaderPropAngelRing) != 0);
            EditorGUI.BeginDisabledGroup(!angelRingEnabled);
            {
                m_MaterialEditor.TexturePropertySingleLine(Styles.angelRingText, angelRing_Sampler, angelRing_Color);
                EditorGUI.indentLevel++;
                //m_MaterialEditor.TextureScaleOffsetProperty(angelRing_Sampler);
                GUI_RangeProperty(material, Styles.arOffsetU_Text);
                GUI_RangeProperty(material, Styles.arOffsetV_Text);

                GUI_Toggle(material, Styles.angelRingAlphaAdClippingMaskText, ShaderPropARSampler_AlphaOn, MaterialGetInt(material, ShaderPropARSampler_AlphaOn) != 0);

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


                GUI_FloatProperty(material, Styles.baseSpeedText);
                if (!_SimpleUI)
                {
                    int mode = MaterialGetInt(material, ShaderPropIs_ViewCoord_Scroll);
                    EditorGUI.BeginChangeCheck();
                    mode = EditorGUILayout.Popup(Styles.emissiveScrollAnimationModeText, (int)mode, EmissiveScrollMode);
                    if (EditorGUI.EndChangeCheck())
                    {
                        m_MaterialEditor.RegisterPropertyChangeUndo("Emissive Scroll Mode");
                        MaterialSetInt(material, ShaderPropIs_ViewCoord_Scroll, mode);
                    }
                }

                
                GUI_RangeProperty(material, Styles.scrollEmissiveUText);
                GUI_RangeProperty(material, Styles.scrollEmissiveVText);
                GUI_FloatProperty(material, Styles.rotateEmissiveUVText);

                GUI_Toggle(material, Styles.pingpongMoveText, ShaderPropIs_PingPong_Base, MaterialGetInt(material, ShaderPropIs_PingPong_Base) != 0);


                if (!_SimpleUI)
                {
                    EditorGUILayout.Space();

                    //GUILayout.Space(60);
                    var isColorShiftEnabled = GUI_Toggle(material, Styles.colorShitWithTimeText, ShaderPropIs_ColorShift, MaterialGetInt(material, ShaderPropIs_ColorShift) != 0 );


                    EditorGUI.indentLevel++;
                    EditorGUI.BeginDisabledGroup(!isColorShiftEnabled);
                    {
                        GUI_ColorProperty(material, Styles.colorShiftText);
                        GUI_FloatProperty(material, Styles.colorShiftSpeedText);
                    }
                    EditorGUI.EndDisabledGroup();

                    EditorGUI.indentLevel--;

                    EditorGUILayout.Space();

                    var isViewShiftEnabled = GUI_Toggle(material, Styles.colorShiftWithViewAngle, ShaderPropIs_ViewShift, MaterialGetInt(material, ShaderPropIs_ViewShift) != 0);


                    EditorGUI.indentLevel++;
                    EditorGUI.BeginDisabledGroup(!isViewShiftEnabled);
                    GUI_ColorProperty(material, Styles.viewShiftText);
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
                MaterialSetInt(material, srpDefaultCullMode, (int)CullingMode.Backface);
            }
        }
        internal static void SetupOutline(Material material)
        {
            var srpDefaultLightModeTag = material.GetTag("LightMode", false, srpDefaultLightModeName);
            if (srpDefaultLightModeTag == srpDefaultLightModeName)
            {
                MaterialSetInt(material, srpDefaultColorMask, 15);
                MaterialSetInt(material,srpDefaultCullMode, (int)CullingMode.Frontface);
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
            EditorGUI.indentLevel++;
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
            m_outlineMode = (OutlineMode)EditorGUILayout.EnumPopup(Styles.outlineModeText, m_outlineMode);
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

            GUI_FloatProperty(material, Styles.outlineWidthText);
            GUI_ColorProperty(material, Styles.outlineColorText);

            GUI_Toggle(material, Styles.colorShiftWithViewAngle, ShaderPropIs_BlendBaseColor, MaterialGetInt(material, ShaderPropIs_BlendBaseColor) != 0);

            m_MaterialEditor.TexturePropertySingleLine(Styles.outlineSamplerText, outline_Sampler);
            GUI_FloatProperty(material, Styles.offsetZText);

            if (!_SimpleUI)
            {
                EditorGUILayout.Space();
                //                _AdvancedOutline_Foldout = FoldoutSubMenu(_AdvancedOutline_Foldout, Styles.AdvancedOutlineFoldout);
                //                if (_AdvancedOutline_Foldout)
                {

                    EditorGUILayout .LabelField("Camera Distance for Outline Width");
                    EditorGUI.indentLevel++;
                    GUI_FloatProperty(material, Styles.farthestDistanceText);
                    GUI_FloatProperty(material, Styles.nearestDistanceText);
                    EditorGUI.indentLevel--;

                    var useOutlineTexture =  GUI_Toggle(material, Styles.outlineColorMapText, ShaderPropIs_OutlineTex, MaterialGetInt(material, ShaderPropIs_OutlineTex)!=0); ;
                    EditorGUI.BeginDisabledGroup(!useOutlineTexture);
                    m_MaterialEditor.TexturePropertySingleLine(Styles.outlineTexText, outlineTex);
                    EditorGUI.EndDisabledGroup();
                    EditorGUI.BeginDisabledGroup(m_outlineMode != OutlineMode.NormalDirection);
                    {
                        var isBackedNormal = GUI_Toggle(material, Styles.bakedNormalForOutlineText, ShaderPropIs_BakedNormal, MaterialGetInt(material, ShaderPropIs_BakedNormal) != 0);
                        EditorGUI.BeginDisabledGroup(!isBackedNormal);
                        m_MaterialEditor.TexturePropertySingleLine(Styles.bakedNormalOutlineText, bakedNormal);
                        EditorGUI.EndDisabledGroup();
                    }
                    EditorGUI.EndDisabledGroup();
                }
                EditorGUI.EndDisabledGroup(); //!isOutlineEnabled
            }
            EditorGUI.indentLevel--;

            EditorGUILayout.Space();
        }

        void GUI_Tessellation(Material material)
        {
            GUI_RangeProperty(material, Styles.legacyTessEdgeLengthText);
            GUI_RangeProperty(material, Styles.legacyTessPhongStrengthText);
            GUI_RangeProperty(material, Styles.legacyTessExtrusionAmountText);

            EditorGUILayout.Space();
        }



        void GUI_LightColorEffectiveness(Material material)
        {
            GUI_Toggle(material, Styles.lightColorEffectivinessToBaseColorText, ShaderPropIsLightColor_Base, MaterialGetInt(material, ShaderPropIsLightColor_Base)!= 0);
            GUI_Toggle(material, Styles.lightColorEffectivinessTo1stShadingText, ShaderPropIs_LightColor_1st_Shade, MaterialGetInt(material, ShaderPropIs_LightColor_1st_Shade) != 0);
            GUI_Toggle(material, Styles.lightColorEffectivinessTo2ndShadingText, ShaderPropIs_LightColor_2nd_Shade, MaterialGetInt(material, ShaderPropIs_LightColor_2nd_Shade) != 0);
            GUI_Toggle(material, Styles.lightColorEffectivinessToHighlitText, ShaderPropIs_LightColor_HighColor, MaterialGetInt(material, ShaderPropIs_LightColor_HighColor) != 0);
            GUI_Toggle(material, Styles.lightColorEffectivinessToRimlitText, ShaderPropIs_LightColor_RimLight, MaterialGetInt(material, ShaderPropIs_LightColor_RimLight) != 0);
            GUI_Toggle(material, Styles.lightColorEffectivinessToInvRimlitText, ShaderPropIs_LightColor_Ap_RimLight, MaterialGetInt(material, ShaderPropIs_LightColor_Ap_RimLight) != 0);
            GUI_Toggle(material, Styles.lightColorEffectivinessToMatCapText, ShaderPropIs_LightColor_MatCap, MaterialGetInt(material, ShaderPropIs_LightColor_MatCap) != 0);
            GUI_Toggle(material, Styles.lightColorEffectivinessToOutlineText, ShaderPropIs_LightColor_Outline, MaterialGetInt(material, ShaderPropIs_LightColor_Outline) != 0);

            EditorGUILayout.Space();
            GUI_RangeProperty(material, Styles.giIntensityText);

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
            isMetaverseLightEnabled = GUI_Toggle(material, Styles.metaverseLightText, ShaderPropUnlit_Intensity, isMetaverseLightEnabled != 0) ? 1: 0;
            EditorGUI.BeginDisabledGroup(isMetaverseLightEnabled == 0);
            {
                EditorGUI.indentLevel++;
                GUI_RangeProperty(material, Styles.metaverseRangePropText);

                var isBold = GUI_Toggle(material, Styles.metaverseLightDirectionText, ShaderPropIs_BLD, MaterialGetInt(material, ShaderPropIs_BLD) != 0);
                EditorGUI.BeginDisabledGroup(!isBold);

                EditorGUI.indentLevel++;
                GUI_RangeProperty(material, Styles.metaverseOffsettXaxisText);
                GUI_RangeProperty(material, Styles.metaverseOffsettYaxisText);

                GUI_Toggle(material, Styles.invertZaxisDirection, ShaderPropInverse_Z_Axis_BLD, MaterialGetInt(material, ShaderPropInverse_Z_Axis_BLD) != 0);

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

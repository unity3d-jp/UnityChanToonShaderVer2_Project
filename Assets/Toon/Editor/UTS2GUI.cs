//Unitychan Toon Shader ver.2.0
//UTS2GUI.cs for UTS2 v.2.0.6
//nobuyuki@unity3d.com
//https://github.com/unity3d-jp/UnityChanToonShaderVer2_Project
//(C)Unity Technologies Japan/UCL
using UnityEngine;
using UnityEditor;

namespace UnityChan
{
    public class UTS2GUI : ShaderGUI {

        public enum _UTS_Technique{
            DoubleSideWithFeather, ShadingGradeMap, OutlineObject
        }
        public enum _OutlineMode{
            NormalDirection, PositionScaling
        }

        public enum _CullingMode{
            CullingOff, FrontCulling, BackCulling
        }

        //enum _OutlineMode の状態を保持するための変数.
        public _OutlineMode outlineMode;
        public _CullingMode cullingMode;

        //ボタンサイズ
        public GUILayoutOption[] shortButtonStyle = new GUILayoutOption[]{ GUILayout.Width(130) }; 
        public GUILayoutOption[] middleButtonStyle = new GUILayoutOption[]{ GUILayout.Width(130) }; 



        //
        static int _StencilNo_Setting;
        static bool _HasOutline = true;
        static bool _OriginalInspector = false;
        static bool _SimpleUI = false; 
        bool _Use_VrcRecommend = false;

        //Foldoutの初期値.
        static bool _BasicShaderSettings_Foldout = false;
        static bool _BasicThreeColors_Foldout = true;
            // static bool _SharingTextures_Foldout = false;
            static bool _NormalMap_Foldout = false;
            static bool _ShadowControlMaps_Foldout = false;
        static bool _StepAndFeather_Foldout = true;
            //static bool _SystemShadows_Foldout = true;
            //static bool _BasicLookdevs_Foldout = true;
            static bool _AdditionalLookdevs_Foldout = false;
        static bool _HighColor_Foldout = true;
        static bool _RimLight_Foldout = true;
        static bool _MatCap_Foldout = true;
        static bool _AngelRing_Foldout = true;
        static bool _Emissive_Foldout = true;
        static bool _Outline_Foldout = true;
            static bool _AdvancedOutline_Foldout = false;
        static bool _Tessellation_Foldout = false;
        static bool _LightColorContribution_Foldout = false;
        static bool _AdditionalLightingSettings_Foldout = false;

    // -----------------------------------------------------
        // UTS2 materal properties -------------------------
    //    MaterialProperty ustTechnique = null;
    //    MaterialProperty cullMode = null;
    //    MaterialProperty stencilNo = null;
        MaterialProperty clippingMask = null;
    //    MaterialProperty inverse_Clipping = null;
        MaterialProperty clipping_Level = null;
        MaterialProperty tweak_transparency = null;
    //    MaterialProperty isBaseMapAlphaAsClippingMask = null;
    //    MaterialProperty simpleUI = null;
        MaterialProperty mainTex = null;
        MaterialProperty baseColor = null;
        MaterialProperty firstShadeMap = null;
        MaterialProperty firstShadeColor = null;
        MaterialProperty secondShadeMap = null;
        MaterialProperty secondShadeColor = null;
    //    MaterialProperty use_BaseAs1st = null;
    //    MaterialProperty use_1stAs2nd = null;
        MaterialProperty normalMap = null;
        MaterialProperty bumpScale = null;
    //    MaterialProperty is_NormalMapToBase = null;
    //    MaterialProperty is_NormalMapToHighColor = null;
    //    MaterialProperty is_NormalMapToRimLight = null;
        MaterialProperty set_1st_ShadePosition = null;
        MaterialProperty set_2nd_ShadePosition = null;
        MaterialProperty shadingGradeMap = null;
        MaterialProperty tweak_ShadingGradeMapLevel = null;
        MaterialProperty blurLevelSGM = null;
    //    MaterialProperty set_SystemShadowsToBase =null;
        MaterialProperty tweak_SystemShadowsLevel = null;
        MaterialProperty baseColor_Step = null;
        MaterialProperty baseShade_Feather = null;
        MaterialProperty shadeColor_Step = null;
        MaterialProperty first2nd_Shades_Feather = null;
        MaterialProperty first_ShadeColor_Step = null;
        MaterialProperty first_ShadeColor_Feather = null;
        MaterialProperty second_ShadeColor_Step = null;
        MaterialProperty second_ShadeColor_Feather = null;
        MaterialProperty stepOffset = null;
    //    MaterialProperty is_Filter_HiCutPointLightColor = null;
        MaterialProperty highColor_Tex = null;
        MaterialProperty highColor = null;
        MaterialProperty highColor_Power = null;
    //    MaterialProperty is_SpecularToHighColor = null;
    //    MaterialProperty is_BlendAddToHiColor = null;
    //    MaterialProperty is_UseTweakHighColorOnShadow = null;
        MaterialProperty tweakHighColorOnShadow = null;
        MaterialProperty set_HighColorMask = null;
        MaterialProperty tweak_HighColorMaskLevel = null;
    //    MaterialProperty rimLight = null;
        MaterialProperty rimLightColor = null;
        MaterialProperty rimLight_Power = null;
        MaterialProperty rimLight_InsideMask = null;
    //    MaterialProperty rimLight_FeatherOff = null;
    //    MaterialProperty lightDirection_MaskOn = null;
        MaterialProperty tweak_LightDirection_MaskLevel = null;
    //    MaterialProperty add_Antipodean_RimLight = null;
        MaterialProperty ap_RimLightColor = null;
        MaterialProperty ap_RimLight_Power = null;
    //    MaterialProperty ap_RimLight_FeatherOff = null;
        MaterialProperty set_RimLightMask = null;
        MaterialProperty tweak_RimLightMaskLevel = null;
    //    MaterialProperty matCap = null;
        MaterialProperty matCap_Sampler = null;
        MaterialProperty matCapColor = null;
        MaterialProperty blurLevelMatcap = null;
    //    MaterialProperty is_BlendAddToMatCap = null;
        MaterialProperty tweak_MatCapUV = null;
        MaterialProperty rotate_MatCapUV = null;
    //    MaterialProperty cameraRolling_Stabilizer = null;
    //    MaterialProperty is_NormalMapForMatCap = null;
        MaterialProperty normalMapForMatCap = null;
        MaterialProperty bumpScaleMatcap = null;
        MaterialProperty rotate_NormalMapForMatCapUV = null;
    //    MaterialProperty is_UseTweakMatCapOnShadow = null;
        MaterialProperty tweakMatCapOnShadow = null;
    //    MaterialProperty is_Ortho = null;
        MaterialProperty set_MatcapMask = null;
        MaterialProperty tweak_MatcapMaskLevel = null;
    //    MaterialProperty inverse_MatcapMask = null;
    //    MaterialProperty angelRing = null;
        MaterialProperty angelRing_Sampler = null;
        MaterialProperty angelRing_Color = null;
        MaterialProperty ar_OffsetU = null;
        MaterialProperty ar_OffsetV = null;
    //    MaterialProperty arSampler_AlphaOn = null;
        MaterialProperty emissive_Tex = null;
        MaterialProperty emissive_Color = null;
    //    MaterialProperty outline = null;
        MaterialProperty outline_Width = null;
        MaterialProperty outline_Color = null;
    //    MaterialProperty is_BlendBaseColor = null;
        MaterialProperty outline_Sampler = null;
        MaterialProperty offset_Z = null;
        MaterialProperty farthest_Distance = null;
        MaterialProperty nearest_Distance = null;
    //    MaterialProperty is_OutlineTex = null;
        MaterialProperty outlineTex = null;
    //    MaterialProperty is_BakedNormal = null;
        MaterialProperty bakedNormal = null;
        MaterialProperty tessEdgeLength = null;
        MaterialProperty tessPhongStrength = null;
        MaterialProperty tessExtrusionAmount = null;
    //    MaterialProperty is_LightColor_Base = null;
    //    MaterialProperty is_LightColor_1st_Shade = null;
    //    MaterialProperty is_LightColor_2nd_Shade = null;
    //    MaterialProperty is_LightColor_HighColor = null;
    //    MaterialProperty is_LightColor_RimLight = null;
    //    MaterialProperty is_LightColor_Ap_RimLight = null;
    //    MaterialProperty is_LightColor_MatCap = null;
    //    MaterialProperty is_LightColor_AR = null;
        MaterialProperty gi_Intensity = null;
        MaterialProperty unlit_Intensity = null;
    //    MaterialProperty is_Filter_LightColor = null;
    //   MaterialProperty is_BLD = null;
        MaterialProperty offset_X_Axis_BLD = null;
        MaterialProperty offset_Y_Axis_BLD = null;
    //    MaterialProperty inverse_Z_Axis_BLD = null;
        //------------------------------------------------------

        MaterialEditor m_MaterialEditor;

        // -----------------------------------------------------

        public void FindProperties(MaterialProperty[] props)
        {
    //		ustTechnique = FindProperty("_utsTechnique", props);
    //        cullMode = FindProperty("_CullMode", props);
            //シェーダーによって無い可能性があるプロパティはfalseを追加.
    //        stencilNo = FindProperty("_StencilNo", props, false);
            clippingMask = FindProperty("_ClippingMask", props, false);
    //        inverse_Clipping = FindProperty("_Inverse_Clipping", props, false);
            clipping_Level = FindProperty("_Clipping_Level", props, false);
            tweak_transparency = FindProperty("_Tweak_transparency", props, false);
    //        isBaseMapAlphaAsClippingMask = FindProperty("_IsBaseMapAlphaAsClippingMask", props, false);
    //        simpleUI = FindProperty("_simpleUI", props);
            mainTex = FindProperty("_MainTex", props);
            baseColor = FindProperty("_BaseColor", props);
            firstShadeMap = FindProperty("_1st_ShadeMap", props);
            firstShadeColor = FindProperty("_1st_ShadeColor", props);
            secondShadeMap = FindProperty("_2nd_ShadeMap", props);
            secondShadeColor = FindProperty("_2nd_ShadeColor", props);
    //        use_BaseAs1st = FindProperty("_Use_BaseAs1st", props);
    //        use_1stAs2nd = FindProperty("_Use_1stAs2nd", props);
            normalMap = FindProperty("_NormalMap", props);
            bumpScale = FindProperty("_BumpScale", props);
    //        is_NormalMapToBase = FindProperty("_Is_NormalMapToBase", props);
    //        is_NormalMapToHighColor = FindProperty("_Is_NormalMapToHighColor", props);
    //        is_NormalMapToRimLight = FindProperty("_Is_NormalMapToRimLight", props);
            set_1st_ShadePosition = FindProperty("_Set_1st_ShadePosition", props, false);
            set_2nd_ShadePosition = FindProperty("_Set_2nd_ShadePosition", props, false);
            shadingGradeMap = FindProperty("_ShadingGradeMap", props, false);
            tweak_ShadingGradeMapLevel = FindProperty("_Tweak_ShadingGradeMapLevel", props, false);
            blurLevelSGM = FindProperty("_BlurLevelSGM", props, false);
    //        set_SystemShadowsToBase = FindProperty("_Set_SystemShadowsToBase",props);
            tweak_SystemShadowsLevel = FindProperty("_Tweak_SystemShadowsLevel",props);
            baseColor_Step = FindProperty("_BaseColor_Step",props);
            baseShade_Feather = FindProperty("_BaseShade_Feather", props);
            shadeColor_Step = FindProperty("_ShadeColor_Step",props);
            first2nd_Shades_Feather = FindProperty("_1st2nd_Shades_Feather",props);
            first_ShadeColor_Step = FindProperty("_1st_ShadeColor_Step",props);
            first_ShadeColor_Feather = FindProperty("_1st_ShadeColor_Feather", props);
            second_ShadeColor_Step = FindProperty("_2nd_ShadeColor_Step", props);
            second_ShadeColor_Feather = FindProperty("_2nd_ShadeColor_Feather",props);
            stepOffset = FindProperty("_StepOffset", props, false);
    //        is_Filter_HiCutPointLightColor = FindProperty("_Is_Filter_HiCutPointLightColor",props);
            highColor_Tex = FindProperty("_HighColor_Tex",props);
            highColor = FindProperty("_HighColor", props);
            highColor_Power = FindProperty("_HighColor_Power", props);
    //        is_SpecularToHighColor = FindProperty("_Is_SpecularToHighColor", props);
    //        is_BlendAddToHiColor = FindProperty("_Is_BlendAddToHiColor", props);
    //        is_UseTweakHighColorOnShadow = FindProperty("_Is_UseTweakHighColorOnShadow", props);
            tweakHighColorOnShadow = FindProperty("_TweakHighColorOnShadow", props);
            set_HighColorMask = FindProperty("_Set_HighColorMask", props);
            tweak_HighColorMaskLevel = FindProperty("_Tweak_HighColorMaskLevel", props);
    //        rimLight = FindProperty("_RimLight", props);
            rimLightColor = FindProperty("_RimLightColor", props);
            rimLight_Power = FindProperty("_RimLight_Power", props);
            rimLight_InsideMask = FindProperty("_RimLight_InsideMask", props);
    //        rimLight_FeatherOff = FindProperty("_RimLight_FeatherOff", props);
    //        lightDirection_MaskOn = FindProperty("_LightDirection_MaskOn", props);
            tweak_LightDirection_MaskLevel = FindProperty("_Tweak_LightDirection_MaskLevel", props);
    //        add_Antipodean_RimLight = FindProperty("_Add_Antipodean_RimLight", props);
            ap_RimLightColor = FindProperty("_Ap_RimLightColor", props);
            ap_RimLight_Power = FindProperty("_Ap_RimLight_Power", props);
    //        ap_RimLight_FeatherOff = FindProperty("_Ap_RimLight_FeatherOff", props);
            set_RimLightMask = FindProperty("_Set_RimLightMask", props);
            tweak_RimLightMaskLevel = FindProperty("_Tweak_RimLightMaskLevel", props);
    //        matCap = FindProperty("_MatCap", props);
            matCap_Sampler = FindProperty("_MatCap_Sampler", props);
            matCapColor = FindProperty("_MatCapColor", props);
            blurLevelMatcap = FindProperty("_BlurLevelMatcap", props);
    //        is_BlendAddToMatCap = FindProperty("_Is_BlendAddToMatCap", props);
            tweak_MatCapUV = FindProperty("_Tweak_MatCapUV", props);
            rotate_MatCapUV = FindProperty("_Rotate_MatCapUV", props);
    //        cameraRolling_Stabilizer = FindProperty("_CameraRolling_Stabilizer", props);
    //        is_NormalMapForMatCap = FindProperty("_Is_NormalMapForMatCap", props);
            normalMapForMatCap = FindProperty("_NormalMapForMatCap", props);
            bumpScaleMatcap = FindProperty("_BumpScaleMatcap", props);
            rotate_NormalMapForMatCapUV = FindProperty("_Rotate_NormalMapForMatCapUV", props);
    //        is_UseTweakMatCapOnShadow = FindProperty("_Is_UseTweakMatCapOnShadow", props);
            tweakMatCapOnShadow = FindProperty("_TweakMatCapOnShadow", props);
    //        is_Ortho = FindProperty("_Is_Ortho", props);
            set_MatcapMask = FindProperty("_Set_MatcapMask", props);
            tweak_MatcapMaskLevel = FindProperty("_Tweak_MatcapMaskLevel", props);
    //        inverse_MatcapMask = FindProperty("_Inverse_MatcapMask", props);
    //        angelRing = FindProperty("_AngelRing", props, false);
            angelRing_Sampler = FindProperty("_AngelRing_Sampler", props, false);
            angelRing_Color = FindProperty("_AngelRing_Color", props, false);
            ar_OffsetU = FindProperty("_AR_OffsetU", props, false);
            ar_OffsetV = FindProperty("_AR_OffsetV", props, false);
    //        arSampler_AlphaOn = FindProperty("_ARSampler_AlphaOn", props, false);
            emissive_Tex = FindProperty("_Emissive_Tex", props);
            emissive_Color = FindProperty("_Emissive_Color", props);
    //        outline = FindProperty("_OUTLINE", props, false);
            outline_Width = FindProperty("_Outline_Width", props, false);
            outline_Color = FindProperty("_Outline_Color", props, false);
    //        is_BlendBaseColor = FindProperty("_Is_BlendBaseColor", props, false);
            outline_Sampler = FindProperty("_Outline_Sampler", props, false);
            offset_Z = FindProperty("_Offset_Z", props, false);
            farthest_Distance = FindProperty("_Farthest_Distance", props, false);
            nearest_Distance = FindProperty("_Nearest_Distance", props, false);
    //        is_OutlineTex = FindProperty("_Is_OutlineTex", props, false);
            outlineTex = FindProperty("_OutlineTex", props, false);
    //        is_BakedNormal = FindProperty("_Is_BakedNormal", props, false);
            bakedNormal = FindProperty("_BakedNormal", props, false);
            tessEdgeLength = FindProperty("_TessEdgeLength", props, false);
            tessPhongStrength = FindProperty("_TessPhongStrength", props, false);
            tessExtrusionAmount = FindProperty("_TessExtrusionAmount", props, false);
    //        is_LightColor_Base = FindProperty("_Is_LightColor_Base", props);
    //        is_LightColor_1st_Shade = FindProperty("_Is_LightColor_1st_Shade", props);
    //        is_LightColor_2nd_Shade = FindProperty("_Is_LightColor_2nd_Shade", props, false);
    //        is_LightColor_HighColor = FindProperty("_Is_LightColor_HighColor", props);
    //        is_LightColor_RimLight = FindProperty("_Is_LightColor_RimLight", props);
    //        is_LightColor_Ap_RimLight = FindProperty("_Is_LightColor_Ap_RimLight", props);
    //        is_LightColor_MatCap = FindProperty("_Is_LightColor_MatCap", props);
    //        is_LightColor_AR = FindProperty("_Is_LightColor_AR", props, false);
            gi_Intensity = FindProperty("_GI_Intensity", props);
            unlit_Intensity = FindProperty("_Unlit_Intensity", props);
    //        is_Filter_LightColor = FindProperty("_Is_Filter_LightColor", props);
    //        is_BLD = FindProperty("_Is_BLD", props);
            offset_X_Axis_BLD = FindProperty("_Offset_X_Axis_BLD", props);
            offset_Y_Axis_BLD = FindProperty("_Offset_Y_Axis_BLD", props);
    //s        inverse_Z_Axis_BLD = FindProperty("_Inverse_Z_Axis_BLD", props);
        }
    // --------------------------------

    // --------------------------------
        static void Line()
        {
            GUILayout.Box("", GUILayout.ExpandWidth(true), GUILayout.Height(1));
        }

        static bool Foldout(bool display, string title)
        {
            var style = new GUIStyle("ShurikenModuleTitle");
            style.font = new GUIStyle(EditorStyles.boldLabel).font;
            style.border = new RectOffset(15, 7, 4, 4);
            style.fixedHeight = 22;
            style.contentOffset = new Vector2(20f, -2f);

            var rect = GUILayoutUtility.GetRect(16f, 22f, style);
            GUI.Box(rect, title, style);

            var e = Event.current;

            var toggleRect = new Rect(rect.x + 4f, rect.y + 2f, 13f, 13f);
            if (e.type == EventType.Repaint)
            {
                EditorStyles.foldout.Draw(toggleRect, false, false, display, false);
            }

            if (e.type == EventType.MouseDown && rect.Contains(e.mousePosition))
            {
                display = !display;
                e.Use();
            }

            return display;
        }

        static bool FoldoutSubMenu(bool display, string title)
        {
            var style = new GUIStyle("ShurikenModuleTitle");
            style.font = new GUIStyle(EditorStyles.boldLabel).font;
            style.border = new RectOffset(15, 7, 4, 4);
            style.padding = new RectOffset(5, 7, 4, 4);
            style.fixedHeight = 22;
            style.contentOffset = new Vector2(32f, -2f);

            var rect = GUILayoutUtility.GetRect(16f, 22f, style);
            GUI.Box(rect, title, style);

            var e = Event.current;

            var toggleRect = new Rect(rect.x + 16f, rect.y + 2f, 13f, 13f);
            if (e.type == EventType.Repaint)
            {
                EditorStyles.foldout.Draw(toggleRect, false, false, display, false);
            }

            if (e.type == EventType.MouseDown && rect.Contains(e.mousePosition))
            {
                display = !display;
                e.Use();
            }

            return display;
        }




    // --------------------------------
        private static class Styles
        {
            public static GUIContent baseColorText = new GUIContent("BaseMap","Base Color : Texture(sRGB) × Color(RGB) Default:White");
            public static GUIContent firstShadeColorText = new GUIContent("1st ShadeMap","1st ShadeColor : Texture(sRGB) × Color(RGB) Default:White");
            public static GUIContent secondShadeColorText = new GUIContent("2nd ShadeMap","2nd ShadeColor : Texture(sRGB) × Color(RGB) Default:White");
            public static GUIContent normalMapText = new GUIContent("NormalMap","NormalMap : Texture(bump)");
            public static GUIContent highColorText = new GUIContent("HighColor","High Color : Texture(sRGB) × Color(RGB) Default:Black");
            public static GUIContent highColorMaskText = new GUIContent("HighColor Mask","HighColor Mask : Texture(linear)");
            public static GUIContent rimLightMaskText = new GUIContent("RimLight Mask","RimLight Mask : Texture(linear)");
            public static GUIContent matCapSamplerText = new GUIContent("MatCap Sampler","MatCap Sampler : Texture(sRGB) × Color(RGB) Default:White");
            public static GUIContent matCapMaskText = new GUIContent("MatCap Mask","MatCap Mask : Texture(linear)");
            public static GUIContent angelRingText = new GUIContent("AngelRing","AngelRing : Texture(sRGB) × Color(RGB) Default:Black");
            public static GUIContent emissiveTexText = new GUIContent("Emissive","Emissive : Texture(sRGB) × Color(HDR) Default:Black");
            public static GUIContent shadingGradeMapText = new GUIContent("Shading Grade Map","影のかかり方マップ。UV座標で影のかかりやすい場所を指定する。Shading Grade Map : Texture(linear)");
            public static GUIContent firstPositionMapText = new GUIContent("1st Shade Position Map","1影色領域に落ちる固定影の位置を、UV座標で指定する。1st Position Map : Texture(linear)");
            public static GUIContent secondPositionMapText = new GUIContent("2nd Shade Position Map","2影色領域に落ちる固定影の位置を、UV座標で指定する。2nd Position Map : Texture(linear)");
            public static GUIContent outlineSamplerText = new GUIContent("Outline Sampler","Outline Sampler : Texture(linear)");
            public static GUIContent outlineTexText = new GUIContent("Outline tex","Outline Tex : Texture(sRGB) Default:White");
            public static GUIContent bakedNormalOutlineText = new GUIContent("Baked NormalMap for Outline","Unpacked Normal Map : Texture(linear) ※通常のノーマルマップではないので注意");
            public static GUIContent clippingMaskText = new GUIContent("Clipping Mask","Clipping Mask : Texture(linear)");
        }
    // --------------------------------

        public override void OnGUI(MaterialEditor materialEditor, MaterialProperty[] props)
        {
            EditorGUIUtility.fieldWidth = 0;
            FindProperties(props);
            m_MaterialEditor = materialEditor;
            Material material = materialEditor.target as Material;

            //UTSのシェーダー方式の確認.
            CheckUtsTechnique(material);

            //1行目の横並び3ボタン.
            EditorGUILayout.BeginHorizontal();
                //Original Inspectorの選択チェック.
                if(material.HasProperty("_simpleUI")){
                    var selectedUI = material.GetInt("_simpleUI");
                    if(selectedUI==2){
                        _OriginalInspector = true;  //Original GUI
                    }else if(selectedUI == 1){
                        _SimpleUI = true;   //UTS2 Biginner GUI
                    }
                    //Original/Custom GUI 切り替えボタン.
                    if (_OriginalInspector)
                    {
                        if (GUILayout.Button("Change CustomUI",middleButtonStyle))
                        {
                            _OriginalInspector = false;
                            material.SetInt("_simpleUI",0); //UTS2 Pro GUI
                        }
                        OpenManualLink();
                        //継承したレイアウトのクリア.
                        EditorGUILayout.EndHorizontal();
                        //オリジナルのGUI表示
                        m_MaterialEditor.PropertiesDefaultGUI(props);
                        return;
                    }
                    if (GUILayout.Button("Show All properties",middleButtonStyle))
                    {
                        _OriginalInspector = true;
                        material.SetInt("_simpleUI",2); //Original GUI
                    }        
                }
                //マニュアルを開く.
                OpenManualLink();
            EditorGUILayout.EndHorizontal();

            EditorGUI.BeginChangeCheck();

            EditorGUILayout.Space();

            _BasicShaderSettings_Foldout = Foldout(_BasicShaderSettings_Foldout, "Basic Shader Settings");
            if(_BasicShaderSettings_Foldout)
            {
                EditorGUI.indentLevel++;
                //EditorGUILayout.Space(); 
                GUI_SetCullingMode(material);

                if(material.HasProperty("_StencilNo")){
    
                    GUI_SetStencilNo(material);
                }

                if(material.HasProperty("_ClippingMask")){
                    GUI_SetClippingMask(material);
                }

                if(material.HasProperty("_Tweak_transparency")){
                    GUI_SetTransparencySetting(material);
                }

                GUI_OptionMenu(material);

                EditorGUI.indentLevel--;
            }

            EditorGUILayout.Space();

            _BasicThreeColors_Foldout = Foldout(_BasicThreeColors_Foldout, "【Basic Three Colors and Control Maps Setups】");
            if(_BasicThreeColors_Foldout)
            {
                EditorGUI.indentLevel++;
                //EditorGUILayout.Space(); 
                GUI_BasicThreeColors(material);
                EditorGUI.indentLevel--;
            }

            EditorGUILayout.Space();

            _StepAndFeather_Foldout = Foldout(_StepAndFeather_Foldout, "【Basic Lookdevs : Shading Step and Feather Settings】");
            if (_StepAndFeather_Foldout)
            {
                EditorGUI.indentLevel++;
                //EditorGUILayout.Space(); 
                GUI_StepAndFeather(material);
                EditorGUI.indentLevel--;
            }

            EditorGUILayout.Space();

            _HighColor_Foldout = Foldout(_HighColor_Foldout, "【HighColor Settings】");
            if (_HighColor_Foldout)
            {
                EditorGUI.indentLevel++;
                EditorGUILayout.Space();
                GUI_HighColor(material);
                EditorGUI.indentLevel--;
            }

            EditorGUILayout.Space();

            _RimLight_Foldout = Foldout(_RimLight_Foldout, "【RimLight Settings】");
            if (_RimLight_Foldout)
            {
                EditorGUI.indentLevel++;
                EditorGUILayout.Space();
                GUI_RimLight(material);
                EditorGUI.indentLevel--;
            }

            EditorGUILayout.Space();

            _MatCap_Foldout = Foldout(_MatCap_Foldout, "【MatCap : Texture Projection Settings】");
            if (_MatCap_Foldout)
            {
                EditorGUI.indentLevel++;
                EditorGUILayout.Space();
                GUI_MatCap(material);
                EditorGUI.indentLevel--;
            }

            EditorGUILayout.Space();

            if(material.HasProperty("_AngelRing")){
                _AngelRing_Foldout = Foldout(_AngelRing_Foldout, "【AngelRing Projection Settings】");
                if (_AngelRing_Foldout)
                {
                    EditorGUI.indentLevel++;
                    EditorGUILayout.Space();
                    GUI_AngelRing(material);
                    EditorGUI.indentLevel--;
                }

                EditorGUILayout.Space();
            }

            _Emissive_Foldout = Foldout(_Emissive_Foldout, "【Emissive : Self-luminescence Settings】");
            if (_Emissive_Foldout)
            {
                EditorGUI.indentLevel++;
                //EditorGUILayout.Space();
                GUI_Emissive(material);
                EditorGUI.indentLevel--;
            }

            EditorGUILayout.Space();

            if(material.HasProperty("_OUTLINE")){
                _HasOutline = true;
                _Outline_Foldout = Foldout(_Outline_Foldout, "【Outline Settings】");
                if (_Outline_Foldout)
                {
                    EditorGUI.indentLevel++;
                    EditorGUILayout.Space();
                    GUI_Outline(material);
                    EditorGUI.indentLevel--;
                }
                EditorGUILayout.Space();
            }else{
                _HasOutline = false;
            }

            if(material.HasProperty("_TessEdgeLength")){
                _Tessellation_Foldout = Foldout(_Tessellation_Foldout, "【DX11 Phong Tessellation Settings】");
                if (_Tessellation_Foldout)
                {
                    EditorGUI.indentLevel++;
                    EditorGUILayout.Space();
                    GUI_Tessellation(material);
                    EditorGUI.indentLevel--;
                }

                EditorGUILayout.Space();
            }

            if(!_SimpleUI){
                _LightColorContribution_Foldout = Foldout(_LightColorContribution_Foldout, "【LightColor Contribution to Materials】");
                if (_LightColorContribution_Foldout)
                {
                    EditorGUI.indentLevel++;
                    //EditorGUILayout.Space();
                    GUI_LightColorContribution(material);
                    EditorGUI.indentLevel--;
                }

                EditorGUILayout.Space();

                _AdditionalLightingSettings_Foldout = Foldout(_AdditionalLightingSettings_Foldout, "【Environmental Lighting Contributions Setups】");
                if (_AdditionalLightingSettings_Foldout)
                {
                    EditorGUI.indentLevel++;
                    EditorGUILayout.Space();
                    GUI_AdditionalLightingSettings(material);
                    EditorGUI.indentLevel--;
                }

                EditorGUILayout.Space();
            }

            if (EditorGUI.EndChangeCheck())
            {
                m_MaterialEditor.PropertiesChanged();
            }

        }// End of OnGUI()


    // --------------------------------

        void CheckUtsTechnique(Material material){
            if (material.HasProperty("_utsTechnique"))//DoubleWithFeather==0 or ShadingGradeMap==1
            {
                if(material.GetInt("_utsTechnique") == (int)_UTS_Technique.DoubleSideWithFeather)   //DWF
                {
                    if(!material.HasProperty("_Set_1st_ShadePosition")){
                        //SGMに変更.
                        material.SetInt("_utsTechnique", (int)_UTS_Technique.ShadingGradeMap);
                    }
                }else if(material.GetInt("_utsTechnique") == (int)_UTS_Technique.ShadingGradeMap){    //SGM
                //SGM
                    if(!material.HasProperty("_ShadingGradeMap")){
                        //DWFに変更.
                        material.SetInt("_utsTechnique", (int)_UTS_Technique.DoubleSideWithFeather);
                    }
                }else{

                }
            }else{

            }
        }

        void OpenManualLink(){
            if (GUILayout.Button("日本語マニュアル",middleButtonStyle))
            {
                    Application.OpenURL("https://github.com/unity3d-jp/UnityChanToonShaderVer2_Project/blob/master/Manual/UTS2_Manual_ja.md");
            }
            if (GUILayout.Button("English manual",middleButtonStyle))
            {
                    Application.OpenURL("https://github.com/unity3d-jp/UnityChanToonShaderVer2_Project/blob/master/Manual/UTS2_Manual_en.md");
            }
        }

        void GUI_SetCullingMode(Material material){
            int _CullMode_Setting = material.GetInt("_CullMode");
            //Enum形式に変換して、outlineMode変数に保持しておく.
            if ((int)_CullingMode.CullingOff == _CullMode_Setting){
                cullingMode = _CullingMode.CullingOff;
            }else if((int)_CullingMode.FrontCulling == _CullMode_Setting){
                cullingMode = _CullingMode.FrontCulling;
            }else{
                cullingMode = _CullingMode.BackCulling;
            }
            //EnumPopupでGUI記述.
            cullingMode = (_CullingMode)EditorGUILayout.EnumPopup("Culling Mode", cullingMode);
            //値が変化したらマテリアルに書き込み.
            if(cullingMode == _CullingMode.CullingOff){
                material.SetFloat("_CullMode",0);
            }else if(cullingMode == _CullingMode.FrontCulling){
                material.SetFloat("_CullMode",1);
            }else{
                material.SetFloat("_CullMode",2);
            }
        }

        void GUI_SetStencilNo(Material material){
            GUILayout.Label("For _StencilMask or _StencilOut Shader", EditorStyles.boldLabel);
            _StencilNo_Setting = material.GetInt("_StencilNo");
            int _Current_StencilNo = _StencilNo_Setting;
            _Current_StencilNo = (int)EditorGUILayout.IntField("Stencil No.", _Current_StencilNo);
            if(_StencilNo_Setting != _Current_StencilNo){
                material.SetInt("_StencilNo",_Current_StencilNo);
            }
        }

        void GUI_SetClippingMask(Material material){
            GUILayout.Label("For _Clipping or _TransClipping Shader", EditorStyles.boldLabel);
            m_MaterialEditor.TexturePropertySingleLine(Styles.clippingMaskText, clippingMask);
        
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.PrefixLabel("Inverse Clipping Mask");
            //GUILayout.Space(60);
            if(material.GetFloat("_Inverse_Clipping") == 0){
                if (GUILayout.Button("Off",shortButtonStyle))
                {
                    material.SetFloat("_Inverse_Clipping",1);
                }
            }else{
                if (GUILayout.Button("Active",shortButtonStyle))
                {
                    material.SetFloat("_Inverse_Clipping",0);
                }
            }
            EditorGUILayout.EndHorizontal();
        
            m_MaterialEditor.RangeProperty(clipping_Level, "Clipping Level");
        }

        void GUI_SetTransparencySetting(Material material){

            GUILayout.Label("For _TransClipping Shader", EditorStyles.boldLabel);
            m_MaterialEditor.RangeProperty(tweak_transparency, "Transparency Level");

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.PrefixLabel("Use BaseMap α as Clipping Mask");
            //GUILayout.Space(60);
            if(material.GetFloat("_IsBaseMapAlphaAsClippingMask") == 0){
                if (GUILayout.Button("Off",shortButtonStyle))
                {
                    material.SetFloat("_IsBaseMapAlphaAsClippingMask",1);
                }
            }else{
                if (GUILayout.Button("Active",shortButtonStyle))
                {
                    material.SetFloat("_IsBaseMapAlphaAsClippingMask",0);
                }
            }
            EditorGUILayout.EndHorizontal();
        }

        void GUI_OptionMenu(Material material){
            GUILayout.Label("Option Menu", EditorStyles.boldLabel);
            if(material.HasProperty("_simpleUI")){
                if(material.GetInt("_simpleUI") == 1){
                    _SimpleUI = true; //UTS2 Custom GUI Biginner
                }
                else{
                    _SimpleUI = false; //UTS2 Custom GUI Pro
                }
            }

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.PrefixLabel("Current UI Type");
            //GUILayout.Space(60);
            if(_SimpleUI == false) {
                if (GUILayout.Button("Pro / Full Control",middleButtonStyle))
                {
                    material.SetInt("_simpleUI",1); //UTS2 Custom GUI Biginner
                }
            }else{
                if (GUILayout.Button("Biginner",middleButtonStyle))
                {
                    material.SetInt("_simpleUI",0); //UTS2 Custom GUI Pro
                }
            }
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.PrefixLabel("VRChat Recommendation");
            //GUILayout.Space(60);
            if (GUILayout.Button("Apply Settings",middleButtonStyle))
            {
                Set_Vrchat_Recommendation(material);
                _Use_VrcRecommend = true;
            }
            EditorGUILayout.EndHorizontal();
            if(_Use_VrcRecommend){
                EditorGUILayout.HelpBox("UTS2 : Applied VRChat Recommended Settings.",MessageType.Info);
            }

        }

        void Set_Vrchat_Recommendation(Material material)
        {
            material.SetFloat("_Is_LightColor_Base",1);
            material.SetFloat("_Is_LightColor_1st_Shade",1);
            material.SetFloat("_Is_LightColor_2nd_Shade",1);
            material.SetFloat("_Is_LightColor_HighColor",1);
            material.SetFloat("_Is_LightColor_RimLight",1);
            material.SetFloat("_Is_LightColor_Ap_RimLight",1);
            material.SetFloat("_Is_LightColor_MatCap",1);
            if(material.HasProperty("_AngelRing")){//AngelRingがある場合.
                material.SetFloat("_Is_LightColor_AR",1);
            }
            material.SetFloat("_Set_SystemShadowsToBase",1);
            material.SetFloat("_Is_Filter_HiCutPointLightColor",1);

            material.SetFloat("_CameraRolling_Stabilizer",1);
            material.SetFloat("_Is_Ortho",0);

            if(_HasOutline){
                material.SetFloat("_Is_BlendBaseColor",1);
            }
            material.SetFloat("_GI_Intensity",0);
            material.SetFloat("_Unlit_Intensity",1);
            material.SetFloat("_Is_Filter_LightColor",1);
        }

        void GUI_BasicThreeColors(Material material)
        {
            GUILayout.Label("3 Basic Colors Settings : Textures × Colors", EditorStyles.boldLabel);

            EditorGUILayout.BeginHorizontal();
                m_MaterialEditor.TexturePropertySingleLine(Styles.baseColorText, mainTex, baseColor);
                if(material.GetFloat("_Use_BaseAs1st") == 0){
                    if (GUILayout.Button("No Sharing",middleButtonStyle))
                    {
                        material.SetFloat("_Use_BaseAs1st",1);
                    }
                }else{
                    if (GUILayout.Button("With 1st ShadeMap",middleButtonStyle))
                    {
                        material.SetFloat("_Use_BaseAs1st",0);
                    }
                }
                GUILayout.Space(60);
            EditorGUILayout.EndHorizontal();
            
            EditorGUILayout.BeginHorizontal();
                m_MaterialEditor.TexturePropertySingleLine(Styles.firstShadeColorText, firstShadeMap, firstShadeColor);
                if(material.GetFloat("_Use_1stAs2nd") == 0){
                    if (GUILayout.Button("No Sharing",middleButtonStyle))
                    {
                        material.SetFloat("_Use_1stAs2nd",1);
                    }
                }else{
                    if (GUILayout.Button("With 2nd ShadeMap",middleButtonStyle))
                    {
                        material.SetFloat("_Use_1stAs2nd",0);
                    }
                }
                GUILayout.Space(60);
            EditorGUILayout.EndHorizontal();

            m_MaterialEditor.TexturePropertySingleLine(Styles.secondShadeColorText, secondShadeMap, secondShadeColor);
            
            EditorGUILayout.Space();

            //Line();
            //EditorGUILayout.Space();

            // _SharingTextures_Foldout = FoldoutSubMenu(_SharingTextures_Foldout, "● Sharing Textures");
            // if(_SharingTextures_Foldout)
            // {
            //     GUILayout.Label("Sharing Textures", EditorStyles.boldLabel);

            //     EditorGUILayout.BeginHorizontal();
            //         EditorGUILayout.PrefixLabel("1st_ShadeMap");

            //         if(material.GetFloat("_Use_BaseAs1st") == 0){
            //             if (GUILayout.Button("No Sharing",shortButtonStyle))
            //             {
            //                 material.SetFloat("_Use_BaseAs1st",1);
            //             }
            //         }else{
            //             if (GUILayout.Button("Sharing BaseMap",shortButtonStyle))
            //             {
            //                 material.SetFloat("_Use_BaseAs1st",0);
            //             }
            //         }
            //     EditorGUILayout.EndHorizontal();

            //     EditorGUILayout.BeginHorizontal();
            //         EditorGUILayout.PrefixLabel("2nd_ShadeMap");
            //         if(material.GetFloat("_Use_1stAs2nd") == 0){
            //             if (GUILayout.Button("No Sharing",shortButtonStyle))
            //             {
            //                 material.SetFloat("_Use_1stAs2nd",1);
            //             }
            //         }else{
            //             if (GUILayout.Button("Sharing 1st_ShadeMap",shortButtonStyle))
            //             {
            //                 material.SetFloat("_Use_1stAs2nd",0);
            //             }
            //         }
            //     EditorGUILayout.EndHorizontal();

            //     EditorGUILayout.Space();
            //     //Line();
            // }

            //EditorGUILayout.Space();

            _NormalMap_Foldout = FoldoutSubMenu(_NormalMap_Foldout, "● NormalMap Settings");
            if(_NormalMap_Foldout)
            {
                //GUILayout.Label("NormalMap Settings", EditorStyles.boldLabel);
                m_MaterialEditor.TexturePropertySingleLine(Styles.normalMapText, normalMap, bumpScale);
                m_MaterialEditor.TextureScaleOffsetProperty(normalMap);

                //EditorGUI.indentLevel++;

                GUILayout.Label("NormalMap Effectiveness", EditorStyles.boldLabel);
                EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.PrefixLabel("3 Basic Colors");
                    //GUILayout.Space(60);
                    if(material.GetFloat("_Is_NormalMapToBase") == 0){
                        if (GUILayout.Button("Off",shortButtonStyle))
                        {
                            material.SetFloat("_Is_NormalMapToBase",1);
                        }
                    }else{
                        if (GUILayout.Button("Active",shortButtonStyle))
                        {
                            material.SetFloat("_Is_NormalMapToBase",0);
                        }
                    }
                EditorGUILayout.EndHorizontal();

                EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.PrefixLabel("HighColor");
                    //GUILayout.Space(60);
                    if(material.GetFloat("_Is_NormalMapToHighColor") == 0){
                        if (GUILayout.Button("Off",shortButtonStyle))
                        {
                            material.SetFloat("_Is_NormalMapToHighColor",1);
                        }
                    }else{
                        if (GUILayout.Button("Active",shortButtonStyle))
                        {
                            material.SetFloat("_Is_NormalMapToHighColor",0);
                        }
                    }
                EditorGUILayout.EndHorizontal();

                EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.PrefixLabel("RimLight");
                    //GUILayout.Space(60);
                    if(material.GetFloat("_Is_NormalMapToRimLight") == 0){
                        if (GUILayout.Button("Off",shortButtonStyle))
                        {
                            material.SetFloat("_Is_NormalMapToRimLight",1);
                        }
                    }else{
                        if (GUILayout.Button("Active",shortButtonStyle))
                        {
                            material.SetFloat("_Is_NormalMapToRimLight",0);
                        }
                    }
                EditorGUILayout.EndHorizontal();

                //EditorGUI.indentLevel--;
                EditorGUILayout.Space();
            }

            _ShadowControlMaps_Foldout = FoldoutSubMenu(_ShadowControlMaps_Foldout, "● Shadow Control Maps");
            if (_ShadowControlMaps_Foldout)
            {
                GUI_ShadowControlMaps(material);
                EditorGUILayout.Space();
            }
        }

        void GUI_ShadowControlMaps(Material material)
        {
            if (material.HasProperty("_utsTechnique"))//DoubleWithFeather or ShadingGradeMap
            {
                if(material.GetInt("_utsTechnique") == (int)_UTS_Technique.DoubleSideWithFeather)   //DWF
                {
                    GUILayout.Label("Technipue : Double Shade With Feather", EditorStyles.boldLabel);
                    m_MaterialEditor.TexturePropertySingleLine(Styles.firstPositionMapText, set_1st_ShadePosition);
                    m_MaterialEditor.TexturePropertySingleLine(Styles.secondPositionMapText, set_2nd_ShadePosition);
                }else if(material.GetInt("_utsTechnique") == (int)_UTS_Technique.ShadingGradeMap){    //SGM
                    GUILayout.Label("Technipue : Shading Grade Map", EditorStyles.boldLabel);
                    m_MaterialEditor.TexturePropertySingleLine(Styles.shadingGradeMapText, shadingGradeMap);
                    m_MaterialEditor.RangeProperty(tweak_ShadingGradeMapLevel, "ShadingGradeMap Level");
                    m_MaterialEditor.RangeProperty(blurLevelSGM, "Blur Level of ShadingGradeMap");
                }
            }
        }

        void GUI_StepAndFeather(Material material)
        {
            // _BasicLookdevs_Foldout = FoldoutSubMenu(_BasicLookdevs_Foldout,"● Basic Lookdevs : Shading Step and Feather Settings");
            // if(_BasicLookdevs_Foldout){
                GUI_BasicLookdevs(material);
            // }

            if(!_SimpleUI){
                // _SystemShadows_Foldout = FoldoutSubMenu(_SystemShadows_Foldout, "● System Shadows : Self Shadows Receiving");
                // if(_SystemShadows_Foldout){
                    GUI_SystemShadows(material);
                // }

                if (material.HasProperty("_StepOffset"))//Mobile & Light Modeにはない項目.
                {
                    //Line();
                    //EditorGUILayout.Space();
                    _AdditionalLookdevs_Foldout = FoldoutSubMenu(_AdditionalLookdevs_Foldout,"● Additional Settings");
                    if(_AdditionalLookdevs_Foldout){
                        GUI_AdditionalLookdevs(material);
                    }
                }
            }
        }

        void GUI_SystemShadows(Material material){

                GUILayout.Label("System Shadows : Self Shadows Receiving", EditorStyles.boldLabel);

                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.PrefixLabel("Receive System Shadows");
                //GUILayout.Space(60);
                    if(material.GetFloat("_Set_SystemShadowsToBase") == 0){
                        if (GUILayout.Button("Off",shortButtonStyle))
                        {
                            material.SetFloat("_Set_SystemShadowsToBase",1);
                        }
                    }else{
                        if (GUILayout.Button("Active",shortButtonStyle))
                        {
                            material.SetFloat("_Set_SystemShadowsToBase",0);
                        }
                    }
                EditorGUILayout.EndHorizontal();

                if(material.GetFloat("_Set_SystemShadowsToBase") == 1){
                    EditorGUI.indentLevel++;
                    m_MaterialEditor.RangeProperty(tweak_SystemShadowsLevel, "System Shadows Level");
                    EditorGUI.indentLevel--;
                    EditorGUILayout.Space();
                }
                EditorGUILayout.Space();
        }

        void GUI_BasicLookdevs(Material material){
                if (material.HasProperty("_utsTechnique"))//DoubleWithFeather or ShadingGradeMap
                {
                    if(material.GetInt("_utsTechnique") == (int)_UTS_Technique.DoubleSideWithFeather)   //DWF
                    {
                        GUILayout.Label("Technipue : Double Shade With Feather", EditorStyles.boldLabel);
                        m_MaterialEditor.RangeProperty(baseColor_Step, "BaseColor Step");
                        m_MaterialEditor.RangeProperty(baseShade_Feather, "Base/Shade Feather");
                        m_MaterialEditor.RangeProperty(shadeColor_Step, "ShadeColor Step");
                        m_MaterialEditor.RangeProperty(first2nd_Shades_Feather, "1st/2nd_Shades Feather");
                        //ShadingGradeMap系と変数を共有.
                        material.SetFloat("_1st_ShadeColor_Step", material.GetFloat("_BaseColor_Step"));
                        material.SetFloat("_1st_ShadeColor_Feather", material.GetFloat("_BaseShade_Feather"));
                        material.SetFloat("_2nd_ShadeColor_Step", material.GetFloat("_ShadeColor_Step"));
                        material.SetFloat("_2nd_ShadeColor_Feather", material.GetFloat("_1st2nd_Shades_Feather"));
                    }else if(material.GetInt("_utsTechnique") == (int)_UTS_Technique.ShadingGradeMap){    //SGM
                        GUILayout.Label("Technipue : Shading Grade Map", EditorStyles.boldLabel);
                        m_MaterialEditor.RangeProperty(first_ShadeColor_Step, "1st ShaderColor Step");
                        m_MaterialEditor.RangeProperty(first_ShadeColor_Feather, "1st ShadeColor Feather");
                        m_MaterialEditor.RangeProperty(second_ShadeColor_Step, "2nd ShadeColor Step");
                        m_MaterialEditor.RangeProperty(second_ShadeColor_Feather, "2nd ShadeColor Feather");
                        //DoubleWithFeather系と変数を共有.
                        material.SetFloat("_BaseColor_Step", material.GetFloat("_1st_ShadeColor_Step"));
                        material.SetFloat("_BaseShade_Feather", material.GetFloat("_1st_ShadeColor_Feather"));
                        material.SetFloat("_ShadeColor_Step", material.GetFloat("_2nd_ShadeColor_Step"));
                        material.SetFloat("_1st2nd_Shades_Feather", material.GetFloat("_2nd_ShadeColor_Feather"));
                    }else{
                        // OutlineObj.
                        return;
                    }
                }
                EditorGUILayout.Space();
        }

        void GUI_AdditionalLookdevs(Material material){
            GUILayout.Label("    Settings for PointLights in ForwardAdd Pass");
            EditorGUI.indentLevel++;
            m_MaterialEditor.RangeProperty(stepOffset, "Step Offset for PointLights");
                
            EditorGUILayout.BeginHorizontal();
                EditorGUILayout.PrefixLabel("PointLights Hi-Cut Filter");
                //GUILayout.Space(60);
                if(material.GetFloat("_Is_Filter_HiCutPointLightColor") == 0){
                    if (GUILayout.Button("Off",shortButtonStyle))
                    {
                        material.SetFloat("_Is_Filter_HiCutPointLightColor",1);
                    }
                }else{
                    if (GUILayout.Button("Active",shortButtonStyle))
                    {
                        material.SetFloat("_Is_Filter_HiCutPointLightColor",0);
                    }
                }
            EditorGUILayout.EndHorizontal();

            EditorGUI.indentLevel--;
            EditorGUILayout.Space();
        }

        void GUI_HighColor(Material material)
        {
            m_MaterialEditor.TexturePropertySingleLine(Styles.highColorText, highColor_Tex, highColor);
            m_MaterialEditor.RangeProperty(highColor_Power, "HighColor Power");

            if(!_SimpleUI){
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.PrefixLabel("Specular Mode");
                //GUILayout.Space(60);
                    if(material.GetFloat("_Is_SpecularToHighColor") == 0){
                        if (GUILayout.Button("Off",shortButtonStyle))
                        {
                            material.SetFloat("_Is_SpecularToHighColor",1);
                            material.SetFloat("_Is_BlendAddToHiColor",1);
                        }
                    }else{
                        if (GUILayout.Button("Active",shortButtonStyle))
                        {
                            material.SetFloat("_Is_SpecularToHighColor",0);
                        }
                    }
                EditorGUILayout.EndHorizontal();

                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.PrefixLabel("Color Blend Mode");
                //GUILayout.Space(60);
                    if(material.GetFloat("_Is_BlendAddToHiColor") == 0){
                        if (GUILayout.Button("Multiply",shortButtonStyle))
                        {
                            material.SetFloat("_Is_BlendAddToHiColor",1);
                        }
                    }else{
                        if (GUILayout.Button("Additive",shortButtonStyle))
                        {
                            //加算モードはスペキュラオフでしか使えない.
                            if(material.GetFloat("_Is_SpecularToHighColor") == 1)
                            {
                                material.SetFloat("_Is_BlendAddToHiColor",1);
                            }else{
                                material.SetFloat("_Is_BlendAddToHiColor",0);
                            }
                        }
                    }
                EditorGUILayout.EndHorizontal();

                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.PrefixLabel("ShadowMask on HihgColor");
                //GUILayout.Space(60);
                    if(material.GetFloat("_Is_UseTweakHighColorOnShadow") == 0){
                        if (GUILayout.Button("Off",shortButtonStyle))
                        {
                            material.SetFloat("_Is_UseTweakHighColorOnShadow",1);
                        }
                    }else{
                        if (GUILayout.Button("Active",shortButtonStyle))
                        {
                            material.SetFloat("_Is_UseTweakHighColorOnShadow",0);
                        }
                    }
                EditorGUILayout.EndHorizontal();

                if(material.GetFloat("_Is_UseTweakHighColorOnShadow") == 1){
                    EditorGUI.indentLevel++;
                    m_MaterialEditor.RangeProperty(tweakHighColorOnShadow, "HighColor Power on Shadow");
                    EditorGUI.indentLevel--;
                }
            }

            EditorGUILayout.Space();
            //Line();
            //EditorGUILayout.Space();

            GUILayout.Label("    HighColor Mask", EditorStyles.boldLabel);
            EditorGUI.indentLevel++;
            m_MaterialEditor.TexturePropertySingleLine(Styles.highColorMaskText, set_HighColorMask);
            m_MaterialEditor.RangeProperty(tweak_HighColorMaskLevel, "HighColor Mask Level");
            EditorGUI.indentLevel--;

            EditorGUILayout.Space();
        }

        void GUI_RimLight(Material material)
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.PrefixLabel("RimLight");
            //GUILayout.Space(60);
                if(material.GetFloat("_RimLight") == 0){
                    if (GUILayout.Button("Off",shortButtonStyle))
                    {
                        material.SetFloat("_RimLight",1);
                    }
                }else{
                    if (GUILayout.Button("Active",shortButtonStyle))
                    {
                        material.SetFloat("_RimLight",0);
                    }
                }
            EditorGUILayout.EndHorizontal();

            if(material.GetFloat("_RimLight") == 1){
                EditorGUI.indentLevel++;
                    GUILayout.Label("    RimLight Settings", EditorStyles.boldLabel);
                    m_MaterialEditor.ColorProperty(rimLightColor, "RimLight Color");
                    m_MaterialEditor.RangeProperty(rimLight_Power, "RimLight Power");

                    if(!_SimpleUI){
                        m_MaterialEditor.RangeProperty(rimLight_InsideMask, "RimLight Inside Mask");

                        EditorGUILayout.BeginHorizontal();
                        EditorGUILayout.PrefixLabel("RimLight FeatherOff");
                        //GUILayout.Space(60);
                            if(material.GetFloat("_RimLight_FeatherOff") == 0){
                                if (GUILayout.Button("Off",shortButtonStyle))
                                {
                                    material.SetFloat("_RimLight_FeatherOff",1);
                                }
                            }else{
                                if (GUILayout.Button("Active",shortButtonStyle))
                                {
                                    material.SetFloat("_RimLight_FeatherOff",0);
                                }
                            }
                        EditorGUILayout.EndHorizontal();
                        EditorGUILayout.BeginHorizontal();
                        EditorGUILayout.PrefixLabel("LightDirection Mask");
                        //GUILayout.Space(60);
                            if(material.GetFloat("_LightDirection_MaskOn") == 0){
                                if (GUILayout.Button("Off",shortButtonStyle))
                                {
                                    material.SetFloat("_LightDirection_MaskOn",1);
                                }
                            }else{
                                if (GUILayout.Button("Active",shortButtonStyle))
                                {
                                    material.SetFloat("_LightDirection_MaskOn",0);
                                }
                            }
                        EditorGUILayout.EndHorizontal();

                        if(material.GetFloat("_LightDirection_MaskOn") == 1){
                            EditorGUI.indentLevel++;
                                m_MaterialEditor.RangeProperty(tweak_LightDirection_MaskLevel, "LightDirection MaskLevel");
                            
                                EditorGUILayout.BeginHorizontal();
                                EditorGUILayout.PrefixLabel("Antipodean(Ap)_RimLight");
                                //GUILayout.Space(60);
                                    if(material.GetFloat("_Add_Antipodean_RimLight") == 0){
                                        if (GUILayout.Button("Off",shortButtonStyle))
                                        {
                                            material.SetFloat("_Add_Antipodean_RimLight",1);
                                        }
                                    }else{
                                        if (GUILayout.Button("Active",shortButtonStyle))
                                        {
                                            material.SetFloat("_Add_Antipodean_RimLight",0);
                                        }
                                    }
                                EditorGUILayout.EndHorizontal();

                                if(material.GetFloat("_Add_Antipodean_RimLight") == 1)
                                {
                                    EditorGUI.indentLevel++;
                                        GUILayout.Label("    Ap_RimLight Settings", EditorStyles.boldLabel);
                                        m_MaterialEditor.ColorProperty(ap_RimLightColor, "Ap_RimLight Color");
                                        m_MaterialEditor.RangeProperty(ap_RimLight_Power, "Ap_RimLight Power");

                                        EditorGUILayout.BeginHorizontal();
                                        EditorGUILayout.PrefixLabel("Ap_RimLight FeatherOff");
                                        //GUILayout.Space(60);
                                            if(material.GetFloat("_Ap_RimLight_FeatherOff") == 0){
                                                if (GUILayout.Button("Off",shortButtonStyle))
                                                {
                                                    material.SetFloat("_Ap_RimLight_FeatherOff",1);
                                                }
                                            }else{
                                                if (GUILayout.Button("Active",shortButtonStyle))
                                                {
                                                    material.SetFloat("_Ap_RimLight_FeatherOff",0);
                                                }
                                            }
                                        EditorGUILayout.EndHorizontal();
                                    EditorGUI.indentLevel--;
                                }
                            
                                EditorGUI.indentLevel--;

                            }//Light Direction Mask ON

                    }

                    //EditorGUI.indentLevel++;

                    EditorGUILayout.Space();
                    //Line();
                    //EditorGUILayout.Space();

                    GUILayout.Label("    RimLight Mask", EditorStyles.boldLabel);
                    m_MaterialEditor.TexturePropertySingleLine(Styles.rimLightMaskText,set_RimLightMask);
                    m_MaterialEditor.RangeProperty(tweak_RimLightMaskLevel, "RimLight Mask Level");

                    //EditorGUI.indentLevel--;
        
                EditorGUI.indentLevel--;
                EditorGUILayout.Space();
            }
        }

        void GUI_MatCap(Material material)
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.PrefixLabel("MatCap");
            //GUILayout.Space(60);
                if(material.GetFloat("_MatCap") == 0){
                    if (GUILayout.Button("Off",shortButtonStyle))
                    {
                        material.SetFloat("_MatCap",1);
                    }
                }else{
                    if (GUILayout.Button("Active",shortButtonStyle))
                    {
                        material.SetFloat("_MatCap",0);
                    }
                }
            EditorGUILayout.EndHorizontal();

            if(material.GetFloat("_MatCap") == 1){
                GUILayout.Label("    MatCap Settings", EditorStyles.boldLabel);
                m_MaterialEditor.TexturePropertySingleLine(Styles.matCapSamplerText, matCap_Sampler, matCapColor);
                EditorGUI.indentLevel++;
                m_MaterialEditor.TextureScaleOffsetProperty(matCap_Sampler);

                if(!_SimpleUI){

                    m_MaterialEditor.RangeProperty(blurLevelMatcap, "Blur Level of MatCap Sampler");

                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.PrefixLabel("Color Blend Mode");
                    //GUILayout.Space(60);
                        if(material.GetFloat("_Is_BlendAddToMatCap") == 0){
                            if (GUILayout.Button("Multipy",shortButtonStyle))
                            {
                                material.SetFloat("_Is_BlendAddToMatCap",1);
                            }
                        }else{
                            if (GUILayout.Button("Additive",shortButtonStyle))
                            {
                                    material.SetFloat("_Is_BlendAddToMatCap",0);
                            }
                        }
                    EditorGUILayout.EndHorizontal();

                    m_MaterialEditor.RangeProperty(tweak_MatCapUV, "Scale MatCapUV");
                    m_MaterialEditor.RangeProperty(rotate_MatCapUV, "Rotate MatCapUV");

                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.PrefixLabel("CameraRolling Stabilizer");
                    //GUILayout.Space(60);
                        if(material.GetFloat("_CameraRolling_Stabilizer") == 0){
                            if (GUILayout.Button("Off",shortButtonStyle))
                            {
                                material.SetFloat("_CameraRolling_Stabilizer",1);
                            }
                        }else{
                            if (GUILayout.Button("Active",shortButtonStyle))
                            {
                                    material.SetFloat("_CameraRolling_Stabilizer",0);
                            }
                        }
                    EditorGUILayout.EndHorizontal();

                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.PrefixLabel("NormalMap for MatCap");
                    //GUILayout.Space(60);
                        if(material.GetFloat("_Is_NormalMapForMatCap") == 0){
                            if (GUILayout.Button("Off",shortButtonStyle))
                            {
                                material.SetFloat("_Is_NormalMapForMatCap",1);
                            }
                        }else{
                            if (GUILayout.Button("Active",shortButtonStyle))
                            {
                                material.SetFloat("_Is_NormalMapForMatCap",0);
                            }
                        }
                    EditorGUILayout.EndHorizontal();
                    if(material.GetFloat("_Is_NormalMapForMatCap") == 1){
                        EditorGUI.indentLevel++;
                            GUILayout.Label("       NormalMap for MatCap as SpecularMask", EditorStyles.boldLabel);
                            m_MaterialEditor.TexturePropertySingleLine(Styles.normalMapText, normalMapForMatCap, bumpScaleMatcap);
                            m_MaterialEditor.TextureScaleOffsetProperty(normalMapForMatCap);
                            m_MaterialEditor.RangeProperty(rotate_NormalMapForMatCapUV, "Rotate NormalMapUV");
                        EditorGUI.indentLevel--;
                    }

                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.PrefixLabel("MatCap on Shadow");
                    //GUILayout.Space(60);
                        if(material.GetFloat("_Is_UseTweakMatCapOnShadow") == 0){
                            if (GUILayout.Button("Off",shortButtonStyle))
                            {
                                material.SetFloat("_Is_UseTweakMatCapOnShadow",1);
                            }
                        }else{
                            if (GUILayout.Button("Active",shortButtonStyle))
                            {
                                material.SetFloat("_Is_UseTweakMatCapOnShadow",0);
                            }
                        }
                    EditorGUILayout.EndHorizontal();
                    if(material.GetFloat("_Is_UseTweakMatCapOnShadow") == 1){
                        EditorGUI.indentLevel++;
                            m_MaterialEditor.RangeProperty(tweakMatCapOnShadow, "MatCap Power on Shadow");
                        EditorGUI.indentLevel--;
                    }

                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.PrefixLabel("MatCap Projection Camera");
                    //GUILayout.Space(60);
                        if(material.GetFloat("_Is_Ortho") == 0){
                            if (GUILayout.Button("Perspective",middleButtonStyle))
                            {
                                material.SetFloat("_Is_Ortho",1);
                            }
                        }else{
                            if (GUILayout.Button("Orthographic",middleButtonStyle))
                            {
                                material.SetFloat("_Is_Ortho",0);
                            }
                        }
                    EditorGUILayout.EndHorizontal();
                }

                EditorGUILayout.Space();
                //Line();
                //EditorGUILayout.Space();

                GUILayout.Label("    MatCap Mask", EditorStyles.boldLabel);
                m_MaterialEditor.TexturePropertySingleLine(Styles.matCapMaskText, set_MatcapMask);
                m_MaterialEditor.TextureScaleOffsetProperty(set_MatcapMask);
                m_MaterialEditor.RangeProperty(tweak_MatcapMaskLevel, "MatCap Mask Level");

                EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.PrefixLabel("Inverse Matcap Mask");
                    //GUILayout.Space(60);
                        if(material.GetFloat("_Inverse_MatcapMask") == 0){
                            if (GUILayout.Button("Off",shortButtonStyle))
                            {
                                material.SetFloat("_Inverse_MatcapMask",1);
                            }
                        }else{
                            if (GUILayout.Button("Active",shortButtonStyle))
                            {
                                material.SetFloat("_Inverse_MatcapMask",0);
                            }
                        }
                EditorGUILayout.EndHorizontal();

                EditorGUI.indentLevel--;
            } // MatCap == 1

            //EditorGUILayout.Space();
        }

        void GUI_AngelRing(Material material)
        {
            EditorGUILayout.BeginHorizontal();
                EditorGUILayout.PrefixLabel("AngelRing Projection");
                //GUILayout.Space(60);
                    if(material.GetFloat("_AngelRing") == 0){
                        if (GUILayout.Button("Off",shortButtonStyle))
                        {
                            material.SetFloat("_AngelRing",1);
                        }
                    }else{
                        if (GUILayout.Button("Active",shortButtonStyle))
                        {
                            material.SetFloat("_AngelRing",0);
                        }
                    }
            EditorGUILayout.EndHorizontal();

            if(material.GetFloat("_AngelRing") == 1){
                GUILayout.Label("    AngelRing Sampler Settings", EditorStyles.boldLabel);
                m_MaterialEditor.TexturePropertySingleLine(Styles.angelRingText, angelRing_Sampler, angelRing_Color);
                EditorGUI.indentLevel++;
                //m_MaterialEditor.TextureScaleOffsetProperty(angelRing_Sampler);
                m_MaterialEditor.RangeProperty(ar_OffsetU, "Offset U");
                m_MaterialEditor.RangeProperty(ar_OffsetV, "Offset V");
                
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.PrefixLabel("Use α channel as Clipping Mask");
                //GUILayout.Space(60);
                    if(material.GetFloat("_ARSampler_AlphaOn") == 0){
                        if (GUILayout.Button("Off",shortButtonStyle))
                        {
                            material.SetFloat("_ARSampler_AlphaOn",1);
                        }
                    }else{
                        if (GUILayout.Button("Active",shortButtonStyle))
                        {
                            material.SetFloat("_ARSampler_AlphaOn",0);
                        }
                    }
                EditorGUILayout.EndHorizontal();
                EditorGUI.indentLevel--;

            }
        }

        void GUI_Emissive(Material material)
        {
            GUILayout.Label("Emissive Tex × HDR Color", EditorStyles.boldLabel);
            GUILayout.Label("(Bloom Post-Processing Effect necessary)");
            EditorGUILayout.Space();
            m_MaterialEditor.TexturePropertySingleLine(Styles.emissiveTexText, emissive_Tex, emissive_Color);
            m_MaterialEditor.TextureScaleOffsetProperty(emissive_Tex);
            
            EditorGUILayout.Space();
        }


        void GUI_Outline(Material material)
        {
            //Shaderプロパティ [KeywordEnum(NML,POS)] をEumPopupで表現する.
            //マテリアル内のアウトラインモードの設定を読み込み.
            int _OutlineMode_Setting = material.GetInt("_OUTLINE");
            //Enum形式に変換して、outlineMode変数に保持しておく.
            if ((int)_OutlineMode.NormalDirection == _OutlineMode_Setting){
                outlineMode = _OutlineMode.NormalDirection;
            }else if((int)_OutlineMode.PositionScaling == _OutlineMode_Setting){
                outlineMode = _OutlineMode.PositionScaling;
            }
            //EnumPopupでGUI記述.
            outlineMode = (_OutlineMode)EditorGUILayout.EnumPopup("Outline Mode", outlineMode);
            //値が変化したらマテリアルに書き込み.
            if(outlineMode == _OutlineMode.NormalDirection){
                material.SetFloat("_OUTLINE",0);
                //UTCS_Outline.cginc側のキーワードもトグル入れ替え.
                material.EnableKeyword("_OUTLINE_NML");
                material.DisableKeyword("_OUTLINE_POS");
            }else if(outlineMode == _OutlineMode.PositionScaling){
                material.SetFloat("_OUTLINE",1);
                material.EnableKeyword("_OUTLINE_POS");
                material.DisableKeyword("_OUTLINE_NML");
            }

            m_MaterialEditor.FloatProperty(outline_Width, "Outline Width");
            m_MaterialEditor.ColorProperty(outline_Color, "Outline Color");

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.PrefixLabel("Blend BaseColor to Outline");
            //GUILayout.Space(60);
                if(material.GetFloat("_Is_BlendBaseColor") == 0){
                    if (GUILayout.Button("Off",shortButtonStyle))
                    {
                        material.SetFloat("_Is_BlendBaseColor",1);
                    }
                }else{
                    if (GUILayout.Button("Active",shortButtonStyle))
                    {
                        material.SetFloat("_Is_BlendBaseColor",0);
                    }
                }
            EditorGUILayout.EndHorizontal();

            m_MaterialEditor.TexturePropertySingleLine(Styles.outlineSamplerText, outline_Sampler);
            m_MaterialEditor.FloatProperty(offset_Z, "Offset Outline with Camera Z-axis");

            if(!_SimpleUI){

                _AdvancedOutline_Foldout = FoldoutSubMenu(_AdvancedOutline_Foldout, "● Advanced Outline Settings");
                if(_AdvancedOutline_Foldout){
                    EditorGUI.indentLevel++;
                    GUILayout.Label("    Camera Distance for Outline Width");
                    m_MaterialEditor.FloatProperty(farthest_Distance, "● Farthest Distance to vanish");
                    m_MaterialEditor.FloatProperty(nearest_Distance, "● Nearest Distance to draw with Outline Width");
                    EditorGUI.indentLevel--;

                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.PrefixLabel("Use Outline Texture");
                    //GUILayout.Space(60);
                    if(material.GetFloat("_Is_OutlineTex") == 0){
                        if (GUILayout.Button("Off",shortButtonStyle))
                        {
                            material.SetFloat("_Is_OutlineTex",1);
                        }
                        EditorGUILayout.EndHorizontal();
                    }else{
                        if (GUILayout.Button("Active",shortButtonStyle))
                        {
                            material.SetFloat("_Is_OutlineTex",0);
                        }
                        EditorGUILayout.EndHorizontal();
                        m_MaterialEditor.TexturePropertySingleLine(Styles.outlineTexText, outlineTex);
                    }

                    if(outlineMode == _OutlineMode.NormalDirection){
                        EditorGUILayout.BeginHorizontal();
                        EditorGUILayout.PrefixLabel("Use Baked Normal for Outline");
                        //GUILayout.Space(60);
                        if(material.GetFloat("_Is_BakedNormal") == 0){
                            if (GUILayout.Button("Off",shortButtonStyle))
                            {
                                material.SetFloat("_Is_BakedNormal",1);
                            }
                            EditorGUILayout.EndHorizontal();
                        }else{
                            if (GUILayout.Button("Active",shortButtonStyle))
                            {
                                material.SetFloat("_Is_BakedNormal",0);
                            }
                            EditorGUILayout.EndHorizontal();
                            m_MaterialEditor.TexturePropertySingleLine(Styles.bakedNormalOutlineText, bakedNormal);
                        }
                    }
                }
            }
        }

        void GUI_Tessellation(Material material)
        {
            GUILayout.Label("Technique : DX11 Phong Tessellation", EditorStyles.boldLabel);
            m_MaterialEditor.RangeProperty(tessEdgeLength, "Edge Length");
            m_MaterialEditor.RangeProperty(tessPhongStrength, "Phong Strength");
            m_MaterialEditor.RangeProperty(tessExtrusionAmount, "Extrusion Amount");

            EditorGUILayout.Space();
        }

        void GUI_LightColorContribution(Material material)
        {
            GUILayout.Label("Realtime LightColor Contribution to each colors", EditorStyles.boldLabel);
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.PrefixLabel("Base Color");
            //GUILayout.Space(60);
                if(material.GetFloat("_Is_LightColor_Base") == 0){
                    if (GUILayout.Button("Off",shortButtonStyle))
                    {
                        material.SetFloat("_Is_LightColor_Base",1);
                    }
                }else{
                    if (GUILayout.Button("Active",shortButtonStyle))
                    {
                        material.SetFloat("_Is_LightColor_Base",0);
                    }
                }
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.PrefixLabel("1st ShadeColor");
            //GUILayout.Space(60);
                if(material.GetFloat("_Is_LightColor_1st_Shade") == 0){
                    if (GUILayout.Button("Off",shortButtonStyle))
                    {
                        material.SetFloat("_Is_LightColor_1st_Shade",1);
                    }
                }else{
                    if (GUILayout.Button("Active",shortButtonStyle))
                    {
                        material.SetFloat("_Is_LightColor_1st_Shade",0);
                    }
                }
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.PrefixLabel("2nd ShadeColor");
            //GUILayout.Space(60);
                if(material.GetFloat("_Is_LightColor_2nd_Shade") == 0){
                    if (GUILayout.Button("Off",shortButtonStyle))
                    {
                        material.SetFloat("_Is_LightColor_2nd_Shade",1);
                    }
                }else{
                    if (GUILayout.Button("Active",shortButtonStyle))
                    {
                        material.SetFloat("_Is_LightColor_2nd_Shade",0);
                    }
                }
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.PrefixLabel("HighColor");
            //GUILayout.Space(60);
                if(material.GetFloat("_Is_LightColor_HighColor") == 0){
                    if (GUILayout.Button("Off",shortButtonStyle))
                    {
                        material.SetFloat("_Is_LightColor_HighColor",1);
                    }
                }else{
                    if (GUILayout.Button("Active",shortButtonStyle))
                    {
                        material.SetFloat("_Is_LightColor_HighColor",0);
                    }
                }
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.PrefixLabel("RimLight");
            //GUILayout.Space(60);
                if(material.GetFloat("_Is_LightColor_RimLight") == 0){
                    if (GUILayout.Button("Off",shortButtonStyle))
                    {
                        material.SetFloat("_Is_LightColor_RimLight",1);
                    }
                }else{
                    if (GUILayout.Button("Active",shortButtonStyle))
                    {
                        material.SetFloat("_Is_LightColor_RimLight",0);
                    }
                }
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.PrefixLabel("Ap_RimLight");
            //GUILayout.Space(60);
                if(material.GetFloat("_Is_LightColor_Ap_RimLight") == 0){
                    if (GUILayout.Button("Off",shortButtonStyle))
                    {
                        material.SetFloat("_Is_LightColor_Ap_RimLight",1);
                    }
                }else{
                    if (GUILayout.Button("Active",shortButtonStyle))
                    {
                        material.SetFloat("_Is_LightColor_Ap_RimLight",0);
                    }
                }
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.PrefixLabel("MatCap");
            //GUILayout.Space(60);
                if(material.GetFloat("_Is_LightColor_MatCap") == 0){
                    if (GUILayout.Button("Off",shortButtonStyle))
                    {
                        material.SetFloat("_Is_LightColor_MatCap",1);
                    }
                }else{
                    if (GUILayout.Button("Active",shortButtonStyle))
                    {
                        material.SetFloat("_Is_LightColor_MatCap",0);
                    }
                }
            EditorGUILayout.EndHorizontal();

            if(material.HasProperty("_AngelRing"))//AngelRingがある場合.
            {
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.PrefixLabel("Angel Ring");
                //GUILayout.Space(60);
                    if(material.GetFloat("_Is_LightColor_AR") == 0){
                        if (GUILayout.Button("Off",shortButtonStyle))
                        {
                            material.SetFloat("_Is_LightColor_AR",1);
                        }
                    }else{
                        if (GUILayout.Button("Active",shortButtonStyle))
                        {
                            material.SetFloat("_Is_LightColor_AR",0);
                        }
                    }
                EditorGUILayout.EndHorizontal();
            }
            EditorGUILayout.Space();
        }

        void GUI_AdditionalLightingSettings(Material material)
        {
            m_MaterialEditor.RangeProperty(gi_Intensity, "GI Intensity");
            m_MaterialEditor.RangeProperty(unlit_Intensity, "Unlit Intensity");

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.PrefixLabel("SceneLights Hi-Cut Filter");
            //GUILayout.Space(60);
                if(material.GetFloat("_Is_Filter_LightColor") == 0){
                    if (GUILayout.Button("Off",shortButtonStyle))
                    {
                        material.SetFloat("_Is_Filter_LightColor",1);
                        material.SetFloat("_Is_LightColor_Base",1);
                        material.SetFloat("_Is_LightColor_1st_Shade",1);
                        material.SetFloat("_Is_LightColor_2nd_Shade",1);
                    }
                }else{
                    if (GUILayout.Button("Active",shortButtonStyle))
                    {
                        material.SetFloat("_Is_Filter_LightColor",0);
                    }
                }
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.PrefixLabel("Built-in Light Direction");
            //GUILayout.Space(60);
                if(material.GetFloat("_Is_BLD") == 0){
                    if (GUILayout.Button("Off",shortButtonStyle))
                    {
                        material.SetFloat("_Is_BLD",1);
                    }
                }else{
                    if (GUILayout.Button("Active",shortButtonStyle))
                    {
                        material.SetFloat("_Is_BLD",0);
                    }
                }
            EditorGUILayout.EndHorizontal();
            if(material.GetFloat("_Is_BLD") == 1){
                GUILayout.Label("    Built-in Light Direction Settings");
                EditorGUI.indentLevel++;
                m_MaterialEditor.RangeProperty(offset_X_Axis_BLD, "● Offset X-Axis Direction");
                m_MaterialEditor.RangeProperty(offset_Y_Axis_BLD, "● Offset Y-Axis Direction");

                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.PrefixLabel("● Inverse Z-Axis Direction");
                //GUILayout.Space(60);
                    if(material.GetFloat("_Inverse_Z_Axis_BLD") == 0){
                        if (GUILayout.Button("Off",shortButtonStyle))
                        {
                            material.SetFloat("_Inverse_Z_Axis_BLD",1);
                        }
                    }else{
                        if (GUILayout.Button("Active",shortButtonStyle))
                        {
                            material.SetFloat("_Inverse_Z_Axis_BLD",0);
                        }
                    }
                EditorGUILayout.EndHorizontal();
                EditorGUI.indentLevel--;
            }
            EditorGUILayout.Space();
        }

    } // End of UTS2GUI2
}// End of namespace UnityChan
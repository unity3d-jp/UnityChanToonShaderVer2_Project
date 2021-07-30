using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
namespace UnityEditor.Rendering.Toon
{
    public class UnitychanToonShader2UnityToonShader : EditorWindow
    {
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


        const string ShaderDefineSHADINGGRADEMAP = "_SHADINGGRADEMAP";
        const string ShaderDefineANGELRING_ON = "_IS_ANGELRING_ON";
        const string ShaderDefineANGELRING_OFF = "_IS_ANGELRING_OFF";

        const string ShaderDefineIS_TRANSCLIPPING_OFF = "_IS_TRANSCLIPPING_OFF";
        const string ShaderDefineIS_TRANSCLIPPING_ON = "_IS_TRANSCLIPPING_ON";

        const string ShaderDefineIS_CLIPPING_OFF = "_IS_CLIPPING_OFF";
        const string ShaderDefineIS_CLIPPING_MODE = "_IS_CLIPPING_MODE";
        const string ShaderDefineIS_CLIPPING_TRANSMODE = "_IS_CLIPPING_TRANSMODE";
        const string ShaderDefineIS_OUTLINE_CLIPPING_NO = "_IS_OUTLINE_CLIPPING_NO";
        const string ShaderDefineIS_OUTLINE_CLIPPING_YES = "_IS_OUTLINE_CLIPPING_YES";

        const string ShaderPropAngelRing = "_AngelRing";

        const string ShaderProp1st_ShadeColor_Step = "_1st_ShadeColor_Step";
        const string ShaderPropBaseColor_Step = "_BaseColor_Step";
        const string ShaderProp1st_ShadeColor_Feather = "_1st_ShadeColor_Feather";
        const string ShaderPropBaseShade_Feather = "_BaseShade_Feather";
        const string ShaderProp2nd_ShadeColor_Step = "_2nd_ShadeColor_Step";
        const string ShaderPropShadeColor_Step = "_ShadeColor_Step";
        const string ShaderProp2nd_ShadeColor_Feather = "_2nd_ShadeColor_Feather";
        const string ShaderProp1st2nd_Shades_Feather = "_1st2nd_Shades_Feather";
        const string ShaderPropIs_NormalMapForMatCap = "_Is_NormalMapForMatCap";
        const string ShaderPropIs_UseTweakMatCapOnShadow = "_Is_UseTweakMatCapOnShadow";
        const string ShaderPropIs_ViewCoord_Scroll = "_Is_ViewCoord_Scroll";
        const string ShaderPropIs_PingPong_Base = "_Is_PingPong_Base";

        const string ShaderPropMatCap = "_MatCap";
        const string ShaderPropClippingMode = "_ClippingMode";

        const string ShaderPropOutline = "_OUTLINE";
        const string ShaderPropIs_Ortho = "_Is_Ortho";
        const string ShaderPropGI_Intensity = "_GI_Intensity";
        const string ShaderPropCameraRolling_Stabilizer = "_CameraRolling_Stabilizer";
        const string ShaderPropIs_Filter_LightColor = "_Is_Filter_LightColor";
        const string ShaderPropUnlit_Intensity = "_Unlit_Intensity";
        const string ShaderPropStencilMode = "_StencilMode";
        const string ShaderPropStencilNo = "_StencilNo";
        const string ShaderPropTransparentEnabled = "_TransparentEnabled";
        const string ShaderPropStencilComp = "_StencilComp";
        const string ShaderPropStencilOpPass = "_StencilOpPass";
        const string ShaderPropStencilOpFail = "_StencilOpFail";

        const string ShaderPropIsLightColor_Base = "_Is_LightColor_Base";
        const string ShaderPropIs_LightColor_1st_Shade = "_Is_LightColor_1st_Shade";
        const string ShaderPropIs_LightColor_2nd_Shade = "_Is_LightColor_2nd_Shade";
        const string ShaderPropIs_LightColor_HighColor = "_Is_LightColor_HighColor";
        const string ShaderPropIs_LightColor_RimLight = "_Is_LightColor_RimLight";
        const string ShaderPropIs_LightColor_Ap_RimLight = "_Is_LightColor_Ap_RimLight";
        const string ShaderPropIs_LightColor_MatCap = "_Is_LightColor_MatCap";
        const string ShaderPropIs_LightColor_AR = "_Is_LightColor_AR";
        const string ShaderPropIs_LightColor_Outline = "_Is_LightColor_Outline";
        const string ShaderPropSetSystemShadowsToBase = "_Set_SystemShadowsToBase";
        const string ShaderPropIsFilterHiCutPointLightColor = "_Is_Filter_HiCutPointLightColor";


        const string ShaderPropAutoRenderQueue = "_AutoRenderQueue";
        const string ShaderPropUtsTechniqe = "_utsTechnique";



        public int _autoRenderQueue = 1;
        public int _renderQueue = (int)UnityEngine.Rendering.RenderQueue.Geometry;
        static _UTS_Transparent _Transparent_Setting;
        static int _StencilNo_Setting;
        //        static bool _OriginalInspector = false;
        //        static bool _SimpleUI = false;

        // for converter
        Vector2 m_scrollPos;
        bool m_initialzed;
        string[] guids;
        const string legacyShaderPrefix = "UnityChanToonShader/";
        readonly string[] m_RendderPipelineNames = { "Legacy", "Universal", "HDRP" };
        int m_selectedRenderPipeline;
        int m_materialCount = 0;
        [MenuItem("Window/Toon Shader/Unitychan Toon Shader Material Converter", false, 9999)]
        static private void OpenWindow()
        {
            var window = GetWindow<UnitychanToonShader2UnityToonShader>(true, "Unitychan Toon Shader Material Converter");
            window.Show();
        }

        private void OnGUI()
        {

            if (!m_initialzed)
            {
                guids = AssetDatabase.FindAssets("t:Material", null);
            }
            m_initialzed = true;
            int buttonHeight = 20;
            Rect rect =  new Rect(0, 0, position.width, position.height - buttonHeight); // GUILayoutUtility.GetRect(position.width, position.height - buttonHeight);
            Rect rect2 = new Rect(2, 2, position.width - 4, position.height - 4 - buttonHeight);
            // scroll view background
            EditorGUI.DrawRect(rect, Color.gray);
            EditorGUI.DrawRect(rect2, new Color(0.3f, 0.3f, 0.3f));
            using (new EditorGUI.DisabledScope(m_materialCount == 0))
            {
                EditorGUILayout.BeginHorizontal();

                EditorGUILayout.LabelField("Convert to ");
                m_selectedRenderPipeline = EditorGUILayout.Popup(m_selectedRenderPipeline, m_RendderPipelineNames);
                EditorGUILayout.EndHorizontal();
            }


            // scroll view 
            m_scrollPos =
                 EditorGUILayout.BeginScrollView(m_scrollPos, GUILayout.Width(position.width - 4));
            EditorGUILayout.BeginVertical();


            int materialCount = 0;
            int versionErrorCount = 0;
            for (int ii = 0; ii < guids.Length; ii++)
            {
                var guid = guids[ii];
                string path = AssetDatabase.GUIDToAssetPath(guid);
                Material material = AssetDatabase.LoadAssetAtPath<Material>(path);
                var shaderName = material.shader.ToString();
                if (!shaderName.StartsWith(legacyShaderPrefix))
                {
                    continue;

                }
                const string utsVersionProp = "_utsVersion";
                if (material.HasProperty(utsVersionProp))
                {
                    float utsVersion = material.GetFloat(utsVersionProp);
                    if (utsVersion < 2.07)
                    {
                        versionErrorCount++;
                        continue;
                    }
                }
                else
                {
                    versionErrorCount++;
                    continue;
                }
                materialCount++;
                Debug.Log(shaderName);

                EditorGUILayout.BeginHorizontal();
                GUILayout.Space(16);
                string str = "" + materialCount + ":";

                EditorGUILayout.LabelField(str, GUILayout.Width(40));
                EditorGUILayout.LabelField(path, GUILayout.Width(Screen.width - 130));
                GUILayout.Space(1);
                EditorGUILayout.EndHorizontal();
            }
            m_materialCount = materialCount;
            if (m_materialCount == 0)
            {
                GUILayout.Space(16);
                if (versionErrorCount > 0 )
                {
                    EditorGUILayout.LabelField("   Error: Unitychan Toon Shader version must be newer than 2.0.7");
                }
                else
                {
                    EditorGUILayout.LabelField("   No Unitychan Toon Shader material was found.");
                }
            }
            EditorGUILayout.EndVertical();
            EditorGUILayout.EndScrollView();


            // buttons 
            EditorGUILayout.BeginVertical();
            EditorGUILayout.BeginHorizontal();
            using (new EditorGUI.DisabledScope(m_materialCount == 0))
            {
                if (GUILayout.Button(new GUIContent("Convert")))
                {
                    ConvertMaterials(m_selectedRenderPipeline, guids);
                }
            }
            if ( GUILayout.Button(new GUIContent("Close")) )
            {
                Close();
            }
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.EndVertical();


        }

        void ConvertMaterials(int renderPipelineIndex, string[] guids)
        {


 


            foreach (var guid in guids)
            {
                string path = AssetDatabase.GUIDToAssetPath(guid);
                Material material  = AssetDatabase.LoadAssetAtPath<Material>(path) ;
                var shaderName = material.shader.ToString();
                if (!shaderName.StartsWith(legacyShaderPrefix))
                {
                    continue;

                }
                Debug.Log(shaderName);
                switch (renderPipelineIndex)
                {
                    case 0: // built in
                        material.shader = Shader.Find("Toon (Built-in)"); 
                        break;
                    case 1: // Universal
                        material.shader = Shader.Find("Universal Render Pipeline/Toon");
                        break;
                    case 2: // HDRP
                        material.shader = Shader.Find("HDRP/Toon");
                        break;
                }

               
                _Transparent_Setting = (_UTS_Transparent)material.GetInt(ShaderPropTransparentEnabled);
                _StencilNo_Setting = material.GetInt(ShaderPropStencilNo);
                _autoRenderQueue = material.GetInt(ShaderPropAutoRenderQueue);
                _renderQueue = material.renderQueue;
                _UTS_Technique technique = (_UTS_Technique)material.GetInt(ShaderPropUtsTechniqe);

                switch (technique)
                {
                    case _UTS_Technique.DoubleShadeWithFeather:
                        material.DisableKeyword(ShaderDefineSHADINGGRADEMAP);
                        break;
                    case _UTS_Technique.ShadingGradeMap:
                        material.EnableKeyword(ShaderDefineSHADINGGRADEMAP);
                        break;
                }
                BasicLookdevs(material);
                SetGameRecommendation(material);
                ApplyClippingMode(material);
                ApplyStencilMode(material);
                ApplyAngelRing(material);
                ApplyMatCapMode(material);
                ApplyQueueAndRenderType(technique, material);


            }
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();

        }
        void BasicLookdevs(Material material)
        {
            if (material.HasProperty(ShaderPropUtsTechniqe))//DoubleWithFeather or ShadingGradeMap
            {
                if (material.GetInt(ShaderPropUtsTechniqe) == (int)_UTS_Technique.DoubleShadeWithFeather)   //DWF
                {

                    //Sharing variables with ShadingGradeMap method.

                    material.SetFloat(ShaderProp1st_ShadeColor_Step, material.GetFloat(ShaderPropBaseColor_Step));
                    material.SetFloat(ShaderProp1st_ShadeColor_Feather, material.GetFloat(ShaderPropBaseShade_Feather));
                    material.SetFloat(ShaderProp2nd_ShadeColor_Step, material.GetFloat(ShaderPropShadeColor_Step));
                    material.SetFloat(ShaderProp2nd_ShadeColor_Feather, material.GetFloat(ShaderProp1st2nd_Shades_Feather));
                }
                else if (material.GetInt(ShaderPropUtsTechniqe) == (int)_UTS_Technique.ShadingGradeMap)
                {    //SGM

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
        private bool IsShadingGrademap(Material material)
        {
            return material.GetInt(ShaderPropUtsTechniqe) == (int)_UTS_Technique.ShadingGradeMap;
        }

        void ApplyQueueAndRenderType(_UTS_Technique technique, Material material)
        {
            var stencilMode = (_UTS_StencilMode)material.GetInt(ShaderPropStencilMode);
            if (_autoRenderQueue == 1)
            {
                material.renderQueue = (int)UnityEngine.Rendering.RenderQueue.Geometry;
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

            if (_Transparent_Setting == _UTS_Transparent.On)
            {
                renderType = TRANSPARENT;
                ignoreProjection = DO_IGNOREPROJECTION;
            }
            else
            {
                switch (technique)
                {
                    case _UTS_Technique.DoubleShadeWithFeather:
                        {
                            _UTS_ClippingMode clippingMode = (_UTS_ClippingMode)material.GetInt(ShaderPropClippingMode);
                            if (clippingMode == _UTS_ClippingMode.Off)
                            {

                            }
                            else
                            {
                                renderType = TRANSPARENTCUTOUT;

                            }

                            break;
                        }
                    case _UTS_Technique.ShadingGradeMap:
                        {
                            _UTS_TransClippingMode transClippingMode = (_UTS_TransClippingMode)material.GetInt(ShaderPropClippingMode);
                            if (transClippingMode == _UTS_TransClippingMode.Off)
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
            if (_autoRenderQueue == 1)
            {
                if (_Transparent_Setting == _UTS_Transparent.On)
                {
                    material.renderQueue = (int)UnityEngine.Rendering.RenderQueue.Transparent;
                }
                else if (stencilMode == _UTS_StencilMode.StencilMask)
                {
                    material.renderQueue = (int)UnityEngine.Rendering.RenderQueue.AlphaTest - 1;
                }
                else if (stencilMode == _UTS_StencilMode.StencilOut)
                {
                    material.renderQueue = (int)UnityEngine.Rendering.RenderQueue.AlphaTest;
                }
            }
            else
            {
                material.renderQueue = _renderQueue;
            }

            material.SetOverrideTag(RENDERTYPE, renderType);
            material.SetOverrideTag(IGNOREPROJECTION, ignoreProjection);
        }
        void ApplyMatCapMode(Material material)
        {
            if (material.GetInt(ShaderPropClippingMode) == 0)
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
            int angelRingEnabled = material.GetInt(ShaderPropAngelRing);
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
            _UTS_StencilMode mode = (_UTS_StencilMode)(material.GetInt(ShaderPropStencilMode));
            switch (mode)
            {
                case _UTS_StencilMode.Off:
                    //    material.SetInt(ShaderPropStencilNo,0);
                    material.SetInt(ShaderPropStencilComp, (int)_StencilCompFunction.Disabled);
                    material.SetInt(ShaderPropStencilOpPass, (int)_StencilOperation.Keep);
                    material.SetInt(ShaderPropStencilOpFail, (int)_StencilOperation.Keep);
                    break;
                case _UTS_StencilMode.StencilMask:
                    //    material.SetInt(ShaderPropStencilNo,0);
                    material.SetInt(ShaderPropStencilComp, (int)_StencilCompFunction.Always);
                    material.SetInt(ShaderPropStencilOpPass, (int)_StencilOperation.Replace);
                    material.SetInt(ShaderPropStencilOpFail, (int)_StencilOperation.Replace);
                    break;
                case _UTS_StencilMode.StencilOut:
                    //    material.SetInt(ShaderPropStencilNo,0);
                    material.SetInt(ShaderPropStencilComp, (int)_StencilCompFunction.NotEqual);
                    material.SetInt(ShaderPropStencilOpPass, (int)_StencilOperation.Keep);
                    material.SetInt(ShaderPropStencilOpFail, (int)_StencilOperation.Keep);

                    break;
            }



        }
        void ApplyClippingMode(Material material)
        {

            if (!IsShadingGrademap(material))
            {


                material.DisableKeyword(ShaderDefineIS_TRANSCLIPPING_OFF);
                material.DisableKeyword(ShaderDefineIS_TRANSCLIPPING_ON);

                switch (material.GetInt(ShaderPropClippingMode))
                {
                    case 0:
                        material.EnableKeyword(ShaderDefineIS_CLIPPING_OFF);
                        material.DisableKeyword(ShaderDefineIS_CLIPPING_MODE);
                        material.DisableKeyword(ShaderDefineIS_CLIPPING_TRANSMODE);
                        material.EnableKeyword(ShaderDefineIS_OUTLINE_CLIPPING_NO);
                        material.DisableKeyword(ShaderDefineIS_OUTLINE_CLIPPING_YES);
                        break;
                    case 1:
                        material.DisableKeyword(ShaderDefineIS_CLIPPING_OFF);
                        material.EnableKeyword(ShaderDefineIS_CLIPPING_MODE);
                        material.DisableKeyword(ShaderDefineIS_CLIPPING_TRANSMODE);
                        material.DisableKeyword(ShaderDefineIS_OUTLINE_CLIPPING_NO);
                        material.EnableKeyword(ShaderDefineIS_OUTLINE_CLIPPING_YES);
                        break;
                    default:
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
                switch (material.GetInt(ShaderPropClippingMode))
                {
                    case 0:
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
        void SetGameRecommendation(Material material)
        {


            material.SetFloat(ShaderPropIsLightColor_Base, 1);
            material.SetFloat(ShaderPropIs_LightColor_1st_Shade, 1);
            material.SetFloat(ShaderPropIs_LightColor_2nd_Shade, 1);
            material.SetFloat(ShaderPropIs_LightColor_HighColor, 1);
            material.SetFloat(ShaderPropIs_LightColor_RimLight, 1);
            material.SetFloat(ShaderPropIs_LightColor_Ap_RimLight, 1);
            material.SetFloat(ShaderPropIs_LightColor_MatCap, 1);
            if (material.HasProperty(ShaderPropAngelRing))
            {//When AngelRing is available
                material.SetFloat(ShaderPropIs_LightColor_AR, 1);
            }
            if (material.HasProperty(ShaderPropOutline))//OUTLINEがある場合.
            {
                material.SetFloat(ShaderPropIs_LightColor_Outline, 1);
            }
            material.SetFloat(ShaderPropSetSystemShadowsToBase, 1);
            material.SetFloat(ShaderPropIsFilterHiCutPointLightColor, 1);
            material.SetFloat(ShaderPropCameraRolling_Stabilizer, 1);
            material.SetFloat(ShaderPropIs_Ortho, 0);
            material.SetFloat(ShaderPropGI_Intensity, 0);
            material.SetFloat(ShaderPropUnlit_Intensity, 1);
            material.SetFloat(ShaderPropIs_Filter_LightColor, 1);
        }
    }
}
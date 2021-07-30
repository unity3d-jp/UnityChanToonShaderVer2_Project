using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
namespace UnityEditor.Rendering.Toon
{
    public class OldLegacyUTStoCurrentLegacyUTS : EditorWindow
    {
        const string ShaderPropOutline = "_OUTLINE";
        const string ShaderPropIs_LightColor_Outline = "_Is_LightColor_Outline";
        const string ShaderPropIs_BakedNormal = "_Is_BakedNormal";
        [MenuItem("Window/Toon Shader/Convert old Legacy materials to 2.0.7.5 materials", false, 9999)]
        static private void OpenWindow()
        {
            var window = GetWindow<OldLegacyUTStoCurrentLegacyUTS>();
            window.Show();
        }

        private void OnGUI()
        {
            EditorGUILayout.BeginVertical();
            EditorGUILayout.BeginHorizontal();
            if ( GUILayout.Button(new GUIContent("Convert")) )
            {
                ConvertMaterials();
            }
            if ( GUILayout.Button(new GUIContent("Close")) )
            {
                Close();
            }
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.EndVertical();
        }

        void ConvertMaterials()
        {

            const string legacyShaderPrefix = "UnityChanToonShader/";
            var guids = AssetDatabase.FindAssets("t:Material", null);
            foreach (var guid in guids)
            {
                string path = AssetDatabase.GUIDToAssetPath(guid);
                Material material  = AssetDatabase.LoadAssetAtPath<Material>(path) ;
                var shaderName = material.shader.ToString();
                if (!shaderName.StartsWith(legacyShaderPrefix))
                {
                    continue;

                }
                if (material.HasProperty(ShaderPropOutline))
                {
                    material.SetFloat(ShaderPropIs_LightColor_Outline, 1);
                }
                if (material.HasProperty(ShaderPropIs_BakedNormal))
                { 
                    material.SetFloat(ShaderPropIs_BakedNormal, 0);
                }
                if (material.HasProperty("_Emissive_Tex"))
                {
                    material.SetTexture("_Emissive_Tex", Texture2D.whiteTexture);
                }
                if (material.HasProperty("_HighColor_Tex"))
                {
                    material.SetTexture("_HighColor_Tex", Texture2D.whiteTexture);
                }
                if (material.HasProperty("_MainTex"))
                {
                    var baseMap = material.GetTexture("_BaseMap");
                    material.SetTexture("_MainTex", baseMap);
                }
                if (material.HasProperty("_OutlineTex"))
                {
                    material.SetTexture("_OutlineTex", Texture2D.whiteTexture);
                }
                if (material.HasProperty("_Set_MatcapMask"))
                {
                    material.SetTexture("_Set_MatcapMask", Texture2D.whiteTexture);
                }
                if (material.HasProperty("_Set_MatcapMask"))
                {
                    material.SetTexture("_Set_MatcapMask", Texture2D.whiteTexture);
                }
                if (material.HasProperty("_1st2nd_Shades_Feather"))
                {
                    material.SetFloat("_1st2nd_Shades_Feather", 0.0001f);
                }
                if ( material.HasProperty("_BaseColor_Step"))
                {
                    material.SetFloat("_BaseColor_Step", 0.315f);
                }
                if (material.HasProperty("_BaseShade_Feather"))
                {
                    material.SetFloat("_BaseShade_Feather", 0.0001f);
                }
                if (material.HasProperty("_Base_Speed"))
                {
                    material.SetFloat("_Base_Speed", 0.0000f);
                }
                if (material.HasProperty("_BlurLevelMatcap"))
                {
                    material.SetInt("_BlurLevelMatcap", 0);
                }
                if (material.HasProperty("_BlurLevelSGM"))
                {
                    material.SetInt("_BlurLevelSGM", 0);
                }
                if (material.HasProperty("_BumpScaleMatcap"))
                {
                    material.SetInt("_BumpScaleMatcap", 0);
                }
                if (material.HasProperty("_CameraRolling_Stabilizer"))
                {
                    material.SetInt("_CameraRolling_Stabilizer", 0);
                }
                if (material.HasProperty("_ColorShift_Speed"))
                {
                    material.SetInt("_ColorShift_Speed", 0);
                }
                if (material.HasProperty("_EMISSIVE"))
                {
                    material.SetInt("_EMISSIVE", 0);
                }
                if (material.HasProperty("_Inverse_MatcapMask"))
                {
                    material.SetInt("_Inverse_MatcapMask", 0);
                }
                if (material.HasProperty("_Inverse_Z_Axis_BLD"))
                {
                    material.SetInt("_Inverse_Z_Axis_BLD", 1);
                }
                if (material.HasProperty("_Is_BLD"))
                {
                    material.SetInt("_Is_BLD", 0);
                }
                if (material.HasProperty("_Is_BlendBaseColor"))
                {
                    material.SetInt("_Is_BlendBaseColor", 0);
                }
                if (material.HasProperty("_Is_ColorShift"))
                {
                    material.SetInt("_Is_ColorShift", 0);
                }
                if (material.HasProperty("_Is_Filter_HiCutPointLightColor"))
                {
                    material.SetInt("_Is_Filter_HiCutPointLightColor", 0);
                }
                if (material.HasProperty("_Is_Filter_LightColor"))
                {
                    material.SetInt("_Is_Filter_LightColor", 0);
                }
                if (material.HasProperty("_Is_LightColor_Outline"))
                {
                    material.SetInt("_Is_LightColor_Outline", 1);
                }
                if (material.HasProperty("_Is_NormalMapToBase"))
                {
                    material.SetInt("_Is_NormalMapToBase", 0);
                }
                if (material.HasProperty("_Is_Ortho"))
                {
                    material.SetInt("_Is_Ortho", 0);
                }
                if (material.HasProperty("_Is_OutlineTex"))
                {
                    material.SetInt("_Is_OutlineTex", 0);
                }
                if (material.HasProperty("_Is_PingPong_Base"))
                {
                    material.SetInt("_Is_PingPong_Base", 0);
                }
                if (material.HasProperty("_Is_ViewCoord_Scroll"))
                {
                    material.SetInt("_Is_ViewCoord_Scroll", 0);
                }
                if (material.HasProperty("_Is_ViewShift"))
                {
                    material.SetInt("_Is_ViewShift", 0);
                }
                if (material.HasProperty("_OUTLINE"))
                {
                    material.SetInt("_OUTLINE", 0);
                }
                if (material.HasProperty("_Offset_X_Axis_BLD"))
                {
                    material.SetFloat("_Offset_X_Axis_BLD", -0.05f);
                }
                if (material.HasProperty("_Offset_Y_Axis_BLD"))
                {
                    material.SetFloat("_Offset_Y_Axis_BLD", 0.09f);
                }
                if (material.HasProperty("_Rotate_EmissiveUV"))
                {
                    material.SetInt("_Rotate_EmissiveUV", 0);
                }
                if (material.HasProperty("_Scroll_EmissiveU"))
                {
                    material.SetInt("_Scroll_EmissiveU", 0);
                }
                if (material.HasProperty("_Scroll_EmissiveV"))
                {
                    material.SetInt("_Scroll_EmissiveV", 0);
                }
                if (material.HasProperty("_ShadeColor_Step"))
                {
                    material.SetFloat("_ShadeColor_Step", 0.196f);
                }
                if (material.HasProperty("_StepOffset"))
                {
                    material.SetInt("_StepOffset", 0);
                }
                if (material.HasProperty("_Tweak_MatcapMaskLevel"))
                {
                    material.SetInt("_Tweak_MatcapMaskLevel", 0);
                }
                if (material.HasProperty("Tweak_ShadingGradeMapLevel"))
                {
                    material.SetInt("Tweak_ShadingGradeMapLevel", 0);
                }
                if (material.HasProperty("_ColorShift"))
                {
                    material.SetColor("_ColorShift", Color.clear);
                }
                if (material.HasProperty("_Emissive_Color"))
                {
                    material.SetColor("_Emissive_Color", Color.clear);
                }
                if (material.HasProperty("_ViewShift"))
                {
                    material.SetColor("_ViewShift", Color.clear);
                }
                material.SetInt("_utsTechnique", material.HasProperty("_ShadingGradeMap") ? 1 : 0);
                material.SetFloat("_utsVersion", 2.075f);

            }
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();

        }

    }
}
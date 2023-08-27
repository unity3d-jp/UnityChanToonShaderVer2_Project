//Unity Toon Shader/HDRP
//nobuyuki@unity3d.com
//toshiyuki@unity3d.com (Universal RP/HDRP) 

using UnityEngine;
using UnityEditor;
using System;
using UnityEngine.Rendering;

namespace UnityEditor.Rendering.Toon
{
    internal partial class UTS3GUI
    {
        enum TessellationMode
        {
            None,
            Phong
        }

        internal class TessellationStyles
        {
            public const string header = "Tessellation Options";

            public static GUIContent tessellationModeText = new GUIContent("Tessellation Mode","Tessellation Mode. None/Phong.");

            public static readonly string[] tessellationModeNames = System.Enum.GetNames(typeof(
            TessellationMode));
            public static GUIContent tessellationText = new GUIContent("Tessellation Options", "Tessellation options");
            public static GUIContent tessellationFactorText = new GUIContent("Tessellation Factor", "Controls the strength of the tessellation effect. Higher values result in more tessellation. Maximum tessellation factor is 15 on the Xbox One and PS4.");
            public static GUIContent tessellationFactorMinDistanceText = new GUIContent("Start Fade Distance", "Sets the distance (in meters) at which tessellation begins to fade out.");
            public static GUIContent tessellationFactorMaxDistanceText = new GUIContent("End Fade Distance", "Sets the maximum distance (in meters) to the Camera where HDRP tessellates triangle.");
            public static GUIContent tessellationFactorTriangleSizeText = new GUIContent("Triangle Size", "Sets the desired screen space size of triangles (in pixels). Smaller values result in smaller triangle.");
            public static GUIContent tessellationShapeFactorText = new GUIContent("Shape Factor", "Controls the strength of Phong tessellation shape (lerp factor).");
            public static GUIContent tessellationBackFaceCullEpsilonText = new GUIContent("Triangle Culling Epsilon", "Controls triangle culling. A value of -1.0 disables back face culling for tessellation, higher values produce more aggressive culling and better performance.");
        }

        // tessellation params
        MaterialProperty tessellationMode = null;
        const string kTessellationMode = "_TessellationMode";
        MaterialProperty tessellationFactor = null;
        const string kTessellationFactor = "_TessellationFactor";
        MaterialProperty tessellationFactorMinDistance = null;
        const string kTessellationFactorMinDistance = "_TessellationFactorMinDistance";
        MaterialProperty tessellationFactorMaxDistance = null;
        const string kTessellationFactorMaxDistance = "_TessellationFactorMaxDistance";
        MaterialProperty tessellationFactorTriangleSize = null;
        const string kTessellationFactorTriangleSize = "_TessellationFactorTriangleSize";
        MaterialProperty tessellationShapeFactor = null;
        const string kTessellationShapeFactor = "_TessellationShapeFactor";
        MaterialProperty tessellationBackFaceCullEpsilon = null;
        const string kTessellationBackFaceCullEpsilon = "_TessellationBackFaceCullEpsilon";
        MaterialProperty doubleSidedEnable = null;
        const string kDoubleSidedEnable = "_DoubleSidedEnable";

        public static GUIContent tessellationModeText = new GUIContent("Tessellation Mode",
    "Transparent  mode that fits you. ");



        internal   void FindTessellationPropertiesHDRP(MaterialProperty[] props)
        {
            tessellationMode = FindProperty(kTessellationMode, props, false);
            tessellationFactor = FindProperty(kTessellationFactor, props, false);
            tessellationFactorMinDistance = FindProperty(kTessellationFactorMinDistance, props, false);
            tessellationFactorMaxDistance = FindProperty(kTessellationFactorMaxDistance, props, false);
            tessellationFactorTriangleSize = FindProperty(kTessellationFactorTriangleSize, props, false);
            tessellationShapeFactor = FindProperty(kTessellationShapeFactor, props, false);
            tessellationBackFaceCullEpsilon = FindProperty(kTessellationBackFaceCullEpsilon, props, false);
            doubleSidedEnable = FindProperty(kDoubleSidedEnable, props, false);

        }

        static void SetKeyword(Material material, string keyword, bool state)
        {
            if (state)
                material.EnableKeyword(keyword);
            else
                material.DisableKeyword(keyword);
        }

        internal  void ApplyTessellationHDRP(Material material)
        {
            if (material.HasProperty(kTessellationMode))
            {
                TessellationMode tessMode = (TessellationMode)material.GetFloat(kTessellationMode);
                SetKeyword(material, "_TESSELLATION_PHONG", tessMode == TessellationMode.Phong);
            }
        }
        void DrawDelayedFloatProperty(MaterialProperty prop, GUIContent content)
        {
            Rect position = EditorGUILayout.GetControlRect();
            EditorGUI.BeginChangeCheck();
            EditorGUI.showMixedValue = prop.hasMixedValue;
            float newValue = EditorGUI.DelayedFloatField(position, content, prop.floatValue);
            EditorGUI.showMixedValue = false;
            if (EditorGUI.EndChangeCheck())
                prop.floatValue = newValue;
        }
        void TessellationModePopup()
        {
            EditorGUI.showMixedValue = tessellationMode == null ? false: tessellationMode.hasMixedValue;
            TessellationMode mode = TessellationMode.None;
            mode = (TessellationMode)tessellationMode?.floatValue;

            EditorGUI.BeginChangeCheck();
            mode = (TessellationMode)EditorGUILayout.Popup(TessellationStyles.tessellationModeText, (int)mode, TessellationStyles.tessellationModeNames);
            if (EditorGUI.EndChangeCheck())
            {
                m_MaterialEditor.RegisterPropertyChangeUndo("Tessellation Mode");
                tessellationMode.floatValue = (float)mode;
            }

            EditorGUI.showMixedValue = false;
        }



        void GUI_TessellationHDRP(Material material)
        {
            TessellationModePopup();
            m_MaterialEditor.ShaderProperty(tessellationFactor, TessellationStyles.tessellationFactorText);
            DrawDelayedFloatProperty(tessellationFactorMinDistance, TessellationStyles.tessellationFactorMinDistanceText);
            DrawDelayedFloatProperty(tessellationFactorMaxDistance, TessellationStyles.tessellationFactorMaxDistanceText);
            // clamp min distance to be below max distance
            tessellationFactorMinDistance.floatValue = Math.Min(tessellationFactorMaxDistance.floatValue, tessellationFactorMinDistance.floatValue);
            m_MaterialEditor.ShaderProperty(tessellationFactorTriangleSize, TessellationStyles.tessellationFactorTriangleSizeText);
            if ((TessellationMode)tessellationMode.floatValue == TessellationMode.Phong)
            {
                m_MaterialEditor.ShaderProperty(tessellationShapeFactor, TessellationStyles.tessellationShapeFactorText);
            }
            if (doubleSidedEnable.floatValue == 0.0)
            {
                m_MaterialEditor.ShaderProperty(tessellationBackFaceCullEpsilon, TessellationStyles.tessellationBackFaceCullEpsilonText);
            }
        }
    } // End of UTS2GUI2
}// End of namespace UnityChan
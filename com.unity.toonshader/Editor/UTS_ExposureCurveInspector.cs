using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Unity.Rendering.Toon;
namespace UnityEditor.Rendering.Toon
{
    [CustomEditor(typeof(UTS_ExposureCurve))]

    public class UTS_ExposureCurveInspector : Editor
    {
        public override void OnInspectorGUI()
        {
            var obj = target as UTS_ExposureCurve;
            var mode = obj.m_logic;
            const string label = "Curve Logic";
            EditorGUI.BeginChangeCheck();
            mode = EditorGUILayout.Popup(label, (int)mode, System.Enum.GetNames(typeof(UTS_ExposureCurveType)));
            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(target, "Changed UTS Curve Logic");
                obj.m_logic =(int) mode;
            }

            Rect area = GUILayoutUtility.GetRect(Screen.width, 200.0f);
            Handles.DrawSolidDisc(area.center, Vector3.forward, 80f);
        }
    }
}

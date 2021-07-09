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
                obj.m_logic = (int)mode;
            }

            Rect rect = GUILayoutUtility.GetRect(Screen.width, 200.0f);
            EditorGUI.DrawRect(rect, Color.black);
            float zoom = 2.0f;
            float yMin = 0;
            float yMax = 1;
            float step = 1/10.0f;
            Handles.color = Color.green;
            Vector3 prevPos = new Vector3(0, curveFunc(0, (UTS_ExposureCurveType)mode), 0);
            for (float t = step; t < 1 * zoom +step ; t += step)
            {
                Vector3 pos = new Vector3(t, curveFunc(t, (UTS_ExposureCurveType)mode), 0);
                Handles.DrawLine(
                    new Vector3(rect.xMin + prevPos.x * rect.width /zoom, rect.yMax - ((prevPos.y - yMin) / (yMax - yMin)) * rect.height/zoom, 0),
                    new Vector3(rect.xMin + pos.x * rect.width/zoom, rect.yMax - ((pos.y - yMin) / (yMax - yMin)) * rect.height/zoom, 0));

                prevPos = pos;

            }
            //            Handles.DrawSolidDisc(area.center, Vector3.forward, 80f);
        }
        float curveFunc(float t, UTS_ExposureCurveType curveType)
        {
            const float logOffset = 1.0f;
            float result = 0.0f;
            switch (curveType)
            {
                case UTS_ExposureCurveType.Linear:
                    result =  t;
                    break;
                case UTS_ExposureCurveType.Log:
                    result = Mathf.Log(t + logOffset);
                    break;
                case UTS_ExposureCurveType.Log2:
                    result =  Mathf.Log(t + logOffset, 2);
                    break;
                case UTS_ExposureCurveType.Log10:
                    result = Mathf.Log10(t + logOffset);
                    break;
                default:
                    break;
            }
            return result;
        }
    }
}

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
        float zoom = 2.0f;
        public override void OnInspectorGUI()
        {
            bool isChanged = false;

            var obj = target as UTS_ExposureCurve;
            const string labelCurveLogic = "Curve Logic";

            EditorGUI.BeginChangeCheck();
            var curveType = EditorGUILayout.Popup(labelCurveLogic, obj.m_CurveType, System.Enum.GetNames(typeof(UTS_ExposureCurveType)));
            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(target, "Changed UTS Curve Logic");
                obj.m_CurveType = (int)curveType;
                isChanged = true;
            }


            Rect rect = GUILayoutUtility.GetRect(Screen.width, 200.0f);
            EditorGUI.DrawRect(rect, Color.black);
            const int zoomMax = 10;
            float yMin = -zoomMax;
            float yMax = zoomMax;
            float xMin = -zoomMax;
            float xMax = zoomMax;
            float step = 1/1000.0f;
            const float one = 1.0f;
            Handles.color = Color.green;
            Vector3 prevPos = new Vector3(0, curveFunc(0.0001f, (UTS_ExposureCurveType)curveType), 0);
            for (float t = step; t < 10.0  +step ; t += step)
            {
                Vector3 pos = new Vector3(t, curveFunc(t, (UTS_ExposureCurveType)curveType), 0);
                Handles.DrawLine(
                    new Vector3(rect.xMin + (prevPos.x - xMin) / (xMax - xMin)  * rect.width /zoom, rect.yMax - ((prevPos.y - yMin) / (yMax - yMin)) * rect.height/zoom, 0),
                    new Vector3(rect.xMin + (pos.x - xMin) / (xMax - xMin)  * rect.width/zoom, rect.yMax - ((pos.y - yMin) / (yMax - yMin)) * rect.height/zoom, 0));

                prevPos = pos;

            }
            Handles.color = Color.white;
            // y == 1.0f;
            Handles.DrawLine(
                    new Vector3(rect.xMin, rect.yMax - ((1.0f - yMin) / (yMax - yMin)) * rect.height / zoom, 0),
                    new Vector3(rect.xMax, rect.yMax - ((1.0f - yMin) / (yMax - yMin)) * rect.height / zoom, 0));
            // y == 0.0f;
            Handles.DrawLine(
                    new Vector3(rect.xMin, rect.yMax - ((0.0f - yMin) / (yMax - yMin)) * rect.height / zoom, 0),
                    new Vector3(rect.xMax, rect.yMax - ((0.0f - yMin) / (yMax - yMin)) * rect.height / zoom, 0));
            // x == 1.0f;
            Handles.DrawLine(
                   new Vector3(rect.xMin + (1.0f - xMin) / (xMax - xMin) * rect.width / zoom, rect.yMin, 0),
                   new Vector3(rect.xMin + (1.0f - xMin) / (xMax - xMin) * rect.width / zoom, rect.yMax, 0));
            // x == 0.0f;
            Handles.DrawLine(
                   new Vector3(rect.xMin + (0.0f - xMin) / (xMax - xMin) * rect.width / zoom, rect.yMin, 0),
                   new Vector3(rect.xMin + (0.0f - xMin) / (xMax - xMin) * rect.width / zoom, rect.yMax, 0));
            Handles.color = Color.white;


            zoom = EditorGUILayout.Slider(zoom, 1, zoomMax);
 
            Handles.Label(new Vector3(rect.xMin + (1.0f - xMin) / (xMax - xMin) * rect.width / zoom - 20.0f, rect.yMax - ((1.0f - yMin) / (yMax - yMin)) * rect.height/zoom + 8.0f,0),
                one.ToString() );
            if ( isChanged)
            {
                // at leaset 2020.3.12f1, not neccessary. but, from which version??
                EditorApplication.QueuePlayerLoopUpdate();
            }
            //            Handles.DrawSolidDisc(area.center, Vector3.forward, 80f);
        }
        float curveFunc(float t, UTS_ExposureCurveType curveType)
        {

            float result = 0.0f;
            switch (curveType)
            {
                case UTS_ExposureCurveType.Linear:
                    result =  t;
                    break;
                case UTS_ExposureCurveType.Log:
                    result = Mathf.Log(t);

                    break;
                case UTS_ExposureCurveType.Log2:
                    result = Mathf.Log(t);

                    break;
                case UTS_ExposureCurveType.Log10:
                    result = Mathf.Log10(t);

                    break;
                default:
                    break;
            }
            return result;
        }
    }
}

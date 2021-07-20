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

        string numberString = "1";

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            bool isChanged = false;

            var obj = target as UTS_ExposureCurve;

           // const string labelExposureAdjustment = "Exposure Adjustment";
            EditorGUI.BeginChangeCheck();

            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(target, "Changed UTS Curve Logic");

                isChanged = true;
            }


            //Rect rect = GUILayoutUtility.GetRect(Screen.width, 300.0f);

            EditorGUI.BeginChangeCheck();
            /*
            bool exposureAdjustment = EditorGUILayout.Toggle(labelExposureAdjustment, obj.m_expssureAdjustmnt);
            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(target, "Changed Exposure Adjustment");
                obj.m_expssureAdjustmnt = exposureAdjustment;
                isChanged = true;
            }
            */
            numberString = EditorGUILayout.TextField("lux: ", numberString);
            float fLux = float.Parse(numberString);
            fLux = Mathf.Max(fLux, 0.0001f);
            fLux = Mathf.Min(fLux, 100000 );
            float log10value = Mathf.Log10(fLux);
            fLux = Mathf.Clamp((log10value + 5.0f) / 10.0f, 0.0f, 1.0f);
            string label = fLux.ToString();
            EditorGUILayout.LabelField(label);
            if ( isChanged)
            {
                // at leaset 2020.3.12f1, not neccessary. but, from which version??
                EditorApplication.QueuePlayerLoopUpdate();
            }

        }

    }
}

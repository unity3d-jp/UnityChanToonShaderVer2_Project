using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Unity.Rendering.Toon;
namespace UnityEditor.Rendering.Toon
{
    [CustomEditor(typeof(ToonEvAdjustmentCurve))]

    public class ToonEvAdjustmentCurveCurveInspector : Editor
    {
        SerializedObject m_SerializedObject;
#if ADJUSTMENT_CURVE_DEBUG_UI
        string numberString = "1";
#endif //
        public override void OnInspectorGUI()
        {
            const string labelLightAdjustment = "Toon EV Adjustment";
            const string labelLightAdjustmentCurve = "Curve";
#if ADJUSTMENT_CURVE_DEBUG_UI
            const string labelExposureMin = "Min:";
            const string labelExposureMax = "Max:";
#endif
            bool isChanged = false;

            var obj = target as ToonEvAdjustmentCurve;
            EditorGUI.BeginChangeCheck();
            bool exposureAdjustment = EditorGUILayout.Toggle(labelLightAdjustment, obj.m_ExposureAdjustmnt);
            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(target, "Changed Expsure Adjustment");
                obj.m_ExposureAdjustmnt = exposureAdjustment;
                isChanged = true;
            }

            EditorGUI.BeginDisabledGroup(!obj.m_ExposureAdjustmnt);
            {
                EditorGUI.indentLevel++;
                EditorGUI.BeginChangeCheck();
                //               var ranges = new Rect(-10, -10, 20, 20);
                //               var curve = EditorGUILayout.CurveField(labelExposureCurave, obj.m_AnimationCurve, Color.green,ranges);
                var curve = EditorGUILayout.CurveField(labelLightAdjustmentCurve, obj.m_AnimationCurve);
                if (EditorGUI.EndChangeCheck())
                {
                    Undo.RecordObject(target, "Changed Curve");
                    obj.m_AnimationCurve = curve;
                    isChanged = true;
                }
                EditorGUI.indentLevel--;
            }
            EditorGUI.EndDisabledGroup();

            //Rect rect = GUILayoutUtility.GetRect(Screen.width, 300.0f);
#if false
            EditorGUI.BeginChangeCheck();

            bool lightAdjustment = EditorGUILayout.Toggle(labelLightAdjustment, obj.m_LightAdjustment);
            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(target, "Changed Light Adjustment");
                obj.m_LightAdjustment = lightAdjustment;
                isChanged = true;
            }
#endif

            if ( isChanged)
            {
                // at leaset 2020.3.12f1, not neccessary. but, from which version??
                EditorApplication.QueuePlayerLoopUpdate();
            }
#if ADJUSTMENT_CURVE_DEBUG_UI

            numberString = EditorGUILayout.TextField("lux: ", numberString);
            float fLux = float.Parse(numberString);
            fLux = Mathf.Max(fLux, 0.0001f);
            fLux = Mathf.Min(fLux, 100000 );
            float log10value = Mathf.Log10(fLux);
            fLux = Mathf.Clamp((log10value + 5.0f) / 10.0f, 0.0f, 1.0f);
            string label = fLux.ToString();
            EditorGUILayout.LabelField(label);


            obj.m_DebugUI = EditorGUILayout.Toggle("Debug UI", obj.m_DebugUI);
            if (m_SerializedObject == null )
            {
                m_SerializedObject = new SerializedObject(obj);
            }
            EditorGUI.BeginDisabledGroup(!obj.m_DebugUI);
            {
                EditorGUI.indentLevel++;
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField(labelExposureMin);
                EditorGUILayout.LabelField(obj.m_Min.ToString());
                EditorGUILayout.EndHorizontal();
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField(labelExposureMax);
                EditorGUILayout.LabelField(obj.m_Max.ToString());
                EditorGUILayout.EndHorizontal();
                /*
                var prop = m_SerializedObject.FindProperty("m_ExposureArray");
                EditorGUILayout.PropertyField(prop, new GUIContent("ExposureArray"), true);
                m_SerializedObject.ApplyModifiedProperties();
                */
                for ( int ii = 0; ii < obj.m_ExposureArray.Length; ii++)
                {
                    EditorGUIUtility.labelWidth = 40;
                    EditorGUILayout.BeginHorizontal();
                    EditorGUILayout.LabelField(ii.ToString() + ":");
                    EditorGUILayout.LabelField(obj.m_ExposureArray[ii].ToString());
                    EditorGUILayout.LabelField(ConvertFromEV100(obj.m_ExposureArray[ii]).ToString());
                    EditorGUILayout.EndHorizontal();
                }

                float brightness = 130000;
                float ev100_Color = ConvertToEV100(brightness);
                ev100_Color =  Mathf.Clamp(ev100_Color, obj.m_Min, obj.m_Max);
                float ev100_remap = (ev100_Color - obj.m_Min) * (obj.m_ExposureArray.Length-1) / (obj.m_Max - obj.m_Min);

                int ev100_idx = (int)ev100_remap;
                EditorGUILayout.LabelField("ev100_Color:" + ev100_Color.ToString());
                EditorGUILayout.LabelField("idx:" + ev100_idx.ToString()); 
                EditorGUI.indentLevel--;
            }
            EditorGUI.EndDisabledGroup();


#endif
        }


        float ConvertFromEV100(float EV100)
        {

            float val = Mathf.Pow(2, EV100) * 2.5f;
            return val;

        }

        float ConvertToEV100(float val)
        {

            return Mathf.Log(val*0.4f,2.0f);

        }

    }
}

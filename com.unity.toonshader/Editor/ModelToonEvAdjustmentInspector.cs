using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Unity.Rendering.Toon;
namespace UnityEditor.Rendering.Toon
{
    [CustomEditor(typeof(ModelToonEvAdjustment))]

    internal class ModelToonEvAdjustmentInspector : Editor
    {
        SerializedObject m_SerializedObject;

        public override void OnInspectorGUI()
        {

            const string labelLightAdjustment = "Toon EV Adjustment";
            const string labelLightAdjustmentCurve = "Curve";
            const string labelLightHighCutFilter = "Light High-Cut Filter";
            const string labeIgnoreVolumeExposure = "Ignore Volume Exposure";
            const string labelCompensation = "Compensation";

            bool isChanged = false;

            var obj = target as ModelToonEvAdjustment;

            // hi cut filter
            EditorGUI.BeginChangeCheck();

            bool egnoreExposure = EditorGUILayout.Toggle(labeIgnoreVolumeExposure, obj.m_IgnorVolumeExposure);
            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(target, "Changed Ignore Volume Exposure");
                obj.m_IgnorVolumeExposure = egnoreExposure;
                isChanged = true;
            }

            // hi cut filter
            EditorGUI.BeginChangeCheck();

            bool lightFilterr = EditorGUILayout.Toggle(labelLightHighCutFilter, obj.m_ToonLightHiCutFilter);
            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(target, "Changed Light Hi Cut Filter");
                obj.m_ToonLightHiCutFilter = lightFilterr;
                isChanged = true;
            }


            // Compensation
            EditorGUILayout.BeginHorizontal();
            EditorGUI.BeginChangeCheck();
            float compensation = EditorGUILayout.FloatField(labelCompensation, obj.m_Compensation);
            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(target, "Changed Compensation");
                obj.m_Compensation = compensation;
                isChanged = true;
            }
            EditorGUILayout.EndHorizontal();


            // curve
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
                EditorGUILayout.BeginHorizontal();

                EditorGUI.BeginChangeCheck();
                //               var ranges = new Rect(-10, -10, 20, 20);
                //               var curve = EditorGUILayout.CurveField(labelExposureCurave, obj.m_AnimationCurve, Color.green,ranges);
                AnimationCurve curve = EditorGUILayout.CurveField(labelLightAdjustmentCurve, obj.m_AnimationCurve);

                if (EditorGUI.EndChangeCheck())
                {
                    Undo.RecordObject(target, "Changed Curve");
                    obj.m_AnimationCurve = curve;
                    isChanged = true;
                }
                EditorGUI.BeginChangeCheck();
                bool buttonIsPressed = GUILayout.Button("Reset", GUILayout.Width(50));
                var curve2 = obj.m_AnimationCurve;
                if (buttonIsPressed)
                {
                    curve2 = SceneToonEvAdjustment.DefaultAnimationCurve();
                }
                if (EditorGUI.EndChangeCheck())
                {
                    Undo.RecordObject(target, "Changed Curve");
                    obj.m_AnimationCurve = curve2;
                    isChanged = true;
                }
                EditorGUILayout.EndHorizontal();

                var rangeMinLux = ConvertFromEV100(obj.m_Min);
                var rangeMaxLux = ConvertFromEV100(obj.m_Max);
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("Min EV/LUX:" + obj.m_Min + " / " + rangeMinLux.ToString());
                EditorGUILayout.LabelField("Max EV/LUX:" + obj.m_Max + " / " + rangeMaxLux.ToString());
                EditorGUILayout.EndHorizontal();




                EditorGUI.indentLevel--;
            }
            EditorGUI.EndDisabledGroup();

            if (isChanged)
            {
                // at leaset 2020.3.12f1, not neccessary. but, from which version??
                EditorApplication.QueuePlayerLoopUpdate();
            }




        }


        float ConvertFromEV100(float EV100)
        {

            float val = Mathf.Pow(2, EV100) * 2.5f;
            return val;

        }

        float ConvertToEV100(float val)
        {

            return Mathf.Log(val * 0.4f, 2.0f);

        }




        [MenuItem("GameObject/Toon Shader/Attach Model Toon Ev Adjustment", false, 9999)]
        static void AttachToonEvAdjustmentCurve()
        {

            var go = Selection.activeGameObject;
            if (go == null)
               go.AddComponent<ModelToonEvAdjustment>();
            else
            {
                Debug.LogError("Please, select a GameObject you want to attach.");
            }

        }

    }
}
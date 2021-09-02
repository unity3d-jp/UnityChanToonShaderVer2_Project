using System;
using UnityEditor;
using UnityEngine;

namespace UnityChan.ImageEffects
{
    [CustomEditor (typeof(UTS_EdgeDetection))]
    class UTS_EdgeDetectionEditor : Editor
    {
        SerializedObject serObj;
        SerializedProperty mode;
        SerializedProperty edgesColor;
        SerializedProperty edgeExp;
        SerializedProperty sampleDist;
        SerializedProperty filterPower;
        SerializedProperty threshold;
        SerializedProperty sensitivityDepth;
        SerializedProperty sensitivityNormals;
        SerializedProperty edgesOnly;



        void OnEnable () {
            serObj = new SerializedObject (target);
            mode = serObj.FindProperty("mode");

            edgesColor = serObj.FindProperty("edgesColor");
            edgeExp = serObj.FindProperty("edgeExp");
            sampleDist = serObj.FindProperty("sampleDist");
            filterPower = serObj.FindProperty("filterPower");
            threshold = serObj.FindProperty("threshold");
            sensitivityDepth = serObj.FindProperty("sensitivityDepth");
            sensitivityNormals = serObj.FindProperty("sensitivityNormals");
            edgesOnly = serObj.FindProperty("edgesOnly");
        }


        public override void OnInspectorGUI () {
            serObj.Update ();
            GUILayout.Label("Detects spatial or color differences and converts into outlines", EditorStyles.miniBoldLabel);
            //GUILayout.Label("Recommend to use in LDR Mode", EditorStyles.miniBoldLabel);            
            EditorGUILayout.PropertyField (mode, new GUIContent("Mode"));
            EditorGUILayout.PropertyField(edgesColor, new GUIContent("Edges Color"));
            if(mode.intValue < 2){
                GUILayout.Label ("Sobel Depth Options");
                edgeExp.floatValue = EditorGUILayout.Slider(" Edge Exponent", edgeExp.floatValue, 0.1f, 3.0f);
                //EditorGUILayout.PropertyField (edgeExp, new GUIContent(" Edge Exponent"));
                sampleDist.floatValue = EditorGUILayout.Slider(" Sample Distance", sampleDist.floatValue, 0.0f, 2.0f);
                //EditorGUILayout.PropertyField (sampleDist, new GUIContent(" Sample Distance"));
            }
            else if (mode.intValue < 3) {
                GUILayout.Label ("Roberts's Cross Depth Normals Options");
                EditorGUILayout.PropertyField (sensitivityDepth, new GUIContent(" Depth Sensitivity"));
                EditorGUILayout.PropertyField (sensitivityNormals, new GUIContent(" Normals Sensitivity"));
            }
            
            else{
                GUILayout.Label ("Sobel Color Options");
                filterPower.floatValue = EditorGUILayout.Slider(" Color Filter Power", filterPower.floatValue, 0.0f,1.0f);
                threshold.floatValue = EditorGUILayout.Slider(" Threshold", threshold.floatValue, 0.0f,1.0f);
            }
            EditorGUILayout.Separator ();
            GUILayout.Label ("Debug Options");
            edgesOnly.floatValue = EditorGUILayout.Slider (" Edges only", edgesOnly.floatValue, 0.0f, 0.5f);
            serObj.ApplyModifiedProperties();
        }
    }
}

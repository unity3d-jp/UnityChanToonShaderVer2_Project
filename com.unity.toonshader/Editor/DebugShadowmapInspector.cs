using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Unity.Rendering.Toon;
namespace UnityEditor.Rendering.Toon
{
    [CustomEditor(typeof(DebugShadowmap))]

    internal class DebugShadowmapInspector : Editor
    {

        public override void OnInspectorGUI()
        {
            const string labelDebugShadowmap = "Show Shadowmap";
            const string labelDebugSelfShadow = "Show SelfShadow";
            const string labelBinalization = "Binalization";
            const string labelNoOutline = "No Outline";



            bool isChanged = false;

            var obj = target as DebugShadowmap;

            // hi cut filter
            EditorGUI.BeginChangeCheck();

            bool showShadow = EditorGUILayout.Toggle(labelDebugShadowmap, obj.m_enableShadowmapDebugging);
            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(target, "Changed Shadowmap debbuging flag");
                obj.m_enableShadowmapDebugging = showShadow;
                isChanged = true;
            }


            EditorGUI.BeginDisabledGroup(!obj.m_enableShadowmapDebugging);
            {
                EditorGUI.indentLevel++;

                EditorGUILayout.BeginHorizontal();
                EditorGUI.BeginChangeCheck();
                bool binalization = EditorGUILayout.Toggle(labelBinalization, obj.m_enableBinalization);

                if (EditorGUI.EndChangeCheck())
                {
                    Undo.RecordObject(target, "Changed Binalization flag");
                    obj.m_enableBinalization = binalization;
                    isChanged = true;
                }
                EditorGUILayout.EndHorizontal();



                EditorGUI.indentLevel--;
            }
            EditorGUI.EndDisabledGroup();

            EditorGUILayout.BeginHorizontal();
            EditorGUI.BeginChangeCheck();
            bool showSlefShadow = EditorGUILayout.Toggle(labelDebugSelfShadow, obj.m_enableSelfShadowDebugging);

            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(target, "Changed Self shadow debbuging flag");
                obj.m_enableSelfShadowDebugging = showSlefShadow;
                isChanged = true;
            }
            EditorGUILayout.EndHorizontal();


            EditorGUILayout.BeginHorizontal();
            EditorGUI.BeginChangeCheck();
            bool showOutline = EditorGUILayout.Toggle(labelNoOutline, obj.m_enableOutlineDebugging);

            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(target, "Changed Outline flag");
                obj.m_enableOutlineDebugging = showOutline;
                isChanged = true;
            }
            EditorGUILayout.EndHorizontal();
            if (isChanged)
            {
                // at leaset 2020.3.12f1, not neccessary. but, from which version??
                EditorApplication.QueuePlayerLoopUpdate();
            }




        }







    }
}
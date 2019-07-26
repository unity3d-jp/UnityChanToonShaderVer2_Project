// Utility scripts for the post processing stack
// https://github.com/keijiro/PostProcessingUtilities

using UnityEngine;
using UnityEditor;

namespace UnityEngine.PostProcessing.Utilities
{
    [CanEditMultipleObjects]
    [CustomEditor(typeof(FocusPuller))]
    public class FocusPullerEditor : Editor
    {
        SerializedProperty _target;
        SerializedProperty _offset;
        SerializedProperty _speed;

        static GUIContent _textOffset = new GUIContent("Depth Offset");
        static GUIContent _textSpeed = new GUIContent("Interpolation Speed");

        void OnEnable()
        {
            _target = serializedObject.FindProperty("_target");
            _offset = serializedObject.FindProperty("_offset");
            _speed = serializedObject.FindProperty("_speed");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            EditorGUILayout.PropertyField(_target);
            EditorGUILayout.PropertyField(_offset, _textOffset);
            EditorGUILayout.PropertyField(_speed, _textSpeed);

            serializedObject.ApplyModifiedProperties();
        }
    }
}

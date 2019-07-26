// Utility scripts for the post processing stack
// https://github.com/keijiro/PostProcessingUtilities

using UnityEngine;
using UnityEditor;

namespace UnityEngine.PostProcessing.Utilities
{
    [CanEditMultipleObjects]
    [CustomEditor(typeof(PostProcessingController))]
    public class PostProcessingControllerEditor : Editor
    {
        #region Public structs

        SerializedProperty _controlAntialiasing;
        SerializedProperty _enableAntialiasing;
        SerializedProperty _antialiasing;

        SerializedProperty _controlAmbientOcclusion;
        SerializedProperty _enableAmbientOcclusion;
        SerializedProperty _ambientOcclusion;

        SerializedProperty _controlScreenSpaceReflection;
        SerializedProperty _enableScreenSpaceReflection;
        SerializedProperty _screenSpaceReflection;

        SerializedProperty _controlDepthOfField;
        SerializedProperty _enableDepthOfField;
        SerializedProperty _depthOfField;

        SerializedProperty _controlMotionBlur;
        SerializedProperty _enableMotionBlur;
        SerializedProperty _motionBlur;

        SerializedProperty _controlEyeAdaptation;
        SerializedProperty _enableEyeAdaptation;
        SerializedProperty _eyeAdaptation;

        SerializedProperty _controlBloom;
        SerializedProperty _enableBloom;
        SerializedProperty _bloom;

        SerializedProperty _controlColorGrading;
        SerializedProperty _enableColorGrading;
        SerializedProperty _colorGrading;

        SerializedProperty _controlUserLut;
        SerializedProperty _enableUserLut;
        SerializedProperty _userLut;

        SerializedProperty _controlChromaticAberration;
        SerializedProperty _enableChromaticAberration;
        SerializedProperty _chromaticAberration;

        SerializedProperty _controlGrain;
        SerializedProperty _enableGrain;
        SerializedProperty _grain;

        SerializedProperty _controlVignette;
        SerializedProperty _enableVignette;
        SerializedProperty _vignette;

        #endregion

        #region Text labels

        static GUIContent _textEnableControl = new GUIContent("Enable Control");
        static GUIContent _textEnableEffect = new GUIContent("Enable Effect");
        static GUIContent _textSettings = new GUIContent("Settings");

        #endregion

        #region Editor functions

        void OnEnable()
        {
            _controlAntialiasing = serializedObject.FindProperty("controlAntialiasing");
            _enableAntialiasing = serializedObject.FindProperty("enableAntialiasing");
            _antialiasing = serializedObject.FindProperty("antialiasing");

            _controlAmbientOcclusion = serializedObject.FindProperty("controlAmbientOcclusion");
            _enableAmbientOcclusion = serializedObject.FindProperty("enableAmbientOcclusion");
            _ambientOcclusion = serializedObject.FindProperty("ambientOcclusion");

            _controlScreenSpaceReflection = serializedObject.FindProperty("controlScreenSpaceReflection");
            _enableScreenSpaceReflection = serializedObject.FindProperty("enableScreenSpaceReflection");
            _screenSpaceReflection = serializedObject.FindProperty("screenSpaceReflection");

            _controlDepthOfField = serializedObject.FindProperty("controlDepthOfField");
            _enableDepthOfField = serializedObject.FindProperty("enableDepthOfField");
            _depthOfField = serializedObject.FindProperty("depthOfField");

            _controlMotionBlur = serializedObject.FindProperty("controlMotionBlur");
            _enableMotionBlur = serializedObject.FindProperty("enableMotionBlur");
            _motionBlur = serializedObject.FindProperty("motionBlur");

            _controlEyeAdaptation = serializedObject.FindProperty("controlEyeAdaptation");
            _enableEyeAdaptation = serializedObject.FindProperty("enableEyeAdaptation");
            _eyeAdaptation = serializedObject.FindProperty("eyeAdaptation");

            _controlBloom = serializedObject.FindProperty("controlBloom");
            _enableBloom = serializedObject.FindProperty("enableBloom");
            _bloom = serializedObject.FindProperty("bloom");

            _controlColorGrading = serializedObject.FindProperty("controlColorGrading");
            _enableColorGrading = serializedObject.FindProperty("enableColorGrading");
            _colorGrading = serializedObject.FindProperty("colorGrading");

            _controlUserLut = serializedObject.FindProperty("controlUserLut");
            _enableUserLut = serializedObject.FindProperty("enableUserLut");
            _userLut = serializedObject.FindProperty("userLut");

            _controlChromaticAberration = serializedObject.FindProperty("controlChromaticAberration");
            _enableChromaticAberration = serializedObject.FindProperty("enableChromaticAberration");
            _chromaticAberration = serializedObject.FindProperty("chromaticAberration");

            _controlGrain = serializedObject.FindProperty("controlGrain");
            _enableGrain = serializedObject.FindProperty("enableGrain");
            _grain = serializedObject.FindProperty("grain");

            _controlVignette = serializedObject.FindProperty("controlVignette");
            _enableVignette = serializedObject.FindProperty("enableVignette");
            _vignette = serializedObject.FindProperty("vignette");
        }

        public override void OnInspectorGUI()
        {
            var isPlaying = Application.isPlaying;

            serializedObject.Update();

            EditorGUILayout.Space();

            EditorGUILayout.LabelField("Antialiasing", EditorStyles.boldLabel);
            EditorGUILayout.PropertyField(_controlAntialiasing, _textEnableControl);
            if (isPlaying && _controlAntialiasing.boolValue)
            {
                EditorGUILayout.PropertyField(_enableAntialiasing, _textEnableEffect);
                if (_enableAntialiasing.boolValue)
                    EditorGUILayout.PropertyField(_antialiasing, _textSettings, true);
            }

            EditorGUILayout.Space();

            EditorGUILayout.LabelField("Ambient Occlusion", EditorStyles.boldLabel);
            EditorGUILayout.PropertyField(_controlAmbientOcclusion, _textEnableControl);
            if (isPlaying && _controlAmbientOcclusion.boolValue)
            {
                EditorGUILayout.PropertyField(_enableAmbientOcclusion, _textEnableEffect);
                if (_enableAmbientOcclusion.boolValue)
                    EditorGUILayout.PropertyField(_ambientOcclusion, _textSettings, true);
            }

            EditorGUILayout.Space();

            EditorGUILayout.LabelField("Screen Space Reflection", EditorStyles.boldLabel);
            EditorGUILayout.PropertyField(_controlScreenSpaceReflection, _textEnableControl);
            if (isPlaying && _controlScreenSpaceReflection.boolValue)
            {
                EditorGUILayout.PropertyField(_enableScreenSpaceReflection, _textEnableEffect);
                if (_enableScreenSpaceReflection.boolValue)
                    EditorGUILayout.PropertyField(_screenSpaceReflection, _textSettings, true);
            }

            EditorGUILayout.Space();

            EditorGUILayout.LabelField("Depth Of Field", EditorStyles.boldLabel);
            EditorGUILayout.PropertyField(_controlDepthOfField, _textEnableControl);
            if (isPlaying && _controlDepthOfField.boolValue)
            {
                EditorGUILayout.PropertyField(_enableDepthOfField, _textEnableEffect);
                if (_enableDepthOfField.boolValue)
                    EditorGUILayout.PropertyField(_depthOfField, _textSettings, true);
            }

            EditorGUILayout.Space();

            EditorGUILayout.LabelField("Motion Blur", EditorStyles.boldLabel);
            EditorGUILayout.PropertyField(_controlMotionBlur, _textEnableControl);
            if (isPlaying && _controlMotionBlur.boolValue)
            {
                EditorGUILayout.PropertyField(_enableMotionBlur, _textEnableEffect);
                if (_enableMotionBlur.boolValue)
                    EditorGUILayout.PropertyField(_motionBlur, _textSettings, true);
            }

            EditorGUILayout.Space();

            EditorGUILayout.LabelField("Eye Adaptation", EditorStyles.boldLabel);
            EditorGUILayout.PropertyField(_controlEyeAdaptation, _textEnableControl);
            if (isPlaying && _controlEyeAdaptation.boolValue)
            {
                EditorGUILayout.PropertyField(_enableEyeAdaptation, _textEnableEffect);
                if (_enableEyeAdaptation.boolValue)
                    EditorGUILayout.PropertyField(_eyeAdaptation, _textSettings, true);
            }

            EditorGUILayout.Space();

            EditorGUILayout.LabelField("Bloom", EditorStyles.boldLabel);
            EditorGUILayout.PropertyField(_controlBloom, _textEnableControl);
            if (isPlaying && _controlBloom.boolValue)
            {
                EditorGUILayout.PropertyField(_enableBloom, _textEnableEffect);
                if (_enableBloom.boolValue)
                    EditorGUILayout.PropertyField(_bloom, _textSettings, true);
            }

            EditorGUILayout.Space();

            EditorGUILayout.LabelField("Color Grading", EditorStyles.boldLabel);
            EditorGUILayout.PropertyField(_controlColorGrading, _textEnableControl);
            if (isPlaying && _controlColorGrading.boolValue)
            {
                EditorGUILayout.PropertyField(_enableColorGrading, _textEnableEffect);
                if (_enableColorGrading.boolValue)
                    EditorGUILayout.PropertyField(_colorGrading, _textSettings, true);
            }

            EditorGUILayout.Space();

            EditorGUILayout.LabelField("User Lut", EditorStyles.boldLabel);
            EditorGUILayout.PropertyField(_controlUserLut, _textEnableControl);
            if (isPlaying && _controlUserLut.boolValue)
            {
                EditorGUILayout.PropertyField(_enableUserLut, _textEnableEffect);
                if (_enableUserLut.boolValue)
                    EditorGUILayout.PropertyField(_userLut, _textSettings, true);
            }

            EditorGUILayout.Space();

            EditorGUILayout.LabelField("Chromatic Aberration", EditorStyles.boldLabel);
            EditorGUILayout.PropertyField(_controlChromaticAberration, _textEnableControl);
            if (isPlaying && _controlChromaticAberration.boolValue)
            {
                EditorGUILayout.PropertyField(_enableChromaticAberration, _textEnableEffect);
                if (_enableChromaticAberration.boolValue)
                    EditorGUILayout.PropertyField(_chromaticAberration, _textSettings, true);
            }

            EditorGUILayout.Space();

            EditorGUILayout.LabelField("Grain", EditorStyles.boldLabel);
            EditorGUILayout.PropertyField(_controlGrain, _textEnableControl);
            if (isPlaying && _controlGrain.boolValue)
            {
                EditorGUILayout.PropertyField(_enableGrain, _textEnableEffect);
                if (_enableGrain.boolValue)
                    EditorGUILayout.PropertyField(_grain, _textSettings, true);
            }

            EditorGUILayout.Space();

            EditorGUILayout.LabelField("Vignette", EditorStyles.boldLabel);
            EditorGUILayout.PropertyField(_controlVignette, _textEnableControl);
            if (isPlaying && _controlVignette.boolValue)
            {
                EditorGUILayout.PropertyField(_enableVignette, _textEnableEffect);
                if (_enableVignette.boolValue)
                    EditorGUILayout.PropertyField(_vignette, _textSettings, true);
            }

            serializedObject.ApplyModifiedProperties();
        }

        #endregion
    }
}

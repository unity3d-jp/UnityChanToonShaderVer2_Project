using UnityEngine;

namespace UnityEditor.Rendering
{

    /// <summary>
    /// Set of extensions to allow storing, getting and setting the expandable states of a <see cref="MaterialEditor"/> areas
    /// </summary>
    public static partial class UTS3MaterialEditorExtension
    {
        const string k_KeyPrefix = "CoreRP:Material:UI_State:";

        /// <summary>
        /// Obtains if an area is expanded in a <see cref="MaterialEditor"/>
        /// </summary>
        /// <param name="editor"><see cref="MaterialEditor"/></param>
        /// <param name="mask">The mask identifying the area to check the state</param>
        /// <param name="defaultExpandedState">Default value if is key is not present</param>
        /// <returns>true if the area is expanded</returns>
        internal static bool IsAreaExpanded(this MaterialEditor editor, uint mask, uint defaultExpandedState = uint.MaxValue)
        {
            string key = editor.GetEditorPrefsKey();

            if (EditorPrefs.HasKey(key))
            {
                uint state = (uint)EditorPrefs.GetInt(key);
                return (state & mask) > 0;
            }

            EditorPrefs.SetInt(key, (int)defaultExpandedState);
            return (defaultExpandedState & mask) > 0;
        }

        /// <summary>
        /// Sets if the area is expanded <see cref="MaterialEditor"/>
        /// </summary>
        /// <param name="editor"><see cref="MaterialEditor"/></param>
        /// <param name="mask">The mask identifying the area to check the state</param>
        internal static void SetIsAreaExpanded(this MaterialEditor editor, uint mask, bool value)
        {
            string key = editor.GetEditorPrefsKey();

            uint state = (uint)EditorPrefs.GetInt(key);

            if (value)
            {
                state |= mask;
            }
            else
            {
                mask = ~mask;
                state &= mask;
            }

            EditorPrefs.SetInt(key, (int)state);
        }

        static string GetEditorPrefsKey(this MaterialEditor editor)
        {
            return k_KeyPrefix + (editor.target as Material).shader.name;
        }
    }

    /// <summary>
    /// Set of extensions to handle more shader property drawer
    /// </summary>
    public static partial class MaterialEditorExtension
    {
        static Rect GetRect(MaterialProperty prop)
        {
            return EditorGUILayout.GetControlRect(true, MaterialEditor.GetDefaultPropertyHeight(prop), EditorStyles.layerMaskField);
        }

        /// <summary>
        /// Draw an integer property field for a float shader property.
        /// </summary>
        /// <param name="editor"><see cref="MaterialEditor"/></param>
        /// <param name="prop">The MaterialProperty to make a field for</param>
        /// <param name="label">Label for the property</param>
        /// <param name="transform">Optional function to apply on the new value</param>
        public static void IntShaderProperty(this MaterialEditor editor, MaterialProperty prop, GUIContent label, System.Func<int, int> transform = null)
        {
            EditorGUI.BeginChangeCheck();
            EditorGUI.showMixedValue = prop.hasMixedValue;
            int newValue = EditorGUI.IntField(GetRect(prop), label, (int)prop.floatValue);
            EditorGUI.showMixedValue = false;
            if (EditorGUI.EndChangeCheck())
            {
                if (transform != null)
                    newValue = transform(newValue);
                prop.floatValue = newValue;
            }
        }

        /// <summary>
        /// Draw an integer slider for a range shader property.
        /// </summary>
        /// <param name="editor"><see cref="MaterialEditor"/></param>
        /// <param name="prop">The MaterialProperty to make a field for</param>
        /// <param name="label">Label for the property</param>
        public static void IntSliderShaderProperty(this MaterialEditor editor, MaterialProperty prop, GUIContent label)
        {
            var limits = prop.rangeLimits;
            editor.IntSliderShaderPropertyUTS3(prop, (int)limits.x, (int)limits.y, label);
        }

        /// <summary>
        /// Draw an integer slider for a float shader property.
        /// </summary>
        /// <param name="editor"><see cref="MaterialEditor"/></param>
        /// <param name="prop">The MaterialProperty to make a field for</param>
        /// <param name="min">The value at the left end of the slider</param>
        /// <param name="max">The value at the right end of the slider</param>
        /// <param name="label">Label for the property</param>
        public static void IntSliderShaderPropertyUTS3(this MaterialEditor editor, MaterialProperty prop, int min, int max, GUIContent label)
        {
            EditorGUI.BeginChangeCheck();
            EditorGUI.showMixedValue = prop.hasMixedValue;
            int newValue = EditorGUI.IntSlider(GetRect(prop), label, (int)prop.floatValue, min, max);
            EditorGUI.showMixedValue = false;
            if (EditorGUI.EndChangeCheck())
            {
                editor.RegisterPropertyChangeUndo(label.text);
                prop.floatValue = newValue;
            }
        }

        /// <summary>
        /// Draw a property field for a float shader property.
        /// </summary>
        /// <param name="editor"><see cref="MaterialEditor"/></param>
        /// <param name="prop">The MaterialProperty to make a field for</param>
        /// <param name="label">Label for the property</param>
        /// <param name="min">The minimum value the user can specify</param>
        public static void MinFloatShaderProperty(this MaterialEditor editor, MaterialProperty prop, GUIContent label, float min)
        {
            EditorGUI.BeginChangeCheck();
            EditorGUI.showMixedValue = prop.hasMixedValue;
            float newValue = EditorGUI.FloatField(GetRect(prop), label, prop.floatValue);
            newValue = Mathf.Max(min, newValue);
            EditorGUI.showMixedValue = false;
            if (EditorGUI.EndChangeCheck())
                prop.floatValue = newValue;
        }

        /// <summary>
        /// Draw a popup selection field for a float shader property.
        /// </summary>
        /// <param name="editor"><see cref="MaterialEditor"/></param>
        /// <param name="prop">The MaterialProperty to make a field for</param>
        /// <param name="label">Label for the property</param>
        /// <param name="displayedOptions">An array with the options shown in the popup</param>
        /// <returns>The index of the option that has been selected by the user</returns>
        public static int PopupShaderProperty(this MaterialEditor editor, MaterialProperty prop, GUIContent label, string[] displayedOptions)
        {
            int val = (int)prop.floatValue;

            EditorGUI.BeginChangeCheck();
            EditorGUI.showMixedValue = prop.hasMixedValue;
            int newValue = EditorGUILayout.Popup(label, val, displayedOptions);
            EditorGUI.showMixedValue = false;
            if (EditorGUI.EndChangeCheck() && (newValue != val || prop.hasMixedValue))
            {
                editor.RegisterPropertyChangeUndo(label.text);
                prop.floatValue = val = newValue;
            }

            return val;
        }

        /// <summary>
        /// Draw an integer popup selection field for a float shader property.
        /// </summary>
        /// <param name="editor"><see cref="MaterialEditor"/></param>
        /// <param name="prop">The MaterialProperty to make a field for</param>
        /// <param name="label">Label for the property</param>
        /// <param name="displayedOptions">An array with the options shown in the popup</param>
        /// <param name="optionValues">An array with the values for each option</param>
        /// <returns>The value of the option that has been selected by the user</returns>
        public static int IntPopupShaderProperty(this MaterialEditor editor, MaterialProperty prop, string label, string[] displayedOptions, int[] optionValues)
        {
            int val = (int)prop.floatValue;

            EditorGUI.BeginChangeCheck();
            EditorGUI.showMixedValue = prop.hasMixedValue;
            int newValue = EditorGUILayout.IntPopup(label, val, displayedOptions, optionValues);
            EditorGUI.showMixedValue = false;
            if (EditorGUI.EndChangeCheck() && (newValue != val || prop.hasMixedValue))
            {
                editor.RegisterPropertyChangeUndo(label);
                prop.floatValue = val = newValue;
            }

            return val;
        }

        /// <summary>
        /// Draw a special slider to specify a range between a min and a max for two float shader properties.
        /// </summary>
        /// <param name="editor"><see cref="MaterialEditor"/></param>
        /// <param name="min">The MaterialProperty containing the lower value of the range the slider shows</param>
        /// <param name="max">The MaterialProperty containing the upper value of the range the slider shows</param>
        /// <param name="minLimit">The limit at the left end of the slider</param>
        /// <param name="maxLimit">The limit at the right end of the slider</param>
        /// <param name="label">Label for the property</param>
        public static void MinMaxShaderProperty(this MaterialEditor editor, MaterialProperty min, MaterialProperty max, float minLimit, float maxLimit, GUIContent label)
        {
            float minValue = min.floatValue;
            float maxValue = max.floatValue;
            EditorGUI.BeginChangeCheck();
            EditorGUILayout.MinMaxSlider(label, ref minValue, ref maxValue, minLimit, maxLimit);
            if (EditorGUI.EndChangeCheck())
            {
                min.floatValue = minValue;
                max.floatValue = maxValue;
            }
        }

        /// <summary>
        /// Draw a special slider to specify a range between a min and a max for a vector shader property.
        /// </summary>
        /// <param name="editor"><see cref="MaterialEditor"/></param>
        /// <param name="remapProp">The MaterialProperty containing the range the slider shows in the x and y components of its vectorValue</param>
        /// <param name="minLimit">The limit at the left end of the slider</param>
        /// <param name="maxLimit">The limit at the right end of the slider</param>
        /// <param name="label">Label for the property</param>
        public static void MinMaxShaderProperty(this MaterialEditor editor, MaterialProperty remapProp, float minLimit, float maxLimit, GUIContent label)
        {
            Vector2 remap = remapProp.vectorValue;

            EditorGUI.BeginChangeCheck();
            EditorGUILayout.MinMaxSlider(label, ref remap.x, ref remap.y, minLimit, maxLimit);
            if (EditorGUI.EndChangeCheck())
                remapProp.vectorValue = remap;
        }
    }

}

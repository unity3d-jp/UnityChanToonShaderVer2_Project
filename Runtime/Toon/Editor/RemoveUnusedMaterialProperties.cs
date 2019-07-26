//
// RemoveUnusedMaterialProperties.cs
// http://light11.hatenadiary.com/entry/2018/12/04/224253
//
#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;

public class Example {

    [MenuItem("CONTEXT/Material/Remove Unused Properties")]
    private static void RemoveUnusedMaterialProperties(MenuCommand menuCommand)
    {
        var material    = menuCommand.context as Material;
        if (material == null) {
            return;
        }

        var so          = new SerializedObject(material);
        so.Update();

        var savedProp   = so.FindProperty("m_SavedProperties");

        // Tex Envs
        var texProp     = savedProp.FindPropertyRelative("m_TexEnvs");
        for (int i = texProp.arraySize - 1; i >= 0; i--) {
            var propertyName    = texProp.GetArrayElementAtIndex(i).FindPropertyRelative("first").stringValue;
            if (!material.HasProperty(propertyName)) {
                texProp.DeleteArrayElementAtIndex(i);
            }
        }

        // Floats
        var floatProp   = savedProp.FindPropertyRelative("m_Floats");
        for (int i = floatProp.arraySize - 1; i >= 0; i--) {
            var propertyName    = floatProp.GetArrayElementAtIndex(i).FindPropertyRelative("first").stringValue;
            if (!material.HasProperty(propertyName)) {
                floatProp.DeleteArrayElementAtIndex(i);
            }
        }

        // Colors
        var colorProp   = savedProp.FindPropertyRelative("m_Colors");
        for (int i = colorProp.arraySize - 1; i >= 0; i--) {
            var propertyName    = colorProp.GetArrayElementAtIndex(i).FindPropertyRelative("first").stringValue;
            if (!material.HasProperty(propertyName)) {
                colorProp.DeleteArrayElementAtIndex(i);
            }
        }

        so.ApplyModifiedProperties();
    }
}
#endif
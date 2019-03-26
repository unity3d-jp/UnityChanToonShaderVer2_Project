using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace UnityChan
{

	public class RemoveUnusedShaderKeywordsFromUTS2Material : EditorWindow 
	{
		[SerializeField]
		static Material source;
		static Material oldSource;
		Material target;
		static EditorWindow window;
		bool _RemovedUnusedParameterMessage = false; 

    	[MenuItem("CONTEXT/Material/Remove Unused ShaderKeywords from UTS2 Material")]
		static void Init (MenuCommand command)
		{
			source = (Material) command.context;
			window = EditorWindow.GetWindow<RemoveUnusedShaderKeywordsFromUTS2Material> (true, "Remove Unused ShaderKeywords : Select UTS2 Material", true);
			window.Show ();
		}

		void OnGUI ()
		{
			source = (Material)EditorGUILayout.ObjectField (" UTS2 material", source, typeof(Material),true);

            EditorGUILayout.Space();

			if (GUILayout.Button ("Remove Unused ShaderKeywords")) {
				RemoveUnusedMaterialProperties(source);
				RemoveShaderKeywords(source);
				_RemovedUnusedParameterMessage = true;
				oldSource = source;
				//window.Close();
			}

			if(_RemovedUnusedParameterMessage){
				EditorGUILayout.HelpBox("Unused Material Properties and ShaderKeywords are removed.",MessageType.Info);
			}

			if(source != oldSource){
				_RemovedUnusedParameterMessage = false;
			}

		}

		void RemoveShaderKeywords(Material material)
		{
			string shaderKeywords = "";

			if(material.HasProperty("_EMISSIVE")){
				float outlineMode = material.GetFloat("_EMISSIVE");
				if(outlineMode == 0)
				{
					shaderKeywords = shaderKeywords + "_EMISSIVE_SIMPLE";
				}else{
					shaderKeywords = shaderKeywords + "_EMISSIVE_ANIMATION";
				}
			}
			if(material.HasProperty("_OUTLINE")){
				float outlineMode = material.GetFloat("_OUTLINE");
				if(outlineMode == 0)
				{
					shaderKeywords = shaderKeywords + " _OUTLINE_NML";
				}else{
					shaderKeywords = shaderKeywords + " _OUTLINE_POS";
				}
			}

			var so = new SerializedObject(material);
			so.Update();
			so.FindProperty("m_ShaderKeywords").stringValue = shaderKeywords;
			so.ApplyModifiedProperties();
		}

		// http://light11.hatenadiary.com/entry/2018/12/04/224253
		void RemoveUnusedMaterialProperties(Material material)
		{
			var sourceProps = new SerializedObject(material);
			sourceProps.Update();

			var savedProp = sourceProps.FindProperty("m_SavedProperties");

			// Tex Envs
			var texProp = savedProp.FindPropertyRelative("m_TexEnvs");
			for (int i = texProp.arraySize - 1; i >= 0; i--) {
				var propertyName = texProp.GetArrayElementAtIndex(i).FindPropertyRelative("first").stringValue;
				if (!material.HasProperty(propertyName)) {
					texProp.DeleteArrayElementAtIndex(i);
				}
			}

			// Floats
			var floatProp = savedProp.FindPropertyRelative("m_Floats");
			for (int i = floatProp.arraySize - 1; i >= 0; i--) {
				var propertyName = floatProp.GetArrayElementAtIndex(i).FindPropertyRelative("first").stringValue;
				if (!material.HasProperty(propertyName)) {
					floatProp.DeleteArrayElementAtIndex(i);
				}
			}

			// Colors
			var colorProp = savedProp.FindPropertyRelative("m_Colors");
			for (int i = colorProp.arraySize - 1; i >= 0; i--) {
				var propertyName = colorProp.GetArrayElementAtIndex(i).FindPropertyRelative("first").stringValue;
				if (!material.HasProperty(propertyName)) {
					colorProp.DeleteArrayElementAtIndex(i);
				}
			}
			sourceProps.ApplyModifiedProperties();
		}

	}

}

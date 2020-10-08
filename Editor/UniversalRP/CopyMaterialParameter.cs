using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace UnityEditor.Rendering.Universal.Toon
{
	public class CopyMaterialParameter : EditorWindow
	{
		[SerializeField]
		static Material source;
		Material target;
		static EditorWindow window;
		bool _RemovedUnusedParameter = false; 

		[MenuItem("CONTEXT/Material/Copy Material Parameter")]
		static void Init (MenuCommand command)
		{
			source = (Material) command.context;
			window = EditorWindow.GetWindow<CopyMaterialParameter> (true, "Copy Material Parameter : Select Materials", true);
			window.Show ();
		}

		void OnGUI ()
		{
			source = (Material)EditorGUILayout.ObjectField ("Source material", source, typeof(Material),true);
			target = (Material)EditorGUILayout.ObjectField ("Target material", target, typeof(Material),true);

            EditorGUILayout.Space();

			if (GUILayout.Button ("Copy Sorce ⇒ Target")) {
				CopyMaterial (source, target);
				window.Close();
			}

			if (GUILayout.Button ("Switch Sorce/Target")) {
				_RemovedUnusedParameter = false;
				var tmp = target;
				target = source;
				source = tmp;
			}

			if (GUILayout.Button ("Remove Unused Properties from Sorce")) {
				RemoveUnusedMaterialProperties(source);
				_RemovedUnusedParameter = true;
			}

			if(_RemovedUnusedParameter){
				EditorGUILayout.HelpBox("Unused Material Properties are removed.",MessageType.Info);
			}
		}

		void CopyMaterial (Material source, Material target)
		{
			target.shader = source.shader;
			target.CopyPropertiesFromMaterial (source);
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

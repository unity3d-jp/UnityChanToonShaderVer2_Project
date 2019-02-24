using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace UnityChan
{
	public class CopyMaterialParameter : EditorWindow
	{
		[SerializeField]
		static Material source;
		Material target;
		static EditorWindow window;

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
				var tmp = target;
				target = source;
				source = tmp;
			}


		}

		void CopyMaterial (Material source, Material target)
		{
			target.shader = source.shader;
			target.CopyPropertiesFromMaterial (source);
		}
	}	
}

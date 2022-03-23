using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using System.Linq;
using System;
using System.Text;
using UnityEngine.UIElements;
namespace UnityEditor.Rendering.Toon
{
    internal  class ProjectSettingProvider : SettingsProvider
    {
        #region fields



        GUIContent guiContentShowConverterStartup = new GUIContent("Show converter on start up");
        #endregion

        [SettingsProvider]
        private static SettingsProvider CreateProjectSettingsProvider()
        {
            var path = "Project/Unity Toon Shader";

            var provider = new ProjectSettingProvider(path, SettingsScope.Project);
            provider.keywords = new[] { "Toon Shader", "ToonShader", "UTS" };

            return provider;
        }

        internal ProjectSettingProvider(string path, SettingsScope scope)
            : base(path, scope)
        {



        }


        public override void OnActivate
        (
            string searchContext,
            VisualElement rootElement
        )
        {

        }


        public override void OnDeactivate()
        {

        }

        public override void OnGUI(string searchContext)
        {
            using (new GUIScope())
            {
 
                EditorGUILayout.BeginVertical();


                UnityToonShaderSettings.instance.m_ShowConverter = EditorGUILayout.Toggle(guiContentShowConverterStartup, UnityToonShaderSettings.instance.m_ShowConverter);

                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.Space();



                if (GUILayout.Button("Apply"))
                {
                    AssetDatabase.SaveAssets();
                }
                EditorGUILayout.EndHorizontal();
                EditorGUILayout.EndVertical();

            }
        }


        public override void OnTitleBarGUI()
        {
            //            EditorGUILayout.LabelField("");
        }



        public override void OnFooterBarGUI()
        {
            EditorGUILayout.LabelField("Settings become effective after pressing Apply button.");
        }


    }
    internal class GUIScope : GUI.Scope
    {
        float m_LabelWidth;
        public GUIScope(float layoutMaxWidth)
        {
            m_LabelWidth = EditorGUIUtility.labelWidth;
            EditorGUIUtility.labelWidth = 250;
            GUILayout.BeginHorizontal();
            GUILayout.Space(10);
            GUILayout.BeginVertical();
            GUILayout.Space(15);
        }

        public GUIScope() : this(500)
        {
        }

        protected override void CloseScope()
        {
            GUILayout.EndVertical();
            GUILayout.EndHorizontal();
            EditorGUIUtility.labelWidth = m_LabelWidth;
        }
    }
}
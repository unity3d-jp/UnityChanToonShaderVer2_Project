using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;
using Tests;
using System.Linq;

namespace UnityEditor.Rendering.Toon
{
    public class UniversalGraphicsTestSetup : EditorWindow
    {
        // https://docs.unity3d.com/ScriptReference/EditorBuildSettings-scenes.html
        Vector2 m_scrollPos;
        bool m_initialzed;
        List<EditorBuildSettingsScene> m_SceneAssets = new List<EditorBuildSettingsScene>();
        List<string> m_SceneNames = new List<string>();

        string[] monobehavioursToDisable =
        {
            "Rotator",
            "IdleChanger",
            "AutoBlink",
            "AutoBlinkforSD",
            "FaceUpdate",
            "IdleChanger",
            "IKCtrlRightHand",
            "RandomWind",
            "RefleshProbe",
            "SpringManager",
            "Animation",
            "Animator"
        };

        [MenuItem("Window/Toon Shader/Universal/Graphics Test Setup", false, 9999)]
        static private void OpenWindow()
        {
            var window = GetWindow<UniversalGraphicsTestSetup>(true, "Graphics Test Setup");
            window.Show();
        }
        private void OnGUI()
        {
            if (!m_initialzed)
            {

                for (int ii = 0; ii < EditorBuildSettings.scenes.Length; ii++)
                {
                    m_SceneNames.Add(EditorBuildSettings.scenes[ii].ToString());

                }
                m_initialzed = true;
            }


            // scroll view 
            m_scrollPos =
                 EditorGUILayout.BeginScrollView(m_scrollPos, GUILayout.Width(position.width - 4));
            EditorGUILayout.BeginVertical();


            int sceneCount = 0;

            for (int ii = 0; ii < EditorBuildSettings.scenes.Length; ii++)
            {
                var guid = EditorBuildSettings.scenes[ii].guid;
                string path = EditorBuildSettings.scenes[ii].path;
                if (EditorBuildSettings.scenes[ii].enabled )
                {
                    SceneAsset scene = AssetDatabase.LoadAssetAtPath<SceneAsset>(path);

                    sceneCount++;

                    EditorGUILayout.BeginHorizontal();
                    GUILayout.Space(16);
                    string str = "" + sceneCount + ":";

                    EditorGUILayout.LabelField(str, GUILayout.Width(40));
                    EditorGUILayout.LabelField(scene.name, GUILayout.Width(Screen.width - 130));
                    GUILayout.Space(1);
                    EditorGUILayout.EndHorizontal();
                }
            }

            EditorGUILayout.EndVertical();
            EditorGUILayout.EndScrollView();



            using (new EditorGUI.DisabledScope(EditorBuildSettings.scenes.Length == 0))
            {
                EditorGUILayout.BeginHorizontal();
                if ( GUILayout.Button("Set up scenes above.") )
                {
                    for ( int sceneIndex = 0; sceneIndex < EditorBuildSettings.scenes.Length; sceneIndex++)
                    {
                        SetupScenes(sceneIndex);
                    }
                }

                EditorGUILayout.EndHorizontal();
            }
        }
        void SetupScenes(int scneneIndex)
        {
            var scene = EditorSceneManager.OpenScene(EditorBuildSettings.scenes[scneneIndex].path);
            var cameras = GameObject.FindGameObjectsWithTag("MainCamera").Select(x => x.GetComponent<Camera>());
            var cameraList = cameras.ToList<Camera>();
            if ( cameraList == null || cameraList.Count == 0 )
            {
                Debug.LogError("Unable to Find MainCamera in " + EditorBuildSettings.scenes[scneneIndex].path );
            }
            UniversalUTS_GraphicsTestSettings settings = cameraList[0].gameObject.GetComponent<UniversalUTS_GraphicsTestSettings>();


            if ( settings == null )
            {
                settings = cameraList[0].gameObject.AddComponent<UniversalUTS_GraphicsTestSettings>();
            }
            settings.ImageComparisonSettings.ImageResolution = UnityEngine.TestTools.Graphics.ImageComparisonSettings.Resolution.w960h540;
            settings.ImageComparisonSettings.PerPixelCorrectnessThreshold = 0.005f;
            settings.CheckMemoryAllocation = false;
            settings.WaitFrames = 480;
            foreach (GameObject obj in FindObjectsOfType(typeof(GameObject)))
            {
                if (! obj.activeInHierarchy )
                {
                    continue;
                }
                for ( int jj = 0; jj < monobehavioursToDisable.Length; jj++)
                {
                    var component = obj.GetComponent(monobehavioursToDisable[jj]);
                    var mb = component as Behaviour;
                    if (mb == null )
                    {
                        continue;
                    }
                    mb.enabled = false;
                }
                var renderer = obj.GetComponent<Renderer>();
                if (renderer != null)
                {
                    for (int kk = 0; kk < renderer.sharedMaterials.Length; kk++)
                    {
                        var mat = renderer.sharedMaterials[kk];
                        if (mat == null)
                        {
                            continue;
                        }
                        if (mat.IsKeywordEnabled("_EMISSIVE_ANIMATION"))
                        {
                            mat.SetFloat("_EMISSIVE", 0);
                            mat.EnableKeyword("_EMISSIVE_SIMPLE");
                            mat.DisableKeyword("_EMISSIVE_ANIMATION");
                        }
                    }
                }
            }

            EditorSceneManager.SaveScene(scene);
        }
    }
}
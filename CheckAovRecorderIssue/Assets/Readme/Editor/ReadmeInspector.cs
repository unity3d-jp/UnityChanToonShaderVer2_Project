#define ENABLE_EDIT_MODE

using UnityEngine;
using UnityEditor;
using UnityEditor.PackageManager;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;


[CustomEditor(typeof(Readme))]
[InitializeOnLoad]
public class ReadmeInspector : Editor {

    
    static ReadmeInspector() {
        EditorApplication.delayCall += SelectReadmeAutomatically;
    }
    
    [MenuItem("Tutorial/Show Tutorial Instructions")]
    static Readme SelectReadme() {
        var ids = AssetDatabase.FindAssets("Readme t:Readme");
        if (ids.Length == 1) {
            var readmeObject = AssetDatabase.LoadMainAssetAtPath(AssetDatabase.GUIDToAssetPath(ids[0]));

            Selection.objects = new UnityEngine.Object[] {readmeObject};

            return (Readme) readmeObject;
        }
        else {
            Debug.Log("Couldn't find a readme");
            return null;
        }
    }
    

    static void SelectReadmeAutomatically() {
        if (SessionState.GetBool(SHOWED_README_SESSION_STATE_NAME, false)) 
            return;
        
        SelectReadme();
        string layoutKey = $"AnimeToolbox-{Application.productName}-{LAYOUT_LOADED_FIRST_TIME}";  
        if (EditorPrefs.GetBool(layoutKey)) 
            return;
        
        LoadCustomLayout(LAYOUT_PATH);
        EditorPrefs.SetBool(layoutKey, true);
    }


    private static void LoadCustomLayout(string layoutPath) {
        if (System.IO.File.Exists(layoutPath)) {
            EditorUtility.LoadWindowLayout(layoutPath);
        }
        else {
            Debug.LogWarning("Layout not loaded. Layout file missing at: " + layoutPath);
        }
    }

//----------------------------------------------------------------------------------------------------------------------    
    protected override void OnHeaderGUI() {
        Readme readme = (Readme) target;
        Init();

        float iconWidth = Mathf.Min(EditorGUIUtility.currentViewWidth / 3f - 20f, 128f);

        GUILayout.BeginHorizontal("In BigTitle");
        {
            GUILayout.Label(readme.icon, GUILayout.Width(iconWidth), GUILayout.Height(iconWidth));
            GUILayout.Label(readme.title, m_titleStyle);
        }
        GUILayout.EndHorizontal();
    }

//----------------------------------------------------------------------------------------------------------------------    
    
    public override void OnInspectorGUI() {

#if ENABLE_EDIT_MODE        
        m_editMode = EditorGUILayout.Toggle("Edit Mode", m_editMode);
        EditorGUILayout.Space(15);
        if (m_editMode) {
            base.OnInspectorGUI();
            return;
        }
#endif //ENABLE_EDIT_MODE        
        
        var readme = (Readme) target;
        Init();

        if (GUILayout.Button("Anime Toolbox のレイアウトを適応する")) {
            LoadCustomLayout(LAYOUT_PATH);
        }

        //sections and included packages
        GUILayout.Space(SPACE);
        foreach (Readme.Section section in readme.sections) {
            if (string.IsNullOrEmpty(section.heading) && string.IsNullOrEmpty(section.text))
                continue;
                
            DrawSection(section);
            GUILayout.Space(SPACE);
        }
        
        //Optional packages
        GUILayout.Space(SPACE);
        foreach (Readme.PackageInstallSection section in readme.optionalPackages) {
            if (string.IsNullOrEmpty(section.heading) && string.IsNullOrEmpty(section.text))
                continue;

            DrawSection(section);           
            GUILayout.Space(SPACE);

            if (string.IsNullOrEmpty(section.installText))
                continue;
            
            if (GUILayout.Button(section.installText)) {
                Client.Add(section.installURL);
                
            }
            GUILayout.Space(SPACE);
        }
        
    }

    void DrawSection(Readme.Section section) {
        DrawLabel(section.heading, m_headingStyle);
        DrawLabel(section.text, m_bodyStyle);

        if (!string.IsNullOrEmpty(section.linkText)) {
            if (LinkLabel(new GUIContent(section.linkText))) {
                Application.OpenURL(section.url);
            }
        }
        
    }

    static void DrawLabel(string str, GUIStyle style) {
        if (string.IsNullOrEmpty(str)) 
            return;
            
        GUILayout.Label(str, style);        
    }

    
//----------------------------------------------------------------------------------------------------------------------    

    void Init() {
        if (m_Initialized)
            return;
        m_bodyStyle          = new GUIStyle(EditorStyles.label);
        m_bodyStyle.wordWrap = true;
        m_bodyStyle.fontSize = 14;

        m_titleStyle          = new GUIStyle(m_bodyStyle);
        m_titleStyle.fontSize = 26;

        m_headingStyle          = new GUIStyle(m_bodyStyle);
        m_headingStyle.fontSize = 18;

        m_linkStyle          = new GUIStyle(m_bodyStyle);
        m_linkStyle.wordWrap = false;
        // Match selection color which works nicely for both light and dark skins
        m_linkStyle.normal.textColor = new Color(0x00 / 255f, 0x78 / 255f, 0xDA / 255f, 1f);
        m_linkStyle.stretchWidth     = false;

        m_Initialized = true;
    }

    bool LinkLabel(GUIContent label, params GUILayoutOption[] options) {
        var position = GUILayoutUtility.GetRect(label, m_linkStyle, options);

        Handles.BeginGUI();
        Handles.color = m_linkStyle.normal.textColor;
        Handles.DrawLine(new Vector3(position.xMin, position.yMax), new Vector3(position.xMax, position.yMax));
        Handles.color = Color.white;
        Handles.EndGUI();

        EditorGUIUtility.AddCursorRect(position, MouseCursor.Link);

        return GUI.Button(position, label, m_linkStyle);
    }
    
//----------------------------------------------------------------------------------------------------------------------
    
    private const string SHOWED_README_SESSION_STATE_NAME = "ReadmeInspector.showedReadme";
    private const string LAYOUT_PATH                      = "Assets/Layouts/AnimeToolboxLayout.wlt";
    private const string LAYOUT_LOADED_FIRST_TIME         = "LayoutLoadedFirstTime"; 
    
    const float SPACE     = 16f;
    

    private bool m_editMode = false;
    private bool m_Initialized;

    [FormerlySerializedAs("m_LinkStyle")][SerializeField]    GUIStyle m_linkStyle;
    [FormerlySerializedAs("m_TitleStyle")][SerializeField]   GUIStyle m_titleStyle;
    [FormerlySerializedAs("m_HeadingStyle")][SerializeField] GUIStyle m_headingStyle;
    [FormerlySerializedAs("m_BodyStyle")][SerializeField]    GUIStyle m_bodyStyle;
    

}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;
using UnityEditor.UIElements;
using System;
using System.Linq;
using System.IO;
using UnityEngine.Rendering;

namespace UnityEditor.Rendering.Toon
{
    internal class UTS3Converter : EditorWindow
    {

        // Status for each row item to say in which state they are in.
        // This will make sure they are showing the correct icon
        [Serializable]
        enum Status
        {
            Idle,
            MaterialScanError,
            MaterialScanErrorNextScreen,
            MaterialScanSuccess,
            MaterialConvertingSuccess
        }
        [SerializeField]
        Status m_Status = Status.Idle;
        // This is the serialized class that stores the state of each item in the list of items to convert
        [Serializable]
        class ConverterItemState
        {
            public bool isActive;

            // Message that will be displayed on the icon if warning or failed.
            public string message;

            // Status of the converted item, Pending, Warning, Error or Success
            public Status status;

            internal bool hasConverted = false;
        }

        // Each converter uses the active bool
        // Each converter has a list of active items/assets
        // We do this so that we can use the binding system of the UI Elements
        [Serializable]
        class ConverterState
        {
            // This is the enabled state of the whole converter
            public bool isEnabled;
            public bool isActive;
            public bool isLoading; // to name
            public bool isInitialized;
            public List<ConverterItemState> items = new List<ConverterItemState>();

            public int pending;
            public int warnings;
            public int errors;
            public int success;
            internal int index;

            public bool isActiveAndEnabled => isEnabled && isActive;
            public bool requiresInitialization => !isInitialized && isActiveAndEnabled;
        }


        // these are not included in CoreEditorStyles in 2020.3 and so on.
        /// <summary> Warning icon </summary>
        public static Texture2D iconWarn;
        /// <summary> Help icon </summary>
        public static Texture2D iconHelp;
        /// <summary> Fail icon </summary>
        public static Texture2D iconFail;
        /// <summary> Success icon </summary>
        public static Texture2D iconSuccess;
        /// <summary> Complete icon </summary>
        public static Texture2D iconComplete;
        /// <summary> Pending icon </summary>
        public static Texture2D iconPending;



        public VisualTreeAsset converterEditorAsset;
        public VisualTreeAsset converterItem;
        public VisualTreeAsset converterWidgetMainAsset;
        public VisualTreeAsset converterItemMaterial;






        Button m_ConvertButton;
        Button m_ScanButton;

        Button m_ContainerHelpButton;

        PopupVE m_ConversionsDropDown;
        TextElement m_ConversionInfoText;
        bool m_InitAndConvert;


 

        SerializedObject m_SerializedObject;

        List<string> m_ContainerChoices = new List<string>();
        List<RenderPipelineConverterContainer> m_Containers = new List<RenderPipelineConverterContainer>();
        int m_ContainerChoiceIndex = 0;
        int m_WorkerCount;

        // This is a list of Converter States which holds a list of which converter items/assets are active
        // There is one for each Converter.
        [SerializeField] List<ConverterState> m_ConverterStates = new List<ConverterState>();

        TypeCache.TypeCollection m_ConverterContainers;

        RenderPipelineConverterContainer currentContainer => m_Containers[m_ContainerChoiceIndex];

        UTS3GUI.RenderPipeline m_selectedRenderPipeline;

        internal static ScrollView scrollView { get; set; }

        [MenuItem("Window/Rendering/Unity Toon Shader Converter", false, 51)]
        public static void ShowWindow()
        {
            UTS3Converter wnd = GetWindow<UTS3Converter>();
            wnd.titleContent = new GUIContent("Unity Toon Shader Converter");

            wnd.minSize = new Vector2(650f, 400f);
            wnd.Show();
        }

        private void CreateGUI()
        {
#if false
            iconHelp = CoreEditorUtils.FindTexture("_Help");
#endif
            iconWarn = CoreEditorUtils.LoadIcon("icons", "console.warnicon", ".png");
            iconFail = CoreEditorUtils.LoadIcon("icons", "console.erroricon", ".png");
            iconSuccess = EditorGUIUtility.FindTexture("TestPassed");
            iconComplete = CoreEditorUtils.LoadIcon("icons", "GreenCheckmark", ".png");
            iconPending = EditorGUIUtility.FindTexture("Toolbar Minus");


            string theme = EditorGUIUtility.isProSkin ? "dark" : "light";
            InitIfNeeded();

            if (m_ConverterContainers.Any())
            {
                m_SerializedObject = new SerializedObject(this);
                converterEditorAsset.CloneTree(rootVisualElement);

                m_ConversionInfoText = rootVisualElement.Q<TextElement>("conversionInfo");

                m_ConversionsDropDown = rootVisualElement.Q<PopupVE>("conversionsDropDown");
                m_ConversionsDropDown.choices = m_ContainerChoices;
                m_ConversionsDropDown.index = m_ContainerChoiceIndex;

                // Getting the scrollview where the converters should be added
                scrollView = rootVisualElement.Q<ScrollView>("convertersScrollView");

                m_ConvertButton = rootVisualElement.Q<Button>("convertButton");
                m_ConvertButton.RegisterCallback<ClickEvent>(Convert);

                m_ScanButton = rootVisualElement.Q<Button>("scanButton");
                m_ScanButton.RegisterCallback<ClickEvent>(ScanProject);

                m_ContainerHelpButton = rootVisualElement.Q<Button>("containerHelpButton");

#if UNITY_2021_2_OR_NEWER
                m_ContainerHelpButton.RegisterCallback<ClickEvent>(GotoHelpURL);
                m_ContainerHelpButton.Q<Image>("containerHelpImage").image = CoreEditorStyles.iconHelp;
                m_ContainerHelpButton.RemoveFromClassList("unity-button");
                m_ContainerHelpButton.AddToClassList(theme);
#else
                m_ContainerHelpButton.style.display = DisplayStyle.None;
#endif
                RecreateUI();
            }

        }

        void GotoHelpURL(ClickEvent evt)
        {

            Help.BrowseURL("https://github.com/Unity-Technologies/com.unity.toonshader/blob/development/v1/com.unity.toonshader/Documentation~/index.md");

        }

        static string packageFullPath
        {
            get; set;
        }
        void OnEnable()
        {
            InitIfNeeded();

        }

        private static string GetPackageFullPath()
        {
            const string kUtsPackageName = "com.unity.toonshader";
            // Check for potential UPM package
            string packagePath = Path.GetFullPath("Packages/" + kUtsPackageName);
            if (Directory.Exists(packagePath))
            {
                return packagePath;
            }
            return null;
        }
        void InitIfNeeded()
        {
            m_selectedRenderPipeline = UTS3GUI.currentRenderPipeline;
            packageFullPath = GetPackageFullPath();

            // This is the drop down choices.
            m_ConverterContainers = TypeCache.GetTypesDerivedFrom<RenderPipelineConverterContainer>();
            foreach (var containerType in m_ConverterContainers)
            {
                var container = (RenderPipelineConverterContainer)Activator.CreateInstance(containerType);
                m_Containers.Add(container);
            }

            // this need to be sorted by Priority property
            m_Containers = m_Containers
                .OrderBy(o => o.priority).ToList();

            foreach (var container in m_Containers)
            {
                m_ContainerChoices.Add(container.name);
            }

            if (m_ConverterContainers.Any())
            {
            //    GetConverters();
            }
            else
            {
                ClearConverterStates();
            }
        }

        void ClearConverterStates()
        {

            m_ConverterStates.Clear();
        }


        private bool SaveCurrentSceneAndContinue()
        {
#if false
            Scene currentScene = SceneManager.GetActiveScene();
            if (currentScene.isDirty)
            {
                if (EditorUtility.DisplayDialog("Scene is not saved.",
                    "Current scene is not saved. Please save the scene before continuing.", "Save and Continue",
                    "Cancel"))
                {
                    EditorSceneManager.SaveScene(currentScene);
                }
                else
                {
                    return false;
                }
            }
#endif
            return true;
        }

        void ScanProject(ClickEvent evt)
        {

            currentContainer.CommonSetup();
            int errorCount = currentContainer.CountErrors(addToScrollView:true);
            if (errorCount == 0)
            {
                m_Status = Status.MaterialScanSuccess;
                currentContainer.SetupConverter();
                ApplyConverterStatus();
            }
            else
            {
                m_Status = Status.MaterialScanErrorNextScreen;
                ErrorConverterLayout();
            }
        }

        void BackToConverters(ClickEvent evt)
        {
            m_Status = Status.MaterialScanError;
            HideConverterLayout();
            ApplyConverterStatus();
        }

        void RecreateUI()
        {
            m_SerializedObject.Update();
            // This is temp now to get the information filled in

            scrollView.Clear();
            m_ConversionsDropDown.RegisterCallback<ChangeEvent<string>>((evt) =>
            {
                m_ContainerChoiceIndex = m_ConversionsDropDown.index;

                m_ConversionInfoText.text = currentContainer.info;
                currentContainer.Reset();
                m_Status = Status.Idle;
            });


            ApplyConverterStatus();

            rootVisualElement.Bind(m_SerializedObject);
        }




        void ErrorConverterLayout()
        {

            rootVisualElement.Q<VisualElement>("topInformationVE").style.display = DisplayStyle.None;
            rootVisualElement.Q<VisualElement>("singleConverterVE").style.display = DisplayStyle.Flex;
            // rootVisualElement.Q<VisualElement>("singleConverterVE").Add(element);
            //            element.Q<VisualElement>("converterItems").style.display = DisplayStyle.Flex;
            //            element.Q<VisualElement>("informationVE").style.display = DisplayStyle.Flex;

            rootVisualElement.Q<Button>("backButton").RegisterCallback<ClickEvent>(BackToConverters);
        }

        void HideConverterLayout()
        {
            rootVisualElement.Q<VisualElement>("topInformationVE").style.display = DisplayStyle.Flex;
            rootVisualElement.Q<VisualElement>("singleConverterVE").style.display = DisplayStyle.None;
            // rootVisualElement.Q<VisualElement>("singleConverterVE").Remove(element);

            //element.Q<VisualElement>("converterItems").style.display = DisplayStyle.None;
            //element.Q<VisualElement>("informationVE").style.display = DisplayStyle.None;

            RecreateUI();

        }

        void ApplyConverterStatus()
        {
            switch (m_Status)
            {
                case Status.Idle:
                    m_ConvertButton.style.display = DisplayStyle.None;
                    m_ScanButton.style.display = DisplayStyle.Flex;
                    break;
                case Status.MaterialScanErrorNextScreen:
                    m_ConvertButton.style.display = DisplayStyle.None;
                    m_ScanButton.style.display = DisplayStyle.None;
                    break;
                case Status.MaterialScanSuccess:
                    m_ConvertButton.style.display = DisplayStyle.Flex;
                    m_ScanButton.style.display = DisplayStyle.Flex;
                    break;
                case Status.MaterialScanError:
                    m_ConvertButton.style.display = DisplayStyle.None;
                    m_ScanButton.style.display = DisplayStyle.Flex;
                    break;
                default:
                    m_ConvertButton.style.display = DisplayStyle.Flex;
                    m_ScanButton.style.display = DisplayStyle.None;

                    break;
            }
        }

        internal static void DontSaveToLayout(EditorWindow wnd)
        {
#if true
            return;
#else
            // Making sure that the window is not saved in layouts.
            Assembly assembly = typeof(EditorWindow).Assembly;
            var editorWindowType = typeof(EditorWindow);
            var hostViewType = assembly.GetType("UnityEditor.HostView");
            var containerWindowType = assembly.GetType("UnityEditor.ContainerWindow");
            var parentViewField = editorWindowType.GetField("m_Parent", BindingFlags.Instance | BindingFlags.NonPublic);
            var parentViewValue = parentViewField.GetValue(wnd);
            // window should not be saved to layout
            var containerWindowProperty =
                hostViewType.GetProperty("window", BindingFlags.Instance | BindingFlags.Public);
            var parentContainerWindowValue = containerWindowProperty.GetValue(parentViewValue);
            var dontSaveToLayoutField =
                containerWindowType.GetField("m_DontSaveToLayout", BindingFlags.Instance | BindingFlags.NonPublic);
            dontSaveToLayoutField.SetValue(parentContainerWindowValue, true);
#endif
        }


        void Convert(ClickEvent evt)
        {
            currentContainer.Convert();
            scrollView.Clear();
        }

    }
}
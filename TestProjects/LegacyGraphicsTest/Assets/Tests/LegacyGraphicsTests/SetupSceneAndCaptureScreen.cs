using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Experimental.Rendering;
using UnityEngine.Profiling;
using System;
// GameViewSizeHelper
// https://github.com/anchan828/unity-GameViewSizeHelper

namespace Tests
{
    public class SetupSceneAndCaptureScreen
    {



        static Texture2D s_refereceTexure2D;
        static Texture2D s_capturedTexture2D;
        const int defaultWidth = 960;
        const int defaultHeight = 540;
        [MenuItem("Edit/Unity Toon shader test/Setup Scene and Create Reference Image")]
        private static void SetupSceneAndCreateReferenceImage()
        {
            SetupScene();
            CreateRenferenceImage2(960, 540);
        }
        private static void SetupScene()
        {
            Debug.Log("SetupScene");
            var scenePath = "Assets/Scenes/LightAndShadows/LightAndShadows.unity";

            EditorSceneManager.OpenScene(scenePath, OpenSceneMode.Single);
//            SetupGameViewSize();
        }

        private static void CreateRenferenceImage2(int width, int height)
        {
            int gcAllocThreshold = 2;
            var mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
            var defaultFormat = SystemInfo.GetGraphicsFormat(DefaultFormat.LDR);
            RenderTextureDescriptor desc = new RenderTextureDescriptor(width, height, defaultFormat, 24);

            var rt = RenderTexture.GetTemporary(desc);
            try
            {
                mainCamera.targetTexture = rt;

                // Render the first frame at this resolution (Alloc are allowed here)
                mainCamera.Render();

                var gcAllocRecorder = Recorder.Get("GC.Alloc");
                gcAllocRecorder.FilterToCurrentThread();

                Profiler.BeginSample("GraphicTests_GC_Alloc_Check");
                {
                    gcAllocRecorder.enabled = true;
                    mainCamera.Render();
                    gcAllocRecorder.enabled = false;
                }
                Profiler.EndSample();

                // There are 2 GC.Alloc overhead for calling Camera.CustomRender
                int allocationCountOfRenderPipeline = gcAllocRecorder.sampleBlockCount - gcAllocThreshold;

                if (allocationCountOfRenderPipeline > 0)
                    throw new Exception(
                        $@"Memory allocation test failed, {allocationCountOfRenderPipeline} allocations detected. Steps to find where your allocation is:
                        - Open the profiler window (ctrl-7) and enabled deep profiling.
                        - Run your the test that fails and wait (it can take much longer because deep profiling is enabled).
                        - In the CPU section of the profiler search for the 'GraphicTests_GC_Alloc_Check' marker.
                        - This should give you one result, click on it and press f to go to the frame where it hapended.
                        - Click on the GC Alloc column to sort by allocation and unfold the hierarchy under the 'GraphicTests_GC_Alloc_Check' marker."
                    );

                mainCamera.targetTexture = null;

                // save texture.
                Texture2D savingTexture = new Texture2D(rt.width, rt.height, TextureFormat.RGB24, false);
                RenderTexture.active = rt;
                savingTexture.ReadPixels(new Rect(0, 0, rt.width, rt.height), 0, 0);
                savingTexture.Apply();

                // Encode texture into PNG
                byte[] bytes = savingTexture.EncodeToPNG();
                UnityEngine.Object.DestroyImmediate(savingTexture);

                Scene scene = SceneManager.GetActiveScene();
                string testReferenceFilaneme = scene.name;


                //Write to a file in the project folder
                var path = CommonSettings.legacyPackagePath + testReferenceFilaneme + CommonSettings.filenameExtenstion;
                File.WriteAllBytes(path, bytes);
            }
            finally
            {
                RenderTexture.ReleaseTemporary(rt);
            }
        }
        private static void SetupGameViewSize()
        {

            int width = 960;
            int height = 540;
            string viewSizeName = string.Format("{0} * {1}", width, height);
            GameViewSizeHelper.AddCustomSize(GameViewSizeGroupType.Standalone, GameViewSizeHelper.GameViewSizeType.FixedResolution, width, height, viewSizeName);
            GameViewSizeHelper.ChangeGameViewSize(GameViewSizeGroupType.Standalone, GameViewSizeHelper.GameViewSizeType.FixedResolution, width, height, viewSizeName);
        }
/*
        [MenuItem("Edit/Unity Toon shader test/Capture Screen and Test Iamge")]
        private static void CreateTestImage()
        {
            Debug.Assert(false);
//            ScreenCapture.CaptureScreenshot(testReferenceFilaneme + filenameExtenstion);
        }
*/

  //      [MenuItem("Edit/Unity Toon shader test/Create Reference Iamge")]
        private static void CreateRenferenceImage()
        {
            Scene scene = SceneManager.GetActiveScene();
            string testReferenceFilaneme = scene.name;
            ScreenCapture.CaptureScreenshot(CommonSettings.legacyPackagePath + testReferenceFilaneme + CommonSettings.filenameExtenstion);
        }




    }

}

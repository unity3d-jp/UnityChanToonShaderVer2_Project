using System.Collections;
using System.Linq;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.XR;
using UnityEngine.TestTools.Graphics;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.IO;

namespace Tests
{
    public class LegacyGraphicsTests
    {
        // A Test behaves as an ordinary method
        [Test]
        public void LegacyGraphicsTestsSimplePasses()
        {
            // Use the Assert class to test conditions
        }

        // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
        // `yield return null;` to skip a frame.
        [UnityTest]
        public IEnumerator LegacyGraphicsTestsWithEnumeratorPasses()
        {
            string testReferenceFilaneme = "LightAndShadows";
            var path = CommonSettings.legacyPackagePath + testReferenceFilaneme + CommonSettings.filenameExtenstion;
            GraphicsTestCase testCase = new GraphicsTestCase(
                "Assets/Scenes/LightAndShadows/LightAndShadows.unity",
                LoadPNG(path)
                );
            Debug.Log("path:" + path);
            SceneManager.LoadScene(testCase.ScenePath);

            // Always wait one frame for scene load
            yield return null;

            var cameras = GameObject.FindGameObjectsWithTag("MainCamera").Select(x => x.GetComponent<Camera>());
            //            var settings = Object.FindObjectOfType<LegacyGraphicsTestSettings>();
            //            Assert.IsNotNull(settings, "Invalid test scene, couldn't find LegacyGraphicsTestSettings");

            Scene scene = SceneManager.GetActiveScene();

            if (scene.name.Substring(3, 4).Equals("_xr_"))
            {
#if ENABLE_VR && ENABLE_VR_MODULE
            Assume.That((Application.platform != RuntimePlatform.OSXEditor && Application.platform != RuntimePlatform.OSXPlayer), "Stereo Universal tests do not run on MacOSX.");

            XRSettings.LoadDeviceByName("MockHMD");
            yield return null;

            XRSettings.enabled = true;
            yield return null;

            XRSettings.gameViewRenderMode = GameViewRenderMode.BothEyes;
            yield return null;

            foreach (var camera in cameras)
                camera.stereoTargetEye = StereoTargetEyeMask.Both;
#else
                yield return null;
#endif
            }
            else
            {
#if ENABLE_VR && ENABLE_VR_MODULE
            XRSettings.enabled = false;
#endif
                yield return null;
            }
/*
            int waitFrames = settings.WaitFrames;

            if (settings.ImageComparisonSettings.UseBackBuffer && settings.WaitFrames < 1)
            {
                waitFrames = 1;
            }
*/
            int waitFrames = 1;
            for (int i = 0; i < waitFrames; i++)
                yield return new WaitForEndOfFrame();
            ImageComparisonSettings settings = new ImageComparisonSettings();
            settings.TargetWidth = 960;
            settings.TargetHeight = 540;
            settings.AverageCorrectnessThreshold = 0.01f;
            ImageAssert.AreEqual(testCase.ReferenceImage, cameras.Where(x => x != null), settings);

            // Does it allocate memory when it renders what's on the main camera?
            bool allocatesMemory = false;
            var mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();

            // 2D Renderer is currently allocating memory, skip it as it will always fail GC alloc tests.
            //var additionalCameraData = mainCamera.GetUniversalAdditionalCameraData();
            bool is2DRenderer = true; // additionalCameraData.scriptableRenderer is Renderer2D;

            if (!is2DRenderer)
            {
                try
                {
                    ImageAssert.AllocatesMemory(mainCamera, settings);
                }
                catch (AssertionException)
                {
                    allocatesMemory = true;
                }
                if (allocatesMemory)
                    Assert.Fail("Allocated memory when rendering what is on main camera");
            }
        }

        public static Texture2D LoadPNG(string filePath)
        {

            Texture2D tex2D = null;
            byte[] fileData;

            if (File.Exists(filePath))
            {
                fileData = File.ReadAllBytes(filePath);
                tex2D = new Texture2D(2, 2);
                tex2D.LoadImage(fileData);
            }
            return tex2D;
        }
    }


}

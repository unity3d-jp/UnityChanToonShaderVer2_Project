using System.Collections;
using System.Linq;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.XR;
using UnityEngine.TestTools.Graphics;
using UnityEditor.TestTools.Graphics;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.IO;

namespace Tests
{
    public class LegacyUTS_GraphicsTests
    {
        public const string ReferenceImagePath = "Assets/ReferenceImages";
        [UnityTest, Category("LegacyRP")]
        [PrebuildSetup("SetupGraphicsTestCases")]
        [UseGraphicsTestCases(ReferenceImagePath)]
        public IEnumerator Run(GraphicsTestCase testCase)
        {

            SceneManager.LoadScene(testCase.ScenePath);

            // Always wait one frame for scene load
            yield return null;

            var cameras = GameObject.FindGameObjectsWithTag("MainCamera").Select(x => x.GetComponent<Camera>());
            var settings = Object.FindObjectOfType<LegacyUTS_GraphicsTestSettings>();
            Assert.IsNotNull(settings, "Invalid test scene, couldn't find UTS_GraphicsTestSettings");

            Scene scene = SceneManager.GetActiveScene();


            if (scene.name.Length > (3+ 4) && scene.name.Substring(3, 4).Equals("_xr_"))
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

            int waitFrames = settings.WaitFrames;

            if (settings.ImageComparisonSettings.UseBackBuffer && settings.WaitFrames < 1)
            {
                waitFrames = 1;
            }


            for (int i = 0; i < waitFrames; i++)
                yield return new WaitForEndOfFrame();

            ImageAssert.AreEqual(testCase.ReferenceImage, cameras.Where(x => x != null), settings.ImageComparisonSettings);

            // Does it allocate memory when it renders what's on the main camera?
            bool allocatesMemory = false;
            var mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();

            if (settings == null || settings.CheckMemoryAllocation)
            {
                try
                {
                    ImageAssert.AllocatesMemory(mainCamera, settings.ImageComparisonSettings);
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

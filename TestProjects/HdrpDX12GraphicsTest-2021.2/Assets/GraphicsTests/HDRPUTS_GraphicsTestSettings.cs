using UnityEngine.TestTools.Graphics;

namespace Tests
{
    public class HDRPUTS_GraphicsTestSettings : GraphicsTestSettings
    {
        public int WaitFrames = 0;
        public bool XRCompatible = true;
#if UNITY_EDITOR_OSX || UNITY_STANDALONE_OSX
        public bool CheckMemoryAllocation = false;
#else
        public bool CheckMemoryAllocation = true;
#endif //#if UNITY_EDITOR_OSX || UNITY_STANDALONE_OSX

        public HDRPUTS_GraphicsTestSettings()
        {
            ImageComparisonSettings.TargetWidth = 960;
            ImageComparisonSettings.TargetHeight = 540;
            ImageComparisonSettings.AverageCorrectnessThreshold = 0.005f;
            ImageComparisonSettings.PerPixelCorrectnessThreshold = 0.001f;
            ImageComparisonSettings.UseHDR = false;
            ImageComparisonSettings.UseBackBuffer = false;
        }
    }
}
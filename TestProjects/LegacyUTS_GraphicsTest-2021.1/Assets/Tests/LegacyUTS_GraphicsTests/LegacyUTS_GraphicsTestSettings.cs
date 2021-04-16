using UnityEngine.TestTools.Graphics;

namespace Tests
{
    public class LegacyUTS_GraphicsTestSettings : GraphicsTestSettings
    {
        public int WaitFrames = 0;

        public LegacyUTS_GraphicsTestSettings()
        {
            ImageComparisonSettings.TargetWidth = 512;
            ImageComparisonSettings.TargetHeight = 512;
            ImageComparisonSettings.AverageCorrectnessThreshold = 0.005f;
            ImageComparisonSettings.PerPixelCorrectnessThreshold = 0.001f;
            ImageComparisonSettings.UseBackBuffer = false;
        }
    }
}
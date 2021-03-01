using UnityEngine.TestTools.Graphics;
namespace Unity.Toonshader.LegacyGraphicsTests
{
    public class LegacyGraphicsTestSettings : GraphicsTestSettings
    {
        public int WaitFrames = 0;

        public LegacyGraphicsTestSettings()
        {
            ImageComparisonSettings.TargetWidth = 512;
            ImageComparisonSettings.TargetHeight = 512;
            ImageComparisonSettings.AverageCorrectnessThreshold = 0.005f;
            ImageComparisonSettings.PerPixelCorrectnessThreshold = 0.001f;
            ImageComparisonSettings.UseBackBuffer = false;
        }
    }
}
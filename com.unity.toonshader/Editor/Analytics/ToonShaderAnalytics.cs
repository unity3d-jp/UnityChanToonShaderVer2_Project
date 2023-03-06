using Unity.FilmInternalUtilities;

namespace UnityEditor.Rendering.Toon
{
    internal partial class ToonShaderAnalytics
    {
        private const string k_EventNamePrefix = "toonshader_";

        [InitializeOnLoadMethod]
        private static void OnLoad()
        {
            AnalyticsSender.SendEventInEditor(new LoadEvent(
#if HDRP_IS_INSTALLED_FOR_UTS
                "high-definition"
#elif URP_IS_INSTALLED_FOR_UTS
                "universal"
#else
                "built-in"
#endif
            ));
        }
    }
}
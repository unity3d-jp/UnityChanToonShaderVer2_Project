using Unity.FilmInternalUtilities;

namespace UnityEditor.Rendering.Toon
{
    internal partial class ToonShaderAnalytics
    {
        internal class ConvertEvent : AnalyticsEvent<ConvertEvent.EventData>
        {
            internal override string eventName => k_EventNamePrefix + "convert";
            internal override int maxItems => 1;

            internal struct EventData
            {
                public string converter;
            }

            internal ConvertEvent(string converter) : base(new EventData { converter = converter })
            {
            }
        }
    }
}
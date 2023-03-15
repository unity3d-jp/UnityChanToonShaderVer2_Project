using Unity.FilmInternalUtilities;

namespace UnityEditor.Rendering.Toon
{
    internal partial class ToonShaderAnalytics
    {
        internal class ConvertEvent : AnalyticsEvent
        {
            internal override string eventName => k_EventNamePrefix + "convert";
            internal override int maxItems => 2;

            internal class EventData : AnalyticsEventData
            {
                public string converter;
            }

            internal ConvertEvent(string converter) : base(new EventData { converter = converter })
            {
            }
        }
    }
}
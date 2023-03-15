using System;
using Unity.FilmInternalUtilities;

namespace UnityEditor.Rendering.Toon
{
    internal partial class ToonShaderAnalytics
    {
        internal class LoadEvent : AnalyticsEvent
        {
            internal override string eventName => k_EventNamePrefix + "load";
            internal override int maxEventPerHour => 1;
            internal override int maxItems => 2;

            internal class EventData : AnalyticsEventData
            {
                public string renderPipeline;
            }

            internal LoadEvent(string renderPipeline) : base(new EventData { renderPipeline = renderPipeline })
            {
            }
        }
    }
}
using System;

namespace UnityEditor.Rendering.Toon
{
    internal partial class ToonShaderAnalytics
    {
        internal class LoadEvent : AnimeToolboxAnalytics.Event<LoadEvent.EventData>
        {
            internal override string eventName => k_EventNamePrefix + "load";
            internal override int maxEventPerHour => 1;
            internal override int maxItems => 1;

            internal struct EventData
            {
                public string renderPipeline;
            }

            internal LoadEvent(string renderPipeline) : base(new EventData { renderPipeline = renderPipeline })
            {
            }
        }
    }
}
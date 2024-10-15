using System;

namespace UnityEditor.Rendering.UTS2 {
internal class AnalyticsEventData {
    public string actualPackageVersion;
}
//--------------------------------------------------------------------------------------------------------------------------------------------------------------

internal class EventData : AnalyticsEventData {
    public string renderPipeline;
}

//--------------------------------------------------------------------------------------------------------------------------------------------------------------
internal abstract class AnalyticsEvent {
    
    internal abstract string eventName       { get; }
    internal virtual  int    version         => 1;
    internal virtual  int    maxEventPerHour => 10000;
    internal virtual  int    maxItems        => 1000;

    // Minimum interval to send this event
    internal virtual TimeSpan minInterval => TimeSpan.Zero;

    internal readonly AnalyticsEventData parameters;

    internal AnalyticsEvent() {
        parameters = new AnalyticsEventData();
    }

    internal AnalyticsEvent(AnalyticsEventData eventData) {
        parameters = eventData;
    }
}

} //end namespace
using UnityEditor;

namespace Unity.SharpZipLib.Editor {
internal static class SharpZipLibAnalytics {
    private class LoadEvent : AnalyticsEvent {
        internal override string eventName       => "sharpziplib_load";
        internal override int    maxEventPerHour => 1;
        internal override int    maxItems        => 2;
        internal LoadEvent() : base(new AnalyticsEventData()) {
        }
    }
    
//--------------------------------------------------------------------------------------------------------------------------------------------------------------    

    [InitializeOnLoadMethod]
    private static void OnLoad() {
        AnalyticsSender.SendEventInEditor(new LoadEvent());
    }
}

} //end namespace
using UnityEditor;

namespace UnityEditor.Rendering.UTS2 {
    internal static class SharpZipLibAnalytics {
        private class LoadEvent : AnalyticsEvent {
            internal override string eventName => "toonshader_load";
            internal override int maxEventPerHour => 1;
            internal override int maxItems => 2;

            internal LoadEvent() : base(new AnalyticsEventData()) {
            }

            internal LoadEvent(string renderPipeline) : base(new EventData { renderPipeline = renderPipeline }) {
            }
        }

//--------------------------------------------------------------------------------------------------------------------------------------------------------------    

        [InitializeOnLoadMethod]
        private static void OnLoad() {
            AnalyticsSender.SendEventInEditor(new LoadEvent("built-in_UTS2"));
        }
    }
} //end namespace
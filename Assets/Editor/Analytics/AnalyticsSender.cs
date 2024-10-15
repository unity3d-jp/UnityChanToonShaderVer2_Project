using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.Analytics;

#if UNITY_EDITOR
using UnityEditor;
#endif


namespace Unity.SharpZipLib.Editor {

internal static class AnalyticsSender {

#if UNITY_2019_4_OR_NEWER && UNITY_EDITOR && ENABLE_CLOUD_SERVICES_ANALYTICS
    private struct EventDetail {
        public string assemblyInfo;
        public string packageName;
        public string packageVersion;
    }
    
    internal static void SendEventInEditor(AnalyticsEvent analyticsEvent) {
        if (!EditorAnalytics.enabled) {
            return;
        }

        if (!IsEventRegistered(analyticsEvent)) {
            var assembly = Assembly.GetCallingAssembly();
            if (!RegisterEvent(analyticsEvent, assembly)) {
                return;
            }
        }

        if (!ShouldSendEvent(analyticsEvent)) {
            return;
        }

        analyticsEvent.parameters.actualPackageVersion = m_registeredEvents[analyticsEvent.eventName].packageVersion;
        AnalyticsResult result = EditorAnalytics.SendEventWithLimit(analyticsEvent.eventName, analyticsEvent.parameters, analyticsEvent.version);
        if (result != AnalyticsResult.Ok) {
            return;
        }

        DateTime now = DateTime.Now;
        m_lastSentDateTime[analyticsEvent.eventName] = now;
    }
    
//--------------------------------------------------------------------------------------------------------------------------------------------------------------
    private static bool IsEventRegistered(AnalyticsEvent analyticsEvent) {
        return m_registeredEvents.ContainsKey(analyticsEvent.eventName);
    }

    private static bool ShouldSendEvent(AnalyticsEvent analyticsEvent) {
        if (!m_lastSentDateTime.ContainsKey(analyticsEvent.eventName)) {
            return true;
        }

        DateTime lastSentDateTime = m_lastSentDateTime[analyticsEvent.eventName];
        return DateTime.Now - lastSentDateTime >= analyticsEvent.minInterval;
    }

    private static bool RegisterEvent(AnalyticsEvent analyticsEvent, Assembly assembly) {
        if (!EditorAnalytics.enabled) {
            return false;
        }

        AnalyticsResult result = EditorAnalytics.RegisterEventWithLimit(analyticsEvent.eventName,
            analyticsEvent.maxEventPerHour, analyticsEvent.maxItems, VENDOR_KEY, analyticsEvent.version);

        if (result != AnalyticsResult.Ok) {
            return false;
        }

        var eventDetails = new EventDetail {
            assemblyInfo = assembly.FullName,
        };
        
        var packageInfo = UnityEditor.PackageManager.PackageInfo.FindForAssembly(assembly);
        if (packageInfo != null) {
            eventDetails.packageName = packageInfo.name;
            eventDetails.packageVersion = packageInfo.version;
        }

        m_registeredEvents[analyticsEvent.eventName] = eventDetails;
        return true;
    }

//--------------------------------------------------------------------------------------------------------------------------------------------------------------    
    
    private const string VENDOR_KEY = "unity.sharp-zip-lib";

    private static readonly Dictionary<string, EventDetail> m_registeredEvents = new Dictionary<string, EventDetail>();
    private static readonly Dictionary<string, DateTime>    m_lastSentDateTime = new Dictionary<string, DateTime>();

#else

    internal static void SendEventInEditor(AnalyticsEvent analyticsEvent) { }


#endif //UNITY_EDITOR
    
}
} //end namespace
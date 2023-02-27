using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

namespace UnityEditor.Rendering.Toon
{
    internal partial class AnimeToolboxAnalytics
    {
        private const string k_VendorKey = "unity.anime-toolbox";

        private static HashSet<string> m_registeredEvents = new HashSet<string>();
        private static Dictionary<string, DateTime> m_lastSentDateTime = new Dictionary<string, DateTime>();

        private static bool IsEventRegistered<T>(Event<T> analyticsEvent)
        {
            return m_registeredEvents.Contains(analyticsEvent.eventName);
        }

        private static bool ShouldSendEvent<T>(Event<T> analyticsEvent)
        {
            if (!m_lastSentDateTime.ContainsKey(analyticsEvent.eventName))
            {
                return true;
            }

            var lastSentDateTime = m_lastSentDateTime[analyticsEvent.eventName];
            return DateTime.Now - lastSentDateTime >= analyticsEvent.minInterval;
        }

        private static bool RegisterEvent<T>(Event<T> analyticsEvent)
        {
            if (!EditorAnalytics.enabled)
            {
                return false;
            }

            var result = EditorAnalytics.RegisterEventWithLimit(analyticsEvent.eventName,
                analyticsEvent.maxEventPerHour, analyticsEvent.maxItems, k_VendorKey, analyticsEvent.version);

            if (result != AnalyticsResult.Ok)
            {
                return false;
            }

            m_registeredEvents.Add(analyticsEvent.eventName);
            return true;
        }

        internal static void SendEvent<T>(Event<T> analyticsEvent)
        {
            if (!EditorAnalytics.enabled)
            {
                return;
            }

            if (!IsEventRegistered(analyticsEvent))
            {
                if (!RegisterEvent(analyticsEvent))
                {
                    return;
                }
            }

            if (!ShouldSendEvent(analyticsEvent))
            {
                return;
            }

            var result = EditorAnalytics.SendEventWithLimit(analyticsEvent.eventName, analyticsEvent.parameters,
                analyticsEvent.version);

            if (result != AnalyticsResult.Ok)
            {
                return;
            }

            var now = DateTime.Now;
            m_lastSentDateTime[analyticsEvent.eventName] = now;
        }
    }
}
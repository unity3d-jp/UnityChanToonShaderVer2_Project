using System;

namespace UnityEditor.Rendering.Toon
{
    internal partial class AnimeToolboxAnalytics
    {
        internal abstract class Event<T>
        {
            internal abstract string eventName { get; }
            internal virtual int version => 1;
            internal virtual int maxEventPerHour => 10000;
            internal virtual int maxItems => 1000;

            // Minimum interval to send this event
            internal virtual TimeSpan minInterval => TimeSpan.Zero;

            internal T parameters;

            internal Event()
            {
            }

            internal Event(T eventData)
            {
                parameters = eventData;
            }
        }
    }
}
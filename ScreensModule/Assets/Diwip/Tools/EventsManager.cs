using System.Collections.Generic;

namespace Diwip.Tools.Events
{
    public abstract class BaseEvent
    {
    };

    public delegate void BaseEventHandler(BaseEvent eventObject);

    public static class EventsManager
    {
        static Dictionary<System.Type, List<BaseEventHandler>> events = new Dictionary<System.Type, List<BaseEventHandler>>();

        public static void AddListener(System.Type eventType, BaseEventHandler handler)
        {
            if (!events.ContainsKey(eventType))
            {
                events[eventType] = new List<BaseEventHandler>();
            }

            if (!events[eventType].Contains(handler))
            {
                events[eventType].Add(handler);
            }
        }

        public static void RemoveListener(System.Type eventType, BaseEventHandler handler)
        {
            if (events.ContainsKey(eventType))
            {
                if (events[eventType].Contains(handler))
                {
                    events[eventType].Remove(handler);
                }
            }
        }

        public static void ClearEventListeners(System.Type eventType)
        {
            if (events.ContainsKey(eventType))
            {
                events.Remove(eventType);
            }
        }

        internal static void Dispatch<T>(T eventInstance) where T : BaseEvent
        {
            if (events.ContainsKey(typeof(T)))
            {
                foreach (BaseEventHandler handler in events[typeof(T)])
                {
                    handler(eventInstance);
                }
            }
        }
    }
}


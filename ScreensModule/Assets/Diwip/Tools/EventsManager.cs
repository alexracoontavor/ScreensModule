using System.Collections.Generic;

namespace Diwip.Tools.Events
{
    public abstract class BaseEvent
    {
    };

    public delegate void BaseEventHandler(BaseEvent eventObject);

    public static class EventsManager
    {
        private static Dictionary<System.Type, List<BaseEventHandler>> _events = new Dictionary<System.Type, List<BaseEventHandler>>();

        public static void AddListener(System.Type eventType, BaseEventHandler handler)
        {
            if (!_events.ContainsKey(eventType))
            {
                _events[eventType] = new List<BaseEventHandler>();
            }

            if (!_events[eventType].Contains(handler))
            {
                _events[eventType].Add(handler);
            }
        }

        public static void RemoveListener(System.Type eventType, BaseEventHandler handler)
        {
            if (_events.ContainsKey(eventType))
            {
                if (_events[eventType].Contains(handler))
                {
                    _events[eventType].Remove(handler);
                }
            }
        }

        public static void ClearEventListeners(System.Type eventType)
        {
            if (_events.ContainsKey(eventType))
            {
                _events.Remove(eventType);
            }
        }

        internal static void Dispatch<T>(T eventInstance) where T : BaseEvent
        {
            if (_events.ContainsKey(typeof(T)))
            {
                foreach (BaseEventHandler handler in _events[typeof(T)])
                {
                    handler(eventInstance);
                }
            }
        }
    }
}


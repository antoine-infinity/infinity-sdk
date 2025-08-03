using System;
using System.Collections.Generic;

namespace Infinity.Runtime.Core.Events
{
    public static class InfinityEventBus
    {
        private static readonly Dictionary<Type, List<Delegate>> Handlers = new();

        public static void Publish<T>(T @event) where T : class
        {
            var eventType = typeof(T);
            if (!Handlers.TryGetValue(eventType, out var handlers))
                return;

            foreach (var handler in handlers)
            {
                ((Action<T>)handler)(@event);
            }
        }

        public static void Subscribe<T>(Action<T> handler) where T : class
        {
            var eventType = typeof(T);
            if (!Handlers.ContainsKey(eventType))
            {
                Handlers.Add(eventType, new List<Delegate>());
            }

            Handlers[eventType].Add(handler);
        }
    }
}
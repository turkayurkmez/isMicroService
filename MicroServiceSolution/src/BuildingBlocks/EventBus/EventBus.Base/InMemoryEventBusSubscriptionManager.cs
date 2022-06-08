using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBus.Base
{
    public class InMemoryEventBusSubscriptionManager : IEventBusSubscriptionManager
    {

        private readonly Dictionary<string, List<SubscriptionInfo>> _handlers;
        private readonly List<Type> _eventTypes;
        private Func<string, string> eventNameGetter;

        public InMemoryEventBusSubscriptionManager(Func<string,string> eventNameGetter)
        {
            _handlers = new Dictionary<string, List<SubscriptionInfo>>();
            _eventTypes = new List<Type>();
            this.eventNameGetter = eventNameGetter;
        }

        public bool IsEmpty => _handlers.Count == 0;

        public event EventHandler<string> OnEventRemoved;

        public void AddSubscription<T, TH>()
            where T : IntegrationEvent
            where TH : IIntegrationEventHandler<T>
        {
            var eventName = GetEventKey<T>();
            addDynamicSubscription<TH>(eventName);
            if (!_eventTypes.Contains(typeof(T)))
            {
                _eventTypes.Add(typeof(T));
            }
            _handlers[eventName].Add(SubscriptionInfo.Typed(typeof(TH)));

        }

        private void addDynamicSubscription<TH>(string eventName) where TH : IIntegrationEventHandler
        {
            if (!HasSubscriptionsForEvent(eventName))
            {
                _handlers.Add(eventName, new List<SubscriptionInfo>());
            }

            if (_handlers[eventName].Any(x=>x.HandlerType == typeof(TH)))
            {
                throw new ArgumentNullException($"{typeof(TH).Name} isimli tip zaten {eventName} event'ına kayıtlı.");
            }

            _handlers[eventName].Add(SubscriptionInfo.Typed(typeof(TH)));
        }

        public void Clear() => _handlers.Clear();


        public string GetEventKey<T>()
        {
            return eventNameGetter(typeof(T).Name);
        }

        public IEnumerable<SubscriptionInfo> GetHandlersForEvent<T>() where T : IntegrationEvent
        {
            var key = GetEventKey<T>();
            return GetHandlersForEvent(key);
        }

        public IEnumerable<SubscriptionInfo> GetHandlersForEvent(string eventName)
        {
            return _handlers[eventName];
        }

        public bool HasSubscriptionsForEvent<T>() where T : IntegrationEvent
        {
            var key = GetEventKey<T>();
            return HasSubscriptionsForEvent(key);

        }

        public bool HasSubscriptionsForEvent(string eventName) => _handlers.ContainsKey(eventName);


        public void RemoveSubscription<T, TH>()
            where T : IntegrationEvent
            where TH : IIntegrationEventHandler<T>
        {
            var handlerToRemove = findSubscriptionToRemove<T, TH>();
            var eventName = GetEventKey<T>();
            removeHandler(handlerToRemove, eventName);
        }

      

        private SubscriptionInfo findSubscriptionToRemove<T, TH>()
            where T : IntegrationEvent
            where TH : IIntegrationEventHandler<T>
        {
            return _handlers[GetEventKey<T>()].SingleOrDefault(s => s.HandlerType == typeof(TH));
            
        }


        private void removeHandler(SubscriptionInfo handlerToRemove, string eventName)
        {
            if (handlerToRemove == null)
            {
                throw new ArgumentNullException("handlerToRemove");
            }
            _handlers[eventName].Remove(handlerToRemove);
            if (!_handlers[eventName].Any())
            {
                _handlers.Remove(eventName);
                var eventType = _eventTypes.SingleOrDefault(e => e.Name == eventName);
                if (eventType != null)
                {
                    _eventTypes.Remove(eventType);
                }
                raiseOnEventRemoved(eventName);

            }
         
           
        }

        private void raiseOnEventRemoved(string eventName)
        {
            var handler = OnEventRemoved;
            handler?.Invoke(this, eventName);
        }

        public Type GetEventByTypeName(string eventName) => _eventTypes.SingleOrDefault(t => t.Name == eventName); 
      
    }
}

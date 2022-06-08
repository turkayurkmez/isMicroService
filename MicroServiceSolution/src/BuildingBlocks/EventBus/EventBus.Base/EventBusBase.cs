
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace EventBus.Base
{
    public abstract class EventBusBase : IEventBus, IDisposable
    {

        protected readonly IEventBusSubscriptionManager _subsManager;
        private readonly IServiceProvider _serviceProvider;
        public EventBusConfig EventBusConfig { get; set; }

        public EventBusBase(EventBusConfig eventBusConfig, IServiceProvider serviceProvider)
        {
            EventBusConfig = eventBusConfig;
            _subsManager = new InMemoryEventBusSubscriptionManager(processEventName);
            _serviceProvider = serviceProvider;
            

        }

        protected string processEventName(string eventName)
        {
            //OrderCreatedIntegrationEvent
            if (EventBusConfig.IsSuffixDeleted)
            {
                eventName = eventName.TrimEnd(EventBusConfig.EventNameSuffix.ToArray());
            }
            //SampleOrderCrated
            if (EventBusConfig.IsPrefixDeleted)
            {
                eventName = eventName.TrimStart(EventBusConfig.EventNamePrefix.ToArray());
            }

            return eventName;
        }



        public string GetSubName(string eventName)
        {
            return $"{EventBusConfig.SubscriberClientName}.{processEventName(eventName)}";
        }

        public async Task<bool> ProcessEvent(string eventName, string message)
        {
            //event'e bind edilen handler'ın handle metodunu çağırıyoruz.
            eventName = processEventName(eventName);
            var handled = false;
            if (_subsManager.HasSubscriptionsForEvent(eventName))
            {
                var subscriptions = _subsManager.GetHandlersForEvent(eventName);
                using (var scope = _serviceProvider.CreateScope())
                {

                    foreach (var subscription in subscriptions)
                    {

                        var eventType = _subsManager.GetEventByTypeName($"{EventBusConfig.EventNamePrefix}{eventName}{EventBusConfig.EventNameSuffix}");
                        var integrationEvent = JsonConvert.DeserializeObject(message, eventType);
                        var handler = _serviceProvider.GetService(subscription.HandlerType);
                        var concreteType = typeof(IIntegrationEventHandler<>).MakeGenericType(eventType);
                        var method = concreteType.GetMethod("Handle");
                        await (Task)method.Invoke(handler, new object[] { integrationEvent });
                      
                    } 
                }
                handled = true;
            }

            return handled;

        }


        public abstract void Publish(IntegrationEvent @event);


        public abstract void Subscribe<TEvent, TEventHandler>()
            where TEvent : IntegrationEvent
            where TEventHandler : IIntegrationEventHandler<TEvent>;


        public abstract void Unsubscribe<TEvent, TEventHandler>()
            where TEvent : IntegrationEvent
            where TEventHandler : IIntegrationEventHandler<TEvent>;
       


        public virtual void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
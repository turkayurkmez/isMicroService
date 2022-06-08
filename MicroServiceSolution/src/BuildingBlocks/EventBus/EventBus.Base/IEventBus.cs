using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBus.Base
{
    public interface IEventBus
    {
        void Publish(IntegrationEvent @event);

        void Subscribe<TEvent, TEventHandler>() 
            where TEvent : IntegrationEvent 
            where TEventHandler : IIntegrationEventHandler<TEvent>;

        void Unsubscribe<TEvent, TEventHandler>()
            where TEvent : IntegrationEvent
            where TEventHandler : IIntegrationEventHandler<TEvent>;

        //ister RabbitMQ ister başka bir message broker ile çalışın...
    }
}

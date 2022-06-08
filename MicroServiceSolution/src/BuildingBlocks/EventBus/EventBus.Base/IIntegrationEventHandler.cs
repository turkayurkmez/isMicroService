using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBus.Base
{
    public interface IIntegrationEventHandler
    {
    }

    public interface IIntegrationEventHandler<TEvent> : IIntegrationEventHandler where TEvent : IntegrationEvent {

        Task Handle(TEvent @event);
    }
}

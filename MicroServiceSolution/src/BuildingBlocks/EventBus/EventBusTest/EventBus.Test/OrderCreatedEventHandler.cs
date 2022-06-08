using EventBus.Base;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBus.Test
{
    public class OrderCreatedEventHandler : IIntegrationEventHandler<OrderCreatedIntegrationEvent>
    {
        public Task Handle(OrderCreatedIntegrationEvent @event)
        {
            Debug.WriteLine("OrderCreatedEventHandler");
            Debug.WriteLine($"OrderId: {@event.OrderId}, OrderDate: {@event.CreationDate.ToString()}");
            return Task.CompletedTask;
        }
    }
}

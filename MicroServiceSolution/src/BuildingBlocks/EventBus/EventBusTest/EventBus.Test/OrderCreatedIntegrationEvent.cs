using EventBus.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBus.Test
{
    //deneme event'i hazırladık:
    public class OrderCreatedIntegrationEvent : IntegrationEvent
    {
        public int OrderId { get; set; }
        public string UserId { get; set; }
        public List<OrderItem> OrderItems { get; set; }
        public OrderCreatedIntegrationEvent(int orderId, string userId, List<OrderItem> orderItems)
        {
            OrderId = orderId;
            UserId = userId;
            OrderItems = orderItems;
        }

    }
}

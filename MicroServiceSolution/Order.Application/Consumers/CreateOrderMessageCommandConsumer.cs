using MassTransit;
using Order.DataInftrastructure;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Application.Consumers
{
    public class CreateOrderMessageCommandConsumer : IConsumer<CreateOrderMessageCommand>
    {

        private readonly OrderDbContext orderDbContext;
        public CreateOrderMessageCommandConsumer(OrderDbContext orderDbContext)
        {
            this.orderDbContext = orderDbContext;
        }
     

        //public async Task Consume(CreateOrderMessageCommand message)
        //{
        //    var order = await _orderRepository.GetById(message.OrderId);
        //    order.AddMessage(message.Message);
        //    await _orderRepository.Save(order);
        //}

        public async Task Consume(ConsumeContext<CreateOrderMessageCommand> context)
        {
            var address = new Domain.OrderAggregate.Address(context.Message.Street, context.Message.City, context.Message.State, context.Message.Country, context.Message.ZipCode);
            var order = new Domain.OrderAggregate.Order(context.Message.CustomerId,address);

            context.Message.OrderItems.ForEach(x => order.AddOrderItem(x.ProductId,x.ProductName,x.UnitPrice, 1));
            await orderDbContext.Orders.AddAsync(order);
            await orderDbContext.SaveChangesAsync();

        }
    }

}
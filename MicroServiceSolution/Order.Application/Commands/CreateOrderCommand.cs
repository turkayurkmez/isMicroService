using MediatR;
using Order.Application.Responses;
using Order.Domain.OrderAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Application.Commands
{
    public class CreateOrderCommand : IRequest<CreateOrderResponse>
    {
        public string CustomerId { get; set; }
        public List<OrderItem> OrderItems { get; set; }
        public Address Address { get; set; }


    }
}

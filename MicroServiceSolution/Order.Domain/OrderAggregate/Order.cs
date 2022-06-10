using Order.Domain.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Domain.OrderAggregate
{
    public class Order : Entity, IAggregateRoot
    {
        public DateTime OrderDate { get; set; }
        public string UserId { get; set; }       
        public Address Address { get; set; }
        private readonly List<OrderItem> _orderItems;
        public IReadOnlyCollection<OrderItem> OrderItems => _orderItems;

        public Order()
        {
            _orderItems = new List<OrderItem>();
        }

        public Order(string userId, Address address)
        {
            UserId = userId;
            Address = address;
            _orderItems = new List<OrderItem>();
            OrderDate = DateTime.Now;
        }

        public void AddOrderItem(int productId, string productName, decimal unitPrice, int quantity)
        {
            if (_orderItems.Any(o => o.ProductId == productId))
            {
                throw new Exception($"Product {productId} already exists in order!");
            }

            _orderItems.Add(new OrderItem(productId, productName, unitPrice));
        }

        public decimal GetTotalPrice => _orderItems.Sum(o => o.UnitPrice);



    }
}

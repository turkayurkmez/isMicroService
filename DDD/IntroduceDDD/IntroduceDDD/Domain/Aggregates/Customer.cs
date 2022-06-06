using IntroduceDDD.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroduceDDD.Domain.Aggregates
{
    public class CustomerAggregate
    {
        public int Id { get; set; }
        public string CustomerName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public ICollection<Order> Orders { get; set; }

        public void CreateOrder(Product product, int quantity)
        {
            var order = new Order();
            order.OrderDate = DateTime.Now;
            order.CustomerId = this.Id;
            order.CustomerName = this.CustomerName;
            order.ShipAddress = this.Address;
            order.ShipCity = this.City;
            order.ShipCountry = this.Country;
          //  order.OrderDetails.Add(new OrderDetail() { ProductId = product.Id, ProductName = product.ProductName, UnitPrice = product.UnitPrice, Quantity = quantity });
            this.Orders.Add(order);
        }

    }
}

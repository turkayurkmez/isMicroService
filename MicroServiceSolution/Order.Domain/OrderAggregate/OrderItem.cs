using Order.Domain.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Domain.OrderAggregate
{
    public class OrderItem : Entity
    {
        public OrderItem()
        {
          
        }

        public OrderItem(int productId, string productName, decimal unitPrice)
        {
            ProductId = productId;
            ProductName = productName;
            UnitPrice = unitPrice;
          
        }     

        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }            
    
        public decimal UnitPrice { get; set; }

        public void Update(int productId, string productName, decimal unitPrice)
        {
            ProductId = productId;
            ProductName = productName;
            UnitPrice = unitPrice;
        }
    }
}

namespace Shared
{
    public class CreateOrderMessageCommand
    {
        public string OrderId { get; set; }
        public string CustomerId { get; set; }
        public List<OrderItem> OrderItems { get; set; }
        public int Quantity { get; set; }

        public string Street { get;  set; }
        public string City { get;  set; }
        public string State { get;  set; }
        public string Country { get;  set; }
        public string ZipCode { get;  set; }


    }
    
    public class OrderItem
    {       
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal UnitPrice { get; set; }
    }
}
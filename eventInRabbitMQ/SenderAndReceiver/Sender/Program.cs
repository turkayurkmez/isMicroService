// See https://aka.ms/new-console-template for more information
using Sender;

Console.WriteLine("Hello, World!");
MessageBusClient client = new MessageBusClient();

client.Send(new ProductDto{   
  
    Name = "Product X",
    Price = 10.0M
});

Console.WriteLine("Mesaj gönderildi");
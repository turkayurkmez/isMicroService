using MassTransit;
using Microsoft.AspNetCore.Mvc;
using miniShop.Client.Models;
using miniShop.Client.Services;

using Shared;
using System.Diagnostics;

namespace miniShop.Client.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly CatalogService catalogService;
        private readonly ISendEndpointProvider sender;

        public HomeController(ILogger<HomeController> logger, CatalogService catalogService, ISendEndpointProvider sender)
        {
            _logger = logger;
            this.catalogService = catalogService;
            this.sender = sender;
        }
       

        public IActionResult Index()
        {
            var products = catalogService.GetProducts();
            return View(products);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost]
        public async Task< IActionResult> CreateOrder(int id, string name, decimal price)
        {
            var createdOrderMessageCommand = new CreateOrderMessageCommand()
            {
                City = "Eskişehir",
                Country = "Turkey",
                State = "Eskişehir",
                Street = "Çiçekler Mahallesi",
                ZipCode = "34000",
                OrderItems = new List<OrderItem>
                {
                    new OrderItem()
                    {
                        ProductId = id,
                        ProductName = name,
                        UnitPrice = price

                    }
                },
                CustomerId = "busekoca",
                Quantity = 1



            };

            var sendEndPoint = await sender.GetSendEndpoint(new Uri("queue:create-order"));
            await sendEndPoint.Send<CreateOrderMessageCommand>(createdOrderMessageCommand);

            //var order = new Order()
            //{
            //    ProductId = id,
            //    ProductName = name,
            //    Price = price
            //};
            
            return View();
        }

    }
}
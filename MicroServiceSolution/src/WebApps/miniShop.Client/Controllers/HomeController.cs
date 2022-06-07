using Microsoft.AspNetCore.Mvc;
using miniShop.Client.Models;
using miniShop.Client.Services;
using System.Diagnostics;

namespace miniShop.Client.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly CatalogService catalogService;

        public HomeController(ILogger<HomeController> logger, CatalogService catalogService)
        {
            _logger = logger;
            this.catalogService = catalogService;
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
    }
}
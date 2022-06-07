using Catalog.API.DataTransferObjects;
using Catalog.API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CatalogController : ControllerBase
    {
        private readonly IProductService productService;

        public CatalogController(IProductService productService)
        {
            this.productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            var products = await productService.GetProductsAsync();
            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            var product = await productService.GetProductByIdAsync(id);
            return Ok(product);
        }

        [HttpGet("category/{id}")]
        public async Task<IActionResult> GetProductsByCategory(int id)
        {
            var products = await productService.GetProductsByCategoryAsync(id);
            return Ok(products);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct(AddProductRequest product)
        {
            if (ModelState.IsValid)
            {
                var response = await productService.CreateProductAsync(product);
                return CreatedAtAction(nameof(GetProductById), new { id = response.Id }, product);
            }

            return BadRequest(ModelState);

        }
    }
}

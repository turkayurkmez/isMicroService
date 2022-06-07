using AutoMapper;
using Catalog.API.Data.Repositories;
using Catalog.API.DataTransferObjects;
using Catalog.API.Models;

namespace Catalog.API.Services
{
    public class ProductService : IProductService
    {
        private IProductRepository productRepository;
        private IMapper mapper;

        public ProductService(IProductRepository productRepository, IMapper mapper)
        {
            this.productRepository = productRepository;
            this.mapper = mapper;
        }

        public async Task<ProductResponse> CreateProductAsync(AddProductRequest product)
        {

            var productEntity = mapper.Map<Product>(product);
            await productRepository.Add(productEntity);

            // TODO 1: AutoMapper ile mapping yapılacak.
            var productResponse = mapper.Map<ProductResponse>(productEntity);
            return productResponse;



        }

        public Task<ProductResponse> DeleteProductAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ProductResponse> GetProductByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<ProductResponse>> GetProductsAsync()
        {
            var products = await productRepository.GetAll();
            var result = mapper.Map<List<ProductResponse>>(products);
            return result;
           
        }

        public Task<IEnumerable<ProductResponse>> GetProductsByCategoryAsync(int categoryId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ProductResponse>> GetProductsByNameAsync(string name)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ProductResponse>> GetProductsByPriceAsync(decimal price)
        {
            throw new NotImplementedException();
        }

        public Task<ProductResponse> UpdateProductAsync(UpdateProductRequest product)
        {
            throw new NotImplementedException();
        }
    }
}

using Catalog.API.Data.Repositories;
using Catalog.API.DataTransferObjects;

namespace Catalog.API.Services
{
    public class ProductService : IProductService
    {
        private IProductRepository productRepository;

        public ProductService(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }

        public async Task<ProductResponse> CreateProductAsync(AddProductRequest product)
        {

            await productRepository.Add(product);


        }

        public Task<ProductResponse> DeleteProductAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ProductResponse> GetProductByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ProductResponse>> GetProductsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ProductResponse>> GetProductsByCategoryAsync(string category)
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

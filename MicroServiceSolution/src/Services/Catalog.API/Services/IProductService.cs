using Catalog.API.DataTransferObjects;

namespace Catalog.API.Services
{
    public interface IProductService
    {
        Task<IEnumerable<ProductResponse>> GetProductsAsync();
        Task<IEnumerable<ProductResponse>> GetProductsByNameAsync(string name);
        Task<IEnumerable<ProductResponse>> GetProductsByCategoryAsync(int categoryId);
      
        Task<ProductResponse> GetProductByIdAsync(int id);
        Task<ProductResponse> CreateProductAsync(AddProductRequest product);
        Task<ProductResponse> UpdateProductAsync(UpdateProductRequest product);
        Task<ProductResponse> DeleteProductAsync(int id);
    }
}

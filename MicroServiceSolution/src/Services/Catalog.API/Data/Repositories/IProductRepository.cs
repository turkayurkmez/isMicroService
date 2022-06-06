using Catalog.API.Models;

namespace Catalog.API.Data.Repositories
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<ICollection<Product>> GetProductsByCategory(int categoryId);
        Task<ICollection<Product>> SearchProductsByName(string name);


    }
}

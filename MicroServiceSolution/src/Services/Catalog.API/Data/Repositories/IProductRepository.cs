using Catalog.API.Models;

namespace Catalog.API.Data.Repositories
{
    public interface IProductRepository: IRepository<Product>
    {
        ICollection<Product> GetProductsByCategory(int categoryId);
        ICollection<Product> SearchProductsByName(string name);
        

    }
}

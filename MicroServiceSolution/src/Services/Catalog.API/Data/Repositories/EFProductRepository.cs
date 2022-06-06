using Catalog.API.Models;

namespace Catalog.API.Data.Repositories
{
    public class EFProductRepository : IProductRepository
    {
        public void Add(Product entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(Product entity)
        {
            throw new NotImplementedException();
        }

        public Product Get(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Product> GetAll()
        {
            throw new NotImplementedException();
        }

        public ICollection<Product> GetProductsByCategory(int categoryId)
        {
            throw new NotImplementedException();
        }

        public ICollection<Product> SearchProductsByName(string name)
        {
            throw new NotImplementedException();
        }

        public void Update(Product entity)
        {
            throw new NotImplementedException();
        }
    }
}

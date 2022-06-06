using Catalog.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Catalog.API.Data.Repositories
{
    public class EFProductRepository : IProductRepository
    {
        private readonly CatalogDbContext context;
        public async Task Add(Product entity)
        {
            context.Products.Add(entity);
            await context.SaveChangesAsync();
        }

        public async Task Delete(Product entity)
        {
            context.Products.Remove(entity);
            await context.SaveChangesAsync();

        }
         
        public async Task< Product> Get(int id)
        {
            return await context.Products.FindAsync(id);
        }

        public async Task<IEnumerable<Product>> GetAll()
        {
            return await  context.Products.ToListAsync();            
        }

        public async Task< ICollection<Product>> GetProductsByCategory(int categoryId)
        {
            return await context.Products.Where(p => p.CategoryId == categoryId).ToListAsync();
        }

        public async Task<ICollection<Product>> SearchProductsByName(string name)
        {
            return await context.Products.Where(p => p.Name.ToLower().Contains(name.ToLower())).ToListAsync();
        }

        public async Task Update(Product entity)
        {
            context.Products.Update(entity);
            await context.SaveChangesAsync();

        }
    }
}

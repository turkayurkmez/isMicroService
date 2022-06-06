using Catalog.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Catalog.API.Data
{
    public class CatalogDbContext : DbContext
    {
        public CatalogDbContext(DbContextOptions<CatalogDbContext> options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
      

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Category>().HasData(new Category  {  Id = 1, Name = "Giyim"},
                                               new Category { Id=2, Name="Elektronik"} 
                );

            var products = new List<Product>
            {
                new Product{ Id=1, Name="Çamaşır Makinesi", Price=100, CategoryId=2, ImageUrl="https://picsum.photos/seed/picsum/200/300"},
                new Product{ Id=2, Name="Tişört", Price=100, CategoryId=1, ImageUrl="https://picsum.photos/seed/picsum/200/300"},
                new Product{ Id=3, Name="Şapka", Price=100, CategoryId=2, ImageUrl="https://picsum.photos/seed/picsum/200/300"},
                


            };

        }

        
    }
   
}

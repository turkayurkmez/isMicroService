namespace Catalog.API.Models
{
    public class Product : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public int? CategoryId { get; set; }
        public virtual Category Category { get; set; }
        public string? ImageUrl { get; set; }
    }
}

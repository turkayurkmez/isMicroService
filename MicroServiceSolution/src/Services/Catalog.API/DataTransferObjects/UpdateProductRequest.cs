namespace Catalog.API.DataTransferObjects
{
    public class UpdateProductRequest
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public int? CategoryId { get; set; }
        public string? ImageUrl { get; set; }
        public decimal Price { get; set; }
        public int StockCount { get; set; }
    }
}

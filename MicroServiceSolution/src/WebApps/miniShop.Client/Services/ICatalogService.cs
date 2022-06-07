using miniShop.Client.Dtos;

namespace miniShop.Client.Services
{
    public interface ICatalogService
    {
        IEnumerable<ProductResponse> GetProducts();
    }
}
using miniShop.Client.Dtos;
using miniShop.Client.Models;
using Newtonsoft.Json;

namespace miniShop.Client.Services
{
    public class CatalogService : ICatalogService
    {
        private readonly HttpClient _httpClient;
        private readonly string _remoteServiceBaseUrl;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CatalogService(HttpClient httpClient, IHttpContextAccessor httpContextAccessor)
        {
            _httpClient = httpClient;
            _remoteServiceBaseUrl = "http://localhost:5004/api/Catalog";
            _httpContextAccessor = httpContextAccessor;
        }

        public IEnumerable<ProductResponse> GetProducts()
        {
            var dataString = _httpClient.GetStringAsync("http://localhost:5078/catalog").GetAwaiter().GetResult();

            return JsonConvert.DeserializeObject<IEnumerable<ProductResponse>>(dataString);
        }

      
    }
}
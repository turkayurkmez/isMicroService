using miniShop.Client.Dtos;
using miniShop.Client.Models;
using Newtonsoft.Json;

namespace miniShop.Client.Services
{
    public class CatalogService
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

        public async Task<IEnumerable<ProductResponse>> GetProducts()
        {
            var dataString = await _httpClient.GetAsync(_remoteServiceBaseUrl);
       
            if (dataString.IsSuccessStatusCode)
            {
              var response =   dataString.Content.ReadFromJsonAsync<object>();
            }
            //return response;
            return null;
        }

        //public async Task<ProductViewModel> GetProductById(int id)
        //{
        //    var dataString = await _httpClient.GetStringAsync(_remoteServiceBaseUrl + id);
        //    var response = JsonConvert.DeserializeObject<Product>(dataString);
        //    return response;
        //}

        //public async Task<IEnumerable<ProductViewModel>> GetProductsByCategory(string categoryName)
        //{
        //    var dataString = await _httpClient.GetStringAsync(_remoteServiceBaseUrl + categoryName);
        //    var response = JsonConvert.DeserializeObject<Catalog>(dataString);
        //    return response.Data.Products;
        //}

        //public async Task<IEnumerable<ProductViewModel>> GetProductsByPrice(decimal from, decimal to)
        //{
        //    var dataString = await _httpClient.GetStringAsync(_remoteServiceBaseUrl + from + "/" + to);
        //    var response = JsonConvert.DeserializeObject<Catalog>(dataString);
        //    return response.Data.Products;
        //}

        //public async Task<IEnumerable<ProductViewModel>> GetProductsByName(string name)
        //{
        //    var dataString = await _httpClient.GetStringAsync(_remoteServiceBaseUrl + name);
        //    var response = JsonConvert.DeserializeObject<Catalog>(dataString);
        //}
    }
}
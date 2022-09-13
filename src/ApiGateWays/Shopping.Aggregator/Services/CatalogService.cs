using Shopping.Aggregator.Extentions;
using Shopping.Aggregator.Models;

namespace Shopping.Aggregator.Services
{
    public class CatalogService : ICatalogService
    {
        private readonly HttpClient _httpClient;

        public CatalogService(HttpClient client)
        {
            _httpClient = client ?? throw new ArgumentNullException(nameof(client));
        }
        public async Task<IEnumerable<CatalogModel>> GetCatalog()
        {
            var response = await _httpClient.GetAsync("/api/v1.0/catalog");
            return await response.ReadContentAs<List<CatalogModel>>();
        }

        public async Task<IEnumerable<CatalogModel>> GetCatalogByCategory(string category)
        {
            var response = await _httpClient.GetAsync($"/api/v1.0/catalog/GetProductByCategory/{category}");
            return await response.ReadContentAs<List<CatalogModel>>();
        }

        public async Task<CatalogModel> GetCatalog(string id)
        {
            var response = await _httpClient.GetAsync($"/api/v1.0/catalog/{id}");
            return await response.ReadContentAs<CatalogModel>();
        }
    }
}

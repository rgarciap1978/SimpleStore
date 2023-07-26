using SimpleStore.Shared.Request;
using SimpleStore.Shared.Response;
using System.Net.Http.Json;

namespace SimpleStore.Client.Services
{
    public class ProxyProduct
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<ProxyProduct> _logger;

        public ProxyProduct(HttpClient httpClient, ILogger<ProxyProduct> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<ResponsePagination<ResponseDTOProduct>> ListAsync(int id)
        {
            var response = await _httpClient.GetFromJsonAsync<ResponsePagination<ResponseDTOProduct>>($"api/Product/Category/{id}");
            if (response!.Success) return response;

            _logger.LogError($"Error getting Products: {response.Message}");
            return new ResponsePagination<ResponseDTOProduct>();
        }

        public async Task<ResponsePagination<ResponseDTOProduct>> ListAsync(string? filter, int page, int rows)
        {
            var response = await _httpClient.GetFromJsonAsync<ResponsePagination<ResponseDTOProduct>>($"api/Product?filter={filter ?? string.Empty}&page={page}&rowa={rows}");
            if (response!.Success) return response;

            _logger.LogError($"Error getting Products: {response.Message}");
            return new ResponsePagination<ResponseDTOProduct>();
        }

        public async Task<ResponseDTOProduct> GetByIdAsync(int id)
        {
            var response = await _httpClient.GetFromJsonAsync<ResponseGeneric<ResponseDTOProduct>>($"api/Product/{id}");
            if (response!.Success) return response.Data!;

            _logger.LogError($"Error getting Product: {response.Message}");
            return new ResponseDTOProduct();
        }

        public async Task CreateAsync(RequestDTOProduct request)
        {
            var response = await _httpClient.PostAsJsonAsync($"api/Product", request);
            if (response.IsSuccessStatusCode) return;

            _logger.LogError($"Error creating Product: {response.ReasonPhrase}");
        }

        public async Task UpdateAsync(int id, RequestDTOProduct request)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/Product/{id}", request);
            if (response.IsSuccessStatusCode) return;

            _logger.LogError($"Error updating Product: {response.ReasonPhrase}");
        }

        public async Task DeleteAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/Product/{id}");
            if(response.IsSuccessStatusCode) return;

            _logger.LogError($"Error deleting Product: {response.ReasonPhrase}");
        }
    }
}

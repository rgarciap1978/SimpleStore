using SimpleStore.Shared.Request;
using SimpleStore.Shared.Response;
using System.Net.Http.Json;

namespace SimpleStore.Client.Services
{
    public class ProxySale
    {
        private readonly HttpClient _httpClient;

        public ProxySale(HttpClient httpClient) => _httpClient = httpClient;

        public async Task<int> CreateSale(RequestDTOSale request)
        {
            var response = await _httpClient.PostAsJsonAsync("api/Sale/", request);
            var jsonResponse = await response.Content.ReadFromJsonAsync<ResponseGeneric<int>>();
            if (jsonResponse!.Success) return jsonResponse.Data;

            throw new InvalidOperationException(jsonResponse.Message);
        }

        public async Task<ResponseDTOSale> GetById(int id)
        {
            var response = await _httpClient.GetFromJsonAsync<ResponseGeneric<ResponseDTOSale>>($"api/Sale/{id}");
            if (response!.Success) return response.Data!;

            throw new InvalidOperationException(response.Message);
        }

        public async Task<ResponsePagination<ResponseDTOSale>> ListSalesByUser(string? filter, int page, int rows)
        {
            var response = await _httpClient.GetFromJsonAsync<ResponsePagination<ResponseDTOSale>>($"api/Sale/ListSales?filter={filter}&page={page}&rows={rows}");
            if(!response!.Success) throw new InvalidOperationException(response.Message);

            return response;
        }

        public async Task<ResponsePagination<ResponseDTOSale>> ListSalesByDateRange(string dateStart, string dateEnd, int page, int rows)
        {
            var response = await _httpClient.GetFromJsonAsync<ResponsePagination<ResponseDTOSale>>($"api/Sale/ListSalesByDateRange?dateStart={dateStart}&dateEnd={dateEnd}&page={page}&rows={rows}");
            if (!response!.Success)
            {
                throw new InvalidOperationException(response.Message);
            }

            return response;
        }
    }
}

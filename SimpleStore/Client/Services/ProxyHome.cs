using SimpleStore.Shared.Response;
using System.Net.Http.Json;

namespace SimpleStore.Client.Services
{
    public class ProxyHome
    {
        private readonly HttpClient _httpClient;

        public ProxyHome(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ResponseDTOHome> GetAsync()
        {
            var response = await _httpClient.GetFromJsonAsync<ResponseDTOHome>("api/Home");
            if(!response!.Success) throw new ApplicationException("Error al obtener los datos");
            return response;
        }
    }
}

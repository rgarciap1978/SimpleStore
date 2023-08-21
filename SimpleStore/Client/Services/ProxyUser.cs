using SimpleStore.Shared.Request;
using SimpleStore.Shared.Response;
using System.Net.Http.Json;

namespace SimpleStore.Client.Services
{
    public class ProxyUser
    {
        private readonly HttpClient _httpClient;

        public ProxyUser(HttpClient httpClient) {
            _httpClient = httpClient;
        }

        public async Task<ResponseDTOLogin> Login(RequestDTOLogin request)
        {
            var response = await _httpClient.PostAsJsonAsync("api/User/Login", request);
            var login = await response.Content.ReadFromJsonAsync<ResponseDTOLogin>();
            if(login!.Success) return login;

            throw new InvalidOperationException(login.Message);
        }

        public async Task Register(RequestDTORegister request)
        {
            var response = await _httpClient.PostAsJsonAsync("api/User/Register", request);
            if (response.IsSuccessStatusCode)
            {
                var login = await response.Content.ReadFromJsonAsync<ResponseGeneric<string>>();
                if (!login!.Success) throw new InvalidOperationException(login.Message);
            }
            else
            {
                throw new InvalidOperationException(response.ReasonPhrase);
            }
        }

        public async Task SendToken(RequestDTOGenerateToken request)
        {
            var response = await _httpClient.PostAsJsonAsync("api/User/SendToken", request);
            if (!response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<ResponseBase>();
                throw new InvalidOperationException(result!.Message);
            }
        }

        public async Task ResetPassword(RequestDTOResetPassword request)
        {
            var response = await _httpClient.PostAsJsonAsync("api/User/ResetPassword", request);
            if(!response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<ResponseBase>();
                throw new InvalidOperationException(result!.Message);
            }
        }
    }
}

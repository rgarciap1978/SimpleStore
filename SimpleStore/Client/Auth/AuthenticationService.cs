using Blazored.SessionStorage;
using Microsoft.AspNetCore.Components.Authorization;
using SimpleStore.Shared.Response;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using System.Security.Claims;

namespace SimpleStore.Client.Auth
{
    public class AuthenticationService : AuthenticationStateProvider
    {
        private readonly ISessionStorageService _sessionStorageService;
        private readonly HttpClient _httpClient;
        private readonly ClaimsPrincipal _anonymous = new ClaimsPrincipal(new ClaimsIdentity());

        public AuthenticationService(
            ISessionStorageService sessionStorageService,
            HttpClient httpClient)
        {
            _sessionStorageService = sessionStorageService;
            _httpClient = httpClient;
        }

        public static JwtSecurityToken ParseToken(ResponseDTOLogin response)
        {
            var handler = new JwtSecurityTokenHandler();
            var token = handler.ReadJwtToken(response.Token);

            return token;
        }

        public async Task Authenticate(ResponseDTOLogin? response)
        {
            ClaimsPrincipal claimsPrincipal;
            if (response != null) {
                var token = ParseToken(response);
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", response.Token);
                claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(token.Claims.ToList(), "JWT"));
                await _sessionStorageService.SaveStorage("session", response);
            }
            else
            {
                claimsPrincipal = _anonymous;
                await _sessionStorageService.RemoveItemAsync("session");
            }

            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(claimsPrincipal)));
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var userSession = await _sessionStorageService.GetStorage<ResponseDTOLogin>("session");
            if (userSession == null) return await Task.FromResult(new AuthenticationState(_anonymous));

            var claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(ParseToken(userSession).Claims, "JWT"));

            return await Task.FromResult(new AuthenticationState(claimsPrincipal));
        }
    }
}

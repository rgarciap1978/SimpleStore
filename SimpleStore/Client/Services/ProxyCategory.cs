﻿using SimpleStore.Shared.Request;
using SimpleStore.Shared.Response;
using System.Net.Http.Json;

namespace SimpleStore.Client.Services
{
    public class ProxyCategory
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<ProxyCategory> _logger;

        public ProxyCategory(HttpClient httpClient, ILogger<ProxyCategory> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<ICollection<ResponseDTOCategory>> ListAsync()
        {
            var response = await _httpClient.GetFromJsonAsync<ResponseGeneric<ICollection<ResponseDTOCategory>>>($"api/Category/all");
            if (response!.Success) return response.Data!;

            _logger.LogError($"Error getting Categories: {response.Message}");
            return new List<ResponseDTOCategory>();
        }

        public async Task<ResponsePagination<ResponseDTOCategory>> ListAsync(string? filter, int page, int rows)
        {
            var response = await _httpClient.GetFromJsonAsync<ResponsePagination<ResponseDTOCategory>>($"api/Category?filter={filter ?? string.Empty}&page={page}&rows={rows}");
            if (response!.Success) return response;

            _logger.LogError($"Error getting Categories: {response.Message}");
            return new ResponsePagination<ResponseDTOCategory>();
        }

        public async Task<ResponseDTOCategory> GetByIdAsync(int id) {
            var response = await _httpClient.GetFromJsonAsync<ResponseGeneric<ResponseDTOCategory>>($"api/Category/{id}");
            if (response!.Success) return response.Data!;

            _logger.LogError($"Error getting Category: {response.Message}");
            return new ResponseDTOCategory();
        }

        public async Task CreateAsync(RequestDTOCategory request)
        {
            var response = await _httpClient.PostAsJsonAsync("api/Category", request);
            if (response.IsSuccessStatusCode) return;

            _logger.LogError($"Error creating Category: {response.ReasonPhrase}");
        }

        public async Task UpdateAsync(int id, RequestDTOCategory request)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/Category/{id}", request);
            if (response.IsSuccessStatusCode) return;

            _logger.LogError($"Error updating Category: {response.ReasonPhrase}");
        }

        public async Task DeleteAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/Category/{id}");
            if (response.IsSuccessStatusCode) return;

            _logger.LogError($"Error deleting Category: {response.ReasonPhrase}");
        }
    }
}

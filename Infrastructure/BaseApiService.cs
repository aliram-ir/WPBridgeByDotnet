using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace WPBridge.Client.Infrastructure
{
    public abstract class BaseApiService
    {
        protected readonly HttpClient _httpClient;
        protected readonly JsonSerializerOptions _jsonOptions;

        protected BaseApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _jsonOptions = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
        }

        protected void SetAuthToken(string token)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }

        protected async Task<T?> SendAsync<T>(HttpMethod method, string endpoint, object? data = null)
        {
            var request = new HttpRequestMessage(method, endpoint);

            if (data != null)
            {
                var json = JsonSerializer.Serialize(data, _jsonOptions);
                request.Content = new StringContent(json, Encoding.UTF8, "application/json");
            }

            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<T>(content, _jsonOptions);
        }
    }
}

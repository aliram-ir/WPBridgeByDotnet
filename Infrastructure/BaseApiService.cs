using System.Diagnostics;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace WPBridge.Client.Infrastructure
{
    /// <summary>
    /// Base class for all API services providing core HTTP functionality and error handling.
    /// Updated to bypass SSL certificate validation for development environments.
    /// </summary>
    public abstract class BaseApiService
    {
        protected readonly HttpClient _httpClient;
        protected readonly JsonSerializerOptions _jsonOptions;

        protected BaseApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;

            // Standard JSON configuration
            _jsonOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
        }

        // Static factory method or shared handler configuration to bypass SSL
        public static HttpClientHandler GetInsecureHandler()
        {
            return new HttpClientHandler
            {
                // This bypasses SSL certificate validation errors (UntrustedRoot, Expired, etc.)
                ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true
            };
        }

        public void SetBasicAuth(string consumerKey, string consumerSecret)
        {
            var authString = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{consumerKey}:{consumerSecret}"));
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", authString);
        }

        public void SetAuthToken(string token)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }

        protected async Task<T?> SendAsync<T>(HttpMethod method, string endpoint, object? data = null)
        {
            try
            {
                var request = new HttpRequestMessage(method, endpoint);

                if (data != null)
                {
                    var json = JsonSerializer.Serialize(data, _jsonOptions);
                    request.Content = new StringContent(json, Encoding.UTF8, "application/json");
                }

                var fullUri = new Uri(_httpClient.BaseAddress!, endpoint);
                Debug.WriteLine($"--- API REQUEST: {method} {fullUri} ---");

                var response = await _httpClient.SendAsync(request);
                var content = await response.Content.ReadAsStringAsync();

                Debug.WriteLine($"--- API RESPONSE: {(int)response.StatusCode} ---");

                if (!response.IsSuccessStatusCode)
                {
                    Debug.WriteLine($"Error Body: {content}");
                    return default;
                }

                return string.IsNullOrWhiteSpace(content) ? default : JsonSerializer.Deserialize<T>(content, _jsonOptions);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("--- API EXCEPTION ---");
                Debug.WriteLine(ex.ToString());
                return default;
            }
        }
    }
}

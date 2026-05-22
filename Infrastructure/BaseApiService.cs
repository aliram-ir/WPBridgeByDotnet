using System.Net;
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

            // Enforce modern security protocols
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls13;

            // Add User-Agent to avoid being blocked by WordPress security plugins
            if (!_httpClient.DefaultRequestHeaders.Contains("User-Agent"))
            {
                _httpClient.DefaultRequestHeaders.Add("User-Agent", "WPBridge-Client/1.0");
            }

            // Ensuring we handle case-insensitive and camelCase naming policies
            _jsonOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
        }

        protected void SetBasicAuth(string consumerKey, string consumerSecret)
        {
            var authString = Convert.ToBase64String(Encoding.ASCII.GetBytes($"{consumerKey}:{consumerSecret}"));
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", authString);
        }

        protected void SetAuthToken(string token)
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

                // Execute the request
                var response = await _httpClient.SendAsync(request);

                if (response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    Console.WriteLine("API Warning: Unauthorized access.");
                    return default;
                }

                var content = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    Console.WriteLine($"API Error Response ({response.StatusCode}): {content}");
                    return default;
                }

                return JsonSerializer.Deserialize<T>(content, _jsonOptions);
            }
            catch (HttpRequestException httpEx)
            {
                // Detailed logging for network diagnostics
                Console.WriteLine($"--- NETWORK CONNECTION ERROR ---");
                Console.WriteLine($"Message: {httpEx.Message}");
                if (httpEx.InnerException != null)
                {
                    Console.WriteLine($"Inner Exception: {httpEx.InnerException.Message}");
                }
                return default;
            }
            catch (TaskCanceledException)
            {
                Console.WriteLine("API Error: The request timed out.");
                return default;
            }
            catch (JsonException jEx)
            {
                Console.WriteLine($"JSON Deserialization Error: {jEx.Message}");
                return default;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An unexpected error occurred: {ex.Message}");
                return default;
            }
        }
    }
}

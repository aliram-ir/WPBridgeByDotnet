using System.Text;
using System.Text.Json;

namespace WPBridge.Client.Infrastructure
{
    /// <summary>
    /// Base class for sending HTTP requests to WordPress and WooCommerce APIs
    /// All API services should inherit from this class
    /// </summary>
    public abstract class BaseApiService
    {
        /// <summary>
        /// Shared HttpClient instance used by the SDK
        /// </summary>
        protected readonly HttpClient _httpClient;

        /// <summary>
        /// JSON serializer configuration
        /// </summary>
        private readonly JsonSerializerOptions _jsonOptions;

        protected BaseApiService(HttpClient httpClient)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));

            // Default JSON settings for WordPress API
            _jsonOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
        }

        /// <summary>
        /// Sends GET request
        /// </summary>
        protected async Task<T?> GetAsync<T>(string endpoint)
        {
            try
            {
                var response = await _httpClient.GetAsync(endpoint);

                if (!response.IsSuccessStatusCode)
                    return default;

                var content = await response.Content.ReadAsStringAsync();

                return JsonSerializer.Deserialize<T>(content, _jsonOptions);
            }
            catch (HttpRequestException)
            {
                // Network error, DNS failure, server unreachable
                return default;
            }
            catch (TaskCanceledException)
            {
                // Timeout
                return default;
            }
            catch
            {
                return default;
            }
        }

        /// <summary>
        /// Sends POST request
        /// </summary>
        protected async Task<TResponse?> PostAsync<TRequest, TResponse>(string endpoint, TRequest data)
        {
            try
            {
                var json = JsonSerializer.Serialize(data, _jsonOptions);

                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync(endpoint, content);

                if (!response.IsSuccessStatusCode)
                    return default;

                var responseContent = await response.Content.ReadAsStringAsync();

                return JsonSerializer.Deserialize<TResponse>(responseContent, _jsonOptions);
            }
            catch (HttpRequestException)
            {
                return default;
            }
            catch (TaskCanceledException)
            {
                return default;
            }
            catch
            {
                return default;
            }
        }

        /// <summary>
        /// Sends PUT request
        /// </summary>
        protected async Task<TResponse?> PutAsync<TRequest, TResponse>(string endpoint, TRequest data)
        {
            try
            {
                var json = JsonSerializer.Serialize(data, _jsonOptions);

                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClient.PutAsync(endpoint, content);

                if (!response.IsSuccessStatusCode)
                    return default;

                var responseContent = await response.Content.ReadAsStringAsync();

                return JsonSerializer.Deserialize<TResponse>(responseContent, _jsonOptions);
            }
            catch (HttpRequestException)
            {
                return default;
            }
            catch (TaskCanceledException)
            {
                return default;
            }
            catch
            {
                return default;
            }
        }

        /// <summary>
        /// Sends DELETE request
        /// </summary>
        protected async Task<bool> DeleteAsync(string endpoint)
        {
            try
            {
                var response = await _httpClient.DeleteAsync(endpoint);

                return response.IsSuccessStatusCode;
            }
            catch (HttpRequestException)
            {
                return false;
            }
            catch (TaskCanceledException)
            {
                return false;
            }
            catch
            {
                return false;
            }
        }
    }
}

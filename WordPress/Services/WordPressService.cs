using System.Net.Http.Json;

namespace WPBridge.Client.WordPress.Services
{
    /// <summary>
    /// Provides WordPress user management operations.
    /// </summary>
    public class WordPressService
    {
        private readonly HttpClient _httpClient;

        /// <summary>
        /// Initializes a new instance of WordPressService.
        /// </summary>
        public WordPressService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        /// <summary>
        /// Updates a single user meta field.
        /// </summary>
        /// <param name="userId">WordPress user ID.</param>
        /// <param name="key">Meta key.</param>
        /// <param name="value">Meta value.</param>
        public async Task UpdateUserMetaAsync(int userId, string key, string value)
        {
            var payload = new
            {
                meta = new Dictionary<string, string>
                {
                    { key, value }
                }
            };

            var response = await _httpClient.PutAsJsonAsync(
                $"/wp-json/wp/v2/users/{userId}",
                payload);

            response.EnsureSuccessStatusCode();
        }

        /// <summary>
        /// Updates multiple user meta fields at once.
        /// </summary>
        /// <param name="userId">WordPress user ID.</param>
        /// <param name="meta">Dictionary of meta fields.</param>
        public async Task UpdateUserMetaAsync(int userId, Dictionary<string, string> meta)
        {
            var payload = new
            {
                meta = meta
            };

            var response = await _httpClient.PutAsJsonAsync(
                $"/wp-json/wp/v2/users/{userId}",
                payload);

            response.EnsureSuccessStatusCode();
        }
    }
}

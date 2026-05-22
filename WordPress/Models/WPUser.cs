using System.Text.Json.Serialization;

namespace WPBridge.Client.WordPress.Models
{
    public class WPUser
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("username")]
        public string Username { get; set; } = string.Empty;

        [JsonPropertyName("email")]
        public string Email { get; set; } = string.Empty;

        [JsonPropertyName("name")]
        public string DisplayName { get; set; } = string.Empty;

        [JsonPropertyName("first_name")]
        public string FirstName { get; set; } = string.Empty;

        [JsonPropertyName("last_name")]
        public string LastName { get; set; } = string.Empty;

        // Meta data is often nested in WordPress responses
        [JsonPropertyName("meta")]
        public Dictionary<string, object>? Meta { get; set; }
    }
}

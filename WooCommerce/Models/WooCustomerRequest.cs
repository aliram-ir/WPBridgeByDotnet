using System.Text.Json.Serialization;

namespace WPBridge.Client.WooCommerce.Models
{
    public class WooCustomerRequest
    {
        [JsonPropertyName("email")]
        public string Email { get; set; } = string.Empty;

        [JsonPropertyName("first_name")]
        public string FirstName { get; set; } = string.Empty;

        [JsonPropertyName("last_name")]
        public string LastName { get; set; } = string.Empty;

        [JsonPropertyName("username")]
        public string Username { get; set; } = string.Empty;

        [JsonPropertyName("password")]
        public string Password { get; set; } = string.Empty;
        public WooAddress? Billing { get; set; }
        public WooAddress? Shipping { get; set; }
    }
}

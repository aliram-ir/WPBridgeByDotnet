using System.Collections.Generic;
using System.Text.Json.Serialization;
using WPBridge.Client.WooCommerce.Models;

namespace WPBridge.Client.WordPress.Models
{
    /// <summary>
    /// Represents a full WordPress registration request.
    /// </summary>
    public class WPRegisterRequest
    {
        [JsonPropertyName("username")]
        public string Username { get; set; } = string.Empty;

        [JsonPropertyName("email")]
        public string Email { get; set; } = string.Empty;

        [JsonPropertyName("password")]
        public string Password { get; set; } = string.Empty;

        [JsonPropertyName("first_name")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? FirstName { get; set; }

        [JsonPropertyName("last_name")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? LastName { get; set; }

        [JsonPropertyName("phone_number")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? PhoneNumber { get; set; }

        [JsonPropertyName("role")]
        public string Role { get; set; } = "customer";

        [JsonPropertyName("billing")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public WooBillingAddress? Billing { get; set; }

        [JsonPropertyName("shipping")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public WooShippingAddress? Shipping { get; set; }

        [JsonPropertyName("meta")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public Dictionary<string, string>? Meta { get; set; }
    }
}

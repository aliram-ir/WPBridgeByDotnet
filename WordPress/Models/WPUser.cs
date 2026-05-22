using System.Text.Json.Serialization;
using WPBridge.Client.WooCommerce.Models;

public class WPUser
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("username")]
    public string Username { get; set; } = string.Empty;

    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("email")]
    public string Email { get; set; } = string.Empty;

    [JsonPropertyName("phone")]
    public string Phone { get; set; } = string.Empty;

    [JsonPropertyName("billing")]
    public WooBillingAddress Billing { get; set; } = new();

    [JsonPropertyName("shipping")]
    public WooShippingAddress Shipping { get; set; } = new();
}

using System.Text.Json;
using System.Text.Json.Serialization;

namespace WPBridge.Client.WordPress.Models
{
    public class WPUser
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string? Name { get; set; }

        [JsonPropertyName("slug")]
        public string? Slug { get; set; }

        // دسترسی به متای عمومی وردپرس
        [JsonPropertyName("meta")]
        public Dictionary<string, object>? Meta { get; set; }

        // دسترسی به متای تخصصی ووکامرس که در خروجی شما بود
        [JsonPropertyName("woocommerce_meta")]
        public Dictionary<string, object>? WooMeta { get; set; }

        // برای هندل کردن فیلدهای متغیر که ممکن است در آینده اضافه شوند
        [JsonExtensionData]
        public Dictionary<string, JsonElement>? AdditionalData { get; set; }
    }
}

using System.Text.Json.Serialization;
using System.Text.Json;

namespace WPBridge.Client.WordPress.Models
{
    public class WPUser
    {
        public int Id { get; set; }
        public string? Username { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Description { get; set; }
        public string? Link { get; set; }
        public string? Slug { get; set; }

        [JsonPropertyName("first_name")]
        public string? FirstName { get; set; }

        [JsonPropertyName("last_name")]
        public string? LastName { get; set; }

        public string? Nickname { get; set; }
        public string? Locale { get; set; }

        [JsonPropertyName("registered_date")]
        public DateTime? RegisteredDate { get; set; }

        public List<string>? Roles { get; set; }

        // جلوگیری از خطا در صورت خالی بودن Meta (تبدیل [] به null یا {})
        public object? Meta { get; set; }

        [JsonPropertyName("avatar_urls")]
        public Dictionary<string, string>? AvatarUrls { get; set; }

        // این بخش تمام فیلدهای اضافی که در کلاس نیستند را جذب می‌کند
        [JsonExtensionData]
        public Dictionary<string, JsonElement>? AdditionalData { get; set; }
    }
}

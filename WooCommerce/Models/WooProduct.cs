using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace WPBridge.Client.WooCommerce.Models
{
    /// <summary>
    /// Represents a WooCommerce product.
    /// </summary>
    public class WooProduct
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;

        [JsonPropertyName("slug")]
        public string Slug { get; set; } = string.Empty;

        [JsonPropertyName("type")]
        public string Type { get; set; } = string.Empty;

        [JsonPropertyName("status")]
        public string Status { get; set; } = string.Empty;

        [JsonPropertyName("description")]
        public string Description { get; set; } = string.Empty;

        [JsonPropertyName("short_description")]
        public string ShortDescription { get; set; } = string.Empty;

        [JsonPropertyName("price")]
        public string Price { get; set; } = string.Empty;

        [JsonPropertyName("regular_price")]
        public string RegularPrice { get; set; } = string.Empty;

        [JsonPropertyName("sale_price")]
        public string SalePrice { get; set; } = string.Empty;

        [JsonPropertyName("purchasable")]
        public bool Purchasable { get; set; }

        [JsonPropertyName("in_stock")]
        public bool InStock { get; set; }

        [JsonPropertyName("images")]
        public List<WooProductImage> Images { get; set; } = new();

        [JsonPropertyName("categories")]
        public List<WooProductCategory> Categories { get; set; } = new();

        // اضافه کردن این فیلد برای جلوگیری از خطا در پاسخ‌های حجیم ووکامرس
        [JsonPropertyName("meta_data")]
        public object? MetaData { get; set; }
    }

    public class WooProductImage
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("src")]
        public string Src { get; set; } = string.Empty;

        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;

        [JsonPropertyName("alt")]
        public string Alt { get; set; } = string.Empty;
    }

    public class WooProductCategory
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;

        [JsonPropertyName("slug")]
        public string Slug { get; set; } = string.Empty;
    }
}

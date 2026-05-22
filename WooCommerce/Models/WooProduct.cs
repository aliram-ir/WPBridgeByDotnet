using System.Collections.Generic;

namespace WPBridge.Client.WooCommerce.Models
{
    /// <summary>
    /// Represents a WooCommerce product.
    /// </summary>
    public class WooProduct
    {
        /// <summary>
        /// Product identifier.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Product name.
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Product slug.
        /// </summary>
        public string Slug { get; set; } = string.Empty;

        /// <summary>
        /// Product type (simple, variable, grouped, external).
        /// </summary>
        public string Type { get; set; } = string.Empty;

        /// <summary>
        /// Product status.
        /// </summary>
        public string Status { get; set; } = string.Empty;

        /// <summary>
        /// Product description.
        /// </summary>
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// Short description.
        /// </summary>
        public string ShortDescription { get; set; } = string.Empty;

        /// <summary>
        /// Product price.
        /// </summary>
        public string Price { get; set; } = string.Empty;

        /// <summary>
        /// Regular price.
        /// </summary>
        public string RegularPrice { get; set; } = string.Empty;

        /// <summary>
        /// Sale price.
        /// </summary>
        public string SalePrice { get; set; } = string.Empty;

        /// <summary>
        /// Indicates whether the product is purchasable.
        /// </summary>
        public bool Purchasable { get; set; }

        /// <summary>
        /// Indicates whether the product is in stock.
        /// </summary>
        public bool InStock { get; set; }

        /// <summary>
        /// Product images.
        /// </summary>
        public List<WooProductImage> Images { get; set; } = new();

        /// <summary>
        /// Product categories.
        /// </summary>
        public List<WooProductCategory> Categories { get; set; } = new();
    }

    /// <summary>
    /// Represents a WooCommerce product image.
    /// </summary>
    public class WooProductImage
    {
        /// <summary>
        /// Image identifier.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Image URL.
        /// </summary>
        public string Src { get; set; } = string.Empty;

        /// <summary>
        /// Image name.
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Alternative text for the image.
        /// </summary>
        public string Alt { get; set; } = string.Empty;
    }

    /// <summary>
    /// Represents a WooCommerce product category.
    /// </summary>
    public class WooProductCategory
    {
        /// <summary>
        /// Category identifier.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Category name.
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Category slug.
        /// </summary>
        public string Slug { get; set; } = string.Empty;
    }
}

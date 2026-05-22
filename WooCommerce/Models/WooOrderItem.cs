namespace WPBridge.Client.WooCommerce.Models
{
    /// <summary>
    /// Represents a WooCommerce order line item.
    /// </summary>
    public class WooOrderItem
    {
        /// <summary>
        /// Product identifier.
        /// </summary>
        public int ProductId { get; set; }

        /// <summary>
        /// Quantity of product.
        /// </summary>
        public int Quantity { get; set; }
    }
}

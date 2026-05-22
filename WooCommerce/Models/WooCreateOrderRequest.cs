using System.Collections.Generic;

namespace WPBridge.Client.WooCommerce.Models
{
    /// <summary>
    /// Represents a request for creating a WooCommerce order.
    /// </summary>
    public class WooCreateOrderRequest
    {
        /// <summary>
        /// Payment method identifier.
        /// </summary>
        public string PaymentMethod { get; set; } = "bacs";

        /// <summary>
        /// Payment method title.
        /// </summary>
        public string PaymentMethodTitle { get; set; } = "Direct Bank Transfer";

        /// <summary>
        /// Indicates whether the order is already paid.
        /// </summary>
        public bool SetPaid { get; set; }

        /// <summary>
        /// Customer identifier.
        /// </summary>
        public int CustomerId { get; set; }

        /// <summary>
        /// Order line items.
        /// </summary>
        public List<WooOrderItem> LineItems { get; set; } = new();
    }
}

using System;
using System.Collections.Generic;
using WPBridge.Client.WordPress.Models;

namespace WPBridge.Client.WooCommerce.Models
{
    /// <summary>
    /// Represents a WooCommerce order.
    /// </summary>
    public class WooOrder
    {
        /// <summary>
        /// Order identifier.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Order status.
        /// </summary>
        public string? Status { get; set; }

        /// <summary>
        /// Order currency.
        /// </summary>
        public string? Currency { get; set; }

        /// <summary>
        /// Order total amount.
        /// </summary>
        public string? Total { get; set; }

        /// <summary>
        /// Customer identifier.
        /// </summary>
        public int CustomerId { get; set; }

        /// <summary>
        /// Date when order was created.
        /// </summary>
        public DateTime DateCreated { get; set; }

        /// <summary>
        /// Payment method identifier.
        /// </summary>
        public string? PaymentMethod { get; set; }

        /// <summary>
        /// Payment method title.
        /// </summary>
        public string? PaymentMethodTitle { get; set; }

        /// <summary>
        /// Indicates whether order is paid.
        /// </summary>
        public bool SetPaid { get; set; }

        /// <summary>
        /// Billing address.
        /// </summary>
        public WooBillingAddress? Billing { get; set; }

        /// <summary>
        /// Shipping address.
        /// </summary>
        public WooShippingAddress? Shipping { get; set; }

        /// <summary>
        /// Line items in the order.
        /// </summary>
        public List<WooOrderLineItem>? LineItems { get; set; }
    }


    /// <summary>
    /// Order line item.
    /// </summary>
    public class WooOrderLineItem
    {
        public int Id { get; set; }

        public string? Name { get; set; }

        public int ProductId { get; set; }

        public int Quantity { get; set; }

        public string? Total { get; set; }
    }
}

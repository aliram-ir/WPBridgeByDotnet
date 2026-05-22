using System.Net.Http;
using System.Net.Http.Json;
using WPBridge.Client.WooCommerce.Models;

namespace WPBridge.Client.WooCommerce
{
    /// <summary>
    /// Provides WooCommerce customer operations.
    /// </summary>
    public class WooCommerceService
    {
        private readonly HttpClient _httpClient;

        /// <summary>
        /// Initializes WooCommerceService.
        /// </summary>
        public WooCommerceService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        /// <summary>
        /// Updates WooCommerce customer billing address.
        /// </summary>
        public async Task UpdateBillingAsync(int userId, WooBillingAddress billing)
        {
            await UpdateCustomerAsync(userId, billing, null);
        }

        /// <summary>
        /// Updates WooCommerce customer shipping address.
        /// </summary>
        public async Task UpdateShippingAsync(int userId, WooShippingAddress shipping)
        {
            await UpdateCustomerAsync(userId, null, shipping);
        }

        /// <summary>
        /// Updates WooCommerce customer billing and/or shipping in a single request.
        /// </summary>
        /// <param name="userId">WooCommerce customer ID.</param>
        /// <param name="billing">Billing address (optional).</param>
        /// <param name="shipping">Shipping address (optional).</param>
        public async Task UpdateCustomerAsync(
            int userId,
            WooBillingAddress? billing,
            WooShippingAddress? shipping)
        {
            var payload = new Dictionary<string, object>();

            if (billing != null)
                payload["billing"] = billing;

            if (shipping != null)
                payload["shipping"] = shipping;

            if (payload.Count == 0)
                return;

            var response = await _httpClient.PutAsJsonAsync(
                $"/wp-json/wc/v3/customers/{userId}",
                payload);

            response.EnsureSuccessStatusCode();
        }
    }
}

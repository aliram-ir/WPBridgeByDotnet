using System;
using System.Threading.Tasks;
using WPBridge.Client.Infrastructure;
using WPBridge.Client.WooCommerce.Models;

namespace WPBridge.Client.WooCommerce.Services
{
    /// <summary>
    /// Provides operations related to WooCommerce orders.
    /// </summary>
    public class WooOrderService : BaseApiService
    {
        private readonly string? _consumerKey;
        private readonly string? _consumerSecret;

        public WooOrderService(HttpClient httpClient, string? consumerKey, string? consumerSecret)
            : base(httpClient)
        {
            _consumerKey = consumerKey;
            _consumerSecret = consumerSecret;
        }

        private string AppendAuth(string endpoint)
        {
            if (string.IsNullOrWhiteSpace(_consumerKey) || string.IsNullOrWhiteSpace(_consumerSecret))
                return endpoint;

            var separator = endpoint.Contains("?") ? "&" : "?";

            return $"{endpoint}{separator}consumer_key={_consumerKey}&consumer_secret={_consumerSecret}";
        }

        /// <summary>
        /// Creates a new WooCommerce order.
        /// </summary>
        public async Task<WooOrder?> CreateOrderAsync(WooCreateOrderRequest request)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            var endpoint = AppendAuth("/wp-json/wc/v3/orders");

            return await PostAsync<WooCreateOrderRequest, WooOrder>(endpoint, request);
        }

    }
}

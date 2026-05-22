using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WPBridge.Client.Infrastructure;
using WPBridge.Client.WooCommerce.Models;

namespace WPBridge.Client.WooCommerce.Services
{
    /// <summary>
    /// Provides operations related to WooCommerce products.
    /// </summary>
    public class WooProductService : BaseApiService
    {
        private readonly string? _consumerKey;
        private readonly string? _consumerSecret;

        public WooProductService(HttpClient httpClient, string? consumerKey, string? consumerSecret)
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
        /// Retrieves WooCommerce products.
        /// </summary>
        public async Task<List<WooProduct>?> GetProductsAsync()
        {
            var endpoint = AppendAuth("/wp-json/wc/v3/products");

            return await GetAsync<List<WooProduct>>(endpoint);
        }

        /// <summary>
        /// Retrieves a WooCommerce product by identifier.
        /// </summary>
        public async Task<WooProduct?> GetProductAsync(int productId)
        {
            if (productId <= 0)
                throw new ArgumentException("Invalid product id.", nameof(productId));

            var endpoint = AppendAuth($"/wp-json/wc/v3/products/{productId}");

            return await GetAsync<WooProduct>(endpoint);
        }

        /// <summary>
        /// Retrieves WooCommerce products with pagination.
        /// </summary>
        public async Task<List<WooProduct>?> GetProductsAsync(int page, int perPage)
        {
            var endpoint = AppendAuth($"/wp-json/wc/v3/products?page={page}&per_page={perPage}");

            return await GetAsync<List<WooProduct>>(endpoint);
        }
    }
}

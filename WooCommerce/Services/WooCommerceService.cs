using WPBridge.Client.Infrastructure;
using WPBridge.Client.WooCommerce.Models;

namespace WPBridge.Client.WooCommerce.Services
{
    /// <summary>
    /// Service for interacting with WooCommerce REST API.
    /// Handles customers, products, and other WooCommerce-specific operations.
    /// </summary>
    public class WooCommerceService : BaseApiService
    {
        /// <summary>
        /// Initializes a new instance of WooCommerceService with Basic Auth credentials.
        /// </summary>
        /// <param name="httpClient">The shared HttpClient instance.</param>
        /// <param name="consumerKey">WooCommerce Consumer Key.</param>
        /// <param name="consumerSecret">WooCommerce Consumer Secret.</param>
        public WooCommerceService(HttpClient httpClient, string consumerKey, string consumerSecret) : base(httpClient)
        {
            // Set Basic Auth for WooCommerce API
            SetBasicAuth(consumerKey, consumerSecret);
        }

        /// <summary>
        /// Registers a new customer in WooCommerce.
        /// This replaces the standard WordPress user registration.
        /// </summary>
        /// <param name="request">The customer data including email, username, and password.</param>
        /// <returns>The created WooCustomer object or null if failed.</returns>
        public async Task<WooCustomer?> CreateCustomerAsync(WooCustomerRequest request)
        {
            // Endpoint: wp-json/wc/v3/customers
            return await SendAsync<WooCustomer>(HttpMethod.Post, "wc/v3/customers", request);
        }

        /// <summary>
        /// Retrieves a list of products from WooCommerce.
        /// </summary>
        /// <returns>A list of WooProduct objects.</returns>
        public async Task<List<WooProduct>?> GetProductsAsync()
        {
            // Endpoint: wp-json/wc/v3/products
            return await SendAsync<List<WooProduct>>(HttpMethod.Get, "wc/v3/products");
        }

        /// <summary>
        /// Retrieves a single product by its ID.
        /// </summary>
        /// <param name="productId">The unique ID of the product.</param>
        /// <returns>The WooProduct object or null if not found.</returns>
        public async Task<WooProduct?> GetProductAsync(int productId)
        {
            // Endpoint: wp-json/wc/v3/products/{id}
            return await SendAsync<WooProduct>(HttpMethod.Get, $"wc/v3/products/{productId}");
        }

        /// <summary>
        /// Updates an existing customer's information.
        /// Useful for syncing billing and shipping data after registration.
        /// </summary>
        /// <param name="customerId">The WooCommerce customer ID.</param>
        /// <param name="request">The updated customer data.</param>
        /// <returns>The updated WooCustomer object.</returns>
        public async Task<WooCustomer?> UpdateCustomerAsync(int customerId, WooCustomerRequest request)
        {
            // Endpoint: wp-json/wc/v3/customers/{id}
            return await SendAsync<WooCustomer>(HttpMethod.Put, $"wc/v3/customers/{customerId}", request);
        }
    }
}

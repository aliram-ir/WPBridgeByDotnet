using WPBridge.Client.Infrastructure;
using WPBridge.Client.WooCommerce.Models;

namespace WPBridge.WooCommerce.Services
{
    public class WooCommerceService : BaseApiService
    {
        public WooCommerceService(HttpClient httpClient) : base(httpClient) { }

        // Customer Registration (Create)
        public async Task<WooCustomer?> CreateCustomerAsync(WooCustomerRequest request)
        {
            return await SendAsync<WooCustomer>(HttpMethod.Post, "wc/v3/customers", request);
        }

        // Get Product List
        public async Task<List<WooProduct>?> GetProductsAsync()
        {
            return await SendAsync<List<WooProduct>>(HttpMethod.Get, "wc/v3/products");
        }

        // Get Single Product
        public async Task<WooProduct?> GetProductAsync(int productId)
        {
            return await SendAsync<WooProduct>(HttpMethod.Get, $"wc/v3/products/{productId}");
        }

        // Note: Future order methods will be added here to keep everything centralized
    }
}

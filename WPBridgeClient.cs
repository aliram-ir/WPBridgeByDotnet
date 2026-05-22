using WPBridge.Client.WordPress.Services;
using WPBridge.WooCommerce.Services;
using WPBridge.WordPress.Services; // این خط را چک کن

namespace WPBridge.Client
{
    public class WPBridgeClient
    {
        public WPAuthService Auth { get; }
        public WPUserService User { get; } // باید اینجا تعریف شده باشد
        public WooCommerceService Woo { get; }

        public WPBridgeClient(string baseUrl, string consumerKey = "", string consumerSecret = "")
        {
            var httpClient = new HttpClient { BaseAddress = new Uri($"{baseUrl}/wp-json/") };

            // ... تنظیمات Auth ...

            Auth = new WPAuthService(httpClient);
            User = new WPUserService(httpClient); // باید اینجا مقداردهی شود
            Woo = new WooCommerceService(httpClient, consumerKey, consumerSecret);
        }
    }
}

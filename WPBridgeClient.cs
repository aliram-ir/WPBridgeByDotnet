using WPBridge.Client.WordPress.Services;
using WPBridge.WooCommerce.Services;

namespace WPBridge.Client
{
    public class WPBridgeClient
    {
        public WPAuthService Auth { get; }
        public WPUserService User { get; }
        public WooCommerceService Woo { get; }

        public WPBridgeClient(string baseUrl, string consumerKey = "", string consumerSecret = "")
        {
            var httpClient = new HttpClient { BaseAddress = new Uri($"{baseUrl}/wp-json/") };

            // If Woo keys are provided, we could add Basic Auth header here
            if (!string.IsNullOrEmpty(consumerKey))
            {
                var authHeader = Convert.ToBase64String(System.Text.Encoding.ASCII.GetBytes($"{consumerKey}:{consumerSecret}"));
                httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", authHeader);
            }

            Auth = new WPAuthService(httpClient);
            User = new WPUserService(httpClient);
            Woo = new WooCommerceService(httpClient);
        }
    }
}

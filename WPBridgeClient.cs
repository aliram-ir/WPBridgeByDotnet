using WPBridge.Client.Infrastructure;
using WPBridge.Client.WooCommerce.Services;
using WPBridge.Client.WordPress.Services;

namespace WPBridge.Client
{
    /// <summary>
    /// The main entry point for the WPBridge library.
    /// Manages shared HttpClient and provides access to WordPress and WooCommerce services.
    /// </summary>
    public class WPBridgeClient
    {
        private readonly HttpClient _httpClient;

        public WPAuthService Auth { get; }
        public WPUserService User { get; }
        public WooCommerceService Woo { get; }

        /// <summary>
        /// Initializes the library with the site URL and WooCommerce credentials.
        /// Bypasses SSL certificate validation for compatibility.
        /// </summary>
        /// <param name="baseUrl">The base URL of the WordPress site (e.g., https://example.com).</param>
        /// <param name="consumerKey">WooCommerce Consumer Key (CK).</param>
        /// <param name="consumerSecret">WooCommerce Consumer Secret (CS).</param>
        public WPBridgeClient(string baseUrl, string consumerKey = "", string consumerSecret = "")
        {
            // Use the insecure handler from BaseApiService to bypass SSL certificate issues (UntrustedRoot)
            var handler = BaseApiService.GetInsecureHandler();

            // Ensure the URL ends with a trailing slash for correct Uri combining
            var finalUrl = baseUrl.TrimEnd('/') + "/wp-json/";

            _httpClient = new HttpClient(handler)
            {
                BaseAddress = new Uri(finalUrl),
                Timeout = TimeSpan.FromSeconds(30)
            };

            // Setup default headers to prevent blocking by security plugins
            _httpClient.DefaultRequestHeaders.Add("User-Agent", "WPBridge-Client/1.0");
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            // Initialize services
            Auth = new WPAuthService(_httpClient);
            User = new WPUserService(_httpClient);
            Woo = new WooCommerceService(_httpClient, consumerKey, consumerSecret);
        }

        /// <summary>
        /// Manually sets a JWT token for authorized WordPress requests.
        /// </summary>
        /// <param name="token">The JWT token received from LoginAsync.</param>
        public void SetToken(string token)
        {
            Auth.SetAuthToken(token);
            User.SetAuthToken(token);
            // Optionally set it for Woo if the site requires JWT for WooCommerce as well
            Woo.SetAuthToken(token);
        }
    }
}

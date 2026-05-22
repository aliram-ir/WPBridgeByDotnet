using System;
using System.Net.Http;
using WPBridge.Client.Infrastructure;
using WPBridge.Client.WordPress;
using WPBridge.Client.WordPress.Services;
using WPBridge.Client.WooCommerce;
using WPBridge.Client.WooCommerce.Services;

namespace WPBridge.Client
{
    /// <summary>
    /// Main entry point for interacting with WordPress and WooCommerce APIs.
    /// </summary>
    public class WPBridgeClient : IDisposable
    {
        public string BaseUrl { get; }
        public string? WooConsumerKey { get; }
        public string? WooConsumerSecret { get; }
        internal HttpClient HttpClient { get; }
        private readonly WPAuthTokenHandler _authHandler;

        // WordPress core services
        public WPAuthService WPAuth { get; }
        public WPUserService WPUsers { get; }
        public WordPressService WordPress { get; }

        // Digits OTP authentication
        public DigitsService Digits { get; }

        // WooCommerce services
        public WooProductService WooProducts { get; }
        public WooOrderService WooOrders { get; }
        public WooCommerceService WooCommerce { get; }

        private bool _disposed;

        public WPBridgeClient(string baseUrl, string? wooConsumerKey = null, string? wooConsumerSecret = null)
        {
            if (string.IsNullOrWhiteSpace(baseUrl))
                throw new ArgumentException("Base url cannot be null or empty.", nameof(baseUrl));

            BaseUrl = baseUrl.TrimEnd('/');
            WooConsumerKey = wooConsumerKey;
            WooConsumerSecret = wooConsumerSecret;

            _authHandler = new WPAuthTokenHandler
            {
                InnerHandler = new HttpClientHandler()
            };

            HttpClient = new HttpClient(_authHandler)
            {
                BaseAddress = new Uri(BaseUrl)
            };

            // Instantiate shared lower-level WordPress services
            WordPress = new WordPressService(HttpClient);

            // Instantiate WooCommerce base service (without auth) for billing/shipping etc.
            WooCommerce = new WooCommerceService(HttpClient);

            // Inject dependencies properly - WPAuthService needs WordPressService + WooCommerceService
            WPAuth = new WPAuthService(HttpClient, WordPress, WooCommerce);

            WPUsers = new WPUserService(HttpClient);
            Digits = new DigitsService(HttpClient);

            WooProducts = new WooProductService(HttpClient, WooConsumerKey, WooConsumerSecret);
            WooOrders = new WooOrderService(HttpClient, WooConsumerKey, WooConsumerSecret);
        }

        /// <summary>
        /// Sets JWT authentication token.
        /// </summary>
        public void SetToken(string token)
        {
            _authHandler.SetToken(token);
        }

        /// <summary>
        /// Clears JWT authentication token.
        /// </summary>
        public void ClearToken()
        {
            _authHandler.ClearToken();
        }

        public void Dispose()
        {
            if (_disposed)
                return;

            HttpClient.Dispose();
            _disposed = true;
        }
    }
}

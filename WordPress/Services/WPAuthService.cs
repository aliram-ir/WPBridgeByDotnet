using WPBridge.Client.Infrastructure;
using WPBridge.Client.WooCommerce;
using WPBridge.Client.WordPress.Models;

namespace WPBridge.Client.WordPress.Services
{
    /// <summary>
    /// Provides authentication and full registration operations for WordPress users.
    /// </summary>
    public class WPAuthService : BaseApiService
    {
        private readonly WordPressService _wordpress;
        private readonly WooCommerceService _woocommerce;

        /// <summary>
        /// Initializes a new instance of <see cref="WPAuthService"/> with necessary dependencies.
        /// </summary>
        /// <param name="httpClient">HTTP client to send requests.</param>
        /// <param name="wordpressService">WordPress meta service.</param>
        /// <param name="woocommerceService">WooCommerce customer service.</param>
        public WPAuthService(
            HttpClient httpClient,
            WordPressService wordpressService,
            WooCommerceService woocommerceService)
            : base(httpClient)
        {
            _wordpress = wordpressService ?? throw new ArgumentNullException(nameof(wordpressService));
            _woocommerce = woocommerceService ?? throw new ArgumentNullException(nameof(woocommerceService));
        }

        /// <summary>
        /// Authenticates a WordPress user and retrieves JWT token.
        /// </summary>
        /// <param name="request">Login request containing username and password.</param>
        /// <returns>JWT token response or null.</returns>
        public async Task<WPLoginResponse?> LoginAsync(WPLoginRequest request)
        {
            if (request is null)
                throw new ArgumentNullException(nameof(request));

            return await PostAsync<WPLoginRequest, WPLoginResponse>(
                    "/wp-json/jwt-auth/v1/token",
                    request)
                .ConfigureAwait(false);
        }

        /// <summary>
        /// Registers a new WordPress user (simple version).
        /// This relies on a custom REST API endpoint enabled on the WordPress server.
        /// </summary>
        /// <param name="request">User registration data.</param>
        /// <returns>Created WordPress user, or null if failed.</returns>
        public async Task<WPUser?> RegisterAsync(WPRegisterRequest request)
        {
            if (request is null)
                throw new ArgumentNullException(nameof(request));

            return await PostAsync<WPRegisterRequest, WPUser>(
                    "/wp-json/wp/v2/users/register",
                    request)
                .ConfigureAwait(false);
        }

        /// <summary>
        /// Full registration pipeline for WordPress user including metadata and WooCommerce billing/shipping.
        /// </summary>
        /// <param name="request">Complete register request.</param>
        /// <returns>Registered <see cref="WPUser"/> instance.</returns>
        public async Task<WPUser> RegisterFullAsync(WPRegisterRequest request)
        {
            if (request is null)
                throw new ArgumentNullException(nameof(request));

            // 1) Register WordPress user
            var user = await RegisterAsync(request).ConfigureAwait(false);

            if (user is null)
                throw new InvalidOperationException("User registration failed: received null from the server.");

            // 2) Update WordPress user meta if any
            if (request.Meta?.Count > 0)
            {
                await _wordpress.UpdateUserMetaAsync(user.Id, request.Meta).ConfigureAwait(false);
            }

            // 3) Update WooCommerce customer billing and shipping info
            await _woocommerce.UpdateCustomerAsync(user.Id, request.Billing, request.Shipping).ConfigureAwait(false);

            return user;
        }

        /// <summary>
        /// Validates the current JWT token.
        /// </summary>
        /// <returns>True if token is valid; otherwise, false.</returns>
        public async Task<bool> ValidateTokenAsync()
        {
            var result = await PostAsync<object, object>(
                    "/wp-json/jwt-auth/v1/token/validate",
                    new { })
                .ConfigureAwait(false);

            return result != null;
        }
    }
}

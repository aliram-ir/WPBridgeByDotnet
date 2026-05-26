using WPBridge.Client.Infrastructure;
using WPBridge.Client.WordPress.Models;

namespace WPBridge.Client.WordPress.Services
{
    /// <summary>
    /// Service for handling WordPress authentication and authorization.
    /// Uses JWT (JSON Web Token) for secure communication.
    /// </summary>
    public class WPAuthService : BaseApiService
    {
        /// <summary>
        /// Initializes a new instance of WPAuthService.
        /// </summary>
        /// <param name="httpClient">The shared HttpClient instance.</param>
        public WPAuthService(HttpClient httpClient) : base(httpClient) { }

        /// <summary>
        /// Authenticates a user using their username and password.
        /// </summary>
        /// <param name="username">The WordPress username or email.</param>
        /// <param name="password">The user's password.</param>
        /// <returns>A WPLoginResponse containing the JWT token and user details, or null if failed.</returns>
        public async Task<WPLoginResponse?> LoginAsync(string username, string password)
        {
            var request = new WPLoginRequest(username, password);

            // Endpoint provided by JWT Auth plugin: wp-json/jwt-auth/v1/token
            return await SendAsync<WPLoginResponse>(HttpMethod.Post, "jwt-auth/v1/token", request);
        }

        /// <summary>
        /// Validates the existing JWT token.
        /// </summary>
        /// <returns>True if the token is valid, otherwise false.</returns>
        public async Task<bool> ValidateTokenAsync()
        {
            // Note: Token should be added to the header via WPAuthTokenHandler or manual SetToken
            var result = await SendAsync<object>(HttpMethod.Post, "jwt-auth/v1/token/validate", null);
            return result != null;
        }
    }
}

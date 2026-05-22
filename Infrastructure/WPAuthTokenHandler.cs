using System.Net.Http.Headers;

namespace WPBridge.Client.Infrastructure
{
    /// <summary>
    /// HTTP handler that automatically attaches the authentication token
    /// to outgoing HTTP requests.
    /// </summary>
    public class WPAuthTokenHandler : DelegatingHandler
    {
        private string? _token;

        /// <summary>
        /// Sets the JWT authentication token.
        /// </summary>
        public void SetToken(string token)
        {
            _token = token;
        }

        /// <summary>
        /// Clears the stored authentication token.
        /// </summary>
        public void ClearToken()
        {
            _token = null;
        }

        /// <summary>
        /// Intercepts outgoing HTTP requests and attaches the Authorization header.
        /// </summary>
        protected override Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            if (!string.IsNullOrWhiteSpace(_token))
            {
                request.Headers.Authorization =
                    new AuthenticationHeaderValue("Bearer", _token);
            }

            return base.SendAsync(request, cancellationToken);
        }
    }
}

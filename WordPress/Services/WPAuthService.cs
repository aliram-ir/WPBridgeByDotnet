using WPBridge.Client.Infrastructure;
using WPBridge.Client.WordPress.Models;

namespace WPBridge.Client.WordPress.Services
{
    public class WPAuthService : BaseApiService
    {
        public WPAuthService(HttpClient httpClient) : base(httpClient) { }

        public async Task<WPLoginResponse?> LoginAsync(string username, string password)
        {
            var request = new WPLoginRequest(username, password);
            return await SendAsync<WPLoginResponse>(HttpMethod.Post, "jwt-auth/v1/token", request);
        }
    }
}

using WPBridge.Client.Infrastructure;
using WPBridge.Client.WordPress.Models;

namespace WPBridge.Client.WordPress.Services
{
    public class WPUserService : BaseApiService
    {
        public WPUserService(HttpClient httpClient) : base(httpClient) { }

        public async Task<WPUser?> GetCurrentUserAsync(string token)
        {
            SetAuthToken(token);
            return await SendAsync<WPUser>(HttpMethod.Get, "wp/v2/users/me");
        }
    }
}

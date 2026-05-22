using WPBridge.Client.Infrastructure;
using WPBridge.Client.WordPress.Models;

namespace WPBridge.WordPress.Services
{
    public class WPUserService : BaseApiService
    {
        public WPUserService(HttpClient httpClient) : base(httpClient) { }

        // Get list of users (Requires authentication header set)
        public async Task<List<WPUser>?> GetUsersListAsync()
        {
            // Note: If you need to view all users, user must have 'list_users' capability
            return await SendAsync<List<WPUser>>(HttpMethod.Get, $"wp/v2/users");
        }

        public async Task<List<WPUser>?> GetUsersListAsync(int page = 1, int perPage = 10)
        {
            // Note: If you need to view all users, user must have 'list_users' capability
            return await SendAsync<List<WPUser>>(HttpMethod.Get, $"wp/v2/users?page={page}&per_page={perPage}");
        }

        public async Task<WPUser?> GetUserDetailsAsync(int userId)
        {
            return await SendAsync<WPUser>(HttpMethod.Get, $"wp/v2/users/{userId}?context=edit");
        }


        public async Task<WPUser?> GetCurrentUserAsync(string token)
        {
            SetAuthToken(token);
            return await SendAsync<WPUser>(HttpMethod.Get, "wp/v2/users/me");
        }
    }
}

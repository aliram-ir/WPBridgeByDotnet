using System.Text.Json;
using WPBridge.Client.Infrastructure;
using WPBridge.Client.WordPress.Models;

namespace WPBridge.Client.WordPress.Services
{
    /// <summary>
    /// Service for managing WordPress user data and profiles.
    /// </summary>
    public class WPUserService : BaseApiService
    {
        public WPUserService(HttpClient httpClient) : base(httpClient) { }

        /// <summary>
        /// Retrieves a paginated list of users.
        /// </summary>
        public async Task<List<WPUser>?> GetUsersListAsync()
        {
            return await SendAsync<List<WPUser>>(HttpMethod.Get, "wp/v2/users");
        }

        /// <summary>
        /// Retrieves a paginated list of users with paging parameters.
        /// </summary>
        public async Task<List<WPUser>?> GetUsersListPagingAsync(int page = 1, int perPage = 10)
        {
            return await SendAsync<List<WPUser>>(HttpMethod.Get, $"wp/v2/users?page={page}&per_page={perPage}");
        }

        /// <summary>
        /// Retrieves detailed information about a specific user.
        /// If you need metadata or protected fields, set useEditContext to true (requires Admin Login).
        /// </summary>
        public async Task<WPUser?> GetUserDetailsAsync(int userId, bool useEditContext = false)
        {
            string url = $"wp/v2/users/{userId}";
            if (useEditContext)
            {
                url += "?context=edit";
            }

            return await SendAsync<WPUser>(HttpMethod.Get, url);
        }


        /// <summary>
        /// Retrieves the profile of the currently authenticated user.
        /// </summary>
        public async Task<WPUser?> GetCurrentUserAsync(string token)
        {
            SetAuthToken(token);
            // 'me' endpoint automatically returns the current user's profile
            return await SendAsync<WPUser>(HttpMethod.Get, "wp/v2/users/me?context=edit");
        }

        /// <summary>
        /// Updates user profile or meta data.
        /// </summary>
        public async Task<WPUser?> UpdateUserAsync(int userId, object updateData)
        {
            return await SendAsync<WPUser>(HttpMethod.Post, $"wp/v2/users/{userId}", updateData);
        }
    }
}

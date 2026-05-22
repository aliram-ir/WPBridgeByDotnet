using WPBridge.Client.Infrastructure;
using WPBridge.Client.WordPress.Models;
using WPBridge.Client.WordPress.Mappers;

namespace WPBridge.Client.WordPress.Services
{
    /// <summary>
    /// Provides operations related to WordPress users.
    /// </summary>
    public class WPUserService : BaseApiService
    {
        public WPUserService(HttpClient httpClient) : base(httpClient)
        {
        }

        /// <summary>
        /// Retrieves a list of WordPress users.
        /// </summary>
        /// <param name="page">Page number.</param>
        /// <param name="perPage">Number of users per page.</param>
        public async Task<List<WPUser>?> GetUsersListAsync(int page = 1, int perPage = 20)
        {
            var users = await GetAsync<List<WPUser>>(
                $"/wp-json/wp/v2/users?page={page}&per_page={perPage}"
            );

            return users;
        }

        /// <summary>
        /// Retrieves all WordPress users.
        /// </summary>
        public async Task<List<WPUser>?> GetUsersListAsync()
        {
            var users = await GetAsync<List<WPUser>>(
                "/wp-json/wp/v2/users"
            );

            return users;
        }

        /// <summary>
        /// Retrieves a WordPress user by identifier.
        /// </summary>
        /// <param name="userId">WordPress user identifier.</param>
        public async Task<WPUser?> GetUserAsync(int userId)
        {
            if (userId <= 0)
                throw new ArgumentException("Invalid user id.", nameof(userId));

            return await GetAsync<WPUser>(
                $"/wp-json/wp/v2/users/{userId}"
            );
        }

        /// <summary>
        /// Retrieves the currently authenticated WordPress user.
        /// </summary>
        public async Task<WPUser?> GetCurrentUserAsync()
        {
            return await GetAsync<WPUser>(
                "/wp-json/wp/v2/users/me"
            );
        }

        /// <summary>
        /// Retrieves a WordPress user and maps meta data to WPUser model.
        /// </summary>
        /// <param name="userId">WordPress user identifier.</param>
        public async Task<WPUser?> GetUserWithMetaAsync(int userId)
        {
            if (userId <= 0)
                throw new ArgumentException("Invalid user id.", nameof(userId));

            var user = await GetAsync<WPUser>(
                $"/wp-json/wp/v2/users/{userId}"
            );

            if (user == null)
                return null;

            var meta = await GetAsync<Dictionary<string, string>>(
                $"/wp-json/wp/v2/users/{userId}/meta"
            );

            return WPUserMapper.Map(
                user.Id,
                user.Username,
                user.Name,
                user.Email,
                meta ?? new Dictionary<string, string>()
            );
        }

        /// <summary>
        /// Updates a WordPress user.
        /// </summary>
        /// <param name="userId">WordPress user identifier.</param>
        /// <param name="request">Updated user data.</param>
        public async Task<WPUser?> UpdateUserAsync(int userId, WPRegisterRequest request)
        {
            if (userId <= 0)
                throw new ArgumentException("Invalid user id.", nameof(userId));

            if (request == null)
                throw new ArgumentNullException(nameof(request));

            return await PutAsync<WPRegisterRequest, WPUser>(
                $"/wp-json/wp/v2/users/{userId}",
                request
            );
        }
    }
}

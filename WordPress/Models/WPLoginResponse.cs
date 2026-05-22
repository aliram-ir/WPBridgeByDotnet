namespace WPBridge.Client.WordPress.Models
{
    /// <summary>
    /// Represents the response returned after successful authentication.
    /// </summary>
    public class WPLoginResponse
    {
        /// <summary>
        /// JWT authentication token.
        /// </summary>
        public string Token { get; set; } = string.Empty;

        /// <summary>
        /// User email address.
        /// </summary>
        public string UserEmail { get; set; } = string.Empty;

        /// <summary>
        /// User display name.
        /// </summary>
        public string UserDisplayName { get; set; } = string.Empty;
    }
}

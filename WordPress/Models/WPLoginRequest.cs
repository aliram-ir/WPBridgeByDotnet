namespace WPBridge.Client.WordPress.Models
{
    /// <summary>
    /// Represents a login request for WordPress authentication.
    /// </summary>
    public class WPLoginRequest
    {
        /// <summary>
        /// WordPress username or email.
        /// </summary>
        public string Username { get; set; } = string.Empty;

        /// <summary>
        /// WordPress user password.
        /// </summary>
        public string Password { get; set; } = string.Empty;
    }
}

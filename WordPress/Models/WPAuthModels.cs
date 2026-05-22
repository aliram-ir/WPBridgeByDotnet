namespace WPBridge.Client.WordPress.Models
{
    public record WPLoginRequest(string Username, string Password);
    public record WPLoginResponse(string Token, string UserEmail, string UserNicename, string UserDisplayName);
}

namespace WPBridge.Client.WooCommerce.Models
{
    public class WooCustomerRequest
    {
        public string Email { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public WooAddress? Billing { get; set; }
        public WooAddress? Shipping { get; set; }
    }
}

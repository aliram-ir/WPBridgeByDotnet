namespace WPBridge.Client.WooCommerce.Models
{
    public class WooCustomer
    {
        public int Id { get; set; }
        public string Email { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public WooAddress Billing { get; set; } = new();
        public WooAddress Shipping { get; set; } = new();
        public List<WooMetaData> MetaData { get; set; } = new();
    }

    public class WooAddress
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Company { get; set; } = string.Empty;
        public string Address1 { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string Postcode { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
    }

    public class WooMetaData
    {
        public string Key { get; set; } = string.Empty;
        public object Value { get; set; } = string.Empty;
    }
}

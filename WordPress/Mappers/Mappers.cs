using WPBridge.Client.WooCommerce.Models;
using WPBridge.Client.WordPress.Models;

namespace WPBridge.Client.WordPress.Mappers
{
    /// <summary>
    /// Maps WordPress user meta data to WPUser model.
    /// </summary>
    public static class WPUserMapper
    {
        /// <summary>
        /// Maps wp_usermeta dictionary to WPUser object.
        /// </summary>
        public static WPUser Map(int id, string username, string name, string email, Dictionary<string, string> meta)
        {
            var user = new WPUser
            {
                Id = id,
                Username = username,
                Name = name,
                Email = email
            };

            if (meta == null)
                return user;

            user.Phone = Get(meta, "digits_phone") ?? Get(meta, "digits_phone_no") ?? string.Empty;

            user.Billing = new WooBillingAddress
            {
                FirstName = Get(meta, "billing_first_name"),
                LastName = Get(meta, "billing_last_name"),
                Company = Get(meta, "billing_company"),
                Address1 = Get(meta, "billing_address_1"),
                Address2 = Get(meta, "billing_address_2"),
                City = Get(meta, "billing_city"),
                State = Get(meta, "billing_state"),
                Postcode = Get(meta, "billing_postcode"),
                Country = Get(meta, "billing_country"),
                Email = Get(meta, "billing_email"),
                Phone = Get(meta, "billing_phone")
            };

            user.Shipping = new WooShippingAddress
            {
                FirstName = Get(meta, "shipping_first_name"),
                LastName = Get(meta, "shipping_last_name"),
                Company = Get(meta, "shipping_company"),
                Address1 = Get(meta, "shipping_address_1"),
                Address2 = Get(meta, "shipping_address_2"),
                City = Get(meta, "shipping_city"),
                State = Get(meta, "shipping_state"),
                Postcode = Get(meta, "shipping_postcode"),
                Country = Get(meta, "shipping_country")
            };

            return user;
        }

        /// <summary>
        /// Safely retrieves value from user meta dictionary.
        /// </summary>
        private static string Get(Dictionary<string, string> meta, string key)
        {
            if (meta.TryGetValue(key, out var value))
                return value ?? string.Empty;

            return string.Empty;
        }
    }
}

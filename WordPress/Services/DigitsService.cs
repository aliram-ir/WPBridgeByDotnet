using WPBridge.Client.Infrastructure;
using WPBridge.Client.WordPress.Models;

namespace WPBridge.Client.WordPress.Services
{
    /// <summary>
    /// Provides authentication operations using Digits plugin (OTP login/register).
    /// </summary>
    public class DigitsService : BaseApiService
    {
        public DigitsService(HttpClient httpClient) : base(httpClient)
        {
        }

        /// <summary>
        /// Sends an OTP code to the specified phone number using Digits plugin.
        /// </summary>
        /// <param name="phoneNumber">User phone number.</param>
        public async Task<bool> SendOtpAsync(string phoneNumber)
        {
            if (string.IsNullOrWhiteSpace(phoneNumber))
                throw new ArgumentException("Phone number is required.", nameof(phoneNumber));

            var response = await PostAsync<object, object>(
                "/wp-json/digits/v1/send_otp",
                new
                {
                    mobile = phoneNumber
                }
            );

            return response != null;
        }

        /// <summary>
        /// Verifies OTP and logs in the user using Digits plugin.
        /// If the user does not exist, Digits may create the account automatically.
        /// </summary>
        /// <param name="phoneNumber">User phone number.</param>
        /// <param name="otpCode">OTP code received by the user.</param>
        public async Task<WPLoginResponse?> VerifyOtpAsync(string phoneNumber, string otpCode)
        {
            if (string.IsNullOrWhiteSpace(phoneNumber))
                throw new ArgumentException("Phone number is required.", nameof(phoneNumber));

            if (string.IsNullOrWhiteSpace(otpCode))
                throw new ArgumentException("OTP code is required.", nameof(otpCode));

            return await PostAsync<object, WPLoginResponse>(
                "/wp-json/digits/v1/verify_otp",
                new
                {
                    mobile = phoneNumber,
                    otp = otpCode
                }
            );
        }

        /// <summary>
        /// Registers a new user using phone number via Digits plugin.
        /// </summary>
        /// <param name="phoneNumber">User phone number.</param>
        /// <param name="otpCode">OTP verification code.</param>
        /// <param name="username">Optional username.</param>
        /// <param name="email">Optional email.</param>
        public async Task<WPUser?> RegisterWithOtpAsync(string phoneNumber, string otpCode, string? username = null, string? email = null)
        {
            if (string.IsNullOrWhiteSpace(phoneNumber))
                throw new ArgumentException("Phone number is required.", nameof(phoneNumber));

            if (string.IsNullOrWhiteSpace(otpCode))
                throw new ArgumentException("OTP code is required.", nameof(otpCode));

            return await PostAsync<object, WPUser>(
                "/wp-json/digits/v1/register",
                new
                {
                    mobile = phoneNumber,
                    otp = otpCode,
                    username = username,
                    email = email
                }
            );
        }
    }
}

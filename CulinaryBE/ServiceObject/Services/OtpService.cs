using BusinessObject.Models.Dto;
using Microsoft.Extensions.Caching.Memory;
using ServiceObject.IServices;
using System.Security.Cryptography;
using System.Text;

namespace ServiceObject.Services
{
    public class OtpService : IOtpService
    {
        private readonly IMemoryCache _cache;

        public OtpService(IMemoryCache cache)
        {
            _cache = cache;
        }

        public string GenerateAndStoreOtp(string email, object? data, int expirySeconds)
        {
            var random = new Random();
            var otp = random.Next(100000, 999999).ToString();

            using var sha256 = SHA256.Create();
            var hash = Convert.ToBase64String(
                sha256.ComputeHash(Encoding.UTF8.GetBytes(otp))
            );

            _cache.Set($"OTP:{email}", new
            {
                OtpHash = hash,
                Data = data
            }, TimeSpan.FromSeconds(expirySeconds));

            return otp;
        }

        public (bool isValid, T? data) VerifyOtp<T>(string email, string otpInput)
        {
            if (!_cache.TryGetValue($"OTP:{email}", out dynamic? cacheData))
                return (false, default);

            string otpHashStored = cacheData.OtpHash;
            var data = (T)cacheData.Data;

            using var sha256 = SHA256.Create();
            var hashInput = Convert.ToBase64String(
                sha256.ComputeHash(Encoding.UTF8.GetBytes(otpInput))
            );

            return (otpHashStored == hashInput, data);
        }

        public void RemoveOtp(string email)
        {
            _cache.Remove($"OTP:{email}");
        }
    }
}

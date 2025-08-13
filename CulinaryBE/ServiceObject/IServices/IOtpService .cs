using BusinessObject.Models.Dto;

namespace ServiceObject.IServices
{
    public interface IOtpService
    {
        string GenerateAndStoreOtp(string email, object? data, int expirySeconds);
        (bool isValid, T? data) VerifyOtp<T>(string email, string otpInput);
        void RemoveOtp(string email);
    }
}

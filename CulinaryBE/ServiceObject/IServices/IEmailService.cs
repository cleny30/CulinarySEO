namespace ServiceObject.IServices
{
    public interface IEmailService
    {
        Task SendOtpEmailAsync(string email, string otp);
    }
}

using ServiceObject.Background.Queue;
using ServiceObject.IServices;

namespace ServiceObject.Services
{
    public class EmailService : IEmailService
    {
        private readonly IEmailQueue _emailQueue;

        public EmailService(IEmailQueue emailQueue)
        {
            _emailQueue = emailQueue;
        }

        public Task SendOtpEmailAsync(string email, string otp)
        {
            _emailQueue.Enqueue(email, otp);
            return Task.CompletedTask;
        }
    }
}
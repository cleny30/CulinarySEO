using BusinessObject.Models.Dto;
using Microsoft.Extensions.Options;
using ServiceObject.Background.Queue;
using ServiceObject.IServices;
using System.Reflection;
using System.Resources;

namespace ServiceObject.Services
{
    public class EmailService : IEmailService
    {
        private readonly EmailSetting _emailSettings;
        private readonly IEmailQueue _emailQueue;

        public EmailService(IOptions<EmailSetting> emailSettings, IEmailQueue emailQueue)
        {
            _emailSettings = emailSettings.Value;
            _emailQueue = emailQueue;
        }

        public Task SendOtpEmailAsync(string email, string otp)
        {
            string resxFilePath = "ServiceObject.Resources.EmailTemplates";
            var resourceManager = new ResourceManager(resxFilePath, Assembly.GetExecutingAssembly());

            string template = resourceManager.GetString("OtpEmailTemplate")!;
            string subject = resourceManager.GetString("EmailVerificationSubject")!;
            string supportEmail = resourceManager.GetString("SupportEmail")!;

            string body = template
                .Replace("{Date}", DateTime.UtcNow.ToString("dd/MM/yyyy"))
                .Replace("{OtpCode}", otp)
                .Replace("{SupportEmail}", supportEmail);

            // Chỉ enqueue, không gửi ngay
            _emailQueue.Enqueue(new EmailQueueItem(email, subject, body));

            return Task.CompletedTask;
        }
    }
}
using BusinessObject.Models.Dto;
using Microsoft.Extensions.Options;
using ServiceObject.IServices;
using System.Net;
using System.Net.Mail;
using System.Reflection;
using System.Resources;

namespace ServiceObject.Services
{
    public class EmailService : IEmailService
    {
        private readonly EmailSetting _emailSettings;

        public EmailService(IOptions<EmailSetting> emailSettings)
        {
            _emailSettings = emailSettings.Value;
        }


        public async Task SendOtpEmailAsync(string email, string otp)
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

            await SendEmailAsync(email, subject!, body);
        }

        private async Task SendEmailAsync(string toEmail, string subject, string body)
        {
            using var smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential(_emailSettings.Email, _emailSettings.Password),
                EnableSsl = true,
            };

            using var mailMessage = new MailMessage
            {
                From = new MailAddress(_emailSettings.Email),
                Subject = subject,
                Body = body,
                IsBodyHtml = true,
            };
            mailMessage.To.Add(toEmail);

            try
            {
                await smtpClient.SendMailAsync(mailMessage);
            }
            catch (Exception ex)
            {
                // log ex
                throw new InvalidOperationException("Gửi email thất bại", ex);
            }


        }
    }
}

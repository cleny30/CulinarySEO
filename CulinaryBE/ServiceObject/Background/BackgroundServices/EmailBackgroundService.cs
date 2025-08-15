using BusinessObject.Models.Dto;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ServiceObject.Background.Queue;
using ServiceObject.IServices;
using System.Net;
using System.Net.Mail;
using System.Reflection;
using System.Resources;

namespace ServiceObject.Background.BackgroundServices
{
    public class EmailBackgroundService : BackgroundService
    {
        private readonly ILogger<EmailBackgroundService> _logger;
        private readonly IServiceProvider _serviceProvider;
        private readonly IEmailQueue _queue;

        public EmailBackgroundService(ILogger<EmailBackgroundService> logger, IServiceProvider serviceProvider, IEmailQueue queue)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;
            _queue = queue;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("EmailBackgroundService started.");

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    if (_queue.TryDequeue(out var emailItem))
                    {
                        using var scope = _serviceProvider.CreateScope();
                        var emailSettings = scope.ServiceProvider.GetRequiredService<IOptions<EmailSetting>>().Value;

                        // Tạo template email
                        string resxFilePath = "ServiceObject.Resources.EmailTemplates";
                        var resourceManager = new ResourceManager(resxFilePath, Assembly.GetExecutingAssembly());

                        string template = resourceManager.GetString("OtpEmailTemplate")!;
                        string subject = resourceManager.GetString("EmailVerificationSubject")!;
                        string supportEmail = resourceManager.GetString("SupportEmail")!;

                        string body = template
                            .Replace("{Date}", DateTime.UtcNow.ToString("dd/MM/yyyy"))
                            .Replace("{OtpCode}", emailItem.Otp)
                            .Replace("{SupportEmail}", supportEmail);

                        // Gửi email SMTP
                        await SendEmailAsync(emailItem.ToEmail, subject, body, emailSettings);

                        _logger.LogInformation("Sent OTP email to {Email}", emailItem.ToEmail);
                    }
                    else
                    {
                        await Task.Delay(500, stoppingToken);
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error sending email in background");
                    await Task.Delay(2000, stoppingToken);
                }
            }
        }

        private async Task SendEmailAsync(string toEmail, string subject, string body, EmailSetting settings)
        {
            using var smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential(settings.Email, settings.Password),
                EnableSsl = true,
            };

            using var mailMessage = new MailMessage
            {
                From = new MailAddress(settings.Email),
                Subject = subject,
                Body = body,
                IsBodyHtml = true,
            };
            mailMessage.To.Add(toEmail);

            await smtpClient.SendMailAsync(mailMessage);
        }
    }
}
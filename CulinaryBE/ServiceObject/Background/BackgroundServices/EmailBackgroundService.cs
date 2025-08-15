using BusinessObject.Models.Dto;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ServiceObject.Background.Queue;
using System.Net;
using System.Net.Mail;

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

                        await SendEmailAsync(emailItem, emailSettings);
                        _logger.LogInformation("Sent email to {Email}", emailItem.ToEmail);
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

        private async Task SendEmailAsync(EmailQueueItem emailItem, EmailSetting settings)
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
                Subject = emailItem.Subject,
                Body = emailItem.Body,
                IsBodyHtml = true,
            };
            mailMessage.To.Add(emailItem.ToEmail);

            await smtpClient.SendMailAsync(mailMessage);
        }
    }
}
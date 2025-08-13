using BusinessObject.Models.Enum;
using DataAccess.IDAOs;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using ServiceObject.Background.Queue;
using Microsoft.Extensions.Hosting;


namespace ServiceObject.Background.BackgroundServices
{
    public class LogoutBackgroundService : BackgroundService
    {
        private readonly ILogger<LogoutBackgroundService> _logger;
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogoutQueue _queue;

        public LogoutBackgroundService(ILogger<LogoutBackgroundService> logger, IServiceProvider serviceProvider, ILogoutQueue queue)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;
            _queue = queue;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("LogoutBackgroundService started.");

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    if (_queue.TryDequeue(out var logoutData))
                    {
                        using var scope = _serviceProvider.CreateScope();
                        switch (logoutData.AccountType)
                        {
                            case AccountType.Manager:
                                var managerDAO = scope.ServiceProvider.GetRequiredService<IManagerDAO>();
                                await managerDAO.RevokeRefreshTokenAsync(logoutData.RefreshToken);
                                break;
                            case AccountType.Customer:
                                var customerDAO = scope.ServiceProvider.GetRequiredService<ICustomerDAO>();
                                await customerDAO.RevokeRefreshTokenAsync(logoutData.RefreshToken);
                                break;
                        }
                        _logger.LogInformation("Revoked refresh token for {AccountType}", logoutData.AccountType);
                    }
                    else
                    {
                        await Task.Delay(500, stoppingToken);
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error processing logout in background");
                    await Task.Delay(2000, stoppingToken);
                }
            }
        }
    }

}

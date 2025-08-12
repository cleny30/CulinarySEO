using BusinessObject.Models.Enum;
using DataAccess.IDAOs;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace ServiceObject.Background
{
    public class TokenSaveBackgroundService : BackgroundService
    {
        private readonly ILogger<TokenSaveBackgroundService> _logger;
        private readonly IServiceProvider _serviceProvider;
        private readonly ITokenSaveQueue _queue;

        public TokenSaveBackgroundService(ILogger<TokenSaveBackgroundService> logger, IServiceProvider serviceProvider, ITokenSaveQueue queue)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;
            _queue = queue;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("TokenSaveBackgroundService started.");

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    if (_queue.TryDequeue(out var tokenData))
                    {
                        using var scope = _serviceProvider.CreateScope();

                        switch (tokenData.AccountType)
                        {
                            case AccountType.Manager:
                                var managerDao = scope.ServiceProvider.GetRequiredService<IManagerDAO>();
                                await managerDao.SaveRefreshTokenAsync(tokenData.UserId, tokenData.RefreshToken, tokenData.Expiry);
                                break;

                            case AccountType.Customer:
                                var customerDao = scope.ServiceProvider.GetRequiredService<ICustomerDAO>();
                                await customerDao.SaveRefreshTokenAsync(tokenData.UserId, tokenData.RefreshToken, tokenData.Expiry);
                                break;
                        }

                        _logger.LogInformation("Saved refresh token for user {UserId} ({AccountType})",
                            tokenData.UserId, tokenData.AccountType);
                    }
                    else
                    {
                        await Task.Delay(500, stoppingToken);
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error saving refresh token in background");
                    await Task.Delay(2000, stoppingToken);
                }
            }
        }

    }
}

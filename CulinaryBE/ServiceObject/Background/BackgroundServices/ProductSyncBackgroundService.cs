using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ServiceObject.Background.Queue;
using ServiceObject.IServices;

namespace ServiceObject.Background.BackgroundServices
{
    public class ProductSyncBackgroundService : BackgroundService
    {
        private readonly ILogger<ProductSyncBackgroundService> _logger;
        private readonly IProductSyncQueue _queue;
        private readonly IServiceProvider _serviceProvider;

        public ProductSyncBackgroundService(ILogger<ProductSyncBackgroundService> logger, IProductSyncQueue queue, IServiceProvider serviceProvider)
        {
            _logger = logger;
            _queue = queue;
            _serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("ProductSyncBackgroundService started.");

            while (!stoppingToken.IsCancellationRequested)
            {
                if (_queue.TryDequeue(out var syncEvent))
                {
                    using var scope = _serviceProvider.CreateScope();
                    var elasticService = scope.ServiceProvider.GetRequiredService<IElasticService>();

                    try
                    {
                        if (syncEvent.Action == "index" && syncEvent.ProductId.HasValue)
                        {
                            await elasticService.IndexProductAsync(syncEvent.ProductId.Value);
                        }
                        else if (syncEvent.Action == "delete" && syncEvent.ProductId.HasValue)
                        {
                            await elasticService.DeleteProductAsync(syncEvent.ProductId.Value);
                        }
                        else if (syncEvent.Action == "all")
                        {
                            await elasticService.ReindexAllAsync();
                        }
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "Error sync product in background");
                    }
                }
                else
                {
                    // Nếu queue rỗng thì nghỉ 500ms để không tốn CPU
                    await Task.Delay(500, stoppingToken);
                }
            }
        }
    }
}

using BusinessObject.AppDbContext;
using DataAccess.IDAOs;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DataAccess.DAOs
{
    public class StockDAO : IStockDAO
    {
        private readonly CulinaryContext _context;
        private readonly ILogger<StockDAO> _logger;

        public StockDAO(CulinaryContext context, ILogger<StockDAO> logger)
        {
            _context = context;
            _logger = logger;
        }
        public async Task<int> GetTotalStockByProductAsync(Guid productId)
        {
            try
            {
                return await _context.Stocks
                    .Where(s => s.ProductId == productId)
                    .SumAsync(s => s.Quantity);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching stock for product {ProductId}", productId);
                throw;
            }
        }
    }
}

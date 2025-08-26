using BusinessObject.AppDbContext;
using BusinessObject.Models.Entity;
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
        public async Task<List<Stock>> GetStocksByProductAsync(Guid productId)
        {
            try
            {
                return await _context.Stocks
                    .Where(s => s.ProductId == productId && s.Quantity > 0)
                    .OrderBy(s => s.WarehouseId)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching stocks for product {ProductId}", productId);
                throw;
            }
        }
        public async Task<Stock?> GetStockAsync(Guid productId, Guid warehouseId)
        {
            return await _context.Stocks
                .FirstOrDefaultAsync(s => s.ProductId == productId && s.WarehouseId == warehouseId);
        }

        public async Task UpdateStocksAsync(IEnumerable<Stock> stocks)
        {
            _context.Stocks.UpdateRange(stocks);
            await _context.SaveChangesAsync();
        }
    }
}

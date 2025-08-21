using BusinessObject.Models.Entity;

namespace DataAccess.IDAOs
{
    public interface IStockDAO
    {
        Task<int> GetTotalStockByProductAsync(Guid productId);
        Task<Stock?> GetStockAsync(Guid productId, Guid warehouseId);
        Task UpdateStocksAsync(IEnumerable<Stock> stocks);
    }
}

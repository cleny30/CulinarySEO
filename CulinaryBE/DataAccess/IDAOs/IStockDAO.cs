namespace DataAccess.IDAOs
{
    public interface IStockDAO
    {
        Task<int> GetTotalStockByProductAsync(Guid productId);
    }
}

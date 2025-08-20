using BusinessObject.Models.Entity;

namespace DataAccess.IDAOs
{
    public interface IProductDAO
    {
        Task<List<Product>> GetAllProducts();
        Task<Product?> GetProductDetailById(Guid productId);
        Task<List<Product>> GetBestSellingProducts(int topN);
        Task<List<Product>> GetProductSummariesById(IEnumerable<Guid> productIds);
    }
}
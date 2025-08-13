using BusinessObject.Models.Entity;

namespace DataAccess.IDAOs
{
    public interface IProductDAO
    {
        Task<List<Product>> GetAllProducts();
        Task<Product?> GetProductById(Guid productId);
    }
}
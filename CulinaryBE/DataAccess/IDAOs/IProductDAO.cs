using BusinessObject.Models.Entity;

namespace DataAccess.IDAOs
{
    public interface IProductDAO
    {
        Task<List<Product>> GetAllProducts();
    }
}
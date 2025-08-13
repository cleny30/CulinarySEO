using BusinessObject.Models.Dto;
using BusinessObject.Models.Entity;

namespace DataAccess.IDAOs
{
    public interface IProductDAO
    {
        Task<List<Product>> GetAllProducts();
        Task<Product?> GetProductById(Guid productId);
        Task<(int TotalItems, List<Product> Items)> GetFilteredProductsAsync(ProductFilterRequest request);

    }
}
using BusinessObject.Models.Entity;

namespace DataAccess.IDAOs
{
    public interface ICartDAO
    {
        Task<Cart> GetCartDataAsync(Guid customerId);
    }
}

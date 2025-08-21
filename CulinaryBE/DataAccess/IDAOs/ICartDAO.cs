using BusinessObject.Models.Entity;

namespace DataAccess.IDAOs
{
    public interface ICartDAO
    {
        Task<Cart> GetCartDataAsync(Guid customerId);
        Task<Cart?> GetCartByIdAsync(Guid cartId);
        Task<CartItem?> GetCartItemByIdAsync(Guid cartItemId);
        Task<CartItem?> GetCartItemByProductAsync(Guid cartId, Guid productId);
        Task AddCartItemAsync(CartItem cartItem);
        Task UpdateCartItemAsync(CartItem cartItem);
        Task RemoveCartItemAsync(CartItem cartItem);
        Task DeleteCartAsync(Guid cartId);
    }
}

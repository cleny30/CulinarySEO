using BusinessObject.Models.Dto;

namespace ServiceObject.IServices
{
    public interface ICartService
    {
        Task<CartDto> GetCartDataAsync(Guid customerId); 
        Task AddToCartAsync(AddToCartRequest reqyest);
        Task UpdateCartItemAsync(UpdateCartItemRequest request);
        Task RemoveFromCartAsync(Guid cartItemId);
    }
}

using BusinessObject.Models.Dto;

namespace ServiceObject.IServices
{
    public interface ICartService
    {
        Task<CartDto> GetCartDataAsync(Guid customerId);
    }
}

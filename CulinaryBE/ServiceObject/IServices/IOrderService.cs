using BusinessObject.Models.Dto;

namespace ServiceObject.IServices
{
    public interface IOrderService
    {
        Task<CheckoutResponseDto> CheckoutAsync(CheckoutRequestDto request);
    }
}

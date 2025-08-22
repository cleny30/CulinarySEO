using BusinessObject.Models;
using BusinessObject.Models.Dto;
using Microsoft.AspNetCore.Mvc;
using ServiceObject.IServices;

namespace CulinaryAPI.Controllers
{
    [Route("api/order")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost("checkout")]
        public async Task<IActionResult> Checkout([FromBody] CheckoutRequestDto request)
        {
            var result = await _orderService.CheckoutAsync(request);
            return Ok(new ApiResponse
            {
                IsSuccess = true,
                Message = "Checkout successful",
                Result = result
            });
        }
    }
}

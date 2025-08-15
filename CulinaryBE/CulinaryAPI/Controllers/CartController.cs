using BusinessObject.Models;
using Microsoft.AspNetCore.Mvc;
using ServiceObject.IServices;

namespace CulinaryAPI.Controllers
{
    [Route("api/cart")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;

        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }

        [HttpGet("cart-data")]
        public async Task<IActionResult> GetCartData([FromQuery] Guid customerId)
        {
            var cart = await _cartService.GetCartDataAsync(customerId);

            return Ok(new ApiResponse
            {
                IsSuccess = true,
                Message = "Get cart data sucessfull",
                Result = cart,
            });
        }
    }
}

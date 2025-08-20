using BusinessObject.Models;
using BusinessObject.Models.Dto;
using Microsoft.AspNetCore.Authorization;
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
        [Authorize]
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

        [HttpPost("add-to-cart")]
        public async Task<IActionResult> AddToCart([FromBody] AddToCartRequest request)
        {
            await _cartService.AddToCartAsync(request);

            return Ok(new ApiResponse
            {
                Message = "Add product to cart sucessfull!"
            });
        }

        [HttpPut("update-cart-item")]
        public async Task<IActionResult> UpdateCartItem([FromBody] UpdateCartItemRequest request)
        {
            await _cartService.UpdateCartItemAsync(request);
            return Ok(new { message = "Cart item updated" });
        }

        [HttpDelete("remove-cart-item")]
        public async Task<IActionResult> RemoveFromCart(Guid cartItemId)
        {
            await _cartService.RemoveFromCartAsync(cartItemId);
            return Ok(new { message = "Cart item removed" });
        }
    }
}

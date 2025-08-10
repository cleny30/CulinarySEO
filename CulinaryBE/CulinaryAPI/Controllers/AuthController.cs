using BusinessObject.Models;
using BusinessObject.Models.Dto;
using BusinessObject.Models.Dto.Auth;
using Microsoft.AspNetCore.Mvc;
using ServiceObject.IServices;

namespace CulinaryAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login-manager")]
        public async Task<IActionResult> VerifyManager([FromBody] LoginAccountModel loginAccountModel)
        {
            var apiResponse = new ApiResponse();

            var response = await _authService.VerifyManager(loginAccountModel);

            return Ok(new ApiResponse
            {
                IsSuccess = true,
                Message = "Login successful",
                Result = response
            });
        }

        [HttpPost("login-customer")]
        public async Task<IActionResult> VerifyCustomer([FromBody] LoginAccountModel loginAccountModel)
        {
            var apiResponse = new ApiResponse();

            var response = await _authService.VerifyCustomer(loginAccountModel);

            return Ok(new ApiResponse
            {
                IsSuccess = true,
                Message = "Login successful",
                Result = response
            });
        }

        [HttpPost("refresh-token-manager")]
        public async Task<IActionResult> RefreshTokenManager([FromBody] RefreshTokenRequest request)
        {
            var response = await _authService.RefreshTokenManagerAsync(request.AccessToken, request.RefreshToken);
            return Ok(new ApiResponse
            {
                IsSuccess = true,
                Message = "Token refreshed successfully",
                Result = response
            });
        }

        [HttpPost("refresh-token-customer")]
        public async Task<IActionResult> RefreshTokenCustomer([FromBody] RefreshTokenRequest request)
        {
            var response = await _authService.RefreshTokenCustomerAsync(request.AccessToken, request.RefreshToken);
            return Ok(new ApiResponse
            {
                IsSuccess = true,
                Message = "Token refreshed successfully",
                Result = response
            });
        }
    }
}

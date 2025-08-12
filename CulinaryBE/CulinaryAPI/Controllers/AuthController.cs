using BusinessObject.Models;
using BusinessObject.Models.Dto;
using Microsoft.AspNetCore.Mvc;
using ServiceObject.IServices;

namespace CulinaryAPI.Controllers
{
    [Route("api/auth")]
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

            if (response == null || string.IsNullOrEmpty(response.AccessToken) || string.IsNullOrEmpty(response.RefreshToken))
            {
                return Unauthorized(new ApiResponse
                {
                    IsSuccess = false,
                    Message = "Invalid credentials"
                });
            }

            Response.Cookies.Append("AccessToken", response.AccessToken, new CookieOptions
            {
                HttpOnly = true,
                Secure = true, // Set base on develop or producttion
                SameSite = SameSiteMode.None,
                Expires = DateTime.UtcNow.AddSeconds(response.ExpiresIn)
            });

            Response.Cookies.Append("RefreshToken", response.RefreshToken, new CookieOptions
            {
                HttpOnly = true,
                Secure = true, //Set base on develop or producttion
                SameSite = SameSiteMode.None,
                Expires = DateTime.UtcNow.AddDays(7) // Refresh token sống lâu hơn
            });

            return Ok(new ApiResponse
            {
                IsSuccess = true,
                Message = "Login successful",
                Result = response.AccountData
            });
        }

        [HttpPost("login-customer")]
        public async Task<IActionResult> VerifyCustomer([FromBody] LoginAccountModel loginAccountModel)
        {
            var apiResponse = new ApiResponse();

            var response = await _authService.VerifyCustomer(loginAccountModel);

            if (response == null || string.IsNullOrEmpty(response.AccessToken) || string.IsNullOrEmpty(response.RefreshToken))
            {
                return Unauthorized(new ApiResponse
                {
                    IsSuccess = false,
                    Message = "Invalid credentials"
                });
            }

            Response.Cookies.Append("AccessToken", response.AccessToken, new CookieOptions
            {
                HttpOnly = true,
                Secure = true, // Set base on develop or producttion
                SameSite = SameSiteMode.None,
                Expires = DateTime.UtcNow.AddSeconds(response.ExpiresIn)
            });

            Response.Cookies.Append("RefreshToken", response.RefreshToken, new CookieOptions
            {
                HttpOnly = true,
                Secure = true, //Set base on develop or producttion
                SameSite = SameSiteMode.None,
                Expires = DateTime.UtcNow.AddDays(7)
            });

            return Ok(new ApiResponse
            {
                IsSuccess = true,
                Message = "Login successful",
                Result = response.AccountData
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

        [HttpPost("register")]
        public async Task<IActionResult> CustomerRegister()
        {
            return Ok();
        }

        [HttpPost("logout-manager")]
        public async Task<IActionResult> LogoutManager()
        {

            var refreshToken = Request.Cookies["RefreshToken"];
            if (string.IsNullOrEmpty(refreshToken))
            {
                return Unauthorized(new ApiResponse
                {
                    IsSuccess = false,
                    Message = "Missing refresh token",
                    Error = "ERR_INVALID_TOKEN"
                });
            }
            await _authService.LogoutManagerAsync(refreshToken);
            // Xóa cookies
            Response.Cookies.Delete("AccessToken");
            Response.Cookies.Delete("RefreshToken");

            return Ok(new ApiResponse
            {
                IsSuccess = true,
                Message = "Logout successful",
                Result = null
            });
        }

        [HttpPost("logout-customer")]
        public async Task<IActionResult> LogoutCustomer()
        {
            var refreshToken = Request.Cookies["RefreshToken"];
            if (string.IsNullOrEmpty(refreshToken))
            {
                return Unauthorized(new ApiResponse
                {
                    IsSuccess = false,
                    Message = "Missing refresh token",
                    Error = "ERR_INVALID_TOKEN"
                });
            }
            // Xóa cookies
            Response.Cookies.Delete("AccessToken", new CookieOptions
            {
                Secure = true,
                SameSite = SameSiteMode.None,
            });
            Response.Cookies.Delete("RefreshToken", new CookieOptions
            {
                Secure = true,
                SameSite = SameSiteMode.None,
            });

            await _authService.LogoutCustomerAsync(refreshToken);

            return Ok(new ApiResponse
            {
                IsSuccess = true,
                Message = "Logout successful",
                Result = null
            });
        }
    }
}

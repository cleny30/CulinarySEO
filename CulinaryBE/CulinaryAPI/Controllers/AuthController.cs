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
        private readonly ICustomerService _customerService;
        private readonly IOtpService _otpService;
        private readonly IEmailService _emailService;
        public AuthController(IAuthService authService, ICustomerService customerService, IOtpService otpService, IEmailService emailService)
        {
            _authService = authService;
            _customerService = customerService;
            _otpService = otpService;
            _emailService = emailService;
        }

        [HttpPost("login-manager")]
        public async Task<IActionResult> VerifyManager([FromBody] LoginAccountModel loginAccountModel)
        {
            var response = await _authService.VerifyManager(loginAccountModel);
            return HandleLoginResponse(response);
        }

        [HttpPost("login-customer")]
        public async Task<IActionResult> VerifyCustomer([FromBody] LoginAccountModel loginAccountModel)
        {
            var response = await _authService.VerifyCustomer(loginAccountModel);
            return HandleLoginResponse(response);
        }

        [HttpPost("google-login")]
        public async Task<IActionResult> GoogleLogin([FromBody] GoogleLoginRequest request)
        {
            var response = await _authService.VerifyGoogle(request);
            return HandleLoginResponse(response);
        }

        [HttpPost("refresh-token-manager")]
        public async Task<IActionResult> RefreshTokenManager([FromBody] RefreshTokenRequest request)
        {
            var response = await _authService.RefreshTokenManagerAsync(request.AccessToken, request.RefreshToken);
            return HandleRefreshTokenResponse(response);
        }

        [HttpPost("refresh-token-customer")]
        public async Task<IActionResult> RefreshTokenCustomer([FromBody] RefreshTokenRequest request)
        {
            var response = await _authService.RefreshTokenCustomerAsync(request.AccessToken, request.RefreshToken);
            return HandleRefreshTokenResponse(response);
        }

        [HttpPost("logout-manager")]
        public async Task<IActionResult> LogoutManager()
        {
            return await HandleLogoutAsync(async () =>
            {
                var refreshToken = Request.Cookies["RefreshToken"];
                await _authService.LogoutManagerAsync(refreshToken);
            });
        }

        [HttpPost("logout-customer")]
        public async Task<IActionResult> LogoutCustomer()
        {
            return await HandleLogoutAsync(async () =>
            {
                var refreshToken = Request.Cookies["RefreshToken"];
                await _authService.LogoutCustomerAsync(refreshToken);
            });
        }


        [HttpPost("register")]
        public async Task<IActionResult> CustomerRegister([FromBody] RegisterCustomerRequest registerData)
        {
            bool isEmailExist = await _customerService.IsEmailExist(registerData.Email);
            if (isEmailExist)
            {
                return BadRequest(new ApiResponse
                {
                    IsSuccess = false,
                    Message = "Email already used",
                });
            }

            string otp = _otpService.GenerateAndStoreOtp(registerData.Email, registerData, 30);

            await _emailService.SendOtpEmailAsync(registerData.Email, otp);
            return Ok(new ApiResponse
            {
                IsSuccess = true,
                Message = "Send OTP to guess email"
            });
        }

        [HttpPost("resend-otp-register")]
        public async Task<IActionResult> ReSendOtpRegister([FromBody] RegisterCustomerRequest registerData)
        {
            string otp = _otpService.GenerateAndStoreOtp(registerData.Email, registerData, 30);

            await _emailService.SendOtpEmailAsync(registerData.Email, otp);
            return Ok(new ApiResponse
            {
                IsSuccess = true,
                Message = "Re send OTP to guess email"
            });
        }

        [HttpPost("verify-otp-register")]
        public async Task<IActionResult> VerifyOtpRegister([FromBody] OtpVerifyModel model)
        {
            var (isValid, registerData) = _otpService.VerifyOtp<RegisterCustomerRequest>(model.Email, model.Otp);

            if (!isValid || registerData == null)
                return BadRequest(new ApiResponse
                {
                    IsSuccess = false,
                    Message = "OTP is invalid or expired"
                });

            _otpService.RemoveOtp(model.Email);

            var result = await _customerService.AddNewCustomer(registerData);

            if (!result)
            {
                return BadRequest(new ApiResponse
                {
                    IsSuccess = false,
                    Message = "Failed to register customer"
                });
            }

            return Ok(new ApiResponse
            {
                IsSuccess = true,
                Message = "Register sucessfull"
            });
        }
        
        #region controller helper method
        private IActionResult HandleLoginResponse(LoginResponse response)
        {
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
                Secure = true, // Set base on develop or production
                SameSite = SameSiteMode.None,
                Expires = DateTime.UtcNow.AddSeconds(response.ExpiresIn),
                Path = "/"
            });

            Response.Cookies.Append("RefreshToken", response.RefreshToken, new CookieOptions
            {
                HttpOnly = true,
                Secure = true, // Set base on develop or production
                SameSite = SameSiteMode.None,
                Expires = DateTime.UtcNow.AddDays(7),
                Path = "/"
            });

            return Ok(new ApiResponse
            {
                IsSuccess = true,
                Message = "Login successful",
                Result = response.AccountData
            });
        }

        private IActionResult HandleRefreshTokenResponse(LoginResponse response)
        {
            return Ok(new ApiResponse
            {
                IsSuccess = true,
                Message = "Token refreshed successfully",
                Result = response
            });
        }

        private async Task<IActionResult> HandleLogoutAsync(Func<Task> logoutAction)
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

            await logoutAction();

            // Xóa cookies
            Response.Cookies.Delete("AccessToken", new CookieOptions
            {
                Secure = true,
                SameSite = SameSiteMode.None,
                Path = "/"
            });
            Response.Cookies.Delete("RefreshToken", new CookieOptions
            {
                Secure = true,
                SameSite = SameSiteMode.None,
                Path = "/"
            });

            return Ok(new ApiResponse
            {
                IsSuccess = true,
                Message = "Logout successful"
            });
        }

        #endregion
    }
}

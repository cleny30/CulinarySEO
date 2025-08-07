using BusinessObject.Models;
using BusinessObject.Models.Dto;
using Microsoft.AspNetCore.Mvc;
using ServiceObject.IServices;

namespace CulinaryAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService authService;
        private readonly IJwtService jwtService;

        public AuthController(IAuthService authService, IJwtService jwtService)
        {
            this.authService = authService;
            this.jwtService = jwtService;
        }

        [HttpPost]
        public async Task<IActionResult> VerifyManager([FromBody] LoginAccountModel loginAccountModel)
        {
            var apiResponse = new ApiResponse();

            var manager = await authService.VerifyManager(loginAccountModel);

            var token = await jwtService.GenerateJwtToken(manager);

            apiResponse.Result = token;
            apiResponse.Message = "Login successful";

            return Ok(apiResponse);
        }
    }
}

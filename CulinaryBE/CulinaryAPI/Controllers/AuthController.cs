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

        public AuthController(IAuthService authService)
        {
            this.authService = authService;
        }

        [HttpPost]
        public async Task<IActionResult> VerifyManager([FromBody] LoginAccountModel loginAccountModel)
        {
            var apiResponse = new ApiResponse();

            var manager = await authService.VerifyManager(loginAccountModel);

            apiResponse.Result = manager;

            return Ok(apiResponse);
        }
    }
}

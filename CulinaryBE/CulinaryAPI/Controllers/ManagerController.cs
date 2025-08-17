using BusinessObject.Models;
using BusinessObject.Models.Dto;
using BusinessObject.Models.Enum;
using CulinaryAPI.Middleware.Authentication;
using Microsoft.AspNetCore.Mvc;
using ServiceObject.IServices;

namespace CulinaryAPI.Controllers
{
    [Route("api/manager")]
    [ApiController]
    public class ManagerController : ControllerBase
    {
        private readonly IManagerService _managerService;

        public ManagerController(IManagerService managerService)
        {
            _managerService = managerService;
        }

        [HttpPost("add-manager")]
        [HasPermission(PermissionAuth.ManageStaffAccount)]
        public async Task<IActionResult> AddManager([FromBody] AddManagerDto addManagerDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Your ìnormation is invalid.");
            }
            try
            {
                ApiResponse response = new ApiResponse();
                response.IsSuccess = await _managerService.AddManager(addManagerDto);
                response.Message = response.IsSuccess ? "Manager added successfully!" : "Email existed in our system!";
                return response.IsSuccess ? Ok(response) : BadRequest(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }
    }
}

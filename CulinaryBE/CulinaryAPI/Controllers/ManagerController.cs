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
        public async Task<IActionResult> AddManager([FromBody] ManagerDto addManagerDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Your ìnormation is invalid.");
            }
            ApiResponse response = new ApiResponse();
            response.IsSuccess = await _managerService.AddManager(addManagerDto);
            response.Message = response.IsSuccess ? "Manager added successfully!" : "Email existed in our system!";
            return response.IsSuccess ? Ok(response) : BadRequest(response);

        }

        [HttpPut("update-manager")]
        [HasPermission(PermissionAuth.ManageStaffAccount)]
        public async Task<IActionResult> UpdateManager([FromBody] ManagerDto updateManagerDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Your ìnormation is invalid.");
            }

            ApiResponse response = new ApiResponse();
            response.IsSuccess = await _managerService.UpdateManager(updateManagerDto);
            response.Message = response.IsSuccess ? "Manager updated successfully!" : "Email existed in our system!";
            return response.IsSuccess ? Ok(response) : BadRequest(response);
        }

        [HttpDelete("delete-manager")]
        [HasPermission(PermissionAuth.ManageStaffAccount)]
        public async Task<IActionResult> DeleteManager([FromQuery] Guid managerId)
        {

            ApiResponse response = new ApiResponse();
            response.IsSuccess = await _managerService.DeleteManager(managerId);
            response.Message = response.IsSuccess ? "Manager deleted successfully!" : "Cannot found ID in our system!";
            return response.IsSuccess ? Ok(response) : BadRequest(response);
        }

        [HttpGet("id")]
        [HasPermission(PermissionAuth.ManageStaffAccount)]
        public async Task<IActionResult> GetManagerByID([FromQuery] Guid managerId)
        {
            ApiResponse response = new ApiResponse();
            response.Result = await _managerService.GetManagerById(managerId);
            return Ok(response);
        }

        [HttpGet]
        [HasPermission(PermissionAuth.ManageStaffAccount)]
        public async Task<IActionResult> GetManagers()
        {
            ApiResponse response = new ApiResponse();
            response.Result = await _managerService.GetManagers();
            return Ok(response);
        }
    }
}

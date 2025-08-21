using BusinessObject.Models;
using BusinessObject.Models.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceObject.IServices;

namespace CulinaryAPI.Controllers
{
    [Route("api/role")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _roleService;

        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        [HttpGet]
        public async Task<IActionResult> GetRoles()
        {
            try
            {
                ApiResponse response = new ApiResponse();
                response.Result = await _roleService.GetRoles();
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("{roleId}")]
        public async Task<IActionResult> GetRoleById(int roleId)
        {
            try
            {
                ApiResponse response = new ApiResponse();
                response.Result = await _roleService.GetRoleById(roleId);
                if (response.Result == null)
                {
                    return NotFound($"Role with ID {roleId} not found.");
                }
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost("add-role")]
        [Authorize]
        public async Task<IActionResult> AddRole([FromBody] RoleDto roleDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid is incorect.");
            }
            try
            {
                ApiResponse response = new ApiResponse();
                await _roleService.AddRole(roleDto);
                response.Message = "Role added successfully.";
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut("update-role")]
        [Authorize]
        public async Task<IActionResult> UpdateRole([FromBody] RoleDto roleDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid data.");
            }
            try
            {
                ApiResponse response = new ApiResponse();
                await _roleService.UpdateRole(roleDto);
                response.Message = "Role updated successfully.";
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("export-roles-permission")]
        [Authorize]
        public async Task<IActionResult> ExportRolesPermission()
        {
            try
            {
                byte[] result = await _roleService.ExportRolesPermission();
                string fileName = $"Roles_Permissions_{DateTime.Now:ddMMyyyyHHmmss}.xlsx";
                return File(result, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}

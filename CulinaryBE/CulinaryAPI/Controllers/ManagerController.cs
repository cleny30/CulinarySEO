using BusinessObject.Models;
using BusinessObject.Models.Dto;
using BusinessObject.Models.Enum;
using Microsoft.AspNetCore.Authorization;
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
        [Authorize]
        public async Task<IActionResult> AddManager([FromBody] AddManagerDto addManagerDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid manager data.");
            }
            try
            {
                ApiResponse response = new ApiResponse();
                var result = await _managerService.AddManager(addManagerDto);
                switch (result)
                {
                    case AddNewManagerStatus.Success:
                        response.IsSuccess = true;
                        response.Message = "Manager added successfully.";
                        return Ok(response);
                    case AddNewManagerStatus.EmailAlreadyExists:
                        response.IsSuccess = false;
                        response.Message = "Email already exists.";
                        return BadRequest(response);
                    case AddNewManagerStatus.PhoneAlreadyExists:
                        response.IsSuccess = false;
                        response.Message = "Phone number already exists.";
                        return BadRequest(response);
                    case AddNewManagerStatus.DatabaseError:
                        response.IsSuccess = false;
                        response.Message = "An error occurred while adding the manager.";
                        return StatusCode(500, response);
                    default:
                        response.IsSuccess = false;
                        response.Message = "Unknown error occurred.";
                        return StatusCode(500, response);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }
    }
}

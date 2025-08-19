using BusinessObject.Models;
using BusinessObject.Models.Dto;
using BusinessObject.Models.Enum;
using CulinaryAPI.Middleware.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceObject.IServices;

namespace CulinaryAPI.Controllers
{
    [ApiController]
    [Route("api/customer")]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpPut("update-customer")]
        [Authorize]
        public async Task<IActionResult> UpdateCustomer([FromBody] UpdateCustomerDto updateCustomerDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Information's fields is incorect!");
            }
            var apiResponse = new ApiResponse();
            apiResponse.IsSuccess = await _customerService.UpdateCustomer(updateCustomerDto);
            apiResponse.Message = apiResponse.IsSuccess ? "Your information was updated successfully!" : "Failed to update your information!";
            return Ok(apiResponse);
        }

        [HttpPut("change-password")]
        [Authorize]
        public async Task<IActionResult> ChangePassword([FromBody] LoginAccountModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Information's fields is incorect!");
            }
            // Có thể kiểm tra bên FE không?
            if (string.IsNullOrEmpty(model.Email) || string.IsNullOrEmpty(model.Password) || string.IsNullOrEmpty(model.RePassword) || string.IsNullOrEmpty(model.OldPassword))
            {
                return BadRequest("All fields must be fill in");
            }
            // Có thể kiểm tra bên FE không?
            if (model.Password != model.RePassword)
            {
                return BadRequest("New password and re-password must be the same!");
            }
            var apiResponse = new ApiResponse();
            apiResponse.IsSuccess = await _customerService.ChangePassword(model);
            apiResponse.Message = apiResponse.IsSuccess ? "Your password was changed successfully!" : "Old password is incorect!";
            return Ok(apiResponse);
        }
    }
}

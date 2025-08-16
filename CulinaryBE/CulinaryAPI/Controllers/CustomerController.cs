using BusinessObject.Models;
using BusinessObject.Models.Dto;
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
    }
}

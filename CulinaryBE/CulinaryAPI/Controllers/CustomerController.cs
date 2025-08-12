using BusinessObject.Models.Dto.Customer;
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

        [HttpPost("update_customer")]
        public async Task<IActionResult> UpdateCustomer([FromBody] UpdateCustomerDto updateCustomerDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Information is incorect!");
            }

            try
            {
                await _customerService.UpdateCustomer(updateCustomerDto);
                return Ok("Update information sucessfully");
            }
            catch (Exception ex)
            {
                return BadRequest("Update failed!");
            }
        }
    }
}

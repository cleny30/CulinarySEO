using BusinessObject.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceObject.IServices;

namespace CulinaryAPI.Controllers
{
    [Route("api/warehouse")]
    [ApiController]
    public class WarehouseController : ControllerBase
    {
        private readonly IWarehouseService _warehouseService;

        public WarehouseController(IWarehouseService warehouseService)
        {
            _warehouseService = warehouseService;
        }

        [HttpGet("get-warehouses")]
        public async Task<IActionResult> GetWarehouses() {
            var list = await _warehouseService.GetWarehousesAsync();
            return Ok(new ApiResponse
            {
                IsSuccess = true,
                Message = "Lấy thông tin kho hàng thành công",
                Result = list
            });
        } 
    }
    
}

using BusinessObject.Models;
using Microsoft.AspNetCore.Mvc;
using ServiceObject.IServices;

namespace CulinaryAPI.Controllers
{
    [Route("api/category")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet("get-category-product-count")]
        public async Task<IActionResult> GetCategoryAndProductCount()
        {
            var result = await _categoryService.GetCategoriesForShop();

            return Ok(new ApiResponse
            {
                IsSuccess = true,
                Message = "Success get categories and product count",
                Result = result
            });
        }
    }
}

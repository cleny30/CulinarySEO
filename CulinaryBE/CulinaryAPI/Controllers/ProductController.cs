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
    [Route("api/products")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet("")]
        public async Task<IActionResult> GetAllProducts()
        {
            var apiResponse = new ApiResponse();
            var products = await _productService.GetAllProducts();
            apiResponse.Result = products;
            return Ok(apiResponse);
        }

        [HttpGet("filter-product")]
        public async Task<IActionResult> GetFilteredProducts([FromQuery] ProductFilterRequest request)
        {
            var result = await _productService.GetFilteredProductsAsync(request);

            return Ok(new ApiResponse
            {
                IsSuccess = true,
                Result = result
            });
        }

        [HttpGet("{productId}")]
        public async Task<IActionResult> GetProductDetailById(Guid productId)
        {
            var apiResponse = new ApiResponse();
            var product = await _productService.GetProductDetailById(productId);
            apiResponse.Result = product;
            return Ok(apiResponse);
        }
    }
}
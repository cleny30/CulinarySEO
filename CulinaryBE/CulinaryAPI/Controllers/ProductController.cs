using BusinessObject.Models;
using BusinessObject.Models.Dto;
using BusinessObject.Models.Enum;
using CulinaryAPI.Middleware.Authentication;
using Microsoft.AspNetCore.Authorization;
using BusinessObject.Models.Dto;
using Microsoft.AspNetCore.Mvc;
using ServiceObject.IServices;
using System.Security.Claims;

namespace CulinaryAPI.Controllers
{
    [ApiController]
    [Route("api/products")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IRecommendationService _recommendationService;

        public ProductController(IProductService productService, IRecommendationService recommendationService)
        {
            _productService = productService;
            _recommendationService = recommendationService;

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
        [HttpGet("recommendations")]
        public async Task<IActionResult> GetRecommendations()
        {
            var customerIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
            Guid? customerId = !string.IsNullOrEmpty(customerIdString) ? Guid.Parse(customerIdString) : null;

            var result = await _recommendationService.GetHomepageRecommendations(customerId);

            return Ok(result);
        }
    }
}
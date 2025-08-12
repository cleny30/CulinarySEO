using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessObject.Models;
using Microsoft.AspNetCore.Mvc;
using ServiceObject.IServices;
using Supabase.Gotrue;

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
    }
}
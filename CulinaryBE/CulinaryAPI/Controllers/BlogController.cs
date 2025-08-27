
using BusinessObject.Models;
using Microsoft.AspNetCore.Mvc;
using ServiceObject.IServices;

namespace CulinaryAPI.Controllers
{
    [Route("api/blogs")]
    [ApiController]
    public class BlogController : ControllerBase
    {
        private readonly IBlogService _blogService;

        public BlogController(IBlogService blogService)
        {
            _blogService = blogService;
        }

        [HttpGet("")]
        public async Task<IActionResult> GetBlogs()
        {
            var blogs = await _blogService.GetBlogs();
            return Ok(new ApiResponse
            {
                IsSuccess = true,
                Message = "Successfully retrieved blogs",
                Result = blogs
            });
        }
        [HttpGet("{blogId:guid}")]
        public async Task<IActionResult> GetBlogById(Guid blogId)
        {
            var blog = await _blogService.GetBlogById(blogId);
            if (blog == null)
            {
                return NotFound(new ApiResponse
                {
                    IsSuccess = false,
                    Message = "Blog not found",
                    Result = null
                });
            }
            return Ok(new ApiResponse
            {
                IsSuccess = true,
                Message = "Successfully retrieved blog",
                Result = blog
            });
        }
    }
}

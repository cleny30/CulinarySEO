
using BusinessObject.Models;
using BusinessObject.Models.Dto.Blog;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceObject.IServices;
using System.Security.Claims;

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

        [HttpPost("comments")]
        [Authorize]
        public async Task<IActionResult> AddComment([FromBody] CreateBlogCommentRequestDto request)
        {
            var customerId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (customerId == null)
            {
                return Unauthorized();
            }

            await _blogService.AddComment(request, Guid.Parse(customerId));

            return Ok(new ApiResponse
            {
                IsSuccess = true,
                Message = "Successfully added comment",
                Result = null
            });
        }

        [HttpGet("{blogId:guid}/comments")]
        public async Task<IActionResult> GetCommentsByBlogId(Guid blogId)
        {
            var comments = await _blogService.GetCommentsByBlogId(blogId);
            return Ok(new ApiResponse
            {
                IsSuccess = true,
                Message = "Successfully retrieved comments",
                Result = comments
            });
        }
    }
}

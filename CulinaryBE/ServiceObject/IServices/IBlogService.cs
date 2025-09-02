
using BusinessObject.Models.Dto.Blog;

namespace ServiceObject.IServices
{
    public interface IBlogService
    {
        Task<List<GetBlogDto>> GetBlogs();
        Task<GetBlogDetailDto?> GetBlogById(Guid blogId);
        Task AddComment(CreateBlogCommentRequestDto request, Guid customerId);
        Task<List<BlogCommentResponseDto>> GetCommentsByBlogId(Guid blogId);
    }
}

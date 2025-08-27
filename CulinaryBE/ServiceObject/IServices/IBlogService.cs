
using BusinessObject.Models.Dto.Blog;

namespace ServiceObject.IServices
{
    public interface IBlogService
    {
        Task<List<GetBlogDto>> GetBlogs();
        Task<GetBlogDetailDto?> GetBlogById(Guid blogId);
    }
}

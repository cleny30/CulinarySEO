
using BusinessObject.Models.Entity;

namespace DataAccess.IDAOs
{
    public interface IBlogDAO
    {
        Task<List<Blog>> GetAllBlogs();
        Task<Blog?> GetBlogById(Guid blogId);
        Task AddComment(BlogComment comment);
        Task<List<BlogComment>> GetCommentsByBlogId(Guid blogId);
    }
}

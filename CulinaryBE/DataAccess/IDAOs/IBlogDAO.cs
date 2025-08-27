
using BusinessObject.Models.Entity;

namespace DataAccess.IDAOs
{
    public interface IBlogDAO
    {
        Task<List<Blog>> GetAllBlogs();
        Task<Blog?> GetBlogById(Guid blogId);
    }
}

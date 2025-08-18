
using BusinessObject.Models.Dto.Blog;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ServiceObject.IServices
{
    public interface IBlogService
    {
        Task<List<GetBlogDto>> GetBlogs();
    }
}

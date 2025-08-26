
using BusinessObject.Models.Entity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataAccess.IDAOs
{
    public interface IBlogDAO
    {
        Task<List<Blog>> GetAllBlogs();
    }
}

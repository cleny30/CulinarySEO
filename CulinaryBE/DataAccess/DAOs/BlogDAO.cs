
using BusinessObject.AppDbContext;
using BusinessObject.Models.Entity;
using DataAccess.IDAOs;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.DAOs
{
    public class BlogDAO : IBlogDAO
    {
        private readonly CulinaryContext _context;

        public BlogDAO(CulinaryContext context)
        {
            _context = context;
        }

        public async Task<List<Blog>> GetAllBlogs()
        {
            return await _context.Blogs.Include(m => m.Manager).AsNoTracking()
                .Include(m => m.BlogCategoryMappings)
                    .ThenInclude(bcm => bcm.BlogCategory)
                .ToListAsync();
        }
    }
}

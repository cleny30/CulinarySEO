
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
            try
            {
                return await _context.Blogs.Include(m => m.Manager).AsNoTracking()
                .Include(m => m.BlogCategoryMappings)
                    .ThenInclude(bcm => bcm.BlogCategory)
                .ToListAsync();
            }
            catch (DbUpdateException ex)
            {
                throw new DbUpdateException("An error occurred while retrieving blogs.", ex);
            }
        }
        public async Task<Blog?> GetBlogById(Guid blogId)
        {
            try
            {
                return await _context.Blogs
                    .AsNoTracking()
                    .Include(b => b.Manager)
                    .Include(b => b.BlogCategoryMappings)
                        .ThenInclude(bcm => bcm.BlogCategory)
                    .Include(b => b.BlogComments)
                        .ThenInclude(c => c.Customer)
                    .Include(b => b.BlogComments)
                        .ThenInclude(c => c.ChildComments)
                            .ThenInclude(cc => cc.Customer)
                    .FirstOrDefaultAsync(b => b.BlogId == blogId);
            }
            catch (DbUpdateException ex)
            {
                throw new DbUpdateException("An error occurred while retrieving the blog by ID.", ex);
            }
        }

        public async Task AddComment(BlogComment comment)
        {
            try
            {
                await _context.BlogComments.AddAsync(comment);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                throw new DbUpdateException("An error occurred while adding the comment.", ex);
            }
        }

        public async Task<List<BlogComment>> GetCommentsByBlogId(Guid blogId)
        {
            try
            {
                return await _context.BlogComments
                    .Include(c => c.Customer)
                    .Include(c => c.ChildComments)
                        .ThenInclude(child => child.Customer)
                    .Where(c => c.BlogId == blogId && c.ParentCommentId == null)
                    .AsNoTracking()
                    .ToListAsync();
            }
            catch (DbUpdateException ex)
            {
                throw new DbUpdateException("An error occurred while retrieving comments.", ex);
            }
        }
    }
}
using BusinessObject.AppDbContext;
using BusinessObject.Models.Entity;
using DataAccess.IDAOs;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.DAOs
{
    public class CategoryDAO : ICategoryDAO
    {
        private readonly CulinaryContext _context;

        public CategoryDAO(CulinaryContext contextl)
        {
            _context = contextl;
        }
        public async Task<List<Category>> GetCategoriesAndProductCount()
        {
            var categories = await _context.Categories
                .AsNoTracking()
                .Where(c => c.CategoryName != "Featured")
                .Select(c => new Category
                {
                    CategoryId = c.CategoryId,
                    CategoryName = c.CategoryName,
                    ProductCount = c.ProductCategoryMappings.Any() ? c.ProductCategoryMappings.Count(): 0,
                })
                .ToListAsync();
            return categories;
        }
    }
}

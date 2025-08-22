using BusinessObject.AppDbContext;
using BusinessObject.Models.Dto;
using BusinessObject.Models.Entity;
using DataAccess.IDAOs;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace DataAccess.DAOs
{
    public class CategoryDAO : ICategoryDAO
    {
        private readonly CulinaryContext _context;

        public CategoryDAO(CulinaryContext contextl)
        {
            _context = contextl;
        }

        public async Task<List<Category>> GetCategories()
        {
          
            try
            {
                var categories = await _context.Categories
                 .AsNoTracking()
                 .ToListAsync();
                return categories;
            }
            catch (NpgsqlException ex)
            {
                throw new DbUpdateException("An error occurred while retrieving the list of categories", ex);
            }
        }

        public async Task<List<Category>> GetCategoriesAndProductCount()
        {
            try
            {
                var categories = await _context.Categories
               .AsNoTracking()
               .Where(c => c.CategoryName != "Featured")
               .Select(c => new Category
               {
                   CategoryId = c.CategoryId,
                   CategoryName = c.CategoryName,
                   ProductCount = c.ProductCategoryMappings.Any() ? c.ProductCategoryMappings.Count() : 0,
               })
               .ToListAsync();
                return categories;
            }catch (NpgsqlException ex)
            {
                throw new DbUpdateException("An error occurred while retrieving the list of categories", ex);
            }
        }
    }
}

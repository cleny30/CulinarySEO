using BusinessObject.AppDbContext;
using BusinessObject.Models.Entity;
using DataAccess.IDAOs;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.DAOs
{
    public class ProductDAO : IProductDAO
    {
        private readonly CulinaryContext _context;

        public ProductDAO(CulinaryContext context)
        {
            _context = context;
        }

        public async Task<List<Product>> GetAllProducts()
        {
            try
            {
                var products = await _context.Products
                 .AsNoTracking()
                 .Include(p => p.ProductCategoryMappings)
                 .ThenInclude(pcm=>pcm.Category)
                 .Include(p => p.ProductReviews)
                 .Include(p => p.Stocks)
                 .Include(p => p.ProductImages!.Where(img=>img.IsPrimary))
                 .ToListAsync();


                return products;
            }
            catch (DbUpdateException ex)
            {
                throw new DbUpdateException("An error occurred while retrieving products.", ex);
            }
        }

        public async Task<Product?> GetProductById(Guid productId)
        {
            try
            {
                var product = await _context.Products
                    .AsNoTracking()
                 .Include(p => p.ProductCategoryMappings)
                 .ThenInclude(pcm => pcm.Category).Include(p => p.ProductImages) 
                    .Include(p => p.Stocks)
                    .Include(p => p.ProductReviews.Where(r => r.Rating.HasValue)) 
                        .ThenInclude(pr => pr.Customer) 
                    .FirstOrDefaultAsync(p => p.ProductId == productId);

                return product;
            }
            catch (DbUpdateException ex)
            {
                throw new DbUpdateException($"An error occurred while retrieving the product with ID {productId}.", ex);
            }
        }
    }
}
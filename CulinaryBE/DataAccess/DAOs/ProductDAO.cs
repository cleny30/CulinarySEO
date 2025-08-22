using BusinessObject.AppDbContext;
using BusinessObject.Models;
using BusinessObject.Models.Entity;
using DataAccess.IDAOs;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DataAccess.DAOs
{
    public class ProductDAO : IProductDAO
    {
        private readonly CulinaryContext _context;
        private readonly ILogger<ProductDAO> _logger;

        public ProductDAO(CulinaryContext context, ILogger<ProductDAO> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<List<Product>> GetAllProducts()
        {
            try
            {
                var products = await _context.Products
                 .AsNoTracking()
                 .Include(p => p.ProductCategoryMappings)
                 .ThenInclude(pcm => pcm.Category)
                 .Include(p => p.ProductReviews)
                 .Include(p => p.Stocks)
                 .Include(p => p.ProductImages!)
                         .ToListAsync();


                return products;
            }
            catch (DbUpdateException ex)
            {
                throw new DbUpdateException("An error occurred while retrieving products.", ex);
            }
        }

        public async Task<List<Product>> GetBestSellingProducts(int topN = 10)
        {
            try
            {
                var threshold = DateTime.UtcNow.AddDays(-30); // Last 30 days

                var bestSellingProducts = await _context.OrderDetails
                    .Where(o => o.Order.CreatedAt >= threshold) // Filter orders from the last 30 days
                    .GroupBy(o => o.ProductId) // Group by ProductId
                    .Select(g => new
                    {
                        ProductId = g.Key,
                        TotalQuantity = g.Sum(o => o.Quantity)  // Sum the quantity of each product
                    })
                    .OrderByDescending(g => g.TotalQuantity)
                    .Take(topN)
                    .Join(_context.Products, bsp => bsp.ProductId, p => p.ProductId, (bsp, p) => p) // Join with Products to get product details
                    .ToListAsync();
                return bestSellingProducts;
            }
            catch (DbUpdateException ex)
            {
                throw new DbUpdateException("An error occurred while retrieving best-selling products.", ex);
            }
        }

        public async Task<Product?> GetProductDetailById(Guid productId)
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

        public async Task<List<Product>> GetProductSummariesById(IEnumerable<Guid> ids)
        {
            try
            {
                return await _context.Products
                .Include(p => p.ProductImages.Where(img => img.IsPrimary))
                .Where(p => ids.Contains(p.ProductId))
                .ToListAsync();
            }
            catch (DbUpdateException ex)
            {
                throw new DbUpdateException("An error occurred while retrieving product summaries.", ex);
            }
        }
        public async Task<Product?> GetProductAsync(Guid productId)
        {
            try
            {
                return await _context.Products.FindAsync(productId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "GetProduct failed {ProductId}", productId);
                throw new DatabaseException("Failed to load product");
            }
        }
        public async Task<List<Product>> GetAllProductsWithStocksAsync()
        {
            try
            {
                return await _context.Products
                    .Include(p => p.Stocks) // lấy toàn bộ stocks trong các warehouse
                        .ThenInclude(s => s.Warehouse) // nếu cần thêm thông tin kho
                    .Include(p => p.ProductReviews)
                    .Include(p => p.ProductImages)
                    .Include(p => p.ProductCategoryMappings)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching all products with stocks");
                throw;
            }
        }
    }
}
using BusinessObject.AppDbContext;
using BusinessObject.Models.Dto;
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

        public async Task<(int TotalItems, List<Product> Items)> GetFilteredProductsAsync(ProductFilterRequest request)
        {
            try
            {
                IQueryable<Product> query = _context.Products
                    .Include(p => p.Stocks)
                    .Include(p => p.ProductImages)
                    .Include(p => p.ProductReviews)
                    .Include(p => p.ProductCategoryMappings)
                    .ThenInclude(pcm => pcm.Category)
                    .AsQueryable();

                // Filter category
                if (request.CategoryIds != null && request.CategoryIds.Any())
                {
                    query = query.Where(p =>
                        p.ProductCategoryMappings.Any(pcm => request.CategoryIds.Contains(pcm.CategoryId))
                    );
                }

                // Filter price
                if (request.MinPrice.HasValue)
                    query = query.Where(p => p.Price >= request.MinPrice.Value);

                if (request.MaxPrice.HasValue)
                    query = query.Where(p => p.Price <= request.MaxPrice.Value);

                // Filter available
                if (request.IsAvailable.HasValue)
                {
                    query = request.IsAvailable.Value
                        ? query.Where(p => p.Stocks.Sum(s => s.Quantity) > 0)
                        : query.Where(p => p.Stocks.Sum(s => s.Quantity) == 0);
                }

                // Sort
                query = request.SortBy?.ToLower() switch
                {
                    "best-selling" => query.OrderByDescending(p => p.TotalSold),
                    "a-z" => query.OrderBy(p => p.ProductName),
                    "z-a" => query.OrderByDescending(p => p.ProductName),
                    "price-low-high" => query.OrderBy(p => p.Price),
                    "price-high-low" => query.OrderByDescending(p => p.Price),
                    "date-new-old" => query.OrderByDescending(p => p.CreatedAt),
                    "date-old-new" => query.OrderBy(p => p.CreatedAt),
                    "featured" => query
                        .Where(p => p.ProductCategoryMappings.Any(pcm => pcm.Category!.CategoryName == "Featured"))
                        .OrderByDescending(p => p.CreatedAt),
                    _ => query.OrderBy(p => p.ProductName) // mặc định
                };

                int totalItems = await query.CountAsync();

                var items = await query
                    .Skip((request.Page - 1) * request.PageSize)
                    .Take(request.PageSize)
                    .Select(p => new Product
                    {
                        ProductId = p.ProductId,
                        ProductName = p.ProductName,
                        Price = p.Price,
                        Discount = p.Discount,
                        ProductReviews = p.ProductReviews,
                        Stocks = p.Stocks
                        .Select(s => new Stock
                        {
                            Quantity = s.Quantity
                        }).ToList(),

                        ProductImages = p.ProductImages
                        .Select(img => new ProductImage
                        {
                            ImageUrl = img.ImageUrl
                        }).ToList()
                    })
                    .AsNoTracking()
                    .ToListAsync(); // Trả nguyên Product entity

                return (totalItems, items);
            }
            catch (DbUpdateException ex)
            {
                throw new DbUpdateException("An error occurred while retrieving filtered products.", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("An unexpected error occurred while retrieving filtered products.", ex);
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
    }
}
using BusinessObject.AppDbContext;
using BusinessObject.Models.Entity;
using DataAccess.IDAOs;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.DAOs
{
    internal class RecommendationDAO : IRecommendationDAO
    {
        private readonly CulinaryContext _context;
        public RecommendationDAO(CulinaryContext context)
        {
            _context = context;
        }
        public async Task<List<Product>> GetBuyAgainProducts(Guid customerId, int topN)
        {
            try
            {
                return await _context.OrderDetails
                .Where(od => od.Order.CustomerId == customerId)
                .OrderByDescending(od => od.Order.CreatedAt)
                .Select(od => od.Product)
                .Distinct()
                .Take(topN)
                .Include(p => p.ProductImages.Where(img => img.IsPrimary))
                .ToListAsync();
            }
            catch (DbUpdateException ex)
            {
                throw new Exception("An error occurred while retrieving buy-again products.", ex);
            }
        }
        public async Task<List<Guid>> GetCollaborativeRecommendationIds(List<Guid> productIds, int topN)
        {
            try
            {
                //return await _context.ProductRecommendations
                //.Where(pr => productIds.Contains(pr.ProductId))
                //.OrderByDescending(pr => pr.Score)
                //.Select(pr => pr.RecommendedProductId)
                //.Distinct()
                //.Take(topN)
                //.ToListAsync();
                return null;
            }
            catch (DbUpdateException ex)
            {
                throw new Exception("An error occurred while retrieving collaborative recommendation IDs.", ex);
            }
        }

        public Task<List<Guid>> GetCollaborativeRecommendationIds(Guid userId, int topN)
        {
            throw new NotImplementedException();
        }
    }
}

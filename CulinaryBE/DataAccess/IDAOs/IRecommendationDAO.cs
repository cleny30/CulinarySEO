using BusinessObject.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.IDAOs
{
    public interface IRecommendationDAO
    {
        Task<List<Product>> GetBuyAgainProducts(Guid userId, int topN);
        Task<List<Guid>> GetCollaborativeRecommendationIds(Guid userId, int topN);
    }
}

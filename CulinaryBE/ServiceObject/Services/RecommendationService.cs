using AutoMapper;
using BusinessObject.Models.Dto.Personalize;
using BusinessObject.Models.Dto.Product;
using DataAccess.IDAOs;
using ServiceObject.IServices;

namespace ServiceObject.Services
{
    internal class RecommendationService : IRecommendationService
    {
        private readonly IOrderDAO _orderDAO;
        private readonly IProductDAO _productDAO;
        private readonly IRecommendationDAO _recommendationDAO;
        private readonly IMapper _mapper;

        public RecommendationService(
                IOrderDAO orderDAO,
                IProductDAO productDAO,
                IRecommendationDAO recommendationDAO,
                IMapper mapper)
        {
            _orderDAO = orderDAO;
            _productDAO = productDAO;
            _recommendationDAO = recommendationDAO;
            _mapper = mapper;
        }

        public async Task<HomepageRecommendationDto> GetHomepageRecommendations(Guid? customerId)
        {
            try
            {
                if (customerId == null || await _orderDAO.GetOrderCountByCustomerId(customerId.Value) < 3)
                {
                    var bestSellers = await _productDAO.GetBestSellingProducts(10);
                    return new HomepageRecommendationDto
                    {
                        RecommendationType = "BestSeller",
                        Products = _mapper.Map<List<ProductSummaryDto>>(bestSellers)
                    };
                }
                var buyAgain = await _recommendationDAO.GetBuyAgainProducts(customerId.Value, 5);
                var recommendedIds = await _recommendationDAO.GetCollaborativeRecommendationIds(buyAgain.First().ProductId, 5);
                var recommended = await _productDAO.GetProductSummariesById(recommendedIds);

                return new HomepageRecommendationDto()
                {
                    RecommendationType = "Personalized",
                    PersonalizedResult = new PersonalizedResultDto
                    {
                        BuyAgainProducts = _mapper.Map<List<ProductSummaryDto>>(buyAgain),
                        RecommendedProducts = _mapper.Map<List<ProductSummaryDto>>(recommended)
                    }
                };
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to retrieve homepage recommendations: " + ex.Message);
            }
        }
    }
}

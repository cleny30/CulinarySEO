using BusinessObject.Models.Dto.Personalize;

namespace ServiceObject.IServices
{
    public interface IRecommendationService
    {
        Task<HomepageRecommendationDto> GetHomepageRecommendations(Guid? customerId);
    }
}

using BusinessObject.Models.Dto.Product;

namespace BusinessObject.Models.Dto.Personalize
{
    public class HomepageRecommendationDto
    {
        public string RecommendationType { get; set; } // "BestSeller" or "Personalized"
        public List<ProductSummaryDto> Products { get; set; } = new List<ProductSummaryDto>();
        public PersonalizedResultDto PersonalizedResult { get; set; } = new PersonalizedResultDto();
    }
}

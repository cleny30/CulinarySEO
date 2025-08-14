using BusinessObject.Models.Dto.Product;

namespace BusinessObject.Models.Dto.Personalize
{
    public class PersonalizedResultDto
    {
        public List<ProductSummaryDto> BuyAgainProducts { get; set; } = new List<ProductSummaryDto>();
        public List<ProductSummaryDto> RecommendedProducts { get; set; } = new List<ProductSummaryDto>();

    }
}

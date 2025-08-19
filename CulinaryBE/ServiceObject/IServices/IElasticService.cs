using BusinessObject.Models.Dto;

namespace ServiceObject.IServices
{
    public interface IElasticService
    {
        Task ReindexAllAsync();
        Task DeleteProductAsync(Guid productId);
        Task IndexProductAsync(Guid productId);
        Task<PagedResult<ProductDto>> GetFilteredProducts(ProductFilterRequest request);
    }
}

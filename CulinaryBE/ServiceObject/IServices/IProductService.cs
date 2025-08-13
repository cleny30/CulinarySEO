using BusinessObject.Models.Dto;

namespace ServiceObject.IServices
{
    public interface IProductService
    {
        Task<List<GetProductDto>> GetAllProducts();
        Task<GetProductDetailDto> GetProductDetailById(Guid productId);
        Task<PagedResult<ProductDto>> GetFilteredProductsAsync(ProductFilterRequest request);

    }
}
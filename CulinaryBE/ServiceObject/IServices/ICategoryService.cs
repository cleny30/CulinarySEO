using BusinessObject.Models.Dto;

namespace ServiceObject.IServices
{
    public interface ICategoryService
    {
        Task<List<CategoryForShop>> GetCategoriesForShop();
        Task<List<CategoryDto>> GetCategories();
    }
}

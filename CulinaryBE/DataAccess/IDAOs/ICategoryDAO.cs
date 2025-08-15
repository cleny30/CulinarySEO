using BusinessObject.Models.Entity;

namespace DataAccess.IDAOs
{
    public interface ICategoryDAO
    {
        Task<List<Category>> GetCategoriesAndProductCount();
    }
}

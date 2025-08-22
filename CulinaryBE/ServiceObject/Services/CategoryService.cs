using AutoMapper;
using BusinessObject.Models.Dto;
using DataAccess.IDAOs;
using Microsoft.Extensions.Logging;
using ServiceObject.IServices;

namespace ServiceObject.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryDAO _categoryDAO;
        private readonly IMapper _mapper;
        private ILogger<CategoryService> _logger;

        public CategoryService(ICategoryDAO categoryDAO, IMapper mapper, ILogger<CategoryService> logger)
        {
            _categoryDAO = categoryDAO;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<List<CategoryForShop>> GetCategoriesForShop()
        {
            var result = await _categoryDAO.GetCategoriesAndProductCount();
            return _mapper.Map<List<CategoryForShop>>(result);
        }
        public async Task<List<CategoryDto>> GetCategories()
        {
            var result = await _categoryDAO.GetCategories();
            return _mapper.Map<List<CategoryDto>>(result);
        }
    }
}

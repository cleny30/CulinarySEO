using AutoMapper;
using BusinessObject.Models.Dto;
using DataAccess.IDAOs;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using ServiceObject.IServices;

namespace ServiceObject.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductDAO productDAO;
        private readonly IMemoryCache _cache;
        private ILogger<ProductService> _logger;
        private readonly IMapper _mapper;

        public ProductService(IProductDAO productDAO, IMemoryCache cache, ILogger<ProductService> logger, IMapper mapper)
        {
            this.productDAO = productDAO;
            _cache = cache;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<List<GetProductDto>> GetAllProducts()
        {
            try
            {
                var products = await productDAO.GetAllProducts();

                return _mapper.Map<List<GetProductDto>>(products); ;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving all products");
                throw new Exception("Failed to retrieve products: " + ex.Message);
            }
        }

        public async Task<GetProductDetailDto> GetProductDetailById(Guid productId)
        {
            try
            {
                var product = await productDAO.GetProductById(productId);
                if (product == null)
                {
                    _logger.LogWarning($"Product with ID {productId} not found.");
                    throw new KeyNotFoundException($"Product with ID {productId} not found.");
                }
                return _mapper.Map<GetProductDetailDto>(product);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error retrieving product details for ID {productId}");
                throw new Exception("Failed to retrieve product details: " + ex.Message);
            }
        }

        public async Task<PagedResult<ProductDto>> GetFilteredProductsAsync(ProductFilterRequest request)
        {
            try
            {
                string categoryIdsKey = request.CategoryIds != null && request.CategoryIds.Any()
                    ? string.Join(",", request.CategoryIds.OrderBy(id => id))
                    : "";

                string cacheKey = $"filterProduct:{request.Page}:{request.PageSize}:{categoryIdsKey}:{request.MinPrice}:{request.MaxPrice}:{request.IsAvailable}:{request.SortBy}";

                if (_cache.TryGetValue(cacheKey, out PagedResult<ProductDto> cachedResult))
                {
                    return cachedResult;
                }

                var result = await productDAO.GetFilteredProductsAsync(request);

                var pagedResult = new PagedResult<ProductDto>
                {
                    Items = _mapper.Map<List<ProductDto>>(result.Item2),
                    TotalItems = result.Item1,
                    Page = request.Page,
                    PageSize = request.PageSize
                };

                var cacheOptions = new MemoryCacheEntryOptions
                {
                    SlidingExpiration = TimeSpan.FromSeconds(30)
                };

                // Save cache 30s
                _cache.Set(cacheKey, pagedResult, TimeSpan.FromSeconds(30));

                return pagedResult;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error filtering products with request: {System.Text.Json.JsonSerializer.Serialize(request)}");
                throw new Exception("Failed to filter products: " + ex.Message);
            }
        }
    }
}
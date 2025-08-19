using AutoMapper;
using BusinessObject.Models.Dto;
using DataAccess.IDAOs;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using ServiceObject.Background.Queue;
using ServiceObject.IServices;

namespace ServiceObject.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductDAO productDAO;
        private readonly IMemoryCache _cache;
        private ILogger<ProductService> _logger;
        private readonly IMapper _mapper;
        private readonly IElasticService _elasticService;
        private readonly IProductSyncQueue _syncQueue;

        public ProductService(IProductDAO productDAO, IMemoryCache cache, ILogger<ProductService> logger, IMapper mapper, IElasticService elasticService, IProductSyncQueue syncQueue)
        {
            this.productDAO = productDAO;
            _cache = cache;
            _logger = logger;
            _mapper = mapper;
            _elasticService = elasticService;
            _syncQueue = syncQueue;
        }

        public async Task<List<GetProductDto>> GetAllProducts()
        {
            try
            {
                var products = await productDAO.GetAllProducts();

                return _mapper.Map<List<GetProductDto>>(products); 

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
                var product = await productDAO.GetProductDetailById(productId);
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
            string categoryIdsKey = request.CategoryIds != null && request.CategoryIds.Any()
                ? string.Join(",", request.CategoryIds.OrderBy(id => id))
                : "";

            string cacheKey = $"filterProduct:{request.Page}:{request.PageSize}:{categoryIdsKey}:{request.MinPrice}:{request.MaxPrice}:{request.IsAvailable}:{request.SortBy}";

            if (_cache.TryGetValue(cacheKey, out PagedResult<ProductDto> cachedResult))
            {
                return cachedResult;
            }

            var result = await _elasticService.GetFilteredProducts(request);

            var total = result.TotalItems;
            var items = result.Items;

            var pagedResult = new PagedResult<ProductDto>
            {
                Items = items,
                TotalItems = total,
                Page = request.Page,
                PageSize = request.PageSize
            };

            _cache.Set(cacheKey, pagedResult, TimeSpan.FromSeconds(30));

            return pagedResult;
        }
    }
}
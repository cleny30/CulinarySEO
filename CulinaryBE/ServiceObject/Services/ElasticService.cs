using AutoMapper;
using BusinessObject.Models.Dto;
using BusinessObject.Models.Enum;
using DataAccess.IDAOs;
using Elastic.Clients.Elasticsearch;
using Elastic.Clients.Elasticsearch.QueryDsl;
using Microsoft.Extensions.Logging;
using ServiceObject.IServices;

namespace ServiceObject.Services
{
    public class ElasticService : IElasticService
    {
        private readonly ElasticsearchClient _elasticClient;
        private readonly ILogger<ElasticService> _logger;
        private readonly IProductDAO _productDAO;
        private readonly IMapper _mapper;

        public ElasticService(ElasticsearchClient elasticClient, IProductDAO productDAO, IMapper mapper, ILogger<ElasticService> logger)
        {
            _elasticClient = elasticClient;
            _productDAO = productDAO;
            _logger = logger;
            _mapper = mapper;
        }

        // Index (insert/update) 1 product
        public async Task IndexProductAsync(Guid productId)
        {
            var product = await _productDAO.GetProductDetailById(productId);
            if (product == null) return;

            var dto = _mapper.Map<ElasticProductDto>(product);

            var response = await _elasticClient.IndexAsync(dto, i => i
                .Index("products")
                .Id(dto.ProductId)
                .Refresh(Refresh.True)
            );

            if (!response.IsValidResponse)
            {
                Console.WriteLine($"[Elastic] Failed to index product {productId}: {response.DebugInformation}");
            }
        }

        // Xóa product khỏi index
        public async Task DeleteProductAsync(Guid productId)
        {
            await _elasticClient.DeleteAsync<ProductDto>(productId.ToString(), d => d.Index("products"));
        }

        // Full sync toàn bộ product
        public async Task ReindexAllAsync()
        {
            var products = await _productDAO.GetAllProducts();
            var dtos = _mapper.Map<List<ElasticProductDto>>(products);

            var bulkResponse = await _elasticClient.BulkAsync(b => b
                .Index("products")
                .IndexMany(dtos, (descriptor, doc) => descriptor.Id(doc.ProductId))
            );

            if (bulkResponse.Errors)
            {
                _logger.LogError("[Elastic] Bulk indexing encountered errors.");
            }
        }

        #region Filter Product
        public async Task<PagedResult<ProductDto>> GetFilteredProducts(ProductFilterRequest request)
        {
            try
            {
                var mustQueries = new List<Query>();

                // Category filter
                if (request.CategoryIds?.Any() == true)
                {
                    mustQueries.Add(new TermsQuery
                    {
                        Field = "categoryIds",
                        Terms = new TermsQueryField(
                            request.CategoryIds.Select(id => FieldValue.Long(id)).ToList()
                        )
                    });
                }

                // Price filter
                if (request.MinPrice.HasValue || request.MaxPrice.HasValue)
                {
                    mustQueries.Add(new NumberRangeQuery
                    {
                        Field = "finalPrice",
                        Gte = request.MinPrice.HasValue ? (double?)request.MinPrice.Value : null,
                        Lte = request.MaxPrice.HasValue ? (double?)request.MaxPrice.Value : null
                    });
                }

                // Availability filter
                if (request.IsAvailable.HasValue)
                {
                    mustQueries.Add(new NumberRangeQuery
                    {
                        Field = "totalQuantity",
                        Gte = request.IsAvailable.Value ? 1 : 0,
                        Lte = request.IsAvailable.Value ? null : 0
                    });
                }

                // Sorting
                var sortOptions = new List<SortOptions>();

                switch (request.SortBy)
                {
                    case ProductSortOption.Feature:
                        mustQueries.Add(new TermQuery
                        {
                            Field = "categoryIds",
                            Value = 1
                        });

                        break;
                    case ProductSortOption.PriceLowHigh:
                        sortOptions.Add(new SortOptions
                        {
                            Field = new FieldSort { Field = "finalPrice", Order = SortOrder.Asc }
                        });
                        break;

                    case ProductSortOption.PriceHighLow:
                        sortOptions.Add(new SortOptions
                        {
                            Field = new FieldSort { Field = "finalPrice", Order = SortOrder.Desc }
                        });
                        break;

                    case ProductSortOption.NameAZ:
                        sortOptions.Add(new SortOptions
                        {
                            Field = new FieldSort { Field = "productName.keyword", Order = SortOrder.Asc }
                        });
                        break;

                    case ProductSortOption.NameZA:
                        sortOptions.Add(new SortOptions
                        {
                            Field = new FieldSort { Field = "productName.keyword", Order = SortOrder.Desc }
                        });
                        break;

                    case ProductSortOption.BestSelling:
                        sortOptions.Add(new SortOptions
                        {
                            Field = new FieldSort { Field = "totalSold", Order = SortOrder.Desc }
                        });
                        break;

                    case ProductSortOption.DateNewOld:
                        sortOptions.Add(new SortOptions
                        {
                            Field = new FieldSort { Field = "createdAt", Order = SortOrder.Desc }
                        });
                        break;

                    case ProductSortOption.DateOldNew:
                        sortOptions.Add(new SortOptions
                        {
                            Field = new FieldSort { Field = "createdAt", Order = SortOrder.Asc }
                        });
                        break;

                    default:
                        sortOptions.Add(new SortOptions
                        {
                            Field = new FieldSort { Field = "productName.keyword", Order = SortOrder.Asc }
                        });
                        break;
                }

                // Search request
                var response = await _elasticClient.SearchAsync<ProductDto>(s => s
                    .Indices("products")
                    .From((request.Page - 1) * request.PageSize)
                    .Size(request.PageSize)
                    .Query(q => q.Bool(b => b.Must(mustQueries)))
                    .Sort(sortOptions)
                );

                if (!response.IsValidResponse)
                {
                    _logger.LogError("Elasticsearch search failed: {DebugInfo}", response.DebugInformation);
                    throw new Exception("Search failed in Elasticsearch");
                }

                long totalItems = 0;
                if (response.HitsMetadata?.Total != null)
                {
                    totalItems = response.HitsMetadata.Total.Match(
                        totalHits => totalHits?.Value ?? 0,
                        longValue => longValue
                    );
                }
                var items = response.Documents.ToList();

                return new PagedResult<ProductDto>
                {
                    Items = items,
                    TotalItems = (int)totalItems,
                    Page = request.Page,
                    PageSize = request.PageSize
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while searching products in Elasticsearch");
                throw;
            }
        }

        #endregion
    }
}

using AutoMapper;
using BusinessObject.Models.Dto;
using BusinessObject.Models.Entity;
using DataAccess.DAOs;
using DataAccess.IDAOs;
using Microsoft.Extensions.Logging;
using ServiceObject.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServiceObject.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductDAO productDAO;
        private ILogger<ProductService> _logger;
        private readonly IMapper _mapper;

        public ProductService(IProductDAO productDAO, ILogger<ProductService> logger, IMapper mapper)
        {
            this.productDAO = productDAO;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<List<GetProductDto>> GetAllProducts()
        {
            try
            {
                var products = await productDAO.GetAllProducts();
                var productDtos = _mapper.Map<List<GetProductDto>>(products);
                foreach (var productDto in productDtos)
                {
                    var FinalPrice = productDto.Price;
                    if (productDto.Discount.HasValue)
                    {
                        FinalPrice = productDto.Price - (productDto.Price * (productDto.Discount.Value / 100));
                    }
                    productDto.FinalPrice = FinalPrice;
                    var ratings = products
                        .Where(p => p.ProductId == productDto.ProductId)
                        .SelectMany(p => p.ProductReviews.Where(r => r.Rating.HasValue))
                        .Select(r => r.Rating.Value);
                    var averageRating = ratings.Any() ? ratings.Average() : 0;
                    productDto.AverageRating = (decimal)averageRating;
                }
                return productDtos;
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
                var productDto = _mapper.Map<GetProductDetailDto>(product);
                var FinalPrice = product.Price;
                if (product.Discount.HasValue)
                {
                    FinalPrice = product.Price - (product.Price * (product.Discount.Value / 100));
                }
                productDto.FinalPrice = FinalPrice;

                return productDto;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error retrieving product details for ID {productId}");
                throw new Exception("Failed to retrieve product details: " + ex.Message);
            }
        }
    }
}
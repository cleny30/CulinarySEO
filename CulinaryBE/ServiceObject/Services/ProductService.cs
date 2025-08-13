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
    }
}
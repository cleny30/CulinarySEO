using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BusinessObject.Models.Dto;
using DataAccess.IDAOs;
using Microsoft.Extensions.Logging;
using ServiceObject.IServices;

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
                return _mapper.Map<List<GetProductDto>>(products);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving all products");
                throw new Exception("Failed to retrieve products: " + ex.Message);
            }
        }
    }
}
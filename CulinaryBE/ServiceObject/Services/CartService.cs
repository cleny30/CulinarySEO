using AutoMapper;
using BusinessObject.Models.Dto;
using DataAccess.IDAOs;
using Microsoft.Extensions.Logging;
using ServiceObject.IServices;

namespace ServiceObject.Services
{
    public class CartService : ICartService
    {
        private readonly ICartDAO _cartDAO;
        private readonly ILogger<CartService> _logger;
        private readonly IMapper _mapper;

        public CartService(ICartDAO cartDAO, ILogger<CartService> logger, IMapper mapper)
        {
            _cartDAO = cartDAO;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<CartDto> GetCartDataAsync(Guid customerId)
        {
            try
            {
                var cart = await _cartDAO.GetCartDataAsync(customerId);


                return null;
            }
            catch (Exception ex)
            {
                throw new Exception("Fail to retrieve data from cart", ex);
            }
        }
    }
}

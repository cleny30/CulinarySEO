using AutoMapper;
using BusinessObject.Models;
using BusinessObject.Models.Dto;
using BusinessObject.Models.Entity;
using DataAccess.DAOs;
using DataAccess.IDAOs;
using Microsoft.Extensions.Logging;
using ServiceObject.IServices;

namespace ServiceObject.Services
{
    public class CartService : ICartService
    {
        private readonly ICartDAO _cartDAO;
        private readonly IStockDAO _stockDAO;
        private readonly ILogger<CartService> _logger;
        private readonly IMapper _mapper;

        public CartService(ICartDAO cartDAO, IStockDAO stockDAO, ILogger<CartService> logger, IMapper mapper)
        {
            _cartDAO = cartDAO;
            _stockDAO = stockDAO;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<CartDto> GetCartDataAsync(Guid customerId)
        {
            try
            {
                var cart = await _cartDAO.GetCartDataAsync(customerId);

                return _mapper.Map<CartDto>(cart);
            }
            catch (Exception ex)
            {
                throw new Exception("Fail to retrieve data from cart", ex);
            }
        }

        public async Task AddToCartAsync(AddToCartRequest request)
        {
            try
            {
                var existingItem = await _cartDAO.GetCartItemByProductAsync(request.CartId, request.ProductId);

                //Check request.quantity and current in cart < total stock quantity
                int currentInCart = existingItem?.Quantity ?? 0;
                int totalStock = await _stockDAO.GetTotalStockByProductAsync(request.ProductId);

                if (currentInCart + request.Quantity > totalStock)
                    throw new ValidationException("Not enough stock available");

                if (existingItem != null)
                {
                    existingItem.Quantity += request.Quantity;
                    await _cartDAO.UpdateCartItemAsync(existingItem);
                }
                else
                {
                    var cartItem = _mapper.Map<CartItem>(request);

                    await _cartDAO.AddCartItemAsync(cartItem);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding product {ProductId} to cart {CartId}", request.ProductId, request.CartId);
            }
        }

        public async Task UpdateCartItemAsync(UpdateCartItemRequest request)
        {
            try
            {
                var cartItem = await _cartDAO.GetCartItemByIdAsync(request.CartItemId);

                int totalStock = await _stockDAO.GetTotalStockByProductAsync(cartItem.ProductId);

                if (request.Quantity > totalStock)
                    throw new ValidationException("Not enough stock available");

                if (cartItem == null)
                    throw new NotFoundException("CartItem not found");

                cartItem.Quantity = request.Quantity;
                await _cartDAO.UpdateCartItemAsync(cartItem);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating cartItem {CartItemId}", request.CartItemId);
                throw;
            }
        }
        public async Task RemoveFromCartAsync(Guid cartItemId)
        {
            try
            {
                var cartItem = await _cartDAO.GetCartItemByIdAsync(cartItemId);
                if (cartItem == null)
                    throw new NotFoundException("CartItem not found");

                await _cartDAO.RemoveCartItemAsync(cartItem);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error removing cartItem {CartItemId}", cartItemId);
                throw;
            }
        }
    }
}

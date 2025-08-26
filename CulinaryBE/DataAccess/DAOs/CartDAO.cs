using BusinessObject.AppDbContext;
using BusinessObject.Models;
using BusinessObject.Models.Entity;
using DataAccess.IDAOs;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Npgsql;

namespace DataAccess.DAOs
{
    public class CartDAO : ICartDAO
    {
        private readonly CulinaryContext _context;
        private readonly ILogger<CartDAO> _logger;

        public CartDAO(CulinaryContext context, ILogger<CartDAO> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<Cart> GetCartDataAsync(Guid customerId)
        {
            try
            {
                _logger.LogInformation("Fetching cart data for CustomerId: {CustomerId}", customerId);

                var cart = await _context.Carts
                    .AsNoTracking()
                    .Where(c => c.CustomerId == customerId)
                    .Select(c => new Cart
                    {
                        CartId = c.CartId,
                        CustomerId = c.CustomerId,
                        CartItems = c.CartItems.Select(ci => new CartItem
                        {
                            CartItemId = ci.CartItemId,
                            Quantity = ci.Quantity,
                            ProductId = ci.ProductId,
                            Product = new Product
                            {
                                ProductId = ci.Product.ProductId,
                                ProductName = ci.Product.ProductName,
                                Price = ci.Product.Price,
                                Discount = ci.Product.Discount,
                                ProductImages = ci.Product.ProductImages
                            }
                        }).ToList()
                    })
                    .FirstOrDefaultAsync();

                if (cart == null)
                {
                    _logger.LogWarning("No cart found for CustomerId: {CustomerId}", customerId);
                }
                else
                {
                    _logger.LogInformation("Cart data retrieved successfully for CustomerId: {CustomerId}", customerId);
                }

                return cart;
            }
            catch (NpgsqlException ex)
            {
                _logger.LogError(ex, "Database error while fetching cart for CustomerId: {CustomerId}", customerId);
                throw new DbUpdateException("An error occurred while retrieving cart data.", ex);
            }
        }

        public async Task<Cart?> GetCartByIdAsync(Guid cartId)
        {
            try
            {
                return await _context.Carts.Include(c => c.CartItems)
                                      .FirstOrDefaultAsync(c => c.CartId == cartId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching cart {CartId}", cartId);
                throw;
            }
        }

        public async Task<CartItem?> GetCartItemByIdAsync(Guid cartItemId)
        {
            try
            {
                return await _context.CartItems.FirstOrDefaultAsync(ci => ci.CartItemId == cartItemId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching cartItem {CartItemId}", cartItemId);
                throw;
            }
        }

        public async Task<CartItem?> GetCartItemByProductAsync(Guid cartId, Guid productId)
        {
            try
            {
                return await _context.CartItems.FirstOrDefaultAsync(ci => ci.CartId == cartId && ci.ProductId == productId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching cartItem by product {ProductId} in cart {CartId}", productId, cartId);
                throw;
            }
        }
        public async Task AddCartItemAsync(CartItem cartItem)
        {
            try
            {
                await _context.CartItems.AddAsync(cartItem);
                await _context.SaveChangesAsync();
            }
            catch (NpgsqlException ex)
            {
                _logger.LogError(ex, "Error adding cartItem {@CartItem}", cartItem);
                throw new DatabaseException("Error occurred while adding product to cart");
            }
        }

        public async Task UpdateCartItemAsync(CartItem cartItem)
        {
            try
            {
                _context.CartItems.Update(cartItem); 
                await _context.SaveChangesAsync();
            }
            catch (NpgsqlException ex)
            {
                _logger.LogError(ex, "Error updating cartItem {@CartItem}", cartItem);
                throw new DatabaseException("Error occurred while updating cart item");
            }
        }

        public async Task RemoveCartItemAsync(CartItem cartItem)
        {
            try
            {
                _context.CartItems.Remove(cartItem);
                await _context.SaveChangesAsync();
            }
            catch (NpgsqlException ex)
            {
                _logger.LogError(ex, "Error removing cartItem {@CartItem}", cartItem);
                throw new DatabaseException("Error occurred while removing cart item");
            }
        }

        public async Task DeleteCartAsync(Guid cartId)
        {
            var cart = await _context.Carts
                .Include(c => c.CartItems)
                .FirstOrDefaultAsync(c => c.CartId == cartId);

            if (cart != null)
            {
                _context.CartItems.RemoveRange(cart.CartItems);
                _context.Carts.Remove(cart);
                await _context.SaveChangesAsync();
            }
        }

    }
}


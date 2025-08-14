using BusinessObject.AppDbContext;
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
                                    .Select(pi => new ProductImage
                                    {
                                        ProductId = pi.ProductId,
                                        ImageUrl = pi.ImageUrl
                                    })
                                    .ToList()
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

    }
}


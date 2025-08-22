using BusinessObject.AppDbContext;
using BusinessObject.Models;
using BusinessObject.Models.Entity;
using DataAccess.IDAOs;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DataAccess.DAOs
{
    public class OrderDAO : IOrderDAO
    {
        private readonly CulinaryContext _context;
        private readonly ILogger<OrderDAO> _logger;

        public OrderDAO(CulinaryContext context, ILogger<OrderDAO> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<int> GetOrderCountByCustomerId(Guid customerId)
        {
            try
            {
                return await _context.Orders.CountAsync(o => o.CustomerId == customerId);
            }
            catch (DbUpdateException ex)
            {
                throw new DbUpdateException($"An error occurred while retrieving the order count for customer ID {customerId}.", ex);
            }
        }

        public async Task<Guid> CreateOrderGraphAsync(
        Order order,
        IEnumerable<OrderDetail> details,
        OrderStatusHistory initialStatus,
        List<OrderVoucher>? orderVouchers)
        {
            using var tx = await _context.Database.BeginTransactionAsync();
            try
            {
                await _context.Orders.AddAsync(order);
                await _context.OrderDetails.AddRangeAsync(details);

                await _context.OrderStatusHistories.AddAsync(initialStatus);

                if (orderVouchers != null)
                    await _context.OrderVouchers.AddRangeAsync(orderVouchers);

                await _context.SaveChangesAsync();
                await tx.CommitAsync();
                return order.OrderId;
            }
            catch (Exception ex)
            {
                await tx.RollbackAsync();
                _logger.LogError(ex, "CreateOrderGraph failed");
                throw new DatabaseException("Failed to create order");
            }
        }
    }
}

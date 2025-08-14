using BusinessObject.AppDbContext;
using DataAccess.IDAOs;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.DAOs
{
    public class OrderDAO : IOrderDAO
    {
        private readonly CulinaryContext _context;
        public OrderDAO(CulinaryContext context)
        {
            _context = context;
        }

        public async Task<int> GetOrderCountByCustomerId(Guid customerId)
        {
            try
            {
                return await _context.Orders.CountAsync(o => o.CustomerId == customerId);
            } catch (DbUpdateException ex)
            {
                throw new DbUpdateException($"An error occurred while retrieving the order count for customer ID {customerId}.", ex);
            }
        }
    }
}

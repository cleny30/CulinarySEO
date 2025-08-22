using BusinessObject.Models.Entity;

namespace DataAccess.IDAOs
{
    public interface IOrderDAO
    {
        Task<int> GetOrderCountByCustomerId(Guid customerId);
        Task<Guid> CreateOrderGraphAsync(Order order,IEnumerable<OrderDetail> details, OrderStatusHistory initialStatus, List<OrderVoucher>? orderVouchers);
    }
}

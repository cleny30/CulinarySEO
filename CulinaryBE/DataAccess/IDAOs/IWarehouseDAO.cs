using BusinessObject.Models.Entity;

namespace DataAccess.IDAOs
{
    public interface IWarehouseDAO
    {
        Task<Warehouse?> GetWarehouseByCityAsync(string city);
    }
}

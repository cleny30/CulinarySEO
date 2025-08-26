using BusinessObject.Models.Entity;

namespace DataAccess.IDAOs
{
    public interface IWarehouseDAO
    {
        Task<List<Warehouse>> GetWarehousesAsync();
        Task<Warehouse?> GetWarehouseByCityAsync(string city);
    }
}

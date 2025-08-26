using BusinessObject.Models.Dto;
namespace ServiceObject.IServices
{
    public interface IWarehouseService
    {
        Task<List<GetWarehouse>> GetWarehousesAsync();
    }
}

using BusinessObject.Models.Dto;

namespace ServiceObject.IServices
{
    public interface IManagerService
    {
        Task<bool> AddManager(ManagerDto addManagerDto);
        Task<bool> UpdateManager(ManagerDto updateManagerDto);
        Task<bool> DeleteManager(Guid managerId);
        Task<IEnumerable<ManagerDto>> GetManagers();
        Task<ManagerDto?> GetManagerById(Guid managerId);
    }
}

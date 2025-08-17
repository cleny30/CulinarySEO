using BusinessObject.Models.Dto;

namespace ServiceObject.IServices
{
    public interface IManagerService
    {
        Task<bool> AddManager(AddManagerDto addManagerDto);
    }
}

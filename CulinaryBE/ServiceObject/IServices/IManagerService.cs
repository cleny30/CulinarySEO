using BusinessObject.Models.Dto;
using BusinessObject.Models.Enum;

namespace ServiceObject.IServices
{
    public interface IManagerService
    {
        Task<AddNewManagerStatus> AddManager(AddManagerDto addManagerDto);
    }
}

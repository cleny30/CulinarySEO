using BusinessObject.Models.Dto;

namespace ServiceObject.IServices
{
    public interface IPermissionService
    {
        Task<HashSet<KeyValuePair<string, bool>>> GetUserPermissionsAsync(Guid userId);
        void ClearUserPermissionsCache(Guid userId);
        Task<IEnumerable<PermissionDto>> GetAllPermissions();
    }
}

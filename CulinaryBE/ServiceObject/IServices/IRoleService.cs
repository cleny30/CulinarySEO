using BusinessObject.Models.Dto;

namespace ServiceObject.IServices
{
    public interface IRoleService
    {
        Task AddRole(RoleDto roleDto);
        Task UpdateRole(RoleDto roleDto);
        Task<RoleDto?> GetRoleById(int roleId);
        Task<IEnumerable<RoleDto>> GetRoles();
        Task<byte[]> ExportRolesPermission();
    }
}

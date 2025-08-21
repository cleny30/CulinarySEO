using BusinessObject.Models.Entity;

namespace DataAccess.IDAOs
{
    public interface IRoleDAO
    {
        Task AddRole(Role role);
        Task UpdateRole(Role role);
        Task<Role?> GetRoleById(int roleId);
        Task<IEnumerable<Role>> GetRoles();
        Task<IEnumerable<RolePermission>> GetRolePermissions();
    }
}

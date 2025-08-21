using BusinessObject.Models.Entity;

namespace DataAccess.IDAOs
{
    public interface IPermissionDAO
    {
        Task<HashSet<KeyValuePair<string, bool>>> GetPermissionsByCustomerIdAsync(Guid userId);
        Task<IEnumerable<Permission>> GetPermissions();
    }
}

using BusinessObject.AppDbContext;
using BusinessObject.Models.Enum;
using DataAccess.IDAOs;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DataAccess.DAOs
{
    public class PermissionDAO : IPermissionDAO
    {
        private readonly CulinaryContext _context;
        private readonly ILogger<PermissionDAO> _logger;

        public PermissionDAO(CulinaryContext context, ILogger<PermissionDAO> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<HashSet<KeyValuePair<string, bool>>> GetPermissionsByCustomerIdAsync(Guid userId)
        {
            try
            {
                var permissions = await _context.Managers
                .AsNoTracking()
                .Where(u => u.ManagerId == userId)
                .SelectMany(u => u.Role.RolePermissions.Select(rp =>
                    new KeyValuePair<string, bool>(
                        rp.Permission.PermissionName,
                        u.Revoked == true && u.Status != UserStatus.Suspended
                    )))
                .ToHashSetAsync();
                _logger.LogInformation("Retrieved {Count} permissions for ManagerId: {UserId}", permissions.Count, userId);

                return permissions;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while retrieving permissions for ManagerId: {UserId}", userId);
                return new HashSet<KeyValuePair<string, bool>>(); // Trả về tập rỗng nếu lỗi
            }

        }
    }
}

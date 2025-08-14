using DataAccess.IDAOs;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using ServiceObject.IServices;

namespace ServiceObject.Services
{
    public class PermissionService : IPermissionService
    {
        private readonly IPermissionDAO _permissionDAO;
        private readonly IMemoryCache _cache;
        private readonly ILogger<PermissionService> _logger;

        public PermissionService(IPermissionDAO permissionDAO, IMemoryCache cache, ILogger<PermissionService> logger)
        {
            _permissionDAO = permissionDAO;
            _cache = cache;
            _logger = logger;
        }

        public async Task<HashSet<KeyValuePair<string, bool>>> GetUserPermissionsAsync(Guid userId)
        {
            string cacheKey = $"permissions:{userId}";

            try
            {
                if (_cache.TryGetValue(cacheKey, out HashSet<KeyValuePair<string, bool>> cachedPermissions))
                {
                    _logger.LogInformation("Cache hit for permissions of UserId: {UserId}", userId);
                    return cachedPermissions;
                }

                _logger.LogInformation("Cache miss for permissions of UserId: {UserId}. Querying database...", userId);

                var permissions = await _permissionDAO.GetPermissionsByCustomerIdAsync(userId);

                if (permissions == null || permissions.Count == 0)
                {
                    _logger.LogWarning("No permissions found for UserId: {UserId}", userId);
                }
                else
                {
                    _logger.LogInformation("Retrieved {Count} permissions for UserId: {UserId}", permissions.Count, userId);
                }

                _cache.Set(cacheKey, permissions, TimeSpan.FromMinutes(30));

                return permissions;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving permissions for UserId: {UserId}", userId);
                return new HashSet<KeyValuePair<string, bool>>(); // Tránh throw ra ngoài
            }
        }

        public void ClearUserPermissionsCache(Guid userId)
        {
            _cache.Remove($"permissions:{userId}");
            _logger.LogInformation("Remove permissions for UserId: {UserId}", userId);
        }
    }
}

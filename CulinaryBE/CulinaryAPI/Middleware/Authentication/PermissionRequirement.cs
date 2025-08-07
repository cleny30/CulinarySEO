using Microsoft.AspNetCore.Authorization;

namespace CulinaryAPI.Middleware.Authentication
{
    public class PermissionRequirement : IAuthorizationRequirement
    {
        public IEnumerable<string> Permissions { get; }
        public bool RequireAllPermissions { get; }

        public PermissionRequirement(IEnumerable<string> permissions, bool requireAllPermissions = false)
        {
            Permissions = permissions;
            RequireAllPermissions = requireAllPermissions;
        }
    }
}

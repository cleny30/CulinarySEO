using BusinessObject.Models.Enum;
using Microsoft.AspNetCore.Authorization;

namespace CulinaryAPI.Middleware.Authentication
{
    public class HasPermissionAttribute : AuthorizeAttribute
    {
        public HasPermissionAttribute(params PermissionAuth[] permissions)
        : this(false, permissions)
        {
        }

        public HasPermissionAttribute(bool requireAllPermissions, params PermissionAuth[] permissions)
            : base(policy: string.Join(",", permissions.Select(p => p.ToString())) + ";" + requireAllPermissions)
        {
        }
    }
}

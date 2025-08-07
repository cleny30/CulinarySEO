using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace CulinaryAPI.Middleware.Authentication
{
    public class PermissionAuthorizationHandler : AuthorizationHandler<PermissionRequirement>
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public PermissionAuthorizationHandler(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
        }

        protected override async Task HandleRequirementAsync(
            AuthorizationHandlerContext context,
            PermissionRequirement requirement)
        {
            string? userId = context.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

            if (!Guid.TryParse(userId, out Guid parseUserId))
            {
                return;
            }
            using IServiceScope scope = _serviceScopeFactory.CreateScope();

            //IPermissionRepository permissionRepository = scope.ServiceProvider.GetRequiredService<IPermissionRepository>();

            HashSet<KeyValuePair<string, bool>> permissions = null;

            if (permissions == null || !permissions.Any())
            {
                return; // Từ chối nếu không có quyền
            }

            // Kiểm tra nếu IsRevoked == true
            if (permissions.Any(kvp => kvp.Value))
            {
                return; // Quyền bị thu hồi (IsRevoked == true), từ chối ngay lập tức
            }

            if (!requirement.Permissions.Any() || requirement.Permissions.All(p => p == ""))
            {
                // Nếu không có quyền bị thu hồi, chấp nhận
                if (!permissions.Any(kvp => kvp.Value))
                {
                    context.Succeed(requirement);
                }
            }

            // Kiểm tra quyền theo logic RequireAllPermissions
            if (requirement.RequireAllPermissions)
            {
                if (requirement.Permissions.All(permission => permissions.Any(kvp => kvp.Key == permission && !kvp.Value)))
                {
                    context.Succeed(requirement); // Chấp nhận
                }
            }
            else
            {
                if (requirement.Permissions.Any(permission => permissions.Any(kvp => kvp.Key == permission && !kvp.Value)))
                {
                    context.Succeed(requirement); // Chấp nhận
                }
            }
        }
    }
}

using Microsoft.AspNetCore.Authorization;
using ServiceObject.IServices;
using System.Security.Claims;

namespace CulinaryAPI.Middleware.Authentication
{
    public class PermissionAuthorizationHandler : AuthorizationHandler<PermissionRequirement>
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly ILogger<PermissionAuthorizationHandler> _logger;

        public PermissionAuthorizationHandler(IServiceScopeFactory serviceScopeFactory, ILogger<PermissionAuthorizationHandler> logger)
        {
            _serviceScopeFactory = serviceScopeFactory;
            _logger = logger;
        }

        protected override async Task HandleRequirementAsync(
            AuthorizationHandlerContext context,
            PermissionRequirement requirement)
        {

            //1. Get userId from Claim
            var userIdClaim = context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (!Guid.TryParse(userIdClaim, out Guid userId))
            {
                _logger.LogWarning("Authorization failed: Invalid or missing UserId claim.");
                return;
            }

            // 2. Get service from DI
            using IServiceScope scope = _serviceScopeFactory.CreateScope();

            var permissionService = scope.ServiceProvider.GetRequiredService<IPermissionService>();

            var permissions = await permissionService.GetUserPermissionsAsync(userId);

            if (!permissions.Any())
                return;

            if (permissions.Any(kvp => kvp.Value))
                return;

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

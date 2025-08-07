using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;

namespace CulinaryAPI.Middleware.Authentication
{
    public class PermissionAuthorizationPolicyProvider : DefaultAuthorizationPolicyProvider
    {
        public PermissionAuthorizationPolicyProvider(IOptions<AuthorizationOptions> options)
            : base(options)
        {
        }

        public override async Task<AuthorizationPolicy?> GetPolicyAsync(string policyName)
        {
            // Kiểm tra xem policy đã tồn tại chưa
            AuthorizationPolicy? policy = await base.GetPolicyAsync(policyName);

            if (policy is not null)
            {
                return policy;
            }

            // Phân tích policyName để lấy các permission và tùy chọn yêu cầu tất cả các permission
            var parts = policyName.Split(';');
            var permissionNames = parts[0].Split(',');
            bool requireAllPermissions = parts.Length > 1 && bool.Parse(parts[1]);

            // Tạo một yêu cầu permission mới
            var requirement = new PermissionRequirement(permissionNames, requireAllPermissions);

            // Xây dựng policy với yêu cầu permission
            return new AuthorizationPolicyBuilder()
                .AddRequirements(requirement)
                .Build();
        }
    }
}

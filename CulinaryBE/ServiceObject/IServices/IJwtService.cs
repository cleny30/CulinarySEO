using BusinessObject.Models.Dto;
using System.Security.Claims;

namespace ServiceObject.IServices
{
    public interface IJwtService
    {
        Task<string> GenerateJwtToken(AccountData accountData);
        public string GenerateRefreshToken();
        ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
    }
}

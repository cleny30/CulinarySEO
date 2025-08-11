using BusinessObject.Models.Dto;
using BusinessObject.Models.Entity;

namespace DataAccess.IDAOs
{
    public interface ICustomerDAO
    {
        Task<AccountData> VerifyAccountAsync(LoginAccountModel model);
        Task<string> SaveRefreshTokenAsync(Guid userId, string refreshToken, DateTime expiryDate);
        Task<Customer?> GetRefreshTokenAsync(string refreshToken);
        Task RevokeRefreshTokenAsync(string refreshToken);
    }
}

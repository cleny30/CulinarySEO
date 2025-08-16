using BusinessObject.Models.Dto;
using BusinessObject.Models.Entity;

namespace DataAccess.IDAOs
{
    public interface IManagerDAO
    {
        Task<AccountData> VerifyAccountAsync(LoginAccountModel model);
        Task<string> SaveRefreshTokenAsync(Guid userId, string refreshToken, DateTime expiryDate);
        Task<Manager?> GetRefreshTokenAsync(string refreshToken);
        Task RevokeRefreshTokenAsync(string refreshToken);
        Task<bool> AddManager(Manager model);
        Task<bool> IsEmailExist(string email);
        Task<bool> IsPhoneExist(string phone);
    }
}

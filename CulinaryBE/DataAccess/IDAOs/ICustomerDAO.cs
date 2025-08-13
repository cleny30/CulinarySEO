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
        Task<bool> IsEmailExist(string email);
        Task<bool> IsUsernameExist(string username);
        Task<bool> AddNewCustomer(Customer customer);
        Task<bool> UpdateCustomerAsync(Customer customer);
    }
}

using BusinessObject.Models.Dto;
using BusinessObject.Models.Entity;

namespace DataAccess.IDAOs
{
    public interface IManagerDAO
    {
        Task<AccountData> VerifyAccountAsync(LoginAccountModel model);
    }
}

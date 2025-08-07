using BusinessObject.Models.Dto;

namespace ServiceObject.IServices
{
    public interface IAuthService
    {
        Task<string> GenerateJwtToken(AccountData accountData);
        Task<AccountData> VerifyManager(LoginAccountModel loginAccountModel);
    }
}

using BusinessObject.Models.Dto;

namespace ServiceObject.IServices
{
    public interface IAuthService
    {
        Task<AccountData> VerifyManager(LoginAccountModel loginAccountModel);
    }
}

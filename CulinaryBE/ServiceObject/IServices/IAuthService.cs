using BusinessObject.Models.Dto;

namespace ServiceObject.IServices
{
    public interface IAuthService
    {
        Task<LoginResponse> VerifyManager(LoginAccountModel loginAccountModel);
        Task<LoginResponse> VerifyCustomer(LoginAccountModel loginAccountModel);
        Task<LoginResponse> RefreshTokenManagerAsync(string accessToken, string refreshToken);
        Task<LoginResponse> RefreshTokenCustomerAsync(string accessToken, string refreshToken);
    }
}

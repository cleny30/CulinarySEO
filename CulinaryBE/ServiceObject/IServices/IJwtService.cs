using BusinessObject.Models.Dto;

namespace ServiceObject.IServices
{
    public interface IJwtService
    {
        Task<string> GenerateJwtToken(AccountData accountData);
    }
}

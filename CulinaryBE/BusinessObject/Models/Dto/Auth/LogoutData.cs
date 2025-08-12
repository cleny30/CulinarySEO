using BusinessObject.Models.Enum;

namespace BusinessObject.Models.Dto
{
    public record LogoutData(string RefreshToken, AccountType AccountType);

}

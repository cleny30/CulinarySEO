using BusinessObject.Models.Enum;

namespace BusinessObject.Models.Dto
{
    public record TokenData(Guid UserId, string RefreshToken, DateTime Expiry, AccountType AccountType);

}

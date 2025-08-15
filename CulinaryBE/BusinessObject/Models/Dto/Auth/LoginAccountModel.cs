using System.ComponentModel.DataAnnotations;

namespace BusinessObject.Models.Dto
{
    public class LoginAccountModel
    {

        [MaxLength(50)]
        public string? Email { get; set; }

        [MaxLength(35)]
        public string? Password { get; set; }

        [MaxLength(35)]
        public string? RePassword { get; set; }

        [MaxLength(35)]
        public string? OldPassword { get; set; }
    }

    public class GoogleLoginRequest
    {
        public string IdToken { get; set; }
    }
}

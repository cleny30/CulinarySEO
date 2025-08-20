using BusinessObject.Models.Enum;
using System.ComponentModel.DataAnnotations;

namespace BusinessObject.Models.Dto
{
    public class ManagerDto
    {
        public Guid ManagerId { get; set; }

        [Required]
        [MaxLength(255)]
        public string Password { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]
        public string Email { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]
        public string FullName { get; set; } = string.Empty;

        [Required]
        [MaxLength(20)]
        public string Phone { get; set; } = string.Empty;

        public int RoleId { get; set; } = 2;

        public UserStatus Status { get; set; }
    }
}

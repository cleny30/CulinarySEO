using System.ComponentModel.DataAnnotations;

namespace BusinessObject.Models.Dto
{
    public class RoleDto
    {
        public int RoleId { get; set; } = 0;
        [Required]
        public string RoleName { get; set; } = string.Empty;

        [Required]
        public string Description { get; set; } = string.Empty;
    }
}

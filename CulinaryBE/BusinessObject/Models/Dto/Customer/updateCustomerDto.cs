using System.ComponentModel.DataAnnotations;

namespace BusinessObject.Models.Dto.Customer
{
    public class UpdateCustomerDto
    {
        [Required]
        public Guid CustomerId { get; set; }

        [MaxLength(100)]
        public string FullName { get; set; } = string.Empty;

        [MaxLength(100)]
        public string Email { get; set; } = string.Empty;

        [MaxLength(20)]
        public string Phone { get; set; } = string.Empty;

        [MaxLength(255)]
        public string? ProfilePic { get; set; }
    }
}
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusinessObject.Models.Entity
{
    [Table("permissions")]
    public class Permission
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("permission_id")]
        public int PermissionId { get; set; }

        [Required]
        [MaxLength(50)]
        [Column("permission_name")]
        public string PermissionName { get; set; } = string.Empty;

        [Column("description")]
        public string? Description { get; set; }

        // Navigation properties
        public virtual ICollection<RolePermission>? RolePermissions { get; set; }
    }
}

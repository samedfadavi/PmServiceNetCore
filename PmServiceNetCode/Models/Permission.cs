using System.ComponentModel.DataAnnotations;

namespace PmServiceNetCode.Models
{
    public class Permission
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(150)]
        public string Name { get; set; } = string.Empty;

        public string? Description { get; set; }
        public ICollection<RolePermission> RolePermissions { get; set; }
    = new List<RolePermission>();
    }
}
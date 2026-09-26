using System.ComponentModel.DataAnnotations;

namespace PmServiceNetCode.Models
{
    public class Endpoint
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(10)]
        public string HttpMethod { get; set; } = string.Empty;

        [Required]
        [MaxLength(250)]
        public string Route { get; set; } = string.Empty;

        public string? Description { get; set; }

        public ICollection<EndpointPermission> EndpointPermissions { get; set; }
            = new List<EndpointPermission>();
    }
}
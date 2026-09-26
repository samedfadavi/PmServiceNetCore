namespace PmServiceNetCode.Models
{
    public class EndpointPermission
    {
        public int EndpointId { get; set; }
        public int PermissionId { get; set; }

        public Endpoint Endpoint { get; set; } = null!;
        public Permission Permission { get; set; } = null!;
    }
}
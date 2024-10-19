namespace AuctionManagementAPI.Models
{
    public class RolePermission
    {
        public int RolePermissionId { get; set; }
        public int RoleId { get; set; }
        public int PermissionId { get; set; }

        public UserRole UserRole { get; set; }
        public Permission Permission { get; set; }
    }

}

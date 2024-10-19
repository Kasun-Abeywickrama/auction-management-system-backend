namespace AuctionManagementAPI.Models
{
    public class UserRole
    {
        public int UserRoleId { get; set; }
        public string Role { get; set; }

        public ICollection<RolePermission> RolePermissions { get; set; }
        public ICollection<User> Users { get; set; }
    }

}

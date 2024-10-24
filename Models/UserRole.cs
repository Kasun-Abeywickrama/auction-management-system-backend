namespace AuctionManagementAPI.Models
{
    public class UserRole
    {
        public int UserRoleId { get; set; }
        public required string Role { get; set; }

        // Navigation properties
        public ICollection<User>? Users { get; set; }
        public ICollection<Permission>? Permissions { get; set; }
    }
}

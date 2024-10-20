namespace AuctionManagementAPI.Models
{
    public class Permission
    {
        public int PermissionId { get; set; }
        public required string Name { get; set; }
        public string? Description { get; set; }

        // Navigation properties
        public ICollection<UserRole>? UserRoles { get; set; }
    }
}

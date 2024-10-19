namespace YourNamespace.Models
{
    public class UserRole
    {
        public int UserRoleID { get; set; }
        public string Role { get; set; }

        public ICollection<User> Users { get; set; }
        public ICollection<RolePermission> RolePermissions { get; set; }
    }
}

namespace YourNamespace.Models
{
    public class RolePermission
    {
        public int RolePermissionID { get; set; }
        public int RoleID { get; set; }
        public int PermissionID { get; set; }

        public UserRole UserRole { get; set; }
        public Permission Permission { get; set; }
    }
}

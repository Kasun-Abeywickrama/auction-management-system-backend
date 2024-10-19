namespace YourNamespace.Models
{
    public class Permission
    {
        public int PermissionID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public ICollection<RolePermission> RolePermissions { get; set; }
    }
}

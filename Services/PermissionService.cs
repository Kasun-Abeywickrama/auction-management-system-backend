using AuctionManagementAPI.Data;
using AuctionManagementAPI.Models;

namespace AuctionManagementAPI.Services
{
    public class PermissionService
    {
        private readonly AuctionContext _context;

        public PermissionService(AuctionContext context)
        {
            _context = context;
        }

        public bool HasPermission(User user, string permissionName)
        {
            return user.UserRole.Permissions.Any(p => p.Name == permissionName);
        }
    }
}

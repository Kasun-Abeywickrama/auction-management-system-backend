using AuctionManagementAPI.Data;
using Microsoft.EntityFrameworkCore;

namespace AuctionManagementAPI.Services
{
    public class PermissionService
    {
        private readonly AuctionContext _context;

        public PermissionService(AuctionContext context)
        {
            _context = context;
        }

        public bool UserHasPermission(string userId, string permissionName)
        {
            // Fetch the user's role(s) 
            var userRoleIds = _context.Users
                .Where(u => u.UserId.ToString() == userId)
                .Select(u => u.UserRoleId)
                .ToList();

            // Fetch the permissions of the user's role(s)
            var permissions = _context.UserRoles
                .Include(ur => ur.Permissions)
                .Where(ur => userRoleIds.Contains(ur.UserRoleId))
                .SelectMany(ur => ur.Permissions)
                .Select(p => p.Name)
                .ToList();

            // Check if the user has the required permission
            return permissions.Contains(permissionName);
        }
    }
}

using AuctionManagementAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using System.Security.Claims;

namespace AuctionManagementAPI.Attributes
{
    public class HasPermissionAttribute : AuthorizeAttribute, IAuthorizationFilter
    {
        private readonly string _permission;

        public HasPermissionAttribute(string permission)
        {
            _permission = permission;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            // Check if the user is authenticated
            if (!context.HttpContext.User.Identity.IsAuthenticated)
            {
                context.Result = new Microsoft.AspNetCore.Mvc.ForbidResult();
                return;
            }

            // Get UserId from claims
            var userId = context.HttpContext.User.FindFirstValue("UserId");
            var userRole = context.HttpContext.User.FindFirstValue("Role");

            // print the user's role to the console
            System.Console.WriteLine($"User Role: {userRole}");

            // if this user's role is admin give the access anyway.
            if (userRole == "admin")
            {
                return;
            }


            // Fetch the user's permissions from the database
            var permissionService = context.HttpContext.RequestServices.GetService<PermissionService>();
            var hasPermission = permissionService.UserHasPermission(userId, _permission);

            if (!hasPermission)
            {
                //context.Result = new Microsoft.AspNetCore.Mvc.ForbidResult();

                // send a 403 status code with th meaaage, you do not have permission to access this resource.
                context.Result = new Microsoft.AspNetCore.Mvc.StatusCodeResult(403);

            }
        }
    }
}

using AuctionManagementAPI.Models;
using AuctionManagementAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace AuctionManagementAPI.Attributes
{
    public class HasPermissionAttribute : TypeFilterAttribute
    {
        public HasPermissionAttribute(string permission) : base(typeof(HasPermissionFilter))
        {
            Arguments = new object[] { permission };
        }
    }

    public class HasPermissionFilter : IAuthorizationFilter
    {
        private readonly string _permission;
        private readonly PermissionService _permissionService;

        public HasPermissionFilter(string permission, PermissionService permissionService)
        {
            _permission = permission;
            _permissionService = permissionService;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var user = (User)context.HttpContext.Items["User"];
            if (!_permissionService.HasPermission(user, _permission))
            {
                context.Result = new ForbidResult();
            }
        }
    }
}

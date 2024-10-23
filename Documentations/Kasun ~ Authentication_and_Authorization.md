
## How to use `[Authorize]` and `[HasPermission("permission_name")]` in Controllers

<hr> 

## Overview
In our auction management system, we utilize the `[Authorize]` attribute for authentication and the `[HasPermission("permission_name")]` attribute for authorization checks on specific actions within our controllers.

## How to Use

1. **Add the `[Authorize]` Attribute**
   - This attribute ensures that the user is authenticated before accessing the controller or action.
   - Example:
     ```csharp
     [Authorize]
     public class AuctionController : ControllerBase
     {
         // Controller actions here
     }
     ```

2. **Add the `[HasPermission("permission_name")]` Attribute**
   - This attribute checks if the authenticated user has the specified permission to access the action.
   - Replace `"permission_name"` with the appropriate permission string.
   - Example:
     ```csharp
     [Authorize]
     [HasPermission("to_create_auction")]
     [HttpPost]
     public IActionResult CreateAuction([FromBody] AuctionModel auction)
     {
         // Action logic here
         return Ok();
     }
     ```

## Permission Strings
Below are the permission strings available in our system that you can use with the `[HasPermission]` attribute:

### Structure

```bash 
to_Action_model
```

### Actions

| Permission String                     |
|---------------------------------------|
| `to_create_auction`                  |
| `to_update_auction`                  |
| `to_delete_auction`                  |
| `to_get_all_auctions`                |
| `to_get_auction_by_id`               |
| `to_create_bid`                      |
| `to_update_bid`                      |
| `to_delete_bid`                      |
| `to_get_all_bids`                    |
| `to_get_bid_by_id`                   |
| `to_create_auction_schedule`         |
| `to_update_auction_schedule`         |
| `to_delete_auction_schedule`         |
| `to_get_all_auction_schedules`       |
| `to_get_auction_schedule_by_id`      |
| `to_create_label`                    |
| `to_update_label`                    |
| `to_delete_label`                    |
| `to_get_all_labels`                  |
| `to_get_label_by_id`                 |
| `to_create_product`                  |
| `to_update_product`                  |
| `to_delete_product`                  |
| `to_get_all_products`                |
| `to_get_product_by_id`               |
| `to_create_payment`                  |
| `to_update_payment`                  |
| `to_delete_payment`                  |
| `to_get_all_payments`                |
| `to_get_payment_by_id`               |
| `to_create_category`                 |
| `to_update_category`                 |
| `to_delete_category`                 |
| `to_get_all_categories`              |
| `to_get_category_by_id`              |
| `to_create_transaction`              |
| `to_update_transaction`              |
| `to_delete_transaction`              |
| `to_get_all_transactions`            |
| `to_get_transaction_by_id`           |
| `to_create_user`                     |
| `to_update_user`                     |
| `to_delete_user`                     |
| `to_get_all_users`                   |
| `to_get_user_by_id`                  |
| `to_create_report`                   |
| `to_update_report`                   |
| `to_delete_report`                   |
| `to_get_all_reports`                 |
| `to_get_report_by_id`                |
| `to_create_user_role`                |
| `to_update_user_role`                |
| `to_delete_user_role`                |
| `to_get_all_user_roles`              |
| `to_get_user_role_by_id`             |
| `to_create_permission`                |
| `to_update_permission`                |
| `to_delete_permission`                |
| `to_get_all_permissions`              |
| `to_get_permission_by_id`            |
| `to_create_user_profile`             |
| `to_update_user_profile`             |
| `to_delete_user_profile`             |
| `to_get_all_user_profiles`           |
| `to_get_user_profile_by_id`          |
| `to_create_notification`              |
| `to_update_notification`              |
| `to_delete_notification`              |
| `to_get_all_notifications`            |
| `to_get_notification_by_id`          |
| `to_create_activity_log`             |
| `to_update_activity_log`             |
| `to_delete_activity_log`             |
| `to_get_all_activity_logs`           |
| `to_get_activity_log_by_id`          |
| `to_create_otp`                      |
| `to_update_otp`                      |
| `to_delete_otp`                      |
| `to_get_all_otps`                    |
| `to_get_otp_by_id`                   |

## Example of Combined Usage
Here’s a complete example of a controller that uses both attributes:

```csharp
[Authorize]
[ApiController]
[Route("api/[controller]")]
public class AuctionController : ControllerBase
{
    [HttpPost]
    [HasPermission("to_create_auction")]
    public IActionResult CreateAuction([FromBody] AuctionModel auction)
    {
        // Logic for creating an auction
        return Ok();
    }

    [HttpGet]
    [HasPermission("to_get_all_auctions")]
    public IActionResult GetAuctions()
    {
        // Logic for getting all auctions
        return Ok();
    }
}
```

## Summary
- Use `[Authorize]` to ensure the user is authenticated.
- Use `[HasPermission("permission_name")]` to enforce specific permissions for actions.
- Refer to the permission strings table for available permissions.

Feel free to reach out if you have any questions or need assistance!

---

You can adjust the format or content further based on your group's needs!


<hr>

<br>
<br>

# (Optional to Read) - Detailed Explanation of the Process Flow

Here’s a brief explanation of how your auction management system handles a client request for accessing a controller with a permission check using the `[HasPermission]` attribute. The explanation covers the relevant files and folders:

### 1. **Client Request**
When a client (like a frontend application) wants to access a resource, it sends a request to a specific API endpoint, e.g., `GET /api/permissions`. This request includes the JWT in the `Authorization` header for authentication.

### 2. **Middleware Configuration**
- **File**: `Program.cs`
- **Folder**: `AuctionManagementAPI`
  
When the request hits the server, the middleware configured in `Program.cs` handles authentication and authorization:
- The `app.UseAuthentication()` method checks the JWT token to ensure the user is authenticated.
- The `app.UseAuthorization()` method checks if the user has the required permissions based on the specified roles.

### 3. **Controller Action**
- **File**: `PermissionsController.cs`
- **Folder**: `Controllers`
  
The controller action might look like this:

```csharp
[Authorize]
[HasPermission("to_get_all_permissions")]
[ApiController]
[Route("api/[controller]")]
public class PermissionsController : ControllerBase
{
    private readonly PermissionService _permissionService;

    public PermissionsController(PermissionService permissionService)
    {
        _permissionService = permissionService;
    }

    [HttpGet]
    public IActionResult GetAllPermissions()
    {
        var permissions = _permissionService.GetAllPermissions();
        return Ok(permissions);
    }
}
```

### 4. **Custom Authorization Attribute**
- **File**: `HasPermissionAttribute.cs`
- **Folder**: `Attributes`
  
The `[HasPermission("to_get_all_permissions")]` attribute is a custom authorization attribute that checks if the logged-in user has the required permission. It is implemented in the `HasPermissionAttribute` class:

```csharp
public class HasPermissionAttribute : Attribute, IAuthorizationFilter
{
    private readonly string _permissionName;

    public HasPermissionAttribute(string permissionName)
    {
        _permissionName = permissionName;
    }

    public void OnAuthorization(AuthorizationFilterContext context)
    {
        // Logic to check if the user has the permission
        var userId = context.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var userRoleId = ...; // Logic to get user role ID
        
        // Fetch user permissions based on role from the database
        var permissions = ...; // Logic to get permissions from PermissionService

        if (!permissions.Any(p => p.Name == _permissionName))
        {
            context.Result = new ForbidResult(); // User does not have permission
        }
    }
}
```

### 5. **Permission Check**
- **File**: `PermissionService.cs`
- **Folder**: `Services`
  
Inside the `PermissionService`, logic is implemented to fetch permissions associated with the user's role:

```csharp
public class PermissionService
{
    private readonly AuctionContext _context;

    public PermissionService(AuctionContext context)
    {
        _context = context;
    }

    public IEnumerable<Permission> GetUserPermissions(int userRoleId)
    {
        return _context.UserRoles
            .Include(ur => ur.Permissions)
            .FirstOrDefault(ur => ur.UserRoleId == userRoleId)?
            .Permissions;
    }
}
```

### 6. **Response**
If the user has the required permission (`to_get_all_permissions`), the `GetAllPermissions` action retrieves the list of permissions from the database and returns them in the response. If the user lacks the permission, a `403 Forbidden` response is sent.

### Summary of Process Flow
1. **Client sends a request** to access the permissions API with a JWT.
2. **Middleware authenticates** the JWT and checks user roles/permissions.
3. **Controller action** is executed if authorized.
4. **Custom attribute** checks if the user has the necessary permission.
5. **Service fetches permissions** based on the user role from the database.
6. **Response is sent** back to the client based on permission checks.

This flow ensures secure access control, allowing only authorized users to perform certain actions within your auction management system.
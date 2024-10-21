## How this authentication and authorization works 

### Top view

Request &nbsp; -> &nbsp; Program.cs &nbsp; -> &nbsp; Controller &nbsp; -> &nbsp; Response

<hr>

The life cycle of a request in your **.NET Core Web API** application with JWT-based authentication and role-based authorization follows these steps:

### 1. **Client Sends Request**
   - The client (ReactJS front-end) sends an HTTP request to the API, including the **JWT token** in the `Authorization` header.
   
   Example:
   ```javascript
   axios.get('/api/auction', {
     headers: {
       'Authorization': `Bearer ${token}`
     }
   });
   ```

### 2. **Request Received by API**
   The request hits the ASP.NET Core pipeline, starting with the `Program.cs` configuration.

   - **File:** `Program.cs`
   - Middleware like **UseAuthentication()** and **UseAuthorization()** is triggered.

### 3. **Authentication Middleware (JWT Validation)**
   The **JWT Bearer Authentication** middleware reads the JWT token from the `Authorization` header and validates it.

   - **File:** `Program.cs`
   - The JWT token is validated against the configured parameters (issuer, audience, expiration, and signing key).
   - If the token is invalid, the request is rejected, and a `401 Unauthorized` response is sent.

### 4. **Authorization (Role/Permission Checks)**
   If the token is valid, the **Authorization** middleware checks the user's role or permissions based on the attributes used on the controller/action methods.

   - **File:** `Controllers/AuctionController.cs`
     - `[Authorize(Roles = "Admin")]` checks if the user has the required role.
     - `[HasPermission("CreateAuction")]` checks if the user has the required permission (using the `PermissionService`).

   - **File:** `Attributes/HasPermissionAttribute.cs` and `Services/PermissionService.cs`
     - The `HasPermissionAttribute` uses the `PermissionService` to validate the user's permissions.

### 5. **Controller Action Execution**
   Once the user is authenticated and authorized, the API forwards the request to the corresponding controller action.

   - **File:** `Controllers/AuctionController.cs`
   - The requested action (`CreateAuction` or `GetAllAuctions`) is executed, handling business logic like creating an auction or fetching auctions.

### 6. **Response Sent Back to Client**
   After the action completes, the controller returns the result (e.g., auction data or success message), which is serialized into JSON and sent back to the client.

### Summary:
- **Program.cs** configures authentication and authorization.
- **TokenService.cs** generates JWT tokens.
- **AuthController.cs** handles user login and token generation.
- **AuctionController.cs** is protected by `[Authorize]` and `[HasPermission]` attributes to ensure only authorized users can access certain endpoints.
- **HasPermissionAttribute.cs** and **PermissionService.cs** check for permission-based access.



<br>

## Code

<br>

To build a robust and secure authentication and role-based authorization system in your .NET Core Web API with ReactJS, here’s a step-by-step guide. We’ll ensure that certain routes are protected and accessible only by authorized users based on their roles and permissions.

### **Step 1: Set Up Authentication (JWT)**
We will use **JWT (JSON Web Tokens)** for authentication. This will allow you to securely authenticate users and validate their access to resources.

#### **Install Required NuGet Packages**
In your `.NET` project, install the following packages:

```bash
dotnet add package Microsoft.AspNetCore.Authentication.JwtBearer
dotnet add package Microsoft.AspNetCore.Identity.EntityFrameworkCore
dotnet add package Microsoft.IdentityModel.Tokens
```

### **Step 2: Configure JWT Authentication**

#### **Program.cs**
Update your `Program.cs` to configure JWT authentication. You’ll also define the token validation parameters here.

```csharp
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Add DbContext
builder.Services.AddDbContext<AuctionContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add Authentication
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    };
});

// Authorization
builder.Services.AddAuthorization();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Enable authentication middleware
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
```

#### **appsettings.json**
Add the following JWT configuration to your `appsettings.json`:

```json
{
  "Jwt": {
    "Key": "YourSuperSecretKey",
    "Issuer": "YourApp",
    "Audience": "YourAppUsers",
    "ExpiresInMinutes": 60
  }
}
```

### **Step 3: Create a Token Service**

Create a `TokenService` in the `Services` folder to generate JWTs.

#### **File:** `Services/TokenService.cs`

```csharp
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AuctionManagementAPI.Services
{
    public class TokenService
    {
        private readonly IConfiguration _config;

        public TokenService(IConfiguration config)
        {
            _config = config;
        }

        public string GenerateToken(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                new Claim("UserId", user.UserId.ToString()),
                new Claim("Role", user.UserRole.Role)
            };

            var token = new JwtSecurityToken(
                _config["Jwt:Issuer"],
                _config["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddMinutes(Convert.ToDouble(_config["Jwt:ExpiresInMinutes"])),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
```

### **Step 4: Implement Authentication in Controller**

In your `Controllers` folder, create an `AuthController` that handles user login and JWT generation.

#### **File:** `Controllers/AuthController.cs`

```csharp
using AuctionManagementAPI.Data;
using AuctionManagementAPI.Models;
using AuctionManagementAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AuctionManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AuctionContext _context;
        private readonly TokenService _tokenService;

        public AuthController(AuctionContext context, TokenService tokenService)
        {
            _context = context;
            _tokenService = tokenService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            var user = await _context.Users
                .Include(u => u.UserRole)
                .FirstOrDefaultAsync(u => u.Email == loginDto.Email && u.Password == loginDto.Password); // Use proper hashing in production

            if (user == null)
            {
                return Unauthorized("Invalid credentials");
            }

            var token = _tokenService.GenerateToken(user);
            return Ok(new { token });
        }
    }

    public class LoginDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
```

### **Step 5: Protect Routes with Authorization**

You can now protect your controllers using the `[Authorize]` attribute. For role-based authorization, specify the role(s) that can access specific endpoints.

#### **File:** `Controllers/AuctionController.cs`

```csharp
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuctionManagementAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AuctionController : ControllerBase
    {
        // Allow only users with "Admin" role to access this route
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult CreateAuction()
        {
            // Auction creation logic
            return Ok("Auction created");
        }

        // Allow any authenticated user to access this route
        [HttpGet]
        public IActionResult GetAllAuctions()
        {
            // Fetch auctions logic
            return Ok("All auctions");
        }
    }
}
```

### **Step 6: Implement Permission-Based Access**

To implement permission-based access control, you can create a custom `Authorize` attribute that checks for specific permissions.

#### **File:** `Services/PermissionService.cs`

```csharp
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
```

#### **File:** `Attributes/HasPermissionAttribute.cs`

```csharp
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
```

#### **Usage in Controller:**

```csharp
using AuctionManagementAPI.Attributes;
using Microsoft.AspNetCore.Mvc;

namespace AuctionManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuctionController : ControllerBase
    {
        [HasPermission("CreateAuction")]
        [HttpPost]
        public IActionResult CreateAuction()
        {
            // Only users with "CreateAuction" permission can access this
            return Ok("Auction created");
        }
    }
}
```

### **Step 7: Front-End (ReactJS) Integration**

On the front-end, when a user logs in, store the JWT token in local storage or session storage. For each request to protected routes, include the token in the `Authorization` header.

#### **Example Axios Request:**

```javascript
import axios from 'axios';

const token = localStorage.getItem('token');

axios.get('/api/auction', {
  headers: {
    'Authorization': `Bearer ${token}`
  }
})
.then(response => {
  console.log(response.data);
})
.catch(error => {
  console.error("Error fetching auctions:", error);
});
```

---

This guide provides a complete structure for setting up authentication and authorization, handling role-based and permission-based access in a secure way. You can extend this further by adding password hashing and refreshing tokens.


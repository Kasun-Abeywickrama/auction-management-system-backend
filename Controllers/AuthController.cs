using AuctionManagementAPI.Attributes;
using AuctionManagementAPI.Models.DTOs.AuthDTOs;
using AuctionManagementAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;


namespace AuctionManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authService;
        private readonly TokenService _tokenService;

        public AuthController(AuthService authService, TokenService tokenService)
        {
            _authService = authService;
            _tokenService = tokenService;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            var user = await _authService.LoginAsync(loginDto);
            if (user == null)
            {
                return Unauthorized("Invalid credentials or user not verified.");
            }

            var token = _tokenService.GenerateToken(user);

            // return token, role, firstname and lastname
            return Ok(new { token, user.UserRole?.Role, user.FirstName, user.LastName });


            //return Ok(new { token });
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserDTO registerUserDTO)
        {
            var result = await _authService.RegisterAsync(registerUserDTO);
            if (result.Contains("User registered successfully"))
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("VerifyOtp")]
        public async Task<IActionResult> VerifyOtp([FromBody] VerifyOtpDTO verifyOtpDTO)

        {
            var result = await _authService.VerifyOtpAsync(verifyOtpDTO);
            if (result.Contains("successfully"))
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("ReGenerateOtp")]
        public async Task<IActionResult> ReGenerateOtp([FromBody] GenerateOtpDTO generateOtpDTO)
        {
            var result = await _authService.ReGenerateOtpAsync(generateOtpDTO);
            if (result.Contains("New OTP has been sent"))
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        // Request to reset password and use Forgot password also.
        [HttpPost("ResetPasswordRequest")]
        public async Task<IActionResult> ResetPasswordRequest([FromBody] ResetPasswordRequestDTO resetPasswordRequestDTO)
        {
            var result = await _authService.ResetPasswordRequestAsync(resetPasswordRequestDTO);
            if (result.Contains("An email has been sent to your email address with the OTP."))
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        // Reset password
        [HttpPost("ResetPassword")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordOtpDTO resetPasswordDTO)
        {
            var result = await _authService.ResetPasswordAsync(resetPasswordDTO);
            if (result.Contains("Password reset successfully."))
            {
                return Ok(result);
            }
            return BadRequest(result);
        }


        // Role controllers to create, update, delete and get roles.

        // Create role
        [HttpPost("CreateUserRole")]
        public async Task<IActionResult> CreateUserRole([FromBody] CreateUserRoleDTO createUserRoleDTO)
        {
            var result = await _authService.CreateRoleAsync(createUserRoleDTO);
            if (result.Contains("Role created successfully."))
            {
                return Ok(result);
            }
            return BadRequest(result);
        }


        // Update role
        [HttpPut("UpdateUserRole")]
        public async Task<IActionResult> UpdateUserRole([FromBody] UpdateUserRoleDTO updateUserRoleDTO)
        {
            var result = await _authService.UpdateRoleAsync(updateUserRoleDTO);
            if (result.Contains("Role updated successfully."))
            {
                return Ok(result);
            }
            return BadRequest(result);
        }


        // Delete role
        [HttpDelete("DeleteUserRole")]
        public async Task<IActionResult> DeleteUserRole([FromBody] DeleteUserRoleDTO deleteUserRoleDTO)
        {
            var result = await _authService.DeleteRoleAsync(deleteUserRoleDTO);
            if (result.Contains("Role deleted successfully."))
            {
                return Ok(result);
            }
            return BadRequest(result);
        }


        // Get all roles
        [HttpGet("GetUserRoles")]
        public async Task<IActionResult> GetUserRoles()
        {
            var result = await _authService.GetRolesAsync();
            if (result != null)
            {
                return Ok(result);
            }
            return BadRequest("No roles found.");
        }


        // Get role by id using from query string
        [HttpGet("GetUserRoleById")]
        public async Task<IActionResult> GetUserRoleById([FromQuery] int roleId)
        {
            var result = await _authService.GetRoleByIdAsync(roleId);
            if (result != null)
            {
                return Ok(result);
            }
            return BadRequest("Role not found.");
        }


        // permission controllers to only get all permissions.


        // Get all permissions
        [Authorize]
        [HttpGet("GetPermissions")]
        [HasPermission("to_get_all_permissions")]
        public async Task<IActionResult> GetPermissions()
        {

            // Extract UserId and Role from token claims
            var userId = User.FindFirstValue("UserId");
            var role = User.FindFirstValue("Role");

            // print the userId and role to the console
            Console.WriteLine("UserId: " + userId);
            Console.WriteLine("Role: " + role);

            var result = await _authService.GetPermissionsAsync();
            if (result != null)
            {
                return Ok(result);
            }
            return BadRequest("No permissions found.");
        }


    }
}

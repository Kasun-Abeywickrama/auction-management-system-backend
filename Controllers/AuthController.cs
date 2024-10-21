using AuctionManagementAPI.Data;
using AuctionManagementAPI.Models;
using AuctionManagementAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AuctionManagementAPI.Models.DTOs;

namespace AuctionManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AuctionContext _context;
        private readonly TokenService _tokenService;
        private readonly EmailService _emailService;


        public AuthController(AuctionContext context, TokenService tokenService, EmailService emailService)
        {
            _context = context;
            _tokenService = tokenService;
            _emailService = emailService;
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            var user = await _context.Users
                .Include(u => u.UserRole)
                .FirstOrDefaultAsync(u => u.Email == loginDto.Email);

            if (user == null)
            {
                return Unauthorized("Invalid credentials");
            }

            // Check if the user is verified
            if (!user.IsVerified)
            {
                return Unauthorized("User is not verified.");
            }

            // Use the stored salt to hash the provided password
            string hashedPassword = _tokenService.HashPasswordWithSalt(loginDto.Password, user.PasswordSalt);

            if (hashedPassword != user.Password)
            {
                return Unauthorized("Invalid credentials");
            }

            // Generate token after successful password verification
            var token = _tokenService.GenerateToken(user);
            return Ok(new { token });
        }






















        // POST: api/Auth/Register
        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserDTO registerUserDTO)
        {
            // Check if the user already exists
            var existingUser = _context.Users.FirstOrDefault(u => u.Email == registerUserDTO.Email);
            if (existingUser != null)
            {
                return BadRequest("User with this email already exists.");
            }

            // Hash the password
            string salt;
            string hashedPassword = _tokenService.HashPassword(registerUserDTO.Password, out salt);

            // Find the "buyer" role
            var buyerRole = _context.UserRoles.FirstOrDefault(r => r.Role == "buyer");
            if (buyerRole == null)
            {
                return BadRequest("Buyer role not found.");
            }

            // Create new user
            var newUser = new User
            {
                FirstName = registerUserDTO.FirstName,
                LastName = registerUserDTO.LastName,
                Email = registerUserDTO.Email,
                Password = hashedPassword,
                PasswordSalt = salt,
                UserRoleId = buyerRole.UserRoleId,  // Assign the buyer role
                IsVerified = false  // Not verified yet
            };

            // Add user to the database
            _context.Users.Add(newUser);
            await _context.SaveChangesAsync();

            // Generate OTP
            string otpCode = new Random().Next(100000, 999999).ToString();
            var otp = new Otp
            {
                OtpType = "Email Verification",
                OtpCode = otpCode,
                GeneratedAt = DateTime.Now,
                ExpiresAt = DateTime.Now.AddMinutes(10),
                IsUsed = false,
                UserId = newUser.UserId,
                User = newUser
            };

            // Add OTP to the database
            _context.Otps.Add(otp);
            await _context.SaveChangesAsync();

            // Send OTP via email
            _emailService.SendOtpEmail(newUser.Email, otpCode);

            return Ok("User registered successfully. Please check your email for the OTP.");
        }

        // POST: api/Auth/VerifyOtp
        [HttpPost("VerifyOtp")]
        public async Task<IActionResult> VerifyOtp(int userId, string otpCode)
        {
            // Find the user and OTP
            var user = await _context.Users.FindAsync(userId);
            var otp = _context.Otps.FirstOrDefault(o => o.UserId == userId && o.OtpCode == otpCode && !o.IsUsed && o.ExpiresAt > DateTime.Now);

            if (user == null || otp == null)
            {
                return BadRequest("Invalid OTP or User not found.");
            }

            // Mark the OTP as used
            otp.IsUsed = true;
            _context.Otps.Update(otp);

            // Mark the user as verified
            user.IsVerified = true;
            _context.Users.Update(user);

            await _context.SaveChangesAsync();

            return Ok("OTP verified successfully. You can now log in.");
        }






        // POST: api/Auth/RegenerateOtp
        [HttpPost("RegenerateOtp")]
        public async Task<IActionResult> RegenerateOtp([FromBody] RegenerateOtpDTO regenerateOtpDTO)
        {
            // Find the user by email
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == regenerateOtpDTO.Email);
            if (user == null)
            {
                return BadRequest("User not found.");
            }

            // Check if the user is already verified
            if (user.IsVerified)
            {
                return BadRequest("User is already verified.");
            }

            // Mark all previous unused OTPs as expired
            var existingOtps = _context.Otps.Where(o => o.UserId == user.UserId && !o.IsUsed && o.ExpiresAt > DateTime.Now).ToList();
            foreach (var otp in existingOtps)
            {
                otp.IsUsed = true;  // Mark as used
                _context.Otps.Update(otp);
            }

            // Generate a new OTP
            string otpCode = new Random().Next(100000, 999999).ToString();
            var newOtp = new Otp
            {
                OtpType = "Email Verification",
                OtpCode = otpCode,
                GeneratedAt = DateTime.Now,
                ExpiresAt = DateTime.Now.AddMinutes(10),  // New OTP expires in 10 minutes
                IsUsed = false,
                UserId = user.UserId,
                User = user
            };

            // Add new OTP to the database
            _context.Otps.Add(newOtp);
            await _context.SaveChangesAsync();

            // Send new OTP via email
            _emailService.SendOtpEmail(user.Email, otpCode);

            return Ok("New OTP has been sent to your email.");
        }
























    }

    
}

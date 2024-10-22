using AuctionManagementAPI.Data;
using AuctionManagementAPI.Models;
using AuctionManagementAPI.Models.DTOs;
using Microsoft.EntityFrameworkCore;
using System;

namespace AuctionManagementAPI.Services
{
    public class AuthService
    {
        private readonly AuctionContext _context;
        private readonly TokenService _tokenService;
        private readonly EmailService _emailService;

        public AuthService(AuctionContext context, TokenService tokenService, EmailService emailService)
        {
            _context = context;
            _tokenService = tokenService;
            _emailService = emailService;
        }

        public async Task<User?> LoginAsync(LoginDto loginDTO)
        {
            // check if user exists
            var user = await _context.Users
                .Include(u => u.UserRole)
                .FirstOrDefaultAsync(u => u.Email == loginDTO.Email);

            // if user is not found or not verified, return null
            if (user == null || !user.IsVerified)
            {
                return null;
            }

            // hash the incoming password
            string hashedPassword = _tokenService.HashPasswordWithSalt(loginDTO.Password, user.PasswordSalt);

            // check if password is correct with the hashed password
            if (hashedPassword != user.Password)
            {
                // increment failed login attempts
                user.FailedLoginAttempts += 1;

                // if failed login attempts is 5, lock the account
                if (user.FailedLoginAttempts >= 5)
                {
                    user.IsVerified = false;

                    // send email to user
                    _emailService.SendAccountLockedEmail(user.Email);
                }

                // update the user
                _context.Users.Update(user);
                await _context.SaveChangesAsync();

                return null;

            }

            // update last login date and reset failed login attempts for successful login
            user.LastLogin = DateTime.Now;

            // reset failed login attempts
            if (user.FailedLoginAttempts > 0)
            {
                user.FailedLoginAttempts = 0;
            }

            _context.Users.Update(user);
            await _context.SaveChangesAsync();

            return user;

        }

        public async Task<string> RegisterAsync(RegisterUserDTO registerUserDTO)
        {
            var existingUser = _context.Users.FirstOrDefault(u => u.Email == registerUserDTO.Email);
            if (existingUser != null)
            {
                return "User with this email already exists.";
            }

            string salt;
            string hashedPassword = _tokenService.HashPassword(registerUserDTO.Password, out salt);

            var buyerRole = _context.UserRoles.FirstOrDefault(r => r.Role == "buyer");
            if (buyerRole == null)
            {
                return "Buyer role not found.";
            }

            var newUser = new User
            {
                FirstName = registerUserDTO.FirstName,
                LastName = registerUserDTO.LastName,
                Email = registerUserDTO.Email,
                Password = hashedPassword,
                PasswordSalt = salt,
                UserRoleId = buyerRole.UserRoleId,
                IsVerified = false
            };

            _context.Users.Add(newUser);
            await _context.SaveChangesAsync();

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

            _context.Otps.Add(otp);
            await _context.SaveChangesAsync();

            _emailService.SendOtpEmail(newUser.Email, otpCode);
            return "User registered successfully. Please check your email for the OTP.";
        }

        public async Task<string> VerifyOtpAsync(VerifyOtpDTO verifyOtpDTO)
        {

            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == verifyOtpDTO.Email);

            if (user == null)
            {
                return "User not found.";
            }

            var otp = _context.Otps.FirstOrDefault(o => o.UserId == user.UserId && o.OtpCode == verifyOtpDTO.OtpCode && !o.IsUsed && o.ExpiresAt > DateTime.Now);

            if (otp == null) {
                return "Invalid OTP.";
            }

            otp.IsUsed = true;
            _context.Otps.Update(otp);

            user.IsVerified = true;
            _context.Users.Update(user);

            await _context.SaveChangesAsync();

            return "OTP verified successfully. You can now log in.";

        }

        public async Task<string> ReGenerateOtpAsync(GenerateOtpDTO generateOtpDTO)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == generateOtpDTO.Email);
            if (user == null)
            {
                return "User not found.";
            }

            if (user.IsVerified)
            {
                return "User is already verified.";
            }

            var existingOtps = _context.Otps.Where(o => o.UserId == user.UserId && !o.IsUsed && o.ExpiresAt > DateTime.Now).ToList();
            foreach (var otp in existingOtps)
            {
                otp.IsUsed = true;
                _context.Otps.Update(otp);
            }

            string otpCode = new Random().Next(100000, 999999).ToString();
            var newOtp = new Otp
            {
                OtpType = "Re-Sent OTP",
                OtpCode = otpCode,
                GeneratedAt = DateTime.Now,
                ExpiresAt = DateTime.Now.AddMinutes(10),
                IsUsed = false,
                UserId = user.UserId,
                User = user
            };

            _context.Otps.Add(newOtp);
            await _context.SaveChangesAsync();

            _emailService.SendOtpEmail(user.Email, otpCode);
            return "New OTP has been sent to your email.";
        }

        public async Task<string> ResetPasswordRequestAsync(ResetPasswordRequestDTO resetPasswordRequestDTO)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == resetPasswordRequestDTO.Email);
            if (user == null)
            {
                return "User not found.";
            }

            string otpCode = new Random().Next(100000, 999999).ToString();
            var otp = new Otp
            {
                OtpType = "OTP for Password Reset",
                OtpCode = otpCode,
                GeneratedAt = DateTime.Now,
                ExpiresAt = DateTime.Now.AddMinutes(10),
                IsUsed = false,
                UserId = user.UserId,
                User = user
            };

            _context.Otps.Add(otp);
            await _context.SaveChangesAsync();

            _emailService.SendOtpEmail(user.Email, otpCode);
            return "An email has been sent to your email address with the OTP.";
        }
    
        public async Task<string> ResetPasswordAsync(ResetPasswordOtpDTO resetPasswordDTO)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == resetPasswordDTO.Email);
            if (user == null)
            {
                return "User not found.";
            }

            var otp = _context.Otps.FirstOrDefault(o => o.UserId == user.UserId && o.OtpCode == resetPasswordDTO.OtpCode && !o.IsUsed && o.ExpiresAt > DateTime.Now);

            if (otp == null)
            {
                return "Invalid OTP.";
            }

            // Check if the NewPassword is the same as the ConfirmPassword
            if (resetPasswordDTO.NewPassword != resetPasswordDTO.ConfirmPassword)
            {
                return "Passwords do not match.";
            }

            string salt;
            string hashedPassword = _tokenService.HashPassword(resetPasswordDTO.NewPassword, out salt);

            user.Password = hashedPassword;
            user.PasswordSalt = salt;
            _context.Users.Update(user);

            otp.IsUsed = true;
            _context.Otps.Update(otp);

            await _context.SaveChangesAsync();

            return "Password reset successfully.";
        }

    }
}

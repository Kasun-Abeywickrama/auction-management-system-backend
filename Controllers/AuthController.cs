using AuctionManagementAPI.Models.DTOs;
using AuctionManagementAPI.Services;
using Microsoft.AspNetCore.Mvc;

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
            return Ok(new { token });
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

        [HttpPost("RegenerateOtp")]
        public async Task<IActionResult> RegenerateOtp([FromBody] RegenerateOtpDTO regenerateOtpDTO)
        {
            var result = await _authService.RegenerateOtpAsync(regenerateOtpDTO);
            if (result.Contains("New OTP has been sent"))
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}

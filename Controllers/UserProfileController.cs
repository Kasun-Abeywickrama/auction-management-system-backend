using AuctionManagementAPI.Models.DTOs.AuthDTOs;
using AuctionManagementAPI.Models.DTOs.UserProfileDTOs;
using AuctionManagementAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AuctionManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserProfileController : ControllerBase
    {

        private readonly UserProfileService _userProfileService;

        public UserProfileController(UserProfileService userProfileService)
        {
            this._userProfileService = userProfileService;
        }


        // api to create a user profile
        [HttpPost("CreateUserProfile")]
        [Authorize]
        public async Task<IActionResult> CreateUserProfile([FromBody] UserProfileDTO userProfileDTO)
        {

            // get the user id from the token
            var userId = User.FindFirstValue("UserId");

            if (userId == null){
                return BadRequest("User not found");
            }

            // convert the user id to an integer
            int uId = Int32.Parse(userId);

            var result = await _userProfileService.CreateUserProfileAsync(userProfileDTO, uId);
            if (result.Contains("User profile created successfully"))
            {
                return Ok(result);
            }
            return BadRequest(result);
        }


        // api to update a user profile
        [HttpPut("UpdateUserProfile")]
        [Authorize]
        public async Task<IActionResult> UpdateUserProfile([FromBody] UserProfileDTO userProfileDTO)
        {

            // get the user id from the token
            var userId = User.FindFirstValue("UserId");

            if (userId == null)
            {
                return BadRequest("User not found");
            }

            // convert the user id to an integer
            int uId = Int32.Parse(userId);

            var result = await _userProfileService.UpdateUserProfileAsync(userProfileDTO, uId);
            if (result.Contains("User profile updated successfully"))
            {
                return Ok(result);
            }
            return BadRequest(result);
        }


        //api to delete a user profile
        [HttpDelete("DeleteUserProfile")]
        [Authorize]
        public async Task<IActionResult> DeleteUserProfile()
        {

            // get the user id from the token
            var userId = User.FindFirstValue("UserId");

            if (userId == null)
            {
                return BadRequest("User not found");
            }

            // convert the user id to an integer
            int uId = Int32.Parse(userId);

            var result = await _userProfileService.DeleteUserProfileAsync(uId);
            if (result.Contains("User profile deleted successfully"))
            {
                return Ok(result);
            }
            return BadRequest(result);
        }


        // api to get a user profile
        [HttpGet("GetUserProfile")]
        [Authorize]
        public async Task<IActionResult> GetUserProfile()
        {

            // get the user id from the token
            var userId = User.FindFirstValue("UserId");

            if (userId == null)
            {
                return BadRequest("User not found");
            }

            // convert the user id to an integer
            int uId = Int32.Parse(userId);

            var result = await _userProfileService.GetUserProfileAsync(uId);
            if (result != null)
            {
                return Ok(result);
            }
            return BadRequest("User profile not found");
        }


        // api to get all user profiles
        [HttpGet("GetAllUserProfiles")]
        public async Task<IActionResult> GetAllUserProfiles()
        {
            var result = await _userProfileService.GetAllUserProfilesAsync();
            if (result != null)
            {
                return Ok(result);
            }
            return BadRequest("User profiles not found");
        }


    }
}

using AuctionManagementAPI.Data;
using AuctionManagementAPI.Models;
using AuctionManagementAPI.Models.DTOs.UserProfileDTOs;
using Microsoft.EntityFrameworkCore;


namespace AuctionManagementAPI.Services
{
    public class UserProfileService
    {
        private readonly AuctionContext _context;

        public UserProfileService(AuctionContext context)
        {
            _context = context;
        }

        public async Task<string> CreateUserProfileAsync(UserProfileDTO userProfileDTO, int userId)
        {
            // check if the user profile already exists
            var userProfile = await _context.UserProfiles.FirstOrDefaultAsync(x => x.UserId == userId);
            if (userProfile != null)
            {
                return "User profile already exists";
            }

            // get the user
            var user = await _context.Users.FirstOrDefaultAsync(x => x.UserId == userId);
            if (user == null)
            {
                return "User not found";
            }

            // create a new user profile
            var newUserProfile = new UserProfile
            {
                Address = userProfileDTO.Address,
                SellerRating = userProfileDTO.SellerRating,
                PaymentDetails = userProfileDTO.PaymentDetails,
                AdditionalData = userProfileDTO.AdditionalData,
                User = user,
                UserId = userId
            };

            // add the new user profile to the database
            await _context.UserProfiles.AddAsync(newUserProfile);
            await _context.SaveChangesAsync();

            return "User profile created successfully";

        }



        public async Task<string> UpdateUserProfileAsync(UserProfileDTO userProfileDTO, int userId)
        {
            // check if the user profile exists
            var userProfile = await _context.UserProfiles.FirstOrDefaultAsync(x => x.UserId == userId);
            if (userProfile == null)
            {
                return "User profile not found";
            }

            // update the user profile
            userProfile.Address = userProfileDTO.Address;
            userProfile.SellerRating = userProfileDTO.SellerRating;
            userProfile.PaymentDetails = userProfileDTO.PaymentDetails;
            userProfile.AdditionalData = userProfileDTO.AdditionalData;

            // save the changes to the database
            await _context.SaveChangesAsync();

            return "User profile updated successfully";
        }


        public async Task<string> DeleteUserProfileAsync(int userId)
        {
            // check if the user profile exists
            var userProfile = await _context.UserProfiles.FirstOrDefaultAsync(x => x.UserId == userId);
            if (userProfile == null)
            {
                return "User profile not found";
            }

            // remove the user profile from the database
            _context.UserProfiles.Remove(userProfile);
            await _context.SaveChangesAsync();

            return "User profile deleted successfully";


        }


        public async Task<UserProfile?> GetUserProfileAsync(int userId)
        {

            // get the user profile
            var userProfile = await _context.UserProfiles.FirstOrDefaultAsync(x => x.UserId == userId);

            if (userProfile == null)
            {
                return null;
            }

            return userProfile;
        }


        public async Task<List<UserProfile>> GetAllUserProfilesAsync()
        {
            // get all user profiles
            var userProfiles = await _context.UserProfiles.ToListAsync();

            return userProfiles;
        }

    }
}
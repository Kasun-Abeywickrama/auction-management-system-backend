namespace AuctionManagementAPI.Models.DTOs
{
    public class RegisterUserDTO
    {
        public required string FirstName { get; set; }
        public string? LastName { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }
    }
}

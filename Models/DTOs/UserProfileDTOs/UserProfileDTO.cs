using System.ComponentModel.DataAnnotations;

namespace AuctionManagementAPI.Models.DTOs.UserProfileDTOs;


public class UserProfileDTO
{
    public string? Address { get; set; }
    public decimal SellerRating { get; set; }
    public string? PaymentDetails { get; set; }
    public string? AdditionalData { get; set; }
}
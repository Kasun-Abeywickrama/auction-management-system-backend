﻿using System.ComponentModel.DataAnnotations;


namespace AuctionManagementAPI.Models.DTOs.AuthDTOs
{
    public class GenerateOtpDTO
    {
        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        public required string Email { get; set; }
    }

}

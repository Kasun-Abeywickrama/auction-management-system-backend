using AuctionManagementAPI.Models.DTOs.PaymentDTOs;
using AuctionManagementAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using static AuctionManagementAPI.Models.DTOs.PaymentDTOs.TransactionDTO;


namespace AuctionManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly PaymentService _paymentService;
        
        public PaymentController(PaymentService paymentService)
        {
            _paymentService = paymentService;
            
        }

        //api to add shipping details
        [HttpPost("AddShippingDetails")]
        [Authorize]
        public async Task<IActionResult> AddShippingDetails([FromBody] ShippingDetailsDTO shippingDetailsDTO
            )
        {
            // get the user id from the token
            var userId = User.FindFirstValue("UserId");

            if (userId == null)
            {
                return BadRequest("User not found");
            }

            // convert the user id to an integer
            int uId = Int32.Parse(userId);

            var result = await _paymentService.AddShippingDetailsAsync(shippingDetailsDTO, uId);
            if (result.Contains("Shipping Details Added successfully!"))
            {
                return Ok(result);
            }
            return BadRequest(result);

        }


        //api to update shipping details
        [HttpPut("UpdateShippingDetails")]
        [Authorize]
        public async Task<IActionResult> UpdateShippingDetails([FromBody] ShippingDetailsDTO shippingDetailsDTO)
        {

            // get the user id from the token
            var userId = User.FindFirstValue("UserId");

            if (userId == null)
            {
                return BadRequest("User not found");
            }

            // convert the user id to an integer
            int uId = Int32.Parse(userId);

            var result = await _paymentService.UpdateShippingDetailsAsync(shippingDetailsDTO, uId);
            if (result.Contains("Shipping Details updated successfully"))
            {
                return Ok(result);
            }
            return BadRequest(result);
        }


        // api to get Shipping Details
        [HttpGet("GetShippingDetails")]
        [Authorize]
        public async Task<IActionResult> GetShippingDetails()
        {

            // get the user id from the token
            var userId = User.FindFirstValue("UserId");

            if (userId == null)
            {
                return BadRequest("User not found");
            }

            // convert the user id to an integer
            int uId = Int32.Parse(userId);

            var result = await _paymentService.GetShippingDetailsAsync(uId);
            if (result != null)
            {
                return Ok(result);
            }
            return BadRequest("Shipping Details not found");
        }


        //api to get total amount
        [HttpGet("GetTotalAmount")]
        [Authorize]
        public async Task<IActionResult> GetTotalAmount()
        {
            var totalAmount = await _paymentService.GetTotalAmountAsync();

            if (totalAmount == null)
            {
                return NotFound("Bid or shipping fee not found");
            }

            return Ok(totalAmount);
        }

        

    }
}

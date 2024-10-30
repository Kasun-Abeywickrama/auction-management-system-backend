using AuctionManagementAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AuctionManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private readonly NotificationService _notificationService;

        public NotificationController(NotificationService notificationService)
        {
            _notificationService = notificationService;
        }


        // get all notifications for a user
        [HttpGet("GetAllNotifications")]
        [Authorize]
        public async Task<IActionResult> GetAllNotifications()
        {
            // get the user id from the token
            var userId = User.FindFirstValue("UserId");
            if (userId == null)
                return BadRequest("User not found");
            int uId = Int32.Parse(userId);

            var result = await _notificationService.GetNotificationsAsync(uId);
            if (result != null)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest("Failed to get notifications");
            }

        }

        // get 50 notifications for a user
        [HttpGet("Get50Notifications")]
        [Authorize]
        public async Task<IActionResult> Get50Notifications()
        {
            // get the user id from the token
            var userId = User.FindFirstValue("UserId");
            if (userId == null)
                return BadRequest("User not found");
            int uId = Int32.Parse(userId);

            var result = await _notificationService.Get50NotificationsAsync(uId);
            if (result != null)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest("Failed to get notifications");
            }

        }

        // delete notification for a user
        [HttpDelete("DeleteNotification")]
        [Authorize]
        public async Task<IActionResult> DeleteNotification(int notificationId)
        {
            var result = await _notificationService.DeleteNotificationAsync(notificationId);
            if (result.Contains("Notification deleted successfully"))
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

    }
}

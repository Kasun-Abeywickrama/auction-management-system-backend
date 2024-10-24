using AuctionManagementAPI.Data;
using AuctionManagementAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace AuctionManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private readonly AuctionContext _context;

        public NotificationController(AuctionContext context)
        {
            _context = context;
        }

        // POST: api/Notification/SendBidNotification
        [HttpPost("SendBidNotification")]
        public async Task<IActionResult> SendBidNotification(int userId, string notificationType, string message)
        {
            var notification = new Notification
            {
                UserId = userId,
                NotificationType = notificationType,
                Message = message,
                Timestamp = DateTime.UtcNow
            };

            _context.Notifications.Add(notification);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Bid notification sent successfully." });
        }

        // POST: api/Notification/SendPaymentNotification
        [HttpPost("SendPaymentNotification")]
        public async Task<IActionResult> SendPaymentNotification(int userId, string notificationType, string message)
        {
            var notification = new Notification
            {
                UserId = userId,
                NotificationType = notificationType,
                Message = message,
                Timestamp = DateTime.UtcNow
            };

            _context.Notifications.Add(notification);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Payment notification sent successfully." });
        }

        // GET: api/Notification/History/{userId}
        [HttpGet("History/{userId}")]
        public async Task<IActionResult> GetNotificationHistory(int userId)
        {
            var notifications = await _context.Notifications
                .Where(n => n.UserId == userId)
                .OrderByDescending(n => n.Timestamp)
                .ToListAsync();

            if (notifications == null || notifications.Count == 0)
            {
                return NotFound(new { message = "No notifications found for the user." });
            }

            return Ok(notifications);
        }

        // POST: api/Notification/SendEmailNotification
        [HttpPost("SendEmailNotification")]
        public async Task<IActionResult> SendEmailNotification(int userId, string subject, string message)
        {
            // Simulate sending email notification
            // In a real scenario, integrate with an email service (e.g., SendGrid, SMTP)

            // For now, just save it as a notification
            var notification = new Notification
            {
                UserId = userId,
                NotificationType = "Email",
                Message = $"Email Subject: {subject}, Message: {message}",
                Timestamp = DateTime.UtcNow
            };

            _context.Notifications.Add(notification);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Email notification sent successfully." });
        }
    }
}

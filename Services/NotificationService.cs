using AuctionManagementAPI.Data;
using AuctionManagementAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace AuctionManagementAPI.Services
{
    public class NotificationService
    {
        private readonly AuctionContext _context;
        private readonly EmailService _emailService;

        public NotificationService(AuctionContext context, EmailService emailService)
        {
            _context = context;
            this._emailService = emailService;
        }

        // Get all notifications for a user
        public async Task<List<Notification>> GetNotificationsAsync(int userId)
        {
            var notifications = await _context.Notifications
                .Where(n => n.UserId == userId)
                .ToListAsync();

            return notifications;
        }

        // Get 50 notifications for a user
        public async Task<List<Notification>> Get50NotificationsAsync(int userId)
        {
            var notifications = await _context.Notifications
                .Where(n => n.UserId == userId)
                .Take(50)
                .ToListAsync();

            return notifications;
        }

        // Make notification for a user
        public async Task<string> MakeNotificationAsync(Notification notification)
        {
            _context.Notifications.Add(notification);
            await _context.SaveChangesAsync();

            return "Notification created successfully";
        }


        // Make notification with an email for a user
        public async Task<string> MakeNotificationWithEmailAsync(Notification notification)
        {
            _context.Notifications.Add(notification);
            await _context.SaveChangesAsync();

            // Send email SendEmail function parameters userid title message
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.UserId == notification.UserId);
            if (user == null)
            {
                return "User not found";
            }

             var result = await _emailService.SendEmailAsync(user.UserId, notification.Title, notification.Message);
             if (result == null)
            {
                return "Notification created successfully but email not sent";
            }

            return "Notification created successfully";
        }


        // Delete notification for a user
        public async Task<string> DeleteNotificationAsync(int notificationId)
        {
            var notification = await _context.Notifications
                .FirstOrDefaultAsync(n => n.NotificationId == notificationId);

            if (notification == null)
            {
                return "Notification not found";
            }

            _context.Notifications.Remove(notification);
            await _context.SaveChangesAsync();

            return "Notification deleted successfully";
        }


    }
}

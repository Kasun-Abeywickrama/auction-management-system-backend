using AuctionManagementAPI.Data;
using AuctionManagementAPI.Models;
using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Mail;

namespace AuctionManagementAPI.Services
{
    public class EmailService
    {
        private readonly EmailSettings _emailSettings;
        private readonly AuctionContext _context;

        public EmailService(AuctionContext context, IOptions<EmailSettings> emailSettings)
        {
            _context = context;
            _emailSettings = emailSettings.Value;
        }

        public void SendOtpEmail(string recipientEmail, string otpCode)
        {
            var fromAddress = new MailAddress(_emailSettings.SmtpUsername, _emailSettings.FromName);
            var toAddress = new MailAddress(recipientEmail);
            const string subject = "Your OTP Code";
            string body = $"Your OTP Code is: {otpCode}";

            var smtpClient = new SmtpClient
            {
                Host = _emailSettings.SmtpHost,
                Port = _emailSettings.SmtpPort,
                EnableSsl = true,
                Credentials = new NetworkCredential(_emailSettings.SmtpUsername, _emailSettings.SmtpPassword)
            };

            using (var message = new MailMessage(fromAddress, toAddress)
            {
                Subject = subject,
                Body = body
            })
            {
                smtpClient.Send(message);
            }
        }

        public void SendAccountLockedEmail(string recipientEmail)
        {
            var fromAddress = new MailAddress(_emailSettings.SmtpUsername, _emailSettings.FromName);
            var toAddress = new MailAddress(recipientEmail);
            const string subject = "Account Locked";
            string body = "Your account has been locked due to multiple failed login attempts. Please use forgot password to reset your password.";

            var smtpClient = new SmtpClient
            {
                Host = _emailSettings.SmtpHost,
                Port = _emailSettings.SmtpPort,
                EnableSsl = true,
                Credentials = new NetworkCredential(_emailSettings.SmtpUsername, _emailSettings.SmtpPassword)
            };

            using (var message = new MailMessage(fromAddress, toAddress)
            {
                Subject = subject,
                Body = body
            })
            {
                smtpClient.Send(message);
            }
        }

        public async Task<string> SendEmailAsync(int userId, string subject, string body)
        {
            var user = _context.Users.Find(userId);
            if (user == null)
            {
                return null;
            }

            var fromAddress = new MailAddress(_emailSettings.SmtpUsername, _emailSettings.FromName);
            var toAddress = new MailAddress(user.Email);
            body = $"Dear {user.FirstName} {user.LastName},\n\n{body}";

            var smtpClient = new SmtpClient
            {
                Host = _emailSettings.SmtpHost,
                Port = _emailSettings.SmtpPort,
                EnableSsl = true,
                Credentials = new NetworkCredential(_emailSettings.SmtpUsername, _emailSettings.SmtpPassword)
            };

            using (var message = new MailMessage(fromAddress, toAddress)
            {
                Subject = subject,
                Body = body
            })
            {
                smtpClient.Send(message);
            }

            return "Email sent successfully";
        }
    }
}

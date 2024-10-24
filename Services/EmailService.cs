using System.Net;
using System.Net.Mail;

namespace AuctionManagementAPI.Services
{
    public class EmailService
    {
        private readonly string _smtpHost = "smtp.gmail.com";
        private readonly int _smtpPort = 587;
        private readonly string _smtpUsername = "geshansampath10@gmail.com";
        private readonly string _smtpPassword = "tsbymureurdxxvrd";
        private readonly string _fromName = "Lansuwa.lk";
        public void SendOtpEmail(string recipientEmail, string otpCode)
        {
            var fromAddress = new MailAddress(_smtpUsername, _fromName);
            var toAddress = new MailAddress(recipientEmail);
            const string subject = "Your OTP Code";
            string body = $"Your OTP Code is: {otpCode}";

            var smtpClient = new SmtpClient
            {
                Host = _smtpHost,
                Port = _smtpPort,
                EnableSsl = true,
                Credentials = new NetworkCredential(_smtpUsername, _smtpPassword)
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
            var fromAddress = new MailAddress(_smtpUsername, _fromName);
            var toAddress = new MailAddress(recipientEmail);
            const string subject = "Account Locked";
            string body = "Your account has been locked due to multiple failed login attempts. Please use forgot password to reset your password.";

            var smtpClient = new SmtpClient
            {
                Host = _smtpHost,
                Port = _smtpPort,
                EnableSsl = true,
                Credentials = new NetworkCredential(_smtpUsername, _smtpPassword)
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
    }

}
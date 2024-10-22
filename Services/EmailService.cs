using System.Net;
using System.Net.Mail;

namespace AuctionManagementAPI.Services
{
    public class EmailService
    {
        public void SendOtpEmail(string recipientEmail, string otpCode)
        {
            var fromAddress = new MailAddress("geshansampath10@gmail.com", "Lansuwa.lk");
            var toAddress = new MailAddress(recipientEmail);
            const string subject = "Your OTP Code";
            string body = $"Your OTP Code is: {otpCode}";

            var smtpClient = new SmtpClient
            {
                Host = "smtp.gmail.com",  // Configure with your SMTP settings
                Port = 587,
                EnableSsl = true,
                Credentials = new NetworkCredential("geshansampath10@gmail.com", "tsbymureurdxxvrd")
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

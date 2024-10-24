using System.Text.RegularExpressions;

namespace AuctionManagementAPI.Services
{
    public class ValidationService
    {
        // Method to validate and sanitize regular strings
        public string ValidateAndSanitizeInput(string input, int maxLength = 100)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                throw new ArgumentException("Input cannot be null or empty.");
            }

            // Remove leading and trailing whitespace
            string sanitizedInput = input.Trim();

            // Limit input length
            if (sanitizedInput.Length > maxLength)
            {
                throw new ArgumentException($"Input cannot exceed {maxLength} characters.");
            }

            // Remove HTML tags and escape special characters
            sanitizedInput = Regex.Replace(sanitizedInput, @"<[^>]+>", string.Empty); // Remove HTML tags
            sanitizedInput = Regex.Replace(sanitizedInput, @"[&<>""']", m => { // // Escape special characters
                return m.Value switch
                {
                    "&" => "&amp;",
                    "<" => "&lt;",
                    ">" => "&gt;",
                    "\"" => "&quot;",
                    "'" => "&apos;",
                    _ => m.Value,
                };
            });

            return sanitizedInput;
        }

        // Validate email format
        public void ValidateEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email) || !Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                throw new ArgumentException("Invalid email format.");
            }
        }

        // Validate passwords
        public void ValidatePassword(string password)
        {
            if (string.IsNullOrWhiteSpace(password) || password.Length < 8 ||
                !Regex.IsMatch(password, @"[A-Z]") || !Regex.IsMatch(password, @"[a-z]") ||
                !Regex.IsMatch(password, @"\d"))
            {
                throw new ArgumentException("Password must be at least 8 characters long and include at least one uppercase letter, one lowercase letter, and one digit.");
            }
        }

        // Validate bid amount
        public void ValidateBidAmount(decimal bidAmount, decimal minBid, decimal maxBid)
        {
            if (bidAmount <= 0)
            {
                throw new ArgumentException("Bid amount must be greater than zero.");
            }

            if (bidAmount < minBid || bidAmount > maxBid)
            {
                throw new ArgumentException($"Bid amount must be between {minBid} and {maxBid}.");
            }
        }

        // Validate item description
        public string ValidateItemDescription(string description, int maxLength = 500)
        {
            string sanitizedDescription = ValidateAndSanitizeInput(description, maxLength);
            return sanitizedDescription;
        }

        // Validate phone numbers
        public void ValidatePhoneNumber(string phoneNumber)
        {
            if (string.IsNullOrWhiteSpace(phoneNumber) || !Regex.IsMatch(phoneNumber, @"^\+?[0-9]{10,15}$"))
            {
                throw new ArgumentException("Invalid phone number format.");
            }
        }
    }
}

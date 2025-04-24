namespace NexIPTV.API.DTOs
{
    public class LoginDto
    {
        public required string Email { get; set; }
        public required string Password { get; set; }
    }

    public class AuthResponse
    {
        public required string Token { get; set; }
        public DateTime Expiration { get; set; }
        public required string UserId { get; set; }
        public required string Email { get; set; }
        public required List<string> Roles { get; set; }
    }
}
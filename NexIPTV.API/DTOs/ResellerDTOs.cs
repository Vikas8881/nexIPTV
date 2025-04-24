namespace NexIPTV.API.DTOs
{
    public class CreateResellerDto
    {
        public required string Email { get; set; }
        public required string Password { get; set; }
    }

    public class CreditTransferDto
    {
        public required string FromUserId { get; set; }
        public required string ToUserId { get; set; }
        public decimal Amount { get; set; }
    }
}
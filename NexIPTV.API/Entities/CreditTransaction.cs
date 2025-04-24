namespace NexIPTV.API.Entities
{
    public class CreditTransaction
    {
        public int Id { get; set; }
        public required string FromUserId { get; set; } // Mark as required
        public required string ToUserId { get; set; }   // Mark as required
        public decimal Amount { get; set; }
        public DateTime TransactionDate { get; set; } = DateTime.UtcNow;
    }
}
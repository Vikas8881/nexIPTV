namespace NexIPTV.API.Entities
{
    // CreditTransaction.cs
    public class CreditTransaction
    {
        public int Id { get; set; }
        public string FromUserId { get; set; }
        public string ToUserId { get; set; }
        public decimal Amount { get; set; }
        public DateTime TransactionDate { get; set; } = DateTime.UtcNow;
    }
}

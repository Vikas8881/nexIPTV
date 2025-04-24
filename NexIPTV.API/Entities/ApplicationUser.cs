using Microsoft.AspNetCore.Identity;

namespace NexIPTV.API.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public string? ParentUserId { get; set; }
        public ApplicationUser? ParentUser { get; set; }
        public List<ApplicationUser> SubUsers { get; set; } = new();
        public decimal CreditBalance { get; set; }
        public bool IsTrial { get; set; } = true;
        public DateTime TrialEndDate { get; set; } = DateTime.UtcNow.AddDays(7);
        public DateTime? ActivationDate { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public string? ActivatedById { get; set; }
        public ApplicationUser? ActivatedBy { get; set; }
    }
}
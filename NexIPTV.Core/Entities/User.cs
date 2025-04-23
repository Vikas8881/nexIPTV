using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NexIPTV.Core.Entities
{
    public class User : IdentityUser
    {
        public string? ParentUserId { get; set; }
        public User? ParentUser { get; set; }
        public List<User> SubUsers { get; set; } = new();
        public decimal CreditBalance { get; set; }
        public bool IsTrial { get; set; } = true;
        public DateTime TrialEndDate { get; set; }
        public DateTime? ActivationDate { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public string? ActivatedById { get; set; }
        public User? ActivatedBy { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace NexIPTV.Core.Entities
{
    // User.cs
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

    // CreditTransaction.cs
    public class CreditTransaction
    {
        public int Id { get; set; }
        public string FromUserId { get; set; }
        public string ToUserId { get; set; }
        public decimal Amount { get; set; }
        public DateTime TransactionDate { get; set; } = DateTime.UtcNow;
    }

    // Playlist.cs
    public class Playlist
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string Url { get; set; }
        public List<PlaylistItem> Items { get; set; } = new();
    }

    public class PlaylistItem
    {
        public int Id { get; set; }
        public string GroupTitle { get; set; }
        public string TvgName { get; set; }
        public string TvgLogo { get; set; }
        public string Url { get; set; }
        public ContentType Type { get; set; }
    }

    public enum ContentType { LiveTV, Movie, Series, Other }
}

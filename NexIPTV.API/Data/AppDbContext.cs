using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NexIPTV.API.Entities;

namespace NexIPTV.API.Data
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<CreditTransaction> CreditTransactions { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<ApplicationUser>()
                .HasMany(u => u.SubUsers)
                .WithOne(u => u.ParentUser)
                .HasForeignKey(u => u.ParentUserId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
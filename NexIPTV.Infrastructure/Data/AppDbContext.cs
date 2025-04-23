using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NexIPTV.Core.Entities; // Add project reference to Core
namespace NexIPTV.Infrastructure.Data
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) // Add constructor
            : base(options)
        {
        }
        public DbSet<CreditTransaction> CreditTransactions { get; set; }
        public DbSet<Playlist> Playlists { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<ApplicationUser>()
                .HasMany(u => u.SubUsers)
                .WithOne(u => u.ParentUser)
                .HasForeignKey(u => u.ParentUserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<CreditTransaction>()
                .HasIndex(t => t.TransactionDate);
        }
    }
}

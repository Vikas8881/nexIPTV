using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace NexIPTV.Infrastructure.Data
{
    public class AppDbContext : IdentityDbContext<User>
    {
        public DbSet<CreditTransaction> CreditTransactions { get; set; }
        public DbSet<UserAccount> UserAccounts { get; set; }
        public DbSet<Playlist> Playlists { get; set; }
        // Add other DbSets for Categories, Movies, etc.

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<User>()
                .HasMany(u => u.SubUsers)
                .WithOne(u => u.ParentUser)
                .HasForeignKey(u => u.ParentUserId);
        }
    }
}

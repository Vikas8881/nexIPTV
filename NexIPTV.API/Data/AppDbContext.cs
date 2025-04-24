using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NexIPTV.API.Entities;
using NexIPTV.API.Models;
using NexIPTV.Core.Entities;
using System.Reflection;

namespace NexIPTV.Data
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        // Existing DbSets from current project
        public DbSet<CreditTransaction> CreditTransactions { get; set; }
        public DbSet<Reseller> Resellers { get; set; }
        public DbSet<SubReseller> SubResellers { get; set; }
        public DbSet<UserActivation> UserActivations { get; set; }
        public DbSet<UserCredit> UserCredits { get; set; }

        // New IPTV-related DbSets
        public DbSet<Playlist> Playlists { get; set; }
        public DbSet<PlaylistItem> PlaylistItems { get; set; }
        public DbSet<HiddenContent> HiddenContents { get; set; }
        public DbSet<Series> Series { get; set; }
        public DbSet<Season> Seasons { get; set; }
        public DbSet<Episode> Episodes { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Apply all configurations from the assembly
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            // Existing configurations
            builder.Entity<CreditTransaction>(entity =>
            {
                entity.HasOne(ct => ct.User)
                    .WithMany(u => u.CreditTransactions)
                    .HasForeignKey(ct => ct.UserId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            builder.Entity<Reseller>(entity =>
            {
                entity.HasOne(r => r.Admin)
                    .WithMany(a => a.Resellers)
                    .HasForeignKey(r => r.AdminId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            builder.Entity<SubReseller>(entity =>
            {
                entity.HasOne(sr => sr.Reseller)
                    .WithMany(r => r.SubResellers)
                    .HasForeignKey(sr => sr.ResellerId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            builder.Entity<UserActivation>(entity =>
            {
                entity.HasOne(ua => ua.ActivatedBy)
                    .WithMany()
                    .HasForeignKey(ua => ua.ActivatedById)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(ua => ua.User)
                    .WithMany(u => u.Activations)
                    .HasForeignKey(ua => ua.UserId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            builder.Entity<UserCredit>(entity =>
            {
                entity.HasOne(uc => uc.User)
                    .WithOne(u => u.Credit)
                    .HasForeignKey<UserCredit>(uc => uc.UserId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // New IPTV-related configurations
            builder.Entity<Playlist>(entity =>
            {
                entity.HasOne(p => p.User)
                    .WithMany(u => u.Playlists)
                    .HasForeignKey(p => p.UserId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.Property(p => p.RefreshInterval)
                    .HasConversion(
                        v => v.Ticks,
                        v => TimeSpan.FromTicks(v));
            });

            builder.Entity<PlaylistItem>(entity =>
            {
                entity.HasOne(pi => pi.Playlist)
                    .WithMany(p => p.Items)
                    .HasForeignKey(pi => pi.PlaylistId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.Property(pi => pi.Type)
                    .HasConversion<string>();

                entity.HasIndex(pi => pi.Category);
                entity.HasIndex(pi => pi.Type);
                entity.HasIndex(pi => pi.SeriesId);
            });

            builder.Entity<HiddenContent>(entity =>
            {
                entity.HasOne(hc => hc.Playlist)
                    .WithMany(p => p.HiddenContents)
                    .HasForeignKey(hc => hc.PlaylistId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            builder.Entity<Series>(entity =>
            {
                entity.HasOne(s => s.Playlist)
                    .WithMany(p => p.Series)
                    .HasForeignKey(s => s.PlaylistId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            builder.Entity<Season>(entity =>
            {
                entity.HasOne(s => s.Series)
                    .WithMany(s => s.Seasons)
                    .HasForeignKey(s => s.SeriesId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            builder.Entity<Episode>(entity =>
            {
                entity.HasOne(e => e.Season)
                    .WithMany(s => s.Episodes)
                    .HasForeignKey(e => e.SeasonId)
                    .OnDelete(DeleteBehavior.Cascade);
            });
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            // Existing audit logic if any...
            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}
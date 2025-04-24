using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NexIPTV.Core.Entities;

namespace NexIPTV.API.Data
{
    public class AppDbContext : IdentityDbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Playlist> Playlists { get; set; }
        public DbSet<PlaylistItem> PlaylistItems { get; set; }
        public DbSet<HiddenContent> HiddenContents { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Playlist>()
                .HasMany(p => p.Items)
                .WithOne(i => i.Playlist)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Playlist>()
                .HasMany(p => p.HiddenContents)
                .WithOne(h => h.Playlist)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
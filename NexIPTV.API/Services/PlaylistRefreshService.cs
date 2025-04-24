using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NexIPTV.API.Interfaces;
using NexIPTV.API.Data;

namespace NexIPTV.API.Services
{
    public class PlaylistRefreshService : BackgroundService
    {
        private readonly IServiceProvider _services;
        private readonly ILogger<PlaylistRefreshService> _logger;

        public PlaylistRefreshService(IServiceProvider services, ILogger<PlaylistRefreshService> logger)
        {
            _services = services;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using var scope = _services.CreateScope();
                var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                var parser = scope.ServiceProvider.GetRequiredService<IPlaylistParserService>();

                var outdatedPlaylists = await context.Playlists
                    .Where(p => p.LastUpdated.Add(p.RefreshInterval) < DateTime.UtcNow)
                    .ToListAsync(stoppingToken);

                foreach (var playlist in outdatedPlaylists)
                {
                    try
                    {
                        var updated = await parser.ParsePlaylistAsync(
                            playlist.Source,
                            playlist.IsXtream,
                            playlist.Username,
                            playlist.Password
                        );

                        updated.Id = playlist.Id;
                        updated.UserId = playlist.UserId;
                        updated.HiddenContents = playlist.HiddenContents;

                        context.Playlists.Update(updated);
                        await context.SaveChangesAsync(stoppingToken);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "Error refreshing playlist {PlaylistId}", playlist.Id);
                    }
                }

                await Task.Delay(TimeSpan.FromMinutes(5), stoppingToken);
            }
        }
    }
}
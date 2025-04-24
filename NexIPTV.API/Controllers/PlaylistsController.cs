using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NexIPTV.API.Data;
using NexIPTV.API.Interfaces;
using NexIPTV.Core.Entities;
using System.Security.Claims;

namespace NexIPTV.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class PlaylistsController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IPlaylistParserService _parser;

        public PlaylistsController(AppDbContext context, IPlaylistParserService parser)
        {
            _context = context;
            _parser = parser;
        }

        [HttpPost]
        public async Task<IActionResult> AddPlaylist([FromBody] PlaylistRequest request)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var playlist = await _parser.ParsePlaylistAsync(
                request.Source,
                request.IsXtream,
                request.Username,
                request.Password
            );

            playlist.UserId = userId;
            playlist.Name = string.IsNullOrEmpty(request.Name)
                ? new Uri(request.Source).Host
                : request.Name;

            _context.Playlists.Add(playlist);
            await _context.SaveChangesAsync();

            return Ok(playlist);
        }

        [HttpGet]
        public async Task<IActionResult> GetPlaylists()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var playlists = await _context.Playlists
                .Include(p => p.Items)
                .Include(p => p.HiddenContents)
                .Where(p => p.UserId == userId)
                .ToListAsync();

            return Ok(playlists.Select(p => new PlaylistResponse(p)));
        }

        [HttpPut("{id}/refresh")]
        public async Task<IActionResult> RefreshPlaylist(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var playlist = await _context.Playlists
                .Include(p => p.Items)
                .FirstOrDefaultAsync(p => p.Id == id && p.UserId == userId);

            if (playlist == null) return NotFound();

            var updated = await _parser.ParsePlaylistAsync(
                playlist.Source,
                playlist.IsXtream,
                playlist.Username,
                playlist.Password
            );

            // Preserve hidden content settings
            updated.HiddenContents = playlist.HiddenContents;

            _context.Playlists.Remove(playlist);
            _context.Playlists.Add(updated);
            await _context.SaveChangesAsync();

            return Ok(new PlaylistResponse(updated));
        }
    }

    public class PlaylistRequest
    {
        public required string Source { get; set; }
        public string? Name { get; set; }
        public bool IsXtream { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }
    }

    public class PlaylistResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime LastUpdated { get; set; }
        public List<PlaylistItemResponse> Items { get; set; }

        public PlaylistResponse(Playlist playlist)
        {
            Id = playlist.Id;
            Name = playlist.Name;
            LastUpdated = playlist.LastUpdated;
            Items = playlist.Items.Select(i => new PlaylistItemResponse(i)).ToList();
        }
    }

    public class PlaylistItemResponse
    {
        public string Title { get; set; }
        public string Category { get; set; }
        public string Type { get; set; }
        public bool HasCatchup { get; set; }

        public PlaylistItemResponse(PlaylistItem item)
        {
            Title = item.Title;
            Category = item.Category;
            Type = item.Type.ToString();
            HasCatchup = item.HasCatchup;
        }
    }
}
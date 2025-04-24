using NexIPTV.Core.Entities;

namespace NexIPTV.API.Models
{
    // Models/PlaylistModels.cs
    public class UserPlaylist
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string Name { get; set; }
        public string SourceUrl { get; set; }
        public bool IsXtreamCode { get; set; }
        public string? Host { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }
        public TimeSpan RefreshInterval { get; set; } = TimeSpan.FromHours(6);
        public DateTime LastRefreshed { get; set; }
        public List<PlaylistItem> Items { get; set; } = new();
        public List<HiddenContent> HiddenContents { get; set; } = new();
    }
}
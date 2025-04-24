using NexIPTV.API.Entities;

namespace NexIPTV.API.Entities
{
    // Playlist.cs
    public class Playlist
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string Url { get; set; }
        public List<PlaylistItem> Items { get; set; } = new();
    }
}

using NexIPTV.API.Services;
using System.ComponentModel.DataAnnotations.Schema;

namespace NexIPTV.Core.Entities
{
    public class Playlist
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string Name { get; set; }
        public string Source { get; set; } // URL or Xtream server
        public bool IsXtream { get; set; }
        public string? Host { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }
        public TimeSpan RefreshInterval { get; set; } = TimeSpan.FromHours(6);
        public DateTime LastUpdated { get; set; }
        public List<PlaylistItem> Items { get; set; } = new();
        public List<HiddenContent> HiddenContents { get; set; } = new();
    }

    public class PlaylistItem
    {
        public int Id { get; set; }
        public int PlaylistId { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }
        public string Category { get; set; }
        public ContentType Type { get; set; }
        public string? EpgId { get; set; }
        public bool HasCatchup { get; set; }
        public string? CatchupSource { get; set; }
        public string? SeriesId { get; set; }
        public int? Season { get; set; }
        public int? Episode { get; set; }
        public bool IsHidden { get; set; }

        [ForeignKey("PlaylistId")]
        public Playlist Playlist { get; set; }
        public ContentType ContentType { get; set; }
        public SeriesInfo SeriesInfo { get; set; }
    }

    public class HiddenContent
    {
        public int Id { get; set; }
        public int PlaylistId { get; set; }
        public string? Category { get; set; }
        public string? ContentId { get; set; }
        public bool HideEntireCategory { get; set; }

        [ForeignKey("PlaylistId")]
        public Playlist Playlist { get; set; }
    }

    public enum ContentType
    {
        Live,
        Movie,
        Series,
        Other
    }
}
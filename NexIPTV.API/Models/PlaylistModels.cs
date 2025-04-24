namespace NexIPTV.API.Models
{

    public class Metadata
    {
        public string? GroupTitle { get; set; }
        public string? TvgName { get; set; }
        public string? TvgLogo { get; set; }
        public ContentType Type { get; set; }
    }

    public class ParsedPlaylist
    {
        public List<PlaylistItem> Items { get; set; } = new List<PlaylistItem>();
    }
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

    public class PlaylistItem
    {
        public int Id { get; set; }
        public int PlaylistId { get; set; }
        public string ContentUrl { get; set; }
        public ContentType ContentType { get; set; }
        public string Category { get; set; }
        public string? SeriesId { get; set; }
        public int? SeasonNumber { get; set; }
        public int? EpisodeNumber { get; set; }
        public string Title { get; set; }
        public string? EpgId { get; set; }
        public bool HasCatchup { get; set; }
        public string? CatchupType { get; set; }
        public string? CatchupSource { get; set; }
        public bool IsHidden { get; set; }
    }

    public class HiddenContent
    {
        public int Id { get; set; }
        public int PlaylistId { get; set; }
        public string? Category { get; set; }
        public string? ContentId { get; set; }
        public bool HideEntireCategory { get; set; }
    }

    public enum ContentType
    {
        LiveTV,
        Movie,
        Series,
        Other
    }
}
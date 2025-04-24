using NexIPTV.Core.Entities;

namespace NexIPTV.API.Models
{

    public class Metadata
    {
        public string? GroupTitle { get; set; }
        public string? TvgName { get; set; }
        public string? TvgLogo { get; set; }
        public ContentType Type { get; set; }
    }
    public class ContentMetadata
    {
        public string? EpgId { get; set; }
        public string? CatchupType { get; set; }
        public DateTime? CatchupStart { get; set; }
        public DateTime? CatchupEnd { get; set; }
    }
}
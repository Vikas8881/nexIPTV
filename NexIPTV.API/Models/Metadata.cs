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
}
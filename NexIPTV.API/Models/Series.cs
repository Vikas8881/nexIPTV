using NexIPTV.Core.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace NexIPTV.API.Models
{
    public class Series
    {
        public int Id { get; set; }
        public int PlaylistId { get; set; }
        public string Title { get; set; }
        public string? PosterUrl { get; set; }
        public string? Description { get; set; }

        [ForeignKey("PlaylistId")]
        public Playlist Playlist { get; set; }
        public List<Season> Seasons { get; set; } = new();
    }
}

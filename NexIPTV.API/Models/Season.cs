using System.ComponentModel.DataAnnotations.Schema;

namespace NexIPTV.API.Models
{
    public class Season
    {
        public int Id { get; set; }
        public int SeriesId { get; set; }
        public int SeasonNumber { get; set; }
        public string? PosterUrl { get; set; }

        [ForeignKey("SeriesId")]
        public Series Series { get; set; }
        public List<Episode> Episodes { get; set; } = new();
    }
}

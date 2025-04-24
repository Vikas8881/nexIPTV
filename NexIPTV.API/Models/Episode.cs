using System.ComponentModel.DataAnnotations.Schema;

namespace NexIPTV.API.Models
{
    public class Episode
    {
        public int Id { get; set; }
        public int SeasonId { get; set; }
        public int EpisodeNumber { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public string? StillUrl { get; set; }
        public string? VideoUrl { get; set; }

        [ForeignKey("SeasonId")]
        public Season Season { get; set; }
    }
}

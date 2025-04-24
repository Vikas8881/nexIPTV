namespace NexIPTV.API.Models
{
    public class HiddenContent
    {
        public int Id { get; set; }
        public int PlaylistId { get; set; }
        public string? Category { get; set; }
        public string? ContentId { get; set; }
        public bool HideEntireCategory { get; set; }
    }
}
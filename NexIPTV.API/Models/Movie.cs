namespace NexIPTV.API.Models
{
    // Models/Movie.cs
    public class Movie
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string StreamUrl { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}

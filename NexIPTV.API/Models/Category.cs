namespace NexIPTV.API.Models
{
    // Models/Category.cs
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; } // "Movie", "Series", "Live"
        public string PosterUrl { get; set; }
    }
}

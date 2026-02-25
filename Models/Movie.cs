namespace MoviesManager.Models
{
    // just creating basic getters and setters via class Movie
    public class Movie
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Director { get; set; }
        public string Genre { get; set; }
        public int Year { get; set; }
        public double Rating { get; set; }
    }
}
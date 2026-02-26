using System.ComponentModel.DataAnnotations;

namespace MoviesManager.Models
{
    // Represents one movie record stored in the database
    public class Movie
    {
        public int Id { get; set; }

        // I added this because it will display changes how the field name appears in the UI for better user experience
        [Display(Name = "Movie Title")]
        [Required(ErrorMessage = "Oops! Please enter the movie title!")]
        [StringLength(100, ErrorMessage = "Title cannot exceed 100 characters.")]
        public string Title { get; set; } = string.Empty;  // Initialize with empty string to avoid null reference issues and keep field safe


        [Display(Name = "Director Name")]
        [Required(ErrorMessage = "Oops! Please enter director name!")]
        [StringLength(60, ErrorMessage = "Director name cannot exceed 60 characters.")]
        public string Director { get; set; } = string.Empty;


        [Display(Name = "Genre")]
        [Required(ErrorMessage = "Oops! Please enter movie genre!")]
        [StringLength(60, ErrorMessage = "Genre cannot exceed 60 characters.")]
        public string Genre { get; set; } = string.Empty;


        [Display(Name = "Release Year")]
        [Required(ErrorMessage = "Oops! Please enter the release year!")]
        [Range(1888, 2026, ErrorMessage = "Enter a valid year between 1888 and 2026.")]
        public int? Year { get; set; }


        [Display(Name = "Rating (0–10)")]
        [Required(ErrorMessage = "Oops! Please enter movie rating!")]
        [Range(0, 10, ErrorMessage = "Rating must be between 0 and 10.")]
        public double? Rating { get; set; }
    }
}
using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MoviesManager.Models
{
    public class Movie
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [Display(Name = "Movie Title")]
        [Required(ErrorMessage = "Oops! Please enter the movie title!")]
        [StringLength(100, ErrorMessage = "Title cannot exceed 100 characters.")]
        public string Title { get; set; } = string.Empty;

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
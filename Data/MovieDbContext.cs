using Microsoft.EntityFrameworkCore;
using MoviesManager.Models;

namespace MoviesManager.Data
{
    public class MovieDbContext : DbContext  // Database context for our Movies Manager, handles all database operations
    {
        public MovieDbContext(DbContextOptions<MovieDbContext> options)
            : base(options)
        {
        }

        public DbSet<Movie> Movies { get; set; }   // Represents the Movies table in the database
    }
}
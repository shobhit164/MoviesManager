using Microsoft.AspNetCore.Mvc;
using MoviesManager.Models;

namespace MoviesManager.Controllers
{
    public class MoviesController : Controller
    {
        // here is in-memory movies list
        private static List<Movie> movies = new List<Movie>
        {
            new Movie { Id = 1, Title = "3 Idiots", Director = "Rajkumar Hirani", Genre = "Comedy/Drama", Year = 2009, Rating = 8.4 },
            new Movie { Id = 2, Title = "RRR", Director = "S. S. Rajamouli", Genre = "Action/Drama", Year = 2022, Rating = 7.8 },
            new Movie { Id = 3, Title = "Forrest Gump", Director = "Robert Zemeckis", Genre = "Drama/Romance", Year = 1994, Rating = 8.8 },
            new Movie { Id = 4, Title = "The Prestige", Director = "Christopher Nolan", Genre = "Drama/Mystery", Year = 2006, Rating = 8.5 },
            new Movie { Id = 5, Title = "Mad Max: Fury Road", Director = "George Miller", Genre = "Action/Sci-Fi", Year = 2015, Rating = 8.1 }
        };

        private static int nextId = 6;

        // This will shows all movies on the main page
        public IActionResult Index() => View(movies);

        public IActionResult Create() => View();

        // Adding a new movie to the list and then returns to Index
        [HttpPost]
        public IActionResult Create(Movie movie)
        {
            movie.Id = nextId++;
            movies.Add(movie);

            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            var movie = movies.FirstOrDefault(m => m.Id == id);
            if (movie == null) return NotFound();

            return View(movie);
        }

        [HttpPost]
        public IActionResult Edit(Movie updatedMovie)
        {
            var movie = movies.FirstOrDefault(m => m.Id == updatedMovie.Id);
            if (movie == null) return NotFound();

            movie.Title = updatedMovie.Title;
            movie.Director = updatedMovie.Director;
            movie.Genre = updatedMovie.Genre;
            movie.Year = updatedMovie.Year;
            movie.Rating = updatedMovie.Rating;

            return RedirectToAction("Index");
        }

        // This will shows delete confirmation page and removes movie if confirmed by user
        public IActionResult Delete(int id)
        {
            var movie = movies.FirstOrDefault(m => m.Id == id);
            if (movie == null) return NotFound();

            return View(movie);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var movie = movies.FirstOrDefault(m => m.Id == id);
            if (movie != null) movies.Remove(movie);

            return RedirectToAction("Index");
        }
    }
}

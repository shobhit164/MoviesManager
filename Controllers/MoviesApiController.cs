using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoviesManager.Data;
using MoviesManager.Models;

namespace MoviesManager.Controllers
{
    // Base route → api/moviesapi
    // ApiController enables automatic model validation + JSON binding
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesApiController : ControllerBase
    {
        // Database context injected using Dependency Injection
        private readonly MovieDbContext _context;

        public MoviesApiController(MovieDbContext context)
        {
            _context = context;
        }


        // GET: api/moviesapi
        // It will return all the movies from the database
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Movie>>> Get()
        {
            var movies = await _context.Movies.ToListAsync();
            return Ok(movies);
        }


        // GET: api/moviesapi/1
        // It will return a single movie by Id 
        [HttpGet("{id}")]
        public async Task<ActionResult<Movie>> Get(int id)
        {
            var movie = await _context.Movies.FindAsync(id);

            // If movie not found error will pop → 404
            if (movie == null)
                return NotFound();

            return Ok(movie);
        }


        // POST: api/moviesapi
        // This will create a new movie record
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Movie movie)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _context.Movies.Add(movie);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(Get), new { id = movie.Id }, movie);
        }


        // PUT: api/moviesapi/1
        // To Update an existing movie
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Movie movie)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var existingMovie = await _context.Movies.FindAsync(id);

            if (existingMovie == null)
                return NotFound();

            // Updating fields individually instead of EntityState.Modified
            // makes the update more readable and avoids accidental overwrite
            existingMovie.Title = movie.Title;
            existingMovie.Director = movie.Director;
            existingMovie.Genre = movie.Genre;
            existingMovie.Year = movie.Year;
            existingMovie.Rating = movie.Rating;

            await _context.SaveChangesAsync();

            return NoContent(); 
        }


        // DELETE: api/moviesapi/1
        // This will remove a movie permanently
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var movie = await _context.Movies.FindAsync(id);

            if (movie == null)
                return NotFound();

            _context.Movies.Remove(movie);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
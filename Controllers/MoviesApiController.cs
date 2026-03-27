using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using MoviesManager.Models;
using MoviesManager.Services;

namespace MoviesManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class MoviesApiController : ControllerBase
    {
        private readonly MongoDbService _mongoDbService;

        public MoviesApiController(MongoDbService mongoDbService)
        {
            _mongoDbService = mongoDbService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Movie>>> Get()
        {
            var movies = await _mongoDbService.Movies.Find(_ => true).ToListAsync();
            return Ok(movies);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Movie>> Get(string id)
        {
            var movie = await _mongoDbService.Movies.Find(m => m.Id == id).FirstOrDefaultAsync();

            if (movie == null)
                return NotFound();

            return Ok(movie);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Movie movie)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _mongoDbService.Movies.InsertOneAsync(movie);

            return CreatedAtAction(nameof(Get), new { id = movie.Id }, movie);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, [FromBody] Movie movie)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var existingMovie = await _mongoDbService.Movies.Find(m => m.Id == id).FirstOrDefaultAsync();

            if (existingMovie == null)
                return NotFound();

            movie.Id = existingMovie.Id;

            await _mongoDbService.Movies.ReplaceOneAsync(m => m.Id == id, movie);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var result = await _mongoDbService.Movies.DeleteOneAsync(m => m.Id == id);

            if (result.DeletedCount == 0)
                return NotFound();

            return NoContent();
        }
    }
}
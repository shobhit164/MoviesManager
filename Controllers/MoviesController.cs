using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using MoviesManager.Models;
using MoviesManager.Services;

namespace MoviesManager.Controllers
{
    public class MoviesController : Controller
    {
        private readonly MongoDbService _mongoDbService;

        public MoviesController(MongoDbService mongoDbService)
        {
            _mongoDbService = mongoDbService;
        }

        public async Task<IActionResult> Index()
        {
            var movies = await _mongoDbService.Movies.Find(_ => true).ToListAsync();
            return View(movies);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Movie movie)
        {
            if (ModelState.IsValid)
            {
                await _mongoDbService.Movies.InsertOneAsync(movie);
                return RedirectToAction(nameof(Index));
            }

            return View(movie);
        }

        public async Task<IActionResult> Edit(string id)
        {
            var movie = await _mongoDbService.Movies.Find(m => m.Id == id).FirstOrDefaultAsync();

            if (movie == null)
                return NotFound();

            return View(movie);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, Movie movie)
        {
            if (id != movie.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                await _mongoDbService.Movies.ReplaceOneAsync(m => m.Id == id, movie);
                return RedirectToAction(nameof(Index));
            }

            return View(movie);
        }

        public async Task<IActionResult> Delete(string id)
        {
            var movie = await _mongoDbService.Movies.Find(m => m.Id == id).FirstOrDefaultAsync();

            if (movie == null)
                return NotFound();

            return View(movie);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            await _mongoDbService.Movies.DeleteOneAsync(m => m.Id == id);
            return RedirectToAction(nameof(Index));
        }
    }
}
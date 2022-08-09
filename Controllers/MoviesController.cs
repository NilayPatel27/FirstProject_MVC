using FirstProject.Data.Services;
using FirstProject.Models;
using Microsoft.AspNetCore.Mvc;

namespace FirstProject.Controllers
{
    public class MoviesController : Controller
    {
        private readonly IMoviesService _service;

        public MoviesController(IMoviesService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index(string search)
        {

            var data = await _service.GetAllAsync();
            if (search != null)
            {
                var result = (from s in data
                              where s.Name.ToLower().Contains(search.ToLower())
                              select s).ToList();
                return View(result);
            }
            return View(data);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Movie movie)
        {
            if (!ModelState.IsValid)
            {
                return View(movie);
            }

            Movie data = new Movie
            {
                Name = movie.Name,
                Language = movie.Language,
                Category = movie.Category,
                Status = movie.Status,
                Region = movie.Region,
                Stars = movie.Stars,
                ReleDate = movie.ReleDate,
                MovieCategory = movie.MovieCategory,
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now
            };
            await _service.AddAsync(data);
            return RedirectToAction(nameof(Index));
        }

        //Get :Movies/Details/1
        public async Task<IActionResult> Details(int id)
        {
            var moviesDetails = await _service.GetByIdAsync(id);
            if (moviesDetails == null) return View("NotFound");
            return View(moviesDetails);
        }
        public async Task<IActionResult> Edit(int id)
        {
            var moviesDetails = await _service.GetByIdAsync(id);
            if (moviesDetails == null) return View("NotFound");
            return View(moviesDetails);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int id, Movie movie)
        {
            if (!ModelState.IsValid)
            {
                return View(movie);
            }
            Movie data = new Movie
            {
                Id = id,
                Name = movie.Name,
                Language = movie.Language,
                Category = movie.Category,
                Status = movie.Status,
                Region = movie.Region,
                Stars = movie.Stars,
                ReleDate = movie.ReleDate,
                MovieCategory = movie.MovieCategory,
                CreatedDate = movie.CreatedDate,
                UpdatedDate = DateTime.Now
            };

            await _service.UpdateAsync(id, data);
            return RedirectToAction(nameof(Index));
        }
        //Get:Actors/Delete/1
        public async Task<IActionResult> Delete(int id)
        {
            var moviesDetails = await _service.GetByIdAsync(id);
            if (moviesDetails == null) return View("NotFound");
            return View(moviesDetails);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var moviesDetails = await _service.GetByIdAsync(id);
            if (moviesDetails == null) return View("NotFound");

            await _service.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}

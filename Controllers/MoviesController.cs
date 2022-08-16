using FirstProject.Data.Services;
using FirstProject.Hubs;
using FirstProject.Models;
using FirstProject.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace FirstProject.Controllers
{
    public class MoviesController : Controller
    {
        private readonly IMoviesService _service;
        private readonly IHubContext<NotificationHub> _hubContext;


        public MoviesController(IMoviesService service, IHubContext<NotificationHub> hubContext)
        {
            _service = service;
            _hubContext = hubContext;
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
        public async Task<IActionResult> Create()
        {
            var movieDropdownData = await _service.GetNewMovieData();
            ViewBag.Directors = movieDropdownData.Directors;
            ViewBag.Actors = movieDropdownData.Actors;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(NewMovieVM movie)
        {
            if (!ModelState.IsValid)
            {
                var movieDropdownData = await _service.GetNewMovieData();
                ViewBag.Directors = movieDropdownData.Directors;
                ViewBag.Actors = movieDropdownData.Actors;
                return View(movie);
            }

            await _service.AddAsync(movie);
            await _hubContext.Clients.All.SendAsync("ReceiveMessage", "New movie has been added, please referesh the page");
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
            var movieDropdownData = await _service.GetNewMovieData();
            ViewBag.Directors = movieDropdownData.Directors;
            ViewBag.Actors = movieDropdownData.Actors;
            var moviesDetails = await _service.GetByIdAsync(id);
            if (moviesDetails == null) return View("NotFound");
            return View(moviesDetails);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int id, Movie movie)
        {
            if (!ModelState.IsValid)
            {
                var movieDropdownData = await _service.GetNewMovieData();
                ViewBag.Directors = movieDropdownData.Directors;
                ViewBag.Actors = movieDropdownData.Actors;
                return View(movie);
            }

            await _service.UpdateAsync(id, movie);
            await _hubContext.Clients.All.SendAsync("ReceiveMessage", movie.Name ,"has been updated, please referesh the page");
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
            await _hubContext.Clients.All.SendAsync("ReceiveMessage", "Movie has been deleted, please referesh the page");
            return RedirectToAction(nameof(Index));
        }
    }
}

using FirstProject.Data.Services;
using FirstProject.Hubs;
using FirstProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace FirstProject.Controllers
{
    public class DirectorsController : Controller
    {
        private readonly IDirectorsService _service;
        private readonly IHubContext<NotificationHub> _hubContext;


        public DirectorsController(IDirectorsService service, IHubContext<NotificationHub> hubContext)
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
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Director director)
        {
            if (!ModelState.IsValid)
            {
                return View(director);
            }

            await _service.AddAsync(director);
            await _hubContext.Clients.All.SendAsync("ReceiveMessage", "New director has been added, please referesh the page");
            return RedirectToAction(nameof(Index));
        }

        //Get :Directors/Details/1
        public async Task<IActionResult> Details(int id)
        {
            var directorDetails = await _service.GetByIdAsync(id);
            if (directorDetails == null) return View("NotFound");
            return View(directorDetails);
        }
        public async Task<IActionResult> Edit(int id)
        {
            var moviesDetails = await _service.GetByIdAsync(id);
            if (moviesDetails == null) return View("NotFound");
            return View(moviesDetails);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int id, Director director)
        {
            if (!ModelState.IsValid)
            {
                return View(director);
            }

            await _service.UpdateAsync(id, director);
            await _hubContext.Clients.All.SendAsync("ReceiveMessage", "Director has been updated, please referesh the page");
            return RedirectToAction(nameof(Index));
        }
        //Get:Actors/Delete/1
        public async Task<IActionResult> Delete(int id)
        {
            var directorDetails = await _service.GetByIdAsync(id);
            if (directorDetails == null) return View("NotFound");
            return View(directorDetails);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var directorDetails = await _service.GetByIdAsync(id);
            if (directorDetails == null) return View("NotFound");

            await _service.DeleteAsync(id);
            await _hubContext.Clients.All.SendAsync("ReceiveMessage", "Director has been deleted, please referesh the page");
            return RedirectToAction(nameof(Index));
        }
    }
}

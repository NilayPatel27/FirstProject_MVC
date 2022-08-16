using FirstProject.Data.Services;
using FirstProject.Hubs;
using FirstProject.Models;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace FirstProject.Controllers
{
    public class ActorsController : Controller
    {
        private readonly IActorsService _service;
        private readonly IHubContext<NotificationHub> _hubContext;
        public ActorsController(IActorsService service, IHubContext<NotificationHub> hubContext)
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

        //Get request : Actors/Create
        public IActionResult Create()
        {
            var data = _service.GetAllAsync();
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Actor actor)
        {
            if (!ModelState.IsValid)
            {
                return View(actor);
            }

            await _service.AddAsync(actor);
            await _hubContext.Clients.All.SendAsync("ReceiveMessage", actor.Name, "has been added, please referesh the page");
            return RedirectToAction(nameof(Index));
        }

        //Get :Actors/Details/1
        public async Task<IActionResult> Details(int id)
        {
            var actorDetails = await _service.GetByIdAsync(id);
            var movieDetails = await _service.ActorsMoviesViewModelAsync(id);
            ViewBag.Movies = movieDetails.Movies;
            if (actorDetails == null) return View("NotFound");
            return View(actorDetails);
        }
        public async Task<IActionResult> Edit(int id)
        {
            var actorDetails = await _service.GetByIdAsync(id);
            if (actorDetails == null) return View("NotFound");
            return View(actorDetails);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int id, Actor actor)
        {
            if (!ModelState.IsValid)
            {
                return View(actor);
            }
            await _service.UpdateAsync(id, actor);
            await _hubContext.Clients.All.SendAsync("ReceiveMessage", actor.Name, "has been updated, please referesh the page");
            return RedirectToAction(nameof(Index));
        }
        //Get:Actors/Delete/1
        public async Task<IActionResult> Delete(int id)
        {
            var actorDetails = await _service.GetByIdAsync(id);
            if (actorDetails == null) return View("NotFound");
            return View(actorDetails);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var actorDetails = await _service.GetByIdAsync(id);
            if (actorDetails == null) return View("NotFound");

            await _service.DeleteAsync(id);
            await _hubContext.Clients.All.SendAsync("ReceiveMessage", actorDetails.Name, "has been deleted, please referesh the page");
            return RedirectToAction(nameof(Index));
        }

    }
}

using FirstProject.Data.Services;
using FirstProject.Models;
using Microsoft.AspNetCore.Mvc;

namespace FirstProject.Controllers
{
    public class ActorsController : Controller
    {
        private readonly IActorsService _service;

        public ActorsController(IActorsService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index(string search)
        {

            var data = await _service.GetAllAsync();
            if (search != null)
            {
                var result = (from s in data
                              where s.Name.Contains(search)
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
        public async Task<IActionResult> Create([Bind("Name,Age,DOB,City,Gender,Hobbie,CreatedDate,UpdatedDate")] Actor actor)
        {
            if (!ModelState.IsValid)
            {
                return View(actor);
            }
            await _service.AddAsync(actor);
            return RedirectToAction(nameof(Index));
        }

        //Get :Actors/Details/1
        public async Task<IActionResult> Details(int id)
        {
            var actorDetails = await _service.GetByIdAsync(id);
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
            Actor data = new Actor
            {
                Id = actor.Id,
                Name = actor.Name,
                Age = actor.Age,
                DOB = actor.DOB,
                City = actor.City,
                Gender = actor.Gender,
                Hobbie = actor.Hobbie,
                CreatedDate = actor.CreatedDate,
                UpdatedDate = DateTime.Now
            };
            await _service.UpdateAsync(id, data);
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
            return RedirectToAction(nameof(Index));
        }

    }
}

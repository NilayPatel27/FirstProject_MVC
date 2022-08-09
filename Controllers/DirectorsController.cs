using FirstProject.Data.Services;
using FirstProject.Models;
using Microsoft.AspNetCore.Mvc;

namespace FirstProject.Controllers
{
    public class DirectorsController : Controller
    {
        private readonly IDirectorsService _service;

        public DirectorsController(IDirectorsService service)
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
        public async Task<IActionResult> Create(Director director)
        {
            if (!ModelState.IsValid)
            {
                return View(director);
            }

            Director data = new Director
            {
                Name = director.Name,
                Age = director.Age,
                DOB = director.DOB,
                City = director.City,
                Gender = director.Gender,
                Category = director.Category,
                Language = director.Language,
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now
            };
            await _service.AddAsync(data);
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

            Director data = new Director
            {
                Id = director.Id,
                Name = director.Name,
                Age = director.Age,
                DOB = director.DOB,
                City = director.City,
                Gender = director.Gender,
                Category = director.Category,
                Language = director.Language,
                CreatedDate = director.CreatedDate,
                UpdatedDate = DateTime.Now
            };
            await _service.UpdateAsync(id, data);
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
            return RedirectToAction(nameof(Index));
        }
    }
}

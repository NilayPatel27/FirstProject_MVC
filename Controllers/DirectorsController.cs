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

        public async Task<IActionResult> Index()
        {
            var allDirectors = await _service.GetAllAsync();
            return View(allDirectors);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create([Bind("Name,Age,DOB,City,Gender,Category,Language")] Director director)
        {
            if (!ModelState.IsValid)
            {
                return View(director);
            }
            await _service.AddAsync(director);
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
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Age,DOB,City,Gender,Category,Language")] Director director)
        {
            if (!ModelState.IsValid)
            {
                return View(director);
            }
            await _service.UpdateAsync(id, director);
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

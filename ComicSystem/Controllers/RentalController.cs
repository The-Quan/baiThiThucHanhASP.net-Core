using ComicSystem.Models;
using Microsoft.AspNetCore.Mvc;

namespace ComicSystem.Controllers
{
    public class RentalController : Controller
    {
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Rental rental)
        {
            if (ModelState.IsValid)
            {
                return RedirectToAction("Index"); 
            }
            return View(rental); 
        }
    }
}

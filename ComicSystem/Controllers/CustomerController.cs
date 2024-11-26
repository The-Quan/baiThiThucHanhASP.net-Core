using ComicSystem.data;
using ComicSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ComicSystem.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ComicSystemContext _context;

        public CustomerController(ComicSystemContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var customers = await _context.Customers.ToListAsync();
            return View(customers);
        }



        [HttpPost]
        public async Task<IActionResult> Register(Customer customer)
        {
            if (ModelState.IsValid)
            {
                customer.RegistrationDate = DateTime.Now;
                _context.Customers.Add(customer);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Home");
            }
            return View(customer);
        }
    }

}
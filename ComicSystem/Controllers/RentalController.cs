using Microsoft.AspNetCore.Mvc;
using ComicSystem.Models;
using ComicSystem.data;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace ComicSystem.Controllers
{
    public class RentalController : Controller
    {
        private readonly ComicSystemContext _context;

        public RentalController(ComicSystemContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var rentals = await _context.Rentals.Include(r => r.Customer).ToListAsync();
            return View(rentals);
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.ComicBooks = _context.ComicBooks.ToList();

            ViewBag.Customers = _context.Customers.ToList();

            return View();
        }



        [HttpPost]
        public async Task<IActionResult> Create(Rental rental, List<RentalDetail> rentalDetails)
        {
            try
            {
                var customer = await _context.Customers
                                              .FirstOrDefaultAsync(c => c.CustomerID == rental.CustomerID);

                if (customer == null)
                {
                    ModelState.AddModelError("CustomerID", "Customer not found.");
                    ViewBag.ComicBooks = _context.ComicBooks.ToList();
                    return View(rental);
                }

                rental.Customer = customer;
                _context.Rentals.Add(rental);
                await _context.SaveChangesAsync();

                foreach (var detail in rentalDetails)
                {
                    detail.RentalID = rental.RentalID;
                    _context.RentalDetails.Add(detail);
                }

                await _context.SaveChangesAsync();

                return RedirectToAction("Index", "RentalReport");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "An unexpected error occurred.");
                ViewBag.ComicBooks = _context.ComicBooks.ToList();
                return View(rental);
            }
        }

    }
}

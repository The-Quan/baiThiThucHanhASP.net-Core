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
            ViewBag.Customers = _context.Customers.ToList();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Rental rental)
        {
            try
            {
                Console.WriteLine($"Rental Data: CustomerID={rental.CustomerID}, RentalDate={rental.RentalDate}, ReturnDate={rental.ReturnDate}");

                // Tìm kiếm Customer dựa trên CustomerID
                var customer = await _context.Customers
                                              .FirstOrDefaultAsync(c => c.CustomerID == rental.CustomerID);

                if (customer == null)
                {
                    Console.WriteLine($"CustomerID {rental.CustomerID} not found in the database.");
                    ModelState.AddModelError("CustomerID", "Customer not found.");
                    return View(rental);
                }

                // Gán thông tin Customer vào Rental
                rental.Customer = customer;

                // Thêm Rental vào cơ sở dữ liệu
                _context.Rentals.Add(rental);

                Console.WriteLine("Attempting to save to database...");
                await _context.SaveChangesAsync();
                Console.WriteLine("Save successful!");

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                ModelState.AddModelError("", "An unexpected error occurred.");
                return View(rental);
            }
        }


    }
}

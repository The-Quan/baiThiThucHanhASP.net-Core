using ComicSystem.data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

public class RentalReportController : Controller
{
    private readonly ComicSystemContext _context;

    public RentalReportController(ComicSystemContext context)
    {
        _context = context;
    }
      [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

    public async Task<IActionResult> Index(DateTime? startDate, DateTime? endDate)
    {
        var query = _context.Rentals.Include(r => r.Customer)
                                    .Include(r => r.RentalDetails)
                                    .ThenInclude(rd => rd.ComicBook)
                                    .AsQueryable();

        if (startDate.HasValue && endDate.HasValue)
        {
            query = query.Where(r => r.RentalDate >= startDate && r.RentalDate <= endDate);
        }

        return View(await query.ToListAsync());
    }
}

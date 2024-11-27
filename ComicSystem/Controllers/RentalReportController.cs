using System.Globalization;
using ComicSystem.data;
using ComicSystem.Models;
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
    public IActionResult Index(DateTime? startDate, DateTime? endDate)
    {
        if (!startDate.HasValue) startDate = DateTime.Now.AddMonths(-1); 
        if (!endDate.HasValue) endDate = DateTime.Now; 

        ViewData["StartDate"] = startDate.Value.ToString("yyyy-MM-dd");
        ViewData["EndDate"] = endDate.Value.ToString("yyyy-MM-dd");

        var rentals = _context.Rentals
            .Include(r => r.Customer)
            .Include(r => r.RentalDetails)
                .ThenInclude(rd => rd.ComicBook)
            .Where(r => r.RentalDate >= startDate.Value && r.RentalDate <= endDate.Value)
            .ToList();

        return View(rentals);
    }
}

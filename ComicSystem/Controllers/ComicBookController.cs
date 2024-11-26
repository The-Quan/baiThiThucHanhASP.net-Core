using ComicSystem.data;
using ComicSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ComicSystem.Controllers{
    public class ComicBookController : Controller
{
    private readonly ComicSystemContext _context;

    public ComicBookController(ComicSystemContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        return View(await _context.ComicBooks.ToListAsync());
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(ComicBook comicBook)
    {
        if (ModelState.IsValid)
        {
            _context.ComicBooks.Add(comicBook);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(comicBook);
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var comicBook = await _context.ComicBooks.FindAsync(id);
        if (comicBook == null)
        {
            return NotFound();
        }
        return View(comicBook);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(ComicBook comicBook)
    {
        if (ModelState.IsValid)
        {
            _context.ComicBooks.Update(comicBook);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(comicBook);
    }

    [HttpGet]
    public async Task<IActionResult> Delete(int id)
    {
        var comicBook = await _context.ComicBooks.FindAsync(id);
        if (comicBook == null)
        {
            return NotFound();
        }
        return View(comicBook);
    }

    [HttpPost, ActionName("Delete")]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var comicBook = await _context.ComicBooks.FindAsync(id);
        _context.ComicBooks.Remove(comicBook);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
}

}
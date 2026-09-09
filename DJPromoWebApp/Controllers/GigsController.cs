using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DJPromoWebApp.Data;
using DJPromoWebApp.Models;

namespace DJPromoWebApp.Controllers
{
    public class GigsController : Controller
    {
        private readonly DJContext _context;

        public GigsController(DJContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            List<Gig> gigs = await _context.Gigs
                .Include(g => g.DJ)
                .Include(g => g.Venue)
                .ToListAsync();

            return View(gigs);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var gig = await _context.Gigs
                .Include(g => g.DJ)
                .Include(g => g.Venue)
                .FirstOrDefaultAsync(g => g.GigID == id);

            if (gig == null) return NotFound();

            return View(gig);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewBag.DJs = await _context.DJs.ToListAsync();
            ViewBag.Venues = await _context.Venues.ToListAsync();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            [Bind("GigID,DJID,VenueID,Date,Time")] Gig gig)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Gigs.Add(gig);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Error saving gig: " + ex.Message);
                }
            }

            ViewBag.DJs = await _context.DJs.ToListAsync();
            ViewBag.Venues = await _context.Venues.ToListAsync();
            return View(gig);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var gig = await _context.Gigs
                .FirstOrDefaultAsync(g => g.GigID == id);

            if (gig == null) return NotFound();

            ViewBag.DJs = await _context.DJs.ToListAsync();
            ViewBag.Venues = await _context.Venues.ToListAsync();
            return View(gig);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(
            int id,
            [Bind("GigID,DJID,VenueID,Date,Time")] Gig gig)
        {
            if (id != gig.GigID) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Gigs.Update(gig);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GigExists(gig.GigID)) return NotFound();
                    throw;
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Error updating gig: " + ex.Message);
                }
            }

            ViewBag.DJs = await _context.DJs.ToListAsync();
            ViewBag.Venues = await _context.Venues.ToListAsync();
            return View(gig);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var gig = await _context.Gigs
                .Include(g => g.DJ)
                .Include(g => g.Venue)
                .FirstOrDefaultAsync(g => g.GigID == id);

            if (gig == null) return NotFound();

            return View(gig);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var gig = await _context.Gigs.FindAsync(id);

            if (gig != null)
            {
                _context.Gigs.Remove(gig);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        private bool GigExists(int id)
        {
            return _context.Gigs.Any(e => e.GigID == id);
        }
    }
}
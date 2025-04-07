using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EventEase.Models;

namespace EventEase.Controllers
{
    public class VenueController : Controller
    {
        private readonly ApplicationDbContext _context;

        public VenueController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var venues = await _context.Venue.ToListAsync();
            return View(venues);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var venueDetails = await _context.Venue.FirstOrDefaultAsync(m => m.VenueId == id);

            if (venueDetails == null)
                return NotFound();

            return View(venueDetails);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("VenueId,VenueName,Location,Capacity,ImageUrl")] Venue venue)
        {
            if (ModelState.IsValid)
            {
                _context.Add(venue);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            // Optional: log validation errors if needed
            // var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
            return View(venue);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var venueDetails = await _context.Venue.FindAsync(id);

            if (venueDetails == null)
                return NotFound();

            return View(venueDetails);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("VenueId,VenueName,Location,Capacity,ImageUrl")] Venue venue)
        {
            if (id != venue.VenueId)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(venue);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VenueExists(venue.VenueId))
                        return NotFound();
                    else
                        throw;
                }

                return RedirectToAction(nameof(Index));
            }

            return View(venue);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var venueDetails = await _context.Venue.FirstOrDefaultAsync(m => m.VenueId == id);

            if (venueDetails == null)
                return NotFound();

            return View(venueDetails);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var venueDetails = await _context.Venue
                .Include(v => v.Bookings)
                .FirstOrDefaultAsync(v => v.VenueId == id);

            if (venueDetails == null)
                return NotFound();

            if (venueDetails.Bookings != null && venueDetails.Bookings.Any())
                _context.Booking.RemoveRange(venueDetails.Bookings);

            _context.Venue.Remove(venueDetails);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        private bool VenueExists(int id)
        {
            return _context.Venue.Any(e => e.VenueId == id);
        }
    }
}

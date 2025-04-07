using EventEase.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EventEase.Controllers
{
    public class BookingController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BookingController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var bookings = await _context.Booking
                .Include(b => b.Event)
                .Include(b => b.Venue)
                .ToListAsync();
            return View(bookings);
        }

        public IActionResult Create()
        {
            ViewData["EventId"] = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(_context.Event, "EventId", "EventName");
            ViewData["VenueId"] = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(_context.Venue, "VenueId", "VenueName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BookingId,EventId,VenueId,BookingDate")] Booking booking)
        {
            if (ModelState.IsValid)
            {
                _context.Add(booking);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            // Reload dropdowns if model state fails
            ViewData["EventId"] = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(_context.Event, "EventId", "EventName", booking.EventId);
            ViewData["VenueId"] = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(_context.Venue, "VenueId", "VenueName", booking.VenueId);
            return View(booking);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var booking = await _context.Booking
                .Include(b => b.Event)
                .Include(b => b.Venue)
                .FirstOrDefaultAsync(m => m.BookingId == id);

            if (booking == null)
                return NotFound();

            return View(booking);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var booking = await _context.Booking.FindAsync(id);
            if (booking == null)
                return NotFound();

            ViewData["EventId"] = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(_context.Event, "EventId", "EventName", booking.EventId);
            ViewData["VenueId"] = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(_context.Venue, "VenueId", "VenueName", booking.VenueId);
            return View(booking);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BookingId,EventId,VenueId,BookingDate")] Booking booking)
        {
            if (id != booking.BookingId)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(booking);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookingExists(booking.BookingId))
                        return NotFound();
                    else
                        throw;
                }
                return RedirectToAction(nameof(Index));
            }

            ViewData["EventId"] = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(_context.Event, "EventId", "EventName", booking.EventId);
            ViewData["VenueId"] = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(_context.Venue, "VenueId", "VenueName", booking.VenueId);
            return View(booking);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var booking = await _context.Booking
                .Include(b => b.Event)
                .Include(b => b.Venue)
                .FirstOrDefaultAsync(m => m.BookingId == id);

            if (booking == null)
                return NotFound();

            return View(booking);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var booking = await _context.Booking.FindAsync(id);
            if (booking != null)
            {
                _context.Booking.Remove(booking);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        private bool BookingExists(int id)
        {
            return _context.Booking.Any(e => e.BookingId == id);
        }
    }
}



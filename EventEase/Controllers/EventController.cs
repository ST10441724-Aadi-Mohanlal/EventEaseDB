using EventEase.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EventEase.Controllers
{
    public class EventController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EventController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            

            var eventList = await _context.Event.ToListAsync();
            return View(eventList);
        }


        public async Task<IActionResult> Details(int? id)
        {
            var eventDetails = await _context.Event.FirstOrDefaultAsync(m => m.EventId == id);
            


            if (eventDetails == null)
            {
                return NotFound();
            }

            return View(eventDetails);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Event Event)
        {
            if (ModelState.IsValid)
            {
                _context.Add(Event);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(Event);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            var eventDetails = await _context.Event.FirstOrDefaultAsync(m => m.EventId == id);

            if (eventDetails == null)
            {
                return NotFound();
            }

            return View(eventDetails);
        }
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var eventDetails = await _context.Event.FindAsync(id);
            _context.Event.Remove(eventDetails);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EventExists(int id)
        {
            return _context.Event.Any(e => e.EventId == id);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eventDetails = await _context.Event.FindAsync(id);

            if (eventDetails == null)
            {
                return NotFound();
            }

            return View(eventDetails);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, Event Event)
        {
            if (id != Event.EventId)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(Event);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EventExists(Event.EventId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(Event);
        }




    }

}

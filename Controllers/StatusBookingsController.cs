using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HotelUColombia.Data;
using HotelUColombia.Models;

namespace HotelUColombia.Controllers
{
    public class StatusBookingsController : Controller
    {
        private readonly HotelUColombiaContext _context;

        public StatusBookingsController(HotelUColombiaContext context)
        {
            _context = context;
        }

        // GET: StatusBookings
        public async Task<IActionResult> Index()
        {
            return _context.StatusBooking != null ?
                        View(await _context.StatusBooking.ToListAsync()) :
                        Problem("Entity set 'HotelUColombiaContext.StatusBooking'  is null.");
        }

        // GET: StatusBookings/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.StatusBooking == null)
            {
                return NotFound();
            }

            var statusBooking = await _context.StatusBooking
                .FirstOrDefaultAsync(m => m.Id == id);
            if (statusBooking == null)
            {
                return NotFound();
            }

            return View(statusBooking);
        }

        // GET: StatusBookings/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: StatusBookings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Status,Id")] StatusBooking statusBooking)
        {
            if (ModelState.IsValid)
            {
                _context.Add(statusBooking);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(statusBooking);
        }

        // GET: StatusBookings/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.StatusBooking == null)
            {
                return NotFound();
            }

            var statusBooking = await _context.StatusBooking.FindAsync(id);
            if (statusBooking == null)
            {
                return NotFound();
            }
            return View(statusBooking);
        }

        // POST: StatusBookings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Status,Id")] StatusBooking statusBooking)
        {
            if (id != statusBooking.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(statusBooking);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StatusBookingExists(statusBooking.Id))
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
            return View(statusBooking);
        }

        // GET: StatusBookings/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.StatusBooking == null)
            {
                return NotFound();
            }

            var statusBooking = await _context.StatusBooking
                .FirstOrDefaultAsync(m => m.Id == id);
            if (statusBooking == null)
            {
                return NotFound();
            }

            return View(statusBooking);
        }

        // POST: StatusBookings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.StatusBooking == null)
            {
                return Problem("Entity set 'HotelUColombiaContext.StatusBooking'  is null.");
            }
            var statusBooking = await _context.StatusBooking.FindAsync(id);
            if (statusBooking != null)
            {
                _context.StatusBooking.Remove(statusBooking);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StatusBookingExists(int id)
        {
            return (_context.StatusBooking?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}

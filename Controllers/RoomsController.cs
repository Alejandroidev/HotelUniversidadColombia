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
    public class RoomsController : Controller
    {
        private readonly HotelUColombiaContext _context;

        public RoomsController(HotelUColombiaContext context)
        {
            _context = context;
        }

        #region get rooms
        /// <summary>
        /// get rooms byid
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET: Rooms
        public async Task<IActionResult> Index(int id)
        {
            if (id != 0)
            {
                var room = _context.Rooms.Where(room => room.Id == id);
                return _context.Rooms.Where(room => room.Id == id) != null ?
                View(room) :
                Problem("Entity set 'HotelUColombiaContext.Rooms'  is null.");
            }
            else
            {
                return _context.Rooms != null ?
                View(await _context.Rooms.ToListAsync()) :
                Problem("Entity set 'HotelUColombiaContext.Rooms'  is null.");
            }


        }
        #endregion

        #region Details
        // GET: Rooms/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Rooms == null)
            {
                return NotFound();
            }

            var rooms = await _context.Rooms
                .FirstOrDefaultAsync(m => m.Id == id);
            if (rooms == null)
            {
                return NotFound();
            }

            return View(rooms);
        }
        #endregion

        #region Create room
        // GET: Rooms/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Rooms/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Category,TV,Bathroom,Freezer,NumberBeds,Image,Price,Id")] Rooms rooms)
        {
            if (ModelState.IsValid)
            {
                _context.Add(rooms);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(rooms);
        }
        #endregion

        #region Edit room
        // GET: Rooms/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Rooms == null)
            {
                return NotFound();
            }

            var rooms = await _context.Rooms.FindAsync(id);
            if (rooms == null)
            {
                return NotFound();
            }
            return View(rooms);
        }
        #endregion

        #region Edit room
        // POST: Rooms/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Category,TV,Bathroom,Freezer,NumberBeds,Image,Price,Id")] Rooms rooms)
        {
            if (id != rooms.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(rooms);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RoomsExists(rooms.Id))
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
            return View(rooms);
        }
        #endregion

        // POST: Bookings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateBooking([Bind("IdRoom,IdCliente,PickUpDate,ReturnDate,CreatedDate,ValorDaily,IdStatus,IdUsuario,Id")] Booking booking)
        {

            if (ModelState.IsValid)
            {
                _context.Add(booking);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(booking);
        }

        #region delete room
        // GET: Rooms/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Rooms == null)
            {
                return NotFound();
            }

            var rooms = await _context.Rooms
                .FirstOrDefaultAsync(m => m.Id == id);
            if (rooms == null)
            {
                return NotFound();
            }

            return View(rooms);
        }
        #endregion

        #region
        // POST: Rooms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Rooms == null)
            {
                return Problem("Entity set 'HotelUColombiaContext.Rooms'  is null.");
            }
            var rooms = await _context.Rooms.FindAsync(id);
            if (rooms != null)
            {
                _context.Rooms.Remove(rooms);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        #endregion

        private bool RoomsExists(int id)
        {
            return (_context.Rooms?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}

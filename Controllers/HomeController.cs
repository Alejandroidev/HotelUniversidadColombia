using HotelUColombia.Data;
using HotelUColombia.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HotelUColombia
{
    public class HomeController : Controller
    {
        private readonly HotelUColombiaContext _context;
        private readonly ILogger<HomeController> _logger;

        public HomeController(HotelUColombiaContext context, ILogger<HomeController> logger)
        {
            _logger = logger;
            _context = context;
        }

        // GET: Home
        public async Task<IActionResult> Index()
        {
            return _context.Client != null ?
                        View(await _context.Client.ToListAsync()) :
                        Problem("Entity set 'HotelUColombiaContext.Client'  is null.");
        }

        // GET: Home/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Client == null)
            {
                return NotFound();
            }

            var client = await _context.Client
                .FirstOrDefaultAsync(m => m.Id == id);
            if (client == null)
            {
                return NotFound();
            }

            return View(client);
        }

        // GET: Home/Create
        public IActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// Crea la informacion del cliente traide del front
        /// </summary>
        /// <param name="client">client</param>
        /// <returns>retorna la vista de home si no hay formulario, si hay formulario retorna la vista de quick search</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,LastName,Phone,Email,Coments,Id")] Client client)
        {

            if (ModelState.IsValid)
            {
                _context.Add(client);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(QuickSearch));
            }
            return View(client);
        }

        // GET: Home/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Client == null)
            {
                return NotFound();
            }

            var client = await _context.Client.FindAsync(id);
            if (client == null)
            {
                return NotFound();
            }
            return View(client);
        }

        // POST: Home/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Name,LastName,Phone,Email,Coments,Id")] Client client)
        {
            if (id != client.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(client);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClientExists(client.Id))
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
            return View(client);
        }

        // GET: Home/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Client == null)
            {
                return NotFound();
            }

            var client = await _context.Client
                .FirstOrDefaultAsync(m => m.Id == id);
            if (client == null)
            {
                return NotFound();
            }

            return View(client);
        }

        // POST: Home/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Client == null)
            {
                return Problem("Entity set 'HotelUColombiaContext.Client'  is null.");
            }
            var client = await _context.Client.FindAsync(id);
            if (client != null)
            {
                _context.Client.Remove(client);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClientExists(int id)
        {
            return (_context.Client?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        public IActionResult Home()
        {
            return View();
        }

        public async Task<IActionResult> Disponibilidad(int id)
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

        public IActionResult AcercaDe()
        {
            return View();
        }

        public IActionResult QuickSearch()
        {
            return View();
        }

        /// <summary>
        /// Method <c>QuickSearch</c> crea la reserba con los datos ingresados por usuario
        /// </summary>
        /// <param name="indexDay"> index day</param>
        /// <param name="returnDay"> retun day</param>
        /// <param name="typeRoom"> type room</param>
        /// <returns>vista de disponibilidad</returns>
        [Route("Home")]
        [Route("Home/QuickSearch/{id}")]
        [HttpGet]      
        public IActionResult QuickSearch(DateTime indexDay, DateTime returnDay, int typeRoom)
        {            
            if (typeRoom > 0)
            {
                TimeSpan dias = returnDay - indexDay;
                int dia = dias.Days;
                var rooms = _context.Rooms.ToList().Where(room => room.Id == typeRoom).FirstOrDefault();
                var totalPrice = rooms.Price * dia;
                var id = _context.Client.ToList();

                Booking booking = new()
                {
                    CreatedDate = DateTime.Now,
                    IdRoom = typeRoom,
                    IdStatus = 1,
                    PickUpDate = indexDay,
                    ReturnDate = returnDay,
                    IdCliente = id.LastOrDefault().Id,
                    IdUsuario = 0,
                    ValorTotal = totalPrice
                };

                _context.Booking.Add(booking);
                _context.SaveChanges();
                return RedirectToAction(nameof(Disponibilidad));
            }
            
            return View();
        }

        public IActionResult Contacto()
        {
            return View();
        }

        public async Task<IActionResult> GetClient()
        {
            return _context.Client != null ?
                        View(await _context.Client.ToListAsync()) :
                        Problem("Entity set 'HotelUColombiaContext.Client'  is null.");
        }
    }
}

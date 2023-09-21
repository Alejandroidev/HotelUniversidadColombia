using HotelUColombia.Data;
using HotelUColombia.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HotelUColombia
{
    public class HomeController : Controller
    {
        #region constantes
        private readonly HotelUColombiaContext _context;
        private readonly ILogger<HomeController> _logger;

        public HomeController(HotelUColombiaContext context, ILogger<HomeController> logger)
        {
            _logger = logger;
            _context = context;
        }
        #endregion

        #region get Cliente
        /// <summary>
        /// get info cliente
        /// </summary>
        /// <returns>list clientes</returns>
        public async Task<IActionResult> Index()
        {
            return _context.Client != null ?
                        View(await _context.Client.ToListAsync()) :
                        Problem("Entity set 'HotelUColombiaContext.Client'  is null.");
        }
        #endregion

        #region details
        /// <summary>
        /// Method <c>Details</c> trae el detalle de un cliente individual
        /// </summary>
        /// <param name="id"></param>
        /// <returns>detalle de cliente</returns>
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
        #endregion

        #region create
        public IActionResult Create()
        {
            return View();
        }
        #endregion

        #region Create cliente
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
        #endregion

        #region edit client view
        /// <summary>
        /// Method <c>Edit</c>
        /// </summary>
        /// <param name="id">id client</param>
        /// <returns>vista de edicion</returns>
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
        #endregion

        #region Edit client
        /// <summary>
        /// Edit
        /// </summary>
        /// <param name="id"></param>
        /// <param name="client"></param>
        /// <returns></returns>
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
        #endregion

        #region delete client view
        /// <summary>
        /// Delete view
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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
        #endregion

        #region DeleteClient
        /// <summary>
        /// Delete Client
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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
        #endregion

        #region ClientExists 
        /// <summary>
        /// ClientExists
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private bool ClientExists(int id)
        {
            return (_context.Client?.Any(e => e.Id == id)).GetValueOrDefault();
        }
        #endregion

        #region Home
        /// <summary>
        /// Home view
        /// </summary>
        /// <returns></returns>
        public IActionResult Home()
        {
            return View();
        }
        #endregion

        #region Disponibilidad
        /// <summary>
        /// regresa la vista con las habitaciones disponibles
        /// </summary>
        /// <param name="id">id room</param>
        /// <returns>vista de habitaciones</returns>
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
        #endregion

        #region AcercaDe
        public IActionResult AcercaDe()
        {
            return View();
        }
        #endregion

        #region QuickSearchVista
        public IActionResult QuickSearch()
        {
            return View();
        }
        #endregion

        #region QuickSearch
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
        #endregion

        #region ContactoView
        /// <summary>
        /// Contacto
        /// </summary>
        /// <returns></returns>
        public IActionResult Contacto()
        {
            return View();
        }
        #endregion

        #region GetClient
        /// <summary>
        /// GetClient
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> GetClient()
        {
            return _context.Client != null ?
                        View(await _context.Client.ToListAsync()) :
                        Problem("Entity set 'HotelUColombiaContext.Client'  is null.");
        }
        #endregion
    }
}

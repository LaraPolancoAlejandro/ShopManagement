using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using APITDS;
using APITDS.Models;
using System.Runtime.Caching;
using Microsoft.Extensions.Caching.Memory;

namespace APITDS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StoresController : ControllerBase
    {
        private readonly ApiDbContext _context;
        private readonly IMemoryCache _cache;

        public StoresController(ApiDbContext context, IMemoryCache cache)
        {
            _context = context;
            _cache = cache;
        }

        // GET: api/Stores
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Store>>> GetStores([FromQuery] int page = 1, [FromQuery] int Limit = 10)
        {
            if (page <= 0 || Limit <= 0)
            {
                return BadRequest("Los parámetros 'page' y 'Limit' son requeridos y deben ser mayores que cero.");
            }

            // Intenta recuperar la lista de tiendas desde la caché
            if (_cache.TryGetValue("StoresList", out List<Store> stores))
            {
                return stores.Skip((page - 1) * Limit).Take(Limit).ToList();
            }

            // Si no está en caché, realiza la consulta y almacena en caché el resultado
            stores = await _context.Stores
                .Skip((page - 1) * Limit)
                .Take(Limit)
                .ToListAsync();

            // Almacena en caché la lista de tiendas por 150 minutos, por ejemplo
            var cacheEntryOptions = new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(150)
            };

            _cache.Set("StoresList", stores, cacheEntryOptions);

            return stores;
        }


        // GET: api/Stores/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Store>> GetStore(int id)
        {
          if (_context.Stores == null)
          {
              return NotFound();
          }
            var store = await _context.Stores.FindAsync(id);

            if (store == null)
            {
                return NotFound();
            }

            return store;
        }

        // PUT: api/Stores/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStore(int id, Store store)
        {
            if (id != store.Id)
            {
                return BadRequest();
            }

            _context.Entry(store).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StoreExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Stores
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Store>> PostStore(Store store)
        {
          if (_context.Stores == null)
          {
              return Problem("Entity set 'ApiDbContext.Stores'  is null.");
          }
            _context.Stores.Add(store);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetStore", new { id = store.Id }, store);
        }

        // DELETE: api/Stores/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStore(int id)
        {
            if (_context.Stores == null)
            {
                return NotFound();
            }
            var store = await _context.Stores.FindAsync(id);
            if (store == null)
            {
                return NotFound();
            }

            _context.Stores.Remove(store);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool StoreExists(int id)
        {
            return (_context.Stores?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}

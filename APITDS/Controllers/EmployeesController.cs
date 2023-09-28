using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using APITDS;
using APITDS.Models;
using Microsoft.Extensions.Caching.Memory;
namespace APITDS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly ApiDbContext _context;
        private readonly IMemoryCache _cache; // Agregar una instancia de IMemoryCache
        public EmployeesController(ApiDbContext context, IMemoryCache cache) // Inyectar IMemoryCache
        {
            _context = context;
            _cache = cache; // Inicializar la caché en el constructor
        }
        /// <summary>
        /// Obtiene una lista de empleados.
        /// </summary>
        /// <param name="page">Número de página.</param>
        /// <param name="limit">Límite de elementos por página.</param>
        /// <returns>Lista de empleados.</returns>
        // GET: api/Employees
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Employee>>> GetEmployees(
                    [FromQuery] int page = 1, [FromQuery] int limit = 10)
        {
            if (page <= 0 || limit <= 0)
            {
                return BadRequest("Los parámetros 'page' y 'limit' son requeridos y deben ser mayores que cero.");
            }

            if (_cache.TryGetValue($"EmployeesPage{page}Limit{limit}", out List<Employee> cachedEmployees)) // Intentar obtener datos de la caché
            {
                return cachedEmployees;
            }
            else
            {
                if (_context.Employees == null)
                {
                    return NotFound();
                }

                var query = _context.Employees.AsQueryable();

                query = query.Skip((page - 1) * limit).Take(limit);

                var employees = await query.ToListAsync();

                // Almacenar datos en caché con una duración específica (puedes ajustarla según tus necesidades)
                _cache.Set($"EmployeesPage{page}Limit{limit}", employees, TimeSpan.FromMinutes(10));

                return employees;
            }

        }
            // GET: api/Employees/5
            [HttpGet("{id}")]
        public async Task<ActionResult<Employee>> GetEmployee(int id)
        {
          if (_context.Employees == null)
          {
              return NotFound();
          }
            var employee = await _context.Employees.FindAsync(id);

            if (employee == null)
            {
                return NotFound();
            }

            return employee;
        }

        // PUT: api/Employees/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmployee(int id, Employee employee)
        {
            if (id != employee.Id)
            {
                return BadRequest();
            }

            _context.Entry(employee).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmployeeExists(id))
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

        // POST: api/Employees
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Employee>> PostEmployee(Employee employee)
        {
          if (_context.Employees == null)
          {
              return Problem("Entity set 'ApiDbContext.Employees'  is null.");
          }
            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEmployee", new { id = employee.Id }, employee);
        }

        // DELETE: api/Employees/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            if (_context.Employees == null)
            {
                return NotFound();
            }
            var employee = await _context.Employees.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }

            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EmployeeExists(int id)
        {
            return (_context.Employees?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}

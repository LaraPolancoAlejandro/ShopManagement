using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using APITDS;
using APITDS.Models;
using APITDS.DTO;
using System.Globalization;
using Microsoft.VisualBasic.FileIO;

namespace APITDS.Controllers
{
    [Route("api/Inventory")]
    [ApiController]
    public class InventoriesController : ControllerBase
    {
        private readonly ApiDbContext _context;

        public InventoriesController(ApiDbContext context)
        {
            _context = context;
        }

        // GET: api/Inventories
        [HttpGet]
        public async Task<ActionResult<IEnumerable<InventoryResponseDto>>> GetInventories(
        [FromQuery] int page = 1,
        [FromQuery] int limit = 10,
        [FromQuery] string[] storeNames = null,
        [FromQuery] string[] flavors = null,
        [FromQuery] int? minQuantity = null,
        [FromQuery] int? maxQuantity = null,
        [FromQuery] string? minDate = null, // Cambiado a string para permitir valores de fecha en formato "yyyy-MM-dd"
        [FromQuery] string? maxDate = null, // Cambiado a string para permitir valores de fecha en formato "yyyy-MM-dd"
        [FromQuery] bool? isSeasonFlavor = null)
        {
            if (page <= 0 || limit <= 0)
            {
                return BadRequest("Los parámetros 'page' y 'limit' son requeridos y deben ser mayores que cero.");
            }

            if (_context.Inventories == null)
            {
                return NotFound();
            }

            DateTime? parsedMinDate = !string.IsNullOrEmpty(minDate)
                ? DateTime.SpecifyKind(DateTime.ParseExact(minDate, "yyyy-MM-dd", CultureInfo.InvariantCulture), DateTimeKind.Utc)
                : (DateTime?)null;

            DateTime? parsedMaxDate = !string.IsNullOrEmpty(maxDate)
                ? DateTime.SpecifyKind(DateTime.ParseExact(maxDate, "yyyy-MM-dd", CultureInfo.InvariantCulture), DateTimeKind.Utc)
                : (DateTime?)null;

            var query = _context.Inventories
            .Include(i => i.Store)
            .Include(i => i.Employee)
            .Where(i =>
                (storeNames == null || storeNames.Length == 0 || storeNames.Contains(i.Store.Name)) &&
                (flavors == null || flavors.Length == 0 || flavors.Contains(i.Flavor)) &&
                (!minQuantity.HasValue || i.Quantity >= minQuantity.Value) &&
                (!maxQuantity.HasValue || i.Quantity <= maxQuantity.Value) &&
                (!parsedMinDate.HasValue || i.Date >= parsedMinDate.Value) && // Convertir a DateTime
                (!parsedMaxDate.HasValue || i.Date <= parsedMaxDate.Value) && // Convertir a DateTime
                (isSeasonFlavor == null || i.IsSeasonFlavor == isSeasonFlavor.Value))
            .Select(i => new InventoryResponseDto
            {
                Store = i.Store != null ? i.Store.Name : null,
                Date = i.Date.ToString("yyyy-MM-dd"),
                Flavor = i.Flavor,
                IsSeasonFlavor = i.IsSeasonFlavor ? "Yes" : "No",
                Quantity = i.Quantity,
                ListedBy = i.Employee != null ? i.Employee.Name : null
            });


            // Aplicar paginación si se especifican page y limit
            query = query.Skip((page - 1) * limit).Take(limit);

            var inventories = await query.ToListAsync();

            return inventories;
        }


        // GET: api/Inventories/5
        [HttpGet("{id}")]
        public async Task<ActionResult<InventoryResponseDto>> GetInventory(int id)
        {
            if (_context.Inventories == null)
            {
                return NotFound();
            }

            var inventory = await _context.Inventories
                .Include(i => i.Store)
                .Include(i => i.Employee)
                .FirstOrDefaultAsync(i => i.Id == id);

            if (inventory == null)
            {
                return NotFound();
            }

            var inventoryDto = new InventoryResponseDto
            {
                Store = inventory.Store?.Name,
                Date = inventory.Date.ToString("yyyy-MM-dd"),
                Flavor = inventory.Flavor,
                IsSeasonFlavor = inventory.IsSeasonFlavor ? "Yes" : "No",
                Quantity = inventory.Quantity,
                ListedBy = inventory.Employee?.Name
            };

            return inventoryDto;
        }


        // PUT: api/Inventories/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutInventory(int id, Inventory inventory)
        {
            if (id != inventory.Id)
            {
                return BadRequest();
            }

            _context.Entry(inventory).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InventoryExists(id))
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

   

        // POST: api/Inventories
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Inventory>> PostInventory(Inventory inventory)
        {
          if (_context.Inventories == null)
          {
              return Problem("Entity set 'ApiDbContext.Inventories'  is null.");
          }
            _context.Inventories.Add(inventory);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetInventory", new { id = inventory.Id }, inventory);
        }

        // DELETE: api/Inventories/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInventory(int id)
        {
            if (_context.Inventories == null)
            {
                return NotFound();
            }
            var inventory = await _context.Inventories.FindAsync(id);
            if (inventory == null)
            {
                return NotFound();
            }

            _context.Inventories.Remove(inventory);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool InventoryExists(int id)
        {
            return (_context.Inventories?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        [HttpPost("upload")]
        public async Task<IActionResult> UploadInventory(IFormFile csvFile)
        {
            if (csvFile == null || csvFile.Length <= 0)
            {
                return BadRequest("Invalid file.");
            }

            if (!Path.GetExtension(csvFile.FileName).Equals(".csv", StringComparison.OrdinalIgnoreCase))
            {
                return BadRequest("Invalid file format. Please upload a CSV file.");
            }

            var inventories = new List<InventoryDto>();
            var duplicateInventories = new List<InventoryDto>(); // Para mantener un registro de inventarios duplicados

            using (var parser = new TextFieldParser(csvFile.OpenReadStream()))
            {
                parser.TextFieldType = FieldType.Delimited;
                parser.SetDelimiters(",");
                parser.HasFieldsEnclosedInQuotes = true;

                if (!parser.EndOfData)
                {
                    parser.ReadFields(); // Skip header
                }

                while (!parser.EndOfData)
                {
                    var fields = parser.ReadFields();
                    if (fields != null && fields.Length >= 6)
                    {
                        var dateString = fields[1]?.Trim();
                        if (string.IsNullOrEmpty(dateString))
                        {
                            continue;
                        }

                        var inventory = new InventoryDto
                        {
                            Store = fields[0]?.Trim(),
                            Date = DateTime.ParseExact(dateString, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal).ToUniversalTime(),
                            Flavor = fields[2]?.Trim(),
                            IsSeasonFlavor = string.Equals(fields[3]?.Trim(), "Yes", StringComparison.OrdinalIgnoreCase),
                            Quantity = int.TryParse(fields[4]?.Trim(), out var quantity) ? quantity : 0,
                            ListedBy = fields[5]?.Trim()
                        };

                        // Verificar si ya existe un inventario con la misma combinación de valores
                        var existingInventory = await _context.Inventories.FirstOrDefaultAsync(i =>
                            i.Store.Name == inventory.Store &&
                            i.Date == inventory.Date &&
                            i.Flavor == inventory.Flavor &&
                            i.IsSeasonFlavor == inventory.IsSeasonFlavor &&
                            i.Quantity == inventory.Quantity &&
                            i.Employee.Name == inventory.ListedBy);

                        if (existingInventory != null)
                        {
                            // Registrar el inventario duplicado
                            duplicateInventories.Add(inventory);
                        }
                        else
                        {
                            inventories.Add(inventory);
                        }
                    }
                }
            }

            // Agregar los inventarios válidos al contexto de la base de datos
            foreach (var inventoryDto in inventories)
            {
                // Agregar inventario al contexto de la base de datos
                // (Este es un ejemplo, asegúrate de tener las referencias correctas a Store y Employee)
                var store = await _context.Stores.FirstOrDefaultAsync(s => s.Name == inventoryDto.Store);
                var employee = await _context.Employees.FirstOrDefaultAsync(e => e.Name == inventoryDto.ListedBy);

                var inventory = new Inventory
                {
                    Store = store ?? new Store { Name = inventoryDto.Store },
                    Employee = employee ?? new Employee { Name = inventoryDto.ListedBy },
                    Date = inventoryDto.Date,
                    Flavor = inventoryDto.Flavor,
                    IsSeasonFlavor = inventoryDto.IsSeasonFlavor,
                    Quantity = inventoryDto.Quantity
                };
                _context.Inventories.Add(inventory);
            }

            await _context.SaveChangesAsync();

            // Formatear la respuesta JSON con los inventarios y los duplicados
            var result = new
            {
                Inventories = inventories.Select(i => new
                {
                    Store = i.Store,
                    Date = i.Date.ToString("yyyy-MM-dd"),
                    Flavor = i.Flavor,
                    IsSeasonFlavor = i.IsSeasonFlavor ? "Yes" : "No",
                    Quantity = i.Quantity,
                    ListedBy = i.ListedBy
                }),
                DuplicateInventories = duplicateInventories.Select(i => new
                {
                    Store = i.Store,
                    Date = i.Date.ToString("yyyy-MM-dd"),
                    Flavor = i.Flavor,
                    IsSeasonFlavor = i.IsSeasonFlavor ? "Yes" : "No",
                    Quantity = i.Quantity,
                    ListedBy = i.ListedBy
                })
            };

            return Ok(result);
        }


    }

}

using InspirePO.Data;
using InspirePO.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InspirePO.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RestOutTableMasterController : ControllerBase
    {
        private readonly AppDbContext _context;

        public RestOutTableMasterController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/RestOutTableMaster
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RestOutTableMaster>>> GetTables()
        {
            return await _context.RestOutTableMasters.ToListAsync();
        }

        // GET: api/RestOutTableMaster/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RestOutTableMaster>> GetTable(long id)
        {
            var table = await _context.RestOutTableMasters.FindAsync(id);
            if (table == null)
            {
                return NotFound();
            }
            return table;
        }

        // POST: api/RestOutTableMaster
        [HttpPost]
        public async Task<ActionResult<RestOutTableMaster>> PostTable(RestOutTableMaster table)
        {
            _context.RestOutTableMasters.Add(table);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetTable), new { id = table.ID }, table);
        }

        // PUT: api/RestOutTableMaster/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTable(long id, RestOutTableMaster table)
        {
            if (id != table.ID)
            {
                return BadRequest();
            }

            _context.Entry(table).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: api/RestOutTableMaster/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTable(long id)
        {
            var table = await _context.RestOutTableMasters.FindAsync(id);
            if (table == null)
            {
                return NotFound();
            }

            _context.RestOutTableMasters.Remove(table);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}

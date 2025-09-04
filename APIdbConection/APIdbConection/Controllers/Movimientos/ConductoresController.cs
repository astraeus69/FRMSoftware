using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using APIdbConection.Models;
using APIdbConection.Models.Movimientos;

namespace APIdbConection.Controllers.Movimientos
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConductoresController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ConductoresController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Conductores
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Conductores>>> GetConductores()
        {
            return await _context.Conductores.ToListAsync();
        }

        // GET: api/Conductores/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Conductores>> GetConductores(int id)
        {
            var conductores = await _context.Conductores.FindAsync(id);

            if (conductores == null)
            {
                return NotFound();
            }

            return conductores;
        }

        // PUT: api/Conductores/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutConductores(int id, Conductores conductores)
        {
            if (id != conductores.IdConductor)
            {
                return BadRequest();
            }

            _context.Entry(conductores).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ConductoresExists(id))
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

        // POST: api/Conductores
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Conductores>> PostConductores(Conductores conductores)
        {
            _context.Conductores.Add(conductores);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetConductores", new { id = conductores.IdConductor }, conductores);
        }

        // DELETE: api/Conductores/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteConductores(int id)
        {
            var conductores = await _context.Conductores.FindAsync(id);
            if (conductores == null)
            {
                return NotFound();
            }

            _context.Conductores.Remove(conductores);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ConductoresExists(int id)
        {
            return _context.Conductores.Any(e => e.IdConductor == id);
        }
    }
}

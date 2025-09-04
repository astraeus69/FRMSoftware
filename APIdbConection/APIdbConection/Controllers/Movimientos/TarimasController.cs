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
    public class TarimasController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public TarimasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Tarimas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Tarimas>>> GetTarimas()
        {
            return await _context.Tarimas.ToListAsync();
        }

        // GET: api/Tarimas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Tarimas>> GetTarimas(int id)
        {
            var tarimas = await _context.Tarimas.FindAsync(id);

            if (tarimas == null)
            {
                return NotFound();
            }

            return tarimas;
        }

        // PUT: api/Tarimas/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTarimas(int id, Tarimas tarimas)
        {
            if (id != tarimas.IdTarima)
            {
                return BadRequest();
            }

            _context.Entry(tarimas).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TarimasExists(id))
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

        // POST: api/Tarimas
        [HttpPost]
        public async Task<ActionResult<Tarimas>> PostTarimas(Tarimas tarimas)
        {
            _context.Tarimas.Add(tarimas);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTarimas", new { id = tarimas.IdTarima }, tarimas);
        }

        // DELETE: api/Tarimas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTarimas(int id)
        {
            var tarimas = await _context.Tarimas.FindAsync(id);
            if (tarimas == null)
            {
                return NotFound();
            }

            _context.Tarimas.Remove(tarimas);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TarimasExists(int id)
        {
            return _context.Tarimas.Any(e => e.IdTarima == id);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using APIdbConection.Models;
using APIdbConection.Models.Catalogos;

namespace APIdbConection.Controllers.Catalogos
{
    [Route("api/[controller]")]
    [ApiController]
    public class RanchosController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public RanchosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Ranchos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Ranchos>>> GetRanchos()
        {
            return await _context.Ranchos.ToListAsync();
        }

        // GET: api/Ranchos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Ranchos>> GetRancho(int id)
        {
            var rancho = await _context.Ranchos.FindAsync(id);

            if (rancho == null)
            {
                return NotFound();
            }

            return rancho;
        }

        // GET: api/Ranchos/ExisteRancho
        [HttpGet("ExisteRancho")]
        public async Task<ActionResult<bool>> ExisteRancho(string numeroRancho)
        {
            var existe = await _context.Ranchos
                .AnyAsync(r => r.NumeroRancho == numeroRancho);

            return Ok(existe);
        }

        // PUT: api/Ranchos/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRancho(int id, Ranchos rancho)
        {
            if (id != rancho.IdRancho)
            {
                return BadRequest();
            }

            _context.Entry(rancho).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RanchoExists(id))
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

        // POST: api/Ranchos
        [HttpPost]
        public async Task<ActionResult<Ranchos>> PostRancho(Ranchos rancho)
        {
            _context.Ranchos.Add(rancho);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRancho", new { id = rancho.IdRancho }, rancho);
        }

        // DELETE: api/Ranchos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRancho(int id)
        {
            var rancho = await _context.Ranchos.FindAsync(id);
            if (rancho == null)
            {
                return NotFound();
            }

            _context.Ranchos.Remove(rancho);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RanchoExists(int id)
        {
            return _context.Ranchos.Any(e => e.IdRancho == id);
        }
    }
}

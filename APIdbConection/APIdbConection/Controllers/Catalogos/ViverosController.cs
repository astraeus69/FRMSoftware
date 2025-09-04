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
    public class ViverosController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ViverosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Viveros
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Viveros>>> GetViveros()
        {
            return await _context.Viveros.ToListAsync();
        }

        // GET: api/Viveros/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Viveros>> GetVivero(int id)
        {
            var vivero = await _context.Viveros.FindAsync(id);

            if (vivero == null)
            {
                return NotFound();
            }

            return vivero;
        }

        // GET: api/Viveros/ExisteVivero
        [HttpGet("ExisteVivero")]
        public async Task<ActionResult<bool>> ExisteVivero(string codigoVivero)
        {
            var existe = await _context.Viveros
                .AnyAsync(v => v.CodigoVivero == codigoVivero);

            return Ok(existe);
        }

        // PUT: api/Viveros/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVivero(int id, Viveros vivero)
        {
            if (id != vivero.IdVivero)
            {
                return BadRequest();
            }

            _context.Entry(vivero).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ViveroExists(id))
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

        // POST: api/Viveros
        [HttpPost]
        public async Task<ActionResult<Viveros>> PostVivero(Viveros vivero)
        {
            _context.Viveros.Add(vivero);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetVivero", new { id = vivero.IdVivero }, vivero);
        }

        // DELETE: api/Viveros/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVivero(int id)
        {
            var vivero = await _context.Viveros.FindAsync(id);
            if (vivero == null)
            {
                return NotFound();
            }

            _context.Viveros.Remove(vivero);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ViveroExists(int id)
        {
            return _context.Viveros.Any(e => e.IdVivero == id);
        }
    }
}

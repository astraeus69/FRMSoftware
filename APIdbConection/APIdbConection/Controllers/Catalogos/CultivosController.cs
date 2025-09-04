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
    public class CultivosController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CultivosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Cultivos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cultivos>>> GetCultivos()
        {
            return await _context.Cultivos.ToListAsync();
        }

        // GET: api/Cultivos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Cultivos>> GetCultivos(int id)
        {
            var cultivos = await _context.Cultivos.FindAsync(id);

            if (cultivos == null)
            {
                return NotFound();
            }

            return cultivos;
        }

        // GET: api/Cultivos/ExisteCultivo 
        // DUPLICADOS
        [HttpGet("ExisteCultivo")]
        public async Task<ActionResult<bool>> ExisteCultivo(string tipoBerry, string variedad)
        {
            var existe = await _context.Cultivos
                .AnyAsync(c => c.TipoBerry == tipoBerry && c.Variedad == variedad);

            return Ok(existe);
        }

        // PUT: api/Cultivos/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCultivos(int id, Cultivos cultivos)
        {
            if (id != cultivos.IdCultivo)
            {
                return BadRequest();
            }

            _context.Entry(cultivos).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CultivosExists(id))
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

        // POST: api/Cultivos
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Cultivos>> PostCultivos(Cultivos cultivos)
        {
            _context.Cultivos.Add(cultivos);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCultivos", new { id = cultivos.IdCultivo }, cultivos);
        }

        // DELETE: api/Cultivos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCultivos(int id)
        {
            var cultivos = await _context.Cultivos.FindAsync(id);
            if (cultivos == null)
            {
                return NotFound();
            }

            _context.Cultivos.Remove(cultivos);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CultivosExists(int id)
        {
            return _context.Cultivos.Any(e => e.IdCultivo == id);
        }
    }
}

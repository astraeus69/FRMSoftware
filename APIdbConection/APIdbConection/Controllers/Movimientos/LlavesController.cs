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
    public class LlavesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public LlavesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Llaves
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Llaves>>> GetLlaves()
        {
            return await _context.Llaves.ToListAsync();
        }

        // GET: api/Llaves/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Llaves>> GetLlave(int id)
        {
            var llaves = await _context.Llaves.FindAsync(id);

            if (llaves == null)
            {
                return NotFound();
            }

            return llaves;
        }

        [HttpGet("GetLlavesConRancho")]
        public async Task<ActionResult<IEnumerable<LlavesConRancho>>> GetLlavesConRancho()
        {
            var query = from l in _context.Llaves
                        join r in _context.Ranchos on l.IdRancho equals r.IdRancho
                        select new LlavesConRancho
                        {
                            IdLlave = l.IdLlave,
                            IdRancho = l.IdRancho,
                            NombreLlave = l.NombreLlave,
                            SuperficieHa = l.SuperficieHa,
                            SuperficieAcres = l.SuperficieAcres,
                            CantidadTuneles = l.CantidadTuneles,
                            Disponibilidad = l.Disponibilidad,
                            NombreRancho = r.NombreRancho,
                            NumeroRancho = r.NumeroRancho
                        };

            var result = await query.ToListAsync();

            if (result == null || !result.Any())
            {
                return NotFound();
            }

            return Ok(result);
        }

        // GET: api/Llaves/GetLlavesPorRancho/{idRancho}
        [HttpGet("GetLlavesPorRancho/{idRancho}")]
        public async Task<ActionResult<IEnumerable<Llaves>>> GetLlavesPorRancho(int idRancho)
        {
            var llaves = await _context.Llaves.Where(l => l.IdRancho == idRancho).ToListAsync();

            if (llaves == null || !llaves.Any())
            {
                return NotFound();  // Retorna 404 si no se encuentran llaves
            }

            return Ok(llaves);  // Retorna las llaves encontradas
        }


        // PUT: api/Llaves/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLlaves(int id, Llaves llaves)
        {
            if (id != llaves.IdLlave)
            {
                return BadRequest();
            }

            _context.Entry(llaves).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LlavesExists(id))
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

        // POST: api/Llaves
        [HttpPost]
        public async Task<ActionResult<Llaves>> PostLlaves(Llaves llaves)
        {
            _context.Llaves.Add(llaves);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetLlaves", new { id = llaves.IdLlave }, llaves);
        }

        // DELETE: api/Llaves/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLlaves(int id)
        {
            var llaves = await _context.Llaves.FindAsync(id);
            if (llaves == null)
            {
                return NotFound();
            }

            _context.Llaves.Remove(llaves);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool LlavesExists(int id)
        {
            return _context.Llaves.Any(e => e.IdLlave == id);
        }
    }
}

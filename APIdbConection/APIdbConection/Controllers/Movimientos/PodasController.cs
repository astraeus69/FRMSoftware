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
    public class PodasController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public PodasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Podas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Podas>>> GetPodas()
        {
            return await _context.Podas.ToListAsync();
        }

        // GET: api/Podas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Podas>> GetPoda(int id)
        {
            var poda = await _context.Podas.FindAsync(id);

            if (poda == null)
            {
                return NotFound();
            }

            return poda;
        }

        // GET: api/PodasDetalles
        [HttpGet("GetPodasDetalles")]
        public async Task<ActionResult<IEnumerable<PodasDetalles>>> GetPodasDetalles()
        {
            var podasDetalles = await (from p in _context.Podas
                                       join pl in _context.Plantaciones on p.IdPlantacion equals pl.IdPlantacion
                                       join c in _context.Cultivos on pl.IdCultivo equals c.IdCultivo
                                       join l in _context.Llaves on pl.IdLlave equals l.IdLlave
                                       join ran in _context.Ranchos on l.IdRancho equals ran.IdRancho
                                       select new PodasDetalles
                                       {
                                           IdPoda = p.IdPoda,
                                           TipoPoda = p.TipoPoda,
                                           FechaPoda = p.FechaPoda,
                                           NumSemPoda = p.NumSemPoda,


                                           IdPlantacion = pl.IdPlantacion,
                                           FechaPlantacion = pl.FechaPlantacion,
                                           NumSemPlantacion = pl.NumSemPlantacion,
                                           EstatusPlantacion = pl.EstatusPlantacion,


                                           IdCultivo = c.IdCultivo,
                                           TipoBerry = c.TipoBerry,
                                           Variedad = c.Variedad,


                                           IdLlave = l.IdLlave,
                                           NombreLlave = l.NombreLlave,
                                           SuperficieHa = l.SuperficieHa,
                                           SuperficieAcres = l.SuperficieAcres,
                                           Disponibilidad = l.Disponibilidad,


                                           IdRancho = ran.IdRancho,
                                           NombreRancho = ran.NombreRancho,
                                           NumeroRancho = ran.NumeroRancho,
                                       }).ToListAsync();

            return Ok(podasDetalles ?? new List<PodasDetalles>());
        }

        // GET: api/PodasDetalles/5
        [HttpGet("GetPodasDetalles/{id}")]
        public async Task<ActionResult<PodasDetalles>> GetPodaDetalles(int id)
        {
            var podaDetalles = await (from p in _context.Podas
                                      join pl in _context.Plantaciones on p.IdPlantacion equals pl.IdPlantacion
                                      join c in _context.Cultivos on pl.IdCultivo equals c.IdCultivo
                                      join l in _context.Llaves on pl.IdLlave equals l.IdLlave
                                      join ran in _context.Ranchos on l.IdRancho equals ran.IdRancho
                                      where p.IdPoda == id
                                      select new PodasDetalles
                                      {
                                          IdPoda = p.IdPoda,
                                          TipoPoda = p.TipoPoda,
                                          FechaPoda = p.FechaPoda,
                                          NumSemPoda = p.NumSemPoda,


                                          IdPlantacion = pl.IdPlantacion,
                                          FechaPlantacion = pl.FechaPlantacion,
                                          NumSemPlantacion = pl.NumSemPlantacion,
                                          EstatusPlantacion = pl.EstatusPlantacion,


                                          IdCultivo = c.IdCultivo,
                                          TipoBerry = c.TipoBerry,
                                          Variedad = c.Variedad,


                                          IdLlave = l.IdLlave,
                                          NombreLlave = l.NombreLlave,
                                          SuperficieHa = l.SuperficieHa,
                                          SuperficieAcres = l.SuperficieAcres,
                                          Disponibilidad = l.Disponibilidad,


                                          IdRancho = ran.IdRancho,
                                          NombreRancho = ran.NombreRancho,
                                          NumeroRancho = ran.NumeroRancho,
                                      }).FirstOrDefaultAsync();

            return Ok(podaDetalles ?? new PodasDetalles());
        }

        // PUT: api/Podas/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPoda(int id, Podas poda)
        {
            if (id != poda.IdPoda)
            {
                return BadRequest();
            }

            _context.Entry(poda).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PodaExists(id))
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

        // POST: api/Podas
        [HttpPost]
        public async Task<ActionResult<Podas>> PostPoda(Podas poda)
        {
            _context.Podas.Add(poda);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPoda", new { id = poda.IdPoda }, poda);
        }

        // DELETE: api/Podas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePoda(int id)
        {
            var poda = await _context.Podas.FindAsync(id);
            if (poda == null)
            {
                return NotFound();
            }

            _context.Podas.Remove(poda);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PodaExists(int id)
        {
            return _context.Podas.Any(e => e.IdPoda == id);
        }
    }
}

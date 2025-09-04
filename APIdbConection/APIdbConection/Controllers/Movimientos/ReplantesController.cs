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
    public class ReplantesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ReplantesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Replantes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Replantes>>> GetReplantes()
        {
            return await _context.Replantes.ToListAsync();
        }

        // GET: api/Replantes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Replantes>> GetReplantes(int id)
        {
            var replantes = await _context.Replantes.FindAsync(id);

            if (replantes == null)
            {
                return NotFound();
            }

            return replantes;
        }


        // GET: api/ReplantesDetalles
        [HttpGet("GetReplantesDetalles")]
        public async Task<ActionResult<IEnumerable<ReplantesDetalles>>> GetReplantesDetalles()
        {
            var replantesDetalles = await (from r in _context.Replantes
                                           join p in _context.Plantaciones on r.IdPlantacion equals p.IdPlantacion
                                           join c in _context.Cultivos on r.IdCultivo equals c.IdCultivo
                                           join l in _context.Llaves on p.IdLlave equals l.IdLlave
                                           join ran in _context.Ranchos on l.IdRancho equals ran.IdRancho
                                           join v in _context.Viveros on r.IdVivero equals v.IdVivero
                                           select new ReplantesDetalles
                                           {
                                               IdReplante = r.IdReplante,
                                               CantidadReplante = r.CantidadReplante,
                                               FechaReplante = r.FechaReplante,
                                               NumSemReplante = r.NumSemReplante,


                                               IdPlantacion = p.IdPlantacion,
                                               FechaPlantacion = p.FechaPlantacion,
                                               NumSemPlantacion = p.NumSemPlantacion,
                                               EstatusPlantacion = p.EstatusPlantacion,


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


                                               IdVivero = v.IdVivero,
                                               NombreVivero = v.NombreVivero,
                                               CodigoVivero = v.CodigoVivero
                                           }).ToListAsync();

            // En lugar de retornar NotFound, devolvemos una lista vacía
            return Ok(replantesDetalles ?? new List<ReplantesDetalles>());
        }

        // GET: api/ReplantesDetalles/5
        [HttpGet("GetReplantesDetalles/{id}")]
        public async Task<ActionResult<ReplantesDetalles>> GetReplanteDetalles(int id)
        {
            var replanteDetalles = await (from r in _context.Replantes
                                          join p in _context.Plantaciones on r.IdPlantacion equals p.IdPlantacion
                                          join c in _context.Cultivos on r.IdCultivo equals c.IdCultivo
                                          join l in _context.Llaves on p.IdLlave equals l.IdLlave
                                          join ran in _context.Ranchos on l.IdRancho equals ran.IdRancho
                                          join v in _context.Viveros on r.IdVivero equals v.IdVivero
                                          where r.IdReplante == id
                                          select new ReplantesDetalles
                                          {
                                              IdReplante = r.IdReplante,
                                              CantidadReplante = r.CantidadReplante,
                                              FechaReplante = r.FechaReplante,
                                              NumSemReplante = r.NumSemReplante,


                                              IdPlantacion = p.IdPlantacion,
                                              FechaPlantacion = p.FechaPlantacion,
                                              NumSemPlantacion = p.NumSemPlantacion,
                                              EstatusPlantacion = p.EstatusPlantacion,


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


                                              IdVivero = v.IdVivero,
                                              NombreVivero = v.NombreVivero,
                                              CodigoVivero = v.CodigoVivero
                                          }).FirstOrDefaultAsync();

            // Devolvemos un objeto vacío en lugar de NotFound
            return Ok(replanteDetalles ?? new ReplantesDetalles());
        }




        // PUT: api/Replantes/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutReplantes(int id, Replantes replantes)
        {
            if (id != replantes.IdReplante)
            {
                return BadRequest();
            }

            _context.Entry(replantes).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReplantesExists(id))
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

        // POST: api/Replantes
        [HttpPost]
        public async Task<ActionResult<Replantes>> PostReplantes(Replantes replantes)
        {
            _context.Replantes.Add(replantes);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetReplantes", new { id = replantes.IdReplante }, replantes);
        }

        // DELETE: api/Replantes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReplantes(int id)
        {
            var replantes = await _context.Replantes.FindAsync(id);
            if (replantes == null)
            {
                return NotFound();
            }

            _context.Replantes.Remove(replantes);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ReplantesExists(int id)
        {
            return _context.Replantes.Any(e => e.IdReplante == id);
        }
    }
}

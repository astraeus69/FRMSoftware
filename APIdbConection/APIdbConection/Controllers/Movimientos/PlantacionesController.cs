using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using APIdbConection.Models;
using APIdbConection.Models.Movimientos;

namespace APIdbConection.Controllers.Movimientos
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlantacionesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public PlantacionesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Llaves
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Plantaciones>>> GetPlantaciones()
        {
            return await _context.Plantaciones.ToListAsync();
        }


        // GET: api/Plantaciones
        [HttpGet("GetPlantacionesDetalles")]
        public async Task<ActionResult<IEnumerable<PlantacionesDetalles>>> GetPlantacionesDetalles()
        {
            var query = from p in _context.Plantaciones
                        join c in _context.Cultivos on p.IdCultivo equals c.IdCultivo
                        join l in _context.Llaves on p.IdLlave equals l.IdLlave
                        join r in _context.Ranchos on l.IdRancho equals r.IdRancho
                        join v in _context.Viveros on p.IdVivero equals v.IdVivero
                        select new PlantacionesDetalles
                        {
                            IdPlantacion = p.IdPlantacion,
                            CantidadPlantas = p.CantidadPlantas,
                            PlantasPorMetro = p.PlantasPorMetro,
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

                            IdRancho = r.IdRancho,
                            NombreRancho = r.NombreRancho,
                            NumeroRancho = r.NumeroRancho,

                            IdVivero = v.IdVivero,
                            NombreVivero = v.NombreVivero,
                            CodigoVivero = v.CodigoVivero
                        };

            var result = await query.ToListAsync();

            if (result == null) return NotFound(); // Solo devuelve 404 si result es null
            return Ok(result); // Devuelve lista vacía si no hay datos

        }

        // GET: api/Plantaciones/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PlantacionesDetalles>> GetPlantacion(int id)
        {
            var plantacion = await (from p in _context.Plantaciones
                                    join c in _context.Cultivos on p.IdCultivo equals c.IdCultivo
                                    join l in _context.Llaves on p.IdLlave equals l.IdLlave
                                    join r in _context.Ranchos on l.IdRancho equals r.IdRancho
                                    join v in _context.Viveros on p.IdVivero equals v.IdVivero
                                    where p.IdPlantacion == id
                                    select new PlantacionesDetalles
                                    {
                                        IdPlantacion = p.IdPlantacion,
                                        CantidadPlantas = p.CantidadPlantas,
                                        PlantasPorMetro = p.PlantasPorMetro,
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

                                        IdRancho = r.IdRancho,
                                        NombreRancho = r.NombreRancho,
                                        NumeroRancho = r.NumeroRancho,

                                        IdVivero = v.IdVivero,
                                        NombreVivero = v.NombreVivero,
                                        CodigoVivero = v.CodigoVivero
                                    }).FirstOrDefaultAsync();

            if (plantacion == null)
            {
                return NotFound();
            }

            return Ok(plantacion);
        }

        // POST: api/Plantaciones
        [HttpPost]
        public async Task<ActionResult<Plantaciones>> PostPlantacion(Plantaciones plantacion)
        {
            _context.Plantaciones.Add(plantacion);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetPlantacion), new { id = plantacion.IdPlantacion }, plantacion);
        }

        // PUT: api/Plantaciones/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPlantacion(int id, Plantaciones plantacion)
        {
            if (id != plantacion.IdPlantacion)
            {
                return BadRequest();
            }

            _context.Entry(plantacion).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PlantacionExists(id))
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

        // DELETE: api/Plantaciones/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePlantacion(int id)
        {
            var plantacion = await _context.Plantaciones.FindAsync(id);
            if (plantacion == null)
            {
                return NotFound();
            }

            _context.Plantaciones.Remove(plantacion);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PlantacionExists(int id)
        {
            return _context.Plantaciones.Any(e => e.IdPlantacion == id);
        }
    }
}

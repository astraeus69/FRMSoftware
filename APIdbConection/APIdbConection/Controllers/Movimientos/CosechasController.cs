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
    public class CosechasController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CosechasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/ProduccionCosechas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Produccion>>> GetProducciones()
        {
            return await _context.Producciones.ToListAsync();
        }

        // GET: api/ProduccionCosechas/5
        [HttpGet("Producciones/{id}")]
        public async Task<ActionResult<Produccion>> GetProduccion(int id)
        {
            var produccion = await _context.Producciones.FindAsync(id);

            if (produccion == null)
            {
                return NotFound();
            }

            return produccion;
        }

        // GET: api/ProduccionCosechas/Cosechas
        [HttpGet("Cosechas")]
        public async Task<ActionResult<IEnumerable<Cosechas>>> GetCosechas()
        {
            return await _context.Cosechas.ToListAsync();
        }

        // GET: api/ProduccionCosechas/Cosechas/5
        [HttpGet("Cosechas/{id}")]
        public async Task<ActionResult<Cosechas>> GetCosecha(int id)
        {
            var cosecha = await _context.Cosechas.FindAsync(id);

            if (cosecha == null)
            {
                return NotFound();
            }

            return cosecha;
        }

        // GET: api/CosechasProduccion
        [HttpGet("GetCosechasProduccion")]
        public async Task<ActionResult<IEnumerable<CosechasProduccion>>> GetCosechasProduccion()
        {
            var result = await (from c in _context.Cosechas
                                join p in _context.Producciones on c.IdCosecha equals p.IdCosecha into prodGroup
                                from p in prodGroup.DefaultIfEmpty()
                                join pl in _context.Plantaciones on c.IdPlantacion equals pl.IdPlantacion
                                join cu in _context.Cultivos on pl.IdCultivo equals cu.IdCultivo
                                join l in _context.Llaves on pl.IdLlave equals l.IdLlave
                                join r in _context.Ranchos on l.IdRancho equals r.IdRancho
                                select new CosechasProduccion
                                {
                                    IdCosecha = c.IdCosecha,
                                    FechaCosecha = c.FechaCosecha,
                                    NumSemCosecha = c.NumSemCosecha,

                                    IdProduccion = p != null ? p.IdProduccion : 0,
                                    TipoCaja = p != null ? p.TipoCaja : string.Empty,
                                    CantidadCajas = p != null ? p.CantidadCajas : 0,
                                    KilosProceso = p != null ? p.KilosProceso : 0,

                                    IdPlantacion = pl.IdPlantacion,
                                    FechaPlantacion = pl.FechaPlantacion,
                                    NumSemPlantacion = pl.NumSemPlantacion,
                                    EstatusPlantacion = pl.EstatusPlantacion,

                                    IdCultivo = cu.IdCultivo,
                                    TipoBerry = cu.TipoBerry,
                                    Variedad = cu.Variedad,

                                    IdLlave = l.IdLlave,
                                    NombreLlave = l.NombreLlave,
                                    SuperficieHa = l.SuperficieHa,
                                    SuperficieAcres = l.SuperficieAcres,
                                    Disponibilidad = l.Disponibilidad,

                                    IdRancho = r.IdRancho,
                                    NombreRancho = r.NombreRancho,
                                    NumeroRancho = r.NumeroRancho,
                                }).ToListAsync();

            return Ok(result);
        }

        // GET: api/CosechasProduccion/5
        [HttpGet("GetCosechasProduccion/{id}")]
        public async Task<ActionResult<CosechasProduccion>> GetCosechaProduccion(int id)
        {
            var result = await (from c in _context.Cosechas
                                join p in _context.Producciones on c.IdCosecha equals p.IdCosecha into prodGroup
                                from p in prodGroup.DefaultIfEmpty()
                                join pl in _context.Plantaciones on c.IdPlantacion equals pl.IdPlantacion
                                join cu in _context.Cultivos on pl.IdCultivo equals cu.IdCultivo
                                join l in _context.Llaves on pl.IdLlave equals l.IdLlave
                                join r in _context.Ranchos on l.IdRancho equals r.IdRancho
                                where c.IdCosecha == id
                                select new CosechasProduccion
                                {
                                    IdCosecha = c.IdCosecha,
                                    FechaCosecha = c.FechaCosecha,
                                    NumSemCosecha = c.NumSemCosecha,

                                    IdProduccion = p != null ? p.IdProduccion : 0,
                                    TipoCaja = p != null ? p.TipoCaja : string.Empty,
                                    CantidadCajas = p != null ? p.CantidadCajas : 0,
                                    KilosProceso = p != null ? p.KilosProceso : 0,

                                    IdPlantacion = pl.IdPlantacion,
                                    FechaPlantacion = pl.FechaPlantacion,
                                    NumSemPlantacion = pl.NumSemPlantacion,
                                    EstatusPlantacion = pl.EstatusPlantacion,

                                    IdCultivo = cu.IdCultivo,
                                    TipoBerry = cu.TipoBerry,
                                    Variedad = cu.Variedad,

                                    IdLlave = l.IdLlave,
                                    NombreLlave = l.NombreLlave,
                                    SuperficieHa = l.SuperficieHa,
                                    SuperficieAcres = l.SuperficieAcres,
                                    Disponibilidad = l.Disponibilidad,

                                    IdRancho = r.IdRancho,
                                    NombreRancho = r.NombreRancho,
                                    NumeroRancho = r.NumeroRancho,
                                }).FirstOrDefaultAsync();

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }


        // POST: api/ProduccionCosechas
        [HttpPost("Produccion")]
        public async Task<ActionResult<Produccion>> PostProduccion(Produccion produccion)
        {
            _context.Producciones.Add(produccion);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProduccion", new { id = produccion.IdProduccion }, produccion);
        }

        // POST: api/ProduccionCosechas/Cosechas
        [HttpPost("Cosechas")]
        public async Task<ActionResult<Cosechas>> PostCosecha(Cosechas cosecha)
        {
            _context.Cosechas.Add(cosecha);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCosecha", new { id = cosecha.IdCosecha }, cosecha);
        }

        // PUT: api/ProduccionCosechas/5
        [HttpPut("Produccion/{id}")]
        public async Task<IActionResult> PutProduccion(int id, Produccion produccion)
        {
            if (id != produccion.IdProduccion)
            {
                return BadRequest();
            }

            _context.Entry(produccion).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProduccionExists(id))
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

        // PUT: api/ProduccionCosechas/Cosechas/5
        [HttpPut("Cosechas/{id}")]
        public async Task<IActionResult> PutCosecha(int id, Cosechas cosecha)
        {
            if (id != cosecha.IdCosecha)
            {
                return BadRequest();
            }

            _context.Entry(cosecha).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CosechaExists(id))
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

        // DELETE: api/ProduccionCosechas/Produccion/por-cosecha/5
        [HttpDelete("Produccion/porcosecha/{idCosecha}")]
        public async Task<IActionResult> DeleteProduccionesPorCosecha(int idCosecha)
        {
            var producciones = _context.Producciones.Where(p => p.IdCosecha == idCosecha);
            if (!producciones.Any())
            {
                return NotFound("No hay producciones asociadas.");
            }

            _context.Producciones.RemoveRange(producciones);
            await _context.SaveChangesAsync();

            return NoContent();
        }


        // DELETE: api/ProduccionCosechas/Cosechas/5
        [HttpDelete("Cosechas/{id}")]
        public async Task<IActionResult> DeleteCosecha(int id)
        {
            var cosecha = await _context.Cosechas.FindAsync(id);
            if (cosecha == null)
            {
                return NotFound();
            }

            _context.Cosechas.Remove(cosecha);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProduccionExists(int id)
        {
            return _context.Producciones.Any(e => e.IdProduccion == id);
        }

        private bool CosechaExists(int id)
        {
            return _context.Cosechas.Any(e => e.IdCosecha == id);
        }
    }
}

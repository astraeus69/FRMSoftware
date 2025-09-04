using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using APIdbConection.Models;
using APIdbConection.Models.Movimientos;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIdbConection.Controllers.Movimientos
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProcesoController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ProcesoController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Procesos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Proceso>>> GetProcesos()
        {
            return await _context.Procesos.ToListAsync();
        }

        // GET: api/Procesos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Proceso>> GetProceso(int id)
        {
            var proceso = await _context.Procesos.FindAsync(id);

            if (proceso == null)
                return NotFound();

            return proceso;
        }

        // GET: api/ProcesosDetalles
        [HttpGet("GetProcesosDetalles")]
        public async Task<ActionResult<IEnumerable<ProcesoDetalles>>> GetProcesosDetalles()
        {
            var procesosDetalles = await (from p in _context.Procesos
                                          join viaj in _context.Viajes on p.IdViaje equals viaj.IdViaje
                                          join tar in _context.Tarimas on viaj.IdViaje equals tar.IdViaje
                                          join prod in _context.Producciones on tar.IdProduccion equals prod.IdProduccion
                                          join cosecha in _context.Cosechas on prod.IdCosecha equals cosecha.IdCosecha
                                          join pl in _context.Plantaciones on cosecha.IdPlantacion equals pl.IdPlantacion
                                          join c in _context.Cultivos on pl.IdCultivo equals c.IdCultivo
                                          join l in _context.Llaves on pl.IdLlave equals l.IdLlave
                                          join ran in _context.Ranchos on l.IdRancho equals ran.IdRancho
                                          select new ProcesoDetalles
                                          {
                                              IdProceso = p.IdProceso,
                                              ClaseAkg = p.ClaseAkg,
                                              ClaseBkg = p.ClaseBkg,
                                              ClaseCkg = p.ClaseCkg,
                                              Rechazo = p.Rechazo,

                                              IdViaje = viaj.IdViaje,
                                              FechaSalida = viaj.FechaSalida,
                                              NumSemViaje = viaj.NumSemViaje,

                                              KilosProcesoViaje = tar.KilosProcesoViaje,

                                              IdCosecha = cosecha.IdCosecha,
                                              FechaCosecha = cosecha.FechaCosecha,
                                              NumSemCosecha = cosecha.NumSemCosecha,

                                              IdProduccion = prod.IdProduccion,
                                              TipoCaja = prod.TipoCaja,
                                              CantidadCajas = prod.CantidadCajas,
                                              KilosProceso = prod.KilosProceso,

                                              IdPlantacion = pl.IdPlantacion,
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

            return Ok(procesosDetalles);
        }

        // GET: api/ProcesosDetalles/5
        [HttpGet("GetProcesosDetalles/{id}")]
        public async Task<ActionResult<ProcesoDetalles>> GetProcesoDetalles(int id)
        {
            var procesoDetalles = await (from p in _context.Procesos
                                         join viaj in _context.Viajes on p.IdViaje equals viaj.IdViaje
                                         join tar in _context.Tarimas on viaj.IdViaje equals tar.IdViaje
                                         join prod in _context.Producciones on tar.IdProduccion equals prod.IdProduccion
                                         join cosecha in _context.Cosechas on prod.IdCosecha equals cosecha.IdCosecha
                                         join pl in _context.Plantaciones on cosecha.IdPlantacion equals pl.IdPlantacion
                                         join c in _context.Cultivos on pl.IdCultivo equals c.IdCultivo
                                         join l in _context.Llaves on pl.IdLlave equals l.IdLlave
                                         join ran in _context.Ranchos on l.IdRancho equals ran.IdRancho
                                         where p.IdProceso == id
                                         select new ProcesoDetalles
                                         {
                                             IdProceso = p.IdProceso,
                                             ClaseAkg = p.ClaseAkg,
                                             ClaseBkg = p.ClaseBkg,
                                             ClaseCkg = p.ClaseCkg,
                                             Rechazo = p.Rechazo,

                                             IdViaje = viaj.IdViaje,
                                             FechaSalida = viaj.FechaSalida,
                                             NumSemViaje = viaj.NumSemViaje,

                                             KilosProcesoViaje = tar.KilosProcesoViaje,

                                             IdCosecha = cosecha.IdCosecha,
                                             FechaCosecha = cosecha.FechaCosecha,
                                             NumSemCosecha = cosecha.NumSemCosecha,

                                             IdProduccion = prod.IdProduccion,
                                             TipoCaja = prod.TipoCaja,
                                             CantidadCajas = prod.CantidadCajas,
                                             KilosProceso = prod.KilosProceso,
                                             
                                             IdPlantacion = pl.IdPlantacion,
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

            return procesoDetalles != null ? Ok(procesoDetalles) : NotFound();
        }

        // PUT: api/Procesos/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProceso(int id, Proceso proceso)
        {
            if (id != proceso.IdProceso)
                return BadRequest();

            _context.Entry(proceso).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _context.Procesos.AnyAsync(e => e.IdProceso == id))
                    return NotFound();

                throw;
            }

            return NoContent();
        }

        // POST: api/Procesos
        [HttpPost]
        public async Task<ActionResult<Proceso>> PostProceso(Proceso proceso)
        {
            bool existeProceso = await _context.Procesos.AnyAsync(p => p.IdViaje == proceso.IdViaje);

            if (existeProceso)
                return BadRequest("Ya existe un proceso registrado para esta producción.");

            _context.Procesos.Add(proceso);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetProceso), new { id = proceso.IdProceso }, proceso);
        }

        // DELETE: api/Procesos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProceso(int id)
        {
            var proceso = await _context.Procesos.FindAsync(id);
            if (proceso == null)
                return NotFound();

            _context.Procesos.Remove(proceso);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}

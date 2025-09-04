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
    public class ViajesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ViajesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Viajes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Viajes>>> GetViajes()
        {
            return await _context.Viajes.ToListAsync();
        }

        // GET: api/Viajes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Viajes>> GetViaje(int id)
        {
            var viaje = await _context.Viajes.FindAsync(id);

            if (viaje == null)
            {
                return NotFound();
            }

            return viaje;
        }

        // GET: api/Viajes/Tarimas
        [HttpGet("Tarimas")]
        public async Task<ActionResult<IEnumerable<Tarimas>>> GetTarimas()
        {
            return await _context.Tarimas.ToListAsync();
        }

        [HttpGet("GetTarimasPorId/{id}")]
        public async Task<ActionResult<Tarimas>> GetTarimasPorId(int id)
        {
            var tarima = await _context.Tarimas.FindAsync(id);

            if (tarima == null)
            {
                return NotFound();
            }

            return tarima;
        }

        [HttpGet("GetTarimaPorIdViaje/{idViaje}")]
        public async Task<ActionResult<Tarimas>> GetTarimaPorIdViaje(int idViaje)
        {
            var tarima = await _context.Tarimas
                .FirstOrDefaultAsync(t => t.IdViaje == idViaje);

            if (tarima == null)
            {
                return NotFound();
            }

            return tarima;
        }


        // Obtener todos los viajes con detalles
        [HttpGet("GetViajesDetalles")]
        public async Task<ActionResult<IEnumerable<ViajesDetalles>>> GetViajesDetalles()
        {
            var result = await (from v in _context.Viajes
                                join t in _context.Tarimas on v.IdViaje equals t.IdViaje into tarimasGroup
                                from t in tarimasGroup.DefaultIfEmpty()
                                join p in _context.Producciones on t.IdProduccion equals p.IdProduccion into produccionGroup
                                from p in produccionGroup.DefaultIfEmpty()
                                join c in _context.Cosechas on p.IdCosecha equals c.IdCosecha into cosechaGroup
                                from c in cosechaGroup.DefaultIfEmpty()
                                join pl in _context.Plantaciones on c.IdPlantacion equals pl.IdPlantacion into plantacionGroup
                                from pl in plantacionGroup.DefaultIfEmpty()
                                join cu in _context.Cultivos on pl.IdCultivo equals cu.IdCultivo into cultivoGroup
                                from cu in cultivoGroup.DefaultIfEmpty()
                                join r in _context.Ranchos on c.IdPlantacion equals r.IdRancho into ranchoGroup
                                from r in ranchoGroup.DefaultIfEmpty()
                                join e in _context.Empleados on v.IdConductor equals e.IdEmpleado into empleadoGroup
                                from e in empleadoGroup.DefaultIfEmpty()
                                join ve in _context.Vehiculos on v.IdVehiculo equals ve.IdVehiculo into vehiculoGroup
                                from ve in vehiculoGroup.DefaultIfEmpty()
                                select new ViajesDetalles
                                {
                                    IdViaje = v.IdViaje,
                                    FechaSalida = v.FechaSalida,
                                    NumSemViaje = v.NumSemViaje,
                                    EstadoAprobacion = v.EstadoAprobacion,

                                    // Datos de Tarimas
                                    IdTarima = t != null ? t.IdTarima : 0,
                                    CantidadCajasViaje = t != null ? t.CantidadCajasViaje : 0,
                                    Licencia = t != null ? t.Licencia : 0,
                                    KilosProcesoViaje = t != null ? t.KilosProcesoViaje : 0,

                                    // Cosecha
                                    IdCosecha = c != null ? c.IdCosecha : 0,
                                    FechaCosecha = c != null ? c.FechaCosecha : DateTime.MinValue,
                                    NumSemCosecha = c != null ? c.NumSemCosecha : 0,

                                    // Producción
                                    IdProduccion = p != null ? p.IdProduccion : 0,
                                    TipoCaja = p != null ? p.TipoCaja : string.Empty,
                                    CantidadCajas = p != null ? p.CantidadCajas : 0,
                                    KilosProceso = p != null ? p.KilosProceso : 0,

                                    // Cultivo
                                    IdCultivo = cu != null ? cu.IdCultivo : 0,
                                    TipoBerry = cu != null ? cu.TipoBerry : string.Empty,
                                    Variedad = cu != null ? cu.Variedad : string.Empty,

                                    // Rancho
                                    IdRancho = r != null ? r.IdRancho : 0,
                                    NombreRancho = r != null ? r.NombreRancho : string.Empty,
                                    NumeroRancho = r != null ? r.NumeroRancho : string.Empty,

                                    // Empleado (Conductor)
                                    IdEmpleado = e != null ? e.IdEmpleado : 0,
                                    Nombre = e != null ? e.Nombre : string.Empty,
                                    Telefono = e != null ? e.Telefono : string.Empty,

                                    // Vehículo
                                    IdVehiculo = ve != null ? ve.IdVehiculo : 0,
                                    Placas = ve != null ? ve.Placas : string.Empty,
                                    Modelo = ve != null ? ve.Modelo : string.Empty,
                                    Marca = ve != null ? ve.Marca : string.Empty,

                                }).ToListAsync();

            return Ok(result);
        }

        // Obtener los detalles de un viaje por IdViaje
        [HttpGet("GetViajeDetalles/{id}")]
        public async Task<ActionResult<ViajesDetalles>> GetViajeDetalles(int id)
        {
            var result = await (from v in _context.Viajes
                                join t in _context.Tarimas on v.IdViaje equals t.IdViaje into tarimasGroup
                                from t in tarimasGroup.DefaultIfEmpty()
                                join p in _context.Producciones on t.IdProduccion equals p.IdProduccion into produccionGroup
                                from p in produccionGroup.DefaultIfEmpty()
                                join c in _context.Cosechas on p.IdCosecha equals c.IdCosecha into cosechaGroup
                                from c in cosechaGroup.DefaultIfEmpty()
                                join pl in _context.Plantaciones on c.IdPlantacion equals pl.IdPlantacion into plantacionGroup
                                from pl in plantacionGroup.DefaultIfEmpty()
                                join cu in _context.Cultivos on pl.IdCultivo equals cu.IdCultivo into cultivoGroup
                                from cu in cultivoGroup.DefaultIfEmpty()
                                join r in _context.Ranchos on c.IdPlantacion equals r.IdRancho into ranchoGroup
                                from r in ranchoGroup.DefaultIfEmpty()
                                join e in _context.Empleados on v.IdConductor equals e.IdEmpleado into empleadoGroup
                                from e in empleadoGroup.DefaultIfEmpty()
                                join ve in _context.Vehiculos on v.IdVehiculo equals ve.IdVehiculo into vehiculoGroup
                                from ve in vehiculoGroup.DefaultIfEmpty()
                                where v.IdViaje == id
                                select new ViajesDetalles
                                {
                                    IdViaje = v.IdViaje,
                                    FechaSalida = v.FechaSalida,
                                    NumSemViaje = v.NumSemViaje,
                                    EstadoAprobacion = v.EstadoAprobacion,

                                    IdTarima = t != null ? t.IdTarima : 0,
                                    CantidadCajasViaje = t != null ? t.CantidadCajasViaje : 0,
                                    Licencia = t != null ? t.Licencia : 0,
                                    KilosProcesoViaje = t != null ? t.KilosProcesoViaje : 0,

                                    IdCosecha = c != null ? c.IdCosecha : 0,
                                    FechaCosecha = c != null ? c.FechaCosecha : DateTime.MinValue,
                                    NumSemCosecha = c != null ? c.NumSemCosecha : 0,

                                    IdProduccion = p != null ? p.IdProduccion : 0,
                                    TipoCaja = p != null ? p.TipoCaja : string.Empty,
                                    CantidadCajas = p != null ? p.CantidadCajas : 0,
                                    KilosProceso = p != null ? p.KilosProceso : 0,

                                    IdCultivo = cu != null ? cu.IdCultivo : 0,
                                    TipoBerry = cu != null ? cu.TipoBerry : string.Empty,
                                    Variedad = cu != null ? cu.Variedad : string.Empty,

                                    IdRancho = r != null ? r.IdRancho : 0,
                                    NombreRancho = r != null ? r.NombreRancho : string.Empty,
                                    NumeroRancho = r != null ? r.NumeroRancho : string.Empty,

                                    IdEmpleado = e != null ? e.IdEmpleado : 0,
                                    Nombre = e != null ? e.Nombre : string.Empty,
                                    Telefono = e != null ? e.Telefono : string.Empty,

                                    IdVehiculo = ve != null ? ve.IdVehiculo : 0,
                                    Placas = ve != null ? ve.Placas : string.Empty,
                                    Modelo = ve != null ? ve.Modelo : string.Empty,
                                    Marca = ve != null ? ve.Marca : string.Empty,

                                }).FirstOrDefaultAsync();

            if (result == null)
            {
                return NotFound("No se encontró el viaje con el ID proporcionado.");
            }

            return Ok(result);
        }


        // POST: api/Viajes
        [HttpPost]
        public async Task<ActionResult<Viajes>> PostViaje(Viajes viaje)
        {
            _context.Viajes.Add(viaje);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetViaje), new { id = viaje.IdViaje }, viaje);
        }

        // POST: api/Viajes/Tarimas
        [HttpPost("Tarimas")]
        public async Task<ActionResult<Tarimas>> PostTarima(Tarimas tarimaDto)
        {
            // Validar que haya producción asociada
            if (tarimaDto.IdProduccion == 0)
            {
                return BadRequest("No se puede grabar la tarima sin una producción asociada.");
            }

            // Validar reglas de negocio
            if (tarimaDto.CantidadCajasViaje > 0)
            {
                tarimaDto.KilosProcesoViaje = 0; // Si hay cajas, los kilos de proceso deben ser 0
            }
            else
            {
                tarimaDto.CantidadCajasViaje = 0;
                tarimaDto.Licencia = 0; // Si hay kilos de proceso, las cajas y licencia deben ser 0
            }

            var tarima = new Tarimas
            {
                IdProduccion = tarimaDto.IdProduccion,
                IdViaje = tarimaDto.IdViaje,
                CantidadCajasViaje = tarimaDto.CantidadCajasViaje,
                Licencia = tarimaDto.Licencia,
                KilosProcesoViaje = tarimaDto.KilosProcesoViaje
            };

            _context.Tarimas.Add(tarima);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetTarimas), new { id = tarima.IdTarima }, tarima);
        }


        // POST: api/Viajes/Tarimas/Lote
        [HttpPost("Tarimas/Lote")]
        public async Task<ActionResult> PostTarimasLote(List<Tarimas> tarimasDto)
        {
            if (tarimasDto == null || !tarimasDto.Any())
            {
                return BadRequest("No hay tarimas para registrar.");
            }

            foreach (var tarima in tarimasDto)
            {
                if (tarima.IdProduccion == 0)
                {
                    return BadRequest("Cada tarima debe tener una producción asociada.");
                }

                _context.Tarimas.Add(new Tarimas
                {
                    IdProduccion = tarima.IdProduccion,
                    IdViaje = tarima.IdViaje,
                    CantidadCajasViaje = tarima.CantidadCajasViaje,
                    Licencia = tarima.Licencia,
                    KilosProcesoViaje = tarima.KilosProcesoViaje
                });
            }

            await _context.SaveChangesAsync();
            return Ok(new { Success = true, Message = "Tarimas registradas correctamente." });
        }


        // PUT: api/Viajes/{id} (Actualizar un viaje)
        [HttpPut("ActualizarViajes/{id}")]
        public async Task<IActionResult> PutViaje(int id, Viajes viajeDto)
        {
            if (id != viajeDto.IdViaje)
            {
                return BadRequest("El ID del viaje no coincide.");
            }

            var viajeExistente = await _context.Viajes.FindAsync(id);
            if (viajeExistente == null)
            {
                return NotFound("El viaje no existe.");
            }

            // Actualizar datos del viaje
            viajeExistente.FechaSalida = viajeDto.FechaSalida;
            viajeExistente.NumSemViaje = viajeDto.NumSemViaje;
            viajeExistente.IdConductor = viajeDto.IdConductor;
            viajeExistente.IdVehiculo = viajeDto.IdVehiculo;
            viajeExistente.EstadoAprobacion = viajeDto.EstadoAprobacion;

            await _context.SaveChangesAsync();
            return Ok(new { Success = true, Message = "Viaje actualizado correctamente." });
        }


        // PUT: api/Tarimas/{id} (Actualizar una tarima)
        [HttpPut("ActualizarTarima/{id}")]
        public async Task<IActionResult> PutTarima(int id, Tarimas tarimaDto)
        {
            if (id != tarimaDto.IdTarima)
            {
                return BadRequest("El ID de la tarima no coincide.");
            }

            var tarimaExistente = await _context.Tarimas.FindAsync(id);
            if (tarimaExistente == null)
            {
                return NotFound("La tarima no existe.");
            }

            // Actualizar datos de la tarima
            tarimaExistente.IdProduccion = tarimaDto.IdProduccion;
            tarimaExistente.CantidadCajasViaje = tarimaDto.CantidadCajasViaje;
            tarimaExistente.Licencia = tarimaDto.Licencia;
            tarimaExistente.KilosProcesoViaje = tarimaDto.KilosProcesoViaje;

            await _context.SaveChangesAsync();
            return Ok(new { Success = true, Message = "Tarima actualizada correctamente." });
        }

        // PUT: api/Tarimas/Lote (Actualizar múltiples tarimas)
        [HttpPut("ActualizarTarimas/Lote")]
        public async Task<IActionResult> PutTarimasLote(List<Tarimas> tarimasDto)
        {
            if (tarimasDto == null || !tarimasDto.Any())
            {
                return BadRequest("No hay tarimas para actualizar.");
            }

            foreach (var tarimaDto in tarimasDto)
            {
                var tarimaExistente = await _context.Tarimas.FindAsync(tarimaDto.IdTarima);
                if (tarimaExistente != null)
                {
                    tarimaExistente.IdProduccion = tarimaDto.IdProduccion;
                    tarimaExistente.CantidadCajasViaje = tarimaDto.CantidadCajasViaje;
                    tarimaExistente.Licencia = tarimaDto.Licencia;
                    tarimaExistente.KilosProcesoViaje = tarimaDto.KilosProcesoViaje;
                }
            }

            await _context.SaveChangesAsync();
            return Ok(new { Success = true, Message = "Tarimas actualizadas correctamente." });
        }


        // DELETE: api/Viajes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteViaje(int id)
        {
            var viaje = await _context.Viajes.FindAsync(id);
            if (viaje == null)
            {
                return NotFound();
            }

            // 🔸 Eliminar las tarimas relacionadas con el viaje
            var tarimasRelacionadas = await _context.Tarimas
                .Where(t => t.IdViaje == id)
                .ToListAsync();

            if (tarimasRelacionadas.Any())
            {
                _context.Tarimas.RemoveRange(tarimasRelacionadas);
            }

            // 🔸 Eliminar el viaje
            _context.Viajes.Remove(viaje);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ViajeExists(int id)
        {
            return _context.Viajes.Any(e => e.IdViaje == id);
        }
    }
}

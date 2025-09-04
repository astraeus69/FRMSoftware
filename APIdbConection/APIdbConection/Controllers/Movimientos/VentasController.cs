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
    public class VentasController : ControllerBase
    {

        private readonly ApplicationDbContext _context;

        public VentasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Ventas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Ventas>>> GetVentas()
        {
            return await _context.Ventas.ToListAsync();
        }

        // GET: api/Ventas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Ventas>> GetVenta(int id)
        {
            var venta = await _context.Ventas.FindAsync(id);

            if (venta == null)
            {
                return NotFound();
            }

            return venta;
        }

        // GET: api/Ventas/DetalleVentas
        [HttpGet("DetalleVentas")]
        public async Task<ActionResult<IEnumerable<VentasDetalles>>> GetDetalleVentas()
        {
            return await _context.VentasDetalles.ToListAsync();
        }

        // GET: api/Ventas/GetDetalleVentaPorId/5
        [HttpGet("GetDetalleVentaPorId/{id}")]
        public async Task<ActionResult<VentasDetalles>> GetDetalleVentaPorId(int id)
        {
            var detalleVenta = await _context.VentasDetalles.FindAsync(id);

            if (detalleVenta == null)
            {
                return NotFound();
            }

            return detalleVenta;
        }

        // GET: api/Ventas/GetIdsViajesConVentas
        [HttpGet("GetIdsViajesConVentas")]
        public async Task<ActionResult<IEnumerable<int>>> GetIdsViajesConVentas()
        {
            var idsViajesConVentas = await (
                from vd in _context.VentasDetalles
                join t in _context.Tarimas on vd.IdTarima equals t.IdTarima
                select t.IdViaje
            )
            .Distinct()
            .ToListAsync();

            return Ok(idsViajesConVentas);
        }

        [HttpGet("GetVentasDC")]
        public async Task<ActionResult<IEnumerable<VentasDetallesCompletos>>> GetVentasDC()
        {
            var ventas = await (
                    from v in _context.Viajes
                    join t in _context.Tarimas on v.IdViaje equals t.IdViaje
                    join vd in _context.VentasDetalles on t.IdTarima equals vd.IdTarima
                    join ve in _context.Ventas on vd.IdVenta equals ve.IdVenta
                    join p in _context.Producciones on t.IdProduccion equals p.IdProduccion into prodGroup
                    from p in prodGroup.DefaultIfEmpty()
                    join c in _context.Cosechas on p.IdCosecha equals c.IdCosecha into cosechaGroup
                    from c in cosechaGroup.DefaultIfEmpty()
                    join pl in _context.Plantaciones on c.IdPlantacion equals pl.IdPlantacion into plantacionGroup
                    from pl in plantacionGroup.DefaultIfEmpty()
                    join cu in _context.Cultivos on pl.IdCultivo equals cu.IdCultivo into cultivoGroup
                    from cu in cultivoGroup.DefaultIfEmpty()
                    join r in _context.Ranchos on c.IdPlantacion equals r.IdRancho into ranchoGroup
                    from r in ranchoGroup.DefaultIfEmpty()
                    select new VentasDetallesCompletos
                    {
                        IdVenta = ve.IdVenta,
                        FechaFacturacion = ve.FechaFacturacion,
                        TotalVenta = ve.Total,
                        PrecioDolar = ve.PrecioDolar,

                        IdViaje = v.IdViaje,
                        FechaSalida = v.FechaSalida,
                        NumSemViaje = v.NumSemViaje,

                        IdCultivo = cu != null ? cu.IdCultivo : 0,
                        TipoBerry = cu != null ? cu.TipoBerry : string.Empty,
                        Variedad = cu != null ? cu.Variedad : string.Empty,

                        IdRancho = r != null ? r.IdRancho : 0,
                        NombreRancho = r != null ? r.NombreRancho : string.Empty,
                        NumeroRancho = r != null ? r.NumeroRancho : string.Empty,

                        IdTarima = t != null ? t.IdTarima : 0,
                        CantidadCajasViaje = t != null ? t.CantidadCajasViaje : 0,
                        Licencia = t != null ? t.Licencia : 0,
                        KilosProcesoViaje = t != null ? t.KilosProcesoViaje : 0,

                        FechaRecepcion = vd.FechaRecepcion,
                        PrecioVentaTarima = vd.PrecioVentaTarima
                    }
            ).ToListAsync();

            return Ok(ventas);
        }


        [HttpGet("GetVentasPorId/{idViaje}")]
        public async Task<ActionResult<IEnumerable<VentasDetallesCompletos>>> GetVentasPorId(int idVenta)
        {
            var ventas = await (
                    from v in _context.Viajes
                    join t in _context.Tarimas on v.IdViaje equals t.IdViaje
                    join vd in _context.VentasDetalles on t.IdTarima equals vd.IdTarima
                    join ve in _context.Ventas on vd.IdVenta equals ve.IdVenta
                    join p in _context.Producciones on t.IdProduccion equals p.IdProduccion into prodGroup
                    from p in prodGroup.DefaultIfEmpty()
                    join c in _context.Cosechas on p.IdCosecha equals c.IdCosecha into cosechaGroup
                    from c in cosechaGroup.DefaultIfEmpty()
                    join pl in _context.Plantaciones on c.IdPlantacion equals pl.IdPlantacion into plantacionGroup
                    from pl in plantacionGroup.DefaultIfEmpty()
                    join cu in _context.Cultivos on pl.IdCultivo equals cu.IdCultivo into cultivoGroup
                    from cu in cultivoGroup.DefaultIfEmpty()
                    join r in _context.Ranchos on c.IdPlantacion equals r.IdRancho into ranchoGroup
                    from r in ranchoGroup.DefaultIfEmpty()
                    where ve.IdVenta == idVenta
                    select new VentasDetallesCompletos
                    {                
                        IdVenta = ve.IdVenta,
                        FechaFacturacion = ve.FechaFacturacion,
                        TotalVenta = ve.Total,
                        PrecioDolar = ve.PrecioDolar,

                        IdViaje = v.IdViaje,
                        FechaSalida = v.FechaSalida,
                        NumSemViaje = v.NumSemViaje,

                        IdCultivo = cu != null ? cu.IdCultivo : 0,
                        TipoBerry = cu != null ? cu.TipoBerry : string.Empty,
                        Variedad = cu != null ? cu.Variedad : string.Empty,

                        IdRancho = r != null ? r.IdRancho : 0,
                        NombreRancho = r != null ? r.NombreRancho : string.Empty,
                        NumeroRancho = r != null ? r.NumeroRancho : string.Empty,

                        IdTarima = t != null ? t.IdTarima : 0,
                        CantidadCajasViaje = t != null ? t.CantidadCajasViaje : 0,
                        Licencia = t != null ? t.Licencia : 0,
                        KilosProcesoViaje = t != null ? t.KilosProcesoViaje : 0,

                        FechaRecepcion = vd.FechaRecepcion,
                        PrecioVentaTarima = vd.PrecioVentaTarima
                }
            ).ToListAsync();

            return Ok(ventas);
        }



        // POST: api/Ventas
        [HttpPost]
        public async Task<ActionResult<Ventas>> PostVenta(Ventas venta)
        {
            _context.Ventas.Add(venta);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetVenta), new { id = venta.IdVenta }, venta);
        }

        // POST: api/Ventas/VentasDetalles
        [HttpPost("VentasDetalles")]
        public async Task<ActionResult<VentasDetalles>> PostVentasDetalle(VentasDetalles detalleDto)
        {
            // Validar que haya una venta asociada
            if (detalleDto.IdVenta == 0)
            {
                return BadRequest("No se puede registrar el detalle sin una venta asociada.");
            }

            var detalleVenta = new VentasDetalles
            {
                IdVenta = detalleDto.IdVenta,
                IdTarima = detalleDto.IdTarima,
                PrecioVentaTarima = detalleDto.PrecioVentaTarima,
                FechaRecepcion = detalleDto.FechaRecepcion
            };

            _context.VentasDetalles.Add(detalleVenta);
            await _context.SaveChangesAsync();

            return Ok(detalleVenta); // No hay IdDetalleVenta, así que no usamos CreatedAtAction aquí
        }

        // POST: api/Ventas/VentasDetalles/Lote
        [HttpPost("VentasDetalles/Lote")]
        public async Task<ActionResult> PostVentasDetallesLote(List<VentasDetalles> detallesDto)
        {
            if (detallesDto == null || !detallesDto.Any())
            {
                return BadRequest("No hay detalles de venta para registrar.");
            }

            foreach (var detalle in detallesDto)
            {
                if (detalle.IdVenta == 0)
                {
                    return BadRequest("Cada detalle debe estar asociado a una venta.");
                }

                _context.VentasDetalles.Add(new VentasDetalles
                {
                    IdVenta = detalle.IdVenta,
                    IdTarima = detalle.IdTarima,
                    PrecioVentaTarima = detalle.PrecioVentaTarima,
                    FechaRecepcion = detalle.FechaRecepcion
                });
            }

            await _context.SaveChangesAsync();
            return Ok(new { Success = true, Message = "Detalles de venta registrados correctamente." });
        }

        // DELETE: api/Ventas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVenta(int id)
        {
            var venta = await _context.Ventas.FindAsync(id);
            if (venta == null)
            {
                return NotFound();
            }

            // Eliminar los detalles relacionados con la venta
            var detallesRelacionados = await _context.VentasDetalles
                .Where(d => d.IdVenta == id)
                .ToListAsync();

            if (detallesRelacionados.Any())
            {
                _context.VentasDetalles.RemoveRange(detallesRelacionados);
            }

            // Eliminar la venta
            _context.Ventas.Remove(venta);
            await _context.SaveChangesAsync();

            return NoContent();
        }

    }
}

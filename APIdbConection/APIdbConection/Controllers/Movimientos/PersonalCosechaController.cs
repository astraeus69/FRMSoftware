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
    public class PersonalCosechaController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public PersonalCosechaController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/PersonalCosecha
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PersonalCosecha>>> GetPersonalCosecha()
        {
            return await _context.PersonalCosecha.ToListAsync();
        }

        // GET: api/PersonalCosecha/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PersonalCosecha>> GetPersonalCosecha(int id)
        {
            var personalCosecha = await _context.PersonalCosecha.FindAsync(id);

            if (personalCosecha == null)
            {
                return NotFound();
            }

            return personalCosecha;
        }

        // GET: api/PersonalCosechaDetalles
        [HttpGet("GetPersonalCosechaDetalles")]
        public async Task<ActionResult<IEnumerable<PersonalCosechaDetalles>>> GetPersonalCosechaDetalles()
        {
            var personalCosechaDetalles = await (from p in _context.PersonalCosecha
                                                 join c in _context.Cosechas on p.IdCosecha equals c.IdCosecha
                                                 join pl in _context.Plantaciones on c.IdPlantacion equals pl.IdPlantacion
                                                 join cu in _context.Cultivos on pl.IdCultivo equals cu.IdCultivo
                                                 join e in _context.Empleados on p.IdEmpleado equals e.IdEmpleado
                                                 select new PersonalCosechaDetalles
                                                 {
                                                     IdPersonalCosecha = p.IdPersonalCosecha,
                                                     Jarras = p.Jarras ?? 0,
                                                     PrecioJarra = p.PrecioJarra ?? 0,
                                                     IdCosecha = c.IdCosecha,
                                                     FechaCosecha = c.FechaCosecha,
                                                     IdCultivo = cu.IdCultivo,
                                                     TipoBerry = cu.TipoBerry,
                                                     Variedad = cu.Variedad,
                                                     IdEmpleado = e.IdEmpleado,
                                                     Nombre = e.Nombre,
                                                     Telefono = e.Telefono,
                                                     Estatus = e.Estatus,
                                                 }).ToListAsync();

            return Ok(personalCosechaDetalles ?? new List<PersonalCosechaDetalles>());
        }

        // GET: api/PersonalCosechaDetalles/5
        [HttpGet("GetPersonalCosechaDetalles/{id}")]
        public async Task<ActionResult<PersonalCosechaDetalles>> GetPersonalCosechaDetalles(int id)
        {
            var personalCosechaDetalle = await (from p in _context.PersonalCosecha
                                                join c in _context.Cosechas on p.IdCosecha equals c.IdCosecha
                                                join pl in _context.Plantaciones on c.IdPlantacion equals pl.IdPlantacion
                                                join cu in _context.Cultivos on pl.IdCultivo equals cu.IdCultivo
                                                join e in _context.Empleados on p.IdEmpleado equals e.IdEmpleado
                                                where p.IdPersonalCosecha == id
                                                select new PersonalCosechaDetalles
                                                {
                                                    IdPersonalCosecha = p.IdPersonalCosecha,
                                                    Jarras = p.Jarras ?? 0,
                                                    PrecioJarra = p.PrecioJarra ?? 0,
                                                    IdCosecha = c.IdCosecha,
                                                    FechaCosecha = c.FechaCosecha,
                                                    IdCultivo = cu.IdCultivo,
                                                    TipoBerry = cu.TipoBerry,
                                                    Variedad = cu.Variedad,
                                                    IdEmpleado = e.IdEmpleado,
                                                    Nombre = e.Nombre,
                                                    Telefono = e.Telefono,
                                                    Estatus = e.Estatus,
                                                }).FirstOrDefaultAsync();

            return Ok(personalCosechaDetalle ?? new PersonalCosechaDetalles());
        }

        // PUT: api/PersonalCosecha/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPersonalCosecha(int id, PersonalCosecha personalCosecha)
        {
            if (id != personalCosecha.IdPersonalCosecha)
            {
                return BadRequest();
            }

            _context.Entry(personalCosecha).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PersonalCosechaExists(id))
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

        // POST: api/PersonalCosecha
        [HttpPost]
        public async Task<ActionResult<PersonalCosecha>> PostPersonalCosecha(PersonalCosecha personalCosecha)
        {
            _context.PersonalCosecha.Add(personalCosecha);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPersonalCosecha", new { id = personalCosecha.IdPersonalCosecha }, personalCosecha);
        }

        // DELETE: api/PersonalCosecha/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePersonalCosecha(int id)
        {
            var personalCosecha = await _context.PersonalCosecha.FindAsync(id);
            if (personalCosecha == null)
            {
                return NotFound();
            }

            _context.PersonalCosecha.Remove(personalCosecha);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PersonalCosechaExists(int id)
        {
            return _context.PersonalCosecha.Any(e => e.IdPersonalCosecha == id);
        }
    }
}

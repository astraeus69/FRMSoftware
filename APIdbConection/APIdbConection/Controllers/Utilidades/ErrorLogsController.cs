using Microsoft.AspNetCore.Mvc;
using APIdbConection.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APIdbConection.Models.Utilidades;

namespace APIdbConection.Controllers.Utilidades
{
    [Route("api/[controller]")]
    [ApiController]
    public class ErrorLogsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ErrorLogsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // POST: api/ErrorLogs
        [HttpPost]
        public async Task<ActionResult<ErrorLog>> PostErrorLog(ErrorLog errorLog)
        {
            _context.ErrorLogs.Add(errorLog);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetErrorLog), new { id = errorLog.Id }, errorLog);
        }

        // GET: api/ErrorLogs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ErrorLog>>> GetErrorLogs()
        {
            return await _context.ErrorLogs.OrderByDescending(e => e.ErrorTime).ToListAsync();
        }

        // GET: api/ErrorLogs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ErrorLog>> GetErrorLog(int id)
        {
            var errorLog = await _context.ErrorLogs.FindAsync(id);

            if (errorLog == null)
            {
                return NotFound();
            }

            return errorLog;
        }
    }
}

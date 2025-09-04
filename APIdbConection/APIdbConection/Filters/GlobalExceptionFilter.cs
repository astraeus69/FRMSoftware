using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Linq;
using System.Threading.Tasks;
using APIdbConection.Models;
using APIdbConection.Models.Utilidades;


namespace APIdbConection.Filters
{
    public class GlobalExceptionFilter : IAsyncExceptionFilter
    {
        private readonly ApplicationDbContext _context;

        public GlobalExceptionFilter(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task OnExceptionAsync(ExceptionContext context)
        {
            var errorDto = new ErrorLog
            {
                UserName = context.HttpContext.User.Identity?.Name ?? "Unknown",
                ErrorMessage = context.Exception.Message,
                ErrorProcedure = context.Exception.Source,
                ErrorLine = context.Exception.StackTrace?
                    .Split('\n')
                    .FirstOrDefault(line => line.Contains(": line "))?
                    .Split(':')
                    .LastOrDefault() is string lineNumber && int.TryParse(lineNumber, out int num)
                    ? Convert.ToString(num) : "Unknown",
                ErrorTime = DateTime.UtcNow
            };

            // Mapear el DTO a la entidad del modelo de base de datos
            var errorEntity = new ErrorLog
            {
                UserName = errorDto.UserName,
                ErrorMessage = errorDto.ErrorMessage,
                ErrorProcedure = errorDto.ErrorProcedure,
                ErrorLine = errorDto.ErrorLine,
                ErrorTime = errorDto.ErrorTime
            };

            _context.ErrorLogs.Add(errorEntity);
            await _context.SaveChangesAsync();

            context.Result = new ObjectResult(new { error = "An internal error occurred." })
            {
                StatusCode = 500
            };

            context.ExceptionHandled = true;
        }
    }
}

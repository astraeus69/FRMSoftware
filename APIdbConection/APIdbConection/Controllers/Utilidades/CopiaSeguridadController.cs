using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace APIdbConection.Controllers.Utilidades
{
    [Route("api/[controller]")]
    [ApiController]
    public class CopiaSeguridadController : ControllerBase
    {
        private readonly string _connectionString;

        public CopiaSeguridadController(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        [HttpGet("descargar")]
        public async Task<IActionResult> DescargarCopiaSeguridad()
        {
            try
            {
                string nombreArchivo = $"copia_seguridad_{DateTime.Now:yyyyMMddHHmmss}.sql";
                var sb = new StringBuilder();

                using (var conexion = new MySqlConnection(_connectionString))
                {
                    await conexion.OpenAsync();

                    // Obtener todas las tablas de la base de datos
                    var obtenerTablasQuery = "SHOW TABLES;";
                    using (var cmdTablas = new MySqlCommand(obtenerTablasQuery, conexion))
                    using (var readerTablas = await cmdTablas.ExecuteReaderAsync())
                    {
                        var tablas = new List<string>();
                        while (await readerTablas.ReadAsync())
                        {
                            tablas.Add(readerTablas.GetString(0));
                        }
                        readerTablas.Close();

                        // Generar estructura y datos de cada tabla
                        foreach (var nombreTabla in tablas)
                        {
                            await GenerarScriptTabla(nombreTabla, conexion, sb);
                            await GenerarDatosTabla(nombreTabla, conexion, sb);
                        }
                    }
                }

                // Convertir el contenido en un archivo descargable
                var bytes = Encoding.UTF8.GetBytes(sb.ToString());
                var memoryStream = new MemoryStream(bytes);

                return File(memoryStream, "application/sql", nombreArchivo);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensaje = "Error al generar la copia de seguridad", error = ex.Message });
            }
        }

        private async Task GenerarScriptTabla(string nombreTabla, MySqlConnection conexion, StringBuilder sb)
        {
            var obtenerEstructuraTablaQuery = $"SHOW CREATE TABLE `{nombreTabla}`;";
            using (var cmdEstructura = new MySqlCommand(obtenerEstructuraTablaQuery, conexion))
            using (var readerEstructura = await cmdEstructura.ExecuteReaderAsync())
            {
                if (await readerEstructura.ReadAsync())
                {
                    string createTableScript = readerEstructura.GetString(1);
                    sb.AppendLine($"DROP TABLE IF EXISTS `{nombreTabla}`;");
                    sb.AppendLine(createTableScript + ";");
                    sb.AppendLine();
                }
            }
        }

        private async Task GenerarDatosTabla(string nombreTabla, MySqlConnection conexion, StringBuilder sb)
        {
            var obtenerDatosQuery = $"SELECT * FROM `{nombreTabla}`;";
            using (var cmdDatos = new MySqlCommand(obtenerDatosQuery, conexion))
            using (var readerDatos = await cmdDatos.ExecuteReaderAsync())
            {
                while (await readerDatos.ReadAsync())
                {
                    var columnas = new List<string>();
                    var valores = new List<string>();

                    for (int i = 0; i < readerDatos.FieldCount; i++)
                    {
                        columnas.Add($"`{readerDatos.GetName(i)}`");
                        var valor = readerDatos.IsDBNull(i) ? "NULL" : $"'{readerDatos.GetValue(i).ToString().Replace("'", "''")}'";
                        valores.Add(valor);
                    }

                    sb.AppendLine($"INSERT INTO `{nombreTabla}` ({string.Join(", ", columnas)}) VALUES ({string.Join(", ", valores)});");
                }
            }
            sb.AppendLine();
        }
    }
}

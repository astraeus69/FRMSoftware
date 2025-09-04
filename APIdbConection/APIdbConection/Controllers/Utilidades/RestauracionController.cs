using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace APIdbConection.Controllers.Utilidades
{
    [Route("api/[controller]")]
    [ApiController]
    public class RestauracionController : ControllerBase
    {
        private readonly string _connectionString;

        public RestauracionController(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        [HttpPost("restaurar")]
        public async Task<IActionResult> RestaurarBaseDeDatos([FromForm] IFormFile archivoSql)
        {
            if (archivoSql == null || archivoSql.Length == 0)
            {
                return BadRequest("No se proporcionó un archivo.");
            }

            try
            {
                // 1️ Conectar sin base de datos y verificar si existe
                var builder = new MySqlConnectionStringBuilder(_connectionString);
                string conexionSinBD = $"Server={builder.Server};User Id={builder.UserID};Password={builder.Password};SslMode=none;";

                using (var conexionSinBDObj = new MySqlConnection(conexionSinBD))
                {
                    await conexionSinBDObj.OpenAsync();

                    // Verificar si la base de datos existe
                    string verificarBDQuery = $"SELECT COUNT(*) FROM information_schema.schemata WHERE schema_name = '{builder.Database}';";
                    using (var cmdVerificarBD = new MySqlCommand(verificarBDQuery, conexionSinBDObj))
                    {
                        var resultado = await cmdVerificarBD.ExecuteScalarAsync();
                        if (Convert.ToInt32(resultado) == 0)
                        {
                            // Si la base de datos no existe, la creamos
                            string crearBDQuery = $"CREATE DATABASE `{builder.Database}`;";
                            using (var cmdCrearBD = new MySqlCommand(crearBDQuery, conexionSinBDObj))
                            {
                                await cmdCrearBD.ExecuteNonQueryAsync();
                            }
                        }
                    }
                }

                // 2️ Leer el contenido del archivo .sql
                string contenidoSql;
                using (var reader = new StreamReader(archivoSql.OpenReadStream()))
                {
                    contenidoSql = await reader.ReadToEndAsync();
                }

                using (var conexion = new MySqlConnection(_connectionString))
                {
                    await conexion.OpenAsync();

                    // 3️ Eliminar todas las tablas de la base de datos antes de restaurar
                    string eliminarTablasQuery = @"
                        SET FOREIGN_KEY_CHECKS = 0;
                        SELECT table_name FROM information_schema.tables WHERE table_schema = DATABASE();";

                    try
                    {
                        using (var cmdEliminarTablas = new MySqlCommand(eliminarTablasQuery, conexion))
                        {
                            using (var reader = await cmdEliminarTablas.ExecuteReaderAsync())
                            {
                                var dropTableQueries = new List<string>();

                                while (await reader.ReadAsync())
                                {
                                    var tableName = reader.GetString(0);
                                    dropTableQueries.Add($"DROP TABLE IF EXISTS `{tableName}`;");
                                }

                                reader.Close();

                                foreach (var dropQuery in dropTableQueries)
                                {
                                    using (var cmdDropTable = new MySqlCommand(dropQuery, conexion))
                                    {
                                        await cmdDropTable.ExecuteNonQueryAsync();
                                    }
                                }
                            }
                        }
                    }
                    catch (Exception exEliminarTablas)
                    {
                        return StatusCode(500, new { mensaje = "Error al eliminar las tablas.", error = exEliminarTablas.ToString() });
                    }

                    // 4️ Ejecutar el contenido del archivo .sql para restaurar la base de datos
                    try
                    {
                        using (var cmdRestaurar = new MySqlCommand(contenidoSql, conexion))
                        {
                            cmdRestaurar.CommandType = System.Data.CommandType.Text;
                            await cmdRestaurar.ExecuteNonQueryAsync();
                        }
                    }
                    catch (Exception exRestaurar)
                    {
                        return StatusCode(500, new { mensaje = "Error al restaurar la base de datos.", error = exRestaurar.ToString() });
                    }
                }

                return Ok(new { mensaje = "Restauración completada con éxito." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensaje = "Error inesperado.", error = ex.ToString() });
            }
        }
    }
}

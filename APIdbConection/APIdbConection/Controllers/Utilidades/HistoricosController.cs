using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using APIdbConection.Models;
using System.Collections.Generic;
using System.Linq;
using APIdbConection.Models.Utilidades;

namespace APIdbConection.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HistoricosController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly HistoricoDbContext _contextHistorico;

        public HistoricosController(ApplicationDbContext context, HistoricoDbContext contextHistorico)
        {
            _context = context;
            _contextHistorico = contextHistorico;
        }


        // Métodos adicionales para el traspaso de catálogos

        // POST: api/Historicos/TraspasarUsuarios
        [HttpPost("TraspasarUsuarios")]
        public async Task<ActionResult<object>> TraspasarUsuarios(string usuario)
        {
            try
            {
                // 1. Obtener usuarios inactivos
                var usuariosParaTraspaso = await _context.Usuarios
                    .Where(u => u.Estatus == "Inactivo")
                    .ToListAsync();

                if (!usuariosParaTraspaso.Any())
                {
                    return Ok(new { Success = false, Message = "No se encontraron usuarios inactivos para traspasar." });
                }

                // 2. Obtener IDs ya existentes en históricos
                var idsExistentesHistorico = await _contextHistorico.UsuariosHistorico
                    .Select(h => h.IdUsuario)
                    .ToListAsync();

                var usuariosFiltrados = usuariosParaTraspaso
                    .Where(u => !idsExistentesHistorico.Contains(u.IdUsuario))
                    .ToList();

                // 3. Insertar en la tabla histórica
                await using (var transactionHistorico = await _contextHistorico.Database.BeginTransactionAsync())
                {
                    try
                    {
                        foreach (var usr in usuariosFiltrados)
                        {
                            var usuarioHistorico = new UsuariosHistorico
                            {
                                IdUsuario = usr.IdUsuario,
                                Usuario = usr.Usuario,
                                Contrasena = usr.Contrasena,
                                Nombre = usr.Nombre,
                                Telefono = usr.Telefono,
                                Email = usr.Email,
                                Rol = usr.Rol,
                                Estatus = usr.Estatus,
                                FechaTraspaso = DateTime.Now
                            };

                            _contextHistorico.UsuariosHistorico.Add(usuarioHistorico);
                        }

                        await _contextHistorico.SaveChangesAsync();
                        await transactionHistorico.CommitAsync();
                    }
                    catch (Exception ex)
                    {
                        await transactionHistorico.RollbackAsync();

                        var errorLog = new ErrorLog
                        {
                            UserName = usuario,
                            ErrorMessage = ex.Message,
                            ErrorProcedure = "TraspasarEmpleados",
                            ErrorLine = ex.StackTrace?.Split('\n')[0],
                            ErrorTime = DateTime.Now
                        };
                        _context.ErrorLogs.Add(errorLog);
                        await _context.SaveChangesAsync();

                        return StatusCode(500, new { Success = false, Message = $"Error al guardar en históricos: {ex.Message}" });
                    }
                }

                // 4. Eliminar usuarios originales
                await using (var transaction = await _context.Database.BeginTransactionAsync())
                {
                    try
                    {
                        _context.Usuarios.RemoveRange(usuariosFiltrados);
                        await _context.SaveChangesAsync();
                        await transaction.CommitAsync();
                    }
                    catch (Exception ex)
                    {
                        await transaction.RollbackAsync();

                        var errorLog = new ErrorLog
                        {
                            UserName = usuario,
                            ErrorMessage = ex.Message,
                            ErrorProcedure = "TraspasarEmpleados",
                            ErrorLine = ex.StackTrace?.Split('\n')[0],
                            ErrorTime = DateTime.Now
                        };
                        _context.ErrorLogs.Add(errorLog);
                        await _context.SaveChangesAsync();

                        return StatusCode(500, new { Success = false, Message = $"Error al eliminar registros originales: {ex.Message}" });
                    }
                }

                return Ok(new
                {
                    Success = true,
                    Message = $"Se traspasaron {usuariosFiltrados.Count} usuarios a históricos.",
                    TraspasoCount = usuariosFiltrados.Count
                });
            }
            catch (Exception ex)
            {
                var errorLog = new ErrorLog
                {
                    UserName = usuario,
                    ErrorMessage = ex.Message,
                    ErrorProcedure = "TraspasarEmpleados",
                    ErrorLine = ex.StackTrace?.Split('\n')[0],
                    ErrorTime = DateTime.Now
                };
                _context.ErrorLogs.Add(errorLog);
                await _context.SaveChangesAsync();

                return StatusCode(500, new { Success = false, Message = $"Error general: {ex.Message}" });
            }
        }

        // POST: api/Historicos/TraspasarEmpleados
        [HttpPost("TraspasarEmpleados")]
        public async Task<ActionResult<object>> TraspasarEmpleados(string usuario)
        {
            try
            {
                // 1. Obtener empleados inactivos
                var empleadosParaTraspaso = await _context.Empleados
                    .Where(e => e.Estatus == "Inactivo")
                    .ToListAsync();

                if (!empleadosParaTraspaso.Any())
                {
                    return Ok(new { Success = false, Message = "No se encontraron empleados inactivos para traspasar." });
                }

                // 2. Obtener IDs ya existentes en históricos
                var idsExistentesHistorico = await _contextHistorico.EmpleadosHistorico
                    .Select(h => h.IdEmpleado)
                    .ToListAsync();

                var empleadosFiltrados = empleadosParaTraspaso
                    .Where(e => !idsExistentesHistorico.Contains(e.IdEmpleado))
                    .ToList();

                // 3. Insertar en la tabla histórica
                await using (var transactionHistorico = await _contextHistorico.Database.BeginTransactionAsync())
                {
                    try
                    {
                        foreach (var emp in empleadosFiltrados)
                        {
                            var empleadoHistorico = new EmpleadosHistorico
                            {
                                IdEmpleado = emp.IdEmpleado,
                                Nombre = emp.Nombre,
                                Departamento = emp.Departamento,
                                Puesto = emp.Puesto,
                                Direccion = emp.Direccion,
                                Colonia = emp.Colonia,
                                CP = emp.Cp,
                                Municipio = emp.Municipio,
                                Estado = emp.Estado,
                                Telefono = emp.Telefono,
                                Estatus = emp.Estatus,
                                FechaTraspaso = DateTime.Now
                            };

                            _contextHistorico.EmpleadosHistorico.Add(empleadoHistorico);
                        }

                        await _contextHistorico.SaveChangesAsync();
                        await transactionHistorico.CommitAsync();
                    }
                    catch (Exception ex)
                    {
                        await transactionHistorico.RollbackAsync();

                        var errorLog = new ErrorLog
                        {
                            UserName = usuario,
                            ErrorMessage = ex.Message,
                            ErrorProcedure = "TraspasarEmpleados",
                            ErrorLine = ex.StackTrace?.Split('\n')[0],
                            ErrorTime = DateTime.Now
                        };
                        _context.ErrorLogs.Add(errorLog);
                        await _context.SaveChangesAsync();

                        return StatusCode(500, new { Success = false, Message = $"Error al guardar en históricos: {ex.Message}" });
                    }
                }

                // 4. Eliminar de la tabla original
                await using (var transaction = await _context.Database.BeginTransactionAsync())
                {
                    try
                    {
                        _context.Empleados.RemoveRange(empleadosFiltrados);
                        await _context.SaveChangesAsync();
                        await transaction.CommitAsync();
                    }
                    catch (Exception ex)
                    {
                        await transaction.RollbackAsync();

                        var errorLog = new ErrorLog
                        {
                            UserName = usuario,
                            ErrorMessage = ex.Message,
                            ErrorProcedure = "TraspasarEmpleados",
                            ErrorLine = ex.StackTrace?.Split('\n')[0],
                            ErrorTime = DateTime.Now
                        };
                        _context.ErrorLogs.Add(errorLog);
                        await _context.SaveChangesAsync();

                        return StatusCode(500, new { Success = false, Message = $"Error al eliminar registros originales: {ex.Message}" });
                    }
                }

                return Ok(new
                {
                    Success = true,
                    Message = $"Se traspasaron {empleadosFiltrados.Count} empleados a históricos.",
                    TraspasoCount = empleadosFiltrados.Count
                });
            }
            catch (Exception ex)
            {
                var errorLog = new ErrorLog
                {
                    UserName = usuario,
                    ErrorMessage = ex.Message,
                    ErrorProcedure = "TraspasarEmpleados",
                    ErrorLine = ex.StackTrace?.Split('\n')[0],
                    ErrorTime = DateTime.Now
                };
                _context.ErrorLogs.Add(errorLog);
                await _context.SaveChangesAsync();

                return StatusCode(500, new { Success = false, Message = $"Error general: {ex.Message}" });
            }
        }


        // POST: api/Historicos/TraspasarCultivos
        [HttpPost("TraspasarCultivos")]
        public async Task<ActionResult<object>> TraspasarCultivos(string usuario)
        {
            try
            {
                // 1. Obtener cultivos que ya no están en uso (sin plantaciones activas)
                var cultivosParaTraspaso = await _context.Cultivos.ToListAsync();

                if (!cultivosParaTraspaso.Any())
                {
                    return Ok(new { Success = false, Message = "No se encontraron cultivos sin uso para traspasar." });
                }

                // 2. Insertar en la tabla histórica (evitando duplicados)
                await using (var transactionHistorico = await _contextHistorico.Database.BeginTransactionAsync())
                {
                    try
                    {
                        // Obtener IDs ya existentes en la tabla histórica
                        var idsExistentes = await _contextHistorico.CultivosHistorico
                            .Select(h => h.IdCultivo)
                            .ToListAsync();

                        // Filtrar cultivos que no estén ya en la tabla histórica
                        var nuevosCultivos = cultivosParaTraspaso
                            .Where(c => !idsExistentes.Contains(c.IdCultivo))
                            .ToList();

                        if (!nuevosCultivos.Any())
                        {
                            return Ok(new { Success = false, Message = "Todos los cultivos ya existen en la tabla histórica." });
                        }

                        foreach (var cultivo in nuevosCultivos)
                        {
                            var cultivoHistorico = new CultivosHistorico
                            {
                                IdCultivo = cultivo.IdCultivo,
                                TipoBerry = cultivo.TipoBerry,
                                Variedad = cultivo.Variedad,
                                FechaTraspaso = DateTime.Now

                            };

                            _contextHistorico.CultivosHistorico.Add(cultivoHistorico);
                        }

                        await _contextHistorico.SaveChangesAsync();
                        await transactionHistorico.CommitAsync();

                        return Ok(new
                        {
                            Success = true,
                            Message = $"Se traspasaron {nuevosCultivos.Count} cultivos a históricos.",
                            TraspasoCount = nuevosCultivos.Count
                        });
                    }
                    catch (Exception ex)
                    {
                        await transactionHistorico.RollbackAsync();

                        var errorLog = new ErrorLog
                        {
                            UserName = usuario,
                            ErrorMessage = ex.Message,
                            ErrorProcedure = "TraspasarCultivos",
                            ErrorLine = ex.StackTrace?.Split('\n')[0],
                            ErrorTime = DateTime.Now
                        };
                        _context.ErrorLogs.Add(errorLog);
                        await _context.SaveChangesAsync();

                        return StatusCode(500, new { Success = false, Message = $"Error al guardar en históricos: {ex.Message}" });
                    }
                }
            }
            catch (Exception ex)
            {
                var errorLog = new ErrorLog
                {
                    UserName = usuario,
                    ErrorMessage = ex.Message,
                    ErrorProcedure = "TraspasarCultivos",
                    ErrorLine = ex.StackTrace?.Split('\n')[0],
                    ErrorTime = DateTime.Now
                };
                _context.ErrorLogs.Add(errorLog);
                await _context.SaveChangesAsync();

                return StatusCode(500, new { Success = false, Message = $"Error general: {ex.Message}" });
            }
        }


        // POST: api/Historicos/TraspasarViveros
        [HttpPost("TraspasarViveros")]
        public async Task<ActionResult<object>> TraspasarViveros(string usuario)
        {
            try
            {
                // 1. Obtener viveros inactivos (sin uso en plantaciones)
                var viverosParaTraspaso = await _context.Viveros.ToListAsync();

                if (!viverosParaTraspaso.Any())
                {
                    return Ok(new { Success = false, Message = "No se encontraron viveros sin uso para traspasar." });
                }

                // 2. Insertar en la tabla histórica (evitando duplicados)
                await using (var transactionHistorico = await _contextHistorico.Database.BeginTransactionAsync())
                {
                    try
                    {
                        // Obtener IDs ya existentes en la tabla histórica
                        var idsExistentes = await _contextHistorico.ViverosHistorico
                            .Select(h => h.IdVivero)
                            .ToListAsync();

                        // Filtrar viveros que no estén ya en la tabla histórica
                        var nuevosViveros = viverosParaTraspaso
                            .Where(v => !idsExistentes.Contains(v.IdVivero))
                            .ToList();

                        if (!nuevosViveros.Any())
                        {
                            return Ok(new { Success = false, Message = "Todos los viveros ya existen en la tabla histórica." });
                        }

                        foreach (var vivero in nuevosViveros)
                        {
                            var viveroHistorico = new ViverosHistorico
                            {
                                IdVivero = vivero.IdVivero,
                                NombreVivero = vivero.NombreVivero,
                                CodigoVivero = vivero.CodigoVivero,
                                FechaTraspaso = DateTime.Now

                            };

                            _contextHistorico.ViverosHistorico.Add(viveroHistorico);
                        }

                        await _contextHistorico.SaveChangesAsync();
                        await transactionHistorico.CommitAsync();

                        return Ok(new
                        {
                            Success = true,
                            Message = $"Se traspasaron {nuevosViveros.Count} viveros a históricos.",
                            TraspasoCount = nuevosViveros.Count
                        });
                    }
                    catch (Exception ex)
                    {
                        await transactionHistorico.RollbackAsync();

                        var errorLog = new ErrorLog
                        {
                            UserName = usuario,
                            ErrorMessage = ex.Message,
                            ErrorProcedure = "TraspasarViveros",
                            ErrorLine = ex.StackTrace?.Split('\n')[0],
                            ErrorTime = DateTime.Now
                        };
                        _context.ErrorLogs.Add(errorLog);
                        await _context.SaveChangesAsync();

                        return StatusCode(500, new { Success = false, Message = $"Error al guardar en históricos: {ex.Message}" });
                    }
                }
            }
            catch (Exception ex)
            {
                var errorLog = new ErrorLog
                {
                    UserName = usuario,
                    ErrorMessage = ex.Message,
                    ErrorProcedure = "TraspasarViveros",
                    ErrorLine = ex.StackTrace?.Split('\n')[0],
                    ErrorTime = DateTime.Now
                };
                _context.ErrorLogs.Add(errorLog);
                await _context.SaveChangesAsync();

                return StatusCode(500, new { Success = false, Message = $"Error general: {ex.Message}" });
            }
        }


        // POST: api/Historicos/TraspasarRanchos
        [HttpPost("TraspasarRanchos")]
        public async Task<ActionResult<object>> TraspasarRanchos(string usuario)
        {
            try
            {
                // 1. Obtener ranchos que no tengan llaves (plantaciones) activas
                var ranchosParaTraspaso = await _context.Ranchos.ToListAsync();

                if (!ranchosParaTraspaso.Any())
                {
                    return Ok(new { Success = false, Message = "No se encontraron ranchos sin uso para traspasar." });
                }

                // 2. Insertar en la tabla histórica (evitando duplicados)
                await using (var transactionHistorico = await _contextHistorico.Database.BeginTransactionAsync())
                {
                    try
                    {
                        // Obtener IDs ya existentes en la tabla histórica
                        var idsExistentes = await _contextHistorico.RanchosHistorico
                            .Select(h => h.IdRancho)
                            .ToListAsync();

                        // Filtrar ranchos que no estén ya en la tabla histórica
                        var nuevosRanchos = ranchosParaTraspaso
                            .Where(r => !idsExistentes.Contains(r.IdRancho))
                            .ToList();

                        if (!nuevosRanchos.Any())
                        {
                            return Ok(new { Success = false, Message = "Todos los ranchos ya existen en la tabla histórica." });
                        }

                        foreach (var rancho in nuevosRanchos)
                        {
                            var ranchoHistorico = new RanchosHistorico
                            {
                                IdRancho = rancho.IdRancho,
                                NombreRancho = rancho.NombreRancho,
                                NumeroRancho = rancho.NumeroRancho,
                                SuperficieHA = rancho.SuperficieHa,
                                SuperficieAcres = rancho.SuperficieAcres,
                                Direccion = rancho.Direccion,
                                CP = rancho.Cp,
                                Municipio = rancho.Municipio,
                                Estado = rancho.Estado,
                                FechaTraspaso = DateTime.Now

                            };

                            _contextHistorico.RanchosHistorico.Add(ranchoHistorico);
                        }

                        await _contextHistorico.SaveChangesAsync();
                        await transactionHistorico.CommitAsync();

                        return Ok(new
                        {
                            Success = true,
                            Message = $"Se traspasaron {nuevosRanchos.Count} ranchos a históricos.",
                            TraspasoCount = nuevosRanchos.Count
                        });
                    }
                    catch (Exception ex)
                    {
                        await transactionHistorico.RollbackAsync();

                        var errorLog = new ErrorLog
                        {
                            UserName = usuario,
                            ErrorMessage = ex.Message,
                            ErrorProcedure = "TraspasarRanchos",
                            ErrorLine = ex.StackTrace?.Split('\n')[0],
                            ErrorTime = DateTime.Now
                        };
                        _context.ErrorLogs.Add(errorLog);
                        await _context.SaveChangesAsync();

                        return StatusCode(500, new { Success = false, Message = $"Error al guardar en históricos: {ex.Message}" });
                    }
                }
            }
            catch (Exception ex)
            {
                var errorLog = new ErrorLog
                {
                    UserName = usuario,
                    ErrorMessage = ex.Message,
                    ErrorProcedure = "TraspasarRanchos",
                    ErrorLine = ex.StackTrace?.Split('\n')[0],
                    ErrorTime = DateTime.Now
                };
                _context.ErrorLogs.Add(errorLog);
                await _context.SaveChangesAsync();

                return StatusCode(500, new { Success = false, Message = $"Error general: {ex.Message}" });
            }
        }




        // POST: api/Historicos/TraspasarVehiculos
        [HttpPost("TraspasarVehiculos")]
        public async Task<ActionResult<object>> TraspasarVehiculos(string usuario)
        {
            try
            {
                var vehiculosParaTraspaso = await _context.Vehiculos.ToListAsync();

                if (!vehiculosParaTraspaso.Any())
                {
                    return Ok(new { Success = false, Message = $"No se encontraron vehículos para traspasar." });
                }

                // 2. Insertar en la tabla histórica (evitando duplicados)
                await using (var transactionHistorico = await _contextHistorico.Database.BeginTransactionAsync())
                {
                    try
                    {
                        // Obtener IDs de vehículos ya existentes en la tabla histórica
                        var idsExistentes = await _contextHistorico.VehiculosHistorico
                            .Select(h => h.IdVehiculo)
                            .ToListAsync();

                        // Filtrar vehículos que no estén ya en la tabla histórica
                        var nuevosVehiculos = vehiculosParaTraspaso
                            .Where(v => !idsExistentes.Contains(v.IdVehiculo))
                            .ToList();

                        if (!nuevosVehiculos.Any())
                        {
                            return Ok(new { Success = false, Message = "Todos los vehículos ya existen en la tabla histórica." });
                        }

                        foreach (var vehiculo in nuevosVehiculos)
                        {
                            var vehiculoHistorico = new VehiculosHistorico
                            {
                                IdVehiculo = vehiculo.IdVehiculo,
                                Placas = vehiculo.Placas,
                                Modelo = vehiculo.Modelo,
                                Marca = vehiculo.Marca,
                                FechaTraspaso = DateTime.Now

                            };

                            _contextHistorico.VehiculosHistorico.Add(vehiculoHistorico);
                        }

                        await _contextHistorico.SaveChangesAsync();
                        await transactionHistorico.CommitAsync();

                        return Ok(new
                        {
                            Success = true,
                            Message = $"Se traspasaron {nuevosVehiculos.Count} vehículos a históricos.",
                            TraspasoCount = nuevosVehiculos.Count
                        });
                    }
                    catch (Exception ex)
                    {
                        await transactionHistorico.RollbackAsync();

                        var errorLog = new ErrorLog
                        {
                            UserName = usuario,
                            ErrorMessage = ex.Message,
                            ErrorProcedure = "TraspasarVehiculos",
                            ErrorLine = ex.StackTrace?.Split('\n')[0],
                            ErrorTime = DateTime.Now
                        };
                        _context.ErrorLogs.Add(errorLog);
                        await _context.SaveChangesAsync();

                        return StatusCode(500, new { Success = false, Message = $"Error al guardar en históricos: {ex.Message}" });
                    }
                }
            }
            catch (Exception ex)
            {
                var errorLog = new ErrorLog
                {
                    UserName = usuario,
                    ErrorMessage = ex.Message,
                    ErrorProcedure = "TraspasarVehiculos",
                    ErrorLine = ex.StackTrace?.Split('\n')[0],
                    ErrorTime = DateTime.Now
                };
                _context.ErrorLogs.Add(errorLog);
                await _context.SaveChangesAsync();

                return StatusCode(500, new { Success = false, Message = $"Error general: {ex.Message}" });
            }
        }




        // POST: api/Historicos/TraspasarLlaves
        [HttpPost("TraspasarLlaves")]
        public async Task<ActionResult<object>> TraspasarLlaves(string usuario)
        {
            try
            {


                var llavesParaTraspaso = await _context.Llaves.ToListAsync();

                if (!llavesParaTraspaso.Any())
                {
                    return Ok(new { Success = false, Message = "No se encontraron llaves sin uso para traspasar." });
                }

                // 2. Insertar en la tabla histórica (evitando duplicados)
                await using (var transactionHistorico = await _contextHistorico.Database.BeginTransactionAsync())
                {
                    try
                    {
                        // Obtener IDs de llaves ya existentes en la tabla histórica
                        var idsExistentes = await _contextHistorico.LlavesHistorico
                            .Select(h => h.IdLlave)
                            .ToListAsync();

                        // Filtrar llaves que no estén ya en la tabla histórica
                        var nuevasLlaves = llavesParaTraspaso
                            .Where(l => !idsExistentes.Contains(l.IdLlave))
                            .ToList();

                        if (!nuevasLlaves.Any())
                        {
                            return Ok(new { Success = false, Message = "Todas las llaves ya existen en la tabla histórica." });
                        }

                        foreach (var llave in nuevasLlaves)
                        {
                            var llaveHistorica = new LlavesHistorico
                            {
                                IdLlave = llave.IdLlave,
                                IdRancho = llave.IdRancho,
                                NombreLlave = llave.NombreLlave,
                                SuperficieHA = llave.SuperficieHa,
                                CantidadTuneles = llave.CantidadTuneles,
                                Disponibilidad = llave.Disponibilidad,
                                SuperficieAcres = llave.SuperficieAcres,
                                FechaTraspaso = DateTime.Now
                            };

                            _contextHistorico.LlavesHistorico.Add(llaveHistorica);
                        }

                        await _contextHistorico.SaveChangesAsync();
                        await transactionHistorico.CommitAsync();

                        return Ok(new
                        {
                            Success = true,
                            Message = $"Se traspasaron {nuevasLlaves.Count} llaves a históricos.",
                            TraspasoCount = nuevasLlaves.Count
                        });
                    }
                    catch (Exception ex)
                    {
                        await transactionHistorico.RollbackAsync();

                        var errorLog = new ErrorLog
                        {
                            UserName = usuario,
                            ErrorMessage = ex.Message,
                            ErrorProcedure = "TraspasarLlaves",
                            ErrorLine = ex.StackTrace?.Split('\n')[0],
                            ErrorTime = DateTime.Now
                        };
                        _context.ErrorLogs.Add(errorLog);
                        await _context.SaveChangesAsync();

                        return StatusCode(500, new { Success = false, Message = $"Error al guardar en históricos: {ex.Message}" });
                    }
                }
            }
            catch (Exception ex)
            {
                var errorLog = new ErrorLog
                {
                    UserName = usuario,
                    ErrorMessage = ex.Message,
                    ErrorProcedure = "TraspasarLlaves",
                    ErrorLine = ex.StackTrace?.Split('\n')[0],
                    ErrorTime = DateTime.Now
                };
                _context.ErrorLogs.Add(errorLog);
                await _context.SaveChangesAsync();

                return StatusCode(500, new { Success = false, Message = $"Error general: {ex.Message}" });
            }
        }




        // POST: api/Historicos/TraspasarPlantaciones
        [HttpPost("TraspasarPlantaciones")]
        public async Task<ActionResult<object>> TraspasarPlantaciones(int anio, string usuario)
        {
            try
            {
                // 1. Obtener plantaciones inactivas del año especificado
                var plantacionesParaTraspaso = await _context.Plantaciones
                    .Where(p => p.EstatusPlantacion != "Activa" && p.FechaPlantacion.Year == anio)
                    .ToListAsync();

                if (!plantacionesParaTraspaso.Any())
                {
                    return Ok(new { Success = false, Message = $"No se encontraron plantaciones inactivas del año {anio} para traspasar." });
                }

                // 2. Obtener IDs ya existentes en históricos
                var idsExistentesHistorico = await _contextHistorico.PlantacionesHistorico
                    .Select(h => h.IdPlantacion)
                    .ToListAsync();

                var plantacionesFiltradas = plantacionesParaTraspaso
                    .Where(p => !idsExistentesHistorico.Contains(p.IdPlantacion))
                    .ToList();

                // Iniciar transacción para la tabla histórica
                await using (var transactionHistorico = await _contextHistorico.Database.BeginTransactionAsync())
                {
                    try
                    {
                        // 3. Insertar en la tabla histórica
                        foreach (var plantacion in plantacionesFiltradas)
                        {
                            var plantacionHistorica = new PlantacionesHistorico
                            {
                                IdPlantacion = plantacion.IdPlantacion,
                                IdCultivo = plantacion.IdCultivo,
                                IdVivero = plantacion.IdVivero,
                                IdLlave = plantacion.IdLlave,
                                CantidadPlantas = plantacion.CantidadPlantas,
                                PlantasPorMetro = plantacion.PlantasPorMetro,
                                FechaPlantacion = plantacion.FechaPlantacion,
                                NumSemPlantacion = plantacion.NumSemPlantacion,
                                EstatusPlantacion = plantacion.EstatusPlantacion,
                                FechaTraspaso = DateTime.Now
                            };

                            _contextHistorico.PlantacionesHistorico.Add(plantacionHistorica);
                        }

                        await _contextHistorico.SaveChangesAsync();
                        await transactionHistorico.CommitAsync();
                    }
                    catch (Exception ex)
                    {
                        await transactionHistorico.RollbackAsync();

                        var errorLog = new ErrorLog
                        {
                            UserName = usuario,
                            ErrorMessage = ex.Message,
                            ErrorProcedure = "TraspasarPlantaciones",
                            ErrorLine = ex.StackTrace?.Split('\n')[0],
                            ErrorTime = DateTime.Now
                        };
                        _context.ErrorLogs.Add(errorLog);
                        await _context.SaveChangesAsync();

                        return StatusCode(500, new { Success = false, Message = $"Error al guardar en históricos: {ex.Message}" });
                    }
                }

                // Iniciar transacción para la tabla original
                await using (var transaction = await _context.Database.BeginTransactionAsync())
                {
                    try
                    {
                        // 4. Eliminar registros originales
                        _context.Plantaciones.RemoveRange(plantacionesFiltradas);
                        await _context.SaveChangesAsync();
                        await transaction.CommitAsync();
                    }
                    catch (Exception ex)
                    {
                        await transaction.RollbackAsync();

                        var errorLog = new ErrorLog
                        {
                            UserName = usuario,
                            ErrorMessage = ex.Message,
                            ErrorProcedure = "TraspasarPlantaciones",
                            ErrorLine = ex.StackTrace?.Split('\n')[0],
                            ErrorTime = DateTime.Now
                        };
                        _context.ErrorLogs.Add(errorLog);
                        await _context.SaveChangesAsync();

                        return StatusCode(500, new { Success = false, Message = $"Error al eliminar registros originales: {ex.Message}" });
                    }
                }

                return Ok(new
                {
                    Success = true,
                    Message = $"Se traspasaron {plantacionesFiltradas.Count} plantaciones a históricos.",
                    TraspasoCount = plantacionesFiltradas.Count
                });
            }
            catch (Exception ex)
            {
                var errorLog = new ErrorLog
                {
                    UserName = usuario,
                    ErrorMessage = ex.Message,
                    ErrorProcedure = "TraspasarPlantaciones",
                    ErrorLine = ex.StackTrace?.Split('\n')[0],
                    ErrorTime = DateTime.Now
                };
                _context.ErrorLogs.Add(errorLog);
                await _context.SaveChangesAsync();

                return StatusCode(500, new { Success = false, Message = $"Error general: {ex.Message}" });
            }
        }




        // POST: api/Historicos/TraspasarReplantes
        [HttpPost("TraspasarReplantes")]
        public async Task<ActionResult<object>> TraspasarReplantes(int anio, string usuario)
        {
            try
            {
                // 1. Obtener replantes del año especificado
                var replantesParaTraspaso = await _context.Replantes
                    .Where(r => r.FechaReplante.Year == anio)
                    .ToListAsync();

                if (!replantesParaTraspaso.Any())
                {
                    return Ok(new { Success = false, Message = $"No se encontraron replantes del año {anio} para traspasar." });
                }

                // 2. Obtener IDs ya existentes en históricos
                var idsExistentesHistorico = await _contextHistorico.ReplantesHistorico
                    .Select(h => h.IdReplante)
                    .ToListAsync();

                var replantesFiltrados = replantesParaTraspaso
                    .Where(r => !idsExistentesHistorico.Contains(r.IdReplante))
                    .ToList();

                // Iniciar transacción para la tabla histórica
                await using (var transactionHistorico = await _contextHistorico.Database.BeginTransactionAsync())
                {
                    try
                    {
                        // 3. Insertar en la tabla histórica
                        foreach (var replante in replantesFiltrados)
                        {
                            var replanteHistorico = new ReplantesHistorico
                            {
                                IdReplante = replante.IdReplante,
                                IdPlantacion = replante.IdPlantacion,
                                IdCultivo = replante.IdCultivo,
                                IdVivero = replante.IdVivero,
                                CantidadReplante = replante.CantidadReplante,
                                FechaReplante = replante.FechaReplante,
                                NumSemReplante = replante.NumSemReplante,
                                FechaTraspaso = DateTime.Now
                            };

                            _contextHistorico.ReplantesHistorico.Add(replanteHistorico);
                        }

                        await _contextHistorico.SaveChangesAsync();
                        await transactionHistorico.CommitAsync();
                    }
                    catch (Exception ex)
                    {
                        await transactionHistorico.RollbackAsync();

                        var errorLog = new ErrorLog
                        {
                            UserName = usuario,
                            ErrorMessage = ex.Message,
                            ErrorProcedure = "TraspasarReplantes",
                            ErrorLine = ex.StackTrace?.Split('\n')[0],
                            ErrorTime = DateTime.Now
                        };
                        _context.ErrorLogs.Add(errorLog);
                        await _context.SaveChangesAsync();

                        return StatusCode(500, new { Success = false, Message = $"Error al guardar en históricos: {ex.Message}" });
                    }
                }

                // Iniciar transacción para la tabla original
                await using (var transaction = await _context.Database.BeginTransactionAsync())
                {
                    try
                    {
                        // 4. Eliminar los replantes originales
                        _context.Replantes.RemoveRange(replantesFiltrados);
                        await _context.SaveChangesAsync();
                        await transaction.CommitAsync();
                    }
                    catch (Exception ex)
                    {
                        await transaction.RollbackAsync();

                        var errorLog = new ErrorLog
                        {
                            UserName = usuario,
                            ErrorMessage = ex.Message,
                            ErrorProcedure = "TraspasarReplantes",
                            ErrorLine = ex.StackTrace?.Split('\n')[0],
                            ErrorTime = DateTime.Now
                        };
                        _context.ErrorLogs.Add(errorLog);
                        await _context.SaveChangesAsync();

                        return StatusCode(500, new { Success = false, Message = $"Error al eliminar registros originales: {ex.Message}" });
                    }
                }

                return Ok(new
                {
                    Success = true,
                    Message = $"Se traspasaron {replantesFiltrados.Count} replantes a históricos.",
                    TraspasoCount = replantesFiltrados.Count
                });
            }
            catch (Exception ex)
            {
                var errorLog = new ErrorLog
                {
                    UserName = usuario,
                    ErrorMessage = ex.Message,
                    ErrorProcedure = "TraspasarReplantes",
                    ErrorLine = ex.StackTrace?.Split('\n')[0],
                    ErrorTime = DateTime.Now
                };
                _context.ErrorLogs.Add(errorLog);
                await _context.SaveChangesAsync();

                return StatusCode(500, new { Success = false, Message = $"Error general: {ex.Message}" });
            }
        }



        // POST: api/Historicos/TraspasarPodas
        [HttpPost("TraspasarPodas")]
        public async Task<ActionResult<object>> TraspasarPodas(int anio, string usuario)
        {
            try
            {
                // 1. Obtener podas del año especificado
                var podasParaTraspaso = await _context.Podas
                    .Where(p => p.FechaPoda.Year == anio)
                    .ToListAsync();

                if (!podasParaTraspaso.Any())
                {
                    return Ok(new { Success = false, Message = $"No se encontraron podas del año {anio} para traspasar." });
                }

                // 2. Obtener IDs ya existentes en históricos
                var idsExistentesHistorico = await _contextHistorico.PodasHistorico
                    .Select(h => h.IdPoda)
                    .ToListAsync();

                var podasFiltradas = podasParaTraspaso
                    .Where(p => !idsExistentesHistorico.Contains(p.IdPoda))
                    .ToList();

                // Iniciar transacción para la tabla histórica
                await using (var transactionHistorico = await _contextHistorico.Database.BeginTransactionAsync())
                {
                    try
                    {
                        // 3. Insertar en la tabla histórica
                        foreach (var poda in podasFiltradas)
                        {
                            var podaHistorica = new PodasHistorico
                            {
                                IdPoda = poda.IdPoda,
                                IdPlantacion = poda.IdPlantacion,
                                FechaPoda = poda.FechaPoda,
                                NumSemPoda = poda.NumSemPoda,
                                TipoPoda = poda.TipoPoda,
                                FechaTraspaso = DateTime.Now
                            };

                            _contextHistorico.PodasHistorico.Add(podaHistorica);
                        }

                        // Guardar cambios en la tabla histórica
                        await _contextHistorico.SaveChangesAsync();
                        await transactionHistorico.CommitAsync();
                    }
                    catch (Exception ex)
                    {
                        await transactionHistorico.RollbackAsync();

                        // Registrar el error
                        var errorLog = new ErrorLog
                        {
                            UserName = usuario,
                            ErrorMessage = ex.Message,
                            ErrorProcedure = "TraspasarPodas",
                            ErrorLine = ex.StackTrace?.Split('\n')[0],
                            ErrorTime = DateTime.Now
                        };
                        _context.ErrorLogs.Add(errorLog);
                        await _context.SaveChangesAsync();

                        return StatusCode(500, new { Success = false, Message = $"Error al guardar en históricos: {ex.Message}" });
                    }
                }

                // Iniciar transacción para la tabla original
                await using (var transaction = await _context.Database.BeginTransactionAsync())
                {
                    try
                    {
                        // 4. Eliminar las podas originales
                        _context.Podas.RemoveRange(podasFiltradas);
                        await _context.SaveChangesAsync();
                        await transaction.CommitAsync();
                    }
                    catch (Exception ex)
                    {
                        await transaction.RollbackAsync();

                        // Registrar el error
                        var errorLog = new ErrorLog
                        {
                            UserName = usuario,
                            ErrorMessage = ex.Message,
                            ErrorProcedure = "TraspasarPodas",
                            ErrorLine = ex.StackTrace?.Split('\n')[0],
                            ErrorTime = DateTime.Now
                        };
                        _context.ErrorLogs.Add(errorLog);
                        await _context.SaveChangesAsync();

                        return StatusCode(500, new { Success = false, Message = $"Error al eliminar registros originales: {ex.Message}" });
                    }
                }

                return Ok(new
                {
                    Success = true,
                    Message = $"Se traspasaron {podasFiltradas.Count} podas a históricos.",
                    TraspasoCount = podasFiltradas.Count
                });
            }
            catch (Exception ex)
            {
                // Registrar el error general
                var errorLog = new ErrorLog
                {
                    UserName = usuario,
                    ErrorMessage = ex.Message,
                    ErrorProcedure = "TraspasarPodas",
                    ErrorLine = ex.StackTrace?.Split('\n')[0],
                    ErrorTime = DateTime.Now
                };
                _context.ErrorLogs.Add(errorLog);
                await _context.SaveChangesAsync();

                return StatusCode(500, new { Success = false, Message = $"Error general: {ex.Message}" });
            }
        }


        // POST: api/Historicos/TraspasarPersonalCosecha
        [HttpPost("TraspasarPersonalCosecha")]
        public async Task<ActionResult<object>> TraspasarPersonalCosecha(int anio, string usuario)
        {
            try
            {
                // 1. Obtener registros de personal de cosecha del año especificado
                var cosechasDelAnio = await _context.Cosechas
                    .Where(c => c.FechaCosecha.Year == anio)
                    .Select(c => c.IdCosecha)
                    .ToListAsync();

                var todosPersonal = await _context.PersonalCosecha.ToListAsync();
                var personalParaTraspaso = todosPersonal
                    .Where(pc => cosechasDelAnio.Contains(pc.IdCosecha))
                    .ToList();

                if (!personalParaTraspaso.Any())
                {
                    return Ok(new { Success = false, Message = $"No se encontró personal de cosecha del año {anio} para traspasar." });
                }

                // 2. Obtener IDs ya existentes en la tabla histórica
                var idsExistentesHistorico = await _contextHistorico.PersonalCosechaHistorico
                    .Select(h => h.IdPersonalCosecha)
                    .ToListAsync();

                var personalFiltrado = personalParaTraspaso
                    .Where(pc => !idsExistentesHistorico.Contains(pc.IdPersonalCosecha))
                    .ToList();

                // Iniciar transacción para la tabla histórica
                await using (var transactionHistorico = await _contextHistorico.Database.BeginTransactionAsync())
                {
                    try
                    {
                        foreach (var personal in personalFiltrado)
                        {
                            var personalHistorico = new PersonalCosechaHistorico
                            {
                                IdPersonalCosecha = personal.IdPersonalCosecha,
                                IdCosecha = personal.IdCosecha,
                                IdEmpleado = personal.IdEmpleado,
                                Jarras = personal.Jarras,
                                PrecioJarra = personal.PrecioJarra,
                                FechaTraspaso = DateTime.Now
                            };

                            _contextHistorico.PersonalCosechaHistorico.Add(personalHistorico);
                        }

                        // Guardar cambios en la tabla histórica
                        await _contextHistorico.SaveChangesAsync();
                        await transactionHistorico.CommitAsync();
                    }
                    catch (Exception ex)
                    {
                        await transactionHistorico.RollbackAsync();

                        // Registrar el error
                        var errorLog = new ErrorLog
                        {
                            UserName = usuario,
                            ErrorMessage = ex.Message,
                            ErrorProcedure = "TraspasarPersonalCosecha",
                            ErrorLine = ex.StackTrace?.Split('\n')[0],
                            ErrorTime = DateTime.Now
                        };
                        _context.ErrorLogs.Add(errorLog);
                        await _context.SaveChangesAsync();

                        return StatusCode(500, new { Success = false, Message = $"Error al guardar en históricos: {ex.Message}" });
                    }
                }

                // Iniciar transacción para la tabla original
                await using (var transaction = await _context.Database.BeginTransactionAsync())
                {
                    try
                    {
                        // Eliminar los registros originales
                        _context.PersonalCosecha.RemoveRange(personalFiltrado);
                        await _context.SaveChangesAsync();
                        await transaction.CommitAsync();
                    }
                    catch (Exception ex)
                    {
                        await transaction.RollbackAsync();

                        // Registrar el error
                        var errorLog = new ErrorLog
                        {
                            UserName = usuario,
                            ErrorMessage = ex.Message,
                            ErrorProcedure = "TraspasarPersonalCosecha",
                            ErrorLine = ex.StackTrace?.Split('\n')[0],
                            ErrorTime = DateTime.Now
                        };
                        _context.ErrorLogs.Add(errorLog);
                        await _context.SaveChangesAsync();

                        return StatusCode(500, new { Success = false, Message = $"Error al eliminar registros originales: {ex.Message}" });
                    }
                }

                return Ok(new
                {
                    Success = true,
                    Message = $"Se traspasaron {personalFiltrado.Count} registros de personal de cosecha a históricos.",
                    TraspasoCount = personalFiltrado.Count
                });
            }
            catch (Exception ex)
            {
                // Registrar el error general
                var errorLog = new ErrorLog
                {
                    UserName = usuario,
                    ErrorMessage = ex.Message,
                    ErrorProcedure = "TraspasarPersonalCosecha",
                    ErrorLine = ex.StackTrace?.Split('\n')[0],
                    ErrorTime = DateTime.Now
                };
                _context.ErrorLogs.Add(errorLog);
                await _context.SaveChangesAsync();

                return StatusCode(500, new { Success = false, Message = $"Error general: {ex.Message}" });
            }
        }


        // POST: api/Historicos/TraspasarProceso
        [HttpPost("TraspasarProceso")]
        public async Task<ActionResult<object>> TraspasarProceso(int anio, string usuario)
        {
            try
            {

                // 1. Obtener viajes del año especificado
                var viajesDelAnio = await _context.Viajes
                    .Where(v => v.FechaSalida.Year == anio && v.EstadoAprobacion.Trim().ToLower() != "pendiente")
                    .ToListAsync();

                if (!viajesDelAnio.Any())
                {
                    return Ok(new { Success = false, Message = $"No se encontraron viajes del año {anio} para traspasar." });
                }

                // 2. Obtener procesos relacionados con esos viajes
                var viajesIds = viajesDelAnio.Select(v => v.IdViaje).ToList();

                // 3. Obtener los procesos para esos viajes
                var procesosParaTraspaso = (await _context.Procesos.ToListAsync())
                    .Where(p => viajesIds.Contains(p.IdViaje))
                    .ToList();

                if (!procesosParaTraspaso.Any())
                {
                    return Ok(new { Success = false, Message = $"No se encontraron procesos del año {anio} para traspasar." });
                }

                // Hacemos cada operación en su propia transacción para evitar bloqueos entre DBs
                // 2. Insertar en la tabla histórica primero
                await using (var transactionHistorico = await _contextHistorico.Database.BeginTransactionAsync())
                {
                    try
                    {
                        foreach (var proceso in procesosParaTraspaso)
                        {
                            var procesoHistorico = new ProcesoHistorico
                            {
                                IdProceso = proceso.IdProceso,
                                IdViaje = proceso.IdViaje,
                                ClaseAkg = proceso.ClaseAkg,
                                ClaseBkg = proceso.ClaseBkg,
                                ClaseCkg = proceso.ClaseCkg,
                                Rechazo = proceso.Rechazo,
                                FechaTraspaso = DateTime.Now
                            };

                            _contextHistorico.ProcesoHistorico.Add(procesoHistorico);
                        }

                        // Guardar cambios en la tabla histórica
                        await _contextHistorico.SaveChangesAsync();
                        await transactionHistorico.CommitAsync();
                    }
                    catch (Exception ex)
                    {
                        await transactionHistorico.RollbackAsync();

                        // Registrar el error
                        var errorLog = new ErrorLog
                        {
                            UserName = usuario,
                            ErrorMessage = ex.Message,
                            ErrorProcedure = "TraspasarProceso",
                            ErrorLine = ex.StackTrace?.Split('\n')[0],
                            ErrorTime = DateTime.Now
                        };
                        _context.ErrorLogs.Add(errorLog);
                        await _context.SaveChangesAsync();

                        return StatusCode(500, new { Success = false, Message = $"Error al guardar en históricos: {ex.Message}" });
                    }
                }

                // 3. Ahora eliminamos de la tabla original
                await using (var transaction = await _context.Database.BeginTransactionAsync())
                {
                    try
                    {
                        _context.Procesos.RemoveRange(procesosParaTraspaso);
                        await _context.SaveChangesAsync();
                        await transaction.CommitAsync();
                    }
                    catch (Exception ex)
                    {
                        await transaction.RollbackAsync();

                        // Registrar el error
                        var errorLog = new ErrorLog
                        {
                            UserName = usuario,
                            ErrorMessage = ex.Message,
                            ErrorProcedure = "TraspasarProceso",
                            ErrorLine = ex.StackTrace?.Split('\n')[0],
                            ErrorTime = DateTime.Now
                        };
                        _context.ErrorLogs.Add(errorLog);
                        await _context.SaveChangesAsync();

                        return StatusCode(500, new { Success = false, Message = $"Error al eliminar registros originales: {ex.Message}" });
                    }
                }

                return Ok(new
                {
                    Success = true,
                    Message = $"Se traspasaron {procesosParaTraspaso.Count} procesos a históricos.",
                    TraspasoCount = procesosParaTraspaso.Count
                });
            }
            catch (Exception ex)
            {
                // Registrar el error
                var errorLog = new ErrorLog
                {
                    UserName = usuario,
                    ErrorMessage = ex.Message,
                    ErrorProcedure = "TraspasarProceso",
                    ErrorLine = ex.StackTrace?.Split('\n')[0],
                    ErrorTime = DateTime.Now
                };
                _context.ErrorLogs.Add(errorLog);
                await _context.SaveChangesAsync();

                return StatusCode(500, new { Success = false, Message = $"Error general: {ex.Message}" });
            }
        }


        // POST: api/Historicos/TraspasarCosechasProduccion
        [HttpPost("TraspasarCosechasProduccion")]
        public async Task<ActionResult<object>> TraspasarCosechasProduccion(int anio, string usuario)
        {
            try
            {
                // 1. Obtener cosechas del año especificado
                var cosechasParaTraspaso = await _context.Cosechas
                    .Where(c => c.FechaCosecha.Year == anio)
                    .ToListAsync();

                if (!cosechasParaTraspaso.Any())
                {
                    return Ok(new { Success = false, Message = $"No se encontraron cosechas del año {anio} para traspasar." });
                }

                // 2. Obtener producciones relacionadas con esas cosechas
                var cosechasIds = cosechasParaTraspaso.Select(c => c.IdCosecha).ToList();

                var todasProducciones = await _context.Producciones.ToListAsync();
                var produccionesParaTraspaso = todasProducciones
                    .Where(p => cosechasIds.Contains(p.IdCosecha))
                    .ToList();

                int produccionesTraspasoCount = 0;
                int cosechasTraspasoCount = 0;

                // 3. Filtrar producciones para evitar duplicados en la tabla histórica
                var idsProduccionHistoricaExistente = await _contextHistorico.ProduccionHistorico
                    .Select(h => h.IdProduccion)
                    .ToListAsync();

                var produccionesFiltradas = produccionesParaTraspaso
                    .Where(p => !idsProduccionHistoricaExistente.Contains(p.IdProduccion))
                    .ToList();

                // 4. Traspasar producciones
                if (produccionesFiltradas.Any())
                {
                    await using var transHistProduccion = await _contextHistorico.Database.BeginTransactionAsync();
                    try
                    {
                        foreach (var produccion in produccionesFiltradas)
                        {
                            var historica = new ProduccionHistorico
                            {
                                IdProduccion = produccion.IdProduccion,
                                IdCosecha = produccion.IdCosecha,
                                TipoCaja = produccion.TipoCaja,
                                CantidadCajas = produccion.CantidadCajas,
                                KilosProceso = produccion.KilosProceso,
                                FechaTraspaso = DateTime.Now
                            };
                            _contextHistorico.ProduccionHistorico.Add(historica);
                        }

                        await _contextHistorico.SaveChangesAsync();
                        await transHistProduccion.CommitAsync();
                        produccionesTraspasoCount = produccionesFiltradas.Count;
                    }
                    catch (Exception ex)
                    {
                        await transHistProduccion.RollbackAsync();
                        // Registrar el error
                        var errorLog = new ErrorLog
                        {
                            UserName = usuario,
                            ErrorMessage = ex.Message,
                            ErrorProcedure = "TraspasarCosechasProduccion",
                            ErrorLine = ex.StackTrace?.Split('\n')[0],
                            ErrorTime = DateTime.Now
                        };
                        _context.ErrorLogs.Add(errorLog);
                        await _context.SaveChangesAsync();
                        return StatusCode(500, new { Success = false, Message = $"Error al traspasar producciones a históricos: {ex.Message}" });
                    }

                    // 5. Eliminar producciones originales
                    await using var transEliminarProd = await _context.Database.BeginTransactionAsync();
                    try
                    {
                        _context.Producciones.RemoveRange(produccionesFiltradas);
                        await _context.SaveChangesAsync();
                        await transEliminarProd.CommitAsync();
                    }
                    catch (Exception ex)
                    {
                        await transEliminarProd.RollbackAsync();
                        // Registrar el error
                        var errorLog = new ErrorLog
                        {
                            UserName = usuario,
                            ErrorMessage = ex.Message,
                            ErrorProcedure = "TraspasarCosechasProduccion",
                            ErrorLine = ex.StackTrace?.Split('\n')[0],
                            ErrorTime = DateTime.Now
                        };
                        _context.ErrorLogs.Add(errorLog);
                        await _context.SaveChangesAsync();
                        return StatusCode(500, new { Success = false, Message = $"Error al eliminar producciones: {ex.Message}" });
                    }
                }

                // 6. Filtrar cosechas para evitar duplicados en la tabla histórica
                var idsCosechaHistoricaExistente = await _contextHistorico.CosechasHistorico
                    .Select(h => h.IdCosecha)
                    .ToListAsync();

                var cosechasFiltradas = cosechasParaTraspaso
                    .Where(c => !idsCosechaHistoricaExistente.Contains(c.IdCosecha))
                    .ToList();

                // 7. Traspasar cosechas
                await using var transHistCosechas = await _contextHistorico.Database.BeginTransactionAsync();
                try
                {
                    foreach (var cosecha in cosechasFiltradas)
                    {
                        var historica = new CosechasHistorico
                        {
                            IdCosecha = cosecha.IdCosecha,
                            IdPlantacion = cosecha.IdPlantacion,
                            FechaCosecha = cosecha.FechaCosecha,
                            NumSemCosecha = cosecha.NumSemCosecha,
                            FechaTraspaso = DateTime.Now
                        };
                        _contextHistorico.CosechasHistorico.Add(historica);
                    }

                    await _contextHistorico.SaveChangesAsync();
                    await transHistCosechas.CommitAsync();
                    cosechasTraspasoCount = cosechasFiltradas.Count;
                }
                catch (Exception ex)
                {
                    await transHistCosechas.RollbackAsync();
                    // Registrar el error
                    var errorLog = new ErrorLog
                    {
                        UserName = usuario,
                        ErrorMessage = ex.Message,
                        ErrorProcedure = "TraspasarCosechasProduccion",
                        ErrorLine = ex.StackTrace?.Split('\n')[0],
                        ErrorTime = DateTime.Now
                    };
                    _context.ErrorLogs.Add(errorLog);
                    await _context.SaveChangesAsync();
                    return StatusCode(500, new { Success = false, Message = $"Error al traspasar cosechas a históricos: {ex.Message}" });
                }

                // 8. Eliminar cosechas originales
                await using var transEliminarCosechas = await _context.Database.BeginTransactionAsync();
                try
                {
                    _context.Cosechas.RemoveRange(cosechasFiltradas);
                    await _context.SaveChangesAsync();
                    await transEliminarCosechas.CommitAsync();
                }
                catch (Exception ex)
                {
                    await transEliminarCosechas.RollbackAsync();
                    // Registrar el error
                    var errorLog = new ErrorLog
                    {
                        UserName = usuario,
                        ErrorMessage = ex.Message,
                        ErrorProcedure = "TraspasarCosechasProduccion",
                        ErrorLine = ex.StackTrace?.Split('\n')[0],
                        ErrorTime = DateTime.Now
                    };
                    _context.ErrorLogs.Add(errorLog);
                    await _context.SaveChangesAsync();
                    return StatusCode(500, new { Success = false, Message = $"Error al eliminar cosechas originales: {ex.Message}" });
                }

                return Ok(new
                {
                    Success = true,
                    Message = $"Se traspasaron {cosechasTraspasoCount} cosechas y {produccionesTraspasoCount} producciones a históricos.",
                    CosechasTraspasoCount = cosechasTraspasoCount,
                    ProduccionesTraspasoCount = produccionesTraspasoCount
                });
            }
            catch (Exception ex)
            {
                // Registrar el error general
                var errorLog = new ErrorLog
                {
                    UserName = usuario,
                    ErrorMessage = ex.Message,
                    ErrorProcedure = "TraspasarCosechasProduccion",
                    ErrorLine = ex.StackTrace?.Split('\n')[0],
                    ErrorTime = DateTime.Now
                };
                _context.ErrorLogs.Add(errorLog);
                await _context.SaveChangesAsync();
                return StatusCode(500, new { Success = false, Message = $"Error general: {ex.Message}" });
            }
        }


        // POST: api/Historicos/TraspasarTarimas
        [HttpPost("TraspasarTarimas")]
        public async Task<ActionResult<object>> TraspasarTarimas(int anio, string usuario)
        {
            try
            {
                // 1. Obtener viajes del año especificado
                var viajesDelAnio = await _context.Viajes
                    .Where(v => v.FechaSalida.Year == anio && v.EstadoAprobacion.Trim().ToLower() != "Pendiente")
                    .Select(v => v.IdViaje)
                    .ToListAsync();

                var tarimasParaTraspaso = _context.Tarimas
                    .AsEnumerable()
                    .Where(t => viajesDelAnio.Contains(t.IdViaje))
                    .ToList();

                if (!tarimasParaTraspaso.Any())
                {
                    return Ok(new { Success = false, Message = $"No se encontraron tarimas del año {anio} para traspasar." });
                }

                // 2. Filtrar tarimas para evitar duplicados en la tabla histórica
                var idsTarimaHistoricaExistente = await _contextHistorico.TarimasHistorico
                    .Select(h => h.IdTarima)
                    .ToListAsync();

                var tarimasFiltradas = tarimasParaTraspaso
                    .Where(t => !idsTarimaHistoricaExistente.Contains(t.IdTarima))
                    .ToList();

                // 3. Insertar en la tabla histórica primero
                await using (var transactionHistorico = await _contextHistorico.Database.BeginTransactionAsync())
                {
                    try
                    {
                        foreach (var tarima in tarimasFiltradas)
                        {
                            var tarimaHistorica = new TarimasHistorico
                            {
                                IdTarima = tarima.IdTarima,
                                IdProduccion = tarima.IdProduccion,
                                IdViaje = tarima.IdViaje,
                                CantidadCajasViaje = tarima.CantidadCajasViaje,
                                Licencia = tarima.Licencia,
                                KilosProcesoViaje = tarima.KilosProcesoViaje,
                                FechaTraspaso = DateTime.Now
                            };

                            _contextHistorico.TarimasHistorico.Add(tarimaHistorica);
                        }

                        // Guardar cambios en la tabla histórica
                        await _contextHistorico.SaveChangesAsync();
                        await transactionHistorico.CommitAsync();
                    }
                    catch (Exception ex)
                    {
                        await transactionHistorico.RollbackAsync();

                        // Registrar el error
                        var errorLog = new ErrorLog
                        {
                            UserName = usuario,
                            ErrorMessage = ex.Message,
                            ErrorProcedure = "TraspasarTarimas",
                            ErrorLine = ex.StackTrace?.Split('\n')[0],
                            ErrorTime = DateTime.Now
                        };
                        _context.ErrorLogs.Add(errorLog);
                        await _context.SaveChangesAsync();

                        return StatusCode(500, new { Success = false, Message = $"Error al guardar en históricos: {ex.Message}" });
                    }
                }

                // 4. Ahora eliminamos de la tabla original
                await using (var transaction = await _context.Database.BeginTransactionAsync())
                {
                    try
                    {
                        _context.Tarimas.RemoveRange(tarimasFiltradas);
                        await _context.SaveChangesAsync();
                        await transaction.CommitAsync();
                    }
                    catch (Exception ex)
                    {
                        await transaction.RollbackAsync();

                        // Registrar el error
                        var errorLog = new ErrorLog
                        {
                            UserName = usuario,
                            ErrorMessage = ex.Message,
                            ErrorProcedure = "TraspasarTarimas",
                            ErrorLine = ex.StackTrace?.Split('\n')[0],
                            ErrorTime = DateTime.Now
                        };
                        _context.ErrorLogs.Add(errorLog);
                        await _context.SaveChangesAsync();

                        return StatusCode(500, new { Success = false, Message = $"Error al eliminar registros originales: {ex.Message}" });
                    }
                }

                return Ok(new
                {
                    Success = true,
                    Message = $"Se traspasaron {tarimasFiltradas.Count} tarimas a históricos.",
                    TraspasoCount = tarimasFiltradas.Count
                });
            }
            catch (Exception ex)
            {
                // Registrar el error general
                var errorLog = new ErrorLog
                {
                    UserName = usuario,
                    ErrorMessage = ex.Message,
                    ErrorProcedure = "TraspasarTarimas",
                    ErrorLine = ex.StackTrace?.Split('\n')[0],
                    ErrorTime = DateTime.Now
                };
                _context.ErrorLogs.Add(errorLog);
                await _context.SaveChangesAsync();

                return StatusCode(500, new { Success = false, Message = $"Error general: {ex.Message}" });
            }
        }



        // POST: api/Historicos/TraspasarViajes
        [HttpPost("TraspasarViajes")]
        public async Task<ActionResult<object>> TraspasarViajes(int anio, string usuario)
        {
            try
            {
                // 1. Obtener viajes del año especificado
                var viajesParaTraspaso = await _context.Viajes
                    .Where(v => v.FechaSalida.Year == anio &&
                                v.EstadoAprobacion != null &&
                                v.EstadoAprobacion.Trim().ToLower() != "pendiente")
                    .ToListAsync();

                if (!viajesParaTraspaso.Any())
                {
                    return Ok(new { Success = false, Message = $"No se encontraron viajes del año {anio} para traspasar." });
                }

                // 2. Filtrar viajes para evitar duplicados en la tabla histórica
                var idsViajeHistoricaExistente = await _contextHistorico.ViajesHistorico
                    .Select(h => h.IdViaje)
                    .ToListAsync();

                var viajesFiltrados = viajesParaTraspaso
                    .Where(v => !idsViajeHistoricaExistente.Contains(v.IdViaje))
                    .ToList();

                // Hacemos cada operación en su propia transacción para evitar bloqueos entre DBs
                // 3. Insertar en la tabla histórica primero
                await using (var transactionHistorico = await _contextHistorico.Database.BeginTransactionAsync())
                {
                    try
                    {
                        foreach (var viaje in viajesFiltrados)
                        {
                            var viajeHistorico = new ViajesHistorico
                            {
                                IdViaje = viaje.IdViaje,
                                IdVehiculo = viaje.IdVehiculo,
                                IdConductor = viaje.IdConductor,
                                FechaSalida = viaje.FechaSalida,
                                NumSemViaje = viaje.NumSemViaje,
                                EstadoAprobacion = viaje.EstadoAprobacion,
                                FechaTraspaso = DateTime.Now
                            };

                            _contextHistorico.ViajesHistorico.Add(viajeHistorico);
                        }

                        // Guardar cambios en la tabla histórica
                        await _contextHistorico.SaveChangesAsync();
                        await transactionHistorico.CommitAsync();
                    }
                    catch (Exception ex)
                    {
                        await transactionHistorico.RollbackAsync();

                        // Registrar el error
                        var errorLog = new ErrorLog
                        {
                            UserName = usuario,
                            ErrorMessage = ex.Message,
                            ErrorProcedure = "TraspasarViajes",
                            ErrorLine = ex.StackTrace?.Split('\n')[0],
                            ErrorTime = DateTime.Now
                        };
                        _context.ErrorLogs.Add(errorLog);
                        await _context.SaveChangesAsync();

                        return StatusCode(500, new { Success = false, Message = $"Error al guardar en históricos: {ex.Message}" });
                    }
                }

                // 4. Ahora eliminamos de la tabla original
                await using (var transaction = await _context.Database.BeginTransactionAsync())
                {
                    try
                    {
                        _context.Viajes.RemoveRange(viajesFiltrados);
                        await _context.SaveChangesAsync();
                        await transaction.CommitAsync();
                    }
                    catch (Exception ex)
                    {
                        await transaction.RollbackAsync();

                        // Registrar el error
                        var errorLog = new ErrorLog
                        {
                            UserName = usuario,
                            ErrorMessage = ex.Message,
                            ErrorProcedure = "TraspasarViajes",
                            ErrorLine = ex.StackTrace?.Split('\n')[0],
                            ErrorTime = DateTime.Now
                        };
                        _context.ErrorLogs.Add(errorLog);
                        await _context.SaveChangesAsync();

                        return StatusCode(500, new { Success = false, Message = $"Error al eliminar registros originales: {ex.Message}" });
                    }
                }

                return Ok(new
                {
                    Success = true,
                    Message = $"Se traspasaron {viajesFiltrados.Count} viajes a históricos.",
                    TraspasoCount = viajesFiltrados.Count
                });
            }
            catch (Exception ex)
            {
                // Registrar el error
                var errorLog = new ErrorLog
                {
                    UserName = usuario,
                    ErrorMessage = ex.Message,
                    ErrorProcedure = "TraspasarViajes",
                    ErrorLine = ex.StackTrace?.Split('\n')[0],
                    ErrorTime = DateTime.Now
                };
                _context.ErrorLogs.Add(errorLog);
                await _context.SaveChangesAsync();

                return StatusCode(500, new { Success = false, Message = $"Error general: {ex.Message}" });
            }
        }


        // POST: api/Historicos/TraspasarVentasDetalles
        [HttpPost("TraspasarVentasDetalles")]
        public async Task<ActionResult<object>> TraspasarVentasDetalles(int anio, string usuario)
        {
            try
            {
                // 1. Obtener detalles de ventas del año especificado a través de la fecha de facturación de la venta
                var ventasIdsDelAnio = await _context.Ventas
                    .Where(v => v.FechaFacturacion.Year == anio)
                    .Select(v => v.IdVenta)
                    .ToListAsync();

                if (!ventasIdsDelAnio.Any())
                {
                    return Ok(new { Success = false, Message = $"No se encontraron ventas del año {anio} para traspasar detalles." });
                }

                var detallesParaTraspaso = (await _context.VentasDetalles.ToListAsync())
                .Where(d => ventasIdsDelAnio.Contains(d.IdVenta))
                .ToList();

                if (!detallesParaTraspaso.Any())
                {
                    return Ok(new { Success = false, Message = $"No se encontraron detalles de ventas del año {anio} para traspasar." });
                }

                // 2. Filtrar detalles para evitar duplicados en la tabla histórica
                var idsVentasDetallesHistoricos = await _contextHistorico.VentasDetallesHistorico
                    .Select(h => new { h.IdVenta, h.IdTarima })
                    .ToListAsync();

                var detallesFiltrados = detallesParaTraspaso
                    .Where(d => !idsVentasDetallesHistoricos
                        .Any(h => h.IdVenta == d.IdVenta && h.IdTarima == d.IdTarima))
                    .ToList();

                // 3. Insertar en la tabla histórica
                await using (var transactionHistorico = await _contextHistorico.Database.BeginTransactionAsync())
                {
                    try
                    {
                        foreach (var detalle in detallesFiltrados)
                        {
                            var detalleHistorico = new VentasDetallesHistorico
                            {
                                IdVenta = detalle.IdVenta,
                                IdTarima = detalle.IdTarima,
                                PrecioVentaTarima = detalle.PrecioVentaTarima,
                                FechaRecepcion = detalle.FechaRecepcion,
                                FechaTraspaso = DateTime.Now
                            };

                            _contextHistorico.VentasDetallesHistorico.Add(detalleHistorico);
                        }

                        await _contextHistorico.SaveChangesAsync();
                        await transactionHistorico.CommitAsync();
                    }
                    catch (Exception ex)
                    {
                        await transactionHistorico.RollbackAsync();
                        throw new Exception($"Error al guardar en históricos: {ex.Message}");
                    }
                }

                // 4. Eliminar de la tabla original
                await using (var transaction = await _context.Database.BeginTransactionAsync())
                {
                    try
                    {
                        _context.VentasDetalles.RemoveRange(detallesFiltrados);
                        await _context.SaveChangesAsync();
                        await transaction.CommitAsync();
                    }
                    catch (Exception ex)
                    {
                        await transaction.RollbackAsync();
                        throw new Exception($"Error al eliminar registros originales: {ex.Message}");
                    }
                }

                return Ok(new
                {
                    Success = true,
                    Message = $"Se traspasaron {detallesFiltrados.Count} detalles de ventas a históricos.",
                    TraspasoCount = detallesFiltrados.Count
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Success = false, Message = $"Error general: {ex.Message}" });
            }
        }


        // POST: api/Historicos/TraspasarVentas
        [HttpPost("TraspasarVentas")]
        public async Task<ActionResult<object>> TraspasarVentas(int anio, string usuario)
        {
            try
            {
                // 1. Obtener ventas del año especificado
                var ventasParaTraspaso = await _context.Ventas
                    .Where(v => v.FechaFacturacion.Year == anio)
                    .ToListAsync();

                if (!ventasParaTraspaso.Any())
                {
                    return Ok(new { Success = false, Message = $"No se encontraron ventas del año {anio} para traspasar." });
                }

                // 2. Filtrar ventas para evitar duplicados en la tabla histórica
                var idsVentasHistoricasExistentes = await _contextHistorico.VentasHistorico
                    .Select(h => h.IdVenta)
                    .ToListAsync();

                var ventasFiltradas = ventasParaTraspaso
                    .Where(v => !idsVentasHistoricasExistentes.Contains(v.IdVenta))
                    .ToList();

                // 3. Insertar en la tabla histórica
                await using (var transactionHistorico = await _contextHistorico.Database.BeginTransactionAsync())
                {
                    try
                    {
                        foreach (var venta in ventasFiltradas)
                        {
                            var ventaHistorica = new VentasHistorico
                            {
                                IdVenta = venta.IdVenta,
                                FechaFacturacion = venta.FechaFacturacion,
                                Total = venta.Total,
                                PrecioDolar = venta.PrecioDolar,
                                FechaTraspaso = DateTime.Now
                            };

                            _contextHistorico.VentasHistorico.Add(ventaHistorica);
                        }

                        await _contextHistorico.SaveChangesAsync();
                        await transactionHistorico.CommitAsync();
                    }
                    catch (Exception ex)
                    {
                        await transactionHistorico.RollbackAsync();
                        throw new Exception($"Error al guardar en históricos: {ex.Message}");
                    }
                }

                // 4. Eliminar de la tabla original
                await using (var transaction = await _context.Database.BeginTransactionAsync())
                {
                    try
                    {
                        _context.Ventas.RemoveRange(ventasFiltradas);
                        await _context.SaveChangesAsync();
                        await transaction.CommitAsync();
                    }
                    catch (Exception ex)
                    {
                        await transaction.RollbackAsync();
                        throw new Exception($"Error al eliminar registros originales: {ex.Message}");
                    }
                }

                return Ok(new
                {
                    Success = true,
                    Message = $"Se traspasaron {ventasFiltradas.Count} ventas a históricos.",
                    TraspasoCount = ventasFiltradas.Count
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Success = false, Message = $"Error general: {ex.Message}" });
            }
        }


        // PlantacionesController.cs
        [HttpGet("anios-disponibles")]
        public async Task<IActionResult> ObtenerAniosDisponibles()
        {
            var anios = await _context.Plantaciones
                .Where(p => p.FechaPlantacion != null)
                .Select(p => p.FechaPlantacion.Year)
                .Distinct()
                .OrderByDescending(a => a)
                .ToListAsync();

            return Ok(anios);
        }


    }
}
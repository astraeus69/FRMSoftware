using System.ComponentModel.DataAnnotations;

namespace FRMSoftware.Data
{
    // CATÁLOGOS
    // USUARIOS
    public class UsuariosDto
    {
        public int IdUsuario { get; set; }

        [Required(ErrorMessage = "El usuario es obligatorio")]
        [MaxLength(20, ErrorMessage = "El usuario no puede exceder los 20 caracteres")]
        public string Usuario { get; set; }

        [Required(ErrorMessage = "La contraseña es obligatoria")]
        [MaxLength(60, ErrorMessage = "La contraseña no puede exceder los 60 caracteres")]
        public string Contrasena { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio")]
        [MaxLength(100, ErrorMessage = "El nombre no puede exceder los 100 caracteres")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El teléfono es obligatorio")]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "El teléfono debe tener 10 dígitos")]
        public string Telefono { get; set; }

        [Required(ErrorMessage = "El email es obligatorio")]
        [EmailAddress(ErrorMessage = "Ingrese un correo válido")]
        [MaxLength(40, ErrorMessage = "El email no puede exceder los 40 caracteres")]
        public string Email { get; set; }

        [Required(ErrorMessage = "El rol es obligatorio")]
        [MaxLength(15, ErrorMessage = "El rol no puede exceder los 15 caracteres")]
        public string Rol { get; set; }

        [Required(ErrorMessage = "El estatus es obligatorio")]
        [MaxLength(20, ErrorMessage = "El estatus no puede exceder los 20 caracteres")]
        public string Estatus { get; set; }
    }


    public class LoginDto
    {
        [Required(ErrorMessage = "El nombre de usuario es obligatorio.")]
        public string Usuario { get; set; }

        [Required(ErrorMessage = "La contraseña es obligatoria.")]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "La contraseña debe tener entre 6 y 20 caracteres.")]
        [RegularExpression(@"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d@#$%&*!]+$",
            ErrorMessage = "La contraseña debe contener al menos una letra, un número y solo puede usar letras, números y @#$%&*!")]
        public string Contrasena { get; set; }
    }


    public class LoginResponse
    {
        public int IdUsuario { get; set; }
        public string Usuario { get; set; }
        public string Rol { get; set; }
        public string Nombre { get; set; }
        public string Token { get; set; }
    }

    public class UserSession
    {
        public int IdUsuario { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Rol { get; set; } = string.Empty;
        public string Usuario { get; set; } = string.Empty;
    }

    public class CambioContrasenaModel
    {
        [Required(ErrorMessage = "La contraseña actual es requerida")]
        public string ContrasenaActual { get; set; } = string.Empty;

        [Required(ErrorMessage = "La nueva contraseña es requerida")]
        public string NuevaContrasena { get; set; } = string.Empty;

        [Required(ErrorMessage = "Confirma la nueva contraseña")]
        public string ConfirmarNuevaContrasena { get; set; } = string.Empty;
    }

    public class ValidarContrasenaRequest
    {
        public int IdUsuario { get; set; }
        public string ContrasenaActual { get; set; } = string.Empty;
    }


    // EMPLEADOS
    public class EmpleadosDto
    {
        public int IdEmpleado { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio")]
        [MaxLength(100, ErrorMessage = "El nombre no puede exceder los 100 caracteres")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El departamento es obligatorio")]
        [MaxLength(50, ErrorMessage = "El departamento no puede exceder los 50 caracteres")]
        public string Departamento { get; set; }

        [Required(ErrorMessage = "El puesto es obligatorio")]
        [MaxLength(50, ErrorMessage = "El puesto no puede exceder los 50 caracteres")]
        public string Puesto { get; set; }

        [Required(ErrorMessage = "La dirección es obligatoria")]
        [MaxLength(100, ErrorMessage = "La dirección no puede exceder los 100 caracteres")]
        public string Direccion { get; set; }

        [Required(ErrorMessage = "La colonia es obligatoria")]
        [MaxLength(50, ErrorMessage = "La colonia no puede exceder los 50 caracteres")]
        public string Colonia { get; set; }

        [Required(ErrorMessage = "El código postal es obligatorio")]
        [RegularExpression(@"^\d{5}$", ErrorMessage = "El código postal debe tener 5 dígitos")]
        [MaxLength(5, ErrorMessage = "El código postal no puede exceder los 5 caracteres")]
        public string CP { get; set; }

        [Required(ErrorMessage = "El municipio es obligatorio")]
        [MaxLength(50, ErrorMessage = "El municipio no puede exceder los 50 caracteres")]
        public string Municipio { get; set; }

        [Required(ErrorMessage = "El estado es obligatorio")]
        [MaxLength(50, ErrorMessage = "El estado no puede exceder los 50 caracteres")]
        public string Estado { get; set; }

        [Required(ErrorMessage = "El teléfono es obligatorio")]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "El teléfono debe tener 10 dígitos")]
        public string Telefono { get; set; }

        [Required(ErrorMessage = "El estatus es obligatorio")]
        [MaxLength(20, ErrorMessage = "El estatus no puede exceder los 20 caracteres")]
        public string Estatus { get; set; }
    }


    // CULTIVOS
    public class CultivosDto
    {
        public int IdCultivo { get; set; }

        [Required(ErrorMessage = "El tipo de berry es obligatorio")]
        [MaxLength(50, ErrorMessage = "El tipo de berry no puede exceder los 50 caracteres")]
        public string TipoBerry { get; set; }

        [Required(ErrorMessage = "La variedad es obligatoria")]
        [MaxLength(50, ErrorMessage = "La variedad no puede exceder los 50 caracteres")]
        public string Variedad { get; set; }
    }


    // VIVEROS
    public class ViverosDto
    {
        public int IdVivero { get; set; }

        [Required(ErrorMessage = "El nombre del vivero es obligatorio")]
        [MaxLength(50, ErrorMessage = "El nombre del vivero no puede exceder los 50 caracteres")]
        public string NombreVivero { get; set; }

        [Required(ErrorMessage = "El código del vivero es obligatorio")]
        [MaxLength(20, ErrorMessage = "El código del vivero no puede exceder los 20 caracteres")]
        public string CodigoVivero { get; set; }
    }


    // RANCHOS
    public class RanchosDto
    {
        public int IdRancho { get; set; }

        [Required(ErrorMessage = "El nombre del rancho es obligatorio")]
        [MaxLength(60, ErrorMessage = "El nombre del rancho no puede exceder los 60 caracteres")]
        public string NombreRancho { get; set; }

        [Required(ErrorMessage = "El número del rancho es obligatorio")]
        [MaxLength(60, ErrorMessage = "El número del rancho no puede exceder los 60 caracteres")]
        public string NumeroRancho { get; set; }

        [Required(ErrorMessage = "La superficie en hectáreas es obligatoria")]
        public decimal? SuperficieHa { get; set; }

        [Required(ErrorMessage = "La superficie en acres es obligatoria")]
        public decimal? SuperficieAcres { get; set; }

        [Required(ErrorMessage = "La dirección es obligatoria")]
        [MaxLength(100, ErrorMessage = "La dirección no puede exceder los 100 caracteres")]
        public string Direccion { get; set; }

        [Required(ErrorMessage = "El código postal es obligatorio")]
        [RegularExpression(@"^\d{5}$", ErrorMessage = "El código postal debe tener 5 dígitos")]
        [MaxLength(5, ErrorMessage = "El código postal no puede exceder los 5 caracteres")]
        public string CP { get; set; }

        [Required(ErrorMessage = "El municipio es obligatorio")]
        [MaxLength(30, ErrorMessage = "El municipio no puede exceder los 30 caracteres")]
        public string Municipio { get; set; }

        [Required(ErrorMessage = "El estado es obligatorio")]
        [MaxLength(30, ErrorMessage = "El estado no puede exceder los 30 caracteres")]
        public string Estado { get; set; }
    }


    // VEHÍCULOS
    public class VehiculosDto
    {
        public int IdVehiculo { get; set; }

        [Required(ErrorMessage = "Las placas son obligatorias")]
        [MaxLength(10, ErrorMessage = "Las placas no pueden exceder los 10 caracteres")]
        public string Placas { get; set; }

        [Required(ErrorMessage = "El modelo es obligatorio")]
        [MaxLength(20, ErrorMessage = "El modelo no puede exceder los 20 caracteres")]
        public string Modelo { get; set; }

        [Required(ErrorMessage = "La marca es obligatoria")]
        [MaxLength(20, ErrorMessage = "La marca no puede exceder los 20 caracteres")]
        public string Marca { get; set; }
    }



    // MOVIMIENTOS
    // LLAVES
    public class LlavesDto
    {
        public int IdLlave { get; set; }

        [Required(ErrorMessage = "El rancho es obligatorio")]
        public int IdRancho { get; set; }

        [Required(ErrorMessage = "El nombre de la llave es obligatorio")]
        [MaxLength(50, ErrorMessage = "El nombre de la llave no puede exceder los 50 caracteres")]
        public string NombreLlave { get; set; }

        [Required(ErrorMessage = "La superficie en hectáreas es obligatoria")]
        public decimal? SuperficieHa { get; set; }

        [Required(ErrorMessage = "La superficie en acres es obligatoria")]
        public decimal? SuperficieAcres { get; set; }

        [Required(ErrorMessage = "La cantidad de túneles es obligatoria")]
        public int? CantidadTuneles { get; set; }

        [Required(ErrorMessage = "La disponibilidad es obligatoria")]
        [MaxLength(20, ErrorMessage = "La disponibilidad no puede exceder los 20 caracteres")]
        public string Disponibilidad { get; set; }
    }

    public class LlaveConRanchoDto
    {
        public int IdLlave { get; set; }
        public int IdRancho { get; set; }
        public string NombreLlave { get; set; }
        public decimal? SuperficieHa { get; set; }
        public decimal? SuperficieAcres { get; set; }
        public int CantidadTuneles { get; set; }
        public string Disponibilidad { get; set; }

        // Propiedades de Rancho
        public string NombreRancho { get; set; }
        public string NumeroRancho { get; set; }
    }



    // PLANTACIONES
    public class PlantacionesDto
    {
        public int IdPlantacion { get; set; }

        [Required(ErrorMessage = "El cultivo es obligatorio")]
        public int IdCultivo { get; set; }

        [Required(ErrorMessage = "La llave es obligatoria")]
        public int IdLlave { get; set; }

        [Required(ErrorMessage = "El vivero es obligatorio")]
        public int IdVivero { get; set; }

        [Required(ErrorMessage = "La cantidad de plantas es obligatoria")]
        public int? CantidadPlantas { get; set; }

        [Required(ErrorMessage = "Las plantas por metro son obligatorias")]
        public decimal? PlantasPorMetro { get; set; }

        [Required(ErrorMessage = "La fecha de plantación es obligatoria")]
        public DateTime? FechaPlantacion { get; set; } = DateTime.Now;
        
        [Required(ErrorMessage = "El número de semana es obligatorio")]
        public int? NumSemPlantacion { get; set; }

        [Required(ErrorMessage = "El estatus de la plantación es obligatorio")]
        [MaxLength(20, ErrorMessage = "El estatus no puede exceder los 20 caracteres")]
        public string? EstatusPlantacion { get; set; }
    }

    public class PlantacionesDetallesDto
    {
        public int IdPlantacion { get; set; }

        // Datos del Cultivo
        public int IdCultivo { get; set; }
        public string TipoBerry { get; set; }
        public string Variedad { get; set; }

        // Datos de la Llave
        public int IdLlave { get; set; }
        public string NombreLlave { get; set; }
        public decimal? SuperficieHa { get; set; }
        public decimal? SuperficieAcres { get; set; }
        public string Disponibilidad { get; set; }

        // Datos del Rancho
        public int IdRancho { get; set; }
        public string NombreRancho { get; set; }
        public string NumeroRancho { get; set; }

        // Datos del Vivero
        public int IdVivero { get; set; }
        public string NombreVivero { get; set; }
        public string CodigoVivero { get; set; }

        // Datos de la Plantación
        public int? CantidadPlantas { get; set; }
        public decimal? PlantasPorMetro { get; set; }
        public DateTime FechaPlantacion { get; set; }
        public int? NumSemPlantacion { get; set; }
        public string EstatusPlantacion { get; set; }
    }


    // REPLANTES
    public class ReplantesDto
    {
        public int IdReplante { get; set; }

        [Required(ErrorMessage = "La plantación es obligatoria")]
        public int IdPlantacion { get; set; }

        [Required(ErrorMessage = "El cultivo es obligatorio")]
        public int IdCultivo { get; set; }

        [Required(ErrorMessage = "El vivero es obligatorio")]
        public int IdVivero { get; set; }

        [Required(ErrorMessage = "La cantidad de replante es obligatoria")]
        public int? CantidadReplante { get; set; }

        [Required(ErrorMessage = "La fecha de replante es obligatoria")]
        public DateTime FechaReplante { get; set; } = DateTime.Now;
        
        [Required(ErrorMessage = "El número de semana es obligatorio")]
        public int? NumSemReplante { get; set; }

    }

    public class ReplantesDetallesDto
    {
        // Datos del Replante
        public int IdReplante { get; set; }
        public int? CantidadReplante { get; set; }
        public DateTime FechaReplante { get; set; } = DateTime.Now;
        public int NumSemReplante { get; set; }


        // Datos de la Plantación
        public int IdPlantacion { get; set; }
        public DateTime FechaPlantacion { get; set; }
        public int NumSemPlantacion { get; set; }
        public string? EstatusPlantacion { get; set; }

        // Datos del Cultivo
        public int IdCultivo { get; set; }
        public string? TipoBerry { get; set; }
        public string? Variedad { get; set; }

        // Datos de la Llave
        public int IdLlave { get; set; }
        public string? NombreLlave { get; set; }
        public decimal? SuperficieHa { get; set; }
        public decimal? SuperficieAcres { get; set; }
        public string? Disponibilidad { get; set; }

        // Datos del Rancho
        public int IdRancho { get; set; }
        public string? NombreRancho { get; set; }
        public string? NumeroRancho { get; set; }

        // Datos del Vivero
        public int IdVivero { get; set; }
        public string? NombreVivero { get; set; }
        public string? CodigoVivero { get; set; }

    }



    // PODAS
    public class PodasDto
    {
        public int IdPoda { get; set; }

        [Required(ErrorMessage = "La plantación es obligatoria")]
        public int IdPlantacion { get; set; }

        [Required(ErrorMessage = "El tipo de poda es obligatorio")]
        [MaxLength(50, ErrorMessage = "El tipo de poda no puede exceder los 50 caracteres")]
        public string TipoPoda { get; set; }

        [Required(ErrorMessage = "La fecha de poda es obligatoria")]
        public DateTime FechaPoda { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "El número de semana es obligatorio")]
        public int? NumSemPoda { get; set; }

    }


    public class PodasDetallesDto
    {
        // Datos del Replante
        public int IdPoda { get; set; }
        public string? TipoPoda { get; set; }
        public DateTime FechaPoda { get; set; } = DateTime.Now;
        public int NumSemPoda { get; set; }


        // Datos de la Plantación
        public int IdPlantacion { get; set; }
        public DateTime FechaPlantacion { get; set; }
        public int NumSemPlantacion { get; set; }
        public string? EstatusPlantacion { get; set; }

        // Datos del Cultivo
        public int IdCultivo { get; set; }
        public string? TipoBerry { get; set; }
        public string? Variedad { get; set; }

        // Datos de la Llave
        public int IdLlave { get; set; }
        public string? NombreLlave { get; set; }
        public decimal? SuperficieHa { get; set; }
        public decimal? SuperficieAcres { get; set; }
        public string? Disponibilidad { get; set; }

        // Datos del Rancho
        public int IdRancho { get; set; }
        public string? NombreRancho { get; set; }
        public string? NumeroRancho { get; set; }

    }


    // COSECHAS
    public class CosechasDto
    {
        public int IdCosecha { get; set; }

        public int IdPlantacion { get; set; }

        [Required(ErrorMessage = "La fecha de cosecha es obligatoria")]
        public DateTime FechaCosecha { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "El número de semana es obligatorio")]
        public int? NumSemCosecha { get; set; }

    }


    // PRODUCCIÓN
    public class ProduccionDto
    {
        public int IdProduccion { get; set; }

        public int IdCosecha { get; set; }

        [Required(ErrorMessage = "El tipo de caja es obligatorio")]
        [MaxLength(10, ErrorMessage = "El tipo de caja no puede exceder los 10 caracteres")]
        public string TipoCaja { get; set; } = string.Empty;

        [Required(ErrorMessage = "La cantidad de cajas es obligatoria")]
        public int? CantidadCajas { get; set; }

        [Required(ErrorMessage = "Los kilos de proceso son obligatorios")]
        [Range(0, double.MaxValue, ErrorMessage = "Los kilos deben ser un número positivo")]
        public decimal? KilosProceso { get; set; }
    }


    public class CosechasProduccionDto
    {
        // Cosecha
        public int IdCosecha { get; set; }
        public DateTime FechaCosecha { get; set; }
        public int NumSemCosecha { get; set; }


        // Producción
        public int IdProduccion { get; set; }
        public string TipoCaja { get; set; } = string.Empty;
        public int? CantidadCajas { get; set; }
        public decimal? KilosProceso { get; set; }


        // Datos de la Plantación
        public int IdPlantacion { get; set; }
        public DateTime FechaPlantacion { get; set; }
        public int NumSemPlantacion { get; set; }
        public string? EstatusPlantacion { get; set; }

        // Datos del Cultivo
        public int IdCultivo { get; set; }
        public string? TipoBerry { get; set; }
        public string? Variedad { get; set; }

        // Datos de la Llave
        public int IdLlave { get; set; }
        public string? NombreLlave { get; set; }
        public decimal? SuperficieHa { get; set; }
        public decimal? SuperficieAcres { get; set; }
        public string? Disponibilidad { get; set; }

        // Datos del Rancho
        public int IdRancho { get; set; }
        public string? NombreRancho { get; set; }
        public string? NumeroRancho { get; set; }
    }

    public class ProcesoDto
    {
        public int IdProceso { get; set; }

        public int IdViaje { get; set; }

        [Required(ErrorMessage = "Los kilos de Clase A son obligatorios")]
        [Range(0, double.MaxValue, ErrorMessage = "Los kilos deben ser un número positivo")]
        public decimal? ClaseAkg { get; set; }

        [Required(ErrorMessage = "Los kilos de Clase B son obligatorios")]
        [Range(0, double.MaxValue, ErrorMessage = "Los kilos deben ser un número positivo")]
        public decimal? ClaseBkg { get; set; }

        [Required(ErrorMessage = "Los kilos de Clase C son obligatorios")]
        [Range(0, double.MaxValue, ErrorMessage = "Los kilos deben ser un número positivo")]
        public decimal? ClaseCkg { get; set; }

        [Required(ErrorMessage = "Los kilos de rechazo son obligatorios")]
        [Range(0, double.MaxValue, ErrorMessage = "Los kilos deben ser un número positivo")]
        public decimal? Rechazo { get; set; }
    }


    public class ProcesoDetallesDto
    {
        // Proceso
        public int IdProceso { get; set; }
        public decimal? ClaseAkg { get; set; }
        public decimal? ClaseBkg { get; set; }
        public decimal? ClaseCkg { get; set; }
        public decimal? Rechazo { get; set; }

        // Viaje
        public int IdViaje { get; set; }
        public DateTime FechaSalida { get; set; }
        public int NumSemViaje { get; set; }
        public decimal KilosProcesoViaje { get; set; }

        // Cosecha
        public int IdCosecha { get; set; }
        public DateTime FechaCosecha { get; set; }
        public int NumSemCosecha { get; set; }


        // Producción
        public int IdProduccion { get; set; }
        public string TipoCaja { get; set; } = string.Empty;
        public int? CantidadCajas { get; set; }
        public decimal? KilosProceso { get; set; }


        // Datos de la Plantación
        public int IdPlantacion { get; set; }
        public string? EstatusPlantacion { get; set; }

        // Datos del Cultivo
        public int IdCultivo { get; set; }
        public string? TipoBerry { get; set; }
        public string? Variedad { get; set; }

        // Datos de la Llave
        public int IdLlave { get; set; }
        public string? NombreLlave { get; set; }
        public decimal? SuperficieHa { get; set; }
        public decimal? SuperficieAcres { get; set; }
        public string? Disponibilidad { get; set; }

        // Datos del Rancho
        public int IdRancho { get; set; }
        public string? NombreRancho { get; set; }
        public string? NumeroRancho { get; set; }

    }

    // PERSONAL DE COSECHA
    public class PersonalCosechaDto
    {
        public int IdPersonalCosecha { get; set; }

        public int IdCosecha { get; set; }

        [Required(ErrorMessage = "El empleado es obligatorio")]
        public int IdEmpleado { get; set; }

        [Required(ErrorMessage = "La cantidad de jarras es obligatoria")]
        public int? Jarras { get; set; }

        [Required(ErrorMessage = "El precio por jarra es obligatorio")]
        [Range(0, double.MaxValue, ErrorMessage = "El precio debe ser un número positivo")]
        public decimal? PrecioJarra { get; set; }
    }

    public class PersonalCosechaDetallesDto
    {
        // Datos de jarras
        public int IdPersonalCosecha { get; set; }
        public int? Jarras { get; set; }
        public decimal? PrecioJarra { get; set; }


        // Cosecha
        public int IdCosecha { get; set; }
        public DateTime FechaCosecha { get; set; }
        public int NumSemCosecha { get; set; }


        // Datos del Cultivo
        public int IdCultivo { get; set; }
        public string? TipoBerry { get; set; }
        public string? Variedad { get; set; }


        // Datos del empleado
        public int IdEmpleado { get; set; }
        public string Nombre { get; set; }
        public string Telefono { get; set; }
        public string Estatus { get; set; }
    }




    // TARIMAS
    public class TarimasDto
    {
        public int IdTarima { get; set; }

        public int IdProduccion { get; set; }

        public int IdViaje { get; set; }

        [Required(ErrorMessage = "La cantidad de cajas es obligatoria")]
        public int? CantidadCajasViaje { get; set; }

        [Required(ErrorMessage = "La licencia es obligatoria")]
        public int? Licencia { get; set; }

        [Required(ErrorMessage = "La licencia es obligatoria")]
        public decimal? KilosProcesoViaje { get; set; }
    }

    // VIAJES
    public class ViajesDto
    {
        public int IdViaje { get; set; }

        [Required(ErrorMessage = "El vehículo es obligatorio")]
        public int IdVehiculo { get; set; }

        [Required(ErrorMessage = "El conductor es obligatorio")]
        public int IdConductor { get; set; }

        [Required(ErrorMessage = "La fecha de salida es obligatoria")]
        public DateOnly FechaSalida { get; set; } = DateOnly.FromDateTime(DateTime.Now);
        
        [Required(ErrorMessage = "El número de semana es obligatorio")]
        public int NumSemViaje { get; set; }

        [Required(ErrorMessage = "El estado es obligatorio")]
        [MaxLength(30, ErrorMessage = "El estado no puede exceder los 30 caracteres")]
        public string EstadoAprobacion { get; set; } = string.Empty;


    }

    // CONDUCTORES
    public class ConductoresDto
    {
        public int IdConductor { get; set; }

        public int IdEmpleado { get; set; }
    }


    public class ViajesDetallesDto
    {
        // Viaje
        public int IdViaje { get; set; }
        public DateOnly FechaSalida { get; set; }
        public int NumSemViaje { get; set; }
        public string EstadoAprobacion { get; set; } = string.Empty;

        // Datos de viaje
        public int IdTarima { get; set; }
        public int CantidadCajasViaje { get; set; }
        public int Licencia { get; set; }
        public decimal KilosProcesoViaje { get; set; }

        // Cosecha
        public int IdCosecha { get; set; }
        public DateTime FechaCosecha { get; set; }
        public int NumSemCosecha { get; set; }


        // Producción
        public int IdProduccion { get; set; }
        public string TipoCaja { get; set; } = string.Empty;
        public int? CantidadCajas { get; set; }
        public decimal? KilosProceso { get; set; }

        // Datos del Cultivo
        public int IdCultivo { get; set; }
        public string? TipoBerry { get; set; }
        public string? Variedad { get; set; }

        // Rancho
        public int IdRancho { get; set; }
        public string? NombreRancho { get; set; }
        public string? NumeroRancho { get; set; }

        // Datos del empleado
        public int IdEmpleado { get; set; }
        public string Nombre { get; set; }
        public string Telefono { get; set; }

        // Datos del Vehículo
        public int IdVehiculo { get; set; }
        public string Placas { get; set; }
        public string Modelo { get; set; }
        public string Marca { get; set; }
    }



    // RECEPCIÓN DE VIAJES
    public partial class RecepcionViajes
    {
        public int IdRecepcion { get; set; }

        public int IdViaje { get; set; }

        [Required(ErrorMessage = "La fecha de recibo es obligatoria")]
        public DateOnly FechaRecepcion { get; set; }

        [Required(ErrorMessage = "El número de semana es obligatorio")]
        public int NumSemRecepcion { get; set; }

        [Required(ErrorMessage = "La hora de recibo es obligatoria")]
        public TimeOnly HoraRecepcion { get; set; }

        [Required(ErrorMessage = "La hora de inspección es obligatoria")]
        public TimeOnly HoraInspeccion { get; set; }
    }



    // VENTAS
    public class VentasDto
    {
        public int IdVenta { get; set; }

        [Required(ErrorMessage = "La fecha de facturación es obligatoria")]
        public DateOnly FechaFacturacion { get; set; } = DateOnly.FromDateTime(DateTime.Now);

        [Required(ErrorMessage = "El total es obligatorio")]
        [Range(18, 2)]
        public decimal Total { get; set; }

        [Required(ErrorMessage = "El precio del dólar es obligatorio")]
        [Range(18, 2)]
        public decimal? PrecioDolar { get; set; }
    }



    // VENTAS DETALLES
    public class VentasDetallesDto
    {
        [Required(ErrorMessage = "El ID de venta es obligatorio")]
        public int IdVenta { get; set; }

        [Required(ErrorMessage = "El ID de tarima es obligatorio")]
        public int IdTarima { get; set; }

        public int? CantidadCajasViaje { get; set; }

        [Required(ErrorMessage = "La licencia es obligatoria")]
        public int? Licencia { get; set; }

        [Required(ErrorMessage = "La licencia es obligatoria")]
        public decimal? KilosProcesoViaje { get; set; }

        [Required(ErrorMessage = "El precio de venta de la tarima es obligatorio")]
        [Range(18, 2)]
        public decimal? PrecioVentaTarima { get; set; }

        [Required(ErrorMessage = "La fecha de recepción es obligatoria")]
        public DateOnly FechaRecepcion { get; set; } = DateOnly.FromDateTime(DateTime.Now);
    }


    public class VentasDetallesCompletosDto
    {
        public int IdVenta { get; set; }
        public DateOnly FechaFacturacion { get; set; }
        public decimal TotalVenta { get; set; }
        public decimal PrecioDolar { get; set; }

        // Datos de viaje
        public int IdViaje { get; set; }
        public DateOnly FechaSalida { get; set; }
        public int NumSemViaje { get; set; }

        // Rancho
        public int IdRancho { get; set; }
        public string? NombreRancho { get; set; }
        public string? NumeroRancho { get; set; }

        // Cultivo
        public int IdCultivo { get; set; }
        public string? TipoBerry { get; set; }
        public string? Variedad { get; set; }

        // Datos de trima
        public int IdTarima { get; set; }
        public int CantidadCajasViaje { get; set; }
        public int Licencia { get; set; }
        public decimal KilosProcesoViaje { get; set; }

        public DateTime FechaRecepcion { get; set; }
        public decimal PrecioVentaTarima { get; set; }
    }


    // UTILIDADES: MANTENIMIENTO
    public class ErrorLogDto
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string ErrorMessage { get; set; }
        public string ErrorProcedure { get; set; }
        public string ErrorLine { get; set; }
        public DateTime ErrorTime { get; set; }
    }
}

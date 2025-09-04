namespace APIdbConection.Models.Movimientos
{
    public class ViajesDetalles
    {
        // Viaje
        public int IdViaje { get; set; }
        public DateOnly FechaSalida { get; set; }
        public int NumSemViaje { get; set; }
        public string EstadoAprobacion { get; set; } = null!;


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
}

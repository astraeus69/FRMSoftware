namespace APIdbConection.Models.Movimientos
{
    public class ProcesoDetalles
    {
        // Proceso
        public int IdProceso { get; set; }
        public decimal? ClaseAkg { get; set; }
        public decimal? ClaseBkg { get; set; }
        public decimal? ClaseCkg { get; set; }
        public decimal? Rechazo { get; set; }

        // Viaje
        public int IdViaje { get; set; }
        public DateOnly FechaSalida { get; set; }
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
}

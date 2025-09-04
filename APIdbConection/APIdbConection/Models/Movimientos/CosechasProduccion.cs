namespace APIdbConection.Models.Movimientos
{
    public class CosechasProduccion
    {
        // Cosecha
        public int IdCosecha { get; set; }
        public DateTime FechaCosecha { get; set; }
        public int NumSemCosecha { get; set; }


        // Producción
        public int IdProduccion { get; set; }
        public string TipoCaja { get; set; } = string.Empty;
        public int? CantidadCajas { get; set; }
        public string ClaseProceso { get; set; } = string.Empty;
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
}

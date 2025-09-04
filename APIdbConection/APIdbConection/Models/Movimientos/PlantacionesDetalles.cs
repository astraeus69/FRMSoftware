namespace APIdbConection.Models.Movimientos
{
    public class PlantacionesDetalles
    {
        // Datos de la Plantación
        public int IdPlantacion { get; set; }
        public int? CantidadPlantas { get; set; }
        public decimal? PlantasPorMetro { get; set; }
        public DateTime FechaPlantacion { get; set; }
        public int NumSemPlantacion { get; set; }
        public string EstatusPlantacion { get; set; } = null!;

        // Datos del Cultivo
        public int IdCultivo { get; set; }
        public string TipoBerry { get; set; } = null!;
        public string Variedad { get; set; } = null!;

        // Datos del Vivero
        public int IdVivero { get; set; }
        public string NombreVivero { get; set; } = null!;
        public string CodigoVivero { get; set; } = null!;

        // Datos de la Llave
        public int IdLlave { get; set; }
        public string NombreLlave { get; set; } = null!;
        public decimal? SuperficieHa { get; set; }
        public decimal? SuperficieAcres { get; set; }
        public string Disponibilidad { get; set; } = null!;

        // Datos del Rancho
        public int IdRancho { get; set; }
        public string NombreRancho { get; set; } = null!;
        public string NumeroRancho { get; set; } = null!;

    }
}

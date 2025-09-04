namespace APIdbConection.Models.Utilidades
{
    public class PlantacionesHistorico
    {
        public int IdPlantacion { get; set; }
        public int IdCultivo { get; set; }
        public int IdVivero { get; set; }
        public int IdLlave { get; set; }
        public int? CantidadPlantas { get; set; }
        public decimal? PlantasPorMetro { get; set; }
        public DateTime FechaPlantacion { get; set; }
        public int NumSemPlantacion { get; set; }
        public string EstatusPlantacion { get; set; }
        public DateTime FechaTraspaso { get; set; }
    }
}

namespace APIdbConection.Models.Utilidades
{
    public class PodasHistorico
    {
        public int IdPoda { get; set; }
        public int IdPlantacion { get; set; }
        public string TipoPoda { get; set; }
        public DateTime FechaPoda { get; set; }
        public int NumSemPoda { get; set; }
        public DateTime FechaTraspaso { get; set; }
    }
}

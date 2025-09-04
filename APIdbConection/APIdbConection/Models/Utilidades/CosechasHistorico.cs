namespace APIdbConection.Models.Utilidades
{
    public class CosechasHistorico
    {
        public int IdCosecha { get; set; }
        public int IdPlantacion { get; set; }
        public DateTime FechaCosecha { get; set; }
        public int NumSemCosecha { get; set; }
        public DateTime FechaTraspaso { get; set; }
    }
}

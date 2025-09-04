namespace APIdbConection.Models.Utilidades
{
    public class ReplantesHistorico
    {
        public int IdReplante { get; set; }
        public int IdPlantacion { get; set; }
        public int IdCultivo { get; set; }
        public int IdVivero { get; set; }
        public int CantidadReplante { get; set; }
        public DateTime FechaReplante { get; set; }
        public int NumSemReplante { get; set; }
        public DateTime FechaTraspaso { get; set; }
    }
}

namespace APIdbConection.Models.Utilidades
{
    public class ProcesoHistorico
    {
        public int IdProceso { get; set; }
        public int IdViaje { get; set; }
        public decimal ClaseAkg { get; set; }
        public decimal ClaseBkg { get; set; }
        public decimal ClaseCkg { get; set; }
        public decimal Rechazo { get; set; }
        public DateTime FechaTraspaso { get; set; }
    }
}

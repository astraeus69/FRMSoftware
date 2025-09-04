namespace APIdbConection.Models.Utilidades
{
    public class LlavesHistorico
    {
        public int IdLlave { get; set; }
        public int IdRancho { get; set; }
        public string NombreLlave { get; set; }
        public decimal SuperficieHA { get; set; }
        public decimal SuperficieAcres { get; set; }
        public int CantidadTuneles { get; set; }
        public string Disponibilidad { get; set; }
        public DateTime FechaTraspaso { get; set; }
    }
}

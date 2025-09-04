namespace APIdbConection.Models.Utilidades
{
    public class RanchosHistorico
    {
        public int IdRancho { get; set; }
        public string NombreRancho { get; set; }
        public string NumeroRancho { get; set; }
        public decimal SuperficieHA { get; set; }
        public decimal SuperficieAcres { get; set; }
        public string Direccion { get; set; }
        public string CP { get; set; }
        public string Municipio { get; set; }
        public string Estado { get; set; }
        public DateTime FechaTraspaso { get; set; }
    }
}

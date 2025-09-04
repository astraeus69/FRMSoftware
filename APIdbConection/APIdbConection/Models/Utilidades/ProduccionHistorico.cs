namespace APIdbConection.Models.Utilidades
{
    public class ProduccionHistorico
    {
        public int IdProduccion { get; set; }
        public int IdCosecha { get; set; }
        public string TipoCaja { get; set; }
        public int CantidadCajas { get; set; }
        public decimal KilosProceso { get; set; }
        public DateTime FechaTraspaso { get; set; }
    }
}

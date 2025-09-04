namespace APIdbConection.Models.Utilidades
{
    public class TarimasHistorico
    {
        public int IdTarima { get; set; }
        public int IdProduccion { get; set; }
        public int IdViaje { get; set; }
        public int CantidadCajasViaje { get; set; }
        public int Licencia { get; set; }
        public decimal KilosProcesoViaje { get; set; }
        public DateTime FechaTraspaso { get; set; }
    }
}

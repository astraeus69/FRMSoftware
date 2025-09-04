namespace APIdbConection.Models.Utilidades
{
    public class VentasDetallesHistorico
    {
        public int IdVenta { get; set; }
        public int IdTarima { get; set; }
        public decimal PrecioVentaTarima { get; set; }
        public DateTime FechaRecepcion { get; set; }
        public DateTime FechaTraspaso { get; set; }

    }
}

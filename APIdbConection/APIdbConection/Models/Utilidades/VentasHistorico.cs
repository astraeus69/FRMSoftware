namespace APIdbConection.Models.Utilidades
{
    public class VentasHistorico
    {
        public int IdVenta { get; set; }
        public DateOnly FechaFacturacion { get; set; }
        public decimal Total { get; set; }
        public decimal PrecioDolar { get; set; }
        public DateTime FechaTraspaso { get; set; }

    }
}
